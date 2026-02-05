using CSIFLEX.Library;
using CSIFLEX.Library.Commands;
using CSIFLEX.Library.Helpers.Commands;
using CSIFLEX.PartAnalyzer.Service;
using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ReactiveUI;
using CSIFLEX.PartAnalyzer.Entities;
using Newtonsoft.Json;

namespace CSIFLEX.PartAnalyzer.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {  
        private SettingsProvider settingsProvider;
        private bool initialized = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(SettingsProvider settingsProvider)
        {
             
            Save = new AsyncCommand(SaveTask);
            this.settingsProvider = settingsProvider;
            this.settingsProvider
                .Settings
                .ObserveOn(Dispatcher)
                .Subscribe(x =>
                {
                    if (x != null)
                    {
                       if(!initialized)
                        {
                            if (!DbAddress.HasValue())
                            {
                                DbAddress = x.DatabaseServer;
                            }
                            if (!ApiUrl.HasValue())
                            {
                                ApiUrl = SecureStoreApi.TryDecrypt(x.ApiUrl);
                            }
                            if (!ApiUserName.HasValue())
                            {
                                ApiUserName = SecureStoreApi.TryDecrypt(x.ApiUserName);
                            }
                            if (!ApiCompanyName.HasValue())
                            {
                                ApiCompanyName = x.ApiCompanyName;
                            }
                            if(!Password.HasValue())
                            {
                                Password = SecureStoreApi.TryDecrypt(x.ApiPassword);
                            }
                            HasChanges = false;
                        }
                     
                    }
                });
        }


        private Dispatcher Dispatcher => System.Windows.Application.Current.Dispatcher;

        public bool HasChanges { get; set; }

        public string DbAddress { get; set; }

        public string Password { get; set; } 

        public string ApiUrl { get; set; }

        public string ApiUserName { get; set; }

        public string ApiCompanyName { get; set; }
       

        public ICommand Save { get; }

       
        private async Task SaveApiUrlTask()
        {
            if (ApiUrl.HasValue())
            {
                await settingsProvider.SaveApiUrl(ApiUrl);
            }
        }

        private async Task SaveCompanyName()
        {
            if (ApiCompanyName.HasValue())
            {
                await settingsProvider.SaveCompanyName(ApiCompanyName);
            }
        }

        private async Task SaveDatabaseAddressTask()
        {
            if (DbAddress.HasValue())
            {
                await settingsProvider.SaveSettings(DbAddress);
            }
        }

        public async Task SaveApiUserNameTask()
        {
            if (ApiUserName.HasValue())
            {
                await settingsProvider.SaveUserName(ApiUserName);
            }
        } 

        private async Task SavePasswordTask()
        {
            if(Password.HasValue())
            {
                await settingsProvider.SavePassword(Password);
            }
        }

        private async Task SaveTask()
        { 
            var ser = JsonConvert.SerializeObject(settingsProvider.GetSettings);
            var cloned = JsonConvert.DeserializeObject<Settings>(ser);
            cloned.ApiUrl = SecureStoreApi.TryEncrypt(ApiUrl);
            cloned.ApiCompanyName = ApiCompanyName;
            cloned.DatabaseServer = DbAddress;
            cloned.ApiUserName = SecureStoreApi.TryEncrypt(ApiUserName);
            cloned.ApiPassword = SecureStoreApi.TryEncrypt(Password);

           await  settingsProvider.SaveSettings(cloned);

        }
    }
}
