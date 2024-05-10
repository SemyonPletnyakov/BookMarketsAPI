using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

internal static class ProductsWithPaginationGetter
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
    public static IQueryable<Models.Product> WithPaginationInfo(
        this IQueryable<Models.Product> queryable,
        PaginationInfo<ProductSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            ProductSorting.NameAsc =>
                queryable.OrderBy(q => q.Name)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            ProductSorting.NameDesc =>
                queryable.OrderByDescending(q => q.Name)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            ProductSorting.PriceAsc =>
                queryable.OrderBy(q => q.Price)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            ProductSorting.PriceDesc =>
                queryable.OrderByDescending(q => q.Price)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
