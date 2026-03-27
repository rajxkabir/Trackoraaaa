namespace TimeSheetManager_services.Models
{
public class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int AssignedTo { get; set; }
    public User Assignee { get; set; }

    public int AssignedBy { get; set; }
    public User AssignedByUser { get; set; }

    public string Status { get; set; } = "Pending";
    public string Priority { get; set; }

    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
}