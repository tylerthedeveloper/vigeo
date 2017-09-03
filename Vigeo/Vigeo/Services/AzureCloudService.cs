using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Vigeo.Abstractions;
using Vigeo.Models;
using Xamarin.Forms;
using Vigeo.Helpers;
using System;
using System.Text;
using System.Diagnostics;

namespace Vigeo.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient client;

		public AzureCloudService()
        {
			client = new MobileServiceClient(Locations.AppServiceUrl);
			//client = new MobileServiceClient("https://vigeo-events.azurewebsites.net");

		}

		public ICloudTable<T> GetTable<T>() where T : TableData => new AzureCloudTable<T>(client);

		public Task<UserModel> GetUserGraph()
		{
			var loginProvider = DependencyService.Get<ILoginProvider>();
			return loginProvider.GetUserGraph();
		}


		public Task LoginAsync()
		{
			var loginProvider = DependencyService.Get<ILoginProvider>();
			return loginProvider.LoginAsync(client);
		}


		/*
        public async Task<MobileServiceUser> LoginAsync()
        {
			
			Debug.WriteLine("loging async azc ");
            
			var loginProvider = DependencyService.Get<ILoginProvider>();

			Debug.WriteLine("after proider creation loging async azc ");
            
			client.CurrentUser = loginProvider.RetrieveTokenFromSecureStore();

			Debug.WriteLine("after new clientloging async azc ");

            
            if (client.CurrentUser != null)
            {
                // User has previously been authenticated - try to Refresh the token
                try
                {
                    var refreshed = await client.RefreshUserAsync();
                    if (refreshed != null)
                    {
                        loginProvider.StoreTokenInSecureStore(refreshed);
                        return refreshed;
                    }
                }
                catch (Exception refreshException)
                {
                    Debug.WriteLine($"Could not refresh token: {refreshException.Message}");
                }
            }
			

            if (client.CurrentUser != null && !IsTokenExpired(client.CurrentUser.MobileServiceAuthenticationToken))
            {
                // User has previously been authenticated, no refresh is required
                return client.CurrentUser;
            }

			Debug.WriteLine("between token and new await async azc ");

            // We need to ask for credentials at this point
            await loginProvider.LoginAsync(client);
            if (client.CurrentUser != null)
            {
                // We were able to successfully log in
                loginProvider.StoreTokenInSecureStore(client.CurrentUser);
            }
            return client.CurrentUser;
        }
		*/

        bool IsTokenExpired(string token)
        {
            // Get just the JWT part of the token (without the signature).
            var jwt = token.Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+').Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new ArgumentException("The token is not a valid Base64 string.");
            }

            // Convert to a JSON String
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value,
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            return (expire < DateTime.UtcNow);
        }

    }
}
