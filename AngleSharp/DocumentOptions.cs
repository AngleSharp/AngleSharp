using System;
using AngleSharp.Events;

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
        Boolean _validating;
		ParseErrorEventHandler _onerror;

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
        /// <param name="scripting">[Default: false] Scripting mode of HTML documents.</param>
        /// <param name="styling">[Default: true] Implicit CSS parsing in the HTML DOM.</param>
        /// <param name="embedded">[Default: false] Embedding mode of HTML documents.</param>
		/// <param name="validating">[Default: false] Validation of XML documents.</param>
		/// <param name="onerror">[Default: null] Error redirect.</param>
        public DocumentOptions(
            Boolean scripting = false,
            Boolean styling = true,
            Boolean embedded = false,
            Boolean validating = false,
			ParseErrorEventHandler onerror = null)
        {
            _scripting = scripting;
            _styling = styling;
            _embedded = embedded;
            _validating = validating;
			_onerror = onerror;
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

        /// <summary>
        /// Gets if XML documents should get validated. If validation is
        /// active XML documents have to provide and follow a valid DTD.
        /// Otherwise the generated tree will not be validated.
        /// </summary>
        public Boolean IsValidating
        {
            get { return _validating; }
        }

		/// <summary>
		/// Gets the delegate to call in case of an (tolerable) error. If this
		/// is null, then the default behavior is to print these errors on the
		/// debug console (if in debug mode) or to drop the errors completely.
		/// </summary>
		public ParseErrorEventHandler OnError
		{
			get { return _onerror; }
		}

        #endregion
    }
}
