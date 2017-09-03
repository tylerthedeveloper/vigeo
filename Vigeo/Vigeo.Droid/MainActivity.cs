
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Plugin.Permissions;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Net;
using Xamarin.Facebook;
using Android.Content;

[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/app_id")]
[assembly: MetaData("com.facebook.sdk.ApplicationName", Value = "@string/app_name")]

namespace Vigeo.Droid
{
    [Activity (
        Label = "Vigeo", 
        Theme = "@style/VigeoTheme", 
        Icon = "@drawable/icon", 
        NoHistory = true,
        MainLauncher = true,
        WindowSoftInputMode = SoftInput.AdjustResize,
        ConfigurationChanges = ConfigChanges.KeyboardHidden|ConfigChanges.Orientation|ConfigChanges.Keyboard,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        public static ICallbackManager CallbackManager; 
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;
            ServicePointManager.ServerCertificateValidationCallback += delegate
            {
                return true;
            };
            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            FacebookSdk.SdkInitialize(this.ApplicationContext);
            CallbackManager = CallbackManagerFactory.Create();
            UserDialogs.Init(this);

            LoadApplication(new App());

            //ActionBar.SetIcon(new ColorDrawable(Android.Graphics.Color.Transparent));
            

            //Window.AddFlags(WindowManagerFlags.Fullscreen);
			//Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            /*
           var titleId = Resources.GetIdentifier("action_bar_title", "id", "android");
            var titleTextView = FindViewById<TextView>(titleId);
            var layoutParams = (LinearLayout.LayoutParams)titleTextView.LayoutParameters;
            layoutParams.Gravity = GravityFlags.Center;
            layoutParams.Width = Resources.DisplayMetrics.WidthPixels;
            titleTextView.LayoutParameters = layoutParams;
            
            titleTextView.Gravity = GravityFlags.Center;*/
            //Added padding because it is slightly off centered.
            // titleTextView.SetPadding(100, 0, 0, 0);
#pragma warning disable 618
/*            // Hiding ActionBar Icon on Android versions using Material Design
            if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            {
                ActionBar.SetIcon(
                    new ColorDrawable(
                        Resources.GetColor(Android.Resource.Color.Transparent)
                    )
                );
            }
            else
            {
                LocationManager lm = (LocationManager)Forms.Context.GetSystemService(LocationService);
                if (!lm.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    // Build the alert dialog
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Location Services Not Active");
                    builder.SetMessage("Please enable Location Services and GPS");
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {
                        // Show location settings when the user acknowledges the alert dialog
                        Intent intent = new Intent(Settings.ActionLocationSourceSettings);
                        StartActivity(intent);
                    });
                    builder.Create();
                    builder.Show();
                }
            }
        */
#pragma warning restore 618

        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}

