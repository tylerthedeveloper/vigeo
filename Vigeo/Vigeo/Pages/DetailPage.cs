using System.Collections.Generic;
using System.Diagnostics;
using Vigeo.Models;
using Vigeo.Templates;
using Vigeo.ViewModels;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public class DetailPage : ContentPage
    {
        List<string> attending;

        StackLayout layout = new StackLayout
        {
            Spacing = 10,
            Margin = 5,
        };

        AttendingScroll scroll;
        Label label;
        AllEventsModel _event;
        public DetailPage(AllEventsModel e)
        {
			Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);
            //NavigationPage.SetBackButtonTitle(this, ""); 
            BackgroundColor = Color.Gray.MultiplyAlpha(0.2);
            _event = e;
            Title = "Details";
            layout.Children.Add(GetHeader());
            scroll = new AttendingScroll(_event.attending);
            layout.Children.Add(scroll);
            layout.Children.Add(GetDetails());
            layout.Children.Add(GetFooter());
            Content = new ScrollView
            {
                Content = layout
            };
           
        }
        public View GetHeader()
        {
            return new EventTemplate//EventTemplate
            {
                BindingContext = _event
            };
        }

        public StackLayout GetDetails()
        {
            var info = new StackLayout
            {
                Padding = 5,
                Spacing = 5,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = scroll.Height,
                BackgroundColor = Color.White,
            };

            var distance = new StackLayout
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                Children =
                {
                    new Label
                    {
                        Text = "DISTANCE",
                        LineBreakMode = LineBreakMode.NoWrap,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                    },
                    new Label
                    {
                        Text = "20 miles",
                        TextColor = Color.Gray,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 15,
                    },
                }
            };
            var venue = new StackLayout
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
                Children =
                {
                    new Label
                    {
                        Text = "VENUE",
                        LineBreakMode = LineBreakMode.NoWrap,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                    },
                    new Label
                    {
                        Text = _event.location,
                        TextColor = Color.Gray,
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 15,
                    },
                }
            };
            
            info.Children.Add(distance);
            info.Children.Add(venue);

            return info;
        }

        public StackLayout GetTemptations()
        {
            
            var teaser = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.Aqua,
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
                        TextColor = Color.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
            return teaser;
        }

        public StackLayout GetFooter()
        {
            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            var frame = new Frame
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
            };
            label = new Label
            {
                Text = App.Attending.Contains(_event.event_id) ? "Go to Moments" : "Join Moments",
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 20
            };
            frame.Content = label;
            buttons.Children.Add(frame);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += ((o, e) =>
            {
				
                if (!App.Attending.Contains(_event.event_id))
                { 
					_event.attending.Add(App.User.picture);
                    App.Attending.Add(_event.event_id);
                    //
					//EventsViewModel.UpdateAttending(_event.event_id);
					//
                    label.Text = "Go to Moments";
                }
                else
                {
					/*
                    var page = Navigation.NavigationStack[0] as TabbedPage;
                    page.CurrentPage = page.Children[1];
                    Navigation.PushAsync(new ChatPage(_event));
                    Navigation.RemovePage(this);
                    */
					Application.Current.MainPage.DisplayAlert("Congrats", "Yay", "Close");

				}
                
            });

            buttons.GestureRecognizers.Add(tapGestureRecognizer);
            return buttons;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                attending = App.Attending;
                ApplyBindings();
                ForceLayout();
                Debug.WriteLine("changed");
            }
        }


        public void EventInit()
        {
            attending = App.Attending;
        }

    }
}
