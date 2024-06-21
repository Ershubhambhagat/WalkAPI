using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.DTO.AuthDTOs;
using NZWalk_API.Repositories.Auth;

namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region ctor
        private const string Error = "Something Went Wrong";
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _token;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository token)
        {
            _userManager = userManager;
            _token = token;
        }


        #endregion

        #region Register


        public static string Error1 => Error;

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username

            };
            var identityResult = await _userManager.CreateAsync(IdentityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles is not null && registerRequestDto.Roles.Any())
                {
                    //Add roles to user
                    identityResult = await _userManager.AddToRoleAsync(IdentityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Created , Login...");
                    }
                }
            }
            return BadRequest(Error1);
        }
        #endregion

        #region Login

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get the Roles for User

                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = _token.CreateJWTToken(user, roles.ToList());
                        var responce = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(responce);
                    }

                }
            }
            return BadRequest("User name or Password is Incorrect ");
        }

        #endregion
    }





}
