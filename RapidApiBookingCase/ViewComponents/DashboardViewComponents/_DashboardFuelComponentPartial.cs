using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos.DashboardDtos;

namespace RapidApiBookingCase.ViewComponents.DashboardViewComponents
{
    public class _DashboardFuelComponentPartial : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public _DashboardFuelComponentPartial(IConfiguration configuration) { _configuration = configuration; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://fuel-gas-price-api.p.rapidapi.com/state/NY"),
                    Headers = { { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] }, { "x-rapidapi-host", "fuel-gas-price-api.p.rapidapi.com" } }
                };
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<FuelDto>(body));
            }
            catch { return View(new FuelDto { data = new FuelData { regular = new FuelType { current = 3.45f }, diesel = new FuelType { current = 4.12f }, premium = new FuelType { current = 3.90f } } }); }
        }
    }
}