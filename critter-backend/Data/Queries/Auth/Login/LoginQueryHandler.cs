using Dapper;
using MediatR;
using TwitterCloneApp.Contexts;

namespace TwitterCloneApp.Data.Queries.Auth.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, long>
    {
        private readonly DapperContext _context;

        public LoginQueryHandler(DapperContext context)
        {
            _context = context;
        }
        public async Task<long> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var query = @"
                SELECT u.Id as UserId FROM Users u
                WHERE u.Email = @email AND password = @password
            ";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("email", request.Email);
            dynamicParameters.Add("password", request.Password);

            using var conn = _context.CreateSQLiteConnnection();

            var result = await conn.QueryFirstOrDefaultAsync<long>(query, dynamicParameters);

            return result;
        }
    }

}
