using TwitterCloneApp.Data.Inputs.User;

namespace TwitterCloneApp.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<long> CreateUser(UserCreateInputDto userInfo);
        public Task EditUser(UserCreateInputDto userInfo);
    }
}
