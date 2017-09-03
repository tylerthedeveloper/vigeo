using System;
using Vigeo.Services.VideoService;
using System.Threading.Tasks;
using MediaPlayer;
using Foundation;
using UIKit;
using System.IO;

namespace Vigeo.iOS.Services.VideoService
{
	public class VideoService : IVideoService
	{
		#region IVideoService implementation

		public Task<byte[]> GetVideoThumbnailAsync(string url)
		{
			//var movie = new MPMoviePlayerController(new NSUrl(url));
			//movie.ShouldAutoplay = false;
			//double videoPositionSec = (double)0;
			//UIImage videoThumbnail = movie.ThumbnailImageAt(videoPositionSec, MPMovieTimeOption.NearestKeyFrame);
			//MemoryStream ms = new MemoryStream();
			//videoThumbnail.AsPNG().AsStream().CopyTo(ms);
			//return Task.FromResult(ms.ToArray());
			using (var movie = new MPMoviePlayerController(new NSUrl(url)))
			{
				movie.ShouldAutoplay = false;
				//UIImage videoThumbnail = movie.ThumbnailImageAt(0.1, MPMovieTimeOption.NearestKeyFrame);
				var time = new NSNumber[1];

				time[0] = new NSNumber(1f);

				movie.RequestThumbnails(time, MPMovieTimeOption.Exact);
				NSObject notification = null;
				var tcs = new TaskCompletionSource<byte[]>();
				notification = MPMoviePlayerController.Notifications.ObserveThumbnailImageRequestDidFinish((sender, args) =>
				{
					MemoryStream ms = new MemoryStream();
					args.Image.AsPNG().AsStream().CopyTo(ms);
					tcs.SetResult(ms.ToArray());
					notification = null;
				});
				return tcs.Task;
			}
		}

		#endregion
	}
}

