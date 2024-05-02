using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Logic.Abstractions.Processors;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using OrderWithoutId = Models.ForCreate.Order;
using SimleOrder = Models.ForUpdate.Order;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер заказов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="OrdersController"/>.
    /// </summary>
    /// <param name="getOrdersProcessor">
    /// Процессор получения заказов.
    /// </param>
    /// <param name="getOrdersByTimeIntervalProcessor">
    /// Процессор получения заказов по временному интервалу.
    /// </param>
    /// <param name="getOrdersByShopIdProcessor">
    /// Процессор получения заказов по идентификатору магазина.
    /// </param>
    /// <param name="getOrdersByShopIdAndTimeIntervalProcessor">
    /// Процессор получения заказов по идентификатору магазина и временному интервалу.
    /// </param>
    /// <param name="getOrdersByCustomerIdProcessor">
    /// Процессор получения заказов по идентификатору покупателя.
    /// </param>
    /// <param name="getOrdersByCustomerIdAndTimeIntervalProcessor">
    /// Процессор получения заказов по идентификатору покупателя и временному интервалу.
    /// </param>
    /// <param name="addOrderProcessor">
    /// Процессор добавления заказа.
    /// </param>
    /// <param name="updateOrderStatusProcessor">
    /// Процессор обновления статуса заказа.
    /// </param>
    /// <exception cref="ArgumentNullException"></exception>
    public OrdersController(
        IRequestProcessorWithAuthorize<RequestGetManyWithPagination<OrderSorting>, IList<SimleOrder>> getOrdersProcessor,
        IRequestProcessorWithAuthorize<RequestGetManyByTimeIntervalWithPagination<OrderSorting>, IList<SimleOrder>> getOrdersByTimeIntervalProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, OrderSorting>, IList<SimleOrder>> getOrdersByShopIdProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Shop, OrderSorting>, IList<SimleOrder>> getOrdersByShopIdAndTimeIntervalProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Customer, OrderSorting>, IList<SimleOrder>> getOrdersByCustomerIdProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Customer, OrderSorting>, IList<SimleOrder>> getOrdersByCustomerIdAndTimeIntervalProcessor, 
        IRequestProcessorWithAuthorize<RequestAddEntity<OrderWithoutId>> addOrderProcessor, IRequestProcessorWithAuthorize<RequestUpdateOrderStatus> updateOrderStatusProcessor)
    {
        _getOrdersProcessor = getOrdersProcessor 
            ?? throw new ArgumentNullException(nameof(getOrdersProcessor));
        _getOrdersByTimeIntervalProcessor = getOrdersByTimeIntervalProcessor 
            ?? throw new ArgumentNullException(nameof(getOrdersByTimeIntervalProcessor));
        _getOrdersByShopIdProcessor = getOrdersByShopIdProcessor 
            ?? throw new ArgumentNullException(nameof(getOrdersByShopIdProcessor));
        _getOrdersByShopIdAndTimeIntervalProcessor = getOrdersByShopIdAndTimeIntervalProcessor 
            ?? throw new ArgumentNullException(nameof(getOrdersByShopIdAndTimeIntervalProcessor));
        _getOrdersByCustomerIdProcessor = getOrdersByCustomerIdProcessor 
            ?? throw new ArgumentNullException(nameof(getOrdersByCustomerIdProcessor));
        _getOrdersByCustomerIdAndTimeIntervalProcessor = getOrdersByCustomerIdAndTimeIntervalProcessor 
            ?? throw new ArgumentNullException(nameof(getOrdersByCustomerIdAndTimeIntervalProcessor));
        _addOrderProcessor = addOrderProcessor 
            ?? throw new ArgumentNullException(nameof(addOrderProcessor));
        _updateOrderStatusProcessor = updateOrderStatusProcessor 
            ?? throw new ArgumentNullException(nameof(updateOrderStatusProcessor));
    }

    /// <summary>
    /// Получить заказы.
    /// </summary>
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Заказы.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetOrdersAsync(
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей временному интервалу.
    /// </summary>
    /// <param name="startDate">
    /// Начало временного интервала.
    /// </param>
    /// <param name="endDate">
    /// Конец временного интервала.
    /// </param>
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetOrdersByTimeIntervalAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей идентификатору магазина.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetOrdersByShopIdAsync(
        int shopId,
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей идентификатору магазина и временному интервалу.
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
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetOrdersByShopIdAndTimeIntervalAsync(
        int shopId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей идентификатору покупателя.
    /// </summary>
    /// <param name="customerId">
    /// Идентификатор покупателя.
    /// </param>
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetOrdersByCustomerIdAsync(
        int customerId,
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей идентификатору покупателя и временному интервалу.
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
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetOrdersByCustomerIdAndTimeIntervalAsync(
        int customerId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Добавить заказ.
    /// </summary>
    /// <param name="order">
    /// Заказ.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPost]
    //авторизация
    public async Task<IActionResult> AddOrderAsync(
        Transport.Models.ForCreate.Order order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить статус заказа.
    /// </summary>
    /// <param name="orderAndStatus">
    /// Идентификатор заказа и его статус.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateOrderStatusAsync(
        Transport.Models.ForUpdate.OrderIdAndStatus orderAndStatus,
        CancellationToken token)
    {

    }

    private readonly IRequestProcessorWithAuthorize<RequestGetManyWithPagination<OrderSorting>, IList<SimleOrder>> _getOrdersProcessor;//
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByTimeIntervalWithPagination<OrderSorting>, IList<SimleOrder>> _getOrdersByTimeIntervalProcessor;//
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, OrderSorting>, IList<SimleOrder>> _getOrdersByShopIdProcessor;//
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Shop, OrderSorting>, IList<SimleOrder>> _getOrdersByShopIdAndTimeIntervalProcessor;//
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Customer, OrderSorting>, IList<SimleOrder>> _getOrdersByCustomerIdProcessor;//
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Customer, OrderSorting>, IList<SimleOrder>> _getOrdersByCustomerIdAndTimeIntervalProcessor;//
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<OrderWithoutId>> _addOrderProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateOrderStatus> _updateOrderStatusProcessor;
}
