using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiServer.Config;
using ApiServer.Models;
using ApiServer.Proxies;
using ApiServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;

        public AuthController(ApplicationContext context)
        {
            _context = context;
            _userService = new UserService(_context);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
           if (User.Identity.IsAuthenticated == true)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existUser =_context.Users.FirstOrDefault(x => x.Login == user.Login || x.Email == user.Email);
            if (existUser != null)
            {
                return BadRequest("Пользователь с таким логин или почтой уже существует");
            }

            _context.Users.Add(_userService.CreateUser(user));
            await _context.SaveChangesAsync();

            return Created("Register", user);
        }

        [HttpPost("Token")]
        public async Task Token([FromBody]LoginProxy loginData)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == loginData.Login);

            if (user == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Неверный логин или пароль!");
                return;
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password);
            if(!validPassword)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Неверный логин или пароль!");
                return;
            }


            var encodedJwt = _userService.CreateToken(user);

            var response = new
            {
                access_token = encodedJwt,
                username = loginData.Login
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}