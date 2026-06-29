using Newtonsoft.Json;
using System.Collections.Generic;

namespace RapidApiBookingCase.Dtos
{
    public class HotelDetailDto
    {
        [JsonProperty("hotel_id")]
        public int HotelId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("review_score")]
        public string ReviewScore { get; set; }

        [JsonProperty("review_score_word")]
        public string ReviewScoreWord { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("checkin_from")]
        public string CheckinFrom { get; set; }

        [JsonProperty("checkin_to")]
        public string CheckinTo { get; set; }

        [JsonProperty("checkout_from")]
        public string CheckoutFrom { get; set; }

        [JsonProperty("checkout_to")]
        public string CheckoutTo { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        // HARİTAYI KURTARACAK KISIM (Gizli klasörü yakalıyoruz)
        [JsonProperty("location")]
        public HotelLocationDto LocationObj { get; set; }

        [JsonProperty("photos")]
        public List<HotelPhotoDto> Photos { get; set; }
    }

    // GİZLİ KLASÖRÜN İÇİNDEKİ KOORDİNATLAR
    public class HotelLocationDto
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
    }

    public class HotelPhotoDto
    {
        [JsonProperty("url_max")]
        public string UrlMax { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}