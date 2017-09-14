using System.Collections.Generic;

namespace ApiServer.Models
{   
    public class Tag 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleTags> ArticleTagses { get; set; }

        public Tag()
        {
            ArticleTagses = new List<ArticleTags>();
        }
    }
}
