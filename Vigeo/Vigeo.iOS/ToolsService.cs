using System;
using Vigeo.iOS;
using Xamarin.Forms;
using Facebook.LoginKit;
using Vigeo.Dependencies;

[assembly: Dependency(typeof(ToolsService))]
namespace Vigeo.iOS
{
    public class ToolsService : ITools
    {
        public void LogoutFromFacebook()
        {
            var fbSession = new LoginManager();
            fbSession.LogOut();
        }
    }
}