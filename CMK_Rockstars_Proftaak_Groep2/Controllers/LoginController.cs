using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class LoginController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteCookie()
        {
            // If cookie exists
            if (Request.Cookies["RockstarEmail"] != null)
            {
                // Delete cookie
                Response.Cookies.Delete("RockstarEmail");
            }
            return View();
            
        }

        [Authorize]
        public IActionResult Auth()
        {
            // If cookie does not exist
            if (Request.Cookies["RockstarEmail"] == null)
            {
                // Create cookie
                CookieOptions options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                };

                // Add cookie
                Response.Cookies.Append("RockstarEmail", @User.Claims.ElementAt(10).Value, options);
            }

            // RockstarId = Get Rockstar by email
            // Get list or tribe-rol pairs
            // Save those to cookie

            return View();
        }
    }
}
