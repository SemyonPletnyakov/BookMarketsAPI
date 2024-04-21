using Models;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;

using BookWithoutId = Models.ForCreate.Book;
using SimpleBook = Models.ForCreate.Book;

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
    public Task<IList<Book>> GetBooksAsync(
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск книг по совпадению названия в соотвествии с пагинацией.
    /// </summary>
    /// <param name="Name">
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
    public Task<IList<Book>> GetBooksByNameAsync(
        Name<Product> Name,
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
    public Task<IList<Book>> GetBooksByKeyWordsOrderingByNameAsync(
        IReadOnlyCollection<string> keyWords,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление книги.
    /// </summary>
    /// <param name="book">
    /// Книга.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddBookAsync(BookWithoutId book, CancellationToken token);

    /// <summary>
    /// Изменение книги.
    /// </summary>
    /// <param name="book">
    /// Книга.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateBookAsync(SimpleBook book, CancellationToken token);

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
    public Task RemoveProductFromBooksAsync(Id<Product> productId, CancellationToken token);

    /// <summary>
    /// Перевод товара в категорию книги.
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
    public Task AddProductInBooksAsync(Id<Product> productId, CancellationToken token);
}
