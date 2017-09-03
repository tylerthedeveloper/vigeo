using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Vigeo.Abstractions;
using Xamarin.Forms;
using Vigeo.Pages;
using Vigeo.Helpers;
using Vigeo.Models;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Vigeo.ViewModels
{
    public class LoginPageViewModel : Abstractions.BaseViewModel
    {

		public ICloudTable<UserModel> table = App.CloudService.GetTable<UserModel>();
		
		public LoginPageViewModel()
        {
			Title = "login page";
            AppService = Locations.AppServiceUrl;
			LoginCommand = new Command(async () => await ExecuteLoginCommand());
			//GraphCommand = new Command(async () => await ExecuteGraphCommand());
			//SaveCommand = new Command(async () => await ExecuteSaveCommand());
		}

		public string AppService { get; set; }
		//public static UserModel User { get; set; }

		public Command LoginCommand { get; }

		async Task ExecuteLoginCommand()
        {
            try
            {
                var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
				Debug.WriteLine("pre log async");
				await cloudService.LoginAsync();
				/*
				Debug.WriteLine(Application.Current.MainPage.Navigation.GetType().Name);
				Debug.WriteLine(Application.Current.MainPage.Title);
				Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
				*/
                //await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new EventPage2()));
				Application.Current.MainPage = new NavigationPage(new EventPage2());
				/*
				Debug.WriteLine(Application.Current.MainPage.Navigation.GetType().Name);
				Debug.WriteLine(Application.Current.MainPage.Title);
				Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
				*/
				Debug.WriteLine("post log async");
				//await ExecuteSaveCommand();
				//App.SuccessfulLogin();

			}

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Facebook Authentication Error", ex.Message, "OK");
            }
        }

        public Command SaveCommand;
        public async Task ExecuteSaveCommand()
		{
			Debug.WriteLine("in execute save");

			try
			{
				//var cloudService = ServiceLocator.Instance.Resolve<ICloudService>();
				Debug.WriteLine("pre grgpah async");
				//var _user = await cloudService.GetUserGraph();
				//Debug.WriteLine("post graph async");
				await table.CreateItemAsync(App.User);
				Debug.WriteLine("post graph async");
				
				/*
				Debug.WriteLine("in try ");
				if (User.Id == null)
				{
					Debug.WriteLine("save try if ");
					await table.CreateItemAsync(User);
				}
				else
				{
					App.User = User;
					Debug.WriteLine("save try else ");
					await table.UpdateItemAsync(User);
				}
				*/

				//Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
				//Application.Current.MainPage = new NavigationPage(new EventPage());
				//await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new Pages.EventPage()));
				Application.Current.MainPage = new NavigationPage(new TaskList());
				//App.SuccessfulLogin();
				//Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
				//await Application.Current.MainPage.Navigation.PushAsync(new Pages.EventList());
				
			}

			catch (Exception ex)
			{
				Debug.WriteLine($"[Facebook auth] Save error: {ex.Message}");
			}
		}

		public static void GetUsers2()
		{
			using (var client = new HttpClient(new NativeMessageHandler()))
			{
				var uri = "https://vigeo-events.azurewebsites.net/tables/usermodel?ZUMO-API-VERSION=2.0.0";
				var response = client.GetAsync(uri).Result;
				var users = response.Content.ReadAsStringAsync().Result;
				var settings = new JsonSerializerSettings();
				settings.NullValueHandling = NullValueHandling.Ignore;
				//var user_list = JsonConvert.DeserializeObject<List<UserModel>>(users);
				return;
			}
		}

		static TaskCompletionSource<UserModel> _guTCS; 

		public static async Task<string> GetUser(string accessToken)
		//public static async Task<UserModel> GetUser(string accessToken)
		{
			_guTCS = new TaskCompletionSource<UserModel>();

			// Use https to satisfy iOS ATS requirements.
			using (var client = new HttpClient(new NativeMessageHandler()))
			{
				//var fields = "id, email, first_name, last_name, gender, age_range, friends, picture";
				//https://graph.facebook.com/v2.7/me?fields=id%2Cname%2Cemail%2Cfriends%2C%20picture%2C%20gender%2C%20age_range%2C%20first_name%2C%20last_name&access_token
				var response = await client.GetAsync("https://graph.facebook.com/v2.7/me?fields=id%2Cname%2Cemail%2Cfriends%2C%20picture%2C%20gender%2C%20age_range%2C%20first_name%2C%20last_name&access_token=" + accessToken);
				var responseString = await response.Content.ReadAsStringAsync();
				var userJObject = JObject.Parse(responseString);
				Debug.WriteLine(userJObject);

				var age_range = userJObject["age_range"];
				var picData = userJObject["picture"]["data"]["url"];
				//var innerPicData = picData.ValueForKey((NSString)"data");
				var friends = userJObject["friends"]["data"];
				//var friendData = friends.ValueForKey((NSString)"data");						


				var keys = new List<string> { "fb_id", "access_token", "token_type", "email", "first_name", "last_name", "gender", "age", "picture", "friends", "interest_model" };
	
				/*
				var preUser = new JObject
				{
						{"v_id", 0},
						{keys[0],  userJObject["id"]},
						{keys[1], accessToken},
						{keys[2], "facebook"},
						{keys[3], userJObject["email"]},
						{keys[4], userJObject["first_name"]},
						{keys[5], userJObject["last_name"]},
						{keys[6], userJObject["gender"]},
						{keys[7], (int.Parse(age_range["min"].ToString()) + int.Parse(age_range["max"].ToString())) / 2 },
						{keys[8], picData["url"]},
						{keys[9], friends.ToString() }
				};
				*/

				 	var _user = new UserModel()
					{
						picture = picData.ToString(),
						age = (int.Parse(age_range["min"].ToString()) + int.Parse(age_range["max"].ToString())) / 2 ,
						gender = userJObject["gender"].ToString(),
						last_name = userJObject["last_name"].ToString(),
						first_name = userJObject["first_name"].ToString(),
						email = userJObject["email"].ToString(),
						token_type = "facebook",
						access_token = accessToken,
						fb_id = userJObject["id"].ToString(),
						Id = null,
						v_id = 0 //token.TokenString,
					};

				//var _user = JsonConvert.DeserializeObject<UserModel>(preUser.ToString());
				//Debug.WriteLine("jus use" + _user);
				Debug.WriteLine("use to string" + JsonConvert.SerializeObject(_user));
				App.User = _user;

				_guTCS.SetResult(_user);
				
				Debug.WriteLine("after app user");
				return responseString;
			}
		}
    }
}