
using Bdaya.Responses;

namespace MongoDB.Driver.Linq;
public static class PaginatedListExt
{
    public static async Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IMongoQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        if (pageSize == 0)
        {
            throw new ArgumentException("Page size should be > 0", nameof(pageSize));
        }
        var count = await queryable.CountAsync();
        var result = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<TDestination>(pageIndex: pageNumber, pageSize: pageSize, totalCount: count, page: result);
    }
}
