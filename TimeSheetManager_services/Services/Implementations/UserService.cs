using TimeSheetManager_services.Data;
using TimeSheetManager_services.Models;
using TimeSheetManager_services.DTOs.User;
using BCrypt.Net;
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User Create(CreateUserDto dto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Role = dto.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }
    public User Update(int id, UpdateUserDto dto)
{
    var user = _context.Users.Find(id);
    if (user == null)
        throw new Exception("User not found");
    user.UpdatedAt = DateTime.UtcNow;
    user.FirstName = dto.FirstName;
    user.LastName = dto.LastName;
    user.Role = dto.Role;

    _context.SaveChanges();

    return user;
}

public void Delete(int id)
{
    var user = _context.Users.Find(id);
    if (user == null)
        throw new Exception("User not found");

    _context.Users.Remove(user);
    _context.SaveChanges();
}
}