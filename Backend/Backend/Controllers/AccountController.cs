using Backend.Core.Data;
using Backend.Core.Data.Entities;
using Backend.Core.Common.ErrorHandlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Core.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Backend.Core.RepositoryInterface;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Basic")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AccountController> _logger;

        private const string UserListCache = "UserList";

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context, IMemoryCache cache, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;


            _context = context;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        [Route("UserList")]
        public async Task<IActionResult> UsersList()
        {
            var response = new Results<List<UserModel>>();
            try
            {
                if (_cache.TryGetValue(UserListCache, out Results<List<UserModel>> userList))
                {
                    return Ok(userList);
                }
                else
                {
                    response.Response = await _context.UserList.Include(item => item.User).ToListAsync();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);

                    _cache.Set(UserListCache, response, cacheOptions);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                _logger.LogError("Error Occured While Fetching User List");

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("SingleUser")]
        public async Task<IActionResult> SingleUserDetails([FromQuery] string userId)
        {
            var response = new Results<UserModel>();

            try
            {
                response.Response = await _context.UserList.Include(item => item.User)
                    .Where(item => item.UserId == userId).FirstOrDefaultAsync();

                if (response.Response == null)
                    throw new Exception("Invalid User Id");

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                _logger.LogError("Error Occured While Fetching User");

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]

        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.EmailId,
                    Email = model.EmailId,
                    PhoneNumber = model.PhoneNumber
                };

                if (ModelState.IsValid)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        model.UserId = user.Id;
                        _context.UserList.Add(model);
                        _context.SaveChanges();

                        _logger.LogInformation("New User Created");

                        response.Response = new SuccessResult()
                        {
                            Succeeded = result.Succeeded,
                            Message = "User Registration Successfull"
                        };

                        return Ok(response);
                    }
                    var ErrorList = result.Errors.ToList();
                    foreach (var error in ErrorList)
                    {
                        response.Errors = ErrorHandler.GetErrorAsync(response.Errors, error,
                            StatusCodes.Status400BadRequest, null);
                    }

                    return BadRequest(response);
                }

                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Internal Server Error",
                    StatusCodes.Status500InternalServerError, null);

                _logger.LogError("Error Occured While Registering User");

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                _logger.LogError("Error Occured While Registering User");

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var response = new Results<UserModel>();

            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                    throw new Exception("Invalid User Id");

                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if (result)
                {
                    response.Response = await _context.UserList.Include(item => item.User)
                    .Where(item => item.UserId == user.Id).FirstOrDefaultAsync();

                    return Ok(response);
                }

                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Invalid Credentials",
                    StatusCodes.Status400BadRequest, null);

                _logger.LogError("Login Error");

                return BadRequest(response);

            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                _logger.LogError("Login Error");

                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("SingleUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            var response = new Results<SuccessResult>();

            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser user = await _userManager.FindByIdAsync(model.UserId);

                    if (user == null)
                        throw new Exception("Invalid User Id");

                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.EmailId;

                    await _userManager.UpdateAsync(user);

                    _context.UserList.Update(model);
                    _context.SaveChanges();

                    response.Response = new SuccessResult()
                    {
                        Succeeded = true,
                        Message = "User Profile Updated"
                    };

                    if (_cache.Get(UserListCache) != null)
                        _cache.Remove(UserListCache);

                    return Ok(response);
                }

                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Internal Server Error",
                    StatusCodes.Status500InternalServerError, null);

                _logger.LogError("User Update Error");

                return BadRequest(response);

            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                _logger.LogError("User Update Error");

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRoles(string roleId)
        {
            var response = new Results<List<UserModel>>()
            {
                Response = new List<UserModel>()
            };

            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                var users = await _context.UserList.ToListAsync();
                for (int i = 0; i < users.Count; i++)
                {
                    users[i].User = await _userManager.FindByIdAsync(users[i].UserId);
                }

                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user?.User, role.Name))
                    {
                        response?.Response?.Add(user);
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var response = new Results<SuccessResult>();
           
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(model.UserId);

                    if (user == null)
                        throw new Exception("Invalid User Id");


                    var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        response.Response = new SuccessResult()
                        {
                            Succeeded = true,
                            Message = "Password Updated"
                        };

                        return Ok(response);
                    }
                    else
                    {
                        var ErrorList = result.Errors.ToList();
                        foreach (var error in ErrorList)
                        {
                            response.Errors = ErrorHandler.GetErrorAsync(response.Errors, error,
                                StatusCodes.Status400BadRequest, null);
                        }
                    }
                }
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Couldn't Change User Password",
                        StatusCodes.Status400BadRequest, null);

                _logger.LogError("Change Password Error");

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                return BadRequest(response);
            }
        }
    }
}
