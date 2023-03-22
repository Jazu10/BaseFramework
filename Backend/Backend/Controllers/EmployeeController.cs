using Backend.Core.Common.ErrorHandlers;
using Backend.Core.Data.Entities;
using Backend.Core.RepositoryInterface;
using Backend.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [Route("GetAllNews")]
        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var response = new Results<List<NewsModel>>()
            {
                Errors = new List<Error>()
            };

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

        [Route("GetNewsByUserId")]
        [HttpGet]
        public async Task<IActionResult> GetNewsByUserId(string userId)
        {
            var response = new Results<List<NewsModel>>()
            {
                Errors = new List<Error>()
            };

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


        [Route("EditNews")]
        [HttpGet]
        public async Task<IActionResult> EditNews(NewsModel model)
        {
            var response = new Results<bool>()
            {
                Errors = new List<Error>()
            };

            try
            {
                response.Response = await _repository.UpdateNews(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex, 400, null);
                return BadRequest(response);
            }
        }



        [Route("GetAllAdvertisements")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var response = new Results<List<AdvertisementModel>>()
            {
                Errors = new List<Error>()
            };

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

        [Route("GetAdvetisementsByUserId")]
        [HttpGet]
        public async Task<IActionResult> GetAdvertisementsByUserId(string userId)
        {
            var response = new Results<List<AdvertisementModel>>()
            {
                Errors = new List<Error>()
            };

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

        [Route("CreateAdvertisements")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisements(AdvertisementModel model)
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


        [Route("EditAdvertisements")]
        [HttpPut]
        public async Task<IActionResult> EditAdvertisements(AdvertisementModel model)
        {
            var response = new Results<bool>()
            {
                Errors = new List<Error>()
            };

            try
            {
                response.Response = await _repository.UpdateAdvertisements(model);
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
