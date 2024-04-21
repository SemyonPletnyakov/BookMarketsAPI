using Models.Pagination;

namespace Models.Requests;

/// <summary>
/// Запрос на поиск по ключевым словам с учётом пагинации.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyByKeyWordsWithPagination<T> : RequestBase
    where T : Enum
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    public IReadOnlyCollection<string> KeyWords { get; }

    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<T> PaginationInfo { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestGetManyByKeyWordsWithPagination{T}"/>.
    /// </summary>
    /// <param name="keyWords">
    /// Фамилия.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Если <paramref name="keyWords"/> пустой.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestGetManyByKeyWordsWithPagination(
        IReadOnlyCollection<string> keyWords,
        PaginationInfo<T> paginationInfo)
    {
        ArgumentNullException.ThrowIfNull(keyWords);

        if (!keyWords.Any())
        {
            throw new ArgumentException($"{keyWords} не должен быть пустым");
        }

        KeyWords = keyWords;

        PaginationInfo = paginationInfo
            ?? throw new ArgumentNullException(nameof(paginationInfo));
    }
}
