using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.Entities
{
	public class Settings
	{
		public string GeniusBaseUrl { get; set; } 

		public string GeniusPassword{ get; set; }

		public string GeniusUserName { get; set; }

		public string DatabaseServer { get; set; }

		public string CSIFlexUserName { get; set; }

		public string CSFlexDbPort { get; set; }

		public string CSIFlexPassword { get; set; }

		[JsonIgnore]
		public bool ConfigurationComplete => GeniusBaseUrl.HasValue()
			&& GeniusPassword.HasValue()
			&& GeniusUserName.HasValue()
			&& DatabaseServer.HasValue()
			&& CSIFlexUserName.HasValue()
			&& CSFlexDbPort.HasValue()
			&& CSIFlexPassword.HasValue();
	}
}
