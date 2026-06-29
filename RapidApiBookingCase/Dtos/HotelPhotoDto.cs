using Newtonsoft.Json;

namespace RapidApiBookingCase.Dtos
{
    // API direkt liste döndüğü için bunu sınıf olarak değil, List<HotelPhotoItem> olarak kullanacağız
    public class HotelPhotoItem
    {
        [JsonProperty("url_max")]
        public string UrlMax { get; set; }

        [JsonProperty("url_square60")]
        public string UrlSquare60 { get; set; }
    }
}