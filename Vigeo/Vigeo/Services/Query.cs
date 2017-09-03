using System.Net.Http;
using Vigeo.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using ModernHttpClient;
using System.Text;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Vigeo.Services.AllEventsServices
{
	public static class Query
	{

		public static ObservableCollection<AllEventsModel2> GetEvents()
		{


			var jj = JObject.Parse(@"{'query': 'Sports' , 'latitude': 39.1653, 'longitude': -86.5264, 'radius': 1 }");

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "0c774ac5608d46a9a67949b7984e3be8");

				var content = new StringContent(JsonConvert.SerializeObject(jj), Encoding.UTF8, "application/json");
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				var uri = "https://api.allevents.in/events/search/?query=" + jj["query"].ToString() + "&latitude=" + jj["latitude"].ToString() + "&longitude=" + jj["longitude"].ToString() + "&radius=30&3";

				var response = client.PostAsync(uri, content).Result;

				var events = response.Content.ReadAsStringAsync().Result;

				//Debug.WriteLine(events);

				var rootobject = JsonConvert.DeserializeObject<AllEventsRoot2>(events);

				return rootobject.data;


			}
		}
	}

}






/*
var events = JObject.Parse(response.Content.ReadAsStringAsync().Result);
var list = new List<JObject>();
JToken event_data = events["data"];
foreach (JObject eve in event_data)
{
	list.Add(eve);
}
Debug.WriteLine("array");
Debug.WriteLine(list[0]);
Debug.WriteLine(events["data"]);
*/
