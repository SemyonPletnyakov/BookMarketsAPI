using Logic.Abstractions.Autorization;
using Logic.Abstractions.Handlers;
using Logic.Abstractions.Processors;

using Models.Autorization;
using Models.Requests.BaseRequests;

namespace Logic.Processors;

/// <summary>
/// Процессор запросов с авторизацией.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
public sealed class RequestProcessorWithAuthorize<TInput> :
    IRequestProcessorWithAuthorize<TInput>
    where TInput : RequestBase
{
    /// <summary>
    /// Создаёт объект <see cref="RequestProcessorWithAuthorize{TInput}"/>.
    /// </summary>
    /// <param name="ruleChecker">
    /// Сущность, проверяющая, 
    /// достаточно ли прав у пользователя на совершение операции.
    /// </param>
    /// <param name="requestHandler">
    /// Обработчик запроса.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestProcessorWithAuthorize(
        IRuleChecker ruleChecker, 
        IRequestHandler<TInput> requestHandler)
    {
        _ruleChecker = ruleChecker 
            ?? throw new ArgumentNullException(nameof(ruleChecker));

        _requestHandler = requestHandler 
            ?? throw new ArgumentNullException(nameof(requestHandler));
    }

    /// <inheritdoc/>
    public async Task ProcessAsync(
        TInput request,
        JwtToken jwtToken,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(jwtToken);
        token.ThrowIfCancellationRequested();

        await _ruleChecker.CheckRuleAsync(jwtToken, request.OperationDescriprion, token);

        await _requestHandler.HandleAsync(request, token);
    }

    private readonly IRuleChecker _ruleChecker;
    private readonly IRequestHandler<TInput> _requestHandler;
}
