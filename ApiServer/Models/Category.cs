using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Post> Posts { get; set; }

        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
