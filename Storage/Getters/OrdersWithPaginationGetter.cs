using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

internal static class OrdersWithPaginationGetter
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
    public static IQueryable<Models.Order> WithPaginationInfo(
        this IQueryable<Models.Order> queryable,
        PaginationInfo<OrderSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            OrderSorting.CustomerFullNameAsc =>
                queryable.OrderBy(q => q.Customer.LastName)
                    .ThenBy(q => q.Customer.FirstName)
                    .ThenBy(q => q.Customer.Patronymic)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            OrderSorting.CustomerFullNameDesc =>
                queryable.OrderByDescending(q => q.Customer.LastName)
                    .ThenBy(q => q.Customer.FirstName)
                    .ThenBy(q => q.Customer.Patronymic)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            OrderSorting.ShopNameAsc =>
                queryable.OrderBy(q => q.Shop.Name)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            OrderSorting.ShopNameDesc =>
                queryable.OrderByDescending(q => q.Shop.Name)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            OrderSorting.DateAsc =>
                queryable.OrderBy(q => q.DateTime)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            OrderSorting.DateDesc =>
                queryable.OrderByDescending(q => q.DateTime)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
