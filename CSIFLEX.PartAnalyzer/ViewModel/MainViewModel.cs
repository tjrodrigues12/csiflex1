using LiveCharts;
using Newtonsoft.Json;
using CSIFLEX.PartAnalyzer.Entities;
using CSIFLEX.PartAnalyzer.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Windows.Threading;
using System.Reactive.Concurrency;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Diagnostics;
using CSIFLEX.PartAnalyzer.ViewModel;
using CSIFLEX.Library;
using CSIFLEX.PartAnalyzer.Views;
using CSIFLEX.GeniusConnector;
using CSIFLEX.Library.Commands;
using System.Threading;
using System.ComponentModel;
using System.Windows.Data;
using CSIFLEX.PartAnalyzer.Views.Converters;

namespace CSIFLEX.PartAnalyzer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private SearchOptions searchOptions = new SearchOptions();
        private static readonly string[] timeOptions = new[]
        {
            Constants.HoursMinSeconds,
            Constants.MinSeconds,
            Constants.Seconds
        };

        private readonly Window window;
        private readonly SettingsProvider settingsProvider;
        private readonly CSIFlexPartLocator partLocator;
        private readonly GeniusDataProvider geniusDataProvider;
        private readonly CSIFlexDbProvider dbProvider;
        private IScheduler backgroundScheduler = new EventLoopScheduler();
        public MainViewModel(SettingsProvider settingsProvider, CSIFlexDbProvider dbProvider, GeniusDataProvider geniusDataProvider, CSIFlexPartLocator partLocator, Window window)
        {
            this.dbProvider = dbProvider;
            this.partLocator = partLocator;
            this.window = window;
            this.geniusDataProvider = geniusDataProvider;
            ErpId = string.Empty;
            this.SearchErpId = new AsyncCommand(SearchProcedure, errorHandler: new ViewModelErrorHandler(HandleERPSearchFailure));
            DataGridSelectedItem = new RelayCommand(x => x != null, DataGridItemSelected);
            DoubleClickMachPerfCommand = new RelayCommand(x => true, DoubleClickMachPerfTask);
            DisplayTextSearchOptions = new AsyncCommand(DisplayTextSearchOptionsTask);
            ExportToCSV = new AsyncCommand(SaveDataToCSV);
            OpenConnectionSettingsWindow = new AsyncCommand(OpenConnectionSettingsWindowTask);
            this.settingsProvider = settingsProvider;
            PartListIsGreaterThanZero = false;
            IsBusy = false;
            FromDateTime = DateTime.Now;
            ToDateTime = DateTime.Now;
            var textEntered = this.WhenAnyValue(x => x.ErpId)
                .Throttle(TimeSpan.FromMilliseconds(250));
            textEntered
                .Select(x => x.HasValue())
                .Subscribe(x =>
                {
                    CanSearch = x;
                });

            var collections = this.WhenAnyValue(x => x.CSIFLEXProducedParts);
            collections
                .Where(collection => collection != null )
                .ObserveOn(Dispatcher)
                .Subscribe(x =>
                {
                    var count = x.Count;
                    PartListIsGreaterThanZero = count > 0;
                    MachineListTitle = count == 0
                      ? $"No parts found from work order id {ErpId} "
                      : $"Found {x.Count} part production runs for work order id {ErpId}";
                });

            this.settingsProvider
                .Settings
                .ObserveOn(Dispatcher)
                .Subscribe(x =>
                {
                    CanEnterPartNumber = x.ConfigurationComplete;
                });

            this.WhenAnyValue(x => x.MachinePartsListIsExportToCSV)
                .CombineLatest(this.WhenAnyValue(x => x.EntitiesIsExportToCSV), (m, e) => (m, e))
                .Subscribe(input =>
                {
                    CanExportToCSV = input.m || input.e;
                });
            this.WhenAnyValue(x => x.ProductionsTasks)
                .Where(x => x != null)
                .ObserveOn(Dispatcher)  
                .Subscribe(x =>
                {
                    var count = x.Count;
                    ERPListTitle = count== 0
                        ? $"No production entities found for work order id {ErpId}"
                        : $"Found {x.Count} production entities for work order id {ErpId}";
                    ErpListCountIsGreaterThanZero = count > 0;
                });
        }

        private async Task DisplayTextSearchOptionsTask()
        {
            var clonedres = JsonConvert.SerializeObject(searchOptions);
            var clonedVal = JsonConvert.DeserializeObject<SearchOptions>(clonedres);
            var context = new AdvancedSearchOptionsViewModel(
                clonedVal,
                 updateIterateOverSearchDateValue: v=> searchOptions.IsIterateSearchOverDate = v,
                 updateHourWindowValue: h => searchOptions.HourWindowValue = h,
                 updateMinuteWindowValue: m => searchOptions.MinuteWindowValue = m,
                 updateTimeWindowSearchIterations: t => searchOptions.TimeWindowSearchIterations = t,
                 updateIsIteratingOverPartName: p => searchOptions.IsIteratingOverPartName = p,
                 updateIterativeSearchPartNameValue: v => searchOptions.IterativeSearchPartNameValue = v,
                 updateIsSplittingHyphens: h => searchOptions.IsSplittingHyphens = h
                );
            var wind = new AdvancedSearchOptions();
            wind.DataContext = context; 
            wind.Owner = this.window;
            wind.Activate();
            wind.ShowDialog();
        }
         

        private void HandleERPSearchFailure(Exception obj)
        {
            IsBusy = false;
            System.Windows.MessageBox.Show($"There was an issue searching for the part number : {obj.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private Dispatcher Dispatcher => System.Windows.Application.Current.Dispatcher;

        public bool CanEnterPartNumber { get; set; }

        public bool CanEnterPartNumberOpenSettingsWindow { get; set; }

        public string ErpId { get; set; }
        public ICommand SearchErpId { get; }

        public ICommand DataGridSelectedItem { get; }

        public ICommand ExportToCSV { get; }

        public ICommand DisplayTextSearchOptions { get; }

        public bool CanExportToCSV { get; set; }

        public bool EntitiesIsExportToCSV { get; set; }

        public bool MachinePartsListIsExportToCSV { get; set; }

        public ICommand DoubleClickMachPerfCommand;

        public ICommand OpenConnectionSettingsWindow { get; }

        public bool CanSearch { get; set; }

        public bool CanSelectDateRangeGroup { get; set; }

        public bool PartsHaveValues { get; set; }

        public DateTime FromDateTime { get; set; }

        public bool HideZeroValues { get; set; }

        public DateTime ToDateTime { get; set; }

        public ObservableCollection<string> TimeOptions => new ObservableCollection<string>(timeOptions);

        public int SelectedTimeOptionsIndex { get; set; }

        public string MachineListTitle { get; set; }

        public bool MainIsBusy { get; set; }


        public ObservableCollection<MachinePartPerformance> CSIFLEXProducedParts { get; set; }

        public ObservableCollection<ProductionTasksEntity> ProductionsTasks { get; set; }

        public ObservableCollection<GeniusDataViewModel> EntityCollection { get; set; }
      //  public ICollectionView EntityCollection2 { get; set; }

        public bool PartListIsGreaterThanZero { get; set; }

        public bool IsBusy { get; set; }

        public Visibility IsGraphBusy { get; set; }

        public string TotalCycleOn { get; set; }

        public string TotalCycleOff { get; set; }

        public string TotalSetup { get; set; }

        public string TotalOther { get; set; }

        public string TotalTime { get; set; }

        public string ERPListTitle { get; set; }
        
        public string BusyText { get; set; }
        public bool ErpListCountIsGreaterThanZero { get; set; }
        private async Task SearchProcedure()
        {
            if (ErpId.HasValue())
            { 
                IsBusy =true;
                try
                {
                    BusyText = $"Fetching work order for id {ErpId}";
                    var workOrderEntities= await Task.Run(() => geniusDataProvider.GetWorkOrderEntities(CancellationToken.None, ErpId));
                    await Task.Run(() => Task.Delay(TimeSpan.FromMilliseconds(300)));
                    if (workOrderEntities.Count() > 0)
                    {

                        var firstWorkOrderEntity = workOrderEntities.FirstOrDefault();

                        BusyText = $"Fetching production tasks for work order id {ErpId}";
                        await Task.Run(() => Task.Delay(TimeSpan.FromMilliseconds(200)));
                        var productionTaskEntities = await Task.Run(() => geniusDataProvider.GetProductionTaskEntities(CancellationToken.None, ErpId));
                        ProductionsTasks = new ObservableCollection<ProductionTasksEntity>(productionTaskEntities);

                        BusyText = $"Fetching machine parts with id {firstWorkOrderEntity.ProductLink}";
                        await Task.Run(() => Task.Delay(TimeSpan.FromMilliseconds(200)));
                        var parts = await Task.Run(() => dbProvider.GetMachinePartPerformance(searchOptions, firstWorkOrderEntity.ProductionStartDate, firstWorkOrderEntity.ProductionEndDate, firstWorkOrderEntity.ProductLink));
                        CSIFLEXProducedParts = new ObservableCollection<MachinePartPerformance>(parts.Where(x=>x.TotalTimeInSeconds != 0));
                    }                    
                    
                    //var result = await dbProvider.GetMachinePartPerformance(searchOptions, y.ProductionStartDate, y.ProductionEndDate, getPartId(x.t));

                    //var result = await Task.Run(() => partLocator.GetWorkOrders(CancellationToken.None, searchOptions, ErpId));
                    //var collection = result
                    //    .Where(x => x.productionPart != null && x.productionPart.MachinePartPerformance != null && x.productionPart.ProductionTasksEntity != null)
                    //    .Select(x => new GeniusDataViewModel(x.workOrder, x.productionPart));
                    //EntityCollection = new ObservableCollection<GeniusDataViewModel>(collection);
                    //var cView = CollectionViewSource.GetDefaultView(EntityCollection);
                    //var groupdescription = new EntityDescription("Instance");
                    //cView.GroupDescriptions.Add(groupdescription);
                    //EntityCollection2 = cView;

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    IsBusy = false;
                    BusyText = "";
                } 
            }

        }

        private async Task OpenConnectionSettingsWindowTask()
        {
            var window = new SettingsWindow();
            var context = new SettingsViewModel(settingsProvider);
            window.DataContext = context;
            window.Owner = this.window;
            window.Activate();
            window.ShowDialog();
        }

        private async Task SaveDataToCSV()
        {
            var saveFileDialog = new SaveFileDialog();
            MainIsBusy = true;

            saveFileDialog.Filter = "csv files (*.csv)| *.csv";
            if (EntitiesIsExportToCSV)
            {
                await ExportTask("Export ERP Production Tasks", ProductionsTasks, saveFileDialog);
            }
            if(MachinePartsListIsExportToCSV)
            {
                await ExportTask("Export CSIFLEX data", CSIFLEXProducedParts, saveFileDialog);
            }


            MainIsBusy = false;

        }

        private async Task ExportTask<T>(string title, IEnumerable<T> collectionToExport, SaveFileDialog dialog)
        {
            dialog.Title = title;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fName = dialog.FileName;

                MainIsBusy = true;
                try
                {
                    await Task.Run(() =>
                    {
                        using (var sw = new StreamWriter(fName))
                        {
                            using (var csvWriter = new CsvWriter(sw))
                            {
                                csvWriter.WriteRecords(collectionToExport);
                            }
                        }
                        var fInfo = new FileInfo(fName);
                        Process.Start(fInfo.DirectoryName);
                    });
                }
                catch (Exception) { }
            }
        }

        private async void DataGridItemSelected(object obj)
        {
            IsGraphBusy = Visibility.Visible;
            if (obj is MachinePartPerformance machinePartPerf)
            {
            }
            IsGraphBusy = Visibility.Hidden;
        }

        private void DoubleClickMachPerfTask(object obj)
        {
            if (obj is MachinePartPerformance perf)
            {
                var vm = new PartPerformanceViewModel(perf);
                var window = new PartPerformanceGraph();
                window.Show();
                window.DataContext = vm;
            }
        }

    }
}
