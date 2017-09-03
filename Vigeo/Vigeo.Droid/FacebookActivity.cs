using Android.Runtime;
using Vigeo.Droid;
using System;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Vigeo;
using Org.Json;
using Android.OS;
using Vigeo.Pages;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[assembly: ExportRenderer(typeof(FacebookButton), typeof(FacebookButtonRenderer))]
namespace Vigeo.Droid
{
    /// <summary>
    /// FacebookLogin button renderer implementation for Xamarin.Forms in the Android side.
    /// This implement the native look and feel from Facebook SDK for Android LoginButton object and handle
    /// Facebook Login basic authentication for Android
    /// </summary>
    public class FacebookButtonRenderer : ButtonRenderer
    {
        public List<string> readPermissions = new List<string> { "public_profile", "email", "user_friends" };
        private LoginButton loginButton;
        public static LoginManager log_manager;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;
            loginButton = new LoginButton(Forms.Context)
            {
                LoginBehavior = LoginBehavior.NativeOnly
            };
            loginButton.SetReadPermissions(readPermissions.ToArray());
            //Implement FacebookCallback with LoginResult type to handle Callback's result
            var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = loginResult =>
                {
                    LoginPage.Loading();
                    //loginButton.RemoveFromParent();
                    FacebookButton facebookButton = (FacebookButton)e.NewElement;

                    if (loginResult.AccessToken != null)
                    {
                        var fb_id = loginResult.AccessToken.UserId;
                        var access_token = loginResult.AccessToken.Token;
                        var expires = loginResult.AccessToken.Expires;
                        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        var token_expiration = epoch.AddMilliseconds(expires.Time);
                        Bundle parameters = new Bundle();
                        parameters.PutString("fields", "id,first_name,last_name,gender,age_range,email,friends,picture");

                        var request = GraphRequest.NewMeRequest(loginResult.AccessToken, new IGraphCallBack
                        {
                            OnCompleted = (JSONObject json, GraphResponse response) =>
                            {
                                var userInfo = JObject.Parse(json.ToString());
                                System.Diagnostics.Debug.WriteLine(userInfo.ToString());
                                var age_range = userInfo["age_range"];
                                var picture = userInfo["picture"]["data"]["url"].ToString();
                                var keys = new List<string> { "fb_id", "access_token", "token_type", "email", "name", "first_name", "last_name", "gender", "age", "picture", "friends", "interest_model" };
                                System.Diagnostics.Debug.WriteLine(picture);
                                var result = new JObject
                                {
                                    {keys[0], fb_id},
                                    {keys[1], access_token},
                                    {keys[2], "facebook"},
                                    {keys[3], userInfo["email"] },
                                    {keys[5], userInfo["first_name"] },
                                    {keys[6], userInfo["last_name"] },
                                    {keys[7], userInfo["gender"] },
                                    {keys[8], (int.Parse(age_range["min"].ToString()) + int.Parse(age_range["max"].ToString()) / 2) },
                                    {keys[9], picture },
                                    {keys[10], userInfo["friends"]["data"] }
                                };
                                App.SuccessfulLogin(result);
                            }
                        });
                        request.Parameters = parameters;
                        request.ExecuteAsync();
                        log_manager = LoginManager.Instance;
                        log_manager.LogOut();

                    }
                    else
                        System.Diagnostics.Debug.WriteLine("problem");
                    /*
                        Pass the parameters into Login method in the FacebookButton 
                        object and handle it on Xamarin.Forms side
                    */

                },
                HandleCancel = () =>
                {
                    //Handle any cancel the user has perform
                    System.Diagnostics.Debug.WriteLine("User canceled the login");
                },
                HandleError = loginError =>
                {
                    //Handle any error happends here
                    System.Diagnostics.Debug.WriteLine("Operation throws an error: " + loginError.Message);
                }
            };
            LoginManager.Instance.RegisterCallback(MainActivity.CallbackManager, loginCallback);
            //Set the LoginButton as NativeControl
            SetNativeControl(loginButton);

        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }


    }

    /// <summary>
    /// FacebookCallback<TResult> class which will handle any result the FacebookActivity returns.
    /// </summary>
    /// <typeparam name="TResult">The callback result's type you will handle</typeparam>
    /// 
    public class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            HandleCancel?.Invoke();
        }

        public void OnError(FacebookException error)
        {
            HandleError?.Invoke(error);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            HandleSuccess?.Invoke(result.JavaCast<TResult>());
        }
    }

    public class IGraphCallBack : Java.Lang.Object, GraphRequest.IGraphJSONObjectCallback
    {
        public Action<JSONObject, GraphResponse> OnCompleted { get; set; }

        void GraphRequest.IGraphJSONObjectCallback.OnCompleted(JSONObject json, GraphResponse response)
        {
            this.OnCompleted.Invoke(json, response);
        }
    }
}