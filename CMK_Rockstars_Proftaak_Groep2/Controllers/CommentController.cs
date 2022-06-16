using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CMK_Rockstars_Proftaak_Groep2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMK_Rockstars_Proftaak_Groep2.Controllers
{
    public class CommentController : Controller
    {
        // GET
        public async Task<IActionResult> Index(Guid id)
        {
            Article article = new Article();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/Article/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    article = JsonConvert.DeserializeObject<Article>(apiResponse);
                }
                
                using (var response = await httpClient.GetAsync("https://api-rockstarsit.azurewebsites.net/api/comment/all/articleid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != "")
                    {
                        article.Comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse);
                    }
                }
            }

            return View(article);
        }
        
        // DELETE
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id, Guid articleId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://api-rockstarsit.azurewebsites.net/api/comment/" + id))
                {
                    await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", new { id = articleId });
        }

        // UPDATE
        [HttpPost]
        public async Task<IActionResult> AcceptAsync([Bind("Id, ArticleId, UserId, UserName, CommentText, Approved, CommentDate")] Comment comment)
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

            return RedirectToAction("Index", new { id = comment.ArticleId });
        }
    }
}