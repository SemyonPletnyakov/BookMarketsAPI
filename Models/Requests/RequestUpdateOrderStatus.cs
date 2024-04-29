using Models.FullEntities;
using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на обновление статуса заказа.
/// </summary>
public record RequestUpdateOrderStatus : RequestBase
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Id<Order> Id { get; }

    /// <summary>
    /// Статус.
    /// </summary>
    public OrderStatus Status { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestUpdateOrderStatus"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор.
    /// </param>
    /// <param name="status">
    /// Статус.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="status"/> равен <see langword="null"/>.
    /// </exception>
    public RequestUpdateOrderStatus(Id<Order> id, OrderStatus status)
        : base(
            new OperationDescriprion(
                OperationType.Update,
                EntityType.Order))
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Status = status;
    }
}
