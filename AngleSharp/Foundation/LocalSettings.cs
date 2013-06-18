using System;
using System.Globalization;
using System.Threading;

namespace AngleSharp
{
    /// <summary>
    /// Contains a set of useful (global) settings (default settings),
    /// which are (usually) user-specific.
    /// </summary>
    public static class LocalSettings
    {
        static CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

        /// <summary>
        /// Gets the language (code, e.g. en-US, de-DE) to use by-default.
        /// </summary>
        public static String Language
        {
            get { return culture.Name; }
        }

        /// <summary>
        /// Gets or sets the culture to use by-default.
        /// </summary>
        public static CultureInfo Culture
        {
            get { return culture; }
            set { culture = value ?? Thread.CurrentThread.CurrentUICulture; }
        }
    }
}
