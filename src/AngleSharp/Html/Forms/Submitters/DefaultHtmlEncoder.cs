namespace AngleSharp.Html.Forms.Submitters
{
    using AngleSharp.Text;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// Represents the default HTML encoder.
    /// </summary>
    public class DefaultHtmlEncoder : IHtmlEncoder
    {
        /// <summary>
        /// Replaces characters in names and values that cannot be expressed by using the given
        /// encoding with &amp;#...; base-10 unicode point.
        /// </summary>
        /// <param name="value">The value to sanitize.</param>
        /// <param name="encoding">The encoding to consider.</param>
        /// <returns>The sanitized value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Encode(String value, Encoding encoding) => value.HtmlEncode(encoding);
    }
}
