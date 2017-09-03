using System;
using System.Collections.ObjectModel;

namespace Vigeo.Models
{
    
	public class MessageModel
	{
		public string user_id { get; set; }

		public string text { get; set; }
        

        public string fullName { get; set; }

		public DateTime timestamp { get; set; }

        public string picture { get; set; }


	}
}

