using System;
namespace Vigeo.Models
{
	public class MediaFile
	{
		public string Path { get; set; }
		public FileType FileType { get; set; }
	}

	public enum FileType
	{
		Photo,
		Video
	}
}

