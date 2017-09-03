using ModernHttpClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Vigeo.Models;

namespace Vigeo.ViewModels
{
    public static class MessagesViewModel
    {
        public static List<MessageModel> GetMessages(int e_id)
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var uri = $"https://api.vigeo.io/event/{e_id}/messages";
                var response = client.GetAsync(uri).Result;
                var messages = response.Content.ReadAsStringAsync().Result;
                var message_list = JsonConvert.DeserializeObject<List<MessageModel>>(messages);
                return message_list;
            }
        }
        
    }
}
