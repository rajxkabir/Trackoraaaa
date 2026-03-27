namespace TimeSheetManager_services.Models
{
public class DailyReport
{
    public int Id { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }

    public int LeaderId { get; set; }
    public User Leader { get; set; }

    public DateTime ReportDate { get; set; }
    public string Summary { get; set; }

    public string Status { get; set; } = "Submitted";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
}