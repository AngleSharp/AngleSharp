namespace AngleSharp.Services
{
    using AngleSharp.Dom;

    /// <summary>
    /// Defines methods to create event loops.
    /// </summary>
    public interface IEventService : IService
    {
        /// <summary>
        /// Creates an IEventLoop object for the document.
        /// </summary>
        /// <param name="document">The host of the event loop.</param>
        /// <returns>The IEventLoop for the domain.</returns>
        IEventLoop Create(IDocument document);
    }
}
