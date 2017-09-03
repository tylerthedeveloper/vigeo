using Newtonsoft.Json;
//using Realms;
using System;
using Vigeo.Helpers;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;
using Vigeo.Models;

namespace Vigeo
{
    public class FacebookButton : Button
    {
        public Action<object, UserModel, string> OnLogin;
        public static string logg;
        public void Login(object sender, UserModel args, string graph)
        {
            OnLogin?.Invoke(sender, args, graph);
        }
    }
}