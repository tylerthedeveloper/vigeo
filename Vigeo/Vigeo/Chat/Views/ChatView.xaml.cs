using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Vigeo.Views
{
	public partial class ChatView : ContentPage, IKeyboardOverlap
	{
		public ChatView()
		{
			InitializeComponent();
		}

		#region IKeyboardOverlap implementation

		public void OnKeyboardHide(double keyboardHeight)
		{
			
		}

		public async void OnKeyboardShow(double keyboardHeight)
		{
			await Task.Delay(300);
			_list.ScrollToLast();	
		}

		#endregion
	}
}

