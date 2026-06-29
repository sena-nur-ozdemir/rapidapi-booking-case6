using Newtonsoft.Json;
using System;

namespace RapidApiBookingCase.Dtos.DashboardDtos
{
    public class ExchangeDto
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        public Rates rates { get; set; }
        public TimeUpdate time_update { get; set; }
    }
    public class Rates
    {
        public int TRY { get; set; }
        public float USD { get; set; }
        public float EUR { get; set; }
        public float JPY { get; set; }
    }
    public class TimeUpdate
    {
        public DateTime time_utc { get; set; }
        public string time_zone { get; set; }
    }
}