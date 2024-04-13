namespace Storage.Models;

/// <summary>
/// Автор.
/// </summary>
public sealed class Author
{
    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Страны, к которым относится автор.
    /// </summary>
    public List<string>? Countries { get; set; }
}
