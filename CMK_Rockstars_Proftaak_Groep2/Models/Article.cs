using System;
using System.Collections.Generic;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Article
    {
        [Newtonsoft.Json.JsonProperty("Id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string TribeId { get; set; }
        public string RockstarId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Newtonsoft.Json.JsonProperty("TribeName", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TribeName { get; set; }
        [Newtonsoft.Json.JsonProperty("RockstarName", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string RockstarName { get; set; }
        public bool concept { get; set; }
        public bool published { get; set; }
        [Newtonsoft.Json.JsonProperty("publishDate", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DateTime publishDate { get; set; }
        [Newtonsoft.Json.JsonProperty("viewCount", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int viewCount { get; set; }
        [Newtonsoft.Json.JsonProperty("totalViewCount", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int totalViewCount { get; set; }
        public List<Comment> Comments { get; set; }
    }
}