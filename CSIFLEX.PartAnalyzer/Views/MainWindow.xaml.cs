using CSIFLEX.PartAnalyzer.Service; 
using System.Windows;
using System.Windows.Controls;

namespace CSIFLEX.PartAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var settingsProvider = new SettingsProvider();
            var geniusProvider = new GeniusDataProvider(settingsProvider);
            var dbProvider = new CSIFlexDbProvider(settingsProvider);
            var csiflexPartLocator = new CSIFlexPartLocator(dbProvider, geniusProvider);
            var vm = new MainViewModel(settingsProvider,dbProvider,geniusProvider,csiflexPartLocator,this);
            this.DataContext = vm;
        }

        private void LvScroll_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
