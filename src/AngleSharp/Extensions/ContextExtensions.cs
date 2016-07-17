namespace AngleSharp.Extensions
{
    using AngleSharp.Services;

    /// <summary>
    /// Useful methods for browsing contexts.
    /// </summary>
    static class ContextExtensions
    {
        public static TService CreateService<TService>(this IBrowsingContext context)
        {
            var factory = context.Configuration.GetFactory<IServiceFactory>();
            return factory.Create<TService>(context);
        }
    }
}
