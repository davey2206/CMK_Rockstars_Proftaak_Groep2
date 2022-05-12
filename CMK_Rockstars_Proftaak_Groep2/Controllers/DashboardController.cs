using CMK_Rockstars_Proftaak_Groep2.Models;
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
            List<Article> articleList = new List<Article>();
            List<Article> LastArticles = new List<Article>();
            List<Article> TopTotalViews = new List<Article>();
            List<Article> TopViews = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }

            LastArticles = articleList;
            TopTotalViews = articleList;
            TopViews = articleList;

            LastArticles.Sort((x, y) => DateTime.Compare(x.publishDate, y.publishDate));
            LastArticles.RemoveRange(0, LastArticles.Count - 3);
            LastArticles.Reverse();

            TopTotalViews = TopTotalViews.OrderBy(a => a.totalViewCount).ToList();
            TopTotalViews.RemoveRange(0, TopTotalViews.Count - 3);
            TopTotalViews.Reverse();

            TopViews = TopViews.OrderBy(a => a.viewCount).ToList();
            TopViews.RemoveRange(0, TopViews.Count - 3);
            TopViews.Reverse();

            ViewData["LastArticles"] = LastArticles;
            ViewData["TopTotalViews"] = TopTotalViews;
            ViewData["TopViews"] = TopViews;
            return View();
        }
    }
}
