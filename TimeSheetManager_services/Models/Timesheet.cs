namespace TimeSheetManager_services.Models
{
public class Timesheet
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int? TaskId { get; set; }
    public TaskItem Task { get; set; }

    public DateTime WorkDate { get; set; }
    public decimal HoursWorked { get; set; }

    public string Description { get; set; }
    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
}