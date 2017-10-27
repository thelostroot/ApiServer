using ApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Proxies
{
    public class CommentProxy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CommentProxy(Comment comment)
        {
            Id = comment.Id;
            Name = comment.Text;
        }
    }
}
