using MediatR;
using CritterWebApi.Data.Repositories.UserRepository;

namespace CritterWebApi.Data.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.CreateUser(request.UserInfo);
        }
    }
}
