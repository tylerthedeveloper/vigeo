using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VigBE.DataObjects.DBO_Models
{
   
    public class Category : EntityData
    {
        public string category { get; set; }
        public virtual ICollection<AllEventsModel> AllEventsModels { get; set; }
    }

    public class CategoryList
    {
        public ObservableCollection<Category> categories { get; set; }
    }
    //public override string ToString() => JsonConvert.SerializeObject(this);

}