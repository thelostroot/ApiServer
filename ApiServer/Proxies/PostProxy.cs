using ApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Proxies
{
    public class PostProxy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime PublishTime { get; set; }
        public CategoryProxy Category { get; set; }
        public List<TagProxy> Tags { get; set; }

        public PostProxy(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Text = post.PostText;
            Image = post.Image;
            PublishTime = post.PublishTime;
            Category = new CategoryProxy(post.Category);           
            Tags = post.PostTags.Select(at => new TagProxy(at.Tag)).ToList();
        }
    }
}
