namespace Models.Pagination.Sorting;

/// <summary>
/// Сортировка для покупателей.
/// </summary>
public enum CustomerSorting
{
    /// <summary>
    /// Сортировка по электронной почте по возрастанию.
    /// </summary>
    EmailAsc,

    /// <summary>
    /// Сортировка по электронной почте по убыванию.
    /// </summary>
    EmailDesc,

    /// <summary>
    /// Сортировка по номеру телефона по возрастанию.
    /// </summary>
    PhoneAsc,

    /// <summary>
    /// Сортировка по номеру телефона по убыванию.
    /// </summary>
    PhoneDesc,

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
