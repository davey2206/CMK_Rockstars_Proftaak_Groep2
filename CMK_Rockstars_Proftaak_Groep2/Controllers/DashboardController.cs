using CMK_Rockstars_Proftaak_Groep2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class DashboardController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            List<Tribe> userTribeList = new List<Tribe>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Tribe"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userTribeList = JsonConvert.DeserializeObject<List<Tribe>>(apiResponse);
                }
            }
            List<Article> articleList = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }

            if (articleList != null)
            {
                ViewData["Articles"] = articleList;
                ViewData["LastArticles"] = GetLastArticles(articleList);
                ViewData["TopTotalViews"] = GetTopTotalViews(articleList);
                ViewData["TopViews"] = GetTopViews(articleList);
            }
            if (userTribeList != null)
            {
                ViewData["userTribeList"] = userTribeList;
            }
            System.Diagnostics.Debug.WriteLine("submitted");


            return View();
        }

        [HttpPost]
        public ActionResult index(Tribe model)
        {
            System.Diagnostics.Debug.WriteLine("submitted");
            ViewData["SelectedTribe"] = model.displayName;
            return View(model);
        }
        public List<Article> GetLastArticles(List<Article> articleList)
        {
            List<Article> articles = new List<Article>();
            articleList.Sort((x, y) => DateTime.Compare(x.publishDate, y.publishDate));
            articleList.Reverse();
            for (int i = 0; i < 3; i++)
            {
                articles.Add(articleList[i]);
            }

            return articles;
        }

        public List<Article> GetTopTotalViews(List<Article> articleList)
        {
            List<Article> articles = new List<Article>();
            articleList = articleList.OrderBy(a => a.totalViewCount).ToList();
            articleList.Reverse();
            for (int i = 0; i < 3; i++)
            {
                articles.Add(articleList[i]);
            }

            return articles;
        }

        public List<Article> GetTopViews(List<Article> articleList)
        {
            List<Article> articles = new List<Article>();
            articleList = articleList.OrderBy(a => a.viewCount).ToList();
            articleList.Reverse();
            for (int i = 0; i < 3; i++)
            {
                articles.Add(articleList[i]);
            }

            return articles;
        }
    }
}
