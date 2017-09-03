using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Vigeo.Models;

namespace Vigeo.Abstractions
{
    public interface ILoginProvider
    {

		Task<UserModel> GetUserGraph();

        MobileServiceUser RetrieveTokenFromSecureStore();

        void StoreTokenInSecureStore(MobileServiceUser user);

        //void RemoveTokenFromSecureStore();

        //Task<MobileServiceUser> LoginAsync(MobileServiceClient client);
		      
		Task LoginAsync(MobileServiceClient client);


    }
}