using System.ComponentModel.DataAnnotations;
namespace TimeSheetManager_services.Models
{
public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public string? Role { get; set; } = "Employee";

    // 🔥 NEW ADDITIONS
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // 🔥 RELATION
    public ICollection<TeamMember> TeamMembers { get; set; }
}
}