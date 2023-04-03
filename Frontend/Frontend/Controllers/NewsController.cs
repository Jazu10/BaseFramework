using Frontend.Core.HttpClients;
using Frontend.DTO.Response;
using Frontend.DTO.Response.Advertisement;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.IO;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IGenericHttpClient _client;
        private readonly IWebHostEnvironment _env;
        private readonly IToastNotification _toastService;




        public NewsController(IGenericHttpClient client,
        IWebHostEnvironment env,
        IToastNotification toastService) : base(toastService)
        {
            _client = client;
            _env = env;
            _toastService = toastService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var result = await _client.GetAllAsync<NewsResponseDTO>(ApiConstants.GetAllNews);
                return View(result.Response);
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AdminNewsView()
        {
            try
            {
                var result = await _client.GetAllAsync<NewsResponseDTO>(ApiConstants.GetAllNews);
                return View(result.Response);
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SingleUserNews(string id)
        {
            var response = await _client.GetAsync<NewsResponseDTO>($"{ApiConstants.GetNewsByUser}?userId={id}");
            if (!response.IsError)
            {
                return View(response.Response);
            }
            return RedirectToAction("GetAllNews");
        }

        [HttpGet]
        public async Task<IActionResult> SingleNews(string id)
        {
            var response = await _client.GetAsyncSingle<NewsResponseDTO>($"{ApiConstants.GetSingleNews}?newsId={id}");
            if (!response.IsError)

            {
                return View(response.Response);
            }
            return RedirectToAction("GetAllNews");
        }

        [HttpGet]
        public async Task<IActionResult> AddNews()
        {
            NewsResponseDTO newNews = new NewsResponseDTO()
            {
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                IsActive = true,
            };
            return View(newNews);
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(NewsResponseDTO model)
        {
            try
            {
                model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (ModelState.IsValid)
                {
                    if (model.ImgFiles != null)
                    {
                        string folderPath = Path.Combine(_env.WebRootPath, "Images");
                        string filePath = Path.Combine(folderPath, model.ImgFiles.FileName);
                        model.ImgFiles.CopyTo(new FileStream(filePath, FileMode.Create));
                        model.Image = model.ImgFiles.FileName;
                    }

                    var result = await _client.PostAsync<SuccessResultDTO>(ApiConstants.AddNews, model);

                    if (result.Response.Succeeded)
                    {
                        _toastService.AddSuccessToastMessage(result.Response.Message);
                        return RedirectToAction("GetAllNews");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return View("AdminNewsView");
        }


        [HttpGet]

        public async Task<IActionResult> EditNews(string NewsId)
        {
            var response = await _client.GetAsyncSingle<NewsResponseDTO>($"{ApiConstants.EditNews}?NewsId={NewsId}");
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (response.Response != null)
            {
                return View(response.Response);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditNews(NewsResponseDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ImgFiles != null)
                    {
                        string folderPath = Path.Combine(_env.WebRootPath, "Images");
                        string filePath = Path.Combine(folderPath, model.ImgFiles.FileName);
                        model.ImgFiles.CopyTo(new FileStream(filePath, FileMode.Create));
                        model.Image = model.ImgFiles.FileName;
                    }

                    var result = await _client.PutAsync<SuccessResultDTO>(ApiConstants.EditNews, model);

                    if (result.Response.Succeeded)
                    {
                        _toastService.AddSuccessToastMessage(result.Response.Message);
                        return RedirectToAction("AdminNewsView");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return View("AdminNewsView");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNews(string NewsId)
        {
            var response = await _client.DeleteAsync<SuccessResultDTO>($"{ApiConstants.DeleteNews}?NewsId={NewsId}");
            if (!response.IsError)
            {
                _toastService.AddSuccessToastMessage(response.Response.Message);
            }
            return RedirectToAction("AdminNewsView");
        }
    }
}
