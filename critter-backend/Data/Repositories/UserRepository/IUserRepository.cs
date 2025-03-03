using CritterWebApi.Data.Inputs.User;

namespace CritterWebApi.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<long> CreateUser(UserCreateInputDto userInfo);
        public Task EditUser(UserCreateInputDto userInfo);
    }
}
