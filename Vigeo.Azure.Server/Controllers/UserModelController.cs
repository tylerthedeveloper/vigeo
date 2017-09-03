using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using VigBE.DataObjects;
using VigBE.Models;
using VigBE.Helpers;
using VigBE.DataObjects.DBO_Models;

namespace Backend.Controllers
{
    public class UserModelController : TableController<UserModel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<UserModel>(context, Request);
        }

        // GET tables/UserModel
        [ExpandProperty("AllEventsModels")]
        public IQueryable<UserModel> GetAllUserModels() => Query();

        // GET tables/UserModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandProperty("AllEventsModels")]
        public SingleResult<UserModel> GetUserModel(string id) => Lookup(id);

        // PATCH tables/UserModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserModel> PatchUserModel(string id, Delta<UserModel> patch) => UpdateAsync(id, patch);

        // POST tables/UserModel
        public async Task<IHttpActionResult> PostUserModel(UserModel item)
        {
            UserModel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/UserModel/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUserModel(string id) => DeleteAsync(id);
    }
}