using AuthServices.Interfaces;
using AuthServices.Models;
using AuthServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepostory;
        public AuthController(IAuthRepository authRepository)
        {
            this._authRepostory = authRepository;
        }
        [AllowAnonymous]
        [Route("GetToken")]
        [HttpPost]
        public IActionResult GetToken(User user)
        {
            User dbUser = this._authRepostory.GetUserDetailByCredentials(user.EmailId,user.Password);
            if (dbUser is null) 
            {
                return BadRequest("Invalid request");
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var utcNow = DateTime.UtcNow;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub,dbUser.EmailId),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,utcNow.ToUniversalTime().ToString(),ClaimValueTypes.Integer64)
            };
            string validIssuer = "http://localhost:9003";
            string validAudience = "http://localhost:9000";

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=secretKey,
                ValidateIssuer=true,
                ValidateAudience=true,
                ValidateLifetime=true,
                RequireExpirationTime=true,
                ValidIssuer= validIssuer,
                ValidAudience= validAudience
            };
            var tokeOptions = new JwtSecurityToken(
                issuer: validIssuer,
                audience: validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            //var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
            dbUser.Password = "";
            return Ok(new { Token = tokenString, User=dbUser });
        }



        /// <summary>
        /// It will run properly with Authorize attribute only when we authenticate this
        /// service in this same project itself.
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("testWithAuth")]
        public IActionResult testWithAuth()
        {
            var lst = new List<string>() {
                "testWithAuth","x","y","z"
            };
            return Ok(lst);
        }
        [HttpGet]
        [Route("testWithoutAuth")]
        public IActionResult testWithoutAuth()
        {
            var lst = new List<string>() {
                "testWithoutAuth","a","b","c"
            };
            return Ok(lst);
        }
    }
}
