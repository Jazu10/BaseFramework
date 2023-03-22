using Backend.Core.Common.ErrorHandlers;
using Backend.Core.Data.Entities;
using Backend.Core.Data;
using Backend.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AdministrativeController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrativeController(UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> AllRoles()
        {
            var response = new Results<List<IdentityRole>>()
            {
                Errors = new List<Error>()
            };

            try
            {
                response.Response = await _roleManager.Roles.ToListAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Roles")]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            var response = new Results<IdentityResult>()
            {
                Errors = new List<Error>()
            };

            IdentityRole role = new IdentityRole
            {
                Name = model.Name
            };
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        response.Response = result;
                        return Ok(response);
                    }

                    foreach (var error in result.Errors)
                    {
                        response.Errors = ErrorHandler.GetErrorAsync(response.Errors, error,
                            StatusCodes.Status400BadRequest, null);

                    }

                    return BadRequest(response);
                }
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Internal Server Error",
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }

            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("SingleRole")]
        public async Task<IActionResult> SingleRole(string roleId)
        {
            var response = new Results<IdentityRole>()
            {
                Errors = new List<Error>()
            };

            try
            {
                IdentityRole role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                    throw new Exception("No role found");

                response.Response = role;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("SingleRole")]
        public async Task<IActionResult> UpdateRole(RoleViewModel model)
        {
            var response = new Results<IdentityResult>()
            {
                Errors = new List<Error>()
            };

            try
            {
                if (ModelState.IsValid)
                {

                    IdentityRole role = await _roleManager.FindByIdAsync(model.Id);

                    if (role == null)
                        throw new Exception("No role found");

                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        response.Response = result;
                        return Ok(response);
                    }

                    foreach (var error in result.Errors)
                    {
                        response.Errors = ErrorHandler.GetErrorAsync(response.Errors, error,
                            StatusCodes.Status400BadRequest, null);

                    }

                    return BadRequest(response);
                }
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, "Internal Server Error",
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }

            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("UserRoles")]
        public async Task<IActionResult> AddRemoveRoles(string UserId)
        {

            var response = new Results<UserResult>()
            {
                Errors = new List<Error>()
            };

            try
            {
                var user = await _userManager.FindByIdAsync(UserId);

                if (user == null)
                    throw new Exception("Invalid User Id");

                var roles = await _roleManager.Roles.ToListAsync();


                UserResult userResult = new UserResult()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                foreach (var role in roles)
                {
                    userResult.AddRemove.Add(new AddRemoveViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                        IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                    });
                }

                response.Response = userResult;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status400BadRequest, null);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("UserRoles")]
        public async Task<IActionResult> AddRemoveRoles(UserResult model)
        {
            var response = new Results<bool>()
            {
                Errors = new List<Error>()
            };
            try
            {
                var user = await _userManager.FindByIdAsync(model?.UserId);

                if (user == null)
                    throw new Exception("Invalid User Id");

                IdentityResult result = new IdentityResult();

                for (int i = 0; i < model?.AddRemove.Count; i++)
                {
                    if (model.AddRemove[i].IsSelected && !await _userManager.IsInRoleAsync(user, model.AddRemove[i].RoleName))
                    {
                        result = await _userManager.AddToRoleAsync(user, model.AddRemove[i].RoleName);
                    }
                    else if (!model.AddRemove[i].IsSelected && await _userManager.IsInRoleAsync(user, model.AddRemove[i].RoleName))
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.AddRemove[i].RoleName);
                    }
                    else
                    {
                        continue;
                    }
                }
                response.Response = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("UserClaims")]
        public async Task<IActionResult> AddRemoveClaims(string UserId)
        {
            var response = new Results<UserClaimViewModel>()
            {
                Errors = new List<Error>()
            };
            try
            {
                var user = await _userManager.FindByIdAsync(UserId);

                if (user == null)
                    throw new Exception("Invalid User Id");

                var existingClaims = await _userManager.GetClaimsAsync(user);

                UserClaimViewModel model = new UserClaimViewModel()
                {
                    UserId = UserId,
                };

                foreach (var claim in ClaimStores.claims)
                {
                    UserClaim userClaim = new UserClaim()
                    {
                        ClaimType = claim.Type,
                        IsSelected = existingClaims.Any(item => item.Type == claim.Type)
                    };
                    model.UserClaims.Add(userClaim);
                }

                response.Response = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("UserClaims")]
        public async Task<IActionResult> AddRemoveClaims(UserClaimViewModel model)
        {
            var response = new Results<bool>()
            {
                Errors = new List<Error>()
            };
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                var existingClaims = await _userManager.GetClaimsAsync(user);
                var result = await _userManager.RemoveClaimsAsync(user, existingClaims);

                if (!result.Succeeded)
                {
                    throw new Exception("Can't remove claims for the given user");
                }

                var claims = model.UserClaims.Where(item => item.IsSelected).Select(item => new Claim(item.ClaimType, item.ClaimType));

                result = await _userManager.AddClaimsAsync(user, claims);

                if (!result.Succeeded)
                {
                    throw new Exception("Can't add claims for the given user");
                }

                response.Response = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                    StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }


        [HttpGet]
        [Route("RoleClaims")]
        public async Task<IActionResult> AddRemoveRoleClaims(string roleId)
        {
            var response = new Results<RoleClaimViewModel>();
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                var existingClaims = await _roleManager.GetClaimsAsync(role); response.Response = new RoleClaimViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                foreach (var claim in ClaimStores.claims)
                {
                    UserClaim userClaim = new UserClaim()
                    {
                        ClaimType = claim.Type,
                        IsSelected = existingClaims.Any(item => item.Type == claim.Type)
                    };

                    response.Response.userClaims.Add(userClaim);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                   StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("RoleClaims")]
        public async Task<IActionResult> AddRemoveRoleClaims(RoleClaimViewModel model)
        {
            var response = new Results<bool>();
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var existingClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var item in existingClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, item);
                }

                var claims = model.userClaims.Where(item => item.IsSelected)
                    .Select(item => new Claim(item.ClaimType, item.ClaimType));

                foreach (var claim in claims)
                {
                    await _roleManager.AddClaimAsync(role, claim);
                }

                response.Response = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors = ErrorHandler.GetErrorAsync(response.Errors, ex,
                  StatusCodes.Status500InternalServerError, null);

                return BadRequest(response);
            }
        }
    }
}
