using ApiServer.Models;
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
        public string Login { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }

        public UserProxy(User user)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            Login = user.Login;
            Email = user.Email;
            Avatar = user.Avatar;
            Role = user.Role;
        }
    }
}
