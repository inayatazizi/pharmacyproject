using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Controllers
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
