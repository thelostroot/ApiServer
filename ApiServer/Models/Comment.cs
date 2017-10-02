﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public DateTime PublishTime { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
