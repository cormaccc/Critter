namespace CritterWebApi.Data.Models
{
    public class HttpCookie
    {
        public string Name { get; set; }
        public string TokenString { get; set; }
        public CookieOptions CookieOptions { get; set; }
    }
}
