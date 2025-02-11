using Dapper;
using MediatR;
using TwitterCloneApp.Contexts;
using TwitterCloneApp.Data.Outputs;

namespace TwitterCloneApp.Data.Queries.User.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserOutputDto>
    {
        private readonly DapperContext _dapperContext;

        public GetUserQueryHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<UserOutputDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var query = @"
                SELECT 
                    u.Id AS UserId,
                    u.FirstName AS FirstName,
                    u.LastName AS LastName, 
                    u.Username AS UserName
                FROM Users u
                WHERE u.Id = @userId
            ";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("userId", request.UserId);

            using var conn = _dapperContext.CreateSQLiteConnnection();
            var result = await conn.QueryFirstOrDefaultAsync<UserOutputDto>(query, parameters);

            return result;
        }
    }
}
