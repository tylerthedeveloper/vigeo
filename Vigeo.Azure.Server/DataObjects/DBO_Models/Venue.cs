using Newtonsoft.Json;
using Microsoft.Azure.Mobile.Server;

namespace VigBE.DataObjects.DBO_Models
{
    public class Venue : EntityData
	{
		
		[JsonProperty(PropertyName = "street")]
		public string street { get; set; }

		[JsonProperty(PropertyName = "city")]
		public string city { get; set; }

		[JsonProperty(PropertyName = "state")]
		public string state { get; set; }

		[JsonProperty(PropertyName = "full_address")]
		public string full_address { get; set; }

		[JsonProperty(PropertyName = "country")]
		public string country { get; set; }

		[JsonProperty(PropertyName = "latitude")]
		public string latitude { get; set; }

		[JsonProperty(PropertyName = "longitude")]
		public string longitude { get; set; }

	}
}


