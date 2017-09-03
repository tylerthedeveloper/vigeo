using System;
//using Realms;
using Newtonsoft.Json;
//using UXDivers.Artina.Grial.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Vigeo.Abstractions;

namespace Vigeo.Models
{

	public class AllEventsModel2 : TableData
	{
		public AllEventsModel2()
		{
            //EventChats = new List<EventChat>();

            //Attending = new List<Attending>();

            attending = new List<string>();

            Categories = new List<Category>();

            MessageModels = new List<MessageModel>();

			MediaFileModels = new List<MediaFileModel>();

			UserModels = new List<UserModel>();
        }

        //http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application

        #region Time + Date
        [JsonProperty(PropertyName = "end_time")]
		public string end_time { get; set; }

		[JsonProperty(PropertyName = "end_time_display")]
		public string end_time_display { get; set; }

		[JsonProperty(PropertyName = "start_time")]
		public int start_time { get; set; }
		//public DateTime start_time_converted () => new DateTime(1970, 1, 1).AddSeconds(start_time);
		//DateTime startDate = new DateTime(1970, 1, 1);
		//DateTime time = startDate.AddSeconds(1474394400);

		[JsonProperty(PropertyName = "start_time_display")]
		public string start_time_display { get; set; }
		#endregion

		#region Location
		[JsonProperty(PropertyName = "location")]
		public string location { get; set; }

		[JsonProperty(PropertyName = "venue")]
		public Venue Venue { get; set; }

		#endregion

		#region Generic Details
		[JsonProperty(PropertyName = "event_id")]
		public string event_id { get; set; }

		[JsonProperty(PropertyName = "eventname")]
		public string eventname { get; set; }

		[JsonProperty(PropertyName = "event_url")]
		public string event_url { get; set; }

		[JsonProperty(PropertyName = "owner_id")]
		public string owner_id { get; set; }

		#endregion

		#region Social + Media
		[JsonProperty(PropertyName = "share_url")]
		public string share_url { get; set; }

		[JsonProperty(PropertyName = "banner_url")]
		public string banner_url { get; set; }

		[JsonProperty(PropertyName = "categories")]
		public List<Category> Categories { get; set; }

        /*
		[JsonProperty(PropertyName = "tags")]
		public List<string> tags { get; set; }
        */

		/*
		public class Tag
		{
			public Tag()
			{
				string tag { get; set; }
			}
		}
		*/
		#endregion

		//[JsonProperty(PropertyName = "Attending")]
		//public virtual ICollection<Attending> Attending { get; set; }

		// [JsonProperty(PropertyName = "EventChats")]
		//public virtual ICollection<EventChat> EventChats { get; set; }

		//[JsonProperty(PropertyName = "attending")]
		//public List<string> attending { get; set; }

		#region Lists
		[JsonProperty(PropertyName = "MessageModels")]
		public List<MessageModel> MessageModels { get; set; }

		[JsonProperty(PropertyName = "MediaFileModels")]
		public List<MediaFileModel> MediaFileModels { get; set; }

		[JsonProperty(PropertyName = "UserModels")]
		public List<UserModel> UserModels { get; set; }
		#endregion


		[JsonProperty(PropertyName = "attending")]
		public List<string> attending { get; set; }
	}

	public class AllEventsRoot2
	{
		public ObservableCollection<AllEventsModel2> data;
	}

	public class RootObject2
	{
		public ObservableCollection<AllEventsModel2> data { get; set; }
	}

	//public override string ToString() => JsonConvert.SerializeObject(this);

}	

