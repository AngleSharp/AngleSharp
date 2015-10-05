namespace AngleSharp.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents the list of all Xml entities.
    /// </summary>
    [DebuggerStepThrough]
    static class Entities
    {
        #region Fields

        static readonly Dictionary<String, String> _entities = new Dictionary<String, String>
        {
            { "amp", "&" },
            { "lt", "<" },
            { "gt", ">" },
            { "apos", "'" },
            { "quot", "\"" }
        };

        #endregion

        #region Methods

        /// <summary>
        /// Gets an symbol (that ended with a semicolon) specified by its entity
        /// name.
        /// </summary>
        /// <param name="name">
        /// The name of the entity, specified by &amp;NAME; in the Xml code.
        /// </param>
        /// <returns>The string with the symbol or null.</returns>
        public static String GetSymbol(String name)
        {
            var symbol = default(String);

            if (!String.IsNullOrEmpty(name))
                _entities.TryGetValue(name, out symbol);

            return symbol;
        }

        #endregion
    }
}
