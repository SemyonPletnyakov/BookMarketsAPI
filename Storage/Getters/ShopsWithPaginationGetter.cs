using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

internal static class ShopsWithPaginationGetter
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
    public static IQueryable<Models.Shop> WithPaginationInfo(
        this IQueryable<Models.Shop> queryable,
        PaginationInfo<ShopSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            ShopSorting.NameAsc =>
                queryable.OrderBy(q => q.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ShopSorting.NameDesc =>
                queryable.OrderByDescending(q => q.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ShopSorting.AddressAsc =>
                queryable.OrderBy(q => q.Address.Country)
                    .ThenBy(q => q.Address.RegionName)
                    .ThenBy(q => q.Address.City)
                    .ThenBy(q => q.Address.District)
                    .ThenBy(q => q.Address.Street)
                    .ThenBy(q => q.Address.House)
                    .ThenBy(q => q.Address.Room)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ShopSorting.AddressDesc =>
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
