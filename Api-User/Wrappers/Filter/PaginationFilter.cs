namespace Api_User.Wrappers.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1: pageNumber;
            this.PageSize = pageSize >10 ? 10 : pageSize;
        }
    }

    public class PaginationFilterClient
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilterClient()
        {
            this.PageNumber = 1;
            this.PageSize = 12;
        }
        public PaginationFilterClient(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 12 ? 12 : pageSize;
        }
    }
}
