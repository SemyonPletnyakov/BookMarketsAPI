using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

public static class EmployeesWithPaginationGetter
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
    public static IQueryable<Models.Employee> WithPaginationInfo(
        this IQueryable<Models.Employee> queryable,
        PaginationInfo<EmployeeSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            EmployeeSorting.EmailAsc =>
                queryable.OrderBy(q => q.Email)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.EmailDesc =>
                queryable.OrderByDescending(q => q.Email)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.PhoneAsc =>
                queryable.OrderBy(q => q.Phone)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.PhoneDesc =>
                queryable.OrderByDescending(q => q.Phone)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.BirthDateAsc =>
                queryable.OrderBy(q => q.BirthDate)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.BirthDateDesc =>
                queryable.OrderByDescending(q => q.BirthDate)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.FullNameAsc =>
                queryable.OrderBy(q => q.LastName)
                    .ThenBy(q => q.FirstName)
                    .ThenBy(q => q.Patronymic)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            EmployeeSorting.FullNameDesc =>
                queryable.OrderByDescending(q => q.LastName)
                    .ThenBy(q => q.FirstName)
                    .ThenBy(q => q.Patronymic)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
