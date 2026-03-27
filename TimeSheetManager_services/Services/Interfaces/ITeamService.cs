using TimeSheetManager_services.Models;
using TimeSheetManager_services.DTOs.Team;

namespace TimeSheetManager_services.Services.Interfaces
{
    public interface ITeamService
    {
        Team CreateTeam(CreateTeamDto dto);
        List<Team> GetAllTeams();
        void AddMember(AddMemberDto dto);
        Team GetMyTeam(int leaderId);
    }
}