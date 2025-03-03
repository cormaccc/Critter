using MediatR;
using CritterWebApi.Data.Outputs;

namespace CritterWebApi.Data.Queries.User.GetUser
{
    public class GetUserQuery : IRequest<UserOutputDto>
    {
        public long UserId { get; set; }
    }
}
