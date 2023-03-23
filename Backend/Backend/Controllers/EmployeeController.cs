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

        [Route("NewsList")]
        [HttpPost]
        public async Task<IActionResult> CreateNews(NewsModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
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
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var response = new Results<List<AdvertisementModel>>();

            try
            {
                response.Response = await _repository.GetAllAdvertisements();
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

        [Route("AdvertisementList")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisements(AdvertisementModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                model.IsActive = true;
                model.IsDeleted = false;

                if (ModelState.IsValid)
                {
                    response.Response = new SuccessResult()
                    {
                        Succeeded = await _repository.CreateAdvertisements(model)
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

        [Route("GetAllPosts")]
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var response = new Results<List<PostModel>>()
            {
                Errors = new List<Error>()
            };

            try
            {
                response.Response = await _repository.GetAllPosts();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("GetPostsByUserId")]
        [HttpGet]
        public async Task<IActionResult> GetPostsByUserId(string userId)
        {
            var response = new Results<List<PostModel>>()
            {
                Errors = new List<Error>()
            };

            try
            {
                response.Response = await _repository.GetUsersPosts(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }

        [Route("CreatePosts")]
        [HttpPost]
        public async Task<IActionResult> CreatePosts(PostModel model)
        {
            var response = new Results<SuccessResult>()
            {
                Errors = new List<Error>()
            };

            try
            {
                model.IsActive = true;
                model.IsDeleted = false;

                if (ModelState.IsValid)
                {
                    response.Response = new SuccessResult()
                    {
                        Succeeded = await _repository.CreatePost(model)
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
    }
}
