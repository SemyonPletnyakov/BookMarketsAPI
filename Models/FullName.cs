namespace Models;

/// <summary>
/// Сущность, представляющая данные о ФИО.
/// </summary>
public sealed record FullName
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; }

    /// <summary>
    /// Создаёт объект <see cref="FullName"/>.
    /// </summary>
    /// <param name="lastName"> 
    /// Фамилия. 
    /// </param>
    /// <param name="firstName">
    /// Имя.
    /// </param>
    /// <param name="patronymic">
    /// Отчество.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="lastName"/> 
    /// равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="lastName"/> или <paramref name="firstName"/> 
    /// пустой или состоит из пробелов.
    /// </exception>
    public FullName(string lastName, string firstName, string? patronymic)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));

        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
    }   
}
