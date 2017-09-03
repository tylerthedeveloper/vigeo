using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace VigBE.DataObjects.DBO_Models
{
    public class Tag : EntityData
    {
        public string TagName { get; set; }
    }

    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

    }
    //public override string ToString() => JsonConvert.SerializeObject(this);

}