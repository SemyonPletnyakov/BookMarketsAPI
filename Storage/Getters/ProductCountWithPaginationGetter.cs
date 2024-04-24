using Models.Pagination.Sorting;
using Models.Pagination;

namespace Storage.Getters;

internal static class ProductCountWithPaginationGetter
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
    public static IQueryable<Models.ProductsInWarehouse> WithPaginationInfo(
        this IQueryable<Models.ProductsInWarehouse> queryable,
        PaginationInfo<ProductCountSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            ProductCountSorting.NameAsc =>
                queryable.OrderBy(q => q.Product.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.NameDesc =>
                queryable.OrderByDescending(q => q.Product.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.PriceAsc =>
                queryable.OrderBy(q => q.Product.Price)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.PriceDesc =>
                queryable.OrderByDescending(q => q.Product.Price)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.CountAsc =>
                queryable.OrderBy(q => q.Count)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.CountDesc =>
                queryable.OrderByDescending(q => q.Count)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };

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
    public static IQueryable<Models.ProductsInShop> WhithPaginationInfo(
        this IQueryable<Models.ProductsInShop> queryable,
        PaginationInfo<ProductCountSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            ProductCountSorting.NameAsc =>
                queryable.OrderBy(q => q.Product.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.NameDesc =>
                queryable.OrderByDescending(q => q.Product.Name)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.PriceAsc =>
                queryable.OrderBy(q => q.Product.Price)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.PriceDesc =>
                queryable.OrderByDescending(q => q.Product.Price)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.CountAsc =>
                queryable.OrderBy(q => q.Count)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            ProductCountSorting.CountDesc =>
                queryable.OrderByDescending(q => q.Count)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
