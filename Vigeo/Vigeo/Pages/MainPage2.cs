
using Xamarin.Forms;
using Vigeo.ViewModels;
//using Acr.UserDialogs;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Vigeo.Pages
{
	public class MainPage2 : TabbedPage
    {
        public MainPage2()
        {
            Title = "Vigeo";
            //NavigationPage.SetBackButtonTitle(this, "");
            SetupToolbar();
            Children.Add(new EventPage2 { Icon = "calendar.png" });
            Children.Add(new MomentsPage2 { Icon = "ticket.png" });
        }
        public void SetupToolbar()
        {
            var settings = new ToolbarItem
            {
                Text = "Settings",
                Order = ToolbarItemOrder.Primary
            };
            settings.Clicked += async (ob, arg) =>
            {
                await Navigation.PushAsync(new AccountSettings());
            };
            ToolbarItems.Add(settings);
        }
    }
}
