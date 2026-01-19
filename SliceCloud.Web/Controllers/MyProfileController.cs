using Microsoft.AspNetCore.Mvc;

namespace SliceCloud.Web.Controllers;

public class MyProfileController() : Controller
{

    #region MyProfile GET
    public IActionResult MyProfile()
    {
        return View();
    }
    #endregion
}
