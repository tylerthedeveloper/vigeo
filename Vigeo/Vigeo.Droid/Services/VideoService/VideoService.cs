using System;
using System.Threading.Tasks;
using Vigeo.Services.VideoService;

namespace Vigeo.Droid.Services.VideoService
{
	public class VideoService : IVideoService
	{
		#region IVideoService implementation

		public Task<byte[]> GetVideoThumbnailAsync(string url)
		{
			return Task.FromResult<byte[]>(null);
		}

		#endregion
	}
}

