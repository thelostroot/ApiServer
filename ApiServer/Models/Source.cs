using System.Collections.Generic;

namespace ApiServer.Models
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<Article> Articles { get; set; }

        public Source()
        {
            Articles = new List<Article>();
        }
    }
}
