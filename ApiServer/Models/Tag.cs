using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{   
    public class Tag 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ArticleTags> ArticleTags { get; set; }

        public Tag()
        {
            ArticleTags = new List<ArticleTags>();
        }
    }
}
