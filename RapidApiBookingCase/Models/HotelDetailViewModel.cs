using RapidApiBookingCase.Dtos;

namespace RapidApiBookingCase.Models
{
    public class HotelDetailViewModel
    {
        public HotelDetailDto HotelDetail { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }

        // Olanaklar ve Odalar
        public List<string> Facilities { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
    }

    public class ReviewViewModel
    {
        public string AuthorName { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public double Score { get; set; }
        public string Date { get; set; }
    }

    // Odalar için alt model
    public class RoomViewModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string BedType { get; set; }
        public string Price { get; set; }
        public string Feature { get; set; }
    }
}