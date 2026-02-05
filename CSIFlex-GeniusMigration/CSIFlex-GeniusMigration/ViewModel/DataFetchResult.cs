using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.ViewModel
{
	public class DataFetchResult
	{
		public Exception Exception { get; set; }

		public bool IsError { get; set; }

		public string Error { get; set; }
	}
}
