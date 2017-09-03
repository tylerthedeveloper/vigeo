using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
//using Acr.UserDialogs;
using System.Net.Http;

namespace Vigeo.Helpers
{
    public class GPS
    {
        public Position pos;
        public double lat;
        public double lng;
        public string ReadableLocation;
        public string Location;
        public IGeolocator locator;
        public GPS()
        {
            locator = CrossGeolocator.Current;
            pos = new Position();
        }
        public async void GetAndPostLocation()
        {

            var locator = CrossGeolocator.Current;
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    //await Page.DisplayAlert("Need location", "Gunna need that location", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                status = results[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {
                pos = await CrossGeolocator.Current.GetPositionAsync(5000);
            }
            else if (status != PermissionStatus.Unknown)
            {








				//                UserDialogs.Instance.ShowError("Location denied!");
                








				return;
                //await Page.DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
            }

            Location = $"POINT({pos.Longitude} {pos.Latitude})";
               /* var uri = "/user/" +  + "/location?location=" + Location;
                var httpClient = new HttpClient { BaseAddress = new Uri(UrlResources.Api) };
                var response =
                    await httpClient.GetAsync(new Uri(uri));*/
            

        }
    }
}

