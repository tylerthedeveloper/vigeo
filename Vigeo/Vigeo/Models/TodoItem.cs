using System.Collections.Generic;
using Vigeo.Abstractions;

namespace Vigeo.Models
{
    public class Tag : TableData
    {
        public string TagName { get; set; }
    }

    public class TodoItem : TableData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

    }
    //public override string ToString() => JsonConvert.SerializeObject(this);

}