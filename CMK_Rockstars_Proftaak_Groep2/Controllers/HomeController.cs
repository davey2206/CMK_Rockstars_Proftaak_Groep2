﻿using CMK_Rockstars_Proftaak_Groep2.Models;
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
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }
            ViewData["Articles"] = articleList;
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([Bind("Id,TribeId,RockstarId,Title,Content,TribeName,RockstarName")] Article article)
        {
            Article receivedArticle = new Article();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://rockstar-api.azurewebsites.net/api/Article", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedArticle = JsonConvert.DeserializeObject<Article>(apiResponse);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditAsync(string id)
        {
            Article article = new Article();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    article = JsonConvert.DeserializeObject<Article>(apiResponse);
                }
            }
            ViewBag.article = article;
            return View();
        }
        [HttpPost]
        public IActionResult Edit([Bind("Id,TribeId,RockstarId,Title,Content,TribeName,RockstarName")] Article article)
        {
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
