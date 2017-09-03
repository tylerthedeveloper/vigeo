using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Vigeo.Abstractions;

namespace Vigeo
{
	public class Venue : TableData
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


