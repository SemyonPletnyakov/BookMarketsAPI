using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

internal static class WarehousesWithPaginationGetter
{
    /// <summary>
    /// Применить правила пагинации.
    /// </summary>
    /// <param name="queryable">
    /// Запрос к БД.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <returns>
    /// Запрос с правилами пагинации.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Если тип сортировки не поддерживается.
    /// </exception>
    public static IQueryable<Models.Warehouse> WithPaginationInfo(
        this IQueryable<Models.Warehouse> queryable,
        PaginationInfo<WarehouseSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            WarehouseSorting.NameAsc =>
                queryable.OrderBy(q => q.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            WarehouseSorting.NameDesc =>
                queryable.OrderByDescending(q => q.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            WarehouseSorting.AddressAsc =>
                queryable.OrderBy(q => q.Address.Country)
                    .ThenBy(q => q.Address.RegionName)
                    .ThenBy(q => q.Address.City)
                    .ThenBy(q => q.Address.District)
                    .ThenBy(q => q.Address.Street)
                    .ThenBy(q => q.Address.House)
                    .ThenBy(q => q.Address.Room)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            WarehouseSorting.AddressDesc =>
                queryable.OrderByDescending(q => q.Address.Country)
                    .ThenBy(q => q.Address.RegionName)
                    .ThenBy(q => q.Address.City)
                    .ThenBy(q => q.Address.District)
                    .ThenBy(q => q.Address.Street)
                    .ThenBy(q => q.Address.House)
                    .ThenBy(q => q.Address.Room)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
