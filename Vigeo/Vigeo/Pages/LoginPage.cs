using Xamarin.Forms;
using Vigeo;
using Vigeo.Dependencies;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Vigeo.Models;
//using Acr.UserDialogs;
using System;
using Vigeo.ViewModels;

namespace Vigeo.Pages
{

    public class LoginPage : ContentPage
    {
        public static string tokenType;
        static ActivityIndicator loadingIndicator;
        static Image bg;
		static Button azureLoginButton;
		//static Button fbButton;
        RelativeLayout layout = new RelativeLayout();

        public LoginPage()
        {
            BindingContext = new LoginPageViewModel();

            Title = "Login";
            BackgroundColor = Color.White;

			NavigationPage.SetHasNavigationBar(this, false);

            Device.BeginInvokeOnMainThread(async () =>
            {
                while (!App.acceptedTerms)
                {
                    var displayAlert = await DisplayAlert("Terms", "By continuing you agree to the terms and service", "OK", "cancel");
                    App.acceptedTerms = displayAlert;
                }
            });


            bg = new Image
            {
                Source = "bg.jpg",
                Aspect = Aspect.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            loadingIndicator = new ActivityIndicator() { Color = Color.Red, IsRunning = false, IsVisible = false };

            var logo = new Image
            {
                Source = "Vigeo.png"
            };


            azureLoginButton = new Button
            {
                //VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
				Command = (BindingContext as LoginPageViewModel).LoginCommand,
				Text = "Login with Facebook"
            };
            


            /*
            fbButton = new FacebookButton
            {
                VerticalOptions = LayoutOptions.End
            };
			*/

            layout.Children.Add(bg, Constraint.RelativeToParent((parent) =>
            {
                return -(parent.Width / 2);
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return 0;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Width * 2;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Height * .91;
            }));

            layout.Children.Add(loadingIndicator, Constraint.RelativeToParent((parent) =>
            {
                return (parent.Width / 2);
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return (parent.Height / 2);
            }));

            layout.Children.Add(logo, Constraint.RelativeToParent((parent) =>
            {
                return parent.Width * .2;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Height * .03;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Width * .6;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Height / 5;
            }));

			//fbButton
            layout.Children.Add(azureLoginButton, Constraint.Constant(0),
             Constraint.RelativeToParent((parent) =>
             {
                 return parent.Height - (parent.Height * .10);
             }),
             Constraint.RelativeToParent((parent) =>
             {
                 return parent.Width;
             }),
             Constraint.RelativeToParent((parent) =>
             {
                 return parent.Height * .10;
             }));
            


            Content = layout;

        }

        public static async void LoginAlert(string title, string mess, string accept, string cancel)
        {
            var displayAlert = await Application.Current.MainPage.DisplayAlert(title, mess, accept, cancel);
        }

        public static void Loading()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
			//fbButton.IsVisible = false;
			azureLoginButton.IsVisible = false;
			bg.IsVisible = false;
        }

		public Command LoginButtonClicked
		{
			get
			{
				return new Command(() =>
				{
					Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new FacebookLoginPage()));
					//Application.Current.MainPage = new FacebookLoginPage();
				});
			}
		}

    }
}
