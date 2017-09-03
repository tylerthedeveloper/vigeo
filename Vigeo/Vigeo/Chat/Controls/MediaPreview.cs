using System;
using Vigeo.Models;
using Xamarin.Forms;
using Vigeo.Extensions;

namespace Vigeo.Controls
{
	public class MediaPreview : Image
	{
		public MediaPreview()
		{
			IsVisible = false;
			this.IsOpaque = true;
			this.Aspect = Aspect.AspectFit;
		}

		#region public properties

		public static readonly BindableProperty MediaFileProperty =
					BindableProperty.Create(nameof(MediaFile), typeof(MediaFile), typeof(MediaPreview), default(MediaFile));

		public MediaFile MediaFile
		{
			get { return (MediaFile)GetValue(MediaFileProperty); }
			set { SetValue(MediaFileProperty, value); }
		}

		#endregion

		#region overrides

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);
			//HACK: fix bug with cell height on iOS
			if (Device.OS == TargetPlatform.iOS)
				HeightRequest = height;
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == MediaFileProperty.PropertyName) {
				Source = MediaFile.ToImageSource();
				IsVisible = Source != null;
			}
		}

		#endregion
	}
}

