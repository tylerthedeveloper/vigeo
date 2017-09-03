using Vigeo.ViewModels;
using Xamarin.Forms;

namespace Vigeo.Pages
{
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.LoginPageViewModel();
        }
    }
}
