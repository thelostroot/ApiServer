using System;
using System.Collections.Generic;

namespace ApiServer.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishTime { get; set; }

        public int SourceId { get; set; }
        public Source Source { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ArticleText { get; set; }
        public string Tags { get; set; }

        public List<Comment> Comments { get; set; }
        public List<ArticleTags> ArticleTagses { get; set; }

        public Article()
        {
            Comments = new List<Comment>();
            ArticleTagses = new List<ArticleTags>();
        }
    }
}
