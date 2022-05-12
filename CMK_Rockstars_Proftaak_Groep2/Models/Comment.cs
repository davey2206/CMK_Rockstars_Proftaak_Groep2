using System;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public bool Approved { get; set; }
        public DateTime CommentDate { get; set; }
    }
}