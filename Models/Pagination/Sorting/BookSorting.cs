namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для книг.
/// </summary>
public enum BookSorting
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
    /// Сортировка по ФИО автора по возрастанию.
    /// </summary>
    AuthorFullNameAsc,

    /// <summary>
    /// Сортировка по ФИО автора по убыванию.
    /// </summary>
    AuthorFullNameDesc
}
