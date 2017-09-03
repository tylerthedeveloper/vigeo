using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Vigeo.Abstractions;
using Vigeo.iOS.Services;
using UIKit;
using Newtonsoft.Json.Linq;
using Facebook.LoginKit;
using System;
using Foundation;
using System.Diagnostics;
using Facebook.CoreKit;
using Vigeo.Models;
using System.Collections.Generic;
using Vigeo.Pages;
using Newtonsoft.Json;
using Vigeo.ViewModels;
using Xamarin.Forms;
using Xamarin.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace Vigeo.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {
    
     	public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;

		public async System.Threading.Tasks.Task LoginAsync(MobileServiceClient client)
		{
			var accessToken = await LoginFacebookAsync();
			var zumoPayload = new JObject();
			//Debug.WriteLine("in LoginFacebookAsync: ");
			zumoPayload["access_token"] = accessToken;
			Debug.WriteLine("access toekn = " + accessToken);
			//App.user = 
			//var loginPageViewModelBinding = new LoginPageViewModel();
			await LoginPageViewModel.GetUser(accessToken);
#pragma warning disable CS1701 // Assuming assembly reference matches identity
			await client.LoginAsync("facebook", zumoPayload);
#pragma warning restore CS1701 // Assuming assembly reference matches identity
			Debug.WriteLine("after useing access toekn = ");
		}

		TaskCompletionSource<UserModel> _tcs;

		public async Task<UserModel> GetUserGraph()
		{

			_tcs = new TaskCompletionSource<UserModel>();

			try
			{
				var accessToken = await LoginFacebookAsync();

				var fields = "id, email, first_name, last_name, gender, age_range, friends, picture";
				var request = new GraphRequest("/me?", new NSDictionary("fields", fields), accessToken, null, "GET");
				var requestConnection = new GraphRequestConnection();
				requestConnection.AddRequest(request, ((connection, result, error) =>
				{
					LoginPage.Loading();

					// Handle if something went wrong with the request
					if (error != null)
					{
						_tcs.SetException(new Exception("Something went wrong"));
					}

					var userInfo = result as NSDictionary;
					var age_range = userInfo.ValueForKey((NSString)"age_range");
					var picData = userInfo.ValueForKey((NSString)"picture");
					var innerPicData = picData.ValueForKey((NSString)"data");
					var email = "";
					try
					{
						email = userInfo.ValueForKey((NSString)"email").ToString();
					}
					catch
					{
						email = "";
						
					}

					var friends = userInfo.ValueForKey((NSString)"friends");
					var friendData = friends.ValueForKey((NSString)"data");
					//Console.WriteLine(friends);
					//Console.WriteLine(friendData);
					var keys = new List<string> { "fb_id", "access_token", "token_type", "email", "name", "first_name", "last_name", "gender", "age", "picture", "friends", "interest_model" };
					/*
					var interests = new JObject
					{
						{ "sports", true}, { "music", false } //{ "", false },
					};
					*/

					var NSUser = new JObject()
					{
						{keys[0], userInfo.ValueForKey((NSString)"id").ToString()},
						{keys[1], accessToken},
						{keys[2], "facebook"},
						{keys[3], email},
						{keys[5], userInfo.ValueForKey((NSString)"first_name").ToString()},
						{keys[6], userInfo.ValueForKey((NSString)"last_name").ToString()},
						{keys[7], userInfo. ValueForKey((NSString)"gender").ToString()},
						{keys[8], (int.Parse(age_range.ValueForKey((NSString)"min").ToString()) + int.Parse(age_range.ValueForKey((NSString)"max").ToString())) / 2},
						{keys[9], innerPicData.ValueForKey((NSString)"url").ToString()},
						//interests
						{keys[10], friendData.ToString()}
					};

					//loading flow
					var user = JsonConvert.DeserializeObject<UserModel>(NSUser.ToString());

					_tcs.SetResult(user);

				}));

				requestConnection.Start();
			}

			catch
			{
				_tcs.SetResult(new UserModel());
			}

			return await _tcs.Task;
		}

        #region: Xamarin Auth
        public AccountStore AccountStore { get; private set; }

        public iOSLoginProvider()
        {
			//Debug.WriteLine("cons tying to reteive");
            AccountStore = AccountStore.Create();
			//Debug.WriteLine("2nd cons tying to reteive");
        }

        public MobileServiceUser RetrieveTokenFromSecureStore()
        {
			Debug.WriteLine("tying to reteive");
            var accounts = AccountStore.FindAccountsForService("Facebook");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    string token;

                    if (acct.Properties.TryGetValue("token", out token))
                    {
                        return new MobileServiceUser(acct.Username)
                        {
                            MobileServiceAuthenticationToken = token
                        };
                    }
                }
            }
            return null;
        }

        public void StoreTokenInSecureStore(MobileServiceUser user)
        {
			Debug.WriteLine("trying to store");
            var account = new Account(user.UserId);
            account.Properties.Add("token", user.MobileServiceAuthenticationToken);
            AccountStore.Save(account, "Facebook");
        }
        #endregion

		/*
       	public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
		{
			var accessToken = await LoginFacebookAsync();
			Debug.WriteLine("after LoginFacebookAsync: LoginAsync");
			var zumoPayload = new JObject();
			zumoPayload["access_token"] = accessToken;
			return await client.LoginAsync("facebook", zumoPayload);
		}
		*/

		#region Facebook Client Flow
		private TaskCompletionSource<string> fbtcs;

        public async Task<string> LoginFacebookAsync()
        {
			Debug.WriteLine("in facebook login async");
            fbtcs = new TaskCompletionSource<string>();
            var loginManager = new LoginManager();
			loginManager.LogInWithReadPermissions(new[] { "public_profile", "email", "user_friends" }, RootView, LoginTokenHandler);
            Debug.WriteLine("in loginfb try");
            return await fbtcs.Task;
        }

		void LoginTokenHandler(LoginManagerLoginResult loginResult, NSError error)
		{
			//Debug.WriteLine("in token handler");

			if (loginResult.Token != null)
			{
				Debug.WriteLine("in login token hanlder");
				fbtcs.TrySetResult(loginResult.Token.TokenString);
			}
			else
			{
				fbtcs.TrySetException(new Exception("Facebook Client Flow Login Failed"));
			}
		}

		TaskCompletionSource<UserModel> _grTCS = new TaskCompletionSource<UserModel>();

		public async Task<UserModel> GraphRequest(AccessToken token)
		{
			try
			{
				Debug.WriteLine("in graph request");
				var fields = "id, email, first_name, last_name, gender, age_range, friends, picture";
				var graphRequest = new GraphRequest("/" + token.UserID, new NSDictionary("fields", fields), token.TokenString, null, "GET");
				var requestConnection = new GraphRequestConnection();
				requestConnection.AddRequest(graphRequest, (connection, result, error) =>
				{
					LoginPage.Loading();

					Debug.WriteLine("in request");

					// Handle if something went wrong with the request
					if (error != null)
					{
						Console.WriteLine("error with graph request ");
						Console.WriteLine("Error..." + error.Description);
						_grTCS.SetException(new Exception("Something went wrong"));
					}

					/*
					if (error != null)
					{
						Console.WriteLine("error with graph request ");
						Console.WriteLine("Error..." + error.Description);
						return;
					}
					*/

					var userInfo = result as NSDictionary;
					var age_range = userInfo.ValueForKey((NSString)"age_range");
					var picData = userInfo.ValueForKey((NSString)"picture");
					var innerPicData = picData.ValueForKey((NSString)"data");
					var friends = userInfo.ValueForKey((NSString)"friends");
					var friendData = friends.ValueForKey((NSString)"data");
					//Console.WriteLine(friends);
					//Console.WriteLine(friendData);

					var keys = new List<string> { "fb_id", "access_token", "token_type", "email", "name", "first_name", "last_name", "gender", "age", "picture", "friends", "interest_model" };

					var interests = new JObject
					{
						{ "sports", true}, { "music", false }
					};

					var NSUser = new UserModel()
					{
						picture = innerPicData.ValueForKey((NSString)"url").ToString(),
						age = (int.Parse(age_range.ValueForKey((NSString)"min").ToString()) + int.Parse(age_range.ValueForKey((NSString)"max").ToString())) / 2,
						gender = userInfo.ValueForKey((NSString)"gender").ToString(),
						last_name = userInfo.ValueForKey((NSString)"last_name").ToString(),
						first_name = userInfo.ValueForKey((NSString)"first_name").ToString(),
						email = userInfo.ValueForKey((NSString)"email").ToString(),
						token_type = "facebook",
						access_token = token.TokenString,
						fb_id = token.UserID,
						Id = ""
						//v_id = token.TokenString,
						//allEventsModels = new hash
					};
					/*
						{keys[0], token.UserID},
						{keys[1], token.TokenString},
						{keys[2], "facebook"},
						{keys[3], userInfo.ValueForKey((NSString)"email").ToString()},
						{keys[5], userInfo.ValueForKey((NSString)"first_name").ToString()},
						{keys[6], userInfo.ValueForKey((NSString)"last_name").ToString()},
						{keys[7], userInfo. ValueForKey((NSString)"gender").ToString()},
						{keys[8], (int.Parse(age_range.ValueForKey((NSString)"min").ToString()) + int.Parse(age_range.ValueForKey((NSString)"max").ToString())) / 2},
						{keys[9], innerPicData.ValueForKey((NSString)"url").ToString()},
						{keys[10], friendData.ToString() }
					};
					*/

					//App login
					//App.SuccessfulLogin(userPreSerial);
					//var _user = JsonConvert.DeserializeObject<UserModel>(NSUser.ToString());
					//LoginPageViewModel.User = NSUser;
					App.User = NSUser;
					Debug.WriteLine("ios prov " + NSUser);
					_grTCS.SetResult(NSUser);

				});

				requestConnection.Start();

			}

			catch
			{
				_grTCS.SetResult(new UserModel());
			}

			return await _grTCS.Task;

		}
	}
	#endregion
}

//access token
//EAAXgn4e25egBAMUv0bfFpz587DZAnexvJILsX94ZAelIiNmNDZBTZBJwkl3jOe3SSYGKVvQXK43a4CqkgNzAVA3ZCbdthfZAHyZAuaIhAMcKO1nPfnV0NipTPhf21dhAkzRtJwqZByankgCHNoUXYhdBoIUQkJC1hByWxZCp0UnjV6jPd4yWuuYczRfLVaif2ddLEiGQSYJIqrZAucmB7ppl4JXC7jS7ZArvfEZD
//EAAXgn4e25egBAHkbMyBnO7xPfinzNVy2kg5LZCPUicDX28TMyndntTo8TODSRiapSOYOeB4MoxtZAed5AOF4YUSZBgzlF6Qyaf6CPb1nRuto9T29PfmEFPr2MvQUzzdJpKnOy3h7h3MEHVDH55sHJeQ4Yrvc94ZAOYzPNskIwKfO9VRxLcku7vH3bwMDjfRWErOf8bjybXS7YZBjk5PQOtOWsG8Mq3PgZD
//EAAXgn4e25egBAF2HM1zdURNKk9qzlNLLoXa353YtadQL21kDlkUFsteFKvmpOJkZChpZCr17Fi2rzZBZCZAH49D4i8DIYUIZCu4PdSbcSHmRAb8kqUZA8z4ISFRXUuUMZCcfagrafE2YMZAthQZBDauQZCVBL8lCZCzHHo0Hlt871yl0K9rSp33hZBPMF5nk3ZCD5POW8i8VloFkM9MZBOizIuYZAWmvgQb6sMU7hpwZD