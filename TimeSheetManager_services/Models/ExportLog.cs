namespace TimeSheetManager_services.Models
{
public class ExportLog
{
    public int Id { get; set; }

    public int AdminId { get; set; }
    public User Admin { get; set; }

    public string ExportType { get; set; }
    public int ReferenceId { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public string FileType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
}