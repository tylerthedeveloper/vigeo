using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Vigeo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Vigeo.Services.Chat
{
	public class ChatService : IChatService
	{
		private List<MessageModel> _messages;
		private Dictionary<string, Tuple<ChatRoomModel, List<MessageModel>>> _rooms = new Dictionary<string, Tuple<ChatRoomModel, List<MessageModel>>>();

		#region IChatService implementation

		public ObservableCollection<MessageTupleModel> MessageGroups { get; private set; } = new ObservableCollection<MessageTupleModel>();

		public ObservableCollection<ChatRoomModel> Rooms { get; private set; } = new ObservableCollection<ChatRoomModel>();

		public ChatRoomModel CurrentRoom { get; private set; }

		public async Task SendMessageAsync(MessageModel message)
		{
			//HACK: API call
			await Task.Delay(500);

			AddMessageToGroup(message);
			//HACK: simulate response
			//await GenerateRandomeResponseAsync(message);
		}

		public async Task<ChatRoomModel> ConnectToRoomAsync(string roomId)
		{
			//HACK: API call
			await Task.Delay(500);
			if (_rooms.ContainsKey(roomId)) {
				var data = _rooms[roomId];
				CurrentRoom = data.Item1;
				_messages = data.Item2;
				foreach (var msg in _messages)
				{
					AddMessageToGroup(msg);
				}
			}

			return null;
		}

		public async Task LoadRoomsAsync()
		{

            /*
            //HACK: API call
            await Task.Delay(500);
			if (_rooms.Count == 0) {
				for (int i = 0; i < 10; i++)
				{
					var user = new UserModel
					{
						v_id = Guid.NewGuid().ToString(),
						Name = $"Some User {i}"
					};
                    //var messages = new List<MessageModel>();
                    var msg = new MessageModel
					{
						Message = $"Test message {i}",
						Id = Guid.NewGuid().ToString(),
						sender = user
					};
					messages.Add(msg);
					var users = new List<UserModel>();
					users.Add(App.User);
					users.Add(user);
                    
            */
            var messages = getMessages();

            var room = new ChatRoomModel
			{
				Id = Guid.NewGuid().ToString(),
				Users = null
			};
			Rooms.Clear();
			Rooms.Add(room);
			_rooms.Add(room.Id, new Tuple<ChatRoomModel, List<MessageModel>>(room, messages));
		}
			
		

		#endregion

		#region private helpers

		private void AddMessageToGroup(MessageModel message)
		{
			var last = GetLastMessageTuple();
			if (last == null || last.sender.v_id != message.sender.v_id)
			{
				var targetGroup = new MessageTupleModel()
				{
					sender = message.sender
				};
				targetGroup.Messages.Add(message);
				MessageGroups.Add(targetGroup);
			}
			else
				last.Messages.Add(message);
		}

		private MessageTupleModel GetLastMessageTuple()
		{
			MessageTupleModel ret = null;
			if (MessageGroups.Count > 0)
				ret = MessageGroups[MessageGroups.Count - 1];
			return ret;
		}

        //roomId
        public static List<MessageModel> getMessages()
        {

            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
                //var response = client.PostAsync("https://api.vigeo.io/v1/event/" + id + "/chat", content);
                var response = client.GetAsync("http://localhost:5000/v1/event/1869/chat/");
                var messages = response.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(messages);
                var message_list = JsonConvert.DeserializeObject<List<MessageModel>>(messages);
                return message_list;
            };
        }


        /*
		private async Task GenerateRandomeResponseAsync(MessageModel message)
		{
			await Task.Delay(2000);
			var r = new Random().Next(100);
			var msgFrom = CurrentRoom.Users.FirstOrDefault(x => x.v_id != message.From.Id);
			if (r > 80 && msgFrom != null) {
				var response = new MessageModel
				{
					Message = $"Random message {r}",
					id = Guid.NewGuid().ToString(),
					Name= msgFrom
				};
				AddMessageToGroup(response);
			}
		}
        */
		#endregion
	}
}

