using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using VigBE.DataObjects;
using Microsoft.Data.OData;
using Microsoft.Azure.Mobile.Server;
using System.Web.Http.Controllers;
using VigBE.Models;
using VigBE.Helpers;
using VigBE.DataObjects.DBO_Models;

namespace VigBE.Controllers
{
    public class AllEventsModelController : TableController<AllEventsModel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<AllEventsModel>(context, Request);
        }

        // GET tables/AllEventsModel
        [ExpandProperty("UserModels")]
        [ExpandProperty("MessageModels")]
        public IQueryable<AllEventsModel> GetAllAllEventsModels() => Query();

        // GET tables/AllEventsModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandProperty("UserModels")]
        [ExpandProperty("MessageModels")]
        public SingleResult<AllEventsModel> GetAllEventsModel(string id) => Lookup(id);

        // PATCH tables/AllEventsModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AllEventsModel> PatchAllEventsModel(string id, Delta<AllEventsModel> patch) => UpdateAsync(id, patch);

        // POST tables/AllEventsModel
        public async Task<IHttpActionResult> PostAllEventsModel(AllEventsModel item)
        {
            AllEventsModel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AllEventsModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAllEventsModel(string id) => DeleteAsync(id);


        /*
        // POST tables/AllEventsModel
        public async Task<IHttpActionResult> PullAndPostAllEventsModel()
        {
            var eventList = get response..
            for(event in eventList)
            {
                await InsertAsync(event);   
            }

            AllEventsModel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
        */


    }

}
