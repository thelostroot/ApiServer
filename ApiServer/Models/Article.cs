using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{
    public class Article
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название статьи не может быть пустым")]
        public string Title { get; set; }

        public DateTime PublishTime { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // TODO: Исправить имя
        public string ArticleText { get; set; }

        public List<Comment> Comments { get; set; }
        public List<ArticleTags> ArticleTags { get; set; }

        public Article()
        {
            Comments = new List<Comment>();
            ArticleTags = new List<ArticleTags>();
        }
    }
}
