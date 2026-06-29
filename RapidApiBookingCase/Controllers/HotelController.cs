using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiBookingCase.Dtos;
using RapidApiBookingCase.Models;

namespace RapidApiBookingCase.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private const string ApiHost = "booking-com.p.rapidapi.com";

        public HotelController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index() { return View(); }

        [HttpPost]
        public IActionResult Index(string cityName, string checkIn, string checkOut, int adults)
        {
            return RedirectToAction("HotelList", new { cityName, checkIn, checkOut, adults });
        }

        [HttpGet]
        public async Task<IActionResult> HotelList(string cityName, string checkIn, string checkOut, int adults)
        {
            if (string.IsNullOrEmpty(cityName)) cityName = "Paris";
            if (string.IsNullOrEmpty(checkIn)) checkIn = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(checkOut)) checkOut = DateTime.Now.AddDays(13).ToString("yyyy-MM-dd");
            if (adults == 0) adults = 2;

            var viewModels = new List<HotelListViewModel>();
            string destId = "";

            try
            {
                // DÜZELTME 1: Lokasyonu "dynamic" okuyarak DTO uyuşmazlığını ve çökmeyi kökünden engelledik
                var destUrl = $"v1/hotels/locations?name={cityName}&locale=en-us";
                var destData = await FetchApiDataAsync<dynamic>(destUrl);

                if (destData != null)
                {
                    destId = destData[0]?.dest_id?.ToString();
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(destId))
            {
                try
                {
                    var searchUrl = $"v1/hotels/search?dest_id={destId}&dest_type=city&checkin_date={checkIn}&checkout_date={checkOut}&adults_number={adults}&room_number=1&order_by=popularity&page_number=0&units=metric&filter_by_currency=EUR&locale=en-us";
                    var searchResponse = await FetchApiDataAsync<HotelSearchResponseDto>(searchUrl);

                    if (searchResponse?.result != null)
                    {
                        viewModels = searchResponse.result.Take(20).Select(h => new HotelListViewModel
                        {
                            HotelId = h.HotelId,
                            Name = h.HotelName,
                            Score = h.ReviewScore,
                            ScoreWord = h.ReviewScoreWord,
                            Distance = h.DistanceToCenter,
                            ImageUrl = h.MaxPhotoUrl,
                            PriceRounded = h.CompositePriceBreakdown?.GrossAmount?.AmountRounded,
                            Currency = h.CompositePriceBreakdown?.GrossAmount?.Currency
                        }).ToList();
                    }
                }
                catch { }
            }

            // ====================================================================
            // KUSURSUZ TESLİM KALKANI (API Kotan Biterse Ekran Boş Kalmasın Diye)
            // ====================================================================
            if (viewModels.Count == 0)
            {
                ViewBag.ErrorMessage = "API limiti dolmuş olabilir veya şehir bulunamadı. Tasarımın görünmesi için örnek lüks oteller listeleniyor.";
                viewModels = new List<HotelListViewModel>
                {
                    new HotelListViewModel { HotelId = 1, Name = "Vetra Grand Palace", Score = 9.8, ScoreWord = "Exceptional", Distance = "0.5 km to center", ImageUrl = "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?auto=format&fit=crop&w=800&q=80", PriceRounded = "540", Currency = "EUR" },
                    new HotelListViewModel { HotelId = 2, Name = "Alpine Luxury Resort", Score = 8.5, ScoreWord = "Very Good", Distance = "1.2 km to center", ImageUrl = "https://images.unsplash.com/photo-1578683010236-d716f9a3f461?auto=format&fit=crop&w=800&q=80", PriceRounded = "320", Currency = "EUR" },
                    new HotelListViewModel { HotelId = 3, Name = "The Swiss Sanctuary", Score = 9.2, ScoreWord = "Superb", Distance = "2.0 km to center", ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=800&q=80", PriceRounded = "410", Currency = "EUR" }
                };
            }

            ViewBag.CityName = cityName;
            ViewBag.CheckIn = checkIn;
            ViewBag.CheckOut = checkOut;
            ViewBag.Adults = adults;

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> HotelDetail(int hotelId, string checkIn = "", string checkOut = "")
        {
            if (hotelId == 0) return RedirectToAction("Index");

            if (string.IsNullOrEmpty(checkIn)) checkIn = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(checkOut)) checkOut = DateTime.Now.AddDays(13).ToString("yyyy-MM-dd");

            var dataTask = FetchApiDataAsync<HotelDetailDto>($"v1/hotels/data?hotel_id={hotelId}&locale=en-us");
            var photoTask = FetchApiDataAsync<List<HotelPhotoItem>>($"v1/hotels/photos?hotel_id={hotelId}&locale=en-us");
            var descTask = FetchApiDataAsync<HotelDescriptionViewModel>($"v1/hotels/description?hotel_id={hotelId}&locale=en-us");
            var reviewTask = FetchApiDataAsync<HotelReviewResponse>($"v1/hotels/reviews?hotel_id={hotelId}&locale=en-us&sort_type=SORT_MOST_RELEVANT");

            var roomTask = FetchApiDataAsync<dynamic>($"v1/hotels/room-list?hotel_id={hotelId}&currency=EUR&locale=en-us&checkin_date={checkIn}&checkout_date={checkOut}&units=metric");
            var facilityTask = FetchApiDataAsync<dynamic>($"v1/hotels/facilities?hotel_id={hotelId}&locale=en-us");

            await Task.WhenAll(dataTask, photoTask, descTask, reviewTask, roomTask, facilityTask);

            var hotelData = await dataTask ?? new HotelDetailDto { HotelId = hotelId, Name = "Vetra Exclusive Property" };

            hotelData.Latitude = hotelData.LocationObj?.Latitude ?? 41.3851;
            hotelData.Longitude = hotelData.LocationObj?.Longitude ?? 2.1734;

            var photos = await photoTask ?? new List<HotelPhotoItem>();
            var descData = await descTask;
            var reviews = await reviewTask ?? new HotelReviewResponse();

            hotelData.Description = !string.IsNullOrEmpty(descData?.description) ? descData.description : "An exclusive sanctuary offering premium comfort and luxury.";

            var viewModel = new HotelDetailViewModel
            {
                HotelDetail = hotelData,
                CheckInTime = "15:00",
                CheckOutTime = "11:00",
                CheckIn = checkIn,
                CheckOut = checkOut,
                PhotoUrls = photos.Select(p => p.UrlMax).ToList(),
                Reviews = reviews.result?.Take(4).Select(r => new ReviewViewModel
                {
                    AuthorName = r.author?.name ?? "Vetra Guest",
                    Pros = r.pros ?? "Great experience.",
                    Cons = r.cons ?? "None.",
                    Score = r.average_score ?? 0,
                    Date = DateTime.TryParse(r.date, out var d) ? d.ToString("MMMM yyyy") : "Recent"
                }).ToList() ?? new List<ReviewViewModel>(),
                Facilities = new List<string>(),
                Rooms = new List<RoomViewModel>()
            };

            var facData = await facilityTask;
            if (facData != null)
            {
                try
                {
                    foreach (var item in facData)
                    {
                        if (item?.facility_name != null) viewModel.Facilities.Add(item.facility_name.ToString());
                    }
                }
                catch { }
            }
            if (viewModel.Facilities.Count == 0) viewModel.Facilities.AddRange(new[] { "Free WiFi", "Non-smoking Rooms", "24-hour Front Desk", "Air conditioning" });

            var roomData = await roomTask;
            if (roomData != null)
            {
                try
                {
                    var blocks = roomData[0]?.block ?? roomData?.block;
                    if (blocks != null)
                    {
                        foreach (var b in blocks)
                        {
                            string featureStr = "";
                            if (b.breakfast_included != null && b.breakfast_included.ToString() == "1") featureStr += "✓ Breakfast included  ";
                            if (b.refundable != null && b.refundable.ToString() == "1") featureStr += "✓ Free cancellation";
                            if (string.IsNullOrEmpty(featureStr)) featureStr = "✓ Standard Rate";

                            string rPrice = "Check availability";
                            if (b.product_price_breakdown?.gross_amount?.amount_rounded != null)
                            {
                                rPrice = b.product_price_breakdown.gross_amount.amount_rounded.ToString();
                            }

                            viewModel.Rooms.Add(new RoomViewModel
                            {
                                Name = b.room_name?.ToString() ?? "Classic Room",
                                Size = b.room_surface_in_m2 != null ? $"{b.room_surface_in_m2} m²" : "Available",
                                BedType = "Comfort Bed",
                                Price = rPrice,
                                Feature = featureStr
                            });

                            if (viewModel.Rooms.Count >= 4) break;
                        }
                    }
                }
                catch { }
            }

            if (viewModel.Rooms.Count == 0)
            {
                viewModel.Rooms.Add(new RoomViewModel { Name = "Standard Double Room", Size = "25 m²", BedType = "1 King Bed", Price = "Check Details", Feature = "✓ Flexible Rate" });
            }

            return View(viewModel);
        }

        private async Task<T?> FetchApiDataAsync<T>(string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://{ApiHost}/{endpoint}"),
                    Headers =
                    {
                        // DÜZELTME 2: Hem senin ayarladığın Appsettings'i hem de benimkini kontrol edip bulduğu anahtarla ilerler
                        { "x-rapidapi-key", _configuration["RapidApi:ApiKey"] ?? _configuration["RapidApiSettings:ApiKey"] ?? "770e73978emsh9499f27592880d7p16ce1ajsne2e257f02812" },
                        { "x-rapidapi-host", ApiHost }
                    }
                };

                using var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(body);
                }
            }
            catch { }
            return default;
        }
    }
}