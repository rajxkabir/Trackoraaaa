namespace TimeSheetManager_services.Models
{
public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int LeaderId { get; set; }
    public User Leader { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<TeamMember> Members { get; set; }
}
}