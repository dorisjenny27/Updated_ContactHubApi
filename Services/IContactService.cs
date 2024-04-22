using ContactHub.Model;
using ContactHub.Model.DTOs;

namespace ContactHub.Services
{
    public interface IContactService
    {
        Task<bool> RegisterUser(LoginUser user);
        UserToAddDTO CreateUser(UserToAddDTO newUser);
        bool DeleteUser(string Id);
        List<UserToAddDTO> GetAllUsers();
        UserToAddDTO GetUserById(string Id);
        UserToAddDTO UpdateUser(UserToAddDTO updateUser);

    }
}