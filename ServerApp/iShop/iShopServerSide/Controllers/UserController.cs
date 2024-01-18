using iShopServerSide.DAL;
using iShopServerSide.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iShopServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository UserRepo;

        public UserController(UserRepository userRepo)
        {
            UserRepo = userRepo;
        }
        [HttpPost("login")]
        public UserProfile Login(UserCred credentials)
        {
            var (success, profile) = UserRepo.Login(credentials);

            if (success)
            {
                profile.Token = GenerateToken(profile.UserName, profile.Id);
                return profile;
            }
            else
            {
                return new UserProfile();
            }
        }
        [HttpPost("signup")]
        public bool Signup(UserCred credentials)
        {
            bool success = UserRepo.Signup(credentials);

            return success;
        }

        private string GenerateToken(string username, int userId)
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
                    new Claim(ClaimTypes.Name, username), //userId.ToString()
                    new Claim("Id", userId.ToString()) //custom claims can be added this way.
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
