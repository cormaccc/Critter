namespace CritterWebApi.Data.Inputs.Feed
{
    public class GetFeedInputDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
    }
}
