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

namespace Backend.Controllers
{
    public class MessageModelController : TableController<MessageModel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<MessageModel>(context, Request);
        }

        // GET tables/ChatModel
        [ExpandProperty("AllEventsModel")]
        public IQueryable<MessageModel> GetAllChatModels() => Query();

        // GET tables/ChatModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandProperty("AllEventsModel")]
        public SingleResult<MessageModel> GetChatModel(string id) => Lookup(id);

        // PATCH tables/ChatModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MessageModel> PatchChatModel(string id, Delta<MessageModel> patch) => UpdateAsync(id, patch);

        // POST tables/ChatModel
        public async Task<IHttpActionResult> PostChatModel(MessageModel item)
        {
            MessageModel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ChatModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteChatModel(string id) => DeleteAsync(id);
    }

}
