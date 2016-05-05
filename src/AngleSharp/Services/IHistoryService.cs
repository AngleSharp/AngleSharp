namespace AngleSharp.Services
{
    using AngleSharp.Dom;

    /// <summary>
    /// Defines methods to create history instances.
    /// </summary>
    public interface IHistoryService : IService
    {
        /// <summary>
        /// Creates a history object for the given context.
        /// </summary>
        /// <param name="context">The context to track.</param>
        /// <returns>The object for storing states.</returns>
        IHistory CreateHistory(IBrowsingContext context);
    }
}
