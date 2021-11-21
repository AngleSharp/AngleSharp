namespace AngleSharp.Io
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an Internet media type.
    /// </summary>
    public class MimeType : IEquatable<MimeType>
    {
        #region Fields

        private readonly String _general;
        private readonly String _media;
        private readonly String _suffix;
        private readonly String _params;

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
            {
                slash++;
            }

            var plus = slash;

            while (plus < value.Length && value[plus] != '+')
            {
                plus++;
            }

            var semicolon = plus < value.Length ? plus : slash;

            while (semicolon < value.Length && value[semicolon] != ';')
            {
                semicolon++;
            }

            _general = value.Substring(0, slash);
            _media = slash < value.Length ? value.Substring(slash + 1, Math.Min(plus, semicolon) - slash - 1) : String.Empty;
            _suffix = plus < value.Length ? value.Substring(plus + 1, semicolon - plus - 1) : String.Empty;
            _params = semicolon < value.Length ? value.Substring(semicolon + 1).StripLeadingTrailingSpaces() : String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the content part, i.e. everything without the parameters.
        /// </summary>
        public String Content
        {
            get
            {
                if (_media.Length != 0 || _suffix.Length != 0)
                {
                    var front = String.Concat(_general, "/", _media);
                    var back = _suffix.Length > 0 ? "+" + _suffix : String.Empty;
                    return String.Concat(front, back);
                }

                return _general;
            }
        }

        /// <summary>
        /// Gets the general type.
        /// </summary>
        public String GeneralType => _general;

        /// <summary>
        /// Gets the media type, if specified.
        /// </summary>
        public String MediaType => _media;

        /// <summary>
        /// Gets the suffix, if any.
        /// </summary>
        public String Suffix => _suffix;

        private static readonly char[] s_semicolon = { ';' };

        /// <summary>
        /// Gets an iterator over all integrated keys.
        /// </summary>
        public IEnumerable<String> Keys
        {
            get
            {
                foreach (var p in _params.Split(s_semicolon))
                {
                    if (p.Length == 0) continue;

                    int equalIndex = p.IndexOf('=');

                    yield return equalIndex >= 0 ? p.Substring(0, equalIndex) : p;
                }
            }
         }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value of the parameter with the specified key.
        /// </summary>
        /// <param name="key">The parameter's key.</param>
        /// <returns>The value of the parameter or null.</returns>
        public String? GetParameter(String key)
        {
            foreach (var p in _params.Split(s_semicolon))
            {
                if (p.StartsWith(key, StringComparison.Ordinal) && p.Length > key.Length && p[key.Length] == '=')
                {
                    return p.Substring(key.Length + 1);
                }
            }

            return null;
        }
     
        /// <summary>
        /// Returns the string representation of the MIME type.
        /// </summary>
        /// <returns>The currently stored MIME type.</returns>
        public override String ToString()
        {
            if (_media.Length != 0 || _suffix.Length != 0 || _params.Length != 0)
            {
                var front = String.Concat(_general, "/", _media);
                var back = _suffix.Length > 0 ? "+" + _suffix : String.Empty;
                var opt = _params.Length > 0 ? ";" + _params : String.Empty;
                return String.Concat(front, back, opt);
            }

            return _general;
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the MIME types without considering their parameters.
        /// </summary>
        /// <param name="other">The type to compare to.</param>
        /// <returns>True if both types are equal, otherwise false.</returns>
#if NET5_0_OR_GREATER
        public Boolean Equals(MimeType? other) => _general.Isi(other?._general) &&
                                                  _media.Isi(other?._media) &&
                                                  _suffix.Isi(other?._suffix);
#else
        public Boolean Equals(MimeType other) => _general.Isi(other._general) &&
                                                 _media.Isi(other._media) &&
                                                 _suffix.Isi(other._suffix);
#endif

        /// <summary>
        /// Compares to the other object. It has to be a MIME type.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
#if NET5_0_OR_GREATER
        public override Boolean Equals(Object? obj)
        {
            if (!Object.ReferenceEquals(this, obj))
            {
                return obj is MimeType other && Equals(other);
            }

            return true;
        }
#else
        public override Boolean Equals(Object obj)
        {
            if (!Object.ReferenceEquals(this, obj))
            {
                return obj is MimeType other && Equals(other);
            }

            return true;
        }
#endif

        /// <summary>
        /// Computes the hash code for the MIME type.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => (_general.GetHashCode() << 2) ^
            (_media.GetHashCode() << 1) ^
            (_suffix.GetHashCode());

        /// <summary>
        /// Runs the Equals method from a with b.
        /// </summary>
        /// <param name="a">The first MIME type.</param>
        /// <param name="b">The MIME type to compare to.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public static Boolean operator ==(MimeType a, MimeType b) => a.Equals(b);

        /// <summary>
        /// Runs the negated Equals method from a with b.
        /// </summary>
        /// <param name="a">The first MIME type.</param>
        /// <param name="b">The MIME type to compare to.</param>
        /// <returns>True if both are not equal, otherwise false.</returns>
        public static Boolean operator !=(MimeType a, MimeType b) => !a.Equals(b);

        #endregion
    }
}
