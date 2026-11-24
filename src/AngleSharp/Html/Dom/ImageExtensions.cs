namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful extension methods for the HtmlImageElement.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Gathers the source elements for the provided image element.
        /// </summary>
        /// <param name="img">The image to extend.</param>
        /// <returns>The stack of source elements.</returns>
        public static Stack<IHtmlSourceElement> GetSources(this IHtmlImageElement img)
        {
            var parent = img.ParentElement;
            var sources = new Stack<IHtmlSourceElement>();

            if (parent != null && parent.LocalName.Is(TagNames.Picture))
            {
                var element = img.PreviousElementSibling as IHtmlSourceElement;

                while (element != null)
                {
                    sources.Push(element);
                    element = element.PreviousElementSibling as IHtmlSourceElement;
                }
            }

            return sources;
        }

        /// <summary>
        /// Loads the image of the element from the given source, unless it loads that one
        /// already. An image element requests nothing by itself: what shows it (e.g., a
        /// renderer) picks the candidate of its srcset fitting the viewport and loads it once
        /// it is needed. The element fires load or error when done, and the load event of its
        /// document waits for it while the document is still loading. Without a resource
        /// loader nothing is loaded.
        /// </summary>
        /// <param name="img">The image to load.</param>
        /// <param name="source">The absolute URL of the image to load.</param>
        /// <returns>The task completing once the image has been loaded and processed.</returns>
        public static Task LoadAsync(this IHtmlImageElement img, Url source) => img.LoadAsync(source, 1.0);

        /// <summary>
        /// Loads the image of the element from the given source (see
        /// <see cref="LoadAsync(IHtmlImageElement, Url)"/>), whose pixels have the given density,
        /// e.g. 2 for the 2x candidate of a srcset: the natural size of the image (its
        /// naturalWidth and naturalHeight) is divided by it, as in browsers.
        /// </summary>
        /// <param name="img">The image to load.</param>
        /// <param name="source">The absolute URL of the image to load.</param>
        /// <param name="density">The density of the pixels of the image.</param>
        /// <returns>The task completing once the image has been loaded and processed.</returns>
        public static Task LoadAsync(this IHtmlImageElement img, Url source, Double density)
        {
            if (img is null)
            {
                throw new ArgumentNullException(nameof(img));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return img is HtmlImageElement element ? element.LoadAsync(source, density) : Task.CompletedTask;
        }
    }
}
