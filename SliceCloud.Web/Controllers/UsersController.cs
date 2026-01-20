using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SliceCloud.Repository.Models;
using SliceCloud.Repository.ViewModels;
using SliceCloud.Service.Interfaces;
using SliceCloud.Service.Utils;

namespace SliceCloud.Web.Controllers;

public class UsersController(IUsersService usersService, ICountryService countryService, IStateService stateService, ICityService cityService, IEmailSenderService emailSenderService, IRolesService rolesService, IAuthService authService) : Controller
{
    private readonly IUsersService _usersService = usersService;
    private readonly IAuthService _authService = authService;
    private readonly ICountryService _countryService = countryService;
    private readonly ICityService _cityService = cityService;
    private readonly IStateService _stateService = stateService;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly IRolesService _rolesService = rolesService;

    #region Users GET

    public async Task<IActionResult> Users(int pageNumber = 1, int pageSize = 5, string query = "", string sortOrder = "asc", string sortColumn = "FirstName", string search = "")
    {
        try
        {
            PaginatedList<User>? paginatedUsers = await _usersService.GetAllUsersAsync(pageNumber, pageSize, query, sortOrder, sortColumn, search);
            if (Request.Headers.XRequestedWith == "XMLHttpRequest")
            {
                return PartialView("_UserTablePartialView", paginatedUsers);
            }
            return View(paginatedUsers);
        }
        catch
        {
            TempData.SetToast("error", "An error occurred while processing your request. Please try again.");
            return View();
        }
    }

    #endregion
}
