using Models.Requests;

namespace Logic.Abstractions.Handlers;

/// <summary>
/// Обработчик запросов.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
/// <typeparam name="TOutput">
/// Выходные данные.
/// </typeparam>
public interface IRequestHandler<TInput, TOutput>
    where TInput : RequestBase
{
    /// <summary>
    /// Обрабатывает запрос.
    /// </summary>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// <typeparamref name="TOutput"/>.
    /// </returns>
    public TOutput HandleAsync(
        TInput request,
        CancellationToken token);
}
