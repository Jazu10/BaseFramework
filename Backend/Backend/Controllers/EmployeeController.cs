using Backend.Core.Common.ErrorHandlers;
using Backend.Core.Data.Entities;
using Backend.Core.RepositoryInterface;
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
    }
}
