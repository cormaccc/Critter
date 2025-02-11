namespace CritterWebApi.Data.Outputs.User
{
    public class UserLoginOutputDto
    {
        public long UserId { get; set; }
        public string PasswordHash { get; set; }
    }
}
