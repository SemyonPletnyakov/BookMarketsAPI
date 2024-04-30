using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на обновление сущности.
/// </summary>
/// <typeparam name="T">
/// Тип обновляемой сущности.
/// </typeparam>
public record RequestUpdateEntity<T> : RequestBase
    where T : class
{
    /// <summary>
    /// Сущность, которая должна быть обновлена.
    /// </summary>
    public T Entity { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestUpdateEntity{T}"/>.
    /// </summary>
    /// <param name="entity">
    /// Сущность, которая должна быть обновлена.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="entity"/> равен <see langword="null"/>.
    /// </exception>
    public RequestUpdateEntity(T entity)
        : base(
            new OperationDescriptionWithTargetEntity<T>(
                OperationType.Update,
                GetEntityTypeByEntity<T>(),
                entity))
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }
}
