using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ContactHub.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserToReturnDTO> UserByIdAsync(string id);
        IEnumerable<UserToReturnDTO> GetAllUsers(int page, int perPage);
        Task<UserToReturnDTO> UpdateUserAsync(string id, UserUpdateDTO model);
        Task DeleteUserAsync(string id);
        IEnumerable<UserToReturnDTO> SearchUsers(int page, int perPage, string searchTerm);
    }
}
