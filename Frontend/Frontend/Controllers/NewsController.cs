using Frontend.Core.HttpClients;
using Frontend.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;



namespace Frontend.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IGenericHttpClient _client;
        private readonly IWebHostEnvironment _env;



        public NewsController(IGenericHttpClient client,
        IWebHostEnvironment env,
        IToastNotification toastService) : base(toastService)
        {
            _client = client;
            _env = env;
        }
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
            return View();
        }
    }
}