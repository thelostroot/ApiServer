using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Article> Articles { get; set; }

        public Category()
        {
            Articles = new List<Article>();
        }
    }
}
