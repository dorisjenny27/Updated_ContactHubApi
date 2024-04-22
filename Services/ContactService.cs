using AutoMapper;
using ContactHub.Mappers;
using ContactHub.Model;
using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using Microsoft.AspNetCore.Identity;

namespace ContactHub.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _autoMapper;
        private readonly UserManager<User> _userManager;        
        public ContactService(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public UserToAddDTO CreateUser(UserToAddDTO newUser)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string Id)
        {
            throw new NotImplementedException();
        }

        public List<UserToAddDTO> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserToAddDTO GetUserById(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public UserToAddDTO UpdateUser(UserToAddDTO updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
