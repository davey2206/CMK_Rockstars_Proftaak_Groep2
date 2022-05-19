using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMK_Rockstars_Proftaak_Groep2.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class TeamController : Controller
    {
        //[Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            List<Rockstar> rockstarList = new List<Rockstar>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/rockstar"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    rockstarList = JsonConvert.DeserializeObject<List<Rockstar>>(apiResponse);
                }
            }

            List<Tribe> tribeList = new List<Tribe>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/tribe"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tribeList = JsonConvert.DeserializeObject<List<Tribe>>(apiResponse);
                }
            }

            ViewData["Tribes"] = tribeList;
            ViewData["Rockstars"] = rockstarList;
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
