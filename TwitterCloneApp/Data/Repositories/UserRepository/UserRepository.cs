﻿using TwitterCloneApp.Contexts;
using TwitterCloneApp.Data.Inputs.User;
using TwitterCloneApp.Entities.User;

namespace TwitterCloneApp.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {

        private readonly TwitterCloneEFContext _context;

        public UserRepository(TwitterCloneEFContext context)
        {
            _context = context;
        }
        public async Task<long> CreateUser(UserCreateInputDto userInfo)
        {
            using var context = _context;

            var newUser = new UserEntity(userInfo.FirstName, userInfo.LastName, userInfo.Email, userInfo.Password, userInfo.Username);

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return newUser.Id;
        }

        public Task EditUser(UserCreateInputDto userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
