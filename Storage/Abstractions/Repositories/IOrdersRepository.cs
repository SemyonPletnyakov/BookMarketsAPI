using Models;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;

using OrderWithoutId = Models.ForCreate.Order;
using OrderSimple = Models.ForUpdate.Order;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория заказов.
/// </summary>
public interface IOrdersRepository
{
    /// <summary>
    /// Получить часть заказов в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка заказов.
    /// </returns>
    public Task<IList<OrderSimple>> GetOrdersAsync(
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Получить часть заказов за период в соотвествии с пагинацией.
    /// </summary>
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
    /// Часть списка заказов.
    /// </returns>
    public Task<IList<OrderSimple>> GetOrdersByTimeIntervalAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск заказов по идентификатору магазина в соотвествии с пагинацией.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка заказов.
    /// </returns>
    public Task<IList<OrderSimple>> GetOrdersByShopIdAsync(
        Id<Shop> shopId,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск заказов за период по идентификатору магазина в соотвествии с пагинацией.
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
    /// Часть списка заказов.
    /// </returns>
    public Task<IList<OrderSimple>> GetOrdersByShopIdByTimeIntervalAsync(
        Id<Shop> shopId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск заказов по идентификатору покупателя в соотвествии с пагинацией.
    /// </summary>
    /// <param name="customerId">
    /// Идентификатор покупателя.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка заказов.
    /// </returns>
    public Task<IList<OrderSimple>> GetOrdersByCustomerIdAsync(
        Id<Customer> customerId,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск заказов за период по идентификатору покупателя в соотвествии с пагинацией.
    /// </summary>
    /// <param name="customerId">
    /// Идентификатор покупателя.
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
    /// Часть списка заказов.
    /// </returns>
    public Task<IList<OrderSimple>> GetOrdersByCustomerIdByTimeIntervalAsync(
        Id<Customer> customerId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск заказа по идентификатору заказа в соотвествии с пагинацией.
    /// </summary>
    /// <param name="OrderId">
    /// Идентификатор заказа.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Заказ.
    /// </returns>
    public Task<OrderSimple> GetOrderByOrderIdAsync(
        Id<Order> OrderId,
        CancellationToken token);

    /// <summary>
    /// Добавление заказа.
    /// </summary>
    /// <param name="order">
    /// Заказы.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddOrderAsync(OrderWithoutId order, CancellationToken token);

    /// <summary>
    /// Изменение статуса заказа.
    /// </summary>
    /// <param name="orderId">
    /// Идентификатор заказа.
    /// </param>
    /// <param name="orderStatus">
    /// Статус заказа.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateOrderStatusAsync(
        Id<Order> orderId, 
        OrderStatus orderStatus, 
        CancellationToken token);
}
