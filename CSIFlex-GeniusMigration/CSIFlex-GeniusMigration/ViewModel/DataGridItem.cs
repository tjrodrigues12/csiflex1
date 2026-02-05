using CSIFlex_GeniusMigration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.ViewModel
{
	public class DataGridItem : ViewModelBase
	{
		private GeniusUser user;
		private UserRole role = UserRole.None; 
		
		private IEnumerable<string> machines;

		public GeniusUser GeniusUser
		{
			get { return user; }
			set
			{
				user = value;
				RaisePropertyChanged(nameof(GeniusUser));
			}
		}

		public UserRole UserRole
		{
			get { return role; }
			set
			{
				role = value;
				RaisePropertyChanged(nameof(UserRole));
			}
		}

		public IEnumerable<string> Machines
		{
			get
			{
				return machines;
			}
			set
			{
				//creating a new copy of this enumerable.
				//while testing I found this this object loses its reference to its previous value
				//creating a copy resolves this. I spent 2 HOURS trying to trace this, but THIS shouldn't
				//even be a problem because the value isn't being reset by anything
				machines = Copy(value);
			}
		}

		private IEnumerable<string> Copy(IEnumerable<string> toCopy)
		{
			var list = new List<string>();
			foreach (var item in toCopy)
			{
				list.Add(item);
			}
			return list;
		}


	}
}
