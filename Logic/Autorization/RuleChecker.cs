using Logic.Abstractions.Autorization;

using Models;
using Models.Autorization;
using Models.Exceptions;
using Models.FullEntities;
using Models.Requests.Operations;
using Models.Requests.Operations.Types;

using Storage.Abstractions;

using OrderWithoutId = Models.ForCreate.Order;

namespace Logic.Autorization;

/// <summary>
/// Сущность, проверяющая, 
/// достаточно ли прав у пользователя на совершение операции.
/// </summary>
public sealed class RuleChecker : IRuleChecker
{
    /// <summary>
    /// Создаёт объект <see cref="RuleChecker"/>.
    /// </summary>
    /// <param name="tokenDeconstructor">
    /// Сущность, которая достаёт из JWT-токена данные пользователя.
    /// </param>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RuleChecker(JwtTokenDeconstructor tokenDeconstructor, IUnitOfWork unitOfWork)
    {
        _tokenDeconstructor = tokenDeconstructor 
            ?? throw new ArgumentNullException(nameof(tokenDeconstructor));

        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task CheckRuleAsync(
        JwtToken jwtToken,
        OperationDescriprion operationDescriprion,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(jwtToken);
        ArgumentNullException.ThrowIfNull(operationDescriprion);
        token.ThrowIfCancellationRequested();

        var userData = _tokenDeconstructor.Deconstruct(jwtToken);

        return userData switch
        {
            EmployeeAutorizationData data => CheckEmployeeRuleAsync(data, operationDescriprion, token),

            CustomerAutorizationData data => CheckCustomerRuleAsync(data, operationDescriprion, token),

            _ => throw new InvalidOperationException("Не поддерживаемый тип пользователя.")
        };
    }

    private async Task CheckEmployeeRuleAsync(
        EmployeeAutorizationData userData,
        OperationDescriprion operationDescriprion,
        CancellationToken token)
    {
        //Проверка наличия покупателя с таким id
        var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(userData.Id, token);

        _ = (operationDescriprion.OperationType, operationDescriprion.EntityType) switch
        {
            (OperationType.Get, EntityType.Warehouse) => true,

            (OperationType.Add, EntityType.Author)
            or (OperationType.Update, EntityType.Author)
            or (OperationType.Delete, EntityType.Author)
            or (OperationType.Add, EntityType.Product)
            or (OperationType.Update, EntityType.Product)
            or (OperationType.Delete, EntityType.Product)
            or (OperationType.Add, EntityType.Book)
            or (OperationType.Update, EntityType.Book)
            or (OperationType.Delete, EntityType.Book)
                when employee.JobTitle.Value is JOB_TITLE_DIRECTOR or JOB_TITLE_MANAGER => true,

            (OperationType.GetOrAdd, EntityType.Address)
            or (OperationType.Add, EntityType.Shop)
            or (OperationType.Update, EntityType.Shop)
            or (OperationType.Delete, EntityType.Shop)
            or (OperationType.Add, EntityType.Warehouse)
            or (OperationType.Update, EntityType.Warehouse)
            or (OperationType.Delete, EntityType.Warehouse)
            or (OperationType.Add, EntityType.Employee)
            or (OperationType.Update, EntityType.Employee)
            or (OperationType.Delete, EntityType.Employee)
            or (OperationType.Get, EntityType.Order)
                when employee.JobTitle.Value is JOB_TITLE_DIRECTOR => true,

            (OperationType.Get, EntityType.ProductCount)
            or (OperationType.Add, EntityType.ProductCount)
            or (OperationType.Update, EntityType.ProductCount)
            or (OperationType.Delete, EntityType.ProductCount)
                when employee.JobTitle.Value is JOB_TITLE_DIRECTOR => true,

            (OperationType.Get, EntityType.ProductCount)
            or (OperationType.Update, EntityType.ProductCount)
                when employee.JobTitle.Value is JOB_TITLE_MANAGER or JOB_TITLE_EMPLOYEE
                    && ((operationDescriprion is OperationDescriptionWithTargetEntity<Id<Shop>> descShop 
                            && await CheckEmployeeIdInShopAsync(employee.EmployeeId, descShop.Entity, token))
                        || (operationDescriprion is OperationDescriptionWithTargetEntity<Id<Warehouse>> descWarehouse
                            && await CheckEmployeeIdInWarehouseAsync(employee.EmployeeId, descWarehouse.Entity, token))
                => true,

            (OperationType.Get, EntityType.Customer)
                when operationDescriprion is not OperationDescriptionWithTargetEntity<Login>
                    && employee.JobTitle.Value is JOB_TITLE_DIRECTOR 
                => true,

            (OperationType.Get, EntityType.Order)
                when operationDescriprion is OperationDescriptionWithTargetEntity<Id<Shop>> desc
                    && await CheckEmployeeIdInShopAsync(employee.EmployeeId, desc.Entity, token) 
                => true,

            (OperationType.Update, EntityType.Order)
                when employee.JobTitle.Value is JOB_TITLE_EMPLOYEE or JOB_TITLE_MANAGER
                    && operationDescriprion is OperationDescriptionWithTargetEntity<Id<Order>> desc 
                    && await CheckEmployeeCanReadUpdateOrderAsync(employee.EmployeeId, desc.Entity, token) 
                => true,

            _ => throw new NotEnoughRightsException("Недостаточно прав")
        };
    }

    private async Task CheckCustomerRuleAsync(
        CustomerAutorizationData userData,
        OperationDescriprion operationDescriprion,
        CancellationToken token)
    {
        //Проверка наличия покупателя с таким id
        _ = await _unitOfWork.Customers.GetCustomerByIdAsync(userData.Id, token);

        _ = (operationDescriprion.OperationType, operationDescriprion.EntityType) switch
        {
            (OperationType.Add, EntityType.Order) 
                when operationDescriprion is OperationDescriptionWithTargetEntity<OrderWithoutId> desc 
                    && userData.Id == desc.Entity.CustomerId => true,

            (OperationType.Get, EntityType.Order)
                when operationDescriprion is OperationDescriptionWithTargetEntity<Id<Order>> desc 
                    && await CheckCustomerIdInOrderAsync(userData.Id, desc.Entity, token) => true,

            (OperationType.Get, EntityType.Customer)
                when operationDescriprion is OperationDescriptionWithTargetEntity<Id<Customer>> desc 
                    && userData.Id == desc.Entity => true,

            (OperationType.Update, EntityType.Customer)
                when operationDescriprion is OperationDescriptionWithTargetEntity<Customer> desc 
                    && userData.Id == desc.Entity.CustomerId => true,

            _ => throw new NotEnoughRightsException("Недостаточно прав")
        };
    }

    private async Task<bool> CheckCustomerIdInOrderAsync(
        Id<Customer> customerId, 
        Id<Order> orderId, 
        CancellationToken token)
        => (await _unitOfWork.Orders.GetOrderByOrderIdAsync(orderId, token)).Customer.CustomerId == customerId;

    private async Task<bool> CheckEmployeeIdInShopAsync(
        Id<Employee> employeeId,
        Id<Shop> shopId,
        CancellationToken token)
        => (await _unitOfWork.Shops.GetShopIdWhereDoesEmployeeWorkAsync(employeeId, token)) == shopId;

    private async Task<bool> CheckEmployeeIdInWarehouseAsync(
        Id<Employee> employeeId,
        Id<Warehouse> warehouseId,
        CancellationToken token)
        => (await _unitOfWork.Warehouses.GetWarehouseIdWhereDoesEmployeeWorkAsync(employeeId, token)) == warehouseId;

    private async Task<bool> CheckEmployeeCanReadUpdateOrderAsync(
        Id<Employee> employeeId,
        Id<Order> orderId,
        CancellationToken token)
        => await CheckEmployeeIdInShopAsync(
            employeeId, 
            (await _unitOfWork.Orders.GetOrderByOrderIdAsync(orderId, token)).Shop.ShopId,
            token);

    private readonly IJwtTokenDeconstructor _tokenDeconstructor;
    private readonly IUnitOfWork _unitOfWork;

    private const string JOB_TITLE_DIRECTOR = "Директор";
    private const string JOB_TITLE_MANAGER = "Управляющий";
    private const string JOB_TITLE_EMPLOYEE = "Работник";
}
