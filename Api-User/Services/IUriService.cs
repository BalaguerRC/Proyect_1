using Api_User.Wrappers.Filter;

namespace Api_User.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
