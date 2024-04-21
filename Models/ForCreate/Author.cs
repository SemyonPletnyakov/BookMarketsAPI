namespace Models.ForCreate;

public sealed class Author
{
    /// <summary>
    /// Полное имя.
    /// </summary>
    public FullName FullName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Cтраны, к которым относится автор.
    /// </summary>
    public ICollection<string>? Countries { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Author"/>.
    /// </summary>
    /// <param name="fullName">
    /// Полное имя.
    /// </param>
    /// <param name="birthDate">
    /// Дата рождения.
    /// </param>
    /// <param name="countries">
    /// Страны, к которым относится автор.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="fullName"/> 
    /// равен <see langword="null"/>.
    /// </exception>
    public Author(
        FullName fullName, 
        DateOnly? birthDate, 
        ICollection<string>? countries)
    {
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        BirthDate = birthDate;
        Countries = countries;
    }
}
