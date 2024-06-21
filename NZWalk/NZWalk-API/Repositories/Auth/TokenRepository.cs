using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalk_API.Repositories.Auth
{

    public class TokenRepository : ITokenRepository
    {
        #region Ctor
        private readonly IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region CreateJWTToken
        public string CreateJWTToken(IdentityUser user, List<string> Roles)
        {
            //Create Claims from Roles
            var clams = new List<Claim>();
            clams.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in Roles)
            {
                clams.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                clams,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        #endregion
    }


}
