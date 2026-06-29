using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos.DashboardDtos;

namespace RapidApiBookingCase.ViewComponents.DashboardViewComponents
{
    public class _DashboardNewsComponentPartial : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public _DashboardNewsComponentPartial(IConfiguration configuration) { _configuration = configuration; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://real-time-news-data.p.rapidapi.com/top-headlines?limit=10&country=US&lang=en"),
                    Headers = { { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] }, { "x-rapidapi-host", "real-time-news-data.p.rapidapi.com" } }
                };
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NewsDto>(body);
                return View(data?.data.Take(4).ToList());
            }
            catch { return View(new List<NewsData>()); }
        }
    }
}