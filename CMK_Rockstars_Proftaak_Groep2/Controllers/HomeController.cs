using CMK_Rockstars_Proftaak_Groep2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Article> articleList = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }
            ViewData["Articles"] = articleList;
            return View();
        }

        public async Task<IActionResult> Add()
        {
            List<Tribe> userTribeList = new List<Tribe>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/Tribe"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userTribeList = JsonConvert.DeserializeObject<List<Tribe>>(apiResponse);
                }
            }
            if (userTribeList != null)
            {
                ViewData["tribeList"] = userTribeList;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([Bind("Id,TribeId,RockstarId,Title,Content,TribeName,RockstarName,Published")] string submitButton, Article article)
        {
            switch (submitButton)
            {
                case "Concept":
                    article.concept = true;
                    break;
                case "Toevoegen":
                    article.concept = false;
                    break;
            }

            article.published = false;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://api-rockstarsit.azurewebsites.net/api/Article", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditAsync(string id)
        {
            Article article = new Article();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/Article/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    article = JsonConvert.DeserializeObject<Article>(apiResponse);
                }
            }
            ViewBag.article = article;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync([Bind("Id,TribeId,RockstarId,Title,Content,TribeName,RockstarName")] string submitButton, Article article)
        {
            switch (submitButton)
            {
                case "Concept":
                    article.concept = true;
                    break;
                case "Bewerkten":
                    article.concept = false;
                    break;
            }
            article.publishDate = DateTime.Now;
            article.published = false;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://api-rockstarsit.azurewebsites.net/api/Article/" + article.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://api-rockstarsit.azurewebsites.net/api/Article/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
