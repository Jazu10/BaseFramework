using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AdvertisementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
