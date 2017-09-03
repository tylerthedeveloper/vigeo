using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXDivers.Artina.Shared;
using Vigeo.Models;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Vigeo.Pages
{
    public class DetailB : ContentPage
    {
        StackLayout layout = new StackLayout
        {
            Spacing = 0,
            Padding = 0
        };
        View scroll = new AttendingScroll();
        AllEvents _event;
        string VigeoOrange = "#FFAF3F";
        public DetailB(AllEvents e)
        {
            BackgroundColor = Color.White;
            _event = e;
            Title = _event.title;
             
            layout.Children.Add(GetHeader());
            layout.Children.Add(GetTaskBar());
            /*layout.Children.Add(scroll);
            layout.Children.Add(GetBody());
            layout.Children.Add(GetFooter());*/
            Content = new ScrollView
            {
                Content = layout
            };
        }

        public Grid GetHeader()
        {
            var header = new Grid
            {
                HeightRequest = 200,
            };

            var image = new Image
            {
                Aspect = Aspect.AspectFill,
                Source = _event.img,
            };

            var frame = new Frame
            {
                Padding = 0,
                BackgroundColor = Color.Black.MultiplyAlpha(0.8),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
            };
            var title = new Label
            {
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                Text = _event.title,
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            var timer = new Label
            {
                FormattedText = "02:08:32",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
            };
            frame.Content = timer;
            header.Children.Add(image);
            header.Children.Add(frame);
            return header;
        }
        public StackLayout GetTaskBar()
        {
            var buttons = new StackLayout
            {
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.Gray.MultiplyAlpha(0.2)
            };

            var ticket = new Label
            {
                Text = FontAwesome.FATicket,
                Margin = 15,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromHex(VigeoOrange)
            };

            var calendar = new Label
            {
                Text = FontAwesome.FACalendar,
                Margin = 15, 
                FontSize = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromHex(VigeoOrange)
            };

            var share = new Label
            {
                Text = "\uf1e7",
                Margin = 15,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromHex(VigeoOrange)
            };

            buttons.Children.Add(ticket);
            buttons.Children.Add(calendar);
            buttons.Children.Add(share);

            return buttons;
        }
        public StackLayout GetBody()
        {
            var layout = new StackLayout
            {
                Spacing = 0,
            };
            var info = new StackLayout
            {
                Padding = 15,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                HeightRequest = scroll.Height,
            };

            var distance = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "DISTANCE",
                        LineBreakMode = LineBreakMode.MiddleTruncation,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 20,
                    },
                    new Label
                    {
                        Text = "20 miles away",
                        TextColor = Color.Gray,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 15,
                    },
                }
            };
            var venue = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "VENUE",
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                    },
                    new Label
                    {
                        Text = _event.venue_name,
                        TextColor = Color.Gray,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 15,
                    },
                }
            };
            var performer = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "PERFORMER",
                        LineBreakMode = LineBreakMode.MiddleTruncation,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.End,
                        FontSize = 20,
                    },
                    new Label
                    {
                        Text = _event.performer,
                        TextColor = Color.Gray,
                        HorizontalTextAlignment = TextAlignment.End,
                        FontSize = 15,
                    },
                }
            };
            
            info.Children.Add(distance);
            info.Children.Add(venue);
            info.Children.Add(performer);

            var teaser = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                HeightRequest = scroll.Height,
                Children =
                {
                    new BoxView
                    {
                        BackgroundColor = Color.Red,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    new BoxView
                    {
                        BackgroundColor = Color.Yellow,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    new BoxView
                    {
                        BackgroundColor = Color.Black,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    new Label
                    {
                        FormattedText = "+15\nother\nphotos",
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };

            layout.Children.Add(info);
            layout.Children.Add(teaser);

            return layout;
        }

        public StackLayout GetFooter()
        {
            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Frame
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.White,
                        Content = new Label
                        {
                            Text  = "Get tickets",
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontSize = 20,
                        }
                    },
                    new Frame
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.White,
                        Content = new Label
                        {
                            Text = "Join Moments",
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontSize = 20,
                        }
                    }
                }
            };

            return buttons;
        }
    }
}
