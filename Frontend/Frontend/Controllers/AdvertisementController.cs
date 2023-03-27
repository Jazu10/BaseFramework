using Frontend.Core.HttpClients;
using Frontend.DTO.Request.Advertisement;
using Frontend.DTO.Response;
using Frontend.DTO.Response.Advertisement;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Frontend.Controllers
{
    public class AdvertisementController : BaseController
    {
        private readonly IGenericHttpClient _client;
        private readonly IToastNotification _toastService;
        private readonly IWebHostEnvironment _env;

        public AdvertisementController(IGenericHttpClient client, IToastNotification toast, IWebHostEnvironment env) : base(toast)
        {
            _client = client;
            _toastService = toast;
            _env = env;
        }
        public async Task<IActionResult> GetAllAds()
        {
            var response = await _client.GetAllAsync<AdvertisementResponseDTO>(ApiConstants.GetAllAds);
            if (!response.IsError)
            {
                return View(response.Response);
            }
            return View("Login", "Account");
        }

        [HttpGet]
        public IActionResult CreateAds(string id)
        {
            AdvertisementResponseDTO newAd = new AdvertisementResponseDTO()
            {
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
                UserId = id
            };
            return View(newAd);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAds(AdvertisementResponseDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ImgFiles != null)
                    {
                        string folderPath = Path.Combine(_env.WebRootPath, "Images");
                        foreach (var item in model.ImgFiles)
                        {
                            string filePath = Path.Combine(folderPath, item.FileName);
                            item.CopyTo(new FileStream(filePath, FileMode.Create));
                            model.Images.Add(new ImageModelDTO()
                            {
                                Image = item.FileName
                            });
                        }
                    }
                    var result = await _client.PostAsync<SuccessResultDTO>(ApiConstants.GetAllAds, model);
                    if (result.Response.Succeeded)
                    {
                        _toastService.AddSuccessToastMessage(result.Response.Message);
                        return RedirectToAction("GetAllAds");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return View(model);
        }
        public async Task<IActionResult> DisplayPersonalAds(string id)
        {
            var response = await _client.GetAsync<AdvertisementResponseDTO>($"{ApiConstants.GetAllAdsById}?userId={id}");
            if (!response.IsError)
            {
                return View(response.Response);
            }
            return RedirectToAction("GetAllAds");
        }

        public async Task<IActionResult> DeleteAds(string id, string userID)
        {
            var response = await _client.DeleteAsync<SuccessResultDTO>($"{ApiConstants.DltAdsById}?advertisementId={id}");
            if (!response.IsError)
            {
                _toastService.AddSuccessToastMessage(response.Response.Message);
            }
            return RedirectToAction("GetAllAds");
        }
    }
}
