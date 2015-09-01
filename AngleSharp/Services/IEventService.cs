namespace AngleSharp.Services
{
    using AngleSharp.Dom;

    /// <summary>
    /// Defines methods to create event loops.
    /// </summary>
    public interface IEventService : IService
    {
        /// <summary>
        /// Creates an IEventLoop object for the provided context.
        /// </summary>
        /// <param name="context">The host context of the event loop.</param>
        /// <returns>The IEventLoop for the context.</returns>
        IEventLoop Create(IBrowsingContext context);
    }
}
