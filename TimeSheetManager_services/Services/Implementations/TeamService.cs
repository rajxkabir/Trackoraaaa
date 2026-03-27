using Microsoft.EntityFrameworkCore;
using TimeSheetManager_services.Data;
using TimeSheetManager_services.Models;
using TimeSheetManager_services.DTOs.Team;
using TimeSheetManager_services.Services.Interfaces;

namespace TimeSheetManager_services.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Team CreateTeam(CreateTeamDto dto)
        {
            var leader = _context.Users.Find(dto.LeaderId);

            if (leader == null || leader.Role != "Leader")
                throw new Exception("Invalid Leader");

            var team = new Team
            {
                Name = dto.Name,
                LeaderId = dto.LeaderId
            };

            _context.Teams.Add(team);
            _context.SaveChanges();

            return team;
        }

        public List<Team> GetAllTeams()
        {
            return _context.Teams
                .Include(t => t.Leader)
                .Include(t => t.Members)
                .ToList();
        }

        public void AddMember(AddMemberDto dto)
        {
           var alreadyInOtherTeam = _context.TeamMembers
      .Any(x => x.UserId == dto.UserId);

    if (alreadyInOtherTeam)
    throw new Exception("User already in another team");

            var member = new TeamMember
            {
                TeamId = dto.TeamId,
                UserId = dto.UserId
            };

            _context.TeamMembers.Add(member);
            _context.SaveChanges();
        }

        public Team GetMyTeam(int leaderId)
        {
            return _context.Teams
                .Include(t => t.Members)
                .FirstOrDefault(t => t.LeaderId == leaderId);
        }
    }
}