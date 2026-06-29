using Microsoft.AspNetCore.Mvc;

namespace RapidApiBookingCase.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
