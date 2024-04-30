using Logic.Abstractions.Autorization;
using Logic.Abstractions.Handlers;
using Logic.Abstractions.Processors;

using Models;
using Models.Autorization;
using Models.FullEntities;
using Models.Requests;
using System.Security.Authentication;

namespace Logic.Processors;

/// <summary>
/// Процессор запросов авторизации.
/// </summary>
public sealed class AuthorizeRequestProcessor : IAuthorizeRequestProcessor
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorizeRequestProcessor"/>.
    /// </summary>
    /// <param name="jwtTokenConstructorForEmployee">
    /// Создатель JWT-токена для сотрудника.
    /// </param>
    /// <param name="jwtTokenConstructorForCustomer">
    /// Создатель JWT-токена для покупателя.
    /// </param>
    /// <param name="employeeHandler">
    /// Обработчик запроса на получения данных о сотруднике по логину.
    /// </param>
    /// <param name="customerHandler">
    /// Обработчик запроса на получения данных о покупателе по логину.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public AuthorizeRequestProcessor(
        IJwtTokenConstructor<EmployeeAutorizationData> jwtTokenConstructorForEmployee,
        IJwtTokenConstructor<CustomerAutorizationData> jwtTokenConstructorForCustomer,
        IRequestHandler<RequestGetOneByLogin<Employee>, Employee> employeeHandler,
        IRequestHandler<RequestGetOneByEmail<Customer>, Customer> customerHandler)
    {
        _jwtTokenConstructorForEmployee = jwtTokenConstructorForEmployee
            ?? throw new ArgumentNullException(nameof(jwtTokenConstructorForEmployee));

        _jwtTokenConstructorForCustomer = jwtTokenConstructorForCustomer
            ?? throw new ArgumentNullException(nameof(jwtTokenConstructorForCustomer));

        _employeeHandler = employeeHandler
            ?? throw new ArgumentNullException(nameof(employeeHandler));

        _customerHandler = customerHandler
            ?? throw new ArgumentNullException(nameof(customerHandler));
    }

    /// <inheritdoc/>
    public async Task<JwtToken> ProcessAutorizeEmployeeAsync(
        Login login,
        Password password,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(login);
        ArgumentNullException.ThrowIfNull(password);
        token.ThrowIfCancellationRequested();

        var employee = await _employeeHandler.HandleAsync(new(login), token);

        if (employee.Password != password)
        {
            throw new AuthenticationException("Неверный логин или пароль.");
        }

        return _jwtTokenConstructorForEmployee.Construct(
            new(employee.EmployeeId, employee.Login));
    }

    /// <inheritdoc/>
    public async Task<JwtToken> ProcessAutorizeCustomerAsync(
        Email email,
        Password password,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);
        token.ThrowIfCancellationRequested();

        var customer = await _customerHandler.HandleAsync(new(email), token);

        if (customer.Password != password)
        {
            throw new AuthenticationException("Неверный логин или пароль.");
        }

        return _jwtTokenConstructorForCustomer.Construct(
            new(customer.CustomerId, customer.Email));
    }

    private readonly IJwtTokenConstructor<EmployeeAutorizationData> _jwtTokenConstructorForEmployee;
    private readonly IJwtTokenConstructor<CustomerAutorizationData> _jwtTokenConstructorForCustomer;
    private readonly IRequestHandler<RequestGetOneByLogin<Employee>, Employee> _employeeHandler;
    private readonly IRequestHandler<RequestGetOneByEmail<Customer>, Customer> _customerHandler;
}
