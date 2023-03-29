using Frontend.Core.HttpClients;
using Frontend.DTO.Response;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Reflection;
using System.Security.Claims;

namespace Frontend.Controllers
{
    public class PostController : BaseController
    {
        private readonly IToastNotification _toastService;
        private readonly IGenericHttpClient _client;
        private readonly IWebHostEnvironment _env;

        public PostController(IToastNotification toast, IGenericHttpClient client, IWebHostEnvironment env) : base(toast)
        {
            _toastService = toast;
            _client = client;
            _env = env;
        }
        public async Task<IActionResult> GetAllPosts()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _client.GetAllAsync<PostResponseDTO>($"{ApiConstants.GetAllPosts}?userId={id}");
            if (response.Response != null)
            {
                return View(response.Response);
            }
            return RedirectToAction("GetAllNews", "News");
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            PostResponseDTO newPost = new PostResponseDTO()
            {
                LikeCount = 0,
                CreatedAt = DateTime.Now
            };
            return View(newPost);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostResponseDTO model)
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
                    var result = await _client.PostAsync<SuccessResultDTO>(ApiConstants.GetAllPosts, model);
                    if (result.Response.Succeeded)
                    {
                        _toastService.AddSuccessToastMessage(result.Response.Message);
                        return RedirectToAction("GetAllPosts");
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
