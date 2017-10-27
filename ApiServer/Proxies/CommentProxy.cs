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
        public string Text { get; set; }
        public UserProxy User { get; set; }

        public CommentProxy(Comment comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            User = new UserProxy(comment.User);
        }
    }
}
