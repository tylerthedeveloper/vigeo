using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Xamarin.Auth;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Vigeo.Pages;
using Vigeo.iOS;
using Vigeo;
using System.Linq;
using System.Collections.Generic;
using Vigeo.Models;

[assembly: ExportRenderer(typeof(FacebookLoginPage), typeof(FacebookLoginPageRenderer))]

namespace Vigeo.iOS
{
	public class FacebookLoginPageRenderer : PageRenderer
	{
		bool isAuthed;

		public override void ViewDidAppear(bool animated)
		{
			//base.ViewDidAppear(animated);

			if (!isAuthed)
			{
				/*
				var accounts = AccountStore.Create().FindAccountsForService("Facebook");

				var account = accounts.FirstOrDefault();

				if (accounts.Count() != 0) 
					Console.WriteLine("accnt list " + accounts.First());
				
				if (account != null)
					Console.WriteLine("my accnt  " +  account.Username);
				*/

				var auth = new OAuth2Authenticator(
					clientId: "1654350664885736",
					scope: "",
					authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
					redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

				//auth.Error += AuthenticateError;

				auth.AllowCancel = true;

				auth.Completed += (s, e) =>
			   	{
					DismissViewController(true, 
                      	(() =>
				  	 	{
						   Console.WriteLine("succ2 log");
						   Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new EventPage2());
						}));


				   	if (!e.IsAuthenticated)
			   		{
					   //App.GetMainPage();
					   // need to clear token
					   //App.Token == "" or App.Current.Properties["access_token"] = "";
						Console.WriteLine("apparently not authed");
				   	}

				   	else
					{
					   	var accessToken = e.Account.Properties["access_token"];
					   	//App.SaveToken(accessToken);
						AccountStore.Create().Save(e.Account, "Facebook");

					   var facebookURI = new Uri("https://graph.facebook.com/me?fields=id,name, " +
						                   "first_name,last_name, age_range, gender, email, " +
										   "friends, picture");
						
						var request = new OAuth2Request("GET", facebookURI, null, e.Account);

						var userJObject = JObject.Parse(request.GetResponseAsync().Result.GetResponseText());
						   //var response = request.GetResponseAsync();
						   //var userJObject = request.GetResponseAsync().Result.GetResponseText())
						   //var userJObject = JObject.Parse(response.Result.GetResponseText());

						var age_range = userJObject["age_range"];
						var picData = userJObject["picture"]["data"];
						//var innerPicData = picData.ValueForKey((NSString)"data");
					   	var friends = userJObject["friends"]["data"];
						//var friendData = friends.ValueForKey((NSString)"data");						

						var keys = new List<string> { "fb_id", "access_token", "token_type", "email", "first_name", "last_name", "gender", "age", "picture", "friends", "interest_model" };

						var preUser = new JObject
						{
								{"v_id", 0},
								{keys[0], userJObject["id"]},
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

							
						//Console.WriteLine(preUser);
						//Console.WriteLine(JsonConvert.SerializeObject(preUser));
					   	//loading flow
					   	//LoginPage.Loading();
						App.User = JsonConvert.DeserializeObject<UserModel>(preUser.ToString());
						//App.User = JsonConvert.DeserializeObject<UserModel>(response.Result.GetResponseText());

					   }

					   isAuthed = true;
			   	};

				PresentViewController(auth.GetUI(), true, null);
			}
		}

	}
}