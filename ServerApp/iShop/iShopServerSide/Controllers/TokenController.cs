using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using iShopServerSide.Model;
using iShopServerSide.DAL;

namespace iShopServerSide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        UserRepository UserRepo;
        public TokenController(UserRepository UserRepo) {
            this.UserRepo = UserRepo;
        }

        [HttpPost]
        public string Index(UserCred User)
        {

            //TODO - validate username and password
            //if valid: continue, else return error back

            // The secret key used to sign the token
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            // The issuer and audience of the token
            var myIssuer = "https://mysite.com";
            var myAudience = "https://myaudience.com";

            // Create a token handler and a token descriptor
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Add the user id as a claim
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, User.UserName), //userId.ToString()
                    //new Claim("Key", "value") custom claims can be added this way.
                }),
                // Set the token expiration, issuer, audience, and signing credentials
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            // Create and write the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
