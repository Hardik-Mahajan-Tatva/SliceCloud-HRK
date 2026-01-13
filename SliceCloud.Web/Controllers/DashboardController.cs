using Microsoft.AspNetCore.Mvc;

namespace SliceCloud.Web.Controllers;

public class DashboardController() : Controller
{

    #region Login GET
    public IActionResult Dashboard()
    {
        return View();
    }
    #endregion
}
