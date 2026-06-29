using Microsoft.AspNetCore.Mvc;

namespace RapidApiBookingCase.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
