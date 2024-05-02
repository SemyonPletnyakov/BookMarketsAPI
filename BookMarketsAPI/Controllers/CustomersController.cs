using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Logic.Abstractions.Processors;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using CustomerWithoutId = Models.ForCreate.Customer;
using SimleCustomer = Models.ForUpdate.Customer;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер покупателей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="CustomersController"/>.
    /// </summary>
    /// <param name="authorizeProcessor">
    /// Процессор авторизации.
    /// </param>
    /// <param name="getCustomerByIdProcessor">
    /// Процессор получения покупателя по идентификатору.
    /// </param>
    /// <param name="getCustomersProcessor">
    /// Процессор получения покупателей.
    /// </param>
    /// <param name="getCustomersByLastNameProcessor">
    /// Процессор получения покупателей по фамилии.
    /// </param>
    /// <param name="getCustomersByShopIdAndTimeIntervalProcessor">
    /// Процессор получения покупателей 
    /// по идентификатору магазина и времянному интервалу.
    /// </param>
    /// <param name="addCustomerProcessor">
    /// Процессор добавления покупателя.
    /// </param>
    /// <param name="updateCustomerProcessor">
    /// Процессор обновления покупателя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public CustomersController(
        IAuthorizeRequestProcessor authorizeProcessor,
        IRequestProcessorWithAuthorize<RequestGetOneById<Customer, SimleCustomer>, SimleCustomer> getCustomerByIdProcessor,
        IRequestProcessorWithAuthorize<RequestGetManyWithPagination<CustomerSorting>, IList<SimleCustomer>> getCustomersProcessor,
        IRequestProcessorWithAuthorize<RequestGetManyByLastNameWithPagination<CustomerSorting>, IList<SimleCustomer>> getCustomersByLastNameProcessor,
        IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Shop, CustomerSorting>, IList<SimleCustomer>> getCustomersByShopIdAndTimeIntervalProcessor,
        IRequestProcessorWithAuthorize<RequestAddEntity<CustomerWithoutId>> addCustomerProcessor,
        IRequestProcessorWithAuthorize<RequestUpdateEntity<Customer>> updateCustomerProcessor)
    {
        _authorizeProcessor = authorizeProcessor
            ?? throw new ArgumentNullException(nameof(authorizeProcessor));
        _getCustomerByIdProcessor = getCustomerByIdProcessor
            ?? throw new ArgumentNullException(nameof(getCustomerByIdProcessor));
        _getCustomersProcessor = getCustomersProcessor
            ?? throw new ArgumentNullException(nameof(getCustomersProcessor));
        _getCustomersByLastNameProcessor = getCustomersByLastNameProcessor
            ?? throw new ArgumentNullException(nameof(getCustomersByLastNameProcessor));
        _getCustomersByShopIdAndTimeIntervalProcessor = getCustomersByShopIdAndTimeIntervalProcessor
            ?? throw new ArgumentNullException(nameof(getCustomersByShopIdAndTimeIntervalProcessor));
        _addCustomerProcessor = addCustomerProcessor
            ?? throw new ArgumentNullException(nameof(addCustomerProcessor));
        _updateCustomerProcessor = updateCustomerProcessor
            ?? throw new ArgumentNullException(nameof(updateCustomerProcessor));
    }

    /// <summary>
    /// Авторизация покупателя.
    /// </summary>
    /// <param name="customerEmailAndPassword">
    /// Данные для входа.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// JWT-токен и идентификатор пользователя.
    /// </returns>
    [HttpPost("Authorize")]
    public async Task<IActionResult> AuthorizeCustomerAsync(
        Transport.Models.CustomerEmailAndPassword customerEmailAndPassword,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателя.
    /// </summary>
    /// <param name="customerId">
    /// Идентификатор покупателя.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатель.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetCustomerByIdAsync(
        int customerId,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей.
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
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetCustomersAsync(
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить покупателей по фамилии.
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
    /// Покупатели.
    /// </returns>
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetCustomersByLastNameAsync(
        string lname, 
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
    public async Task<IActionResult> GetCustomersByShopIdAndTimeIntervalAsync(
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
    /// Добавить покупателя.
    /// </summary>
    /// <param name="customer">
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
    public async Task<IActionResult> AddCustomerAsync(
        Transport.Models.ForCreate.Customer customer,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить покупателя.
    /// </summary>
    /// <param name="customer">
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
    public async Task<IActionResult> UpdateCustomerAsync(
        Transport.Models.FullModels.Customer customer,
        CancellationToken token)
    {

    }

    private readonly IAuthorizeRequestProcessor _authorizeProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetOneById<Customer, SimleCustomer>, SimleCustomer> _getCustomerByIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyWithPagination<CustomerSorting>, IList<SimleCustomer>> _getCustomersProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByLastNameWithPagination<CustomerSorting>, IList<SimleCustomer>> _getCustomersByLastNameProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Shop, CustomerSorting>, IList<SimleCustomer>> _getCustomersByShopIdAndTimeIntervalProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<CustomerWithoutId>> _addCustomerProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<Customer>> _updateCustomerProcessor;
}
