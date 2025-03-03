using MediatR;
using CritterWebApi.Data.Inputs.User;

namespace CritterWebApi.Data.Commands.User.Create
{
    public class CreateUserCommand : IRequest<long>
    {
        public UserCreateInputDto UserInfo { get; set; }
    }
}
