using MediatR;
using TwitterCloneApp.Data.Outputs;

namespace TwitterCloneApp.Data.Queries.User.GetUser
{
    public class GetUserQuery : IRequest<UserOutputDto>
    {
        public long UserId { get; set; }
    }
}
