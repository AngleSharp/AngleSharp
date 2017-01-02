namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System.Collections.Generic;

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
    }
}
