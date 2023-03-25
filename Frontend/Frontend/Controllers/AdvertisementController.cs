using Frontend.Core.HttpClients;
using Frontend.DTO.Response.Advertisement;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Frontend.Controllers
{
    public class AdvertisementController : BaseController
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _toastService;

        public AdvertisementController(IGenericHttpClient client, IToastNotification toast) : base(toast) 
        {
            _client = client;
            _toastService = toast;
        }
        public async Task<IActionResult> GetAllAds()
        {
            var response = await _client.GetAllAsync<AdvertisementResponseDTO>(ApiConstants.GetAllAds);
            if(!response.IsError)
            {
                return View(response.Response);
            }
            return View("Login", "Account");
        }
    }
}
