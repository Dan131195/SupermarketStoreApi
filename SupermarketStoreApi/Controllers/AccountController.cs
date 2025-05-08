using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SupermarketStoreApi.Models.Auth;
using SupermarketStoreApi.Settings;
using SupermarketStoreApi.DTOs.Account;
using SupermarketStoreApi.Data;
using SupermarketStoreApi.Models;
using SupermarketStoreApi.Services;

namespace SupermarketStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly EmailService _emailService;

        public AccountController(IOptions<Jwt> jwtOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, EmailService emailService)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(registerRequestDto.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { Message = "Email già registrata." });
                }

                var newUser = new ApplicationUser
                {
                    Email = registerRequestDto.Email,
                    UserName = registerRequestDto.Email,
                    FirstName = registerRequestDto.FirstName,
                    LastName = registerRequestDto.LastName
                };

                var result = await _userManager.CreateAsync(newUser, registerRequestDto.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var roleExists = await _roleManager.RoleExistsAsync(registerRequestDto.Ruolo);
                if (!roleExists)
                {
                    return BadRequest(new { Message = $"Il ruolo '{registerRequestDto.Ruolo}' non esiste." });
                }

                await _userManager.AddToRoleAsync(newUser, registerRequestDto.Ruolo);

                Guid? clienteId = null;

                if (registerRequestDto.Ruolo == "User")
                {
                    var cliente = new Cliente
                    {
                        ClienteId = Guid.NewGuid(),
                        CodiceFiscale = registerRequestDto.CodiceFiscale,
                        UserId = newUser.Id
                    };

                    using var scope = HttpContext.RequestServices.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Clienti.Add(cliente);
                    await dbContext.SaveChangesAsync();

                    clienteId = cliente.ClienteId;
                }
                await _emailService.SendEmailAsync(
                    newUser.Email,
                    "Benvenuto su SpeedMarket!",
                    $@"
                    <div style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
                        <h2 style='color: #2e7d32;'>Ciao {newUser.FirstName},</h2>
                        <p>Grazie per esserti registrato su <strong>SpeedMarket</strong>! 🎉</p>
                        <p>Ora puoi ordinare comodamente online e ricevere aggiornamenti sul tuo ordine in tempo reale.</p>
                        <hr style='border: none; border-top: 1px solid #ccc;' />
                        <p style='font-size: 14px; color: #555;'>Buono shopping,<br/>Il team di SpeedMarket</p>
                    </div>"
                );

                return Ok(new RegisterResponseDto
                {
                    UserId = newUser.Id,
                    Email = newUser.Email,
                    FullName = $"{newUser.FirstName} {newUser.LastName}",
                    Ruolo = registerRequestDto.Ruolo,
                    ClienteId = clienteId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Errore durante la registrazione",
                    Details = ex.Message
                });
            }
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized("Invalid email or password.");
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);
            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponse
            {
                Token = tokenString,
                Expires = expiry,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminRequestDto registerRequestDto)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(registerRequestDto.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { Message = "Email già registrata." });
                }

                var newUser = new ApplicationUser
                {
                    Email = registerRequestDto.Email,
                    UserName = registerRequestDto.Email,
                    FirstName = registerRequestDto.FirstName,
                    LastName = registerRequestDto.LastName
                };

                var result = await _userManager.CreateAsync(newUser, registerRequestDto.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var roleExists = await _roleManager.RoleExistsAsync(registerRequestDto.Ruolo);
                if (!roleExists)
                {
                    return BadRequest(new { Message = $"Il ruolo '{registerRequestDto.Ruolo}' non esiste." });
                }

                await _userManager.AddToRoleAsync(newUser, registerRequestDto.Ruolo);

                return Ok(new RegisterResponseDto
                {
                    UserId = newUser.Id,
                    Email = newUser.Email,
                    FullName = $"{newUser.FirstName} {newUser.LastName}",
                    Ruolo = registerRequestDto.Ruolo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Errore durante la registrazione", Details = ex.Message });
            }
        }
    }
}