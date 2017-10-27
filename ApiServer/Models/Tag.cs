using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiServer.Models
{   
    public class Tag 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PostTags> PostTags { get; set; }

        public Tag()
        {
            PostTags = new List<PostTags>();
        }
    }
}
