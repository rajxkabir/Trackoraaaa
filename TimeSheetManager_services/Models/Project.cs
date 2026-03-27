namespace TimeSheetManager_services.Models
{
public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Status { get; set; }

    public int CreatedBy { get; set; }
    public User Creator { get; set; }

    public ICollection<ProjectTeam> ProjectTeams { get; set; }
}
}