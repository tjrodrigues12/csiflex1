using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.PartAnalyzer.Entities
{
	public class AuthenticationResultData
	{
		[JsonProperty("Result")]
		public string Token { get; set; }

		[JsonProperty("Messages")]
		public JArray Messages { get; set; }

		[JsonIgnore]
		public Dictionary<string, object> MessageDictionary =>  Messages
			.First
			.ToObject<Dictionary<string, object>>();
	}
}
