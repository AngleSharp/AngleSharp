namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    sealed class StringMap : IStringMap
    {
        #region Fields

        readonly String _prefix;
        readonly Element _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new map of tokens.
        /// </summary>
        internal StringMap(String prefix, Element parent)
        {
            _prefix = prefix;
            _parent = parent;
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets or sets the value for a specified property.
        /// </summary>
        /// <param name="name">The name of the custom attribute property.</param>
        /// <returns>The value of the custom attribute property.</returns>
        public String this[String name]
        {
            get { return _parent.GetAttribute(null, _prefix + Check(name)); }
            set { _parent.SetAttribute(null, _prefix + Check(name), value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes the given property from the list of attributes.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void Remove(String name)
        {
            if (Contains(name))
                this[name] = null;
        }

        /// <summary>
        /// Checks if the specified property has been set.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        public Boolean Contains(String name)
        {
            return _parent.HasAttribute(null, _prefix + Check(name));
        }

        #endregion

        #region Helper

        /// <summary>
        /// Checks if the given name follows the rules (shall not start with xml, only lowercase, no semicolon).
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The name again.</returns>
        String Check(String name)
        {
            if (name.StartsWith(Tags.Xml, StringComparison.OrdinalIgnoreCase))
                throw new DomException(DomError.Syntax);
            else if (name.IndexOf(Symbols.Semicolon) >= 0)
                throw new DomException(DomError.Syntax);

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i].IsUppercaseAscii())
                    throw new DomException(DomError.Syntax);
            }

            return name;
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Gets an enumerator over all data- attributes.
        /// </summary>
        /// <returns>The generated enumerator.</returns>
        public IEnumerator<KeyValuePair<String, String>> GetEnumerator()
        {
            foreach (var attr in _parent.Attributes)
            {
                if (attr.NamespaceUri == null && attr.Name.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
                    yield return new KeyValuePair<String, String>(attr.Name.Remove(0, _prefix.Length), attr.Value);
            }
        }

        /// <summary>
        /// Gets the common enumerator.
        /// </summary>
        /// <returns>The generated enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
