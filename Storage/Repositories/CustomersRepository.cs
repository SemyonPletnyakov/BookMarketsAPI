using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using Storage.Abstractions.Repositories;
using Storage.Getters;
using Storage.Exceptions;

using CustomerWothoutId = Models.ForCreate.Customer;
using CustomerWithoutPassword = Models.ForUpdate.Customer;
using Microsoft.EntityFrameworkCore;

namespace Storage.Repositories;

public sealed class CustomersRepository : ICustomersRepository
{
    /// <summary>
    /// Создаёт объект <see cref="CustomersRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public CustomersRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<CustomerWithoutPassword>> GetCustomersAsync(
        PaginationInfo<CustomerSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Customers
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(c => 
                new CustomerWithoutPassword(
                    new(c.CustomerId),
                    c.LastName == null
                        ? null
                        : new(c.LastName, c.FirstName, c.Patronymic),
                    c.BirthDate,
                    c.Phone == null
                        ? null
                        : new(c.Phone),
                    new(c.Email)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<CustomerWithoutPassword>> GetCustomersByLastNameAsync(
        LastName lastName,
        PaginationInfo<CustomerSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(lastName);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Customers
            .Where(e => EF.Functions.Like(e.LastName, $"%{lastName.Value}%"))
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(c =>
                new CustomerWithoutPassword(
                    new(c.CustomerId),
                    c.LastName == null
                        ? null
                        : new(c.LastName, c.FirstName, c.Patronymic),
                    c.BirthDate,
                    c.Phone == null
                        ? null
                        : new(c.Phone),
                    new(c.Email)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<CustomerWithoutPassword>> GetCustomersByShopIdByTimeIntervalAsync(
        Id<Shop> shopId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        PaginationInfo<CustomerSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Orders
            .Where(o => o.ShopId == shopId.Value
                    && o.DateTime >= startDate
                    && o.DateTime <= endDate)
            .Include(o => o.Customer)
            .Select(o => o.Customer)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(c =>
                new CustomerWithoutPassword(
                    new(c.CustomerId),
                    c.LastName == null
                        ? null
                        : new(c.LastName, c.FirstName, c.Patronymic),
                    c.BirthDate,
                    c.Phone == null
                        ? null
                        : new(c.Phone),
                    new(c.Email)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Customer> GetCustomerByEmailAsync(
        Email email,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(email);
        token.ThrowIfCancellationRequested();

        var customer = await _context.Customers
            .SingleOrDefaultAsync(p => p.Email == email.Value, token);

        if (customer is null)
        {
            throw new EntityNotFoundException(
                $"Пользователь с email {email.Value} не найден.");
        }

        return new(
            new(customer.CustomerId),
            customer.LastName == null
                ? null
                : new(customer.LastName, customer.FirstName, customer.Patronymic),
            customer.BirthDate,
            customer.Phone == null
                ? null
                : new(customer.Phone),
            new(customer.Email),
            new(customer.Password));
    }

    /// <inheritdoc/>
    public async Task<CustomerWithoutPassword> GetCustomerByIdAsync(
        Id<Customer> customerId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        token.ThrowIfCancellationRequested();

        var customer = await _context.Customers
            .SingleOrDefaultAsync(p => p.CustomerId == customerId.Value, token);

        if (customer is null)
        {
            throw new EntityNotFoundException(
                $"Пользователь с id = {customerId.Value} не найден.");
        }

        return new(
            new(customer.CustomerId),
            customer.LastName == null
                ? null
                : new(customer.LastName, customer.FirstName, customer.Patronymic),
            customer.BirthDate,
            customer.Phone == null
                ? null
                : new(customer.Phone),
            new(customer.Email));
    }

    /// <inheritdoc/>
    public async Task AddCustomerAsync(
        CustomerWothoutId customer,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(customer);
        token.ThrowIfCancellationRequested();

        var contextCustomer = await _context.Customers
            .SingleOrDefaultAsync(p => p.Email == customer.Email.Value, token);

        if (contextCustomer is null)
        {
            throw new EntityNotFoundException(
                $"Пользователь с таким email уже существует.");
        }

        _context.Customers.Add(
            new Models.Customer
            {
                LastName = customer.FullName?.LastName,
                FirstName = customer.FullName?.FirstName,
                Patronymic = customer.FullName?.Patronymic,
                BirthDate = customer.BirthDate,
                Phone = customer.Phone?.Value,
                Email = customer.Email.Value,
                Password = customer.Password.Value
            });
    }

    /// <inheritdoc/>
    public async Task UpdateCustomerAsync(Customer customer, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(customer);
        token.ThrowIfCancellationRequested();

        var contextCustomer = await _context.Customers
            .SingleOrDefaultAsync(p => p.CustomerId == customer.CustomerId.Value, token);

        if (contextCustomer is null)
        {
            throw new EntityNotFoundException(
                $"Пользователь с id = {customer.CustomerId.Value} не найден.");
        }

        contextCustomer.LastName = customer.FullName?.LastName;
        contextCustomer.FirstName = customer.FullName?.FirstName;
        contextCustomer.Patronymic = customer.FullName?.Patronymic;
        contextCustomer.BirthDate = customer.BirthDate;
        contextCustomer.Phone = customer.Phone?.Value;
        contextCustomer.Email = customer.Email.Value;
        contextCustomer.Password = customer.Password.Value;
    }

    private readonly ApplicationContext _context;
}
