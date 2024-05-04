using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models;
using Models.Exceptions;
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
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Customer, OrderSorting>, IList<SimleOrder>> getOrdersByCustomerIdProcessor,
        IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Customer, OrderSorting>, IList<SimleOrder>> getOrdersByCustomerIdAndTimeIntervalProcessor, 
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
    [Authorize]
    public async Task<IActionResult> GetOrdersAsync(
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var orders =
                (await _getOrdersProcessor.ProcessAsync(
                        new(new(size, number, order)),
                        jwtToken,
                        token))
                    .Select(order =>
                        new Transport.Models.ForUpdate.Order
                        {
                            OrderId = order.OrderId.Value,
                            DateTime = order.DateTime,
                            OrderStatus = order.OrderStatus,
                            Shop = new()
                            {
                                ShopId = order.Shop.ShopId.Value,
                                Name = order.Shop.Name?.Value,
                                OpeningTime = order.Shop.OpeningTime,
                                ClosingTime = order.Shop.ClosingTime,
                                Address = new() 
                                { 
                                    AddressId = order.Shop.Address.AddressId.Value,
                                    Country = order.Shop.Address.Country,
                                    RegionNumber = order.Shop.Address.RegionNumber,
                                    RegionName = order.Shop.Address.RegionName,
                                    City = order.Shop.Address.City,
                                    District = order.Shop.Address.District,
                                    Street = order.Shop.Address.Street,
                                    House = order.Shop.Address.House,
                                    Room = order.Shop.Address.Room
                                }
                            },
                            Customer = new()
                            {
                                CustomerId = order.Customer.CustomerId.Value,
                                BirthDate = order.Customer.BirthDate,
                                Email = order.Customer.Email.Value,
                                Phone = order.Customer.Phone?.Value,
                                LastName = order.Customer.FullName?.LastName,
                                FirstName = order.Customer.FullName?.FirstName,
                                Patronymic = order.Customer.FullName?.Patronymic
                            },
                            ProductsInOrder = order.ProductsInOrder
                                .Select(product => 
                                    new Transport.Models.SimpleModels.ProductInfoInOrder
                                    {
                                        ProductId = product.ProductId.Value,
                                        Count = product.ProductId.Value,
                                        ActualPrice = product.ActualPrice.Value
                                    })
                                .ToList(),
                        }).ToArray();

            return Ok(orders);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
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
    [HttpGet("time")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByTimeIntervalAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var orders =
                (await _getOrdersByTimeIntervalProcessor.ProcessAsync(
                        new(new(size, number, order), startDate, endDate),
                        jwtToken,
                        token))
                    .Select(order =>
                        new Transport.Models.ForUpdate.Order
                        {
                            OrderId = order.OrderId.Value,
                            DateTime = order.DateTime,
                            OrderStatus = order.OrderStatus,
                            Shop = new()
                            {
                                ShopId = order.Shop.ShopId.Value,
                                Name = order.Shop.Name?.Value,
                                OpeningTime = order.Shop.OpeningTime,
                                ClosingTime = order.Shop.ClosingTime,
                                Address = new()
                                {
                                    AddressId = order.Shop.Address.AddressId.Value,
                                    Country = order.Shop.Address.Country,
                                    RegionNumber = order.Shop.Address.RegionNumber,
                                    RegionName = order.Shop.Address.RegionName,
                                    City = order.Shop.Address.City,
                                    District = order.Shop.Address.District,
                                    Street = order.Shop.Address.Street,
                                    House = order.Shop.Address.House,
                                    Room = order.Shop.Address.Room
                                }
                            },
                            Customer = new()
                            {
                                CustomerId = order.Customer.CustomerId.Value,
                                BirthDate = order.Customer.BirthDate,
                                Email = order.Customer.Email.Value,
                                Phone = order.Customer.Phone?.Value,
                                LastName = order.Customer.FullName?.LastName,
                                FirstName = order.Customer.FullName?.FirstName,
                                Patronymic = order.Customer.FullName?.Patronymic
                            },
                            ProductsInOrder = order.ProductsInOrder
                                .Select(product =>
                                    new Transport.Models.SimpleModels.ProductInfoInOrder
                                    {
                                        ProductId = product.ProductId.Value,
                                        Count = product.ProductId.Value,
                                        ActualPrice = product.ActualPrice.Value
                                    })
                                .ToList(),
                        }).ToArray();

            return Ok(orders);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
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
    [HttpGet("shop")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByShopIdAsync(
        int shopId,
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var orders =
                (await _getOrdersByShopIdProcessor.ProcessAsync(
                        new(new(shopId), new(size, number, order)),
                        jwtToken,
                        token))
                    .Select(order =>
                        new Transport.Models.ForUpdate.Order
                        {
                            OrderId = order.OrderId.Value,
                            DateTime = order.DateTime,
                            OrderStatus = order.OrderStatus,
                            Shop = new()
                            {
                                ShopId = order.Shop.ShopId.Value,
                                Name = order.Shop.Name?.Value,
                                OpeningTime = order.Shop.OpeningTime,
                                ClosingTime = order.Shop.ClosingTime,
                                Address = new()
                                {
                                    AddressId = order.Shop.Address.AddressId.Value,
                                    Country = order.Shop.Address.Country,
                                    RegionNumber = order.Shop.Address.RegionNumber,
                                    RegionName = order.Shop.Address.RegionName,
                                    City = order.Shop.Address.City,
                                    District = order.Shop.Address.District,
                                    Street = order.Shop.Address.Street,
                                    House = order.Shop.Address.House,
                                    Room = order.Shop.Address.Room
                                }
                            },
                            Customer = new()
                            {
                                CustomerId = order.Customer.CustomerId.Value,
                                BirthDate = order.Customer.BirthDate,
                                Email = order.Customer.Email.Value,
                                Phone = order.Customer.Phone?.Value,
                                LastName = order.Customer.FullName?.LastName,
                                FirstName = order.Customer.FullName?.FirstName,
                                Patronymic = order.Customer.FullName?.Patronymic
                            },
                            ProductsInOrder = order.ProductsInOrder
                                .Select(product =>
                                    new Transport.Models.SimpleModels.ProductInfoInOrder
                                    {
                                        ProductId = product.ProductId.Value,
                                        Count = product.ProductId.Value,
                                        ActualPrice = product.ActualPrice.Value
                                    })
                                .ToList(),
                        }).ToArray();

            return Ok(orders);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
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
    [HttpGet("shop_and_time")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByShopIdAndTimeIntervalAsync(
        int shopId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var orders =
                (await _getOrdersByShopIdAndTimeIntervalProcessor.ProcessAsync(
                        new(new(shopId), new(size, number, order), startDate, endDate),
                        jwtToken,
                        token))
                    .Select(order =>
                        new Transport.Models.ForUpdate.Order
                        {
                            OrderId = order.OrderId.Value,
                            DateTime = order.DateTime,
                            OrderStatus = order.OrderStatus,
                            Shop = new()
                            {
                                ShopId = order.Shop.ShopId.Value,
                                Name = order.Shop.Name?.Value,
                                OpeningTime = order.Shop.OpeningTime,
                                ClosingTime = order.Shop.ClosingTime,
                                Address = new()
                                {
                                    AddressId = order.Shop.Address.AddressId.Value,
                                    Country = order.Shop.Address.Country,
                                    RegionNumber = order.Shop.Address.RegionNumber,
                                    RegionName = order.Shop.Address.RegionName,
                                    City = order.Shop.Address.City,
                                    District = order.Shop.Address.District,
                                    Street = order.Shop.Address.Street,
                                    House = order.Shop.Address.House,
                                    Room = order.Shop.Address.Room
                                }
                            },
                            Customer = new()
                            {
                                CustomerId = order.Customer.CustomerId.Value,
                                BirthDate = order.Customer.BirthDate,
                                Email = order.Customer.Email.Value,
                                Phone = order.Customer.Phone?.Value,
                                LastName = order.Customer.FullName?.LastName,
                                FirstName = order.Customer.FullName?.FirstName,
                                Patronymic = order.Customer.FullName?.Patronymic
                            },
                            ProductsInOrder = order.ProductsInOrder
                                .Select(product =>
                                    new Transport.Models.SimpleModels.ProductInfoInOrder
                                    {
                                        ProductId = product.ProductId.Value,
                                        Count = product.ProductId.Value,
                                        ActualPrice = product.ActualPrice.Value
                                    })
                                .ToList(),
                        }).ToArray();

            return Ok(orders);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
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
    [HttpGet("customer")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByCustomerIdAsync(
        int customerId,
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var orders =
                (await _getOrdersByCustomerIdProcessor.ProcessAsync(
                        new(new(customerId), new(size, number, order)),
                        jwtToken,
                        token))
                    .Select(order =>
                        new Transport.Models.ForUpdate.Order
                        {
                            OrderId = order.OrderId.Value,
                            DateTime = order.DateTime,
                            OrderStatus = order.OrderStatus,
                            Shop = new()
                            {
                                ShopId = order.Shop.ShopId.Value,
                                Name = order.Shop.Name?.Value,
                                OpeningTime = order.Shop.OpeningTime,
                                ClosingTime = order.Shop.ClosingTime,
                                Address = new()
                                {
                                    AddressId = order.Shop.Address.AddressId.Value,
                                    Country = order.Shop.Address.Country,
                                    RegionNumber = order.Shop.Address.RegionNumber,
                                    RegionName = order.Shop.Address.RegionName,
                                    City = order.Shop.Address.City,
                                    District = order.Shop.Address.District,
                                    Street = order.Shop.Address.Street,
                                    House = order.Shop.Address.House,
                                    Room = order.Shop.Address.Room
                                }
                            },
                            Customer = new()
                            {
                                CustomerId = order.Customer.CustomerId.Value,
                                BirthDate = order.Customer.BirthDate,
                                Email = order.Customer.Email.Value,
                                Phone = order.Customer.Phone?.Value,
                                LastName = order.Customer.FullName?.LastName,
                                FirstName = order.Customer.FullName?.FirstName,
                                Patronymic = order.Customer.FullName?.Patronymic
                            },
                            ProductsInOrder = order.ProductsInOrder
                                .Select(product =>
                                    new Transport.Models.SimpleModels.ProductInfoInOrder
                                    {
                                        ProductId = product.ProductId.Value,
                                        Count = product.ProductId.Value,
                                        ActualPrice = product.ActualPrice.Value
                                    })
                                .ToList(),
                        }).ToArray();

            return Ok(orders);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
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
    [HttpGet("customer_and_time")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByCustomerIdAndTimeIntervalAsync(
        int customerId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        OrderSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var orders =
                (await _getOrdersByCustomerIdAndTimeIntervalProcessor.ProcessAsync(
                        new(new(customerId), new(size, number, order), startDate, endDate),
                        jwtToken,
                        token))
                    .Select(order =>
                        new Transport.Models.ForUpdate.Order
                        {
                            OrderId = order.OrderId.Value,
                            DateTime = order.DateTime,
                            OrderStatus = order.OrderStatus,
                            Shop = new()
                            {
                                ShopId = order.Shop.ShopId.Value,
                                Name = order.Shop.Name?.Value,
                                OpeningTime = order.Shop.OpeningTime,
                                ClosingTime = order.Shop.ClosingTime,
                                Address = new()
                                {
                                    AddressId = order.Shop.Address.AddressId.Value,
                                    Country = order.Shop.Address.Country,
                                    RegionNumber = order.Shop.Address.RegionNumber,
                                    RegionName = order.Shop.Address.RegionName,
                                    City = order.Shop.Address.City,
                                    District = order.Shop.Address.District,
                                    Street = order.Shop.Address.Street,
                                    House = order.Shop.Address.House,
                                    Room = order.Shop.Address.Room
                                }
                            },
                            Customer = new()
                            {
                                CustomerId = order.Customer.CustomerId.Value,
                                BirthDate = order.Customer.BirthDate,
                                Email = order.Customer.Email.Value,
                                Phone = order.Customer.Phone?.Value,
                                LastName = order.Customer.FullName?.LastName,
                                FirstName = order.Customer.FullName?.FirstName,
                                Patronymic = order.Customer.FullName?.Patronymic
                            },
                            ProductsInOrder = order.ProductsInOrder
                                .Select(product =>
                                    new Transport.Models.SimpleModels.ProductInfoInOrder
                                    {
                                        ProductId = product.ProductId.Value,
                                        Count = product.ProductId.Value,
                                        ActualPrice = product.ActualPrice.Value
                                    })
                                .ToList(),
                        }).ToArray();

            return Ok(orders);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
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
    [Authorize]
    public async Task<IActionResult> AddOrderAsync(
        Transport.Models.ForCreate.Order order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _addOrderProcessor.ProcessAsync(
                new(new(new(order.CustomerId),
                        new(order.ShopId),
                        DateTimeOffset.UtcNow,
                        OrderStatus.InProcess,
                        order.ProductsInOrder
                            .Select(product => 
                                new Models.ForCreate.ProductCount(
                                    new(product.ProductId), 
                                    new(product.Count)))
                            .ToList())),
                jwtToken,
                token);

            return Created();
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
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
    [Authorize]
    public async Task<IActionResult> UpdateOrderStatusAsync(
        Transport.Models.ForUpdate.OrderIdAndStatus orderAndStatus,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateOrderStatusProcessor.ProcessAsync(
                new(new(orderAndStatus.OrderId), orderAndStatus.OrderStatus),
                jwtToken,
                token);

            return Accepted();
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    private readonly IRequestProcessorWithAuthorize<RequestGetManyWithPagination<OrderSorting>, IList<SimleOrder>> _getOrdersProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByTimeIntervalWithPagination<OrderSorting>, IList<SimleOrder>> _getOrdersByTimeIntervalProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, OrderSorting>, IList<SimleOrder>> _getOrdersByShopIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Shop, OrderSorting>, IList<SimleOrder>> _getOrdersByShopIdAndTimeIntervalProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Customer, OrderSorting>, IList<SimleOrder>> _getOrdersByCustomerIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Customer, OrderSorting>, IList<SimleOrder>> _getOrdersByCustomerIdAndTimeIntervalProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<OrderWithoutId>> _addOrderProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateOrderStatus> _updateOrderStatusProcessor;
}
