using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;
using VigBE.DataObjects.DBO_Models;

namespace VigBE.DataObjects.DTO_Mappers
{
    public class Attending : EntityData
    {
        public virtual AllEventsModel AllEventsModel { get; set; }
        public virtual UserModel UserModel { get; set; }

    }

    //public override string ToString() => JsonConvert.SerializeObject(this);

}