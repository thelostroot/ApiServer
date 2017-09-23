using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;

namespace ApiServer.Proxies
{
    public class CommentsProxy
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserProxy User { get; set; }

        public CommentsProxy(Comment comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            User = new UserProxy(comment.User);
        }
    }
}
