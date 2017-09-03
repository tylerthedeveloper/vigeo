using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Vigeo.ViewModels
{
	public class BindableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			OnPropertyChanged(propertyName);
		}

		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			RaisePropertyChanged(ExtractPropertyName(propertyExpression));
		}

		protected bool SetProperty<T>(ref T backingField, T Value, Expression<Func<T>> propertyExpression)
		{
			var changed = !EqualityComparer<T>.Default.Equals(backingField, Value);

			if (changed)
			{
				backingField = Value;
				RaisePropertyChanged(ExtractPropertyName(propertyExpression));
			}

			return changed;
		}

		protected bool SetProperty<T>(ref T backingField, T Value, [CallerMemberName] string propertyName = null)
		{
			var changed = !EqualityComparer<T>.Default.Equals(backingField, Value);

			if (changed)
			{
				backingField = Value;
				RaisePropertyChanged(propertyName);
			}

			return changed;
		}

		protected static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			var memberExp = propertyExpression.Body as MemberExpression;

			if (memberExp == null)
				throw new ArgumentException("Expression must be a MemberExpression.", nameof(propertyExpression));

			return memberExp.Member.Name;
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{

		}
	}
}

