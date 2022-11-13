namespace Bdaya.Responses.Linq;

public static class PaginatedListContainerExt
{
    public static PaginatedList<TTarget> Select<T, TTarget>(this PaginatedList<T> src, Func<T, TTarget> selector)
    {
        return new PaginatedList<TTarget>(pageIndex: src.PageIndex, pageSize: src.PageSize, page: src.Page.Select(selector).ToList(), totalCount: src.TotalCount);
    }
}