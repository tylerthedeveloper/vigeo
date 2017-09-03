using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Collections.Generic;
using Facebook.LoginKit;
using CoreGraphics;
using System;
using Facebook.CoreKit;
using Newtonsoft.Json.Linq;
using Foundation;
//using Realms;
using Vigeo;
using Vigeo.iOS;
using Vigeo.Pages;
using Microsoft.WindowsAzure.MobileServices;
using Vigeo.iOS.Services;
using Vigeo.Services;
using Newtonsoft.Json;
using Vigeo.Models;
using Vigeo.ViewModels;

[assembly: ExportRenderer(typeof(FacebookButton), typeof(FacebookLoginButtonRendererIos))]

namespace Vigeo.iOS
{
    public class FacebookLoginButtonRendererIos : ButtonRenderer
    {

        #region: Facebook variables
        public List<string> readPermissions = new List<string> { "public_profile", "email", "user_friends" };

        protected LoginButton loginButton;
        
        public static LoginManager log_manager;
        #endregion

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var view = NativeView;

			//this.Window.Screen.Bounds.
			//loginButton = new LoginButton(new CGRect(0, 0, Window.Screen.Bounds.Width, Window.Screen.Bounds.Height / 10))
            loginButton = new LoginButton(new CGRect(0, 0, 330, 50))
            {
                LoginBehavior = LoginBehavior.SystemAccount,
                ReadPermissions = readPermissions.ToArray(),
            };
            
            //UIButton
            loginButton.Completed += (sender, eve) =>
            {
                if (!App.acceptedTerms) return;
                
                if (eve.Error != null)
                {
                    // Handle if there was an error
                    LoginPage.LoginAlert("Login Error", "There was an error logging in to your account", "Try Again", "cancel");
                    Console.WriteLine("error ");
                }

                if (eve.Result.IsCancelled)
                {
                    // Handle if the user cancelled the login request
                    LoginPage.LoginAlert("Incorrect Information", "Sorry, but you must be logged in to continue", "Try Again", "cancel");
                    Console.WriteLine("cancelled ");
                }


                if (eve.Result.Token != null)
                {
                    var fb_id = eve.Result.Token.UserID;
                    var access_token = eve.Result.Token.TokenString;
                    var token_expiration = eve.Result.Token.ExpirationDate.ToDateTime();
					//Tylers-MacBook-Air:App tylercitrin$ grep -R 'setUserDescription:' *
					//me?fields=id,first_name, last_name, email, picture, age_range, gender, friends

					//"https://graph.facebook.com/v2.7/me?fields=id%2Cage_range%2Cemail%2Cfriends%2Cpicture%2Cfirst_name%2Clast_name%2Cgender&access_token="
					var fields = "id, email, first_name, last_name, gender, age_range, friends, picture";
					var request = new GraphRequest("/" + eve.Result.Token.UserID, new NSDictionary("fields", fields), AccessToken.CurrentAccessToken.TokenString, null, "GET");
                    var requestConnection = new GraphRequestConnection();

					try
					{
						requestConnection.AddRequest(request, ((connection, result, error) =>
						{
							// Handle if something went wrong with the request
							if (error != null)
							{
								Console.WriteLine("error with graph request ");
								Console.WriteLine("Error..." + error.Description);
								return;
							}

							var userInfo = result as NSDictionary;
							var age_range = userInfo.ValueForKey((NSString)"age_range");
							var picData = userInfo.ValueForKey((NSString)"picture");
							var innerPicData = picData.ValueForKey((NSString)"data");
							var friends = userInfo.ValueForKey((NSString)"friends");
							var friendData = friends.ValueForKey((NSString)"data");
							//Console.WriteLine(friends);
							//Console.WriteLine(friendData);

							var email = "no email :(";
							try
							{
								email = userInfo.ValueForKey((NSString)"email").ToString();
							}
							catch
							{
								email = "";

							}

							var keys = new List<string> { "fb_id", "access_token", "token_type", "email", "name", "first_name", "last_name", "gender", "age", "picture", "friends", "interest_model" };

							var interests = new JObject
							{
								{ "sports", true}, { "music", false }

							};

							var NSUser = new JObject
							{
								{keys[0], fb_id},
								{keys[1],access_token},
								{keys[2], "facebook"},
								{keys[3], email},
								{keys[5], userInfo.ValueForKey((NSString)"first_name").ToString()},
								{keys[6], userInfo.ValueForKey((NSString)"last_name").ToString()},
								{keys[7], userInfo. ValueForKey((NSString)"gender").ToString()},
								{keys[8], (int.Parse(age_range.ValueForKey((NSString)"min").ToString()) + int.Parse(age_range.ValueForKey((NSString)"max").ToString())) / 2},
								{keys[9], innerPicData.ValueForKey((NSString)"url").ToString()},
								{keys[10], friendData.ToString() }
							};


							//loading flow
							LoginPage.Loading();
							//remove button
							loginButton.RemoveFromSuperview();

							//var user = JsonConvert.DeserializeObject<UserModel>(NSUser.ToString());
							App.User = JsonConvert.DeserializeObject<UserModel>(NSUser.ToString());

							//var logPageVMHolder = new LoginPageViewModel();
							//logPageVMHolder.ExecuteSaveCommand();


							//App login
							App.SuccessfulLogin();
							//log_manager = new LoginManager();
							//log_manager.LogOut();

						}));

						requestConnection.Start();

					}

					catch (Exception ex)
					{ 
						Console.WriteLine($"[Facebook Authentication] Save error: {ex.Message}");
					}

                }


            };

            // Handle actions once the user is logged out
            loginButton.LoggedOut += (sender, eve) =>
            {
                // Handle your logout
                log_manager = new LoginManager();
                log_manager.LogOut();
            };

            view.Add(loginButton);

        }
    }
}