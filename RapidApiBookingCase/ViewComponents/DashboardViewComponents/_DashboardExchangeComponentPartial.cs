using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos.DashboardDtos;

namespace RapidApiBookingCase.ViewComponents.DashboardViewComponents
{
    public class _DashboardExchangeComponentPartial : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public _DashboardExchangeComponentPartial(IConfiguration configuration) { _configuration = configuration; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://exchange-rates7.p.rapidapi.com/latest?base=TRY"),
                    Headers = { { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] }, { "x-rapidapi-host", "exchange-rates7.p.rapidapi.com" } }
                };
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<ExchangeDto>(body));
            }
            catch { return View(new ExchangeDto { Base = "TRY", rates = new Rates { EUR = 35.12f, USD = 32.45f, JPY = 0.22f } }); }
        }
    }
}