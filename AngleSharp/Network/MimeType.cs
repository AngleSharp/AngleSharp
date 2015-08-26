namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an Internet media type.
    /// </summary>
    public class MimeType
    {
        #region Fields

        readonly String _general;
        readonly String _media;
        readonly String _suffix;
        readonly String _params;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new MIME type.
        /// </summary>
        /// <param name="value">The serialized value.</param>
        public MimeType(String value)
        {
            var slash = 0;

            while (slash < value.Length && value[slash] != '/')
                slash++;

            var plus = slash;

            while (plus < value.Length && value[plus] != '+')
                plus++;

            var semicolon = plus < value.Length ? plus : slash;

            while (semicolon < value.Length && value[semicolon] != ';')
                semicolon++;

            _general = value.Substring(0, slash);
            _media = slash < value.Length ? value.Substring(slash + 1, Math.Min(plus, semicolon) - slash - 1) : String.Empty;
            _suffix = plus < value.Length ? value.Substring(plus + 1, semicolon - plus - 1) : String.Empty;
            _params = semicolon < value.Length ? value.Substring(semicolon + 1) : String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the general type.
        /// </summary>
        public String GeneralType
        {
            get { return _general; }
        }

        /// <summary>
        /// Gets the media type, if specified.
        /// </summary>
        public String MediaType
        {
            get { return _media; }
        }

        /// <summary>
        /// Gets the suffix, if any.
        /// </summary>
        public String Suffix
        {
            get { return _suffix; }
        }

        /// <summary>
        /// Gets an iterator over all integrated keys.
        /// </summary>
        public IEnumerable<String> Keys
        {
            get
            {
                return _params.Split(';').
                               Where(m => !String.IsNullOrEmpty(m)).
                               Select(m => m.IndexOf('=') >= 0 ? m.Substring(0, m.IndexOf('=')) : m);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value of the parameter with the specified key.
        /// </summary>
        /// <param name="key">The parameter's key.</param>
        /// <returns>The value of the parameter or null.</returns>
        public String GetParameter(String key)
        {
            return _params.Split(';').
                           Where(m => m.StartsWith(key + "=")).
                           Select(m => m.Substring(m.IndexOf('=') + 1)).
                           FirstOrDefault();
        }

        /// <summary>
        /// Returns the string representation of the MIME type.
        /// </summary>
        /// <returns>The currently stored MIME type.</returns>
        public override String ToString()
        {
            if (_media.Length == 0 && _suffix.Length == 0 && _params.Length == 0)
                return _general;

            var front = String.Concat(_general, "/", _media);
            var back = _suffix.Length > 0 ? "+" + _suffix : String.Empty;
            var opt = _params.Length > 0 ? ";" + _params : String.Empty;
            return String.Concat(front, back, opt);
        }

        #endregion
    }
}
