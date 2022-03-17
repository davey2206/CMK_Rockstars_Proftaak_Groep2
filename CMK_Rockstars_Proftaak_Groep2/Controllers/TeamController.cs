using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add_user()
        {
            return View();
        }

        public IActionResult Add_tribe()
        {
            return View();
        }
    }
}
