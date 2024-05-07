using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.DTO;
using BaiThucHanhWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BaiThucHanhWeb.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            // Your registration logic here
            var user = new User(); // Replace this with your user creation logic
            await _userRepository.AddUserAsync(user);

            // Generate token
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username);

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null || user.Password != request.Password)
                return BadRequest("Invalid username or password");

            return Ok(new { token = _jwtService.GenerateToken(user.Id, user.Username) });
        }

    }
}
