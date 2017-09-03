using System.Diagnostics;
using Vigeo.ViewModels;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public partial class TaskList : ContentPage
    {
        public TaskList()
        {
            InitializeComponent();
            BindingContext = new TaskListViewModel();
			//Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);

		}
    }
}
