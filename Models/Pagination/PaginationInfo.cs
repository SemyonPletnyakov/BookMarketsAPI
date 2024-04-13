namespace Models.Pagination;

/// <summary>
/// Содержит информацию о текущей странице.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record PaginationInfo<T> where T : Enum
{
    /// <summary>
    /// Число элементов на странице.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Сортировка.
    /// </summary>
    public T OrderBy { get; }

    /// <summary>
    /// Создаёт объект <see cref="PaginationInfo{T}"/>.
    /// </summary>
    /// <param name="pageSize">
    /// Число элементов на странице.
    /// </param>
    /// <param name="pageNumber">
    /// Номер страницы.
    /// </param>
    /// <param name="orderBy">
    /// Параметр сортровка.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Если <paramref name="pageSize"/> или <paramref name="pageNumber"/>
    /// равен <see cref="null"/>.
    /// </exception>
    public PaginationInfo(int pageSize, int pageNumber, T orderBy)
    {
        if(pageSize < 1)
        {
            throw new ArgumentException($"{pageSize} не может быть меньше 1.");
        }

        PageSize = pageSize;

        if (pageNumber < 1)
        {
            throw new ArgumentException($"{pageNumber} не может быть меньше 1.");
        }

        PageNumber = pageNumber;
        OrderBy = orderBy;
    }
}
