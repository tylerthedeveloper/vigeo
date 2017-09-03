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
	public static class Geo
	{

		public static ObservableCollection<AllEventsModel2> GetEvents()
		{
			var jj = JObject.Parse(@"{'latitude': 39.1653, 'longitude': -86.5264, 'radius': 1 }");

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "0c774ac5608d46a9a67949b7984e3be8");

				var content = new StringContent(JsonConvert.SerializeObject(jj), Encoding.UTF8, "application/json");
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				var uri = "https://api.allevents.in/events/geo/?latitude=" + jj["latitude"].ToString() + "&longitude=" + jj["longitude"].ToString() + "&radius=30&3";

				var response = client.PostAsync(uri, content).Result;

				var events = response.Content.ReadAsStringAsync().Result;

				var rootobject = JsonConvert.DeserializeObject<AllEventsRoot2>(events);

				foreach (var _event in rootobject.data)
				{
					Debug.WriteLine(JsonConvert.SerializeObject(_event));
				}

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

//Debug.WriteLine("array");
//Debug.WriteLine(list[0]);
*/















/*using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Newtonsoft.Json;

namespace UXDivers.Artina.Grial.AllEvents
{
	public static class Geo
	{

		public static void MakeRequest()
		{


			/* 
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "0c774ac5608d46a9a67949b7984e3be8");
				var postdataJson = JsonConvert.SerializeObject(new { latitude = 39.1653, longitude = -86.5264, radius = 1 });
				Debug.WriteLine(postdataJson);
				var postdataString = new StringContent(postdataJson, Encoding.UTF8, "application/json");
				var responseObj = client.PostAsync("https://api.allevents.in/events/geo", postdataString);
				var responseString = responseObj.Result.Content.ReadAsStringAsync().Result;
				Debug.WriteLine(responseString);
			}
			*/


/*
var jj = JObject.Parse(@"{'latitude': 39.1653, 'longitude': -86.5264, 'radius': 1 }");
var json = JsonConvert.SerializeObject(jj);

//Debug.WriteLine(jj);
//Debug.WriteLine(json);

var client = new HttpClient();
//client.BaseAddress = new Uri("https://api.allevents.in/events/geo/");
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//var cont = JsonCompress(json);

client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "0c774ac5608d46a9a67949b7984e3be8");

//var uri = "https://api.allevents.in/events/geo";
//var uri = "https://api.allevents.in/events/geo/?latitude={latitude}&longitude={longitude}&radius={radius}&3";
var uri = "https://api.allevents.in/events/geo/?latitude=" + jj["latitude"] + "&longitude=" + jj["longitude"] + "&radius=30&3";

byte[] byteData = Encoding.UTF8.GetBytes(json);

using (var content = new ByteArrayContent(byteData))
{
	Debug.WriteLine(content);
	content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
	var ckk = new StringContent(json, Encoding.UTF8, "application/json");
	var response = client.PostAsync(uri, ckk);
	//var events = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
	//var events = response.Content.ReadAsStringAsync().Result;
	var events = response.Result.Content.ReadAsStringAsync();
	Debug.WriteLine(events);
	Debug.WriteLine(response);
	Debug.WriteLine(response.Result);


}
*/





/*
var lat = json["latitude"];
var lon = json["longitude"];
var rad = json["radius"];

using (var client = new HttpClient())
{
	client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "0c774ac5608d46a9a67949b7984e3be8");
	//var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
	byte[] byteData = Encoding.UTF8.GetBytes("{'latitude': 39.1653, 'longitude': -86.5264, 'radius': 1 }");
	var content = new ByteArrayContent(byteData);
	content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
	Debug.WriteLine(json);
	Debug.WriteLine(content);
	var uri = "https://api.allevents.in/events/geo/?latitude={latitude}&longitude={longitude}&radius=1&3";
	var x = new Uri(string.Format("https://api.allevents.in/events/geo/?latitude={0}&longitude={1}&radius={2}&3", lat, lon, rad));

	//var uri = "https://api.allevents.in/events/geo/";
	var response = client.PostAsync(x, content);
	var events = response.Result.Content.ReadAsByteArrayAsync().Result;
	//var events = (JObject)JsonConvert.DeserializeObject(response.Result.Content.ReadAsStringAsync().Result);
	Debug.WriteLine(events);
	Debug.WriteLine(response);
	Debug.WriteLine(response.Result);

}


/*
* 		public bool SendUser(string json)
{
//http://tmenier.github.io/Flurl/

try
{
	var content = new StringContent(json, Encoding.UTF8, "application/json");

	//using (var client = new HttpClient(new NativeMessageHandler()))
	using (var client = new HttpClient())
	{
		Debug.WriteLine("Json = " + json);
		client.MaxResponseContentBufferSize = 256000;
		client.BaseAddress = new Uri("http://localhost:5000");
//client.BaseAddress = new Uri("http://149.160.155.208/test");
//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
//client.Timeout = 0x3e8;
//client.DefaultRequestHeaders.Accept.Clear();
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
var response = client.PostAsync("/test", content);
Debug.WriteLine(response.Result);
	};

	return true;
}
catch
{
	return false;
}

}
*/
