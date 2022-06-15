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
            List<Comment> comments = new List<Comment>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/comment/notapproved"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse);
                }
            }

            if (articleList != null)
            {
                ViewData["CommentsToDo"] = comments.Where(c => c.Approved == false).Take(3).ToList();
                ViewData["ArticlesToDo"] = articleList.Where(a => a.published == false && a.concept == false).Take(3).ToList();
                ViewData["Articles"] = articleList;
                ViewData["LastArticles"] = GetLastArticles(articleList);
                ViewData["TopTotalViews"] = GetTopTotalViews(articleList);
                ViewData["TopViews"] = GetTopViews(articleList);
            }
            return View();
        }

        public List<Article> GetLastArticles(List<Article> articleList)
        {
            List<Article> articles = new List<Article>();
            articleList.Sort((x, y) => DateTime.Compare(x.publishDate, y.publishDate));
            articleList.Reverse();
            for (int i = 0; i < (articleList.Count < 3 ? articleList.Count : 3); i++)
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
            for (int i = 0; i < (articleList.Count < 3 ? articleList.Count : 3); i++)
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
            for (int i = 0; i < (articleList.Count < 3 ? articleList.Count : 3); i++)
            {
                articles.Add(articleList[i]);
            }

            return articles;
        }
    }
}
