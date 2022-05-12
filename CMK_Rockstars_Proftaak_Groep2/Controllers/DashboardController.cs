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
        public IActionResult Index()
        {
            var LastArtikels = GetLastArtikelsAsync();
            var TopTotalViews = GetTopTotalViewsAsync();
            var TopViews = GetTopViewsAsync();

            ViewData["LastArtikels"] = LastArtikels;
            ViewData["TopTotalViews"] = TopTotalViews;
            ViewData["TopViews"] = TopViews;
            return View();
        }
        public async Task<List<Article>> GetLastArtikelsAsync()
        {
            List<Article> articleList = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }
            articleList.Sort((x, y) => DateTime.Compare(x.publishDate, y.publishDate));
            articleList.RemoveRange(0, articleList.Count - 3);
            articleList.Reverse();

            return articleList;
        }

        public async Task<List<Article>> GetTopTotalViewsAsync()
        {
            List<Article> articleList = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }

            articleList = articleList.OrderBy(a => a.totalViewCount).ToList();
            articleList.RemoveRange(0, articleList.Count - 3);
            articleList.Reverse();

            return articleList;
        }
        public async Task<List<Article>> GetTopViewsAsync()
        {
            List<Article> articleList = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }

            articleList = articleList.OrderBy(a => a.viewCount).ToList();
            articleList.RemoveRange(0, articleList.Count - 3);
            articleList.Reverse();

            return articleList;
        }
    }
}
