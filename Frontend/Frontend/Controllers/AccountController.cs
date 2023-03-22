using Frontend.Core.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Frontend.DTO.Request;
using Frontend.DTO.Response;
using Newtonsoft.Json;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IGenericHttpClient _client;
        private readonly IWebHostEnvironment _env;
        private IToastNotification _toastService;

        public AccountController(IGenericHttpClient client,
            IWebHostEnvironment env, IToastNotification toastService):base(toastService)
        {
            _client = client;
            _env = env;
            _toastService = toastService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(UsersList));
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            try
            {
                var result = await _client.PostAsync<UserResponseDTO>(ApiConstants.Login, model);
                if (result.Response != null)
                {
                    var flag = await CreateIdentity(result.Response);
                    if (flag)
                    {
                        _toastService.AddSuccessToastMessage("Welcome User, Login Successfull");
                        return RedirectToAction(nameof(UsersList), "Account");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            RegistrationRequestDTO user = new RegistrationRequestDTO();
            user.DOB = DateTime.Parse("2006/01/01");
            user.DOJ = DateTime.Now;
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (model.File != null)
                    {
                        string folderPath = Path.Combine(_env.WebRootPath, "Images");
                        string filePath = Path.Combine(folderPath, model.File.FileName);
                        model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                        model.Image = model.File.FileName;
                    }
                    var result = await _client.PostAsync<IdentityResultResponseDTO>(ApiConstants.Register, model);

                    if(result.Response.Succeeded) 
                        _toastService.AddSuccessToastMessage("User Registration Successful");

                    return RedirectToAction(nameof(Login));
                }
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            try
            {
                var result = await _client.GetAllAsync<UserResponseDTO>(ApiConstants.GetAllUsers);
                return View(result);
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            try
            {
                var result = await _client.GetAsync<UserResponseDTO>($"{ApiConstants.SingleUser}?userId={userId}");
                return View(result);
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(Result<UserResponseDTO> model)
        {
            try
            {
                RegistrationRequestDTO updateModel = new RegistrationRequestDTO()
                {
                    UserId = model.Response.userId,
                    UserName = model.Response.user.userName,
                    EmailId = model.Response.user.email,
                    PhoneNumber = model.Response.user.phoneNumber,
                    FirstName = model.Response.firstName,
                    LastName = model.Response.lastName,
                    DOB = model.Response.dob,
                    DOJ = model.Response.doj,
                    Gender = model.Response.gender,
                    Address = model.Response.address,
                    Image = model.Response.image,
                    Password = "DefaultPassword",
                    ConfirmPassword = "DefaultPassword",
                };

                if (model.Response.File != null)
                {
                    string folderPath = Path.Combine(_env.WebRootPath, "Images");
                    string filePath = Path.Combine(folderPath, model.Response.File.FileName);
                    model.Response.File.CopyTo(new FileStream(filePath, FileMode.Create));
                    updateModel.Image = model.Response.File.FileName;
                }

                var result = await _client.PutAsync<UserResponseDTO>($"{ApiConstants.SingleUser}", updateModel);
                _toastService.AddSuccessToastMessage("User Details Updated");
                return RedirectToAction(nameof(UsersList));
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string userId)
        {
            try
            {
                var result = await _client.GetAsync<UserResponseDTO>($"{ApiConstants.SingleUser}?userId={userId}");
                return View(result);
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
                return View();
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result = await _client.PostAsync<IdentityResultResponseDTO>(ApiConstants.ChangePassword, model);
                    if (result.Response.Succeeded)
                    {
                        _toastService.AddSuccessToastMessage("Password Updated");
                        await HttpContext.SignOutAsync();
                        return RedirectToAction(nameof(Login));
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                DisplayErrors(ex);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        [NonAction]
        private async Task<bool> CreateIdentity(UserResponseDTO model)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, model.userId),
                new Claim(ClaimTypes.Name, model.user.userName),
                new Claim(ClaimTypes.Email, model.user.email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = false });

            return true;
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
