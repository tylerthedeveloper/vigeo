using System;

using Xamarin.Forms;

namespace Vigeo.Templates
{
	public class SettingsTemplate : ViewCell
	{
		//Command command
		//default: font Icon
		public SettingsTemplate(string text)
		{
			var stack = new StackLayout() { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.White };

			stack.Children.Add(new Label()
			{
				Text = text,
				TextColor = Color.Blue,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			});

			stack.Children.Add(new Label
			{
				Text = "\uf054",
				FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName,
				TextColor = Color.Gray,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.EndAndExpand
			});

			stack.Padding = new Thickness(2);

			View = stack;
		}

	}
		//special
	public class SettingsSpecialTemplate : ViewCell
	{
		//switch
		public SettingsSpecialTemplate(string text, string bind)
		{
			var stack = new StackLayout() { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.White };

			stack.Children.Add(new Label
			{
				Text = text,
				TextColor = Color.Blue,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.StartAndExpand
			});


			var swish = new Switch
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			swish.SetBinding(Switch.IsToggledProperty, bind, BindingMode.TwoWay);

			stack.Children.Add(swish);

			stack.Padding = new Thickness(4);

			View = stack;

		}

		//entrycell
		public SettingsSpecialTemplate(string text, string placeholder, string bind)
		{
			var stack = new StackLayout() { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.White };

			stack.Children.Add(new Label
			{
				Text = text,
				TextColor = Color.Blue,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.StartAndExpand
			});


			var entry = new Entry
			{
				Placeholder = placeholder,
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				TextColor = Color.Blue,
				Keyboard = Keyboard.Default
			};

			entry.SetBinding(Entry.TextProperty, bind, BindingMode.TwoWay);

			stack.Children.Add(entry);

			stack.Padding = new Thickness(4);

			View = stack;

		}

	}
}


