namespace Models.Requests.Operations.Types;

/// <summary>
/// Тип сущности.
/// </summary>
public enum EntityType
{
    /// <summary>
    /// Товар.
    /// </summary>
    Product,

    /// <summary>
    /// Книга.
    /// </summary>
    Book,

    /// <summary>
    /// Авторы.
    /// </summary>
    Author,

    /// <summary>
    /// Адрес.
    /// </summary>
    Address,

    /// <summary>
    /// Магазин.
    /// </summary>
    Shop,

    /// <summary>
    /// Склад.
    /// </summary>
    Warehouse,

    /// <summary>
    /// Число товара.
    /// </summary>
    ProductCount,

    /// <summary>
    /// Покупатель.
    /// </summary>
    Customer,

    /// <summary>
    /// Сотрудник.
    /// </summary>
    Employee,

    /// <summary>
    /// Заказ.
    /// </summary>
    Order
}
