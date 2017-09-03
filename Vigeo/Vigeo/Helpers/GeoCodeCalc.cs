using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vigeo.Helpers
{
    public static class GeoCodeCalc
    {
        public const double EarthRadiusInMiles = 3956.0;
        public const double EarthRadiusInKilometers = 6367.0;
        public static string GeoCodeApi = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        public static string GeoCodeKey = "AIzaSyBZZBiqmFgPo9GRESa2VThklu4jgZo3xS4";

        public static double ToRadian(double val)
        {
            return val*(Math.PI/180);
        }

        public static double DiffRadian(double val1, double val2)
        {
            return ToRadian(val2) - ToRadian(val1);
        }

        public static double CalcDistance(double lat1, double lng1, double lat2, double lng2)
        {
            return CalcDistance(lat1, lng1, lat2, lng2, GeoCodeCalcMeasurement.Miles);
        }

        public static double CalcDistance(double lat1, double lng1, double lat2, double lng2, GeoCodeCalcMeasurement m)
        {
            var radius = EarthRadiusInMiles;

            if (m == GeoCodeCalcMeasurement.Kilometers)
            {
                radius = EarthRadiusInKilometers;
            }
            return radius*2*
                   Math.Asin(Math.Min(1,
                       Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2))/2.0), 2.0) +
                                  Math.Cos(ToRadian(lat1))*Math.Cos(ToRadian(lat2))*
                                  Math.Pow(Math.Sin((DiffRadian(lng1, lng2))/2.0), 2.0)))));
        }
        public static string GeoCode(string address)
        {/*
            var uri = GeoCodeApi + address + "&key=" + GeoCodeKey;
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(uri);
            
            try
            {
                var sub = response.Substring(response.IndexOf("\"location\" : ") + "\"location\" : ".Length);
                var lat = sub.Substring(sub.IndexOf("lat\" : ") + "lat\" : ".Length).Remove(sub.Substring(sub.IndexOf("lat\" : ") + "lat\" : ".Length).IndexOf(",")).Trim();
                var lng = sub.Substring(sub.IndexOf("lng\" : ") + "lng\" : ".Length).Remove(sub.Substring(sub.IndexOf("lng\" : ") + "lng\" : ".Length).IndexOf("}")).Trim();
                App.lat = Double.Parse(lat);
                App.lng = Double.Parse(lng);
                if (CalcDistance(App.lat, App.lng, App.position.Latitude, App.position.Longitude) > 30)
                    return "range";
                return $"POINT({lng} {lat})";
            }
            catch (Exception)
            {
                return "error";
            }*/
            return address;
            
        }
    }

    public enum GeoCodeCalcMeasurement : int
    {
        Miles = 0,
        Kilometers = 1
    }
}