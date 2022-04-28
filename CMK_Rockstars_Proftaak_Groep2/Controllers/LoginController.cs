using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("test");
            return View();
        }

        [Authorize]
        public IActionResult Auth()
        {
            return View();
        }
    }
}
