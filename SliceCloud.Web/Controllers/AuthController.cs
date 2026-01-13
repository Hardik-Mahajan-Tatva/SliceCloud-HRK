using Microsoft.AspNetCore.Mvc;
using SliceCloud.Repository.Models;
using SliceCloud.Repository.ViewModels;
using SliceCloud.Service.Interfaces;

namespace SliceCloud.Web.Controllers;


public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    #region Login GET
    public IActionResult Login()
    {
        return View();
    }
    #endregion
    #region Login POST
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }
        UsersLogin? usersLogin = await _authService.AuthenticateUser(
                     loginViewModel.Email!.ToLower(),
                     loginViewModel.Password!
                 );
        if (usersLogin is not null)
        {
            return RedirectToAction("Dashboard", "Dashboard");
        }
        return View();
    }

    #endregion
}
