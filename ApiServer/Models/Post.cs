using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название новости не может быть пустым")]
        public string Title { get; set; }

        public DateTime PublishTime { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage = "Текст новости не может быть пустым")]
        public string PostText { get; set; }

        public string Image { get; set; }

        public List<Comment> Comments { get; set; }
        public List<PostTags> PostTags { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            PostTags = new List<PostTags>();
        }
    }
}
