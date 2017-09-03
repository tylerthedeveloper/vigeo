using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;
using Vigeo.Abstractions;
using Vigeo.Models;
using Xamarin.Forms;

namespace Vigeo.ViewModels
{
    public class EventsViewModel2 : BaseViewModel
    {
        public EventsViewModel2()
        {
            //Title = "Task List";
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            //RefreshList();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            events.CollectionChanged += this.OnCollectionChanged;
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Debug.WriteLine("[TaskList] OnCollectionChanged: Items have changed");
        }

		//ObservableCollection<AllEventsModel2> Events = EventsViewModel.GetEvents2();

		ObservableCollection<AllEventsModel2> events = new ObservableCollection<AllEventsModel2>();
        public ObservableCollection<AllEventsModel2> Events
        {
            get { return events; }
            set { SetProperty(ref events, value, "Events"); }
        }

        AllEventsModel2 selectedEvent;
        public AllEventsModel2 SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                SetProperty(ref selectedEvent, value, "SelectedItem");
                if (selectedEvent != null)
                {
					//Application.Current.MainPage.Navigation.PushAsync(new Pages.DetailPage(selectedEvent));
					Application.Current.MainPage.DisplayAlert("eventname", "you clicked on: " + selectedEvent.eventname, "ok");
                    SelectedEvent = null;
                }
            }
        }

        //Command refreshCmd;
        //public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

		/*
        async Task ExecuteRefreshCommand()
        {
           
            try
            {
                var table = App.CloudService.GetTable<AllEventsModel>();
                var list = await table.ReadAllItemsAsync();
                Events.Clear();
                foreach (var item in list)
                    Events.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EventList] Error loading items: {ex.Message}");
            }

        }

		*/

		/*
        async Task RefreshList()
        {
            await ExecuteRefreshCommand();
            MessagingCenter.Subscribe<TaskDetailViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
        }
        */
    }
}
