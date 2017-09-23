using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;

namespace ApiServer.Proxies
{
    public class UserProxy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public UserProxy(User user)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            Email = user.Email;
            Pass = user.Pass;
        }
    }
}
