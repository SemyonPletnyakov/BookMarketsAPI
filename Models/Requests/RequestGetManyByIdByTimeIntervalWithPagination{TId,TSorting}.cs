using Models.Pagination;

namespace Models.Requests;

/// <summary>
/// Запрос поиск по идентификатору и временному интервалу с учётом пагинации.
/// </summary>
/// <typeparam name="TId">
/// Тип, по идендификатору которого будет происходить поиск.
/// </typeparam>
/// <typeparam name="TSorting">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyByIdByTimeIntervalWithPagination<TId, TSorting> : RequestBase
    where TSorting : Enum
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Id<TId> Id { get; }

    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<TSorting> PaginationInfo { get; }

    /// <summary>
    /// Начало временного интервала.
    /// </summary>
    public DateTimeOffset StartDate { get; }

    /// <summary>
    /// Конец временного интервала.
    /// </summary>
    public DateTimeOffset EndDate { get; }

    /// <summary>
    /// Создаёт объект 
    /// <see cref="RequestGetManyByIdByTimeIntervalWithPagination{TId, TSorting}"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="startDate">
    /// Начало временного интервала.
    /// </param>
    /// <param name="endDate">
    /// Конец временного интервала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="id"/> или <paramref name="paginationInfo"/>
    /// равен <see langword="null"/>.
    /// </exception>
    public RequestGetManyByIdByTimeIntervalWithPagination(
        Id<TId> id, 
        PaginationInfo<TSorting> paginationInfo,
        DateTime startDate,
        DateTime endDate)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));

        PaginationInfo = paginationInfo
            ?? throw new ArgumentNullException(nameof(paginationInfo));

        StartDate = startDate;
        EndDate = endDate;
    }
}
