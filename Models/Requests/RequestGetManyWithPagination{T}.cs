using Models.Pagination;
using Models.Pagination.Sorting;
using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на получение значений с учётом пагинации.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyWithPagination<T> : RequestBase
    where T : Enum
{
    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<T> PaginationInfo { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestGetManyWithPagination{T}"/>.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="paginationInfo"/> равено <see langword="null"/>.
    /// </exception>
    public RequestGetManyWithPagination(PaginationInfo<T> paginationInfo)
        : base(
            new OperationDescriprion(
                OperationType.Get,
                GetEntityTypeBySortingType<T>()))
    {
        PaginationInfo = paginationInfo 
            ?? throw new ArgumentNullException(nameof(paginationInfo));
    }    
}
