using System;
//using Realms;
using Newtonsoft.Json;
//using UXDivers.Artina.Grial.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Vigeo.Abstractions;

namespace Vigeo.Models
{
    public class AllEventsRoot
    {
        public ObservableCollection<AllEventsModel> data;
    }

    public class AllEventsModel : TableData
    {
        public AllEventsModel()
        {
            //EventChats = new List<EventChat>();

			//Attending = new List<Attending>();

            MessageModels = new List<MessageModel>();

            MediaFileModels = new List<MediaFileModel>();

			UserModels = new List<UserModel>();
        }

        //http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application

        [JsonProperty(PropertyName = "end_time")]
        public string end_time { get; set; }

        [JsonProperty(PropertyName = "end_time_display")]
        public string end_time_display { get; set; }

        [JsonProperty(PropertyName = "event_id")]
        public string event_id { get; set; }

        [JsonProperty(PropertyName = "event_url")]
        public string event_url { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string location { get; set; }

        [JsonProperty(PropertyName = "owner_id")]
        public string owner_id { get; set; }

        [JsonProperty(PropertyName = "share_url")]
        public string share_url { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public string start_time { get; set; }

        [JsonProperty(PropertyName = "start_time_display")]
        public string start_time_display { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string image { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string city { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string country { get; set; }

        [JsonProperty(PropertyName = "full_address")]
        public string full_address { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string longitude { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string state { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string street { get; set; }
        
        
		//[JsonProperty(PropertyName = "Attending")]
        //public virtual ICollection<Attending> Attending { get; set; }

        // [JsonProperty(PropertyName = "EventChats")]
        //public virtual ICollection<EventChat> EventChats { get; set; }

		[JsonProperty(PropertyName = "attending")]
		public List<string> attending { get; set; }

        [JsonProperty(PropertyName = "MessageModels")]
        public List<MessageModel> MessageModels { get; set; }

        [JsonProperty(PropertyName = "MediaFileModels")]
        public List<MediaFileModel> MediaFileModels { get; set; }

		[JsonProperty(PropertyName = "UserModels")]
		public List<UserModel> UserModels { get; set; }

        public class AllEventsRoot
        {
            public ObservableCollection<AllEventsModel> data;
        }

        public class RootObject
        {
            public ObservableCollection<AllEventsModel> data { get; set; }
        }

        //public override string ToString() => JsonConvert.SerializeObject(this);

    }

    public class RootObject
    {
        public ObservableCollection<AllEventsModel> data { get; set; }
    }

    
    
}


