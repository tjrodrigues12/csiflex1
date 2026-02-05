using CSIFlex_GeniusMigration.Helpers;
using CSIFlex_GeniusMigration.ViewModel;
using System.Windows;

namespace CSIFlex_GeniusMigration
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private SettingsProvider settingsProvider;
		public MainWindow()
		{
			InitializeComponent();
			settingsProvider = new SettingsProvider();
			var vm = new MainViewModel(this, settingsProvider);
			this.DataContext = vm;
			var conf = Configure;
			var con = Configure.DataContext;
		}
	}

}
