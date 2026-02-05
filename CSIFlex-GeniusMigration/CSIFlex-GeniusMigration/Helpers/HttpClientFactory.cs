using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration
{
	public static class HttpClientFactory
	{
		private static bool IsInitializedforAndroid;
		public static HttpClient Create(string token = null)
		{

			var handler = new HttpClientHandler()
			{
				UseProxy = false
			};
			var result = new HttpClient(handler);
			if(token!= null || !string.IsNullOrEmpty(token))
			{
				result.DefaultRequestHeaders
				.Accept
				.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				result
					.DefaultRequestHeaders
					.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			
			return result;
		}
	}
}
