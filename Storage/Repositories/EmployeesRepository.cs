using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using Storage.Abstractions.Repositories;
using Storage.Getters;
using Storage.Exceptions;

using EmployeeWothoutId = Models.ForCreate.Employee;
using EmployeeWithoutPassword = Models.ForUpdate.Employee;
using Microsoft.EntityFrameworkCore;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий сотрудников.
/// </summary>
public sealed class EmployeesRepository : IEmployeesRepository
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeesRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public EmployeesRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<EmployeeWithoutPassword>> GetEmployeesAsync(
        PaginationInfo<EmployeeSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Employees
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(e =>
                new EmployeeWithoutPassword(
                    new(e.EmployeeId),
                    new(e.LastName, e.FirstName, e.Patronymic),
                    e.BirthDate,
                    e.Phone == null
                        ? null
                        : new(e.Phone),
                    e.Email == null
                        ? null
                        : new(e.Email),
                    new(e.JobTitle),
                    new(e.Login)))
                .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<EmployeeWithoutPassword>> GetEmployeesByLastNameAsync(
        LastName lastName,
        PaginationInfo<EmployeeSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(lastName);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Employees
            .Where(e => EF.Functions.Like(e.LastName, $"%{lastName.Value}%"))
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(e =>
                new EmployeeWithoutPassword(
                    new(e.EmployeeId),
                    new(e.LastName, e.FirstName, e.Patronymic),
                    e.BirthDate,
                    e.Phone == null
                        ? null
                        : new(e.Phone),
                    e.Email == null
                        ? null
                        : new(e.Email),
                    new(e.JobTitle),
                    new(e.Login)))
                .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<EmployeeWithoutPassword>> GetEmployeesByShopAsync(
        Id<Shop> shopId,
        PaginationInfo<EmployeeSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.LinksEmployeeAndShops
            .Where(e => e.ShopId == shopId.Value)
            .Include(e => e.Employee)
            .Select(e => e.Employee)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(e =>
                new EmployeeWithoutPassword(
                    new(e.EmployeeId),
                    new(e.LastName, e.FirstName, e.Patronymic),
                    e.BirthDate,
                    e.Phone == null
                        ? null
                        : new(e.Phone),
                    e.Email == null
                        ? null
                        : new(e.Email),
                    new(e.JobTitle),
                    new(e.Login)))
                .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<EmployeeWithoutPassword>> GetEmployeesByWarehouseAsync(
        Id<Warehouse> warehouseId,
        PaginationInfo<EmployeeSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouseId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.LinksEmployeeAndWarehouses
            .Where(e => e.WarehouseId == warehouseId.Value)
            .Include(e => e.Employee)
            .Select(e => e.Employee)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(e =>
                new EmployeeWithoutPassword(
                    new(e.EmployeeId),
                    new(e.LastName, e.FirstName, e.Patronymic),
                    e.BirthDate,
                    e.Phone == null
                        ? null
                        : new(e.Phone),
                    e.Email == null
                        ? null
                        : new(e.Email),
                    new(e.JobTitle),
                    new(e.Login)))
                .ToList();
    }

    /// <inheritdoc/>
    public async Task<Employee> GetEmployeeByLoginAsync(
        Login login,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(login);
        token.ThrowIfCancellationRequested();

        var employee = await _context.Employees
            .SingleOrDefaultAsync(p => p.Login == login.Value, token);

        if (employee is null)
        {
            throw new EntityNotFoundException(
                $"Сотрудник с логином {login.Value} не найден.");
        }

        return new(
            new(employee.EmployeeId),
            new(employee.LastName, employee.FirstName, employee.Patronymic),
            employee.BirthDate,
            employee.Phone == null
                ? null
                : new(employee.Phone),
            employee.Email == null
                ? null
                : new(employee.Email),
            new(employee.JobTitle),
            new(employee.Login),
            new(employee.Password));
    }

    /// <inheritdoc/>
    public async Task<EmployeeWithoutPassword> GetEmployeeByIdAsync(
        Id<Employee> employeeId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(employeeId);
        token.ThrowIfCancellationRequested();

        var employee = await _context.Employees
            .SingleOrDefaultAsync(p => p.EmployeeId == employeeId.Value, token);

        if (employee is null)
        {
            throw new EntityNotFoundException(
                $"Сотрудник с id = {employeeId.Value} не найден.");
        }

        return new(
            new(employee.EmployeeId),
            new(employee.LastName, employee.FirstName, employee.Patronymic),
            employee.BirthDate,
            employee.Phone == null
                ? null
                : new(employee.Phone),
            employee.Email == null
                ? null
                : new(employee.Email),
            new(employee.JobTitle),
            new(employee.Login));
    }

    /// <inheritdoc/>
    public async Task AddEmployeeAsync(EmployeeWothoutId employee, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(employee);
        token.ThrowIfCancellationRequested();

        var contextEmployee = await _context.Employees
            .SingleOrDefaultAsync(p => p.Login == employee.Login.Value, token);

        if (contextEmployee is not null)
        {
            throw new EntityNotFoundException(
                $"Сотрудник с таким логином уже существует.");
        }

        _context.Employees.Add(
            new Models.Employee
            {
                LastName = employee.FullName.LastName,
                FirstName = employee.FullName.FirstName,
                Patronymic = employee.FullName.Patronymic,
                BirthDate = employee.BirthDate,
                Phone = employee.Phone?.Value,
                Email = employee.Email?.Value,
                JobTitle = employee.JobTitle.Value,
                Login = employee.Login.Value,
                Password = employee.Password.Value
            });
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeAsync(
        EmployeeWithoutPassword employee,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(employee);
        token.ThrowIfCancellationRequested();

        var contextEmployee = await _context.Employees
            .SingleOrDefaultAsync(p => p.EmployeeId == employee.EmployeeId.Value, token);

        if (contextEmployee is null)
        {
            throw new EntityNotFoundException(
                $"Сотрудник с id = {employee.EmployeeId.Value} не найден.");
        }

        contextEmployee.LastName = employee.FullName.LastName;
        contextEmployee.FirstName = employee.FullName.FirstName;
        contextEmployee.Patronymic = employee.FullName.Patronymic;
        contextEmployee.BirthDate = employee.BirthDate;
        contextEmployee.Phone = employee.Phone?.Value;
        contextEmployee.Email = employee.Email?.Value;
        contextEmployee.JobTitle = employee.JobTitle.Value;
        contextEmployee.Login = employee.Login.Value;
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeePasswordAsync(
        Login login,
        Password password,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(login);
        ArgumentNullException.ThrowIfNull(password);
        token.ThrowIfCancellationRequested();

        var contextEmployee = await _context.Employees
            .SingleOrDefaultAsync(p => p.Login == login.Value, token);

        if (contextEmployee is null)
        {
            throw new EntityNotFoundException(
                $"Сотрудник с логином {login.Value} не найден.");
        }

        contextEmployee.Password = password.Value;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeAsync(Id<Employee> employeeId, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(employeeId);
        token.ThrowIfCancellationRequested();

        var employee = await _context.Employees
            .SingleOrDefaultAsync(p => p.EmployeeId == employeeId.Value, token);

        if (employee is null)
        {
            throw new EntityNotFoundException(
                $"Сотрудник с id = {employeeId.Value} не найден.");
        }

        _context.Employees.Remove(employee);
    }

    private readonly ApplicationContext _context;
}
