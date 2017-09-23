using System.Collections.Generic;
using ApiServer.Models;

namespace ApiServer.Proxies
{
    public class CategoryProxy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryProxy(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}
