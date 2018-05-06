using Demo.BL.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Demo.BL
{
    public class Weather : IWeather
    {
        public async Task<string> GetWeather()
        {
            // Step 1: http://json2csharp.com/ - generate c# classes from json
            // Step 2: https://samples.openweathermap.org/data/2.5/group?id=524901,703448,2643743&units=metric&appid=b6907d289e10d714a6e88b30761fae22

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"http://samples.openweathermap.org/data/2.5/weather");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string myFavoriteCity = "Minsk";
                string json = await client.GetStringAsync($"?q={myFavoriteCity}&appid=b6907d289e10d714a6e88b30761fae22");

                RootObject rootObject = this.Deserialize(json); // works;

                return json; // todo: deserialize
            }
        }

        private RootObject Deserialize(string weatherJson)
        {
            try
            {
                // option 1
                dynamic results = JsonConvert.DeserializeObject<dynamic>(weatherJson);
                var id = results.Id;
                var name = results.Name;

                // option 2
                RootObject results2 = JsonConvert.DeserializeObject<RootObject>(weatherJson);
                return results2;
            }
            catch (Exception e)
            {
                var x = 1;
            }

            return null;
        }
    }
}
