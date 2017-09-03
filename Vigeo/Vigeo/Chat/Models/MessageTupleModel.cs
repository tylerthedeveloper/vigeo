using System;
using System.Collections.ObjectModel;
namespace Vigeo.Models
{
	public class MessageTupleModel
	{
		public ObservableCollection<MessageModel> Messages { get; private set; } = new ObservableCollection<MessageModel>();

		public UserModel sender { get; set; }
	}
}

