namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Html;
    using System;
    using System.IO;

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
        /// Returns the (complete) CSS style representation of the node.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToCss(this IStyleFormattable style, IStyleFormatter formatter)
        {
            var sb = Pool.NewStringBuilder();

            using (var writer = new StringWriter(sb))
            {
                style.ToCss(writer, formatter);
            }

            return sb.ToPool();
        }

        /// <summary>
        /// Writes the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <param name="writer">The output target of the serialization.</param>
        public static void ToCss(this IStyleFormattable style, TextWriter writer)
        {
            style.ToCss(writer, CssStyleFormatter.Instance);
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

        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToHtml(this IMarkupFormattable markup, IMarkupFormatter formatter)
        {
            var sb = Pool.NewStringBuilder();

            using (var writer = new StringWriter(sb))
            {
                markup.ToHtml(writer, formatter);
            }

            return sb.ToPool();
        }

        /// <summary>
        /// Writes the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <param name="writer">The output target of the serialization.</param>
        public static void ToHtml(this IMarkupFormattable markup, TextWriter writer)
        {
            markup.ToHtml(writer, HtmlMarkupFormatter.Instance);
        }
    }
}
