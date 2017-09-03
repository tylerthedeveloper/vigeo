using System;
using System.Windows.Input;
using Xamarin.Forms;
using Vigeo.Models;
using Vigeo.Resources.Strings;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Vigeo.Controls;
using Vigeo.ViewModels;
//using Plugin.Media.Abstractions;

namespace Vigeo.Controls
{
	public class ChatInput : ContentView
	{
		public Entry _entry;
        public Button sendBtn;
		private MediaPreview _attachedIndicator;
		public ChatInput()
		{
			VerticalOptions = LayoutOptions.EndAndExpand;
			BackgroundColor = Color.FromHex("#d3d3d3");
            var entry = new EntryTextView
            {
                Placeholder = LocalizedStrings.MessagePlaceholder,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Keyboard = Keyboard.Create(0)
            };
			_entry = entry;
			sendBtn = new Button
			{
				BackgroundColor = Color.Transparent,
				Text = LocalizedStrings.Send,
			};

			_attachedIndicator = new MediaPreview
			{
				HeightRequest = 40,
				WidthRequest = 40,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};

			var layout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(15, 5),
				BackgroundColor = Color.Transparent,
				Spacing = 5,
				Children = {
					_attachedIndicator,
					entry,
					sendBtn
				}
			};
			var root = new StackLayout
			{
				Spacing = 0,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					new BoxView {
						HeightRequest = 1,
						MinimumHeightRequest = 1,
						BackgroundColor = Color.Black,
						HorizontalOptions = LayoutOptions.FillAndExpand
					},
					layout
				}
			};
			Content = root;
		}

		#region public properties

		public static readonly BindableProperty SendCommandProperty =
					BindableProperty.Create(nameof(SendCommand), typeof(ICommand), typeof(ChatInput), default(ICommand));

		public ICommand SendCommand
		{
			get { return (ICommand)GetValue(SendCommandProperty); }
			set { SetValue(SendCommandProperty, value); }
		}

		#endregion

		#region private helpers

		private async void OnAttachClicked(object sender, EventArgs e)
		{
            /*
			var cancel = LocalizedStrings.Cancel;
			var takePhoto = LocalizedStrings.TakePhoto;
			var pickPhoto = LocalizedStrings.PickPhoto;
			var takeVideo = LocalizedStrings.TakeVideo;
			var pickVideo = LocalizedStrings.PickVideo;
			var delete = _attachedIndicator.MediaFile != null ? LocalizedStrings.Delete : null;
			string[] args = null;
			if (App.Media.IsCameraAvailable)
				args = new [] { takePhoto, pickPhoto, takeVideo, pickVideo };
			else
				args = new[] { pickPhoto, pickVideo };
			var attachActionRes = await App.UserDialogs.ActionSheetAsync(LocalizedStrings.Attach, cancel, delete, null, args);
			if (attachActionRes == delete) {
				_attachedIndicator.MediaFile = null;
			} else if (attachActionRes != cancel) {
				var photoOptions = new StoreCameraMediaOptions();
				var videoOptions = new StoreVideoOptions();
				var mediaType = Models.FileType.Photo;
				Plugin.Media.Abstractions.MediaFile file = null;
				if (attachActionRes == takePhoto)
					file = await App.Media.TakePhotoAsync(photoOptions);
				else if (attachActionRes == pickPhoto)
					file = await App.Media.PickPhotoAsync();
				else if (attachActionRes == takeVideo)
				{
					mediaType = FileType.Video;
					file = await App.Media.TakeVideoAsync(videoOptions);
				}
				else if (attachActionRes == pickVideo)
				{
					mediaType = FileType.Video;
					file = await App.Media.PickVideoAsync();
				}
				if (file != null)
				{
					_attachedIndicator.MediaFile = new Models.MediaFile { Path = file.Path, FileType = mediaType };
				}
			}
            */
		}

		private void OnSendClicked(object sender, EventArgs e)
		{
			var message = new MessageModel();
			//message.Attachment = new MessageAttachmentModel { File = _attachedIndicator.MediaFile };
			message.text = _entry.Text;
            message.fullName = $"{App.User.first_name} {App.User.last_name}";
            message.picture = App.User.picture;
            message.timestamp = DateTime.Now;


			//message.user_id = App.User.v_id;
			message.user_id = App.User.fb_id;
			//_attachedIndicator.MediaFile = null;
			_entry.Text = string.Empty;
            //SendCommand?.Execute(message);
            EventsViewModel.SendMessage((BindingContext as AllEventsModel).event_id, message);
		}
        #endregion
    }
}

