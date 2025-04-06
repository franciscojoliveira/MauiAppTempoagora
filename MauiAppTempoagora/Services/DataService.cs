using MauiAppTempoagora.Models;
using Newtonsoft.Json.Linq;


namespace MauiAppTempoagora.Services

{
    public class DataService

    {
        public static async Task<Tempo?> GetPrevisao(string cidade)

        {
            Tempo? t = null;

            string chave = "6cfcebf9fa9fdd860dba7ce24d165039";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);
                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString("HH:mm:ss"),
                        sunset = sunset.ToString("HH:mm:ss"),

                    }; // Fecha obj do Tempo.
                } // Feha if se o status do servidor foi de sucesso
            } // Fecha laça using


            return t;

        }

        internal static async Task<Tempo?> GetPrevisao(object txt_cidade)
        {
            throw new NotImplementedException();
        }
    }
}
