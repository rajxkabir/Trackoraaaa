using TimeSheetManager_services.Models;
using TimeSheetManager_services.DTOs.User;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User Create(CreateUserDto dto);
    User Update(int id, UpdateUserDto dto);
void Delete(int id);
}