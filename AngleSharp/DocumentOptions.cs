using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents a set of possible options that might be applicable
    /// (or not) for a specific document generation.
    /// </summary>
    public class DocumentOptions
    {
        static readonly DocumentOptions _defaultOptions;

        static DocumentOptions()
        {
            _defaultOptions = new DocumentOptions();
        }

        /// <summary>
        /// Gets the default options. Changing the properties of the
        /// default options will change the default parameters.
        /// </summary>
        public static DocumentOptions Default
        {
            get { return _defaultOptions; }
        }
    }
}
