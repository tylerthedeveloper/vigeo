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

namespace Backend.Controllers
{
    public class AllEventsModelsUserModelsController : TableController<AllEventsModelsUserModels>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<AllEventsModelsUserModels>(context, Request);
        }

        // GET tables/AllEventsModel
        public IQueryable<AllEventsModelsUserModels> GetAllAllEventsModels() => Query();

        // GET tables/AllEventsModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AllEventsModelsUserModels> GetAllEventsModel(string id) => Lookup(id);

        // PATCH tables/AllEventsModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AllEventsModelsUserModels> PatchAllEventsModel(string id, Delta<AllEventsModelsUserModels> patch) => UpdateAsync(id, patch);

        // POST tables/AllEventsModel
        public async Task<IHttpActionResult> PostAllEventsModel(AllEventsModelsUserModels item)
        {
            AllEventsModelsUserModels current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AllEventsModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAllEventsModel(string id) => DeleteAsync(id);
    }
    
}
