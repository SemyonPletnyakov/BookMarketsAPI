using Models.FullEntities;

namespace Models.Autorization;


/// <summary>
/// Данные сотрудника для JWT-токена.
/// </summary>
public sealed record EmployeeAutorizationData
{
    /// <summary>
    /// Идентификатор сотрудника.
    /// </summary>
    public Id<Employee> Id { get; }

    /// <summary>
    /// Логин сотрудника.
    /// </summary>
    public Login Login { get; }

    /// <summary>
    /// Создаёт объект <see cref="EmployeeAutorizationData"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор сотрудника.
    /// </param>
    /// <param name="login">
    /// Логин сотрудника.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен null.
    /// </exception>
    public EmployeeAutorizationData(Id<Employee> id, Login login)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Login = login ?? throw new ArgumentNullException(nameof(login));
    }
}
