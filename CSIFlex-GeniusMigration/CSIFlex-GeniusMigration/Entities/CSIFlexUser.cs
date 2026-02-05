using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.Entities
{
	public class CSIFlexUser
	{
		public string UserName { get; set; }

		public string Name { get; set; }

		public string FirstName { get; set; }

		public string Password { get; set; }

		public string Salt { get; set; }

		public string Email { get; set; }

		public string UserType { get; set; }

		public string Machines { get; set; }

		public string RefId { get; set; }

		public string Title { get; set; }

		public string Department { get; set; }
		 
		public string PhoneExtension { get; set; }
		 
	}
}
