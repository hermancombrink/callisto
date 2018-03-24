using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Callisto.WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("dashboard")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}