namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Navigator;

    /// <summary>
    /// Defines methods to create INavigator instances.
    /// </summary>
    public interface INavigatorService : IService
    {
        /// <summary>
        /// Creates an INavigator object for the provided window.
        /// </summary>
        /// <param name="window">The window that needs an INavigator.</param>
        /// <returns>The INavigator instance for the window.</returns>
        INavigator Create(IWindow window);
    }
}
