using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using VigBE.DataObjects;
using VigBE.Models;
using Microsoft.Azure.Mobile.Server;
using VigBE.DataObjects.DBO_Models;

namespace Backend.Controllers
{
    public class VenueController : TableController<Venue>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Venue>(context, Request);
        }

        // GET tables/Venue
        public IQueryable<Venue> GetAllVenue() => Query();

        // GET tables/Venue/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Venue> GetVenue(string id) => Lookup(id);

        // PATCH tables/Venue/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Venue> PatchVenue(string id, Delta<Venue> patch) => UpdateAsync(id, patch);

        // POST tables/Venue
        public async Task<IHttpActionResult> PostVenue(Venue item)
        {
            Venue current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Venue/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTag(string id) => DeleteAsync(id);
    }
}