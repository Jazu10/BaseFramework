using Frontend.Core.HttpClients;
using Frontend.DTO.Request;
using Frontend.DTO.Response;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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

        public async Task<IActionResult> PersonalPosts()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _client.GetAllAsync<PostResponseDTO>($"{ApiConstants.UserPosts}?userId={id}&postUser={id}");
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
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
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
            return RedirectToAction(nameof(GetAllPosts));
        }



        [HttpGet]
        public async Task<IActionResult> ApproveRejectPost()
        {
            try
            {
                var response = await _client.GetAllAsync<PostResponseDTO>(ApiConstants.GetAllPostsAdmin);
                if (!response.IsError)
                {
                    return View(response.Response);
                }
                return RedirectToAction("GetAllPosts");
            }
            catch (Exception)
            {
                return RedirectToAction("GetAllPosts");
            }
            return RedirectToAction("GetAllPosts");
        }

        public async Task<IActionResult> ApprovePost(string id)
        {
            PostResponseDTO model = new PostResponseDTO();
            var response = await _client.PutAsync<SuccessResultDTO>($"{ApiConstants.ApprovePost}?postId={id}", model);
            return RedirectToAction("ApproveRejectPost");
        }

        public async Task<IActionResult> RejectPost(string id)
        {
            var response = await _client.DeleteAsync<SuccessResultDTO>($"{ApiConstants.RejectPost}?postId={id}");
            //if (user.isinrole("admin"))
            //{
            //    return redirecttoaction("approverejectpost");
            //}
            return RedirectToAction("PersonalPosts");
        }



        [HttpPost]
        public async Task<IActionResult> AddComment(string id, string comments)
        {
            try
            {
                CommentRequestDTO newComment = new CommentRequestDTO()
                {
                    PostId = id,
                    Comment = comments,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                var response = await _client.PostAsync<SuccessResultDTO>(ApiConstants.Comment, newComment);
                if (response.Response.Succeeded)
                {
                    _toastService.AddSuccessToastMessage(response.Response.Message);
                    return RedirectToAction("GetAllPosts", "Post");
                }
                return RedirectToAction("GetAllPosts", "Post");
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return RedirectToAction("GetAllPosts", "Post");
        }

        public async Task<IActionResult> GetAllComments(string id)
        {
            var response = await _client.GetAllAsync<CommentRequestDTO>($"{ApiConstants.GetAllComments}?postId={id}");

            return View(response.Response);
        }

        public async Task<IActionResult> AddDltLikes(string id)
        {
            LikeRequestDTO model = new LikeRequestDTO()
            {
                ContentId = id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            var response = await _client.PostAsync<SuccessResultDTO>($"{ApiConstants.AddDltLikes}?postId={id}&userId={User.FindFirstValue(ClaimTypes.NameIdentifier)}", model);
            if (response.Response.Succeeded)
            {
                return RedirectToAction("GetAllPosts", "Post");
            }
            return RedirectToAction("GetAllPosts", "Post");
        }

        public async Task<IActionResult> DltComment(string id)
        {
            var response = await _client.DeleteAsync<CommentRequestDTO>($"{ApiConstants.GetAllComments}?commentId={id}");

            return RedirectToAction(nameof(GetAllPosts));
        }
    }
}
