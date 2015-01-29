namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The Location interface represents the location of the object it is
    /// linked to. Changes done on it are reflected on the object it relates
    /// to. 
    /// </summary>
    [DomName("Location")]
    public interface ILocation : IUrlUtilities
    {
        /// <summary>
        /// Loads the resource at the URL provided in parameter.
        /// </summary>
        /// <param name="url">The path to the resource.</param>
        [DomName("assign")]
        void Assign(String url);

        /// <summary>
        /// Replaces the current resource with the one at the provided URL. The
        /// difference from the assign() method is that after using replace()
        /// the current page will not be saved in session History, meaning the
        /// user won't be able to use the back button to navigate to it. 
        /// </summary>
        /// <param name="url">
        /// The path to the resource that should replace the current resource.
        /// </param>
        [DomName("replace")]
        void Replace(String url);

        /// <summary>
        /// Reloads the resource from the current URL.
        /// </summary>
        [DomName("reload")]
        void Reload();
    }
}
