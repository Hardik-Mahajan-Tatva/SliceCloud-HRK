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

    #region AddNewUser

    [HttpGet]
    public async Task<IActionResult> AddNewUser()
    {
        try
        {
            CreateUserViewModel createUserViewModel = new()
            {
                Countries = await _countryService.GetAllCountriesAsync(),
                Roles = await _rolesService.GetAllRolesAsync()
            };

            return View(createUserViewModel);
        }
        catch (Exception)
        {
            TempData.SetToast("error", "An error occurred while processing your request. Please try again.");
            return View();
        }
    }

    #endregion

    #region GetStatesByCountry

    [HttpGet]
    public async Task<IActionResult> GetStatesByCountry(int CountryId)
    {
        try
        {
            List<State>? states = await _stateService.GetStatesByCountryIdAsync(CountryId);
            return Json(states);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    #endregion

    #region GetCitiesByState

    [HttpGet]
    public async Task<IActionResult> GetCitiesByState(int StateId)
    {
        try
        {

            var cities = await _cityService.GetCitiesByStateIdAsync(StateId);
            return Json(cities);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    #endregion

    #region AddNewUser

    [HttpPost]
    public async Task<IActionResult> AddNewUser(CreateUserViewModel createUserViewModel, IFormFile? itemImage)
    {
        try
        {
            var uniqueErrors = await _usersService.ValidateUniqueFieldsAsync(createUserViewModel);
            foreach (var error in uniqueErrors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key]?.Errors;
                    if (errors != null)
                        foreach (var error in errors)
                        {
                            Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                        }
                }
            }

            if (!ModelState.IsValid)
            {
                createUserViewModel.Countries = await _countryService.GetAllCountriesAsync();

                createUserViewModel.States = createUserViewModel.CountryId != 0
                        ? await _stateService.GetStatesByCountryIdAsync(createUserViewModel.CountryId)
                        : [];

                createUserViewModel.Cities = createUserViewModel.StateId != 0
                        ? await _cityService.GetCitiesByStateIdAsync(createUserViewModel.StateId)
                        : [];

                createUserViewModel.Roles = await _rolesService.GetAllRolesAsync();
                TempData.SetToast("warn", "Please submit the valid form");

                return View(createUserViewModel);
            }

            bool isUserCreated = await _usersService.CreateUserAsync(createUserViewModel, itemImage!);
            if (isUserCreated)
            {
                string resetToken = await _authService.GeneratePasswordResetTokenAsync(createUserViewModel.Email ?? string.Empty);

                string resetLink = Url.Action("ResetPassword", "Auth", new
                {
                    token = resetToken
                }, Request.Scheme!)!;

                await _emailSenderService.SendResetPasswordEmailAsync(createUserViewModel.Email ?? string.Empty, resetLink);
                TempData.SetToast("success", "User created successfully.");
                return RedirectToAction("Users", "Users");
            }
            else
            {
                TempData.SetToast("error", "User can't created");
                return RedirectToAction("Users", "Users");
            }
        }
        catch (Exception)
        {
            TempData.SetToast("warn", "An error occurred while processing your request. Please try again.");
            return View();
        }
    }
    #endregion

    #region UpdateUser

    [HttpGet]
    public async Task<IActionResult> UpdateUser(int id)
    {
        try
        {
            UpdateUserViewModel? user = await _usersService.GetUserByIdAsync(id);
            UpdateUserViewModel model = new()
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                UserName = user.UserName!,
                RoleId = user.RoleId,
                Email = user.Email,
                Password = user.Password,
                CountryId = user.CountryId,
                StateId = user.StateId,
                CityId = user.CityId,
                Address = user.Address ?? string.Empty,
                ZipCode = user.ZipCode,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                ProfileImage = user.ProfileImage,

                Countries = await _countryService.GetAllCountriesAsync(),
                States = await _stateService.GetStatesByCountryIdAsync(user.CountryId),
                Cities = await _cityService.GetCitiesByStateIdAsync(user.StateId),
                Roles = await _rolesService.GetAllRolesAsync()
            };
            return View(model);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again.";
            return View();
        }
    }

    #endregion
}
