namespace AngleSharp
{
    using AngleSharp.Css;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Threading.Tasks;

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
        public static String ToCss(this IStyleFormattable style) =>
            style.ToCss(CssStyleFormatter.Instance);

        /// <summary>
        /// Returns the (complete) CSS style representation of the node.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToCss(this IStyleFormattable style, IStyleFormatter formatter)
        {
            var sb = StringBuilderPool.Obtain();

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
        public static void ToCss(this IStyleFormattable style, TextWriter writer) =>
            style.ToCss(writer, CssStyleFormatter.Instance);

        /// <summary>
        /// Writes the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <param name="writer">The output target of the serialization.</param>
        public static Task ToCssAsync(this IStyleFormattable style, TextWriter writer) =>
            writer.WriteAsync(style.ToCss());

        /// <summary>
        /// Writes the serialization of the node guided by the formatter to the
        /// given stream.
        /// </summary>
        /// <param name="style">The style node to format.</param>
        /// <param name="stream">The output stream to use.</param>
        public static async Task ToCssAsync(this IStyleFormattable style, Stream stream)
        {
            using (var writer = new StreamWriter(stream))
            {
                await style.ToCssAsync(writer).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Returns the (complete) HTML markup representation of the node.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToHtml(this IMarkupFormattable markup) =>
            markup.ToHtml(HtmlMarkupFormatter.Instance);

        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToHtml(this IMarkupFormattable markup, IMarkupFormatter formatter)
        {
            var sb = StringBuilderPool.Obtain();

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
        public static void ToHtml(this IMarkupFormattable markup, TextWriter writer) =>
            markup.ToHtml(writer, HtmlMarkupFormatter.Instance);

        /// <summary>
        /// Returns a minified serialization of the node guided by the
        /// MinifyMarkupFormatter with the default options.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String Minify(this IMarkupFormattable markup) =>
            markup.ToHtml(new MinifyMarkupFormatter());

        /// <summary>
        /// Returns a prettified serialization of the node guided by the
        /// PrettyMarkupFormatter with the default options.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <returns>The source code snippet.</returns>
        public static String Prettify(this IMarkupFormattable markup) =>
            markup.ToHtml(new PrettyMarkupFormatter());

        /// <summary>
        /// Writes the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <param name="writer">The output target of the serialization.</param>
        public static Task ToHtmlAsync(this IMarkupFormattable markup, TextWriter writer) =>
            writer.WriteAsync(markup.ToHtml());

        /// <summary>
        /// Writes the serialization of the node guided by the formatter to the
        /// given stream.
        /// </summary>
        /// <param name="markup">The markup node to format.</param>
        /// <param name="stream">The output stream to use.</param>
        public static async Task ToHtmlAsync(this IMarkupFormattable markup, Stream stream)
        {
            using (var writer = new StreamWriter(stream))
            {
                await markup.ToHtmlAsync(writer).ConfigureAwait(false);
            }
        }
    }
}
