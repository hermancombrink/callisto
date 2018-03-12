using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Callisto.Web.Client.Controllers
{
    [AllowAnonymous]
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}