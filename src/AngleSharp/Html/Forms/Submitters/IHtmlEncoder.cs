namespace AngleSharp.Html.Forms.Submitters
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents the HTML encoder.
    /// </summary>
    public interface IHtmlEncoder
    {
        /// <summary>
        /// Replaces characters in names and values that cannot be expressed by using the given
        /// encoding with &amp;#...; base-10 unicode point.
        /// </summary>
        /// <param name="value">The value to sanatize.</param>
        /// <param name="encoding">The encoding to consider.</param>
        /// <returns>The sanatized value.</returns>
        String Encode(String value, Encoding encoding);
    }
}
