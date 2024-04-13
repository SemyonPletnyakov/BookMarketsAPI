namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для авторов.
/// </summary>
public enum AuthorSorting
{
    /// <summary>
    /// Сортировка по ФИО по возрастанию.
    /// </summary>
    FullNameAsc,

    /// <summary>
    /// Сортировка по ФИО по убыванию.
    /// </summary>
    FullNameDesc,

    /// <summary>
    /// Сортировка по дате рожления по возрастанию.
    /// </summary>
    BirthDateAsc,

    /// <summary>
    /// Сортировка по дате рожления по убыванию.
    /// </summary>
    BirthDateDesc
}
