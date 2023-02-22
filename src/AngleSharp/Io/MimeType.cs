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

            GeneralType = value.Substring(0, slash);
            MediaType = slash < value.Length ? value.Substring(slash + 1, Math.Min(plus, semicolon) - slash - 1) : String.Empty;
            Suffix = plus < value.Length ? value.Substring(plus + 1, semicolon - plus - 1) : String.Empty;
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
                if (MediaType.Length != 0 || Suffix.Length != 0)
                {
                    var front = String.Concat(GeneralType, "/", MediaType);
                    var back = Suffix.Length > 0 ? "+" + Suffix : String.Empty;
                    return String.Concat(front, back);
                }

                return GeneralType;
            }
        }

        /// <summary>
        /// Gets the general type.
        /// </summary>
        public String GeneralType { get; }

        /// <summary>
        /// Gets the media type, if specified.
        /// </summary>
        public String MediaType { get; }

        /// <summary>
        /// Gets the suffix, if any.
        /// </summary>
        public String Suffix { get; }

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
                    if (p.Length == 0)
                    {
                        continue;
                    }

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
            if (MediaType.Length != 0 || Suffix.Length != 0 || _params.Length != 0)
            {
                var front = String.Concat(GeneralType, "/", MediaType);
                var back = Suffix.Length > 0 ? "+" + Suffix : String.Empty;
                var opt = _params.Length > 0 ? ";" + _params : String.Empty;
                return String.Concat(front, back, opt);
            }

            return GeneralType;
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the MIME types without considering their parameters.
        /// </summary>
        /// <param name="other">The type to compare to.</param>
        /// <returns>True if both types are equal, otherwise false.</returns>
        public Boolean Equals(MimeType? other) => GeneralType.Isi(other?.GeneralType) &&
                                                  MediaType.Isi(other?.MediaType) &&
                                                  Suffix.Isi(other?.Suffix);

        /// <summary>
        /// Compares to the other object. It has to be a MIME type.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object? obj)
        {
            if (!Object.ReferenceEquals(this, obj))
            {
                return obj is MimeType other && Equals(other);
            }

            return true;
        }

        /// <summary>
        /// Computes the hash code for the MIME type.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => (GeneralType.GetHashCode() << 2) ^
            (MediaType.GetHashCode() << 1) ^
            (Suffix.GetHashCode());

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
