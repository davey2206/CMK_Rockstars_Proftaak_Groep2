using System;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Article
    {
        public string Id { get; set; }
        public string TribeId { get; set; }
        public string RockstarId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string TribeName { get; set; }
        public string RockstarName { get; set; }
    }
}