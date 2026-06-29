using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos.DashboardDtos;

namespace RapidApiBookingCase.ViewComponents.DashboardViewComponents
{
    public class _DashboardFootballComponentPartial : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public _DashboardFootballComponentPartial(IConfiguration configuration) { _configuration = configuration; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // YEDEK VERİLER: Eğer dünyada o an canlı maç yoksa veya kota dolarsa, kart boş kalmasın diye 3 adet jilet gibi veri.
            var fallbackMatches = new List<Match> {
                new Match { home = new Team { name = "Real Madrid", score = 2 }, away = new Team { name = "Barcelona", score = 1 } },
                new Match { home = new Team { name = "Man City", score = 3 }, away = new Team { name = "Arsenal", score = 0 } },
            };;

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://free-api-live-football-data.p.rapidapi.com/football-current-live"),
                    Headers = { { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] }, { "x-rapidapi-host", "free-api-live-football-data.p.rapidapi.com" } }
                };
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<FootballDto>(body);

                var matches = data?.response?.live?.Take(2).ToList();

                if (matches == null || !matches.Any())
                {
                    return View(fallbackMatches);
                }

                return View(matches);
            }
            catch
            {
                // Eğer API kotası dolarsa veya hata verirse yine yedek verileri göster.
                return View(fallbackMatches);
            }
        }
    }
}