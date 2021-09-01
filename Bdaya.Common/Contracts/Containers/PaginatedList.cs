﻿namespace Bdaya.Responses;

public class PaginatedList<T>
{
    /// <summary>
    /// One-based Index
    /// </summary>
    public int PageIndex { get; init; }
    public List<T> Page { get; init; } = new();
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public PaginatedList()
    {
    }
    public PaginatedList(int pageIndex, int pageSize, int totalCount, List<T> page)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        Page = page;
    }

    public int TotalPages => TotalCount.CeilDiv(PageSize);

    public int PageIndexZeroBased => PageIndex - 1;
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
}
