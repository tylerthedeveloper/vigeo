using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;
using VigBE.DataObjects.DTO_Mappers;

namespace VigBE.DataObjects.DBO_Models
{
	public class UserModel : EntityData
    {
        public UserModel()
        {
            AllEventsModels = new HashSet<AllEventsModel>();
            //Attending = new List<Attending>();
        }

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

        [JsonProperty("first_name")]
        public string first_name { get; set; }

        [JsonProperty("last_name")]
        public string last_name { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }

        [JsonProperty(PropertyName = "picture")]
		public string picture { get; set; }

        
        [JsonProperty(PropertyName = "AllEventsModels")]
        public virtual ICollection<AllEventsModel> AllEventsModels { get; set; }
        
        
        //[JsonProperty(PropertyName = "Attending")]
        //public virtual ICollection<Attending> Attending { get; set; }

        
        public void DoSave()
		{
            var json = new JObject();
            json.Add("email", email);
        }

		public void Init()
		{
           //mail = App.User.email;
        }

        //public override string ToString() => JsonConvert.SerializeObject(this);

    }
}


