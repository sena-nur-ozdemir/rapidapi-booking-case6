namespace RapidApiBookingCase.Models
{
    public class HotelReviewViewModel
    {
        public List<HotelReviewResult> result { get; set; }
    }

    public class HotelReviewResult
    {
        public string title { get; set; }
        public double? average_score { get; set; }
        public string pros { get; set; }
        public string cons { get; set; }
        public string date { get; set; }
        public HotelReviewAuthor author { get; set; }
    }

    public class HotelReviewAuthor
    {
        public string name { get; set; }
        public string countrycode { get; set; }
    }
}