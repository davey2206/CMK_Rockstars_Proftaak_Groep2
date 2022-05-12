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

        public List<Comment> Comments { get; set; }
    }
}