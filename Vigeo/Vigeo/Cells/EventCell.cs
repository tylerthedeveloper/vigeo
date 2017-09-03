using Xamarin.Forms;
using XLabs.Forms.Controls;
using UXDivers.Artina.Shared;

namespace Vigeo.Cells
{
    public class EventCell : ViewCell
    {
        public EventCell()
        {
            
            var layout = new Grid
            {
                RowSpacing = 0
            };
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });

            var card = new Grid
            {
                BackgroundColor = Color.White,
                ColumnSpacing = 0,
                RowSpacing = 0,
                Padding = 20
            };
            card.RowDefinitions.Add(new RowDefinition { Height = new GridLength(44) });
            card.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            card.RowDefinitions.Add(new RowDefinition { Height = new GridLength(44) });
            card.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });

            var header = new AbsoluteLayout
            {
                Children =
                {
                    new Label {
                        TranslationX = 50,
                        TranslationY = 0,
                        HeightRequest = 40,
                        Text = "username here",
                        VerticalTextAlignment = TextAlignment.Center
                    }
                }
            };

            var body = new StackLayout
            {
                Padding = 20,
                Spacing = 20,

            };

            layout.Children.Add(header, 0, 0);
            //layout.Children.Add(card, 0, 1);
            this.View = layout;
          
        }
    }
}
