using System;
using CSIFlex_GeniusMigration.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration
{
	public class GeniusApi
	{ 
		public async Task<IEnumerable<GeniusUser>> GetGeniusUsers(Settings settings)
		{
			var authResult = await TokenProvider(settings);
			
			using (var client = HttpClientFactory.Create(authResult.Token))
			{
				var response = await client.GetAsync(settings.GeniusBaseUrl + "/api/data/fetch/EmployeeEntity");

				var responseContent = await response.Content.ReadAsStringAsync();
				var geniusUsers = JsonConvert.DeserializeObject<GeniusUserCollection>(responseContent);
				return geniusUsers.Result;
			}
		}

		public async Task<AuthenticationResultData> Login(Settings settings)
		{
			return await TokenProvider(settings); 
		}

		private async Task<AuthenticationResultData> TokenProvider(Settings settings)
		{
			using (var client = HttpClientFactory.Create())
			{
				var un = SecureStoreApi.TryDecrypt(settings.GeniusUserName);
				var pw = SecureStoreApi.TryDecrypt(settings.GeniusPassword);
				var uri = $"{settings.GeniusBaseUrl}/api/auth/";
				var body = new JObject { ["Username"] = un, ["Password"] = pw, ["CompanyCode"]="CSIDB" };
				
				var serializedContent = JsonConvert.SerializeObject(body);
				var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
				var response = await client.PostAsync(uri, stringContent);
				var responseContent = response.Content.ReadAsStringAsync().Result;

				var authResult = JsonConvert.DeserializeObject<AuthenticationResultData>(responseContent);
				return authResult;
			}
		}
	}
}
