using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using VigBE.DataObjects;
using VigBE.Models;
using Microsoft.Azure.Mobile.Server;
using VigBE.DataObjects.DBO_Models;
using VigBE.DataObjects.DTO_Mappers;
using VigBE.Helpers;

namespace Backend.Controllers
{
    public class EventChatController : TableController<EventChat>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<EventChat>(context, Request);
        }

        // GET tables/EventMessage
        [ExpandProperty("MessageModel")]
        [ExpandProperty("AllEventsModel")]
        public IQueryable<EventChat> GetAllEventMessage() => Query();

        // GET tables/EventMessage/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EventChat> GetEventMessage(string id) => Lookup(id);

        // PATCH tables/EventMessage/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<EventChat> PatchEventMessage(string id, Delta<EventChat> patch) => UpdateAsync(id, patch);

        // POST tables/EventMessage
        public async Task<IHttpActionResult> PostEventMessage(EventChat item)
        {
            EventChat current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/EventMessage/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEventMessage(string id) => DeleteAsync(id);
    }
}