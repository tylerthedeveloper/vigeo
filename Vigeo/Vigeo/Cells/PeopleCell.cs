using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Vigeo.Cells
{
    public class PeopleCell : ExtendedViewCell
    {
        public PeopleCell()
        {
            ShowSeparator = false;
            var nameLabel = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 30,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = App.VigeoOrange
            };
            nameLabel.SetBinding(Label.TextProperty, "name");

            var tagLabel = new Label
            {
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = App.CaliforniaOrange
            };
            tagLabel.SetBinding(Label.TextProperty, new Binding("tag_str"));

            var detailsLabel = new Label
            {
                FontSize = 15,
               HorizontalTextAlignment = TextAlignment.Center,
                TextColor = App.CaliforniaOrange
            };
            detailsLabel.SetBinding(Label.TextProperty, new Binding("details"));
            var detailsLayout = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {nameLabel, tagLabel, detailsLabel}
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {detailsLayout}
            };
            var objFrameInner = new Frame
            {
                Padding = new Thickness(Device.OnPlatform(4, 5, 5), 10, Device.OnPlatform(4, 5, 5), 10),
                OutlineColor = App.Bg,
                BackgroundColor = Color.White,
                Content = cellLayout
            };

            var objFrameOuter = new Frame
            {
                Padding = new Thickness(15, 15, 15, 15),
                Content = objFrameInner,
                BackgroundColor = App.Bg
            };

            View = objFrameOuter;
        }
    }
}
