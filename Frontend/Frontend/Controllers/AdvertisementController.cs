using Frontend.Core.HttpClients;
using Frontend.DTO.Response;
using Frontend.DTO.Response.Advertisement;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using NToastNotify;
using System.Security.Claims;

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
        public IActionResult CreateAds()
        {
            AdvertisementResponseDTO newAd = new AdvertisementResponseDTO()
            {
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                IsActive = true
            };
            return View(newAd);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAds(AdvertisementResponseDTO model)
        {
            try
            {
                model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
        public async Task<IActionResult> DisplayPersonalAds()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _client.GetAsync<AdvertisementResponseDTO>($"{ApiConstants.GetAllAdsById}?userId={id}");
            if (!response.IsError)
            {
                return View(response.Response);
            }
            return RedirectToAction("GetAllAds");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAds(string advertisementId)
        {
            var response = await _client.DeleteAsync<SuccessResultDTO>($"{ApiConstants.DltAdsById}?advertisementId={advertisementId}");
            if (!response.IsError)
            {
                _toastService.AddSuccessToastMessage(response.Response.Message);
            }
            return RedirectToAction("GetAllAds");
        }


        public async Task<IActionResult> EditAds(string advertisementId)
        {
            var response = await _client.GetAsyncSingle<AdvertisementResponseDTO>($"{ApiConstants.EditAdsById}?advertisementId={advertisementId}");
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(response.Response!= null)
            {
                return View(response.Response);
            }
            return RedirectToAction("GetAllAds");
        }

        [HttpPost]
        public async Task<IActionResult> EditAds(AdvertisementResponseDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _client.PutAsync<SuccessResultDTO>(ApiConstants.EditAdsById, model);
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
    }
}
