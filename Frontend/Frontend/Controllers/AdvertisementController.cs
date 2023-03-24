using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AdvertisementController : Controller
    {
        public IActionResult GetAllAds()
        {
            return View();
        }
    }
}
