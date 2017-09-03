using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Vigeo.Models;
using System;
using Vigeo.Abstractions;

namespace VigBE.DataObjects.DBO_Models
{

    public class MessageModel : TableData
    {

        [JsonProperty(PropertyName = "user_id")]
        public string user_id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string text { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string fullName { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime timestamp { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string picture { get; set; }

        //[Required]
        //[JsonProperty(PropertyName = "AllEventsModelId")]
        //public string AllEventsModelId { get; set; }

        [JsonProperty(PropertyName = "UserModel")]
        public virtual UserModel UserModel { get; set; }

        /*[ForeignKey("AllEventsModelId")]
        [Required]
        [JsonProperty(PropertyName = "AllEventsModel")]
        public virtual AllEventsModel AllEventsModel { get; set; }
        */
        //[Required]
        [JsonProperty(PropertyName = "AllEventsModel")]
        //[NotMapped]
        public virtual AllEventsModel AllEventsModel { get; set; }


        /*
        public class MessageModelContext : DbContext
        {

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<MessageModel>()
                    // single below may be enough
                    .HasRequired(e => e.Event)
                    .WithMany(m => m.messages)
                    .HasForeignKey(f => f.Id);
                                       
                modelBuilder.Conventions.Add(
                    new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                        "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            }
        }
        */

        //public override string ToString() => JsonConvert.SerializeObject(this);

    }
}

