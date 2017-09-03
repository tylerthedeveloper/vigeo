using Vigeo.Droid;
using Vigeo.Dependencies;
using Xamarin.Forms;
using Xamarin.Facebook.Login;

[assembly: Dependency(typeof(ToolsService))]
namespace Vigeo.Droid
{
    public class ToolsService : ITools
    {
        public void LogoutFromFacebook()
        {
            LoginManager.Instance.LogOut();
        }
    }
}