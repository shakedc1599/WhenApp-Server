using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using WebApplication1.Services;
using whenAppModel.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api")]
    public class AutenticationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUsersService userService;
        private readonly IAuthenticationService JWTService;

        public AutenticationController(IConfiguration _config, IUsersService _service, IAuthenticationService _JWTService)
        {
            userService = _service;
            configuration = _config;
            JWTService = _JWTService;
        }

        public class AutenticationPayload
        {
            public string? username { get; set; }
            public string? password { get; set; }
        }

        //login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AutenticationPayload payload)
        {

            if (string.IsNullOrEmpty(payload.username) || string.IsNullOrEmpty(payload.password))
            {
                return BadRequest(new { message = "Username or password are missing" });
            }

            if (await userService.Validation(payload.username, payload.password))
            {
                var token = JWTService.CreateToken(payload.username);

                Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });

                return Ok(token);
                //return Ok(await service.Get(username));
            }
            return BadRequest(new { message = "Username or password is incorrect" });
        }

        //Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AutenticationPayload payload)
        {
            if (string.IsNullOrEmpty(payload.username) || string.IsNullOrEmpty(payload.password))
            {
                return BadRequest(new { message = "Username or password are missing" });
            }

            if (await userService.Get(payload.username) != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            await userService.Add(payload.username, payload.password);
            var token = JWTService.CreateToken(payload.username);

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(token);
        }
    }

}
