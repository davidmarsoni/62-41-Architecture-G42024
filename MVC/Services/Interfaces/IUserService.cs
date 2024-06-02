﻿using DTO;

namespace MVC.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO?> GetUserById(int id);
        public Task<IEnumerable<UserDTO>?> GetAllUsers();
        public Task<IEnumerable<UserDTO>?> GetUsersByGroupId(int groupId);
        public Task<IEnumerable<UserDTO>?> GetAllUsersActiveWithoutAccount();
        public Task<IEnumerable<UserDTO>?> GetAllUsersActiveWithAccount();
        public Task<UserDTO?> CreateUser(UserDTO accountDTO);
        public Task<Boolean> UpdateUser(UserDTO accountDTO);
        public Task<Boolean> DeleteUser(int id);
        
    }
}
