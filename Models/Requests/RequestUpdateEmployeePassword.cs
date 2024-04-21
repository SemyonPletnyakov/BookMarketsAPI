using Models.Pagination;

namespace Models.Requests;

/// <summary>
/// Запрос на обновление пароля сотруднику.
/// </summary>
public record RequestUpdateEmployeePassword : RequestBase
{
    // <summary>
    /// Логин.
    /// </summary>
    public Login Login { get; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public Password Password { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestUpdateEmployeePassword"/>.
    /// </summary>
    /// <param name="login">
    /// Логин.
    /// </param>
    /// <param name="password">
    /// Пароль.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestUpdateEmployeePassword(
        Login login,
        Password password)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));

        Password = password
            ?? throw new ArgumentNullException(nameof(password));
    }
}
