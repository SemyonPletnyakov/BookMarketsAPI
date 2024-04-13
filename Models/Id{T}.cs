namespace Models;

/// <summary>
/// Идентификатор для типа.
/// </summary>
/// <typeparam name="T">
/// Тип, для которого создаётся идентификатор.
/// </typeparam>
/// <param name="Value">
/// Значение идентификатора.
/// </param>
public sealed record Id<T>(int Value);
