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

namespace Vigeo.ViewModels
{
    public static class EventsViewModel
    {
       public static ObservableCollection<AllEventsModel>GetEvents()
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var uri = "https://api.vigeo.io/v1/events";
                var response = client.GetAsync(uri).Result;
                var events = response.Content.ReadAsStringAsync().Result;
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                var rootobject = JsonConvert.DeserializeObject<AllEventsRoot>(events, settings);
                return rootobject.data;
            }
        }

		public static ObservableCollection<AllEventsModel2> GetEvents2()
		{
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var uri = "https://vigeo.azurewebsites.net/tables/alleventsmodel?ZUMO-API-VERSION=2.0.0";
                var response = client.GetAsync(uri).Result;
                var events = response.Content.ReadAsStringAsync().Result;
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                //Debug.WriteLine(events);
                var event_list = JsonConvert.DeserializeObject<ObservableCollection<AllEventsModel2>>(events);
               	
				foreach (var _event in event_list)
                {
					Debug.WriteLine("Event " + JsonConvert.SerializeObject(_event) + "\n");
                }

                return event_list;
            }
        }

        public static void GetUsers2()
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var uri = "https://vigeo-events.azurewebsites.net/tables/usermodel?ZUMO-API-VERSION=2.0.0";
                var response = client.GetAsync(uri).Result;
                var users = response.Content.ReadAsStringAsync().Result;
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                //Debug.WriteLine(events);
                var user_list = JsonConvert.DeserializeObject<List<UserModel>>(users);
                foreach (var _user in user_list)
                {
                    Debug.WriteLine("Event " + _user.Id + "\n");
                }
                return;
            }
        }

        public static void GetMyEvents(string user_id)
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var uri = "https://vigeo-events.azurewebsites.net/tables/alleventsmodel?ZUMO-API-VERSION=2.0.0";
                var response = client.GetAsync(uri).Result;
                var events = response.Content.ReadAsStringAsync().Result;
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                var event_list = JsonConvert.DeserializeObject<List<AllEventsModel>>(events);
                foreach (var _event in event_list)
                {
                    Debug.WriteLine("Event " + _event + "\n");
                }
                return;
            }
        }


        public static bool UpdateAttending(string event_id)
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.MaxResponseContentBufferSize = 256000;
                    var response = client.GetAsync($"https://api.vigeo.io/v1/event/{event_id}/attending?user_id={App.User.v_id}");
                };

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static List<MessageModel> getMessages(string event_id)
        {

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                client.MaxResponseContentBufferSize = 256000;
                var response = client.GetAsync($"https://api.vigeo.io/v1/event/{event_id}/chat/");
                var messages = response.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(messages);
                var message_list = JsonConvert.DeserializeObject<List<MessageModel>>(messages);
                return message_list;
            };
        }
        public static bool GetAttending(int user_id)
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.MaxResponseContentBufferSize = 256000;
                    var response = client.GetAsync($"https://api.vigeo.io/v1/user/{user_id}/events").Result;
                    var events = response.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    App.Attending = JsonConvert.DeserializeObject<List<string>>(events, settings);
                };

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SendMessage(string event_id, MessageModel message)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.MaxResponseContentBufferSize = 256000;
                    var response = client.PostAsync($"https://api.vigeo.io/v1/event/{event_id}/chat/", content);
                    Debug.WriteLine(response.Result);
                };

                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
