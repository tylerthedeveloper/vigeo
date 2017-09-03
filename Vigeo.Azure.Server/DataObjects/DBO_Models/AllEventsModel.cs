using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;
using VigBE.DataObjects.DTO_Mappers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VigBE.DataObjects.DBO_Models
{

    public class AllEventsModel : EntityData
    {
        public AllEventsModel()
        {
			//EventChats = new List<EventChat>();
			//Attending = new List<Attending>();

			Categories = new HashSet<Category>();

            MessageModels = new HashSet<MessageModel>();

            MediaFileModels = new HashSet<MediaFileModel>();

            UserModels = new HashSet<UserModel>();

        }

        //http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application

        #region Time + Date
        [JsonProperty(PropertyName = "end_time")]
        public string end_time { get; set; }

        [JsonProperty(PropertyName = "end_time_display")]
        public string end_time_display { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public string start_time { get; set; }

        [JsonProperty(PropertyName = "start_time_display")]
        public string start_time_display { get; set; }
        #endregion

        #region Location
        [JsonProperty(PropertyName = "location")]
        public string location { get; set; }

        //[JsonProperty(PropertyName = "VenueID")]
        //public string VenueID { get; set; }

        [JsonProperty(PropertyName = "Venue")]
        //[ForeignKey("VenueID")]
        public virtual Venue Venue { get; set; }

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
        [JsonProperty(PropertyName = "Categories")]
        public virtual ICollection<Category> Categories { get; set; }

        [JsonProperty(PropertyName = "MessageModels")]
        public virtual ICollection<MessageModel> MessageModels { get; set; }

        [JsonProperty(PropertyName = "MediaFileModels")]
        public virtual ICollection<MediaFileModel> MediaFileModels { get; set; }

        [JsonProperty(PropertyName = "UserModels")]
        public virtual ICollection<UserModel> UserModels { get; set; }
        #endregion

        public class AllEventsRoot
        {
            public ObservableCollection<AllEventsModel> data { get; set; }
        }

        /*
        public class RootObject
        {
            public ObservableCollection<AllEventsModel> data { get; set; }
        }
        */

        //public override string ToString() => JsonConvert.SerializeObject(this);

    }

}


