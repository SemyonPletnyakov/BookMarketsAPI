namespace Transport.Models.ForCreate;

/// <summary>
/// Автор.
/// </summary>
public sealed class Author
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Cтраны, к которым относится автор.
    /// </summary>
    public ICollection<string>? Countries { get; set; }
}
