namespace Domain.Models.Response
{
    public class Meta
    {
        public int TotalRecords { get; set; }
        public int? TotalPages { get; set; }

        public Meta(int count, int? pageCount = null)
        {
            TotalRecords = count;
            TotalPages = pageCount;
        }
    }
}
