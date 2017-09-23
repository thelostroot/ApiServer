using System.Collections.Generic;

namespace ApiServer.Models
{   
    public class Tag 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ArticleTags> ArticleTags { get; set; }

        public Tag()
        {
            ArticleTags = new List<ArticleTags>();
        }
    }
}
