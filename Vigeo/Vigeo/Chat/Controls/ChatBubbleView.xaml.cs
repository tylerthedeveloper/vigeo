using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.Specialized;
using Vigeo.Models;
using Vigeo.Messages;

namespace Vigeo.Controls
{
	public partial class ChatBubbleView : ContentView
	{
		public ChatBubbleView()
		{
			InitializeComponent();
		}

		#region public properties

		public static readonly BindableProperty MessagesProperty =
					BindableProperty.Create(nameof(Messages), typeof(IEnumerable), typeof(ChatBubbleView), default(IEnumerable));

		public IEnumerable Messages
		{
			get { return (IEnumerable)GetValue(MessagesProperty); }
			set { SetValue(MessagesProperty, value); }
		}

		#endregion

		#region overrides

		protected override void OnPropertyChanging(string propertyName = null)
		{
			base.OnPropertyChanging(propertyName);
			if (propertyName == MessagesProperty.PropertyName) {
				RemoveMessages(Messages);
			}
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == MessagesProperty.PropertyName)
			{
				SetMessages(Messages);
			}
		}

		#endregion

		#region private helpers

		private void RemoveMessages(IEnumerable messages)
		{
			var changable = messages as INotifyCollectionChanged;
			if (changable != null) {
				changable.CollectionChanged -= Changable_CollectionChanged;
			}
		}

		private void SetMessages(IEnumerable messages)
		{
			var changable = messages as INotifyCollectionChanged;
			if (changable != null)
			{
				changable.CollectionChanged += Changable_CollectionChanged;
			}
			ReloadMessages();
		}

		void Changable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			//TODO: optimize
			ReloadMessages();
			var mes = new ScrollToBottomMessage();
			MessagingCenter.Send((object)this, nameof(ScrollToBottomMessage), mes);
		}

		private void ReloadMessages()
		{
			_messagesContainer.Children.Clear();
			if (Messages != null)
			{
				foreach (var msgObj in Messages)
				{
					var msg = msgObj as MessageModel;
					View view = null;

						view = new ChatBubbleItemView();
					
					if (view != null)
					{
						view.BindingContext = msg;
						_messagesContainer.Children.Add(view);
						this.ForceLayout();
					}
				}
			}
		}

		#endregion
	}
}

