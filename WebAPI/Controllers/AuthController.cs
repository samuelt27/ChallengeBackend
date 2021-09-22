using ChallengeBackend.WebAPI.Identity;
using ChallengeBackend.WebAPI.Identity.Interfaces;
using ChallengeBackend.WebAPI.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService tokenHandlerService)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromForm] UserRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser is not null)
                    return BadRequest($"El Email {user.Email} esta en uso.");

                var isCreated = await _userManager.CreateAsync(new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email
                }, password: user.Password);

                if (isCreated.Succeeded)
                    return StatusCode(201);
                else
                    return BadRequest(isCreated.Errors.Select(e => e.Description));
            }
            else
                return BadRequest("Error al registrarse.");
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromForm] UserRequest user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser is null)
                {
                    return BadRequest(new LoginResponse
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto."
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(isCorrect)
                {
                    var parameters = new TokenParameters
                    {
                        Id = existingUser.Id,
                        UserName = existingUser.UserName,
                        PasswordHash = existingUser.PasswordHash
                    };

                    var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);

                    return Ok(new LoginResponse
                    {
                        Login = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new LoginResponse
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto."
                        }
                    });
                }
            }
            else
            {
                return BadRequest(new LoginResponse
                {
                    Login = false,
                    Errors = new List<string>()
                    {
                        "Usuario o contraseña incorrecto."
                    }
                });
            }
        }
    }
}
