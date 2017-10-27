using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Proxies
{
    public class UserProxy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public UserProxy(UserProxy user)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            Role = user.Role;
        }
    }
}
