using MediatR;
using TwitterCloneApp.Data.Inputs.User;

namespace TwitterCloneApp.Data.Commands.User.Create
{
    public class CreateUserCommand : IRequest<long>
    {
        public UserCreateInputDto UserInfo { get; set; }
    }
}
