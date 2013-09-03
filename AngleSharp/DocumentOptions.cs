using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents a set of possible options that might be applicable
    /// (or not) for a specific document generation.
    /// </summary>
    public class DocumentOptions
    {
        #region Members

        Boolean _scripting;
        Boolean _styling;
        Boolean _embedded;

        #endregion

        #region Default

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

        #endregion

        #region ctor

        /// <summary>
        /// Creates the document options. Use parameter names
        /// to set the options you care about.
        /// </summary>
        /// <param name="scripting">[Default] On if a scripting engine is available.</param>
        /// <param name="styling">[Default] On</param>
        /// <param name="embedded">[Default] Off</param>
        public DocumentOptions(
            Boolean scripting = false,
            Boolean styling = true,
            Boolean embedded = false)
        {
            _scripting = scripting;
            _styling = styling;
            _embedded = embedded;
        }

        #endregion

        #region Options

        /// <summary>
        /// Gets the current scripting mode.
        /// </summary>
        public Boolean IsScripting
        {
            get { return _scripting; }
        }

        /// <summary>
        /// Gets the current CSS mode. Usually CSS stylesheets and inline-
        /// definitions are parsed (can be deactivated here).
        /// </summary>
        public Boolean IsStyling
        {
            get { return _styling; }
        }

        /// <summary>
        /// Gets the current embedding mode. Normally the document is NOT
        /// embedded. Enabling embedding will emulate the document being rendered
        /// in an iframe.
        /// </summary>
        public Boolean IsEmbedded
        {
            get { return _embedded; }
        }

        #endregion
    }
}
