using Xamarin.Forms;
using System;
using Vigeo.ViewModels;
using Vigeo.Templates;
using System.Diagnostics;

namespace Vigeo.Pages
{
    public class EventPage2 : ContentPage
    {
		
        StackLayout list = new StackLayout
        {
            Padding = 3,
            Spacing = 8
        };
		
        public EventPage2()
        {
			//Application.Current.MainPage.DisplayAlert("Greetings " + App.User.first_name, "Welcome", "Enter");

			//Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
			//NavigationPage.SetHasBackButton(this, true);
			BackgroundColor = Color.Gray.MultiplyAlpha(0.2);
            Padding = 5;
            Title = "Events";
            var scroll = new ScrollView();

            if (App.Events2.Count > 0)
            {
				/*
				foreach (var _event in App.Events2)
				{
					var tile = new EventTemplate2
					{
						BindingContext = _event
					};
					var tap = new TapGestureRecognizer();
					tap.Command = new Command(async () =>
					{
						await Application.Current.MainPage.Navigation.PushAsync(new DetailPage2(_event));
						//await Navigation.PushAsync(new DetailPage(_event));
					});
					tile.layout.GestureRecognizers.Add(tap);

					//firstDT = dt;
					list.Children.Add(tile);
				}
				*/


				DateTime firstDT = new DateTime(1970, 1, 1).AddSeconds(App.Events2[0].start_time);
                //DateTime firstDT = App.Events2[0].start_time_converted);
				list.Children.Add(GetDateView(firstDT));
                foreach (var _event in App.Events2)
                {
                    var tile = new EventTemplate2
                    {
                        BindingContext = _event
                    };
                    var tap = new TapGestureRecognizer();
                    tap.Command = new Command(async () =>
                    {
						//Application.Current.MainPage.DisplayAlert("Congrats", "Yay", "Close");
						//await Application.Current.MainPage.Navigation.PushAsync(new DetailPage2(_event));
                        await Navigation.PushAsync(new DetailPage2(_event));
                    });
                    tile.layout.GestureRecognizers.Add(tap);
					DateTime dt = new DateTime(1970, 1, 1).AddSeconds(_event.start_time);
                    //DateTime dt = Convert.ToDateTime(_event.start_time);
                    if (dt.Day != firstDT.Day)
                    {
                        list.Children.Add(GetDateView(dt));
                    }

                    firstDT = dt;
                    list.Children.Add(tile);
                }
                
            }
            scroll.Content = list;
            Content = scroll;
			//Debug.WriteLine("e page2");
        }

        public View GetDateView(DateTime dt)
        {
            return new Label
            {
                Text = dt.ToString("dddd"),
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 20
            };
        }
    }
}
