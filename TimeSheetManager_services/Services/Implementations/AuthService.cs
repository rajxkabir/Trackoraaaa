using System.Linq;
using TimeSheetManager_services.Data;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwtService;

    public AuthService(ApplicationDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public string Login(LoginDto dto)
    {
       var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if (user == null)
            throw new Exception("Invalid Email");

        
       if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
{
    throw new Exception("Invalid email or password");
}

        return _jwtService.GenerateToken(user);
    }
}