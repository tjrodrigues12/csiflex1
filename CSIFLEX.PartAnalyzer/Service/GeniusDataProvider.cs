using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSIFLEX.Library.HTTP;
using CSIFLEX.PartAnalyzer.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CSIFLEX.PartAnalyzer.Extensions;

namespace CSIFLEX.PartAnalyzer.Service
{
    public class GeniusDataProvider
    {
        private const double TimeBeforeLoginAgain = 4;
        private SettingsProvider settingsProvider;
        private DateTime timeSinceLogin = DateTime.Now - TimeSpan.FromDays(1);
        private string token = string.Empty;
        public GeniusDataProvider(SettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        public async Task<IEnumerable<JobEntity>> GetJobEntities(CancellationToken ct, string id)
        {
            return await GetJobEntities(ct, new[] { id });
        }

        public async Task<IEnumerable<WorkOrderEntity>> GetWorkOrderEntities(CancellationToken ct, params string[] ids)
        {
            var retVal = await Fetch<WorkOrderEntity>(ct, ids: ids);
            return retVal;
        }

        public async Task<IEnumerable<ProductionTasksEntity>> GetProductionTaskEntities(CancellationToken ct, string workOrder)
        {
            return await FetchWithFilter<ProductionTasksEntity>(ct, $"WorkOrderCode{{{workOrder}}}");
        }

        public async Task<IEnumerable<T>> Fetch<T>(CancellationToken ct, string additionalFilters = "", params string[] ids)
        {
            var token = await LoginProcedure();
            var paramToSubmit = ids.ToParams();
            var idsToSubmit = paramToSubmit.ToString();
            var settings = settingsProvider.GetSettings;
            using (var client = HttpClientFactory.Create(token))
            {
                var idType = GetGeniusMainIdFromGeniusType(typeof(T));
                var decryptedUrl = SecureStoreApi.TryDecrypt(settings.ApiUrl);
                decryptedUrl = CleanedupUrl(decryptedUrl);
                var response = await client.GetAsync(decryptedUrl + $"api/data/fetch/{typeof(T).Name}?filter={idType}{new string(idsToSubmit)}{additionalFilters}");

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GeniusCollectionEntity<T>>(responseContent);

                if (result.Result == null)
                {
                    return new List<T>();
                }

                return result.Result;
            }
        }

        public async Task<IEnumerable<T>> FetchWithFilter<T>(CancellationToken ct, string filter)
        {
            var token = await LoginProcedure();
            var settings = settingsProvider.GetSettings;
            using (var client = HttpClientFactory.Create(token))
            {
                var decryptedUrl = SecureStoreApi.TryDecrypt(settings.ApiUrl);
                decryptedUrl = CleanedupUrl(decryptedUrl);
                var response = await client.GetAsync(decryptedUrl + $"api/data/fetch/{typeof(T).Name}?filter={filter}");

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GeniusCollectionEntity<T>>(responseContent);

                if (result.Result == null)
                {
                    return new List<T>();
                }

                return result.Result;
            }
        }



        private string GetGeniusMainIdFromGeniusType(Type t)
        {
            var geniusAttribute = (GeniusEntityMainIdAttribute)Attribute.GetCustomAttribute(t, typeof(GeniusEntityMainIdAttribute));
            if (geniusAttribute == null)
            {
                return string.Empty;
            }
            return geniusAttribute.IdName;
        }

        public async Task<IEnumerable<JobEntity>> GetJobEntities(CancellationToken ct, params string[] ids)
        {
            var result = await Fetch<JobEntity>(ct, ids: ids);
            return result;

        }

        private async Task<string> LoginProcedure()
        {
            var timeSinceLastLogin = DateTime.Now - timeSinceLogin;
            var timeSpent = timeSinceLastLogin.TotalMinutes;
            var settings = this.settingsProvider.GetSettings;
            if (timeSpent >= TimeBeforeLoginAgain)
            {
                var decryptedUrl = SecureStoreApi.TryDecrypt(settings.ApiUrl);
                decryptedUrl = CleanedupUrl(decryptedUrl);
                var username = SecureStoreApi.TryDecrypt(settings.ApiUserName);
                var password = SecureStoreApi.TryDecrypt(settings.ApiPassword);
                using (var client = HttpClientFactory.Create())
                {
                    var uri = $"{decryptedUrl}api/auth/";
                    var body = new JObject { ["Username"] = username, ["Password"] = password, ["CompanyCode"] = settings.ApiCompanyName };

                    var serializedContent = JsonConvert.SerializeObject(body);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, stringContent);
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    var authResult = JsonConvert.DeserializeObject<AuthenticationResultData>(responseContent);
                    token = authResult.Token;
                }
                timeSinceLogin = DateTime.Now;
            }
            return token;
        }

        private string CleanedupUrl(string inUrl)
        {
            if (inUrl[inUrl.Length - 1] == '/')
            {
                return inUrl;
            }
            return inUrl + '/';
        }
    }
}
