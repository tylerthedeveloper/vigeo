using Vigeo.ViewModels;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public partial class EventList : ContentPage
    {
        public EventList()
        {
            InitializeComponent();
            BindingContext = new EventsViewModel2();
        }
    }
}
