using System;
namespace Vigeo
{
	public interface IKeyboardOverlap
	{
		void OnKeyboardShow(double keyboardHeight);

		void OnKeyboardHide(double keyboardHeight);
	}
}

