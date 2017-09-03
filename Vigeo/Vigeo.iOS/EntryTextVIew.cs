using System;
using Chat.iOS;
using UIKit;
using Vigeo;
using Vigeo.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryTextView), typeof(EntryTextViewRenderer))]
namespace Chat.iOS
{
	public class EntryTextViewRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			//FacebookLoginButtonRendererIos.OnElementChanged(base, e);

			if(Control != null) {
				// do whatever you want to the UITextField here!
				//Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
				//Control.BorderStyle = UITextBorderStyle.Line;
				Control.KeyboardType = UIKeyboardType.ASCIICapable;
			}

			//var view = NativeView;
		}
	}
}


