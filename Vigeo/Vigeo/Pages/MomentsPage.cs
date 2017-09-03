using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vigeo.Models;
using Vigeo.Templates;
using Vigeo.ViewModels;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public class MomentsPage :  ContentPage
    {
        StackLayout list = new StackLayout
        {
            Padding = 3,
            Spacing = 8
        };
        
        public MomentsPage()
        {
            //attending = App.Attending;
            //BindingContext = attending;
            //BindingContext
            //user = App.User;
            //BindingContext = user;
            //user = new UserModel();

            ApplyBindings();

            BackgroundColor = Color.Gray.MultiplyAlpha(0.2);
            Padding = 5;
            Title = "Moments";
            var scroll = new ScrollView();
            DateTime firstDT = Convert.ToDateTime(App.Events[0].start_time);
            //list.Children.Add(GetDateView(firstDT));
            //attending = BindingContext as List<string>;
            //var attending = user.attending;
            if (App.Events.Count > 0)
            {
                foreach (var _event in App.Events)
                {
                    if (!App.Attending.Contains(_event.event_id)) continue;
                    var tile = new MomentTemplate(_event);
                    var tap = new TapGestureRecognizer();
                    tap.Command = new Command(async () =>
                    {
                        //await Navigation.PushAsync(new ChatPage(_event)); //MessageGroups = _event.messages 
						await Navigation.PushAsync(new CarouselPage());
                	});
                    tile.layout.GestureRecognizers.Add(tap);
                    DateTime dt = Convert.ToDateTime(_event.start_time);
                    if (dt.Day != firstDT.Day)
                    {
                        //list.Children.Add(GetDateView(dt));
                    }

                    firstDT = dt;
                    list.Children.Add(tile);

                }
            }
            scroll.Content = list;
            Content = scroll;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.Attending.Count > list.Children.Count)
            {
                for(int i = list.Children.Count; i < App.Attending.Count; i++)
                {
                    var item = App.Events.Where(_event => _event.event_id == App.Attending[i]).First();
                    var tile = new MomentTemplate(item);
                    var tap = new TapGestureRecognizer();
                    tap.Command = new Command(async () =>
                    {
                        await Navigation.PushAsync(new ChatPage(item));
                    });
                    tile.layout.GestureRecognizers.Add(tap);
                    list.Children.Add(tile);
                }
            }
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
