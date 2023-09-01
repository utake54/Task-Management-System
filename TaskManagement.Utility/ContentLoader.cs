using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility
{
    public static class ContentLoader
    {
        public static Dictionary<string, string> en_US;
        public static string ReturnMessage(string key, string language = "")
        {
            var messageContent = "";
            string messageData = null;
            try
            {
                messageContent = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\en_US.json");
                messageData = File.ReadAllText(messageContent);
                en_US = JsonConvert.DeserializeObject<Dictionary<string, string>>(messageData);

                language = String.IsNullOrEmpty(language) ? "en-US" : language;
                return language switch
                {
                    "en-US" => en_US[key],
                    _ => en_US[key],
                };
            }
            catch (Exception)
            {
                return key;
            }

        }
    }
}
