using System.Collections.ObjectModel;
using Xamarin.Forms;
using Vigeo.Models;
using Vigeo.Pages;
using Vigeo.Helpers;
using System.Collections.Generic;
using Vigeo.Abstractions;
using Vigeo.Services;
using Vigeo.ViewModels;
using Vigeo.Services.AllEventsServices;
using System.Diagnostics;
using System;
//using Xamarin.Auth;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Vigeo
{
	public class App : Application
	{
		#region: Realm
		//Realm
		//public static RealmDBService appDBservice = new RealmDBService();
		//public static Realm _realm = Realm.GetInstance();
		#endregion

		#region: User
		public static UserModel User = new UserModel(); //{ get; set; }
		public static bool acceptedTerms = false;
		public static List<string> Attending = new List<string>();
		#endregion

		#region: Other
		public static ObservableCollection<AllEventsModel> Events { get; set; } // = new ObservableCollection<AllEventsModel>();
		public static ObservableCollection<AllEventsModel2> Events2 = new ObservableCollection<AllEventsModel2>();
		//public static ObservableCollection<AllEventsModel2> Events = new ObservableCollection<AllEventsModel2>();
		public static NavigationPage mainPage { get; set; }
		public static ICloudService CloudService { get; set; }
		#endregion

		public App()
		{
			//User = new UserModel();
			CloudService = new AzureCloudService();
			ServiceLocator.Instance.Add<ICloudService, AzureCloudService>();

			//MainPage = new NavigationPage(new Pages.MainPage());

			try
			{
				//Events = EventsViewModel.GetEvents();
				Events2 = EventsViewModel.GetEvents2();
			}

			catch (Exception e)
			{
				Debug.WriteLine("no inter" + e.Message);
				//Current.MainPage.DisplayAlert("no inter", "no inter" + e.Message, "ok");
			}


			MainPage = new NavigationPage(new EntryPage());
			//MainPage = new LoginPage();


			//azure
			//EventsViewModel.GetEvents2();
			//City.GetCityEvents();
		}

		#region: Navigation
		public static Page GetMainPage()
		{
			if (User != null)
			{
				//EventsViewModel.GetAttending(User.v_id);
				mainPage = new NavigationPage(new MainPage());
				return mainPage;
			}

			return new NavigationPage(new LoginPage());
		}


		public static async void SuccessfulLogin() //JObject json
		{
			//appDBservice.CreateUser(json);
			//appDBservice.SendNewUser(json.ToString());
			//User = JsonConvert.DeserializeObject<UserModel>(json.ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			Debug.WriteLine("succ log");
			//Current.MainPage = new NavigationPage(new MainPage2());
			await Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MainPage2()));
			/*
			await Current.MainPage.Navigation.PushModalAsync(new ContentPage
			{
				Content = new Label
				{
					Text = "Testing build + COming Soon",
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand
				}
			});
			*/
		}

		public static void SuccessfulLogout()
		{
			Current.MainPage = new NavigationPage(new LoginPage());
		}
		#endregion
		/*
        protected override void OnResume()
        {
            if (User != null)
                EventsViewModel.GetAttending(User.v_id);
            base.OnResume();
        }
		*/

	}
}
