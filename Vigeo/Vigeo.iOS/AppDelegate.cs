using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using XLabs.Forms;
using UXDivers.Artina.Grial;
using Facebook.CoreKit;
using System;
//using HockeyApp.iOS;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using Xamarin.Auth;
using HockeyApp.iOS;

namespace Vigeo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : XFormsApplicationDelegate// global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {	
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			//xamarin global

			Forms.Init();

			//Init Facebook SDK parameters
			Profile.EnableUpdatesOnAccessTokenChange(true);
			Settings.AppID = "1654350664885736";
			Settings.DisplayName = "Vigeo";

			//global SDK inits
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            CurrentPlatform.Init();
			//FormsMaps.Init();
			//KeyboardOverlapRenderer.Init();
			//KeyboardOverlap.Forms.Plugin.iOSUnified.KeyboardOverlapRenderer.Init();


			LoadApplication(new App());

            //var x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            //x = typeof(Xamarin.Forms.Themes.iOS.UnderlineEffect);
            //Appearance.Configure();

			//Hockey
			/*
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.LogLevel = BITLogLevel.Verbose;
			manager.Configure("6e1a558844e347f8978ca081e920cec2");
			manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds
			manager.StartManager();
			*/

            return base.FinishedLaunching(app, options);
        } 

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			Debug.WriteLine("in open url");
			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
		}

    }
}
