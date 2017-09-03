using System;
//using Realms;
using Newtonsoft.Json;
//using UXDivers.Artina.Grial.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Vigeo.Abstractions;

namespace Vigeo.Models
{ 
    public class Category : TableData
    {
        public string category { get; set; }
    }

    //public override string ToString() => JsonConvert.SerializeObject(this);

}