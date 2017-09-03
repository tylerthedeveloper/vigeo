﻿using Xamarin.Forms;
using System;
using Vigeo.ViewModels;
using Vigeo.Templates;
using System.Diagnostics;

namespace Vigeo.Pages
{
    public class EventPage : ContentPage
    {
		
        StackLayout list = new StackLayout
        {
            Padding = 3,
            Spacing = 8
        };
		
        public EventPage()
        {
			Application.Current.MainPage.DisplayAlert("Greetings " + App.User.first_name, "Welcome", "Enter");
			//Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
			//NavigationPage.SetHasBackButton(this, true);
			BackgroundColor = Color.Gray.MultiplyAlpha(0.2);
            Padding = 5;
            Title = "Events";
            var scroll = new ScrollView();
            if (App.Events.Count > 0)
            {
                DateTime firstDT = Convert.ToDateTime(App.Events[0].start_time);
                list.Children.Add(GetDateView(firstDT));
                foreach (var _event in App.Events)
                {
                    var tile = new EventTemplate
                    {
                        BindingContext = _event
                    };
                    var tap = new TapGestureRecognizer();
                    tap.Command = new Command(async () =>
                    {

						await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(_event));
                        //await Navigation.PushAsync(new DetailPage(_event));
                    });
                    tile.layout.GestureRecognizers.Add(tap);
                    DateTime dt = Convert.ToDateTime(_event.start_time);
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
