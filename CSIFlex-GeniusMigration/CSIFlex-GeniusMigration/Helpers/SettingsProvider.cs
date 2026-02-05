using CSIFlex_GeniusMigration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSIFlex_GeniusMigration.Helpers
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

		public  async Task SaveSettings(Settings settings)
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
				MessageBox.Show("Failed to apply settings, please try again", "Warning",
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
			return settings;
		}
		 
	}
}
