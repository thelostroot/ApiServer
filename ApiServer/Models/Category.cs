using System.Collections.Generic;

namespace ApiServer.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Article> Articles { get; set; }

        public Category()
        {
            Articles = new List<Article>();
        }
    }
}
