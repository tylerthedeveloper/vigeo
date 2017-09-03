using Xamarin.Forms;
using System.Diagnostics;
using Vigeo.Models;
using Vigeo.Helpers;
using Vigeo.Templates;

namespace Vigeo.Pages
{
	class AccountSettings : ContentPage
	{
        TableView table = new TableView
        {
            Intent = TableIntent.Settings
        };

        UserModel binder;

        public AccountSettings() 
		{
            binder = new UserModel();
            //binder.Init();
            BindingContext = binder;
            
            
            BackgroundColor = Color.Gray.MultiplyAlpha(0.2);
            Padding = 5;

            table.Root.Add(ProfileSection());
            table.Root.Add(LegalSection());
            table.Root.Add(Logout());

			var scroll = new ScrollView
			{
				Content = table,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			
			Content = scroll;
		}
		public TableRoot ProfileSection()
		{

            var root = new TableRoot() {
            new TableSection("Profile Section")
            {
                new SettingsSpecialTemplate("Email", App.User.email, "email"),
            }
        };
			return root;
		}

		public TableRoot LegalSection()
		{

			var root = new TableRoot() 
			{
				new TableSection("Legal")
				{
					new SettingsTemplate("Website"),
					new SettingsTemplate("Help"),
					new SettingsTemplate("Privacy"),
					new SettingsTemplate("Terms of Service")
				}
			};

			return root;
		}

		public TableRoot Logout()
		{
			var button = new Button()
			{
				Text = "Logout",
				TextColor = Color.Red,
				VerticalOptions = LayoutOptions.Center,
				BorderRadius = 0,
				BackgroundColor = Color.White
			};

            button.Clicked += (sender, arg) =>
            {
                //var login = new NavigationPage(new LoginPage());
                //Application.Current.MainPage = login;
                


				//App.appDBservice.DeleteUser();
                


				binder = null;
                /*App._NavPage.Navigation.PushAsync(App.login);
                App._NavPage.Navigation.RemovePage(this);
                App._NavPage.Navigation.RemovePage(App.mainPage);*/
            };

			var root = new TableRoot() {
				new TableSection(" ")
				{
					new ViewCell { View = button }
				}
			};

			return root;
		}
        
        protected override void OnDisappearing()
        {
            //binder.DoSave();
            base.OnDisappearing();
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
                (BindingContext as UserModel).Init();
        }

    }
}
 