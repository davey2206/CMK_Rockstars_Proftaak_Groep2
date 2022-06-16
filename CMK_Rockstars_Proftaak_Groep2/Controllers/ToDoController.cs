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
    public class ToDoController : Controller
    {
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Article> articleList = new List<Article>();
            List<Comment> comments = new List<Comment>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/Article/All"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    articleList = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/comment/notapproved"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse);
                }
            }

            ViewData["Comments"] = comments;
            ViewData["Articles"] = articleList.Where(a => a.published == false && a.concept == false).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCommentAsync(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://api-rockstarsit.azurewebsites.net/api/comment/" + id))
                {
                    await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptCommentAsync([Bind("Id, ArticleId, UserId, UserName, CommentText, Approved, CommentDate")] Comment comment)
        {
            comment.Approved = true;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://api-rockstarsit.azurewebsites.net/api/comment/" + comment.Id, content))
                {
                    await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptArticleAsync([Bind("Id,TribeId,RockstarId,Title,Content,TribeName,RockstarName")] Article article)
        {
            article.published = true;
            article.publishDate = DateTime.Now;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://api-rockstarsit.azurewebsites.net/api/Article/" + article.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticleAsync([Bind("Id,TribeId,RockstarId,Title,Content,TribeName,RockstarName")] Article article)
        {
            article.concept = true;
            article.published = false;
            article.publishDate = DateTime.Now;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://api-rockstarsit.azurewebsites.net/api/Article/" + article.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
