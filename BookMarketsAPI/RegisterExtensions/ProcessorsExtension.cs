using Logic.Abstractions.Processors;
using Logic.Processors;

namespace BookMarketsAPI.RegisterExtensions;

public static class ProcessorsExtension
{
    public static IServiceCollection AddProcessors(
        this IServiceCollection services)
        => services.AddScoped<IAuthorizeRequestProcessor, AuthorizeRequestProcessor>()
            .AddScoped(
                typeof(IRequestProcessorWithoutAuthorize<>),
                typeof(RequestProcessorWithoutAuthorize<>))
            .AddScoped(
                typeof(IRequestProcessorWithoutAuthorize<,>),
                typeof(RequestProcessorWithoutAuthorize<,>))
            .AddScoped(
                typeof(IRequestProcessorWithAuthorize<>),
                typeof(RequestProcessorWithAuthorize<>))
            .AddScoped(
                typeof(IRequestProcessorWithAuthorize<,>),
                typeof(RequestProcessorWithAuthorize<,>));
}
