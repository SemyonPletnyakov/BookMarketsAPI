using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using SimpleCustomer = Models.SimpleEntities.Customer;
using CustomerWothoutId = Models.ForCreate.Customer;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория покупателей.
/// </summary>
public interface ICustomersRepository
{
    /// <summary>
    /// Получить часть покупателей в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка покупателей.
    /// </returns>
    public Task<IList<SimpleCustomer>> GetCustomersAsync(
        PaginationInfo<CustomerSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск покупателей по фамилии в соотвествии с пагинацией.
    /// </summary>
    /// <param name="lastName">
    /// Фамилия.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка покупателей.
    /// </returns>
    public Task<IList<SimpleCustomer>> GetCustomersByLastNameAsync(
        LastName lastName,
        PaginationInfo<CustomerSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Получить часть покупателей, покупавших что-то в магазине
    /// в промежуток дат в соотвествии с пагинацией.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="startDate">
    /// Начало временного интервала.
    /// </param>
    /// <param name="endDate">
    /// Конец временного интервала.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка покупателей.
    /// </returns>
    public Task<IList<SimpleCustomer>> GetCustomersByShopIdByTimeIntervalAsync(
        Id<Shop> shopId,
        DateOnly startDate,
        DateOnly endDate,
        PaginationInfo<CustomerSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Дать всю инофрмацию о покупателе по электронной почте.
    /// </summary>
    /// <param name="email">
    /// Электронная почта.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Полная информация о покупателе.
    /// </returns>
    public Task<Customer> GetCustomerByEmailAsync(
        Email email,
        CancellationToken token);

    /// <summary>
    /// Дать инофрмацию о покупателе по идентификатору.
    /// </summary>
    /// <param name="customerId">
    /// Идентификатор сотрудника.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Информация о покупателе.
    /// </returns>
    public Task<SimpleCustomer> GetCustomerByIdAsync(
        Id<Customer> customerId,
        CancellationToken token);

    /// <summary>
    /// Добавление покупателя.
    /// </summary>
    /// <param name="customer">
    /// Покупатель.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddCustomerAsync(
        CustomerWothoutId customer, 
        CancellationToken token);

    /// <summary>
    /// Изменение покупателя.
    /// </summary>
    /// <param name="customer">
    /// Покупатель.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateCustomerAsync(Customer customer, CancellationToken token);
}
