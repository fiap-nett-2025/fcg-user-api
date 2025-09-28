using FCG.User.Application.DTO;
using FCG.User.Application.Services;
using FCG.User.Domain.Entities;
using FCG.User.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FCG.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly UserManager<Domain.Entities.User> _userManager;
        private readonly SignInManager<Domain.Entities.User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;

        public AuthController(
            UserManager<Domain.Entities.User> userManager,
            SignInManager<Domain.Entities.User> signInManager,
            RoleManager<IdentityRole> roleManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new Domain.Entities.User(dto.Name, dto.Email);
            var password = new Password(dto.Password);

            var result = await _userManager.CreateAsync(user, password.PlainText);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            return CreatedResponse(user, "Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    return UnauthorizedResponse("Usuário ou senha inválidos.");

                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (!result.Succeeded)
                    return UnauthorizedResponse("Usuário ou senha inválidos.");

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.GenerateToken(user, roles);
                var response = new
                {
                    Token = token,
                    User = new
                    {
                        user.Id,
                        user.Email,
                        user.Name,
                        roles
                    }
                };

                return Success(response);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
