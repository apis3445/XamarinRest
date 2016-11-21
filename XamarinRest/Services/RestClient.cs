using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinRest
{
	public class RestClient
	{
		
		public string Serialize()
		{
			var countries = new[] {
				new Country { Name = "Mexico" },
				new Country { Name = "Costra Rica"}
			};

			string json = JsonConvert.SerializeObject(countries);
			Debug.WriteLine(json);
			return json;
		}

		public void Deserialize()
		{
			var json = Serialize();
			var parsedJson = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);
			foreach (Country item in parsedJson)
			{
				Debug.WriteLine(item.Name);
			}
		}

		public async Task<IEnumerable<Country>> GetCountries()
		{
			/*var countries = new[] {
				new Country { Name = "Mexico", Region = "MEX" },
				new Country { Name = "Costra Rica", Region = "CEN"}
			};
			return countries;*/

			return await GetCountriesRest();
		}

		public string BaseUrl { get; private set; } = "https://restcountries.eu/rest/v1/all";

		protected async Task<IEnumerable<Country>> GetCountriesRest()
		{
			var result = Enumerable.Empty<Country>();

			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(
					new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
				);
				var response = await httpClient.GetAsync(BaseUrl).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					if (!string.IsNullOrWhiteSpace(json))
					{
						
							result = await Task.Run(() =>
							{
								return JsonConvert.DeserializeObject<IEnumerable<Country>>(json);
							}).ConfigureAwait(false);

					}
				}
			}
			return result;
		}
		public RestClient()
		{
		}
	}
}
