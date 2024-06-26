﻿using Models.Autorization;
using Models.Requests.BaseRequests;

namespace Logic.Abstractions.Processors;

/// <summary>
/// Контракт процессора запросов с авторизацией с возвращаемыми данными.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
/// <typeparam name="TOutput">
/// Выходные данные.
/// </typeparam>
public interface IRequestProcessorWithAuthorize<TInput, TOutput> 
    where TInput : RequestBase
{
    /// <summary>
    /// Обрабатывает запрос.
    /// </summary>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="jwtToken">
    /// Токен авторизации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// <typeparamref name="TOutput"/>.
    /// </returns>
    public Task<TOutput> ProcessAsync(
        TInput request, 
        JwtToken jwtToken, 
        CancellationToken token);
}
