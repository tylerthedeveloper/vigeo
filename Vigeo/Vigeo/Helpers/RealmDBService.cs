using Realms;
using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Vigeo.Models;
using Xamarin.Forms;
using Vigeo.Pages;
using System.Threading.Tasks;

namespace Vigeo.Helpers
{

    public class RealmDBService
    {
        public Realm realmInstance = Realm.GetInstance();


		/*





        
        public UserModel GetUser()
		{
            if (realmInstance.All<UserModel>().Count() > 0)
            {
                return realmInstance.All<UserModel>().First();
            }
            //else return new UserModel();
            return null;
        }
        
		public bool CreateUser(JObject json)
		{
			try
			{
                realmInstance.Write(() =>
                {
                    var User = realmInstance.CreateObject<UserModel>();
                    User.fb_id = json["fb_id"].ToString();
                    User.access_token = json["access_token"].ToString();
                    User.token_type = json["token_type"].ToString();
                    User.email = json["email"].ToString();
                    User.firstName = json["first_name"].ToString();
                    User.lastName = json["last_name"].ToString();
                    User.gender = json["gender"].ToString();
                    User.picture = json["picture"].ToString();
                    User.age = int.Parse(json["age"].ToString());
                });
                return true;
                // return SendNewUser(JsonConvert.SerializeObject(App.User));
			}

			catch(Exception ex)
			{
                Debug.WriteLine("eror is " + ex.Message);
				return false;
			}
		}

        public bool SaveUser(string email)
		{
			try
			{
                realmInstance.Write(() =>
                {
                    var user = realmInstance.All<UserModel>().First();
                    user.email = email;
                    App.User.email = email;
                });
                return true;
			}
			catch
			{
				return false;
			}
		}

        public bool SendNewUser(string json)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.BaseAddress = new Uri("https://api.vigeo.io");
                    var response = client.PostAsync("/v1/login", content).Result;
                    var server_json = response.Content.ReadAsStringAsync().Result;
                    var v_id = int.Parse(JObject.Parse(server_json)["id"].ToString());
                    realmInstance.Write(() =>
                    {
                        var user = realmInstance.All<UserModel>().First();
                        user.v_id = v_id;
                        App.User.v_id = v_id;
                    });
                    
                };
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("error send: " + ex.Message);
                return false;
            }
        }

        public bool SendSettings(string json)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.MaxResponseContentBufferSize = 256000;
                    client.BaseAddress = new Uri("https://api.vigeo.io");
                    var response = client.PostAsync($"/v1/user/{App.User.v_id}/profile", content).Result;
                };
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void DeleteUser()
		{
            try
            {
                App.User = null;
                App.SuccessfulLogout();
                realmInstance.Write(() =>
                {
                    var user = realmInstance.All<UserModel>().First();
                    realmInstance.Remove(user);
                    App.User = null;
                });
                //return true;
            }

            catch
            {
            }
		}
        
		*/



    }
}
