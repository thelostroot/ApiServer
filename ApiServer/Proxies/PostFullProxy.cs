using ApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Proxies
{
    public class PostFullProxy
    {
        public PostProxy Post { get; set; }
        public List<CommentProxy> Comments { get; set; }

        public PostFullProxy(Post post )
        {
            Post = new PostProxy(post);
            Comments = post.Comments.Select(com => new CommentProxy(com)).ToList();
        }
    }
}
