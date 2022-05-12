using System;
using System.Collections.Generic;
using System.Net.Http;
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
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/Article/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    article = JsonConvert.DeserializeObject<Article>(apiResponse);
                }
                
                using (var response = await httpClient.GetAsync("https://rockstar-api.azurewebsites.net/api/comment/all/articleid/" + id))
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
                using (var response = await httpClient.DeleteAsync("https://rockstar-api.azurewebsites.net/api/comment/" + id))
                {
                    await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", new { id = articleId });
        }   
    }
}