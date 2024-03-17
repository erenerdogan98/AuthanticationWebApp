using AuthanticationWebAPI.Models;
using AuthanticationWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthanticationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService ?? throw new ArgumentNullException(nameof(authService));

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserLogin user)
        {
            var result = await _authService.Register(user);
            if(result)
            {
                return Ok(result);
            }
            return BadRequest("Please , pass all required parameters.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var result = await _authService.Login(user);
            if( result)
            {
                // to generate token
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest("Username or password is not correct");
        }
    }
}
