using CSIFlex_GeniusMigration.Entities;
using CSIFlex_GeniusMigration.Helpers;
using System.IO; 
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System;
using System.Reactive.Linq;
using System.Windows.Threading; 
using System.Collections.Generic;
using System.Windows.Input;
using System.Net.Http;

namespace CSIFlex_GeniusMigration.ViewModel
{
	public class SettingsViewModel : ViewModelBase
	{
		private readonly GeniusApi geniusApi;
		private readonly SettingsProvider settingsProvider;
		private string geniusUrl;
		private string geniusUserName;
		private string geniusPassword;
		private string csiFlexDbUserName;
		private string csiFlexPassword;
		private string databaseConnectionString;
		private string geniusSettingsError;
		private string csiFlexErrors;
		private bool isGeniusSettingsBusy;
		private bool isCSIFlexSettingsBusy;
		private bool geniusSettingsIsValid;
		private bool csiFlexSettingsIsValid;
		private bool canSave = true;
		private string csiFlexPort; 
		private AsyncCommand saveSettings;
		private AsyncCommand validateGeniusSettings;
		private AsyncCommand validateCSIFlexSettings;
 		private CSIFlexDbProvider csiflexProvider;
		private Window currentWindow;

		public SettingsViewModel(Window currWindow, SettingsProvider settingsProvider)
		{
			this.settingsProvider = settingsProvider;
			currentWindow = currWindow;
			geniusApi = new GeniusApi();
			csiflexProvider = new CSIFlexDbProvider();
			validateGeniusSettings = new AsyncCommand(ValidateGeniusSettingsTask);
			validateCSIFlexSettings = new AsyncCommand(ValidateCSIFlexSettingsTask);
			saveSettings = new AsyncCommand(SaveSettings);

			this.settingsProvider
				.Settings
				.ObserveOn(Dispatcher.CurrentDispatcher)
				.Subscribe(x => UpdateSettings(x));
		}

		public ICommand ValidateCSIFlexSettings => validateCSIFlexSettings;

		public ICommand ValidateGeniusSettings => validateGeniusSettings;

		public ICommand Save => saveSettings;

		public bool IsGeniusSettingsBusy
		{
			get { return isGeniusSettingsBusy; }
			set
			{
				CanSave = !value;
				isGeniusSettingsBusy = value;
				RaisePropertyChanged(nameof(IsGeniusSettingsBusy));
			}
		}

		public bool IsCSIFlexSettingsBusy
		{
			get { return isCSIFlexSettingsBusy; }
			set
			{
				CanSave = !value;
				isCSIFlexSettingsBusy = value;
				RaisePropertyChanged(nameof(IsCSIFlexSettingsBusy));
			}
		}

		public string GeniusBaseUrl
		{
			get { return geniusUrl; }
			set
			{
				geniusUrl = value;
				RaisePropertyChanged(GeniusBaseUrl);
				UpdateGeniusErrors(new List<string>() { "" });
			}
		}

		public string GeniusUserName
		{
			get { return geniusUserName; }
			set
			{
				geniusUserName = value;
				UpdateGeniusErrors(new List<string>() { "" });
				RaisePropertyChanged(GeniusUserName);
			}
		}
		public string GeniusPassword
		{
			get { return geniusPassword; }
			set
			{
				geniusPassword = value;
				UpdateGeniusErrors(new List<string>() { "" });
				RaisePropertyChanged(GeniusPassword);
			}
		}

		public string CSIFlexDbUrl
		{
			get { return databaseConnectionString; }
			set
			{
				databaseConnectionString = value;
				UpdateCSIFlexErrors(new List<string>() { "" });
				RaisePropertyChanged(nameof(CSIFlexDbUrl));
			}
		}

		public string CSIFlexPort
		{
			get { return csiFlexPort; }
			set
			{
				csiFlexPort = value;
				var port = -1;
				var isValid = int.TryParse(csiFlexPort, out port);
				if (!isValid)
				{
					UpdateCSIFlexErrors(new List<string>() { "Port must be a numeric value between 0 and 65535" });
				}
				else
				{
					UpdateCSIFlexErrors(new List<string>() { "" });
				}
				RaisePropertyChanged(nameof(CSIFlexPort));
			}
		}

		public string CSIFlexDbUsername
		{
			get { return csiFlexDbUserName; }
			set
			{
				csiFlexDbUserName = value;
				UpdateCSIFlexErrors(new List<string>() { "" });
				RaisePropertyChanged(nameof(CSIFlexDbUsername));
			}
		}

		public string CSIFlexPassword
		{
			get { return csiFlexPassword; }
			set
			{
				csiFlexPassword = value;
				UpdateCSIFlexErrors(new List<string>() { "" });
				RaisePropertyChanged(nameof(CSIFlexPassword));
			}
		}

		public string GeniusSettingsError
		{
			get { return geniusSettingsError; }
			set
			{
				if (!value.HasValue())
				{
					GeniusSettingsIsValid = false;
				}
				geniusSettingsError = value;
				RaisePropertyChanged(nameof(GeniusSettingsError));
			}
		}

		public bool GeniusSettingsIsValid
		{
			get { return geniusSettingsIsValid; }
			set
			{
				geniusSettingsIsValid = value;
				RaisePropertyChanged(nameof(GeniusSettingsIsValid));
			}
		}

		public bool CSIFlexSettingsIsValid
		{
			get { return csiFlexSettingsIsValid; }
			set
			{
				csiFlexSettingsIsValid = value;
				RaisePropertyChanged(nameof(CSIFlexSettingsIsValid));
			}
		}

		public bool CanSave
		{
			get { return canSave; }
			set
			{
				canSave = value;
				RaisePropertyChanged(nameof(CanSave));
			}
		}

		public string CSIFlexError
		{
			get { return csiFlexErrors; }
			set
			{
				csiFlexErrors = value;
				if (!value.HasValue())
				{
					CSIFlexSettingsIsValid = false;
				}
				RaisePropertyChanged(nameof(CSIFlexError));
			}
		} 

		private async Task SaveSettings()
		{
			IsCSIFlexSettingsBusy = true;
			isGeniusSettingsBusy = true;
			await settingsProvider.SaveSettings(Settings);
			await CloseWindow();
			IsCSIFlexSettingsBusy = false;
			isGeniusSettingsBusy = false;
		}
		 

		private void UpdateSettings(Settings settings)
		{
			this.CSIFlexDbUsername = SecureStoreApi.TryDecrypt(settings.CSIFlexUserName);
			this.CSIFlexPassword = SecureStoreApi.TryDecrypt(settings.CSIFlexPassword);
			this.GeniusUserName = SecureStoreApi.TryDecrypt(settings.GeniusUserName);
			this.GeniusPassword = SecureStoreApi.TryDecrypt(settings.GeniusPassword);
			this.CSIFlexDbUrl = settings.DatabaseServer;
			this.GeniusBaseUrl = settings.GeniusBaseUrl;
			this.CSIFlexPort = settings.CSFlexDbPort;
		}

		private async Task<(List<string> csiflexErrors, List<string> geniusErrors)> GetErrors()
		{
			var geniusError = GetGeniusError();
			var csiFlexError = GetCSIFlexError();
			return (csiFlexError, geniusError);
		}

		private List<string> GetGeniusError()
		{
			var geniusError = new List<string>();
			if (!GeniusUserName.HasValue())
			{
				geniusError.Add("Genius username cannot be empty");
			}
			if (!GeniusPassword.HasValue())
			{
				geniusError.Add("Genius password cannot be empty");
			}
			if (!GeniusBaseUrl.HasValue())
			{
				geniusError.Add("Genius base url cannot be empty");
			}
			else if (!GeniusBaseUrl.IsValidUrl())
			{
				geniusError.Add("Genius base url is invalid");
			}
			return geniusError;
		}


		private List<string> GetCSIFlexError()
		{
			var csiFlexError = new List<string>();
			if (!CSIFlexDbUsername.HasValue())
			{
				csiFlexError.Add("CSIFlex username cannot be empty");
			}
			if (!CSIFlexPassword.HasValue())
			{
				csiFlexError.Add("CSIFlex password cannot be empty");
			}
			if (!CSIFlexDbUrl.HasValue())
			{
				csiFlexError.Add("CSIFlex database base url cannot be empty");
			}
			if (CSIFlexPort.HasValue())
			{
				int port;
				var isValid = int.TryParse(csiFlexPort, out port);
				if (!isValid)
				{
					csiFlexError.Add("Port must be a numeric value between 0 and 65535");
				}
			}
			else if (!CSIFlexPort.HasValue())
			{
				csiFlexError.Add("Port must be a numeric value between 0 and 65535");
			}

			return csiFlexError;
		}

		private async Task ValidateGeniusSettingsTask()
		{
			IsGeniusSettingsBusy = true;
			try
			{
				var errors = GetGeniusError();
				if (errors.Count == 0)
				{
					var result = await geniusApi.Login(Settings);
					if (result != null && result.Messages != null && result.Messages.HasValues)
					{
						var messages = result.MessageDictionary;
						var isInvalidLogin = false;

						if (messages.ContainsKey("Source"))
						{
							isInvalidLogin = messages["Source"].ToString().Contains("password");
						}

						if (isInvalidLogin)
						{
							GeniusSettingsError = "Invalid username and/or password";
						}
						else
						{
							GeniusSettingsError = "There was an issue processing your request. Please contact CSIFlex for further investigation.";
						}
						GeniusSettingsIsValid = false;
					}
					else
					{
						await settingsProvider.SaveSettings(Settings);
						GeniusSettingsIsValid = true;
					}
				}
				else
				{
					UpdateGeniusErrors(errors);
				}
			}
			catch (Exception e)
			{
				if (e is HttpRequestException webExc)
				{
					GeniusSettingsError = "A connection could not be made to the target url";
				}
				else
				{
					GeniusSettingsError = e.ToString();
				}
			}
			IsGeniusSettingsBusy = false;
		}

		private async Task ValidateCSIFlexSettingsTask()
		{
			IsCSIFlexSettingsBusy = true;
			try
			{
				var errors = GetCSIFlexError();
				if (errors.Count == 0)
				{
					var successfulConnection = await csiflexProvider.TestConnection(Settings);
					if (successfulConnection)
					{
						CSIFlexSettingsIsValid = true;
						await settingsProvider.SaveSettings(Settings);
					}
					else
					{
						UpdateCSIFlexErrors(new List<string>() { "There was an issue establishing a database connection. Please contact technical support" });
						CSIFlexSettingsIsValid = false;
					}
				}
				else
				{
					UpdateCSIFlexErrors(errors);
					CSIFlexSettingsIsValid = false;
				}
			}
			catch (Exception e)
			{

			}
			IsCSIFlexSettingsBusy = false;
		}

		private void UpdateGeniusErrors(List<string> geniusErrors)
		{
			GeniusSettingsError = string.Join("\r\n", geniusErrors);
		}

		private void UpdateCSIFlexErrors(List<string> csiFlexErrors)
		{
			CSIFlexError = string.Join("\r\n", csiFlexErrors);
		}

		private async Task CloseWindow()
		{
			this.currentWindow.Close();
		}

		private Settings Settings => new Settings()
		{
			GeniusUserName = GeniusUserName.HasValue()
			? SecureStoreApi.TryEncrypt(this.GeniusUserName.Trim())
			: string.Empty,

			GeniusPassword = GeniusPassword.HasValue()
			? SecureStoreApi.TryEncrypt(this.GeniusPassword.Trim())
			: string.Empty,

			GeniusBaseUrl = this.GeniusBaseUrl.HasValue()
			? this.GeniusBaseUrl.Trim()
			: string.Empty,

			DatabaseServer = this.CSIFlexDbUrl.HasValue()
			? this.CSIFlexDbUrl.Trim()
			: string.Empty,

			CSIFlexUserName = this.CSIFlexDbUsername.HasValue()
			? SecureStoreApi.TryEncrypt(this.CSIFlexDbUsername.Trim())
			: string.Empty,

			CSIFlexPassword = this.CSIFlexPassword.HasValue()
			? SecureStoreApi.TryEncrypt(this.CSIFlexPassword.Trim())
			: string.Empty,
			CSFlexDbPort = this.CSIFlexPort.HasValue()
			? this.CSIFlexPort.Trim()
			: string.Empty
		};
	}
}
