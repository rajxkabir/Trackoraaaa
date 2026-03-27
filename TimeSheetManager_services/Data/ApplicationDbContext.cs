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

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .ToTable(tb => tb.HasTrigger("trg_emp_code"));

            modelBuilder.Entity<Team>()
                .ToTable(tb => tb.HasTrigger("trg_team_code"));

            modelBuilder.Entity<Project>()
                .ToTable(tb => tb.HasTrigger("trg_proj_code"));


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(e => e.EMP_TEAM_ID)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Team>()
                .HasOne(t => t.TeamLead)
                .WithMany()
                .HasForeignKey(t => t.team_lead_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}