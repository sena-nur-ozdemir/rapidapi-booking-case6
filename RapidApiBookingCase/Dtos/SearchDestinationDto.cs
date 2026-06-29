using Newtonsoft.Json;

namespace RapidApiBookingCase.Dtos
{
    public class SearchDestinationResponseDto
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<SearchDestinationDto> data { get; set; }
    }

    public class SearchDestinationDto
    {
        [JsonProperty("dest_id")]
        public string DestId { get; set; }

        [JsonProperty("dest_type")]
        public string DestType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}