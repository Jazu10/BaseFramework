using Backend.Core.Common.ErrorHandlers;
using Backend.Core.Data.Entities;
using Backend.Core.RepositoryInterface;
using Backend.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [Route("NewsList")]
        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var response = new Results<List<NewsModel>>();

            try
            {
                response.Response = await _repository.GetAllNews();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("UsersNews")]
        [HttpGet]
        public async Task<IActionResult> GetNewsByUserId(string userId)
        {
            var response = new Results<List<NewsModel>>();

            try
            {
                response.Response = await _repository.GetUsersNews(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SingleNews")]
        [HttpGet]
        public async Task<IActionResult> GetSingleNews(string newsId)
        {
            var response = new Results<NewsModel>();

            try
            {
                response.Response = await _repository.GetSingleNews(newsId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("NewsList")]
        [HttpPost]
        public async Task<IActionResult> CreateNews(NewsModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                model.IsActive = true;

                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.CreateNews(model),
                    Message = "News Created"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }


        [Route("SingleNews")]
        [HttpPut]
        public async Task<IActionResult> EditNews(NewsModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.UpdateNews(model),
                    Message = "News Updated"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SingleNews")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNews(string newsId)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.DeleteNews(newsId),
                    Message = "News Deleted"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }



        [Route("AdvertisementList")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdvertisements(string? search)
        {
            var response = new Results<List<AdvertisementModel>>();

            try
            {
                response.Response = await _repository.GetAllAdvertisements(search);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("UsersAdvertisement")]
        [HttpGet]
        public async Task<IActionResult> GetAdvertisementsByUserId(string userId)
        {
            var response = new Results<List<AdvertisementModel>>();

            try
            {
                response.Response = await _repository.GetUsersAdvertisements(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SingleAdvertisement")]
        [HttpGet]
        public async Task<IActionResult> GetSingleAdvertisement(string advertisementId)
        {
            var response = new Results<AdvertisementModel>();

            try
            {
                response.Response = await _repository.GetSingleAdvertisement(advertisementId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("AdvertisementList")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisements(AdvertisementModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                model.IsActive = true;

                if (ModelState.IsValid)
                {
                    response.Response = new SuccessResult()
                    {
                        Succeeded = await _repository.CreateAdvertisements(model),
                        Message = "Advertisement Created"
                    };
                    return Ok(response);
                }

                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Please Enter All The Fields", 400, null);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SingleAdvertisement")]
        [HttpPut]
        public async Task<IActionResult> EditAdvertisements(AdvertisementModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.UpdateAdvertisements(model),
                    Message = "Advertisement Updated"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SingleAdvertisement")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAdvertisement(string advertisementId)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.DeleteAdvertisements(advertisementId),
                    Message = "Advertisement Deleted"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("AdminPosts")]
        [HttpGet]
        public async Task<IActionResult> GetAdminPosts()
        {
            var response = new Results<List<PostModel>>();

            try
            {
                response.Response = await _repository.GetAdminPosts();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("PostList")]
        [HttpGet]
        public async Task<IActionResult> GetAllPosts(string? search, string userId)
        {
            var response = new Results<List<PostModel>>();

            try
            {
                response.Response = await _repository.GetAllPosts(search, userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("UsersPost")]
        [HttpGet]
        public async Task<IActionResult> GetPostsByUserId(string postUser, string userId)
        {
            var response = new Results<List<PostModel>>();

            try
            {
                response.Response = await _repository.GetUsersPosts(postUser, userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SinglePost")]
        [HttpGet]
        public async Task<IActionResult> GetSinglePost(string postId, string userId)
        {
            var response = new Results<PostModel>();

            try
            {
                response.Response = await _repository.GetSinglePost(postId, userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("PostList")]
        [HttpPost]
        public async Task<IActionResult> CreatePosts(PostModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                if (ModelState.IsValid)
                {
                    response.Response = new SuccessResult()
                    {
                        Succeeded = await _repository.CreatePost(model),
                        Message = "Post Created"
                    };
                    return Ok(response);
                }
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Please Enter All The Fields", 400, null);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SinglePost")]
        [HttpPut]
        public async Task<IActionResult> EditPosts(PostModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                model.IsActive = true;

                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.UpdatePost(model),
                    Message = "Post Updated"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("ApprovePost")]
        [HttpPut]
        public async Task<IActionResult> ApprovePosts(string postId)
        {
            var response = new Results<SuccessResult>();

            try
            {

                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.ApprovePost(postId),
                    Message = "Post Approved"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("SinglePost")]
        [HttpDelete]
        public async Task<IActionResult> DeletePosts(string postId)
        {
            var response = new Results<SuccessResult>();
            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.DeletePost(postId),
                    Message = "Post Deleted"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("Comment")]
        [HttpGet]
        public async Task<IActionResult> GetAllComments(string postId)
        {
            var response = new Results<List<CommentModel>>();

            try
            {
                response.Response = await _repository.GetAllComments(postId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("Comment")]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.CreateComment(model),
                    Message = "Comment Added"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("Comment")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.DeleteComment(commentId),
                    Message = "Comment Deleted"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("Liker")]
        [HttpPost]
        public async Task<IActionResult> Liker(string postId, string userId)
        {
            var response = new Results<SuccessResult>();

            try
            {
                response.Response = new SuccessResult()
                {
                    Succeeded = await _repository.HandleLikes(postId, userId),
                    Message = "Like Updated"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }
        [HttpGet]
        [Route("DashboardItems")]
        public async Task<IActionResult> GetDashboard()
        {

            var response = new Results<DashbordViewModel>();

            try
            {
                response.Response = await _repository.GetDashboard();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

    }
}
