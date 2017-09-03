
using System.Collections.Generic;

namespace Vigeo.Models
{
    public class EventChat 
    {
        public virtual AllEventsModel AllEventsModel { get; set; }
        public virtual MessageModel MessageModel { get; set; }

        public virtual UserModel UserModel { get; set; }

    }

    //public override string ToString() => JsonConvert.SerializeObject(this);

}