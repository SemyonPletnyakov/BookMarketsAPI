namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для товаров.
/// </summary>
public enum ProductCountSorting
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
    /// Сортировка по цене по возрастанию.
    /// </summary>
    PriceAsc,

    /// <summary>
    /// Сортировка по цене по убыванию.
    /// </summary>
    PriceDesc,

    /// <summary>
    /// Сортировка по количеству по возрастанию.
    /// </summary>
    CountAsc,

    /// <summary>
    /// Сортировка по количеству по убыванию.
    /// </summary>
    CountDesc,
}
