using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Vigeo.Templates
{
    public class EventTemplate2 : ContentView
    {
        public Grid layout = new Grid
        {
            BackgroundColor = Color.Black,
            HeightRequest = 200
        };

        public EventTemplate2()
        {
            layout.Children.Add(GetImage());
            layout.Children.Add(GetBody());

            Content = layout;
        }
        public Image GetImage()
        {
            var image = new Image
            {
                Aspect = Aspect.AspectFill,
                Opacity = 0.7
            };
            image.SetBinding(Image.SourceProperty, "banner_url");
            return image;
        }
        public Grid GetBody()
        {
            var body = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0,
                Padding = new Thickness(20, 20, 20, 10),
                VerticalOptions = LayoutOptions.EndAndExpand,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = '*' },
                    new RowDefinition { Height = 40 }
                }
            };

            var title = new Label
            {
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Start
            };
            title.SetBinding(Label.TextProperty, "eventname");

            var info = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var venue = new Button
            {
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End
            };
            venue.SetBinding(Button.TextProperty, "location");

            var date = new Label
            {
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                TranslationY = -10,
                TranslationX = -5,
            };
            date.SetBinding(Label.TextProperty, "start_time_display");
            body.Children.Add(title, 0, 0);
            info.Children.Add(date);
            body.Children.Add(info, 0, 1);
            return body;
        }
    }
}
