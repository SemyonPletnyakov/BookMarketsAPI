using Models;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;

using BookForUpdate = Models.ForUpdate.Book;
using SimpleBook = Models.SimpleEntities.Book;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория книг.
/// </summary>
public interface IBooksRepository
{
    /// <summary>
    /// Получить часть книг в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка книг.
    /// </returns>
    public Task<IList<SimpleBook>> GetBooksAsync(
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск книг по совпадению названия в соотвествии с пагинацией.
    /// </summary>
    /// <param name="name">
    /// Название.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка книг.
    /// </returns>
    public Task<IList<SimpleBook>> GetBooksByNameAsync(
        Name<Product> name,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск книг по автору в соотвествии с пагинацией.
    /// </summary>
    /// <param name="authorId">
    /// Идендификатор автора.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка книг.
    /// </returns>
    public Task<IList<SimpleBook>> GetBooksByAuthorAsync(
        Id<Author> authorId,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск книг по ключевыми словам в соотвествии с пагинацией.
    /// </summary>
    /// <param name="keyWords">
    /// Ключевые слова.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка книг.
    /// </returns>
    public Task<IList<SimpleBook>> GetBooksByKeyWordsOrderingByNameAsync(
        IReadOnlyCollection<string> keyWords,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Получить книгу по идентификатору.
    /// </summary>
    /// <param name="productId">
    /// Идендификатор товара.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка книг.
    /// </returns>
    public Task<Book> GetBooksByProductIdAsync(
        Id<Product> productId,
        CancellationToken token);

    /// <summary>
    /// Перевод товара в категорию книги.
    /// </summary>
    /// <param name="productId">
    /// Идендификатор книги.
    /// </param>
    /// <param name="authorId">
    /// Идендификатор автора.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddProductInBooksAsync(
        Id<Product> productId,
        Id<Author>? authorId,
        CancellationToken token);

    /// <summary>
    /// Обновить автора книги.
    /// </summary>
    /// <param name="productId">
    /// Идендификатор книги.
    /// </param>
    /// <param name="authorId">
    /// Идендификатор автора.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateAuthorForBookAsync(
        Id<Book> productId, 
        Id<Author> authorId, 
        CancellationToken token);

    /// <summary>
    /// Перевод книги в категорию обычного товара.
    /// </summary>
    /// <param name="productId">
    /// Книга.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task RemoveProductFromBooksAsync(
        Id<Product> productId, 
        CancellationToken token);
}
