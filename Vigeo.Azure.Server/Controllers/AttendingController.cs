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
using VigBE.DataObjects.DTO_Mappers;

namespace VigBE.Controllers
{
    public class AttendingController : TableController<Attending>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Attending>(context, Request);
        }

        // GET tables/Attending
        //[ExpandProperty("AllEventsModel")]
        //[ExpandProperty("UserModel")]
        public IQueryable<Attending> GetAllAttendings() => Query();

        // GET tables/Attending/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Attending> GetAttending(string id) => Lookup(id);

        // PATCH tables/Attending/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Attending> PatchAttending(string id, Delta<Attending> patch) => UpdateAsync(id, patch);

        // POST tables/Attending
        public async Task<IHttpActionResult> PostAttending(Attending item)
        {
            Attending current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Attending/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAttending(string id) => DeleteAsync(id);    

    }

}
