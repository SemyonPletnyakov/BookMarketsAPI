using Models;

namespace Transport.Models.ForUpdate;

/// <summary>
/// Заказ.
/// </summary>
public sealed class OrderIdAndStatus
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    public OrderStatus OrderStatus { get; set; }
}
