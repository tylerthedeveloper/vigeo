using System.Diagnostics;
using Vigeo.Models;
using Vigeo.ViewModels;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public partial class TaskDetail : ContentPage
    {
        public TaskDetail(TodoItem item = null)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(item);
			//Debug.WriteLine(Application.Current.MainPage.Navigation.NavigationStack.Count);

        }
    }
}
