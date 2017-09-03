using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Vigeo
{
    public static class JsonDownloader
    {
        public static async Task<T> DownloadSerializedJSONDataAsync<T>(string url) where T : new()
        {
            using (var httpClient = new HttpClient(new NativeMessageHandler()))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonData = string.Empty;
                try
                {
                    jsonData = await httpClient.GetStringAsync(url);
                }
                catch (Exception)
                {
                    return default(T);
                }
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData): default(T);
            }
        }
    }
}