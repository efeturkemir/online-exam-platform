    using examApi.Models;
    using examApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.Data;
    using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace examApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]

      

        public class UsersController : ControllerBase
        {
            private readonly IUserService _mongoDbService;

        public UsersController(IUserService mongoDbService, IConfiguration configuration)
            {
                _mongoDbService = mongoDbService;
        }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            {
                var users = await _mongoDbService.GetAllUsersAsync();
                return Ok(users);
            }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Models.RegisterRequest user)
        {
            try
            {
                // Check if username already exists
                var existingUser = await _mongoDbService.GetByUsernameAsync(user.Username);
                if (existingUser != null)
                {
                    return Conflict(new { message = "Username already exists" });
                }

                if (user.Password != user.ConfirmPassword)
                {
                    return Conflict( new { message = "Passwords do not match" });
                }

                // Hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Create new user object
                var newUser = new User
                {
                    username = user.Username,
                    password = hashedPassword, // Store the hashed password
                    role = user.Role
                };

                var token = await _mongoDbService.RegisterUserAsync(newUser);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500,new { message = $"An error occurred while processing your request. {ex.Message}" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Models.LoginRequest loginRequest)
        {
            try
            {
                var token = await _mongoDbService.LoginUserAsync(loginRequest.Username, loginRequest.Password);
                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                var User = await _mongoDbService.GetByUsernameAsync(loginRequest.Username);

                return Ok(new { Token = token,});
            }
            catch (Exception ex)
            {
                // Log the exception details
                // Consider using a logging framework or service
                return StatusCode(500, new { message = $"An error occurred while processing your request. {ex.Message}" });
            }
        }










    }
}
