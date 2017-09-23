using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;

namespace ApiServer.Proxies
{
    public class SourceProxy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public SourceProxy(Source source)
        {
            Id = source.Id;
            Name = source.Name;
            Url = source.Url;
        }
    }
}
