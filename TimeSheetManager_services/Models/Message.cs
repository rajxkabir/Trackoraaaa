namespace TimeSheetManager_services.Models
{
public class Message
{
    public int Id { get; set; }

    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int? ReceiverId { get; set; }
    public User Receiver { get; set; }

    public int? TeamId { get; set; }
    public Team Team { get; set; }

    public string MessageText { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
}