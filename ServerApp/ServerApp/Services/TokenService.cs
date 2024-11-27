using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Models;
using ServerApp.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerApp.Services
{
    public class TokenService: ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IOptions<JwtOptions> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<TokenResponse> GenerateJwtToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
            //var userRoles = await _userManager.GetRolesAsync(user);



            var tokenDescriptor = new SecurityTokenDescriptor
            {


                Subject = new ClaimsIdentity(new Claim[]
                 {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("nameSurname",user.Name),
                    //new Claim(ClaimTypes.Role,userRoles.FirstOrDefault()),

                 }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinute),
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
            };

            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var response = new TokenResponse();
            response.Token = tokenHandler.WriteToken(securityToken);
            response.Expiration = DateTime.Now.AddMinutes(_jwtOptions.ExpirationMinute);
            return response;
            
        }
    }
}
