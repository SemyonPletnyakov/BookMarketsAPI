using Microsoft.Extensions.Options;

using System.Diagnostics;

namespace Logic.Autorization;

/// <summary>
/// Валидатор для <see cref="AuthConfiguration"/>.
/// </summary>
public sealed class AuthConfigurationValidator :
    IValidateOptions<AuthConfiguration>
{
    /// <inheritdoc/>
    public ValidateOptionsResult Validate(
        string? name,
        AuthConfiguration options)
    {
        Debug.Assert(options is not null);

        var errors = Enumerable.Empty<string>();

        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            errors = errors.Append(
                "Issuer can not be null, " +
                "empty or contain only whitespaces.");
        }

        if (string.IsNullOrWhiteSpace(options.Audience))
        {
            errors = errors.Append(
                "Audience can not be null, " +
                "empty or contain only whitespaces.");
        }

        if (string.IsNullOrWhiteSpace(options.Key))
        {
            errors = errors.Append(
                "Key can not be null, " +
                "empty or contain only whitespaces.");
        }

        if (options.Lifetime <= 0)
        {
            errors = errors.Append(
                "Lifetime must be greater than zero.");
        }

        return errors.Any()
            ? ValidateOptionsResult.Fail(errors)
            : ValidateOptionsResult.Success;
    }
}