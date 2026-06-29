using Newtonsoft.Json;
using System.Collections.Generic;

namespace RapidApiBookingCase.Dtos.DashboardDtos
{
    public class CryptoDto
    {
        public CryptoData data { get; set; }
    }
    public class CryptoData
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        public List<Token> tokens { get; set; }
    }
    public class Token
    {
        public string symbol { get; set; }
        public float price { get; set; }
    }
}