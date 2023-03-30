using Frontend.Core.HttpClients;
using Frontend.DTO.Request;
using Frontend.DTO.Response;
using Frontend.DTO.Response.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace Frontend.Controllers
{
	public class AdministrativeController : BaseController
	{
		private readonly IGenericHttpClient _client;
		private readonly IToastNotification _toastService;

		public AdministrativeController(IGenericHttpClient client,
			IToastNotification toastService) : base(toastService)
		{
			_client = client;
			_toastService = toastService;
		}

		[HttpGet]
		public IActionResult Index()
		{

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> RolesList()
		{
			try
			{
				var result = await _client.GetAllAsync<IdentityRole>(ApiConstants.GetAllRoles);
				return View(result);
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}

		}

		[HttpGet]
		public async Task<IActionResult> SingleRole(string roleId)
		{
			try
			{
				var result = await _client.GetAsync<IdentityRole>($"{ApiConstants.SingleRole}?roleId={roleId}");
				return View(result);
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> CreateRole()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateRole(Result<RoleRequestDTO> model)
		{
			try
			{
				var result = await _client.PostAsync<SuccessResultDTO>(ApiConstants.GetAllRoles, model.Response);
				return RedirectToAction(nameof(RolesList));
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> EditRole(string roleId)
		{
			try
			{
				var result = await _client.GetAsync<RoleRequestDTO>($"{ApiConstants.SingleRole}?roleId={roleId}");
				return View(result);
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> EditRole(Result<RoleRequestDTO> model)
		{
			try
			{
				var result = await _client.PutAsync<SuccessResultDTO>(ApiConstants.SingleRole, model.Response);

				if (result.Response.Succeeded)
					_toastService.AddSuccessToastMessage(result.Response.Message);

				return RedirectToAction(nameof(RolesList));
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> AddRemoveRoles(string userId)
		{
			try
			{
				var result = await _client.GetAsync<AddRemoveRoles>($"{ApiConstants.UserRoles}?userId={userId}");
				return View(result);
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> AddRemoveRoles(Result<AddRemoveRoles> model)
		{
			try
			{
				var result = await _client.PostAsync<SuccessResultDTO>(ApiConstants.UserRoles, model.Response);
				if (result.Response.Succeeded)
					_toastService.AddSuccessToastMessage(result.Response.Message);

				return RedirectToAction("UsersList", "Account");
			}
			catch (Exception ex)
			{
				DisplayErrors(ex);
				return View();
			}
		}
	}
}
