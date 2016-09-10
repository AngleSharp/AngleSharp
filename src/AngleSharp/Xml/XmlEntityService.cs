namespace AngleSharp.Xml
{
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the list of all Xml entities.
    /// </summary>
    public sealed class XmlEntityService : IEntityProvider
    {
        #region Fields

        private readonly Dictionary<String, String> _entities = new Dictionary<String, String>
        {
            { "amp", "&" },
            { "lt", "<" },
            { "gt", ">" },
            { "apos", "'" },
            { "quot", "\"" }
        };

        #endregion

        #region Instance

        /// <summary>
        /// Gets the instance to resolve entities.
        /// </summary>
        public static readonly IEntityProvider Resolver = new XmlEntityService();

        #endregion

        #region ctor

        private XmlEntityService()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a symbol specified by its entity name.
        /// </summary>
        /// <param name="name">The name of the entity in the XML code.</param>
        /// <returns>The string with the symbol or null.</returns>
        public String GetSymbol(String name)
        {
            var symbol = String.Empty;

            if (!String.IsNullOrEmpty(name))
            {
                _entities.TryGetValue(name, out symbol);
            }

            return symbol;
        }

        #endregion
    }
}
