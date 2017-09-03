using System;
using Newtonsoft.Json;
using Vigeo.Abstractions;

namespace Vigeo.Models
{

    public class MediaFileModel : TableData
    {

            [JsonProperty(PropertyName = "user_id")]
            public int user_id { get; set; }

            [JsonProperty(PropertyName = "text")]
            public string text { get; set; }

            [JsonProperty(PropertyName = "title")]
            public string title { get; set; }

            [JsonProperty(PropertyName = "media_type")]
            public MediaType media_type { get; set; }

            [JsonProperty(PropertyName = "path")]
            public string path { get; set; }

            [JsonProperty(PropertyName = "timestamp")]
            public DateTime timestamp { get; set; }

            [JsonProperty(PropertyName = "UserModel")]
            public virtual UserModel UserModel { get; set; }


            //[Required]
            //[JsonProperty(PropertyName = "AllEventsModelId")]
            //public string AllEventsModelId { get; set; }

            /*[ForeignKey("AllEventsModelId")]
            [Required]
            [JsonProperty(PropertyName = "AllEventsModel")]
            public virtual AllEventsModel AllEventsModel { get; set; }
            */
            //[Required]
            [JsonProperty(PropertyName = "AllEventsModel")]
            public virtual AllEventsModel AllEventsModel { get; set; }

            //public override string ToString() => JsonConvert.SerializeObject(this);
        }

        /*public class MessageAttachmentModel
        {
            public MediaFile File { get; set; }
        }
        */
        public enum MediaType
        {
            Photo,
            Video
        }

}


