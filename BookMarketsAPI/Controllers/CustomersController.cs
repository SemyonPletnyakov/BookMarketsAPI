using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models.Exceptions;
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
        IRequestProcessorWithoutAuthorize<RequestAddEntity<CustomerWithoutId>> addCustomerProcessor,
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
        try
        {
            var data = await _authorizeProcessor.ProcessAutorizeCustomerAsync(
                new(customerEmailAndPassword.Email),
                new(customerEmailAndPassword.Password),
                token);

            var transportData = new Transport.Models.JwtTokenAndUserId
            {
                JwtToken = data.JwtToken.Value,
                UserId = data.Id.Value
            };

            return Ok(transportData);
        }
        catch (AuthorizationException)
        {
            return Unauthorized();
        }
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
    [HttpGet("id")]
    [Authorize]
    public async Task<IActionResult> GetCustomerByIdAsync(
        int customerId,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var customer = await _getCustomerByIdProcessor.ProcessAsync(
                new(new(customerId)), 
                jwtToken, 
                token);

            var transportCustomer = new Transport.Models.ForUpdate.Customer
            {
                CustomerId = customer.CustomerId.Value,
                BirthDate = customer.BirthDate,
                Email = customer.Email.Value,
                Phone = customer.Phone?.Value,
                LastName = customer.FullName?.LastName,
                FirstName = customer.FullName?.FirstName,
                Patronymic = customer.FullName?.Patronymic
            };

            return Ok(transportCustomer);
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
    [Authorize]
    public async Task<IActionResult> GetCustomersAsync(
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var customers =
                (await _getCustomersProcessor.ProcessAsync(
                        new(new(size, number, order)),
                        jwtToken,
                        token))
                    .Select(customer =>
                        new Transport.Models.ForUpdate.Customer
                        {
                            CustomerId = customer.CustomerId.Value,
                            BirthDate = customer.BirthDate,
                            Email = customer.Email.Value,
                            Phone = customer.Phone?.Value,
                            LastName = customer.FullName?.LastName,
                            FirstName = customer.FullName?.FirstName,
                            Patronymic = customer.FullName?.Patronymic
                        }).ToArray();

            return Ok(customers);
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
    [HttpGet("last_name")]
    [Authorize]
    public async Task<IActionResult> GetCustomersByLastNameAsync(
        string lname, 
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var customers =
                (await _getCustomersByLastNameProcessor.ProcessAsync(
                        new(new(lname), new(size, number, order)),
                        jwtToken,
                        token))
                    .Select(customer =>
                        new Transport.Models.ForUpdate.Customer
                        {
                            CustomerId = customer.CustomerId.Value,
                            BirthDate = customer.BirthDate,
                            Email = customer.Email.Value,
                            Phone = customer.Phone?.Value,
                            LastName = customer.FullName?.LastName,
                            FirstName = customer.FullName?.FirstName,
                            Patronymic = customer.FullName?.Patronymic
                        }).ToArray();

            return Ok(customers);
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
    public async Task<IActionResult> GetCustomersByShopIdAndTimeIntervalAsync(
        int shopId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        int size,
        int number,
        CustomerSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var customers =
                (await _getCustomersByShopIdAndTimeIntervalProcessor.ProcessAsync(
                        new(new(shopId), new(size, number, order), startDate, endDate),
                        jwtToken,
                        token))
                    .Select(customer =>
                        new Transport.Models.ForUpdate.Customer
                        {
                            CustomerId = customer.CustomerId.Value,
                            BirthDate = customer.BirthDate,
                            Email = customer.Email.Value,
                            Phone = customer.Phone?.Value,
                            LastName = customer.FullName?.LastName,
                            FirstName = customer.FullName?.FirstName,
                            Patronymic = customer.FullName?.Patronymic
                        }).ToArray();

            return Ok(customers);
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
    public async Task<IActionResult> AddCustomerAsync(
        Transport.Models.ForCreate.Customer customer,
        CancellationToken token)
    {
        try
        {
            await _addCustomerProcessor.ProcessAsync(
                new(new(
                        customer.LastName == null || customer.FirstName == null
                            ? null
                            : new(customer.LastName, customer.FirstName, customer.Patronymic), 
                        customer.BirthDate, 
                        customer.Phone == null
                            ? null
                            : new(customer.Phone),
                        new(customer.Email),
                        new(customer.Password))),
                token);

            return Created();
        }
        catch (Exception)
        {
            return BadRequest();
        }
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
    [Authorize]
    public async Task<IActionResult> UpdateCustomerAsync(
        Transport.Models.FullModels.Customer customer,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateCustomerProcessor.ProcessAsync(
                new(new(
                    new(customer.CustomerId),
                    customer.LastName == null || customer.FirstName == null
                        ? null
                        : new(customer.LastName, customer.FirstName, customer.Patronymic),
                    customer.BirthDate,
                    customer.Phone == null
                        ? null
                        : new(customer.Phone),
                    new(customer.Email),
                    new(customer.Password))),
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
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    private readonly IAuthorizeRequestProcessor _authorizeProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetOneById<Customer, SimleCustomer>, SimleCustomer> _getCustomerByIdProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyWithPagination<CustomerSorting>, IList<SimleCustomer>> _getCustomersProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByLastNameWithPagination<CustomerSorting>, IList<SimleCustomer>> _getCustomersByLastNameProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdByTimeIntervalWithPagination<Shop, CustomerSorting>, IList<SimleCustomer>> _getCustomersByShopIdAndTimeIntervalProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestAddEntity<CustomerWithoutId>> _addCustomerProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<Customer>> _updateCustomerProcessor;
}
