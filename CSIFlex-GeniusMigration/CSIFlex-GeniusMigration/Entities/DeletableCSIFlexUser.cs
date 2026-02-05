using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.Entities
{
	public class DeletableCSIFlexUser
	{
		public CSIFlexUser User { get; set; }
		public bool IsToBeDeleted { get; set; }
	}
}
