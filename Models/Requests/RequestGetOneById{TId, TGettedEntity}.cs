using System.Security.Cryptography;
using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на получение объекта по идентификатору.
/// </summary>
/// <typeparam name="TId">
/// Тип, по идентификатору которого будет происходить поиск.
/// </typeparam>
/// <typeparam name="TGettedEntity">
/// Получаемая сущность.
/// </typeparam>
public record RequestGetOneById<TId, TGettedEntity> : RequestBase
{
    /// <summary>
    /// Идендификатор.
    /// </summary>
    public Id<TId> Id { get; }

    /// <summary>
    /// Создаёт объект типа <see cref="RequestGetOneById{T}"/>
    /// </summary>
    /// <param name="id">
    /// Идендификатор.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="id"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetOneById(Id<TId> id)
        : base(
            new OperationDescriptionWithTargetEntity<Id<TId>>(
                OperationType.Get,
                GetEntityTypeByEntity<TGettedEntity>(),
                id))
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}
