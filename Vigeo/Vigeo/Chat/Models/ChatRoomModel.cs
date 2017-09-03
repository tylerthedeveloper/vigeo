using System;
using System.Collections.Generic;
namespace Vigeo.Models
{
	public class ChatRoomModel
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public IEnumerable<UserModel> Users { get; set; }
	}
}

