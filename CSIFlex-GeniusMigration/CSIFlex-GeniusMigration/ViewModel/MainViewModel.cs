using CSIFlex_GeniusMigration.Entities;
using CSIFlex_GeniusMigration.Helpers;
using CSIFlex_GeniusMigration.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CSIFlex_GeniusMigration.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private readonly GeniusApi geniusApi = new GeniusApi();
		private IEnumerable<DataGridItem> masterDataGridItems = new List<DataGridItem>();
		private readonly CSIFlexDbProvider dbProvider = new CSIFlexDbProvider();
		private readonly Window parent;
		private DataGridItem selectedDataItem;
		private ISelectable selectedMachine;
		private ObservableCollection<DataGridItem> collection;
		private List<DataGridItem> selectedGridItems;
		private ObservableCollection<UserRole> userRoles;
		private ObservableCollection<CSIFlexUser> csiFlexUsers;
		private ObservableCollection<CheckableMachineParent> machines;
		private string masterPassword;
		private UserRole selectedMasterUserRole;
		private AsyncCommand navigateToSettings;
		private AsyncCommand exitCommand;
		private AsyncCommand startConnections;
		private RelayCommand parentItemCheckedCommand;
		private RelayCommand childItemCheckedCommand;
		private AsyncCommand syncCommand;
		private bool isBusy;
		private bool geniusIsBusy;
		private bool csiFlexIsBusy;
		private bool csiFlexUsersIsBusy;
		private bool ignoreTerminatedUsers = true; 
		private bool isInitialized;
		private SettingsProvider settingsProvider;
		private string searchText;

		public MainViewModel(Window parentWindow, SettingsProvider settingsProvider)
		{
			this.parent = parentWindow;
			userRoles = new ObservableCollection<UserRole>(new List<UserRole>()
			{
				UserRole.User,
				UserRole.Operator,
				UserRole.Supervisor,
				UserRole.Operator,
				UserRole.Admin
			});
			collection = new ObservableCollection<DataGridItem>();
			csiFlexUsers = new ObservableCollection<CSIFlexUser>();
			selectedGridItems = new List<DataGridItem>();
			syncCommand = new AsyncCommand(SyncTask); 
			parentItemCheckedCommand = new RelayCommand((x) => true, ParentCheckedAction);
			childItemCheckedCommand = new RelayCommand((x) => true, ChildCheckedAction);
			navigateToSettings = new AsyncCommand(NavigateToSettingsTask);
			exitCommand = new AsyncCommand(ExitTask);
			startConnections = new AsyncCommand(StartConnectionsTask);
			this.settingsProvider = settingsProvider;
		}

		public string MasterPassword
		{
			get => masterPassword;
			set
			{
				masterPassword = value;
				RaisePropertyChanged(nameof(MasterPassword));
			}
		}
		public UserRole SelectedMasterUserRole
		{
			get { return selectedMasterUserRole; }
			set
			{
				selectedMasterUserRole = value;
				foreach (var item in SelectedItemsList)
				{
					if (item is DataGridItem dataGridItem)
					{
						dataGridItem.UserRole = value;
					}
				}
				RaisePropertyChanged(nameof(SelectedMasterUserRole));
			}
		}

		public DataGridItem SelectedItem
		{
			get { return selectedDataItem; }
			set
			{
				selectedDataItem = value;
				RaisePropertyChanged(nameof(SelectedItem));
			}
		}

		public string SearchText
		{
			get { return searchText; }
			set
			{
				searchText = value;
				RaisePropertyChanged(searchText);
				UpdateList();
			}
		}

		private void UpdateList()
		{ 
			IEnumerable<DataGridItem> items= null;
			if (!ignoreTerminatedUsers)
			{
				items = masterDataGridItems
					.Where(x => x.GeniusUser.Active); 
			}
			else
			{
				items = masterDataGridItems;
			}
			
			if (searchText.HasValue() && searchText.Length >= 3)
			{
				items = items
					.Where(x => x.GeniusUser
						.FullName
						.ToLowerInvariant()
						.Contains(searchText));  
			}
			 
			Collection = new ObservableCollection<DataGridItem>(items);
		}
		 
		public IList SelectedItemsList
		{
			get => selectedGridItems;
			set
			{
				var newValue = value.Cast<DataGridItem>();
				foreach (var dataGridItem in newValue)
				{
					if (dataGridItem.Machines != null)
					{
						var allMachines = machines
							   .SelectMany(x => x.ChildMachines);
						var subset = allMachines
							   .Join(dataGridItem.Machines, m => m.MachineName, dg => dg, (m, dg) => m);
						var removed = allMachines
							.Except(subset);
						foreach (var machine in machines)
						{
							foreach (var item in machine.ChildMachines)
							{
								if (subset.Contains(item))
								{
									item.IsSelected = true;

								}
								else
								{
									item.IsSelected = false;
								}
								ChildCheckedActionInner(item);
							}
						}
					}
				}
				selectedGridItems = newValue
					.ToList();

				RaisePropertyChanged(nameof(SelectedItemsList));
			}
		}

		public bool IgnoreTerminatedUsers
		{
			get
			{
				return ignoreTerminatedUsers;
			}
			set
			{
				ignoreTerminatedUsers = value;
				RaisePropertyChanged(nameof(IgnoreTerminatedUsers));
				UpdateList();
			}
		}

		public ISelectable SelectedMachine
		{
			get => selectedMachine;
			set
			{
				selectedMachine = value;
				RaisePropertyChanged(nameof(SelectedMachine));
			}
		}


		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				isBusy = value;
				RaisePropertyChanged(nameof(isBusy));
			}
		}

		public bool GeniusIsBusy
		{
			get { return geniusIsBusy; }
			set
			{
				geniusIsBusy = value;
				RaisePropertyChanged(nameof(GeniusIsBusy));
			}
		}

		public bool CSIFlexIsBusy
		{
			get { return csiFlexIsBusy; }
			set
			{
				csiFlexIsBusy = value;
				RaisePropertyChanged(nameof(CSIFlexIsBusy));
			}
		}

		public bool CSIFlexUsersIsBusy
		{
			get { return csiFlexUsersIsBusy; }
			set
			{
				csiFlexUsersIsBusy = value;
				RaisePropertyChanged(nameof(CSIFlexUsersIsBusy));
			}
		}

		public bool IsInitialized
		{
			get { return isInitialized; }
			set
			{
				isInitialized = value;
				RaisePropertyChanged(nameof(isInitialized));
			}
		}

		public ICommand NavigateToSettings => navigateToSettings;

		public ICommand Exit => exitCommand;

		public ICommand StartConnections => startConnections;

		public ObservableCollection<UserRole> UserRoles
		{
			get
			{
				return userRoles;
			}
			set
			{
				userRoles = value;
				RaisePropertyChanged(nameof(UserRoles));
			}
		}

		public ObservableCollection<CheckableMachineParent> Machines
		{
			get { return machines; }
			set
			{
				machines = value;
				RaisePropertyChanged(nameof(Machines));
			}
		}

		public ObservableCollection<DataGridItem> Collection
		{
			get
			{
				return collection;
			}
			set
			{
				collection = value;
				RaisePropertyChanged(nameof(Collection));
			}
		}

		public ObservableCollection<CSIFlexUser> CSIFlexUsers
		{
			get { return csiFlexUsers; }
			set
			{
				csiFlexUsers = value;
				RaisePropertyChanged(nameof(CSIFlexUsers));
			}
		}

		private async Task NavigateToSettingsTask()
		{
			var window = new SettingsDialog();
			var context = new SettingsViewModel(window, settingsProvider);
			window.DataContext = context;
			window.Owner = parent;
			window.Activate();
			window.ShowDialog();
		}

		private async Task ExitTask()
		{
			Application.Current.Shutdown();
		}

		private async Task<DataFetchResult> UpdateMachines(Settings settings)
		{
			CSIFlexIsBusy = true;
			 
			if (settings == null)
			{
				CSIFlexIsBusy = false;
				return new DataFetchResult();
			}
			try
			{
				var groups = await dbProvider.GetAllGroups(settings);
				var machines = groups
					.Select(x =>
					{
						var parent = new CheckableMachineParent(parentItemCheckedCommand)
						{
							GroupName = x.Group
						};

						var children = new ObservableCollection<CheckableMachine>(x
							.Machines
							.Select(c => new CheckableMachine(childItemCheckedCommand)
							{
								MachineName = c,
								Parent = parent
							})
						.ToArray());
						parent.ChildMachines = children;
						return parent;
					});
				Machines = new ObservableCollection<CheckableMachineParent>(machines);
			}
			catch (Exception e)
			{
				CSIFlexIsBusy = false;
				return new DataFetchResult()
				{
					IsError = true,
					Exception = e,
					Error = $"There was an error fetching machine data, message returned was {e.Message}"
				};
			}
			CSIFlexIsBusy = false;
			return new DataFetchResult();
		}

		public ICommand SyncCommand => syncCommand;

		private async Task SyncTask()
		{
			IsBusy = true;
			if (IsInitialized)
			{
				try
				{
					var settings = await TryGetSettings();

					if (settings == null)
					{
						IsBusy = false;
						return;
					}

					var rnd = new Random();
					int length = 6;
					var str = "";
					for (var i = 0; i < length; i++)
					{
						str += ((char)(rnd.Next(1, 26) + 64)).ToString();
					}
					var password = MasterPassword.HasValue()
						? MasterPassword
						: str;
					var users = Collection
						.Where(x=> x.UserRole != UserRole.None)
						.Select(x => x.ToCSIFlexUser(password, !x.GeniusUser.Active && !IgnoreTerminatedUsers));
					var count = users.Count();
					var rowsAffected = await Task.Run(() => dbProvider.UpdateEmployees(settings, users));
					if (!MasterPassword.HasValue())
					{
						MessageBox.Show($"Sync  thiscompleted. The password for these users has been set to {password}", "Sync Success", MessageBoxButton.OK);
						await StartConnectionsTask();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show($"There was an issue syncing data:{e.Message} ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			IsBusy = false;

		}

		private async Task<Settings> TryGetSettings()
		{
			var settings = settingsProvider.GetSettings;
			if (!settings.ConfigurationComplete)
			{
				var result = MessageBox.Show("Connection haven't been configured, press ok to set them", "Warning",
					MessageBoxButton.OKCancel, MessageBoxImage.Question);
				if (result == MessageBoxResult.OK)
				{
					await NavigateToSettingsTask();
				}
				return null;
			}
			return settings;
		}

		private async Task<DataFetchResult> UpdateGeniusData(Settings settings)
		{
			IsBusy = true;
			GeniusIsBusy = true;
			try
			{ 
				if (settings == null)
				{
					GeniusIsBusy = false;
					IsBusy = false;
					return new DataFetchResult();
				}
				var geniusUsersApiResults = await Task.Run(() => geniusApi.GetGeniusUsers(settings));
				 
				masterDataGridItems = geniusUsersApiResults
					.Select(x => new DataGridItem()
					{
						GeniusUser = x,
						UserRole = SelectedMasterUserRole,
						Machines = new string[0]
					});
				if (ignoreTerminatedUsers)
				{
					var list = masterDataGridItems
						.Where(x => x.GeniusUser.Active);
					Collection = new ObservableCollection<DataGridItem>(list);
				}
				else
				{
					Collection = new ObservableCollection<DataGridItem>(masterDataGridItems);
				}

			}
			catch (Exception e)
			{
				return new DataFetchResult()
				{
					IsError = true,
					Exception = e,
					Error = $"There was an error fetching genius data:\r\n {e.Message}"
				};
			}
			GeniusIsBusy = false;
			IsBusy = false;
			return new DataFetchResult();
		}

		private async Task GetCSIFlexData()
		{
			CSIFlexIsBusy = true;
			IsBusy = true;
			var settings = await TryGetSettings();
			if (settings == null)
			{
				CSIFlexIsBusy = false;
				IsBusy = false;
				return;
			}

			var machines = await Task.Run(() => dbProvider.GetAllGroups(settings));
			CSIFlexIsBusy = false;
			IsBusy = false;
		}

		private async Task<DataFetchResult> UpdateCSIFlexUsers(Settings settings)
		{
			CSIFlexUsersIsBusy = true;
			IsBusy = true;
			 
			if (settings == null)
			{
				CSIFlexUsersIsBusy = false;
				IsBusy = false;
				return new DataFetchResult();
			}
			try
			{
				var usersResults = await Task.Run(()=> dbProvider.GetAllUsers(settings));
				CSIFlexUsers = new ObservableCollection<CSIFlexUser>(usersResults);
			}
			catch (Exception e)
			{
				CSIFlexUsersIsBusy = false;
				IsBusy = false;
				return new DataFetchResult()
				{
					Exception = e,
					IsError = true,
					Error = $"There was an issue fetching csiflex user data \r\n{e.Message}"
				};
			}
			CSIFlexUsersIsBusy = false;
			IsBusy = false;
			return new DataFetchResult();
		}

		private async Task StartConnectionsTask()
		{
			CSIFlexUsersIsBusy = true;
			GeniusIsBusy = true;
			CSIFlexIsBusy = true;
			var settings = await TryGetSettings();
			if (settings == null)
			{
				CSIFlexUsersIsBusy = false;
				GeniusIsBusy = false;
				CSIFlexIsBusy = false;
				return; 
			}
			var taskCollection = Task.WhenAll(UpdateGeniusData(settings), UpdateCSIFlexUsers(settings), UpdateMachines(settings));
			var errors = await Task.Run(() => taskCollection);
			var error = errors
				.Where(x => x.IsError)
				.FirstOrDefault();

			if (error != null && error.IsError)
			{
				MessageBox.Show(error.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				IsInitialized = false;
			}
			else
			{
				//reset roles
				foreach (var item in Collection)
				{
					item.UserRole = UserRole.None;
				}
				foreach (var item in CSIFlexUsers)
				{
					var foundItem = Collection
						.FirstOrDefault(x => x
						   .GeniusUser
						   .UserNameForCSIFlex == item.UserName);
					if (foundItem != null)
					{
						var userRole = UserRole.None;
						Enum.TryParse(item.UserType, out userRole);
						foundItem.UserRole = userRole; 
						foundItem.Machines = item.Machines.Split(',');
					}
				}

				Collection = new ObservableCollection<DataGridItem>(Collection);
			 
				IsInitialized = true;
			}
		}

		private async Task DeleteTerminatedUsersFromCSIFlex()
		{
			var terminatedUsers = masterDataGridItems
				.Where(x => !x.GeniusUser.Active);
			var terminatedCSIFlexUsers = CSIFlexUsers
				.Join(terminatedUsers, csiFlex => csiFlex.UserName, tUser => tUser.GeniusUser.UserNameForCSIFlex,
					(c, t) => c);
			var settings = await TryGetSettings();
			if (settings != null && terminatedCSIFlexUsers.Count() > 0)
			{
				var toUpdated = terminatedCSIFlexUsers
					.Select(x =>  new DeletableCSIFlexUser()
					{
						IsToBeDeleted = true,
						User = x
					});
				var resultRowCount = await Task.Run(() => dbProvider.UpdateEmployees(settings, toUpdated));
			}
		}

		private void UpdateSelectedItemMachines()
		{
			foreach (var item in selectedGridItems)
			{
				var s = machines
					.SelectMany(x => x.ChildMachines)
					.Where(c => c.IsSelected)
					.Select(c => c.MachineName);
				item.Machines = s ?? item.Machines;
			}
		}
		private void ParentCheckedAction(object obj)
		{
			if (obj is CheckableMachineParent parent)
			{
				foreach (var item in parent.ChildMachines)
				{
					item.IsSelected = parent.IsSelected;
				}
			}
			UpdateSelectedItemMachines();
		}

		private void ChildCheckedAction(object obj)
		{
			ChildCheckedActionInner(obj);
			UpdateSelectedItemMachines();
		}

		private void ChildCheckedActionInner(object obj)
		{
			if (obj is CheckableMachine machine)
			{
				//check if false first
				if (!machine.IsSelected)
				{
					machine.Parent.IsSelected = false;
				}
				else
				{
					var allSelected = true;
					//check if all other machines are checked
					foreach (var item in machine.Parent.ChildMachines)
					{
						if (!item.IsSelected)
						{
							allSelected = false;
							break;
						}
					}
					machine.Parent.IsSelected = allSelected;
				}

			}
		}
	}
}
