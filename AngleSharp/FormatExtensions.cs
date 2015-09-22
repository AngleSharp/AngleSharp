namespace AngleSharp
{
    using AngleSharp.Css;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Extensions for formatting, e.g., markup or styling.
    /// </summary>
    public static class FormatExtensions
    {
        /// <summary>
        /// Returns the (complete) CSS style representation of the node.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToCss(this IStyleFormattable style)
        {
            return style.ToCss(CssStyleFormatter.Instance);
        }

        /// <summary>
        /// Returns the (complete) HTML markup representation of the node.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToHtml(this IMarkupFormattable markup)
        {
            return markup.ToHtml(HtmlMarkupFormatter.Instance);
        }
    }
}
