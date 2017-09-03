using System;
using Xamarin.Forms;
using Vigeo.Models;
using Vigeo.Controls;

namespace Vigeo.TemplateSelectors
{
	public class ChatMessageTemplateSelector : DataTemplateSelector
	{
		private DataTemplate _fromTemplate;
		private DataTemplate _toTempalte;
		private int _currentUserId;


		public ChatMessageTemplateSelector(int currentUserId)
		{
			_currentUserId = currentUserId;
			//_fromTemplate = new DataTemplate(() =>
			//{
				//var cell = new ViewCell
				//{
				//	View = new ChatLeftMessageItemTemplate()
				//};
				//return cell;
			//});
			//_toTempalte = new DataTemplate(() =>
			//{
			//	var cell = new ViewCell
			//	{
			//		View = new ChatRightMessageItemTemplate()
			//	};
			//	return cell;
			//});
			_fromTemplate = new DataTemplate(() =>
			{
				var view = new ChatBubbleView();
				view.SetBinding(ChatBubbleView.MessagesProperty, "Messages");
				var cell = new ViewCell
				{
					View = view
				};
				return cell;
			});
			_toTempalte = new DataTemplate(() =>
			{
				var view = new ChatBubbleView();
				view.SetBinding(ChatBubbleView.MessagesProperty, "Messages");
				var cell = new ViewCell
				{
					View = view
				};
				return cell;
			});
		}

		#region overrides

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return _fromTemplate;
			//var message = item as MessageModel;
			//if (message.From?.Id == _currentUserId)
			//	return _toTempalte;
			//else
			//	return _fromTemplate;
		}

		#endregion
	}
}

