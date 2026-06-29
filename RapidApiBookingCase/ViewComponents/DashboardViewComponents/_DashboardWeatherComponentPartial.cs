using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos.DashboardDtos;

namespace RapidApiBookingCase.ViewComponents.DashboardViewComponents
{
    public class _DashboardWeatherComponentPartial : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public _DashboardWeatherComponentPartial(IConfiguration configuration) { _configuration = configuration; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://weatherapi-com.p.rapidapi.com/current.json?q=London"),
                    Headers = { { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] }, { "x-rapidapi-host", "weatherapi-com.p.rapidapi.com" } }
                };
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<WeatherDto>(body));
            }
            catch { return View(new WeatherDto { location = new WeatherLocationData { name = "London", country = "UK" }, current = new WeatherCurrentData { temp_c = 15.5, feelslike_c = 14.0, humidity = 60, wind_kph = 18.4, condition = new WeatherConditionData { text = "Cloudy", icon = "//cdn.weatherapi.com/weather/64x64/day/119.png" } } }); }
        }
    }
}