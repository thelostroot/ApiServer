using System.Collections.Generic;

namespace ApiServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public List<Comment> Comments { get; set; }

        public User()
        {
            Comments = new List<Comment>();
        }
    }
}
