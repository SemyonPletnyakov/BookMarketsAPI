namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для товаров.
/// </summary>
public enum ProductSorting
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
    PriceDesc
}
