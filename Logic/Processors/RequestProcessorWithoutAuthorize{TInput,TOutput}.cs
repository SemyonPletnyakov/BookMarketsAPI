using Logic.Abstractions.Handlers;
using Logic.Abstractions.Processors;

using Models.Requests.BaseRequests;

namespace Logic.Processors;

/// <summary>
/// Процессор запросов с авторизацией.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
/// <typeparam name="TOutput">
/// Выходные данные.
/// </typeparam>
public sealed class RequestProcessorWithoutAuthorize<TInput,TOutput> :
    IRequestProcessorWithoutAuthorize<TInput, TOutput>
    where TInput : RequestBase
{
    /// <summary>
    /// Создаёт объект <see cref="RequestProcessorWithoutAuthorize{TInput,TOutput}"/>.
    /// </summary>
    /// <param name="ruleChecker">
    /// Сущность, проверяющая, 
    /// достаточно ли прав у пользователя на совершение операции.
    /// </param>
    /// <param name="requestHandler">
    /// Обработчик запроса.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="requestHandler"/> равен <see langword="null"/>.
    /// </exception>
    public RequestProcessorWithoutAuthorize(IRequestHandler<TInput, TOutput> requestHandler)
    {
        _requestHandler = requestHandler 
            ?? throw new ArgumentNullException(nameof(requestHandler));
    }

    /// <inheritdoc/>
    public Task<TOutput> ProcessAsync(
        TInput request,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);
        token.ThrowIfCancellationRequested();

        return _requestHandler.HandleAsync(request, token);
    }

    private readonly IRequestHandler<TInput, TOutput> _requestHandler;
}
