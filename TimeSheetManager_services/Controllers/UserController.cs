using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TimeSheetManager_services.DTOs.User;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpPost]
    public IActionResult Create(CreateUserDto dto)
    {
        return Ok(_service.Create(dto));
    }

    // 🔥 UPDATE USER
[Authorize(Roles = "Admin")]
[HttpPut("{id}")]
public IActionResult Update(int id, UpdateUserDto dto)
{
    return Ok(_service.Update(id, dto));
}

// 🔥 DELETE USER
[Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    _service.Delete(id);
    return Ok("User deleted");
}
}