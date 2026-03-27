 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TimeSheetManager_services.Data;
using TimeSheetManager_services.Models;

namespace TimeSheetManager_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = null };

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _context.Projects.ToListAsync();

                Console.WriteLine($"\n--- 🏗️ Fetching {projects.Count} Projects ---");
                foreach (var proj in projects)
                {
                    Console.WriteLine($"Project: [{proj.PROJECT_CODE}] {proj.Name} | Status: {proj.Status}");
                }

                if (projects == null || !projects.Any())
                {
                    return NotFound(new { message = "No projects found." });
                }

                return new JsonResult(projects, _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return StatusCode(500, new { message = "Error fetching projects", details = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProject([FromBody] Project model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid project data provided." });
            }

            try
            {
                model.Id = 0; 
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;

                if (string.IsNullOrEmpty(model.Status)) model.Status = "Pending";

                _context.Projects.Add(model);
                await _context.SaveChangesAsync();

                Console.WriteLine($"✅ Project Saved: {model.Name} (Code: {model.PROJECT_CODE})");

                return Ok(new
                {
                    message = "Project successfully created!",
                    projectId = model.Id
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Fatal Error: {ex.Message}");
                return StatusCode(500, new { message = "Internal server error during save.", details = ex.Message });
            }
        }
    }
}