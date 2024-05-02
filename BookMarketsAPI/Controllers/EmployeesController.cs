using Logic.Abstractions.Processors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using EmployeeWithoutId = Models.ForCreate.Employee;
using SimleEmployee = Models.ForUpdate.Employee;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер сотрудников.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeesController"/>.
    /// </summary>
    /// <param name="authorizeProcessor">
    /// Процессор авторизации.
    /// </param>
    /// <param name="getEmployeeByIdProcessor">
    /// Процессор получения сотрудника по идентификатору.
    /// </param>
    /// <param name="getEmployeesProcessor">
    /// Процессор получения сотрудников.
    /// </param>
    /// <param name="getEmployeesByLastNameProcessor">
    /// Процессор получения сотрудников по фамилии.
    /// </param>
    /// <param name="getEmployeesByShopIdProcessor">
    /// Процессор получения сотрудников по идентификатору магазина.
    /// </param>
    /// <param name="getEmployeesByWarehouseIdProcessor">
    /// Процессор получения сотрудников по идентификатору склада.
    /// </param>
    /// <param name="addEmployeeProcessor">
    /// Процессор добавления сотрудника.
    /// </param>
    /// <param name="updateEmployeeProcessor">
    /// Процессор обновления сотрудника.
    /// </param>
    /// <param name="updateEmployeePasswordProcessor">
    /// Процессор обновления пароля сотрудника.
    /// </param>
    /// <param name="deleteEmployeeProcessor">
    /// Процессор удаления сотрудника.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public EmployeesController(
        IAuthorizeRequestProcessor authorizeProcessor, 
        IRequestProcessorWithAuthorize<RequestGetOneById<Employee, SimleEmployee>, SimleEmployee> getEmployeeByIdProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyWithPagination<EmployeeSorting>, IList<SimleEmployee>> getEmployeesProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByLastNameWithPagination<EmployeeSorting>, IList<SimleEmployee>> getEmployeesByLastNameProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, EmployeeSorting>, IList<SimleEmployee>> getEmployeesByShopIdProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Warehouse, EmployeeSorting>, IList<SimleEmployee>> getEmployeesByWarehouseIdProcessor, 
        IRequestProcessorWithAuthorize<RequestAddEntity<RequestAddEntity<EmployeeWithoutId>>> addEmployeeProcessor, 
        IRequestProcessorWithAuthorize<RequestUpdateEntity<SimleEmployee>> updateEmployeeProcessor, 
        IRequestProcessorWithAuthorize<RequestUpdateEmployeePassword> updateEmployeePasswordProcessor, 
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Employee, Employee>> deleteEmployeeProcessor)
    {
        _authorizeProcessor = authorizeProcessor 
            ?? throw new ArgumentNullException(nameof(authorizeProcessor));
        _getEmployeeByIdProcessor = getEmployeeByIdProcessor 
            ?? throw new ArgumentNullException(nameof(getEmployeeByIdProcessor));
        _getEmployeesProcessor = getEmployeesProcessor 
            ?? throw new ArgumentNullException(nameof(getEmployeesProcessor));
        _getEmployeesByLastNameProcessor = getEmployeesByLastNameProcessor 
            ?? throw new ArgumentNullException(nameof(getEmployeesByLastNameProcessor));
        _getEmployeesByShopIdProcessor = getEmployeesByShopIdProcessor 
            ?? throw new ArgumentNullException(nameof(getEmployeesByShopIdProcessor));
        _getEmployeesByWarehouseIdProcessor = getEmployeesByWarehouseIdProcessor 
            ?? throw new ArgumentNullException(nameof(getEmployeesByWarehouseIdProcessor));
        _addEmployeeProcessor = addEmployeeProcessor 
            ?? throw new ArgumentNullException(nameof(addEmployeeProcessor));
        _updateEmployeeProcessor = updateEmployeeProcessor 
            ?? throw new ArgumentNullException(nameof(updateEmployeeProcessor));
        _updateEmployeePasswordProcessor = updateEmployeePasswordProcessor 
            ?? throw new ArgumentNullException(nameof(updateEmployeePasswordProcessor));
        _deleteEmployeeProcessor = deleteEmployeeProcessor 
            ?? throw new ArgumentNullException(nameof(deleteEmployeeProcessor));
    }

    /// <summary>
    /// Авторизация покупателя.
    /// </summary>
    /// <param name="employeeLoginAndPassword">
    /// Данные для входа.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// JWT-токен и идентификатор пользователя.
    /// </returns>
    [HttpPost("Authorize")]
    public async Task<IActionResult> AuthorizeEmployeeAsync(
        Transport.Models.EmployeeLoginAndPassword employeeLoginAndPassword,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить сотрудника.
    /// </summary>
    /// <param name="employeeId">
    /// Идентификатор сотрудника.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Сотрудник.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetEmployeeByIdAsync(
        int employeeId,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить сотрудников.
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
    /// Сотрудники.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetEmployeesAsync(
        int size,
        int number,
        EmployeeSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить сотрудников по фамилии.
    /// </summary>
    /// <param name="lname">
    /// Фамилия.
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
    /// Сотрудники.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetEmployeesByLastNameAsync(
        string lname,
        int size,
        int number,
        EmployeeSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить сотрудников идентификатору магазина.
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
    /// Сотрудники.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetEmployeesByShopIdAsync(
        int shopId,
        int size,
        int number,
        EmployeeSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить сотрудников идентификатору склада.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
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
    /// Сотрудники.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetEmployeesByWarehouseIdAsync(
        int warehouseId,
        int size,
        int number,
        EmployeeSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Добавить сотрудника.
    /// </summary>
    /// <param name="employee">
    /// Покупатель.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPost]
    //авторизация
    public async Task<IActionResult> AddEmployeeAsync(
        Transport.Models.ForCreate.Employee employee,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить сотрудника.
    /// </summary>
    /// <param name="employee">
    /// Покупатель.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateEmployeeAsync(
        Transport.Models.ForUpdate.Employee employee,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить пароль сотрудника.
    /// </summary>
    /// <param name="employeeLoginAndPassword">
    /// Логин и пароль сотрудника.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateEmployeePasswordAsync(
        Transport.Models.EmployeeLoginAndPassword employeeLoginAndPassword,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Удалить сотрудника.
    /// </summary>
    /// <param name="employeeId">
    /// Идентификатор сотрудника.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpDelete]
    //авторизация
    public async Task<IActionResult> DeleteEmployeeAsync(
        Transport.Models.Ids.Employee employeeId,
        CancellationToken token)
    {

    }

    private readonly IAuthorizeRequestProcessor _authorizeProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetOneById<Employee, SimleEmployee>, SimleEmployee> _getEmployeeByIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyWithPagination<EmployeeSorting>, IList<SimleEmployee>> _getEmployeesProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByLastNameWithPagination<EmployeeSorting>, IList<SimleEmployee>> _getEmployeesByLastNameProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, EmployeeSorting>, IList<SimleEmployee>> _getEmployeesByShopIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Warehouse, EmployeeSorting>, IList<SimleEmployee>> _getEmployeesByWarehouseIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<RequestAddEntity<EmployeeWithoutId>>> _addEmployeeProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<SimleEmployee>> _updateEmployeeProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEmployeePassword> _updateEmployeePasswordProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Employee, Employee>> _deleteEmployeeProcessor;
}
