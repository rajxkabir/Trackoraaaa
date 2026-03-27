using Microsoft.EntityFrameworkCore;
using TimeSheetManager_services.Models;

namespace TimeSheetManager_services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    // 🔥 DbSets (All Tables)
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTeam> ProjectTeams { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Timesheet> Timesheets { get; set; }
    public DbSet<DailyReport> DailyReports { get; set; }
    public DbSet<ReportDetail> ReportDetails { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<ExportLog> ExportLogs { get; set; }

    // 🔥 Relationships Configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ User Email Unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // ✅ Team Leader (User)
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Leader)
            .WithMany()
            .HasForeignKey(t => t.LeaderId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ TeamMember (Many-to-Many)
        modelBuilder.Entity<TeamMember>()
            .HasIndex(tm => new { tm.TeamId, tm.UserId })
            .IsUnique();

        modelBuilder.Entity<TeamMember>()
            .HasOne(tm => tm.Team)
            .WithMany(t => t.Members)
            .HasForeignKey(tm => tm.TeamId);

        modelBuilder.Entity<TeamMember>()
            .HasOne(tm => tm.User)
            .WithMany(u => u.TeamMembers)
            .HasForeignKey(tm => tm.UserId);

        // ✅ Project Created By
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Creator)
            .WithMany()
            .HasForeignKey(p => p.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Project ↔ Team
        modelBuilder.Entity<ProjectTeam>()
            .HasIndex(pt => new { pt.ProjectId, pt.TeamId })
            .IsUnique();

        modelBuilder.Entity<ProjectTeam>()
            .HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTeams)
            .HasForeignKey(pt => pt.ProjectId);

        modelBuilder.Entity<ProjectTeam>()
            .HasOne(pt => pt.Team)
            .WithMany()
            .HasForeignKey(pt => pt.TeamId);

        // ✅ Task Relations
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany()
            .HasForeignKey(t => t.ProjectId);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Assignee)
            .WithMany()
            .HasForeignKey(t => t.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.AssignedByUser)
            .WithMany()
            .HasForeignKey(t => t.AssignedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Timesheet
        modelBuilder.Entity<Timesheet>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<Timesheet>()
            .HasOne(t => t.Project)
            .WithMany()
            .HasForeignKey(t => t.ProjectId);

        modelBuilder.Entity<Timesheet>()
            .HasOne(t => t.Task)
            .WithMany()
            
            .HasForeignKey(t => t.TaskId)
            .OnDelete(DeleteBehavior.SetNull);

modelBuilder.Entity<Timesheet>()
        .Property(t => t.HoursWorked)
        .HasPrecision(5, 2);

modelBuilder.Entity<Timesheet>()
    .HasOne(t => t.Task)
    .WithMany()
    .HasForeignKey(t => t.TaskId)
    .OnDelete(DeleteBehavior.NoAction); // 🔥 FIX

    modelBuilder.Entity<Timesheet>()
    .HasOne(t => t.User)
    .WithMany()
    .HasForeignKey(t => t.UserId)
    .OnDelete(DeleteBehavior.NoAction);

modelBuilder.Entity<Timesheet>()
    .HasOne(t => t.Project)
    .WithMany()
    .HasForeignKey(t => t.ProjectId)
    .OnDelete(DeleteBehavior.NoAction);

        // ✅ Daily Report
        modelBuilder.Entity<DailyReport>()
            .HasOne(r => r.Team)
            .WithMany()
            .HasForeignKey(r => r.TeamId);

        modelBuilder.Entity<DailyReport>()
            .HasOne(r => r.Leader)
            .WithMany()
            .HasForeignKey(r => r.LeaderId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Report Details
        modelBuilder.Entity<ReportDetail>()
            .HasOne(rd => rd.Report)
            .WithMany()
            .HasForeignKey(rd => rd.ReportId);

        modelBuilder.Entity<ReportDetail>()
            .HasOne(rd => rd.Task)
            .WithMany()
            .HasForeignKey(rd => rd.TaskId)
            .OnDelete(DeleteBehavior.SetNull);

        // ✅ Notifications
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId);

        // ✅ Messages
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Team)
            .WithMany()
            .HasForeignKey(m => m.TeamId)
            .OnDelete(DeleteBehavior.SetNull);

        // ✅ Task Comments
        modelBuilder.Entity<TaskComment>()
            .HasOne(tc => tc.Task)
            .WithMany()
            .HasForeignKey(tc => tc.TaskId);

        modelBuilder.Entity<TaskComment>()
            .HasOne(tc => tc.User)
            .WithMany()
            .HasForeignKey(tc => tc.UserId);

        // ✅ Attachments
        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Task)
            .WithMany()
            .HasForeignKey(a => a.TaskId);

        // ✅ Export Logs
        modelBuilder.Entity<ExportLog>()
            .HasOne(e => e.Admin)
            .WithMany()
            .HasForeignKey(e => e.AdminId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
}