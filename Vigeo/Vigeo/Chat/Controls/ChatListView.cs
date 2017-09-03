using System;
using Xamarin.Forms;
using Vigeo.TemplateSelectors;
using System.Collections.Specialized;
using Vigeo.Messages;
//using Vigeo.Views;
using Vigeo.Models;

namespace Vigeo.Controls
{
	public class ChatListView : ListView
	{
		public ChatListView()
		{
			HasUnevenRows = true;
			VerticalOptions = LayoutOptions.FillAndExpand;
			//optimizations
			//IsGroupingEnabled = true;
			//GroupHeaderTemplate = new DataTemplate(() =>
			//{
			//	var view = new ChatMessagesTupleHeaderView();
			//	var cell = new ViewCell
			//	{
			//		View = view
			//	};
			//	return cell;
			//});
			ItemTemplate = new ChatMessageTemplateSelector(App.User.v_id);
			this.SeparatorVisibility = SeparatorVisibility.None;


			ItemSelected += (sender, e) =>
			{
				if (e.SelectedItem != null)
					SelectedItem = null;
			};
		}

		#region public properties

		public static readonly BindableProperty AutoScrollProperty =
					BindableProperty.Create(nameof(AutoScroll), typeof(bool), typeof(ChatListView), true);

		public bool AutoScroll
		{
			get { return (bool)GetValue(AutoScrollProperty); }
			set { SetValue(AutoScrollProperty, value); }
		}

		#endregion

		#region public methods

		public void ScrollToLast()
		{
			if (ItemsSource == null)
				return;
			var enumerator = ItemsSource.GetEnumerator();
			object last = null;
			while (enumerator.MoveNext())
			{
				last = enumerator.Current;
			}
			if (last != null)
				this.ScrollTo(last, ScrollToPosition.MakeVisible, true);
		}

		#endregion

		#region overrides

		protected override void OnPropertyChanging(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == ItemsSourceProperty.PropertyName) {
				Unsubscribe();
			}
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == ItemsSourceProperty.PropertyName)
			{
				Subscribe();
			}
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();
			MessagingCenter.Unsubscribe<object, ScrollToBottomMessage>(this, nameof(ScrollToBottomMessage));
			if (Parent != null) {
				MessagingCenter.Subscribe<object, ScrollToBottomMessage>(this, nameof(ScrollToBottomMessage), (sender, args) => {
					ScrollToLast();
				});
			}
		}

		#endregion

		#region private helpers

		private void Subscribe()
		{
			var obsColl = ItemsSource as INotifyCollectionChanged;
			if (obsColl != null) {
				obsColl.CollectionChanged += ItesSource_CollectionChanged;
			}
		}

		private void Unsubscribe()
		{
			var obsColl = ItemsSource as INotifyCollectionChanged;
			if (obsColl != null)
			{
				obsColl.CollectionChanged -= ItesSource_CollectionChanged;
			}
		}

		private void ItesSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ScrollToLast();
		}

		#endregion
	}
}

