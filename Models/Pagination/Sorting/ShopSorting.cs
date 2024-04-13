namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для магазинов.
/// </summary>
public enum ShopSorting
{
    /// <summary>
    /// Сортировка по названию по возрастанию.
    /// </summary>
    NameAsc,

    /// <summary>
    /// Сортировка по названию по убыванию.
    /// </summary>
    NameDesc,

    /// <summary>
    /// Сортировка по адресу по возрастанию.
    /// </summary>
    AddressAsc,

    /// <summary>
    /// Сортировка по адресу по убыванию.
    /// </summary>
    AddressDesc
}
