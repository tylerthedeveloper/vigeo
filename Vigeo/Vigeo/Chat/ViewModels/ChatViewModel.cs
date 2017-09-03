using System;
using System.Collections.Generic;
using Vigeo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using System.Linq;

namespace Vigeo.ViewModels
{
	public class ChatViewModel : BaseViewModel
	{
		public ChatViewModel()
		{
			//TODO: inject dependency
			Init();
		}

		#region public properties
		private IEnumerable<MessageTupleModel> _MessageGroups;

		public IEnumerable<MessageTupleModel> MessageGroups
		{
			get { return _MessageGroups; }
			set { SetProperty(ref _MessageGroups, value); }
		}

		public ICommand SendMessageCommand { get; private set; }

		#endregion

		#region private helpers

		private async void Init()
		{
            //await _chatService.LoadRoomsAsync();
            //await _chatService.ConnectToRoomAsync(_chatService.Rooms.FirstOrDefault().Id);
            //MessageGroups = _chatService.MessageGroups;
            
        }


		#endregion
	}
}

