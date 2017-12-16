using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Proxies
{
    public class EditPostProxy
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public int CategoryId { get; set; }
        public String Text { get; set; }
        public List<TagProxy> Tags { get; set;}

    }
}
