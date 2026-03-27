namespace TimeSheetManager_services.Models
{
public class ReportDetail
{
    public int Id { get; set; }

    public int ReportId { get; set; }
    public DailyReport Report { get; set; }

    public int? TaskId { get; set; }
    public TaskItem Task { get; set; }

    public string Notes { get; set; }
}
}