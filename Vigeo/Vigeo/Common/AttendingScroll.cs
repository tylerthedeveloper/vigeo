using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using UXDivers.Artina.Shared;
using System.Diagnostics;

namespace Vigeo
{
    public class AttendingScroll : ContentView
    {
        public AttendingScroll(List<string> pictures, bool withHeader=true)
        {
            BackgroundColor = Color.White;
            var attending = Attending(pictures);
            var scroll = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
                Content = attending,
            };
            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 10,
                Padding = 10,
            };
            var header = Header(pictures);
            if(withHeader) stack.Children.Add(header);
            stack.Children.Add(scroll);

            Content = stack;
        }
        public StackLayout Header(List<string>pictures)
        {
            var numberOfAttendees = new Label
            {
                FontSize = 20,
                TextColor = Color.Black,
                Text = pictures.Count !=1 ?  pictures.Count + " people going" : "1 person going"
            };
            var stack = new StackLayout
            {
                Spacing = 5,
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,
            };
            stack.Children.Add(numberOfAttendees);
            return stack;
        }
        public StackLayout Attending(List<string> pictures)
        {
            var stack = new StackLayout
            {
                Spacing = 15,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
            };
                foreach (var picture in pictures)
                {
                    Debug.WriteLine(picture);
                    stack.Children.Add(new CircleImage
                    {
                        Source = picture,
                        HeightRequest = 40,
                        WidthRequest = 40,
                        BorderThickness = 0,
                    });
                }
            

            return stack;
        }
    }
}
