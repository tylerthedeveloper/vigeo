using Xamarin.Forms;
namespace Vigeo
{
    public class MyCircleIcon :	ContentView
	{
		//just icon
		public MyCircleIcon(string fIcon, Color fColor, int Fsize)
		{
			var Icon = new Label { Text = fIcon, FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor = fColor, FontSize = Fsize, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
			Content = Icon;
		}

		//tag
		public MyCircleIcon(string fIcon, Color fColor, int Fsize, Color tColor, string text, int textSize)
		{
			var icon = new Label { Text = fIcon, FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor = fColor, FontSize = Fsize, VerticalTextAlignment = TextAlignment.Start, HorizontalTextAlignment = TextAlignment.Start };
			var tag = new Label { Text = text, TextColor = tColor, FontSize = textSize, VerticalTextAlignment = TextAlignment.Center, LineBreakMode=LineBreakMode.WordWrap };
			var grid = new Grid { RowSpacing = 0 };

			grid.Children.Add(icon, 0, 0);
			grid.Children.Add(tag, 0, 1);

			Content = grid;
		}

		//with circle
		public MyCircleIcon(Color bColor, int bSize, string fIcon, Color fColor, int Fsize)
		{
			var Circle = new Label { Text = "\uf111", FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor=bColor, FontSize=bSize, VerticalTextAlignment=TextAlignment.Center, HorizontalTextAlignment=TextAlignment.Center};
			var Icon = new Label { Text = fIcon, FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor=fColor, FontSize = Fsize, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
			var grid = new Grid();

			grid.Children.Add(Circle);
			grid.Children.Add(Icon);

			Content = grid;
		}

		//with circle and label
		public MyCircleIcon(Color bColor, int bSize, string fIcon, Color fColor, int Fsize, string text, int textSize)
		{
			var Circle = new Label { Text = "\uf111", FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor = bColor, FontSize = bSize, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
			var Icon = new Label { Text = fIcon, FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor = fColor, FontSize = Fsize, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
			var label = new Label { Text = text, HorizontalTextAlignment = TextAlignment.Center, FontSize=textSize, LineBreakMode = LineBreakMode.WordWrap };
			var grid = new Grid { RowSpacing = 1, HorizontalOptions = LayoutOptions.Center };
			grid.Children.Add(Circle, 0, 0);
			grid.Children.Add(Icon, 0, 0);
			grid.Children.Add(label, 0, 1);

			Content = grid;
		}


		public MyCircleIcon(string pre, Color bColor, int bSize, string fIcon, Color fColor, int Fsize, string post, int textSize)
		{
			var prLabel = new Label { Text = pre, HorizontalTextAlignment = TextAlignment.Center, FontSize = textSize};
			var Circle = new Label { Text = "\uf111", FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor = bColor, FontSize = bSize, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
			var Icon = new Label { Text = fIcon, FontFamily = UXDivers.Artina.Shared.FontAwesome.FontName, TextColor = fColor, FontSize = Fsize, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
			var poLabel = new Label { Text = post, HorizontalTextAlignment = TextAlignment.Center, FontSize = textSize };
			var grid = new Grid 
			{ 
				RowSpacing = 0, 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				RowDefinitions =
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto }

				}
				                                                 
			};
			grid.Children.Add(prLabel, 0, 0);
			grid.Children.Add(Circle, 0, 1);
			grid.Children.Add(Icon, 0, 1);
			grid.Children.Add(poLabel, 0, 2);

			Content = grid;
		}

		/*
		public static class CircleIcon
		{
			public static Label Circle = new Label 
			{ 
				Text = "\uf111", FontFamily = Shared.FontAwesome.FontName, 
				FontSize= 40, 
				VerticalTextAlignment=TextAlignment.Center, 
				HorizontalTextAlignment=TextAlignment.Center,
				BackgroundColor=Color.Blue
			};
		}
		*/

	}
}

/*
var socialCircle = new Label { Text = "\uf111", FontFamily = Shared.FontAwesome.FontName , FontSize= 40, VerticalTextAlignment=TextAlignment.Center, HorizontalTextAlignment=TextAlignment.Center};
var socialIcon = new Label { Text = "\uf112", FontFamily = Shared.FontAwesome.FontName, TextColor=Color.White, FontSize = 20, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
var infoCircle = new Label { Text = "\uf111", FontFamily = Shared.FontAwesome.FontName, FontSize = 40, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
var infoIcon = new Label { Text = "\uf112", FontFamily = Shared.FontAwesome.FontName, TextColor = Color.White, FontSize = 20, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
var aCircle = new Label { Text = "\uf111", FontFamily = Shared.FontAwesome.FontName, FontSize = 40, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
var aIcon = new Label { Text = "\uf112", FontFamily = Shared.FontAwesome.FontName, TextColor = Color.White, FontSize = 20, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
*/


/*
//var calCircle = new Label { Text = "\uf111", FontFamily = Shared.FontAwesome.FontName, FontSize = 38, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
//var calIcon = new Label { Text = "\uf073", FontFamily = Shared.FontAwesome.FontName, FontSize = 18, TextColor=Color.White, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };
var mapStack = new Grid { RowSpacing = 1, HorizontalOptions=LayoutOptions.Center };


var calIcon = new MyCircleIcon(Color.Black, 38, "\uf073", Color.White, 18);
var calLabel = new Label { Text = "Date", FontSize=10 };
calStack.Children.Add(calIcon, 0, 0);
calStack.Children.Add(calLabel, 0, 1);

var mapIcon = new MyCircleIcon(Color.Black, 38, "\uf073", Color.White, 18);
var mapLabel = new Label { Text = "Venue", FontSize=10 };
mapStack.Children.Add(mapIcon, 0, 0);
mapStack.Children.Add(mapLabel, 0, 1);
*/

