using Microsoft.AspNetCore.Mvc;
using TimeSheetManager_services.Data;

namespace TimeSheetManager_services.Controllers
{
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public AuthController(IAuthService authService, ApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        try
        {
            var token = _authService.Login(dto);

            var user = _context.Users
                .FirstOrDefault(u => u.Email == dto.Email);

            return Ok(new
            {
                token,
                user.Id,
                user.Email,
                user.Role
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
}