using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using SimpleEmployee = Models.SimpleEntities.Employee;
using EmployeeWothoutId = Models.ForCreate.Employee;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория сотрудников.
/// </summary>
public interface IEmployeesRepository
{
    /// <summary>
    /// Получить часть сотрудников в соотвествии с пагинацией.
    /// </summary>
    /// <param name="pagginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка сотрудников.
    /// </returns>
    public Task<IList<SimpleEmployee>> GetEmployeesAsync(
        PaginationInfo<EmployeeSorting> pagginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск сотрудников по фамилии в соотвествии с пагинацией.
    /// </summary>
    /// <param name="lastName">
    /// Фамилия.
    /// </param>
    /// <param name="pagginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка сотрудников.
    /// </returns>
    public Task<IList<SimpleEmployee>> GetEmployeesByLastNameAsync(
        LastName lastName,
        PaginationInfo<EmployeeSorting> pagginationInfo,
        CancellationToken token);

    /// <summary>
    /// Дать всю инофрмацию о сотруднике по логину.
    /// </summary>
    /// <param name="login">
    /// Логин от учётной записи.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Полная информация о сотруднике.
    /// </returns>
    public Task<Employee> GetEmployeeByLoginAsync(
        Login login,
        CancellationToken token);

    /// <summary>
    /// Дать инофрмацию о сотруднике по идентификатору.
    /// </summary>
    /// <param name="employeeId">
    /// Идентификатор сотрудника.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Информация о сотруднике.
    /// </returns>
    public Task<SimpleEmployee> GetEmployeeByIdAsync(
        Id<Employee> employeeId,
        CancellationToken token);

    /// <summary>
    /// Получить часть сотрудников конкретного магазина в соотвествии с пагинацией.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="pagginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка сотрудников.
    /// </returns>
    public Task<IList<SimpleEmployee>> GetEmployeesByShopAsync(
        Id<Shop> shopId,
        PaginationInfo<EmployeeSorting> pagginationInfo,
        CancellationToken token);

    /// <summary>
    /// Получить часть сотрудников конкретного склада в соотвествии с пагинацией.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
    /// <param name="pagginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка сотрудников.
    /// </returns>
    public Task<IList<SimpleEmployee>> GetEmployeesByWarehouseAsync(
        Id<Warehouse> warehouseId,
        PaginationInfo<EmployeeSorting> pagginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление сотрудника.
    /// </summary>
    /// <param name="employee">
    /// Сотрудник.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddEmployeeAsync(EmployeeWothoutId employee, CancellationToken token);

    /// <summary>
    /// Изменение сотрудника.
    /// </summary>
    /// <param name="employee">
    /// Сотрудник.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateEmployeeAsync(SimpleEmployee employee, CancellationToken token);

    /// <summary>
    /// Изменение пароля сотрудника.
    /// </summary>
    /// <param name="login">
    /// Логин от учётной записи.
    /// </param>
    /// <param name="password">
    /// Пароль от учётной записи.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateEmployeePasswordAsync(
        Login login, 
        Password password, 
        CancellationToken token);

    /// <summary>
    /// Удаление сотрудника.
    /// </summary>
    /// <param name="employeeId">
    /// Идентификатор сотрудника.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task DeleteEmployeeAsync(Id<Employee> employeeId, CancellationToken token);
}
