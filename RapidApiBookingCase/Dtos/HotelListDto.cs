using Newtonsoft.Json;


namespace RapidApiBookingCase.Dtos
{
    public class HotelSearchResponseDto
    {
        public List<HotelListDto> result { get; set; }
    }

    public class HotelListDto
    {
        [JsonProperty("hotel_id")]
        public int HotelId { get; set; }

        [JsonProperty("hotel_name")]
        public string HotelName { get; set; }

        [JsonProperty("review_score")]
        public double? ReviewScore { get; set; }

        [JsonProperty("review_score_word")]
        public string ReviewScoreWord { get; set; }

        [JsonProperty("distance_to_cc_formatted")]
        public string DistanceToCenter { get; set; }

        [JsonProperty("max_photo_url")]
        public string MaxPhotoUrl { get; set; }

        [JsonProperty("composite_price_breakdown")]
        public CompositePrice CompositePriceBreakdown { get; set; }
    }

    public class CompositePrice
    {
        [JsonProperty("gross_amount")]
        public GrossAmount GrossAmount { get; set; }
    }

    public class GrossAmount
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("amount_rounded")]
        public string AmountRounded { get; set; }
    }
}