using System;
using System.Threading.Tasks;
using Vigeo.Models;
using System.Collections.ObjectModel;

namespace Vigeo.Services.Chat
{
	public interface IChatService
	{
		Task SendMessageAsync(MessageModel message);

		Task<ChatRoomModel> ConnectToRoomAsync(string roomId);

		Task LoadRoomsAsync();

		ObservableCollection<MessageTupleModel> MessageGroups { get; }

		ObservableCollection<ChatRoomModel> Rooms { get; }

		ChatRoomModel CurrentRoom { get; }
	}
}

