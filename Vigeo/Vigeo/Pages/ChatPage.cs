using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vigeo.Controls;
using Vigeo.Models;
using Vigeo.Resources.Strings;
using Vigeo.ViewModels;
//using Vigeo.Views;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public class ChatPage : ContentPage//, IKeyboardOverlap
    {
        StackLayout layout = new StackLayout
        {
            Spacing = 0
        };
        StackLayout list = new StackLayout
        {
            Spacing = 3,
            Padding = 3,
            VerticalOptions = LayoutOptions.FillAndExpand
        };
        Entry entry;
        AllEventsModel _event;
        ScrollView scroll;
        public ChatPage(AllEventsModel e)
        {
            _event = e;
            scroll = new ScrollView();
            var messages = EventsViewModel.getMessages(e.event_id);
            var tile = new ContentView();
            foreach (var message in messages)
            {
				//fb_id
                if(message.user_id != App.User.fb_id)
                {
                    tile = new UXDivers.Artina.Grial.ChatLeftMessageItemTemplate
                    {
                        BindingContext = message
                    };
                }
                else
                {
                    tile = new UXDivers.Artina.Grial.ChatRightMessageItemTemplate
                    {
                        BindingContext = message
                    };
                }
                list.Children.Add(tile);

            }
            var chatInput = new ChatInput();
            entry = chatInput._entry;
            
            chatInput.sendBtn.Clicked += OnSendClicked;
            chatInput._entry.Completed += OnSendClicked;
            scroll.Content = list;
            
            layout.Children.Add(scroll);
            layout.Children.Add(chatInput);
            Content = layout;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (list.Children.Count > 0)
            {
                await scroll.ScrollToAsync(list.Children[list.Children.Count - 1], ScrollToPosition.End, true);
            }
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            var message = new MessageModel();
            //message.Attachment = new MessageAttachmentModel { File = _attachedIndicator.MediaFile };
            if (string.IsNullOrWhiteSpace(entry.Text)) return;
            message.text = entry.Text;
            message.fullName = $"{App.User.first_name} {App.User.last_name}";
            message.picture = App.User.picture;
            message.timestamp = DateTime.Now;
            message.user_id = App.User.fb_id;

            //_attachedIndicator.MediaFile = null;
            entry.Text = string.Empty;
            //SendCommand?.Execute(message);
            var tile = new UXDivers.Artina.Grial.ChatRightMessageItemTemplate
            {
                BindingContext = message
            };
            list.Children.Add(tile);
            scroll.ScrollToAsync(list.Children[list.Children.Count - 1], ScrollToPosition.End, true);
            this.ForceLayout();
            EventsViewModel.SendMessage(_event.event_id, message);

        }

        public async void OnKeyboardShow(double keyboardHeight)
        {
            if (list.Children.Count > 0)
            {
                await Task.Delay(300);
                await scroll.ScrollToAsync(list.Children[list.Children.Count - 1], ScrollToPosition.End, true);
            }
        }

        public async void OnKeyboardHide(double keyboardHeight)
        {
            if (list.Children.Count > 0)
            {
                await Task.Delay(300);
                await scroll.ScrollToAsync(list.Children[list.Children.Count - 1], ScrollToPosition.End, true);
            }
        }
    }
}
