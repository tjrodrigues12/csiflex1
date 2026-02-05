using Newtonsoft.Json;
using CSIFLEX.PartAnalyzer.Entities;
using System; 
using System.IO; 
using System.Reactive.Linq;
using System.Reactive.Subjects; 
using System.Threading.Tasks;
using System.Windows;

namespace CSIFLEX.PartAnalyzer.Service
{
    public class SettingsProvider
    {
        private static readonly string settingsPath = System.Environment.CurrentDirectory + @"\settings.json";

        private readonly BehaviorSubject<Settings> settings = new BehaviorSubject<Settings>(new Entities.Settings());

        public SettingsProvider()
        {
            Observable.FromAsync(LoadSettings)
                .Subscribe(x => settings.OnNext(x));
        }

        private async Task<Settings> LoadSettings()
        {
            if (!File.Exists(settingsPath))
            {
                var settings = CreateBlankSettingsFile();
                return settings;
            }
            else
            {
                try
                {
                    using (var file = File.OpenText(settingsPath))
                    {
                        var content = await file.ReadToEndAsync();
                        var settings = JsonConvert.DeserializeObject<Settings>(content);
                        settings.CSIFlexPassword = "CSIF1337";
                        settings.CSFlexDbPort = settings.CSFlexDbPort.HasValue()
                            ? settings.CSFlexDbPort
                            : "3306";
                        settings.CSIFlexUserName = "root";
                        return settings;
                    }
                }
                catch (Exception e)
                {
                    return new Settings();
                }
            }
        }

        public IObservable<Settings> Settings => this.settings;

        public Settings GetSettings => this.settings.Value;

        public async Task SaveSettings(string ipAddress)
        {
            try
            {
                using (var file = File.CreateText(settingsPath))
                {
                    var settings = this.settings.Value;
                    var tempSettings = new Settings()
                    {
                        CSIFlexPassword = string.Empty,
                        CSFlexDbPort = settings.CSFlexDbPort,
                        CSIFlexUserName = string.Empty,
                        DatabaseServer = ipAddress
                    };
                    var serializer = new JsonSerializer();
                    var serialized = JsonConvert.SerializeObject(tempSettings);
                    await file.WriteAsync(serialized);
                    tempSettings.CSIFlexPassword = settings.CSIFlexPassword;
                    tempSettings.CSIFlexUserName = settings.CSIFlexUserName;
                    this.settings.OnNext(tempSettings);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Failed to apply settings, please try again", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Settings CreateBlankSettingsFile()
        {
            var settings = new Settings();
            using (var file = File.CreateText(settingsPath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, settings);
            }
            settings.CSIFlexPassword = "CSIF1337";
            settings.CSFlexDbPort = settings.CSFlexDbPort.HasValue()
                ? settings.CSFlexDbPort
                : "3306";
            settings.CSIFlexUserName = "root";
            return settings;
        }

        public async Task SavePassword(string password)
        {
            var currentSettings = settings.Value;
            currentSettings.ApiPassword = SecureStoreApi.TryEncrypt(password);
            await SaveSettings(currentSettings);
        }

        public async Task SaveUserName(string username)
        {
            var currentSettings = settings.Value;
            currentSettings.ApiUserName = SecureStoreApi.TryEncrypt(username);
            await SaveSettings(currentSettings);
        }

        public async Task SaveApiUrl(string url)
        {
            var currentSettings = settings.Value;
            currentSettings.ApiUrl = SecureStoreApi.TryEncrypt(url);
            await SaveSettings(currentSettings);
        }

        public async Task SaveCompanyName(string name)
        {
            var currentSettings = settings.Value;
            currentSettings.ApiCompanyName = name;
            await SaveSettings(currentSettings);
        }

        public async Task SaveSettings(Settings settings)
        {
            try
            {
                using (var file = File.CreateText(settingsPath))
                {                   
                    var serializer = new JsonSerializer();
                    var serialized = JsonConvert.SerializeObject(settings);
                    await file.WriteAsync(serialized);                   
                    this.settings.OnNext(settings);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Failed to apply settings, please try again", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
