using CSIFlex_GeniusMigration.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CSIFlex_GeniusMigration.ViewModel
{
	public class CheckableMachineParent:ViewModelBase, ISelectable
	{
		private ObservableCollection<CheckableMachine> childMachines;
		private bool isSelected;

		private string groupName;
		private RelayCommand checkedCommand;

		public CheckableMachineParent(RelayCommand checkedCommand)
		{
			this.checkedCommand = checkedCommand;
		}

		public bool IsSelected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;				 
				RaisePropertyChanged(nameof(IsSelected));				 
			}
		}

		public ICommand CheckedCommand => checkedCommand;

		public ObservableCollection<CheckableMachine> ChildMachines
		{
			get
			{
				return childMachines;
			}
			set
			{
				childMachines = value; 
				RaisePropertyChanged(nameof(childMachines));
			}
		}

		public string GroupName
		{
			get
			{
				return groupName;
			}
			set
			{
				groupName = value;			 
				RaisePropertyChanged(nameof(GroupName));
			}
		}
	}
}
