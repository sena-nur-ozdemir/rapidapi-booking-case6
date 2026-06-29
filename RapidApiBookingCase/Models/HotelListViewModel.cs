namespace RapidApiBookingCase.Models
{
    // Arama sonucunda listelenecek 20 otelin her bir kartı için kullanılacak model
    public class HotelListViewModel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public double? Score { get; set; }
        public string ScoreWord { get; set; }
        public string Distance { get; set; }
        public string ImageUrl { get; set; }
        public string PriceRounded { get; set; }
        public string Currency { get; set; }
    }
}