namespace AngleSharp.Services
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents a service to determine the default encoding.
    /// </summary>
    public interface IEncodingService : IService
    {
        /// <summary>
        /// Suggests the initial Encoding for the given locale.
        /// </summary>
        /// <param name="locale">
        /// The locale defined by the BCP 47 language tag.
        /// </param>
        /// <returns>The suggested encoding.</returns>
        Encoding Suggest(String locale);
    }
}
