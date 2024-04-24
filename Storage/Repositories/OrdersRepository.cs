using Microsoft.EntityFrameworkCore;

using Models;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.SimpleEntities;

using Storage.Abstractions.Repositories;
using Storage.Getters;
using Storage.Exceptions;

using System.ComponentModel;

using OrderWithoutId = Models.ForCreate.Order;
using OrderSimple = Models.ForUpdate.Order;

namespace Storage.Repositories;

public sealed class OrdersRepository : IOrdersRepository
{
    /// <summary>
    /// Создаёт объект <see cref="OrdersRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public OrdersRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    /// <inheritdoc/>
    public async Task<IList<OrderSimple>> GetOrdersAsync(
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Shop)
            .ThenInclude(o => o.Address)
            .Include(o => o.ProductsInOrder)
            .ThenInclude(o => o.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(o => 
                new OrderSimple(
                    new(o.OrderId),
                    new(
                        new(o.Customer.CustomerId),
                        o.Customer.LastName == null
                            ? null
                            : new(o.Customer.LastName, o.Customer.FirstName, o.Customer.Patronymic),
                        o.Customer.BirthDate,
                        o.Customer.Phone == null
                            ? null
                            : new(o.Customer.Phone),
                        new(o.Customer.Email)),
                    new(
                        new(o.Shop.ShopId),
                        o.Shop.Name == null
                            ? null
                            : new(o.Shop.Name),
                        o.Shop.OpeningTime,
                        o.Shop.ClosingTime,
                        new(
                            new(o.Shop.AddressId),
                            o.Shop.Address.Country,
                            o.Shop.Address.RegionNumber,
                            o.Shop.Address.RegionName,
                            o.Shop.Address.City,
                            o.Shop.Address.District,
                            o.Shop.Address.Street,
                            o.Shop.Address.House,
                            o.Shop.Address.Room)),
                    o.DateTime,
                    o.OrderStatus,
                    o.ProductsInOrder.Select(piw =>
                        new ProductInfoInOrder(
                            new(
                                new(piw.ProductId),
                                new(piw.Product.Name),
                                new(piw.Product.Price),
                                piw.Product.KeyWords?.ToHashSet()),
                            new(piw.Count),
                            new(piw.ActualPrice))).ToList()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<OrderSimple>> GetOrdersByTimeIntervalAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Where(o => o.DateTime >= startDate && o.DateTime <= endDate)
            .Include(o => o.Customer)
            .Include(o => o.Shop)
            .ThenInclude(o => o.Address)
            .Include(o => o.ProductsInOrder)
            .ThenInclude(o => o.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(o =>
                new OrderSimple(
                    new(o.OrderId),
                    new(
                        new(o.Customer.CustomerId),
                        o.Customer.LastName == null
                            ? null
                            : new(o.Customer.LastName, o.Customer.FirstName, o.Customer.Patronymic),
                        o.Customer.BirthDate,
                        o.Customer.Phone == null
                            ? null
                            : new(o.Customer.Phone),
                        new(o.Customer.Email)),
                    new(
                        new(o.Shop.ShopId),
                        o.Shop.Name == null
                            ? null
                            : new(o.Shop.Name),
                        o.Shop.OpeningTime,
                        o.Shop.ClosingTime,
                        new(
                            new(o.Shop.AddressId),
                            o.Shop.Address.Country,
                            o.Shop.Address.RegionNumber,
                            o.Shop.Address.RegionName,
                            o.Shop.Address.City,
                            o.Shop.Address.District,
                            o.Shop.Address.Street,
                            o.Shop.Address.House,
                            o.Shop.Address.Room)),
                    o.DateTime,
                    o.OrderStatus,
                    o.ProductsInOrder.Select(piw =>
                        new ProductInfoInOrder(
                            new(
                                new(piw.ProductId),
                                new(piw.Product.Name),
                                new(piw.Product.Price),
                                piw.Product.KeyWords?.ToHashSet()),
                            new(piw.Count),
                            new(piw.ActualPrice))).ToList()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<OrderSimple>> GetOrdersByShopIdAsync(
        Id<Shop> shopId,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Where(o => o.ShopId == shopId.Value)
            .Include(o => o.Customer)
            .Include(o => o.Shop)
            .ThenInclude(o => o.Address)
            .Include(o => o.ProductsInOrder)
            .ThenInclude(o => o.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(o =>
                new OrderSimple(
                    new(o.OrderId),
                    new(
                        new(o.Customer.CustomerId),
                        o.Customer.LastName == null
                            ? null
                            : new(o.Customer.LastName, o.Customer.FirstName, o.Customer.Patronymic),
                        o.Customer.BirthDate,
                        o.Customer.Phone == null
                            ? null
                            : new(o.Customer.Phone),
                        new(o.Customer.Email)),
                    new(
                        new(o.Shop.ShopId),
                        o.Shop.Name == null
                            ? null
                            : new(o.Shop.Name),
                        o.Shop.OpeningTime,
                        o.Shop.ClosingTime,
                        new(
                            new(o.Shop.AddressId),
                            o.Shop.Address.Country,
                            o.Shop.Address.RegionNumber,
                            o.Shop.Address.RegionName,
                            o.Shop.Address.City,
                            o.Shop.Address.District,
                            o.Shop.Address.Street,
                            o.Shop.Address.House,
                            o.Shop.Address.Room)),
                    o.DateTime,
                    o.OrderStatus,
                    o.ProductsInOrder.Select(piw =>
                        new ProductInfoInOrder(
                            new(
                                new(piw.ProductId),
                                new(piw.Product.Name),
                                new(piw.Product.Price),
                                piw.Product.KeyWords?.ToHashSet()),
                            new(piw.Count),
                            new(piw.ActualPrice))).ToList()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<OrderSimple>> GetOrdersByShopIdByTimeIntervalAsync(
        Id<Shop> shopId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Where(o => 
                o.ShopId == shopId.Value
                && o.DateTime >= startDate 
                && o.DateTime <= endDate)
            .Include(o => o.Customer)
            .Include(o => o.Shop)
            .ThenInclude(o => o.Address)
            .Include(o => o.ProductsInOrder)
            .ThenInclude(o => o.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(o =>
                new OrderSimple(
                    new(o.OrderId),
                    new(
                        new(o.Customer.CustomerId),
                        o.Customer.LastName == null
                            ? null
                            : new(o.Customer.LastName, o.Customer.FirstName, o.Customer.Patronymic),
                        o.Customer.BirthDate,
                        o.Customer.Phone == null
                            ? null
                            : new(o.Customer.Phone),
                        new(o.Customer.Email)),
                    new(
                        new(o.Shop.ShopId),
                        o.Shop.Name == null
                            ? null
                            : new(o.Shop.Name),
                        o.Shop.OpeningTime,
                        o.Shop.ClosingTime,
                        new(
                            new(o.Shop.AddressId),
                            o.Shop.Address.Country,
                            o.Shop.Address.RegionNumber,
                            o.Shop.Address.RegionName,
                            o.Shop.Address.City,
                            o.Shop.Address.District,
                            o.Shop.Address.Street,
                            o.Shop.Address.House,
                            o.Shop.Address.Room)),
                    o.DateTime,
                    o.OrderStatus,
                    o.ProductsInOrder.Select(piw =>
                        new ProductInfoInOrder(
                            new(
                                new(piw.ProductId),
                                new(piw.Product.Name),
                                new(piw.Product.Price),
                                piw.Product.KeyWords?.ToHashSet()),
                            new(piw.Count),
                            new(piw.ActualPrice))).ToList()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<OrderSimple>> GetOrdersByCustomerIdAsync(
        Id<Customer> customerId,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Where(o => o.CustomerId == customerId.Value)
            .Include(o => o.Customer)
            .Include(o => o.Shop)
            .ThenInclude(o => o.Address)
            .Include(o => o.ProductsInOrder)
            .ThenInclude(o => o.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(o =>
                new OrderSimple(
                    new(o.OrderId),
                    new(
                        new(o.Customer.CustomerId),
                        o.Customer.LastName == null
                            ? null
                            : new(o.Customer.LastName, o.Customer.FirstName, o.Customer.Patronymic),
                        o.Customer.BirthDate,
                        o.Customer.Phone == null
                            ? null
                            : new(o.Customer.Phone),
                        new(o.Customer.Email)),
                    new(
                        new(o.Shop.ShopId),
                        o.Shop.Name == null
                            ? null
                            : new(o.Shop.Name),
                        o.Shop.OpeningTime,
                        o.Shop.ClosingTime,
                        new(
                            new(o.Shop.AddressId),
                            o.Shop.Address.Country,
                            o.Shop.Address.RegionNumber,
                            o.Shop.Address.RegionName,
                            o.Shop.Address.City,
                            o.Shop.Address.District,
                            o.Shop.Address.Street,
                            o.Shop.Address.House,
                            o.Shop.Address.Room)),
                    o.DateTime,
                    o.OrderStatus,
                    o.ProductsInOrder.Select(piw =>
                        new ProductInfoInOrder(
                            new(
                                new(piw.ProductId),
                                new(piw.Product.Name),
                                new(piw.Product.Price),
                                piw.Product.KeyWords?.ToHashSet()),
                            new(piw.Count),
                            new(piw.ActualPrice))).ToList()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<OrderSimple>> GetOrdersByCustomerIdByTimeIntervalAsync(
        Id<Customer> customerId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<OrderSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Where(o =>
                o.CustomerId == customerId.Value
                && o.DateTime >= startDate
                && o.DateTime <= endDate)
            .Include(o => o.Customer)
            .Include(o => o.Shop)
            .ThenInclude(o => o.Address)
            .Include(o => o.ProductsInOrder)
            .ThenInclude(o => o.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(o =>
                new OrderSimple(
                    new(o.OrderId),
                    new(
                        new(o.Customer.CustomerId),
                        o.Customer.LastName == null
                            ? null
                            : new(o.Customer.LastName, o.Customer.FirstName, o.Customer.Patronymic),
                        o.Customer.BirthDate,
                        o.Customer.Phone == null
                            ? null
                            : new(o.Customer.Phone),
                        new(o.Customer.Email)),
                    new(
                        new(o.Shop.ShopId),
                        o.Shop.Name == null
                            ? null
                            : new(o.Shop.Name),
                        o.Shop.OpeningTime,
                        o.Shop.ClosingTime,
                        new(
                            new(o.Shop.AddressId),
                            o.Shop.Address.Country,
                            o.Shop.Address.RegionNumber,
                            o.Shop.Address.RegionName,
                            o.Shop.Address.City,
                            o.Shop.Address.District,
                            o.Shop.Address.Street,
                            o.Shop.Address.House,
                            o.Shop.Address.Room)),
                    o.DateTime,
                    o.OrderStatus,
                    o.ProductsInOrder.Select(piw =>
                        new ProductInfoInOrder(
                            new(
                                new(piw.ProductId),
                                new(piw.Product.Name),
                                new(piw.Product.Price),
                                piw.Product.KeyWords?.ToHashSet()),
                            new(piw.Count),
                            new(piw.ActualPrice))).ToList()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<OrderSimple> GetOrderByOrderIdAsync(
        Id<Order> orderId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        token.ThrowIfCancellationRequested();

        var order = await _context.Orders
            .SingleOrDefaultAsync(p => p.OrderId == orderId.Value, token);

        if (order is null)
        {
            throw new EntityNotFoundException(
                $"Заказ с Id = {orderId.Value} не найден.");
        }
        
        return new(
            new(order.OrderId),
            new(
                new(order.Customer.CustomerId),
                order.Customer.LastName == null
                    ? null
                    : new(
                        order.Customer.LastName, 
                        order.Customer.FirstName, 
                        order.Customer.Patronymic),
                order.Customer.BirthDate,
                order.Customer.Phone == null
                    ? null
                    : new(order.Customer.Phone),
                new(order.Customer.Email)),
            new(
                new(order.Shop.ShopId),
                order.Shop.Name == null
                    ? null
                    : new(order.Shop.Name),
                order.Shop.OpeningTime,
                order.Shop.ClosingTime,
                new(
                    new(order.Shop.AddressId),
                    order.Shop.Address.Country,
                    order.Shop.Address.RegionNumber,
                    order.Shop.Address.RegionName,
                    order.Shop.Address.City,
                    order.Shop.Address.District,
                    order.Shop.Address.Street,
                    order.Shop.Address.House,
                    order.Shop.Address.Room)),
            order.DateTime,
            order.OrderStatus,
            order.ProductsInOrder.Select(piw =>
                new ProductInfoInOrder(
                    new(
                        new(piw.ProductId),
                        new(piw.Product.Name),
                        new(piw.Product.Price),
                        piw.Product.KeyWords?.ToHashSet()),
                    new(piw.Count),
                    new(piw.ActualPrice))).ToList());
    }

    /// <inheritdoc/>
    public async Task AddOrderAsync(OrderWithoutId order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);
        token.ThrowIfCancellationRequested();

        var contextOrder = await _context.Orders.AddAsync(
            new Models.Order
            {
                CustomerId = order.CustomerId.Value,
                DateTime = order.DateTime,
                OrderStatus = order.OrderStatus,
                ShopId = order.ShopId.Value
            });

        var allProductIds = order.ProductsInOrder
            .Select(p => p.ProductId.Value)
            .ToHashSet();

        var productsInfo = await _context.Products
            .Where(p => allProductIds.Contains(p.ProductId))
            .ToArrayAsync(token);

        if (allProductIds.Count != productsInfo.Length)
        {
            throw new EntityNotFoundException("Не все продукты найдены");
        }

        var productsInOrder = order.ProductsInOrder
            .Select(p => new Models.ProductsInOrder
            {
                ProductId = p.ProductId.Value,
                Count = p.Count.Value,
                ActualPrice = productsInfo.Single(pi => pi.ProductId == p.ProductId.Value).Price,
            }).ToArray();

        _context.ProductsInOrders.AddRange(productsInOrder);
    }

    /// <inheritdoc/>
    public async Task UpdateOrderStatusAsync(
        Id<Order> orderId,
        OrderStatus orderStatus,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        token.ThrowIfCancellationRequested();

        if (Enum.IsDefined<OrderStatus>(orderStatus))
        {
            throw new InvalidEnumArgumentException(
                nameof(orderStatus),
                (int)orderStatus,
                typeof(OrderStatus));
        }

        var order = await _context.Orders
            .SingleOrDefaultAsync(o => o.OrderId == orderId.Value, token);

        if (order == null)
        {
            throw new EntityNotFoundException($"Заказ с id = {orderId.Value} не найден.");
        }

        order.OrderStatus = orderStatus;
    }

    private readonly ApplicationContext _context;
}
