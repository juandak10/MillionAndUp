using Domain.Entities;
using Domain.References;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Tools
{
    public class JwtToken
    {
        public IConfiguration _configuration { get; }

        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Account account)
        {
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value)), _configuration.GetSection("Jwt:Encrypt").Value);

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, account.RoleType.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration.GetSection("Jwt:Time").Value)),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(token);
            return jwtSecurityToken.Claims.ToList();
        }

    }
}
