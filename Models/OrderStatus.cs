namespace Models;

/// <summary>
/// Статус заказа.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Обрабатывается.
    /// </summary>
    InProcess,

    /// <summary>
    /// В пути.
    /// </summary>
    OnTheWay,

    /// <summary>
    /// Готов к выдаче.
    /// </summary>
    IsReadyForDelivery,

    /// <summary>
    /// Завершён.
    /// </summary>
    Completed,

    /// <summary>
    /// Отменён.
    /// </summary>
    Canceled
}
