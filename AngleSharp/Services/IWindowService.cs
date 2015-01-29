namespace AngleSharp.Services
{
    using AngleSharp.Dom;

    /// <summary>
    /// Defines methods to create IWindow instances.
    /// </summary>
    public interface IWindowService : IService
    {
        /// <summary>
        /// Creates an IWindow object for the provided document.
        /// </summary>
        /// <param name="document">The document that needs an IWindow.</param>
        /// <returns>The IWindow instance for the document.</returns>
        IWindow Create(IDocument document);
    }
}
