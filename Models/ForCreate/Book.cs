﻿namespace Models.ForCreate;

/// <summary>
/// Книга.
/// </summary>
public sealed class Book : Product
{
    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public Id<FullEntities.Author>? AuthorId { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Book"/>
    /// </summary>
    /// <param name="name">
    /// Название товара.
    /// </param>
    /// <param name="description">
    /// Описание.
    /// </param>
    /// <param name="price">
    /// Цена.
    /// </param>
    /// <param name="keyWords">
    /// Ключевые слова, применимые к книге.
    /// </param>
    /// <param name="authorId">
    /// Идентификатор автора.
    /// </param>
    public Book(
        Name<Product> name,
        Description? description,
        Price price,
        ICollection<string>? keyWords,
        Id<FullEntities.Author>? authorId)
        : base(
            name,
            description,
            price,
            keyWords)
    {
        AuthorId = authorId;
    }
}

