using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TimeSheetManager_services.Services.Interfaces;
using TimeSheetManager_services.DTOs.Team;

namespace TimeSheetManager_services.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }

        // 🔥 Admin create team
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateTeamDto dto)
        {
            return Ok(_service.CreateTeam(dto));
        }

        // 🔥 Admin view all teams
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllTeams());
        }

        // 🔥 Leader add member
        [Authorize(Roles = "Leader")]
        [HttpPost("add-member")]
        public IActionResult AddMember(AddMemberDto dto)
        {
            _service.AddMember(dto);
            return Ok("Member Added");
        }

        // 🔥 Leader view own team
        [Authorize(Roles = "Leader")]
        [HttpGet("my")]
        public IActionResult MyTeam()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            return Ok(_service.GetMyTeam(userId));
        }
    }
}