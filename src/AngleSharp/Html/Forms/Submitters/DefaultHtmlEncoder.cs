namespace AngleSharp.Html.Forms.Submitters
{
    using AngleSharp.Text;
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
        /// <param name="value">The value to sanatize.</param>
        /// <param name="encoding">The encoding to consider.</param>
        /// <returns>The sanatized value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Encode(string value, Encoding encoding)
        {
            return value.HtmlEncode(encoding);
        }
    }
}
