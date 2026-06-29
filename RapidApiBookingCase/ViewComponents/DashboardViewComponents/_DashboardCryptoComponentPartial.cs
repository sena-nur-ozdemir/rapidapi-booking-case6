using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos.DashboardDtos;

namespace RapidApiBookingCase.ViewComponents.DashboardViewComponents
{
    public class _DashboardCryptoComponentPartial : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public _DashboardCryptoComponentPartial(IConfiguration configuration) { _configuration = configuration; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://crypto-market-prices.p.rapidapi.com/tokens?base=USD"),
                    Headers = { { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] }, { "x-rapidapi-host", "crypto-market-prices.p.rapidapi.com" } }
                };
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var cryptoData = JsonConvert.DeserializeObject<CryptoDto>(body);

                // Sadece bu saygın ve popüler premium coinleri API'den süzüyoruz
                var allowed = new[] { "BTC", "ETH", "SOL", "BNB", "ADA" };
                var filteredTokens = cryptoData?.data?.tokens
                    .Where(x => allowed.Contains(x.symbol))
                    .Take(4)
                    .ToList();

                return View(filteredTokens);
            }
            catch
            {
                return View(new List<Token> {
                    new Token { symbol = "BTC", price = 64500.5f },
                    new Token { symbol = "ETH", price = 3450.2f },
                    new Token { symbol = "SOL", price = 145.8f },
                    new Token { symbol = "BNB", price = 585.3f }
                });
            }
        }
    }
}