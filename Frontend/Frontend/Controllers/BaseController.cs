using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace Frontend.Controllers
{
    public class BaseController : Controller
    {
        private readonly IToastNotification _toastService;

        public BaseController(IToastNotification toastService)
        {
            _toastService = toastService;
        }

        [NonAction]
        public void DisplayErrors(Exception ex)
        {
            if (ex.Message.Contains("Response", StringComparison.CurrentCultureIgnoreCase))
            {
                var errObject = JsonConvert.DeserializeObject<Result<Error>>(ex.Message);
                foreach (var err in errObject.Errors)
                {
                    _toastService.AddErrorToastMessage($"Status Code: {err.StatusCode}</br>Error Message: {err.ErrorMessage}" +
                        $"<br>{err.InnerException}");
                }
            }
            else
            {
                _toastService.AddErrorToastMessage(ex.Message);
            }
        }
    }
}
