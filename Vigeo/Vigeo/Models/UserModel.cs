using Realms;
using Newtonsoft.Json;
using Xamarin.Forms;
using System;
using System.ComponentModel;
using Vigeo.Helpers;
using System.ServiceModel.Channels;
using Vigeo;
using Plugin.Geolocator.Abstractions;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Vigeo.Abstractions;

namespace Vigeo.Models
{
	public class UserModel : TableData
    {		
        [JsonProperty("v_id")]
		public int v_id { get; set; }

		[JsonProperty("fb_id")]
		public string fb_id { get; set; }

		[JsonProperty("access_token")]
		public string access_token { get; set; }

		[JsonProperty("token_type")]
		public string token_type { get; set; }

        [JsonProperty("email")]
		public string email { get; set; }

		[JsonProperty ("first_name")]
		public string first_name { get; set; }

		[JsonProperty ("last_name")]
		public string last_name { get; set; }

		[JsonProperty("gender")]
		public string gender { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }

		[JsonProperty("picture")]
		public string picture { get; set; }
        
        //[JsonProperty("attending")]
        //public string attending { get; set; }
        
		public void DoSave()
		{
			//App.appDBservice.SaveUser(email);
            var json = new JObject();
            json.Add("email", email);
            //App.appDBservice.SendSettings(json.ToString());

        }

        /*
        public UserModel()
        {   
        }
        */

        public void GetMyEvents()
        {

        }

        public void Init()
		{
            email = App.User.email;
        }

	}

	
}


