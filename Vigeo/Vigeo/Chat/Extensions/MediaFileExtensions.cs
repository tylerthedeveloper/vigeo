using System;
using Xamarin.Forms;
using Vigeo.Models;
using System.IO;

namespace Vigeo.Extensions
{
	public static class MediaFileExtensions
	{
		public static ImageSource ToImageSource(this MediaFile media)
		{
			ImageSource source = null;
			if (media == null)
				source = null;
			 else if (media.FileType == FileType.Photo)
				return media.Path;
			else if (media.FileType == FileType.Video) {
				//right now this service doesn't work
				//source = new StreamImageSource { Stream = async (token) => {
				//		var bytes = await App.VideoService.GetVideoThumbnailAsync(media.Path);
				//		var stream = new MemoryStream(bytes);
				//		return stream;
				//	} };
			}
			return source;
		}
	}
}

