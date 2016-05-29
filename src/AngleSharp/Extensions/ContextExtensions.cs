namespace AngleSharp.Extensions
{
    using AngleSharp.Services;
    using System.Diagnostics;

    /// <summary>
    /// Useful methods for browsing contexts.
    /// </summary>
    [DebuggerStepThrough]
    static class ContextExtensions
    {
        public static TService CreateService<TService>(this IBrowsingContext context)
        {
            var factory = context.Configuration.GetFactory<IServiceFactory>();
            return factory.Create<TService>(context);
        }
    }
}
