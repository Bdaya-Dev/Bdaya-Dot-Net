
using Bdaya.Responses;
using System.Linq;

namespace Microsoft.EntityFrameworkCore;
public static class PaginatedList
{
    public static async Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        var count = await queryable.CountAsync();
        var result = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<TDestination>(pageIndex: pageNumber, pageSize: pageSize, totalCount: count, page: result);
    }
}
