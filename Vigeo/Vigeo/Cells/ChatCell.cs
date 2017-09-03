using Xamarin.Forms;
using XLabs.Forms.Controls;
using Vigeo.Models;

namespace Vigeo.Cells
{
    public class ChatCell : ExtendedViewCell
    {
        public ChatCell()
        {
            ShowSeparator = false;
            var message = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 15,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = App.CaliforniaOrange,
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White
            };
            BackgroundColor = Color.White;
            message.SetBinding(Label.TextProperty, "message");
            message.LineBreakMode = LineBreakMode.WordWrap;
            var from = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 15,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Color.White
            };
            from.LineBreakMode = LineBreakMode.NoWrap;
            from.SetBinding(Label.TextProperty, "from");
            var empty = new Label();

            var outter = new StackLayout
            {
                Spacing = 1,
                Children =
                {
                    from,
                    message,
                    empty,
                    empty
                }
            };

            View = outter;
        }
    }
}
