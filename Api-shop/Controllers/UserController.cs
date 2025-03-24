using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using WebApiUseSqlLiteItog.Models.Context;
using Microsoft.AspNetCore.Identity;
using WebApiUseSqlLiteItog.Models;

namespace WebApiUseSqlLiteItog.Controllers
{
    [Route("api-shop")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly EFProductContext _context;

        public UserController(EFProductContext context)
        {
            _context = context;
        }


        //Регистрация
        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<User>> PostRegisterUser(User user)
        {
            if (_context.Users.Find(user.Email) != null)
            {
                return StatusCode(403);
            }
            string status = "user";
            if(user.IsAdmin == 1)
            {
                status = "admin";
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), new Claim("role", status) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));


            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            user.UserToken = encodedJwt;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return StatusCode(201, encodedJwt);
            
        }



        //Аутентификация
        [HttpPost, Route("Login")]
        public async Task<ActionResult<User>> PostLoginUser(User user)
        {
            var userInDb = _context.Users.Find(user.Email);
            if (userInDb == null || userInDb.Password != user.Password)
            {
                return StatusCode(401, "Auth failed");
            }
            string status = "user";
            if (userInDb.IsAdmin == 1)
            {
                status = "admin";
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), new Claim("role", status) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            userInDb.UserToken = encodedJwt;


            _context.Users.Update(userInDb);
            _context.SaveChanges();
            return Ok(encodedJwt);
        }

    }
}
