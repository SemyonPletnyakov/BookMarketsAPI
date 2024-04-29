using Models.Pagination;
using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос поиск временному интервалу с учётом пагинации.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyByTimeIntervalWithPagination<T> : RequestBase
    where T : Enum
{
    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<T> PaginationInfo { get; }

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
    /// <see cref="RequestGetManyByTimeIntervalWithPagination{T}"/>.
    /// </summary>
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
    /// Если <paramref name="paginationInfo"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetManyByTimeIntervalWithPagination(
        PaginationInfo<T> paginationInfo,
        DateTime startDate,
        DateTime endDate)
        : base(
            new OperationDescriprion(
                OperationType.Get,
                GetEntityTypeBySortingType<T>()))
    {
        PaginationInfo = paginationInfo
            ?? throw new ArgumentNullException(nameof(paginationInfo));

        StartDate = startDate;
        EndDate = endDate;
    }
}
