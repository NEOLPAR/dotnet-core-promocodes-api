using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Authentication;
using System.Threading.Tasks;

namespace PromocodesApp.Authentication
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetId()
        {
            var response = await _userService.GetId();

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var response = await _userService.Login(model);

            if (response == null)
                return BadRequest(new
                {
                    Status = "Error",
                    Message = "Username or password is incorrect."
                });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var response = await _userService.Register(model);

            if (response == null) 
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { Status = "Error", Message = "Please check user details and try again." });

            if (response == "exists")
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { Status = "Error", Message = "User already exists." });

            return Ok(response);
        }


    }
}
