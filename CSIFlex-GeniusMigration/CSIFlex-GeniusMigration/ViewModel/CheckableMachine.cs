using CSIFlex_GeniusMigration.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CSIFlex_GeniusMigration.ViewModel
{
	public class CheckableMachine:ViewModelBase, ISelectable
	{
		private bool isSelected;
		private string machineName;
		private RelayCommand checkedCommand; 

		public CheckableMachine(RelayCommand checkedCommand)
		{
			this.checkedCommand = checkedCommand;
 		}

		public bool IsSelected {
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

		public bool ParentIsCalling { get; set; }
		public string MachineName
		{
			get
			{
				return machineName;
			}
			set
			{
				machineName = value;
				RaisePropertyChanged(nameof(MachineName));
			}
		}

		public CheckableMachineParent Parent { get; set; }
	}
}
