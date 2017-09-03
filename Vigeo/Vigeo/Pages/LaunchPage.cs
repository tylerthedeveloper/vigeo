using Vigeo.Models;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Vigeo.Pages
{
    public class LaunchPage : ContentPage
    {
        public LaunchPage()
        {
            BackgroundImage = "bg.jpg";
            NavigationPage.SetHasNavigationBar(this, false);
            Content = LoginOrSignup();
        }

        public StackLayout LoginOrSignup()
        {
            var stack = new StackLayout
            {
                Spacing = 5,
                Padding = new Thickness(0, 5, 0, 5),
                VerticalOptions = LayoutOptions.EndAndExpand,
                MinimumHeightRequest = 300
            };
            var signup = new Button
            {
                Text = "SIGN UP",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                BorderRadius = 0,
                FontSize = 30
            };
            var login = new Button
            {
                Text = "LOG IN",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                BorderRadius = 0,
                FontSize = 30,
            };
            login.Clicked += (sender, args) =>
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            };
            stack.Children.Add(login);
            stack.Children.Add(signup);
            return stack;
        }
    }
}
