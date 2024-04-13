namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для склада.
/// </summary>
public enum WarehouseSorting
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
