namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для заказов.
/// </summary>
public enum OrderSorting
{
    /// <summary>
    /// Сортировка по ФИО покупателя по возрастанию.
    /// </summary>
    CustomerFullNameAsc,

    /// <summary>
    /// Сортировка по ФИО покупателя по убыванию.
    /// </summary>
    CustomerFullNameDesc,

    /// <summary>
    /// Сортировка по названию магазина по возрастанию.
    /// </summary>
    ShopNameAsc,

    /// <summary>
    /// Сортировка по названию магазина по убыванию.
    /// </summary>
    ShopFullNameDesc,

    /// <summary>
    /// Сортировка по дате заказа по возрастанию.
    /// </summary>
    DateAsc,

    /// <summary>
    /// Сортировка по дате заказа по убыванию.
    /// </summary>
    DateDesc,
}
