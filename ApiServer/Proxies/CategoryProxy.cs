using ApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
