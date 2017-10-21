using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiServer.Config;
using ApiServer.Models;
using ApiServer.Proxies;
using Microsoft.IdentityModel.Tokens;
using ApiServer.Extensions;

namespace ApiServer.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;
        private readonly string RandomSalt = "lgZ6K0JWuxhXwLTEl8Jb3RR8V33O3i2K";

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            var passHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passHash;

            user.Role = UserRoles.BasicUser;
            user.Confirmed = false;

            return user;
        }

        /*public string CreatePassHash(string login, string password)
        {
            var passHash = MD5Helper.CreateHash($"{login}{RandomSalt}{password}");
            return passHash;
        }*/

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
