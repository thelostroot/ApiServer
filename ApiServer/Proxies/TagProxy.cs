using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;

namespace ApiServer.Proxies
{
    public class TagProxy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagProxy(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }
    }
}
