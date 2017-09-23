using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;

namespace ApiServer.Proxies
{
    public class ArticleProxy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishTime { get; set; }
        public string ArticleText { get; set; }

        public SourceProxy Source { get; set; }
        public CategoryProxy Category { get; set; }

        public List<CommentsProxy> Comments { get; set; }
        public List<TagProxy> Tags { get; set; }

        public ArticleProxy(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            PublishTime = article.PublishTime;
            ArticleText = article.ArticleText;
            Source = new SourceProxy(article.Source);
            Category = new CategoryProxy(article.Category);
            Comments = article.Comments.Select(x => new CommentsProxy(x)).ToList();
            Tags = article.ArticleTags.Select(x => new TagProxy(x.Tag)).ToList();
        }
    }
}
