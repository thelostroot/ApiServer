using System;

namespace ApiServer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime PublishTime { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
