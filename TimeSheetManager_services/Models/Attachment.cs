namespace TimeSheetManager_services.Models
{
public class Attachment
{
    public int Id { get; set; }

    public int TaskId { get; set; }
    public TaskItem Task { get; set; }

    public string FilePath { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.Now;
}
}