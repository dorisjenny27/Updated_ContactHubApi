using AutoMapper;
using ContactHub.Helpers;
using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using ContactHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ContactHub.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserToReturnDTO> UserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserToReturnDTO>(user);
        }

        public IEnumerable<UserToReturnDTO> GetAllUsers(int page, int perPage)
        {
            var users = _userManager.Users;
            var usersToReturnList = users.Select(user => _mapper.Map<UserToReturnDTO>(user)).ToList();
            return UtilityMethods.Paginate <UserToReturnDTO>(usersToReturnList, page, perPage);

        }

        public async Task<UserToReturnDTO> UpdateUserAsync(string id, UserUpdateDTO model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ArgumentException($"No record found for user with Id: {id}");

            _mapper.Map<UserUpdateDTO, User>(model, user);

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errList = string.Join(",\n", updateResult.Errors.Select(err => err.Description));
                throw new InvalidOperationException($"Failed to update user: {errList}");
            }

            return _mapper.Map<UserToReturnDTO>(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ArgumentException($"No record found for user with Id: {id}");

            var delResult = await _userManager.DeleteAsync(user);
            if (!delResult.Succeeded)
            {
                var errList = string.Join(",\n", delResult.Errors.Select(err => err.Description));
                throw new InvalidOperationException($"Failed to delete user: {errList}");
            }
        }

        public IEnumerable<UserToReturnDTO> SearchUsers(int page, int perPage, string searchTerm)
        {
            var users = _userManager.Users.Where(x =>
                x.Email.Contains(searchTerm) || x.UserName.Contains(searchTerm) || x.Id == searchTerm);

            var usersToReturnList = users.Select(user => _mapper.Map<UserToReturnDTO>(user)).ToList();
            return UtilityMethods.Paginate(usersToReturnList, page, perPage);
        }


    }
}
