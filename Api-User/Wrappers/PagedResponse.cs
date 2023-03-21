using Api_User.Models;

namespace Api_User.Wrappers
{
    public class PagedResponse<T> : Pagination<T>
    {
       // private IEnumerable<Products> list;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirtstPage { get; set; }

        public Uri lastPage { get; set; }        
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data= data;
            this.Message = null;
            this.Succedd = true;
            this.Errors = null;
        }
    }
}
