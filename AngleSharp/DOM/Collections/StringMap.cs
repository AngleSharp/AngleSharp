namespace AngleSharp.DOM.Collections
{
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
            get { return _parent.GetAttribute(_prefix + Check(name)); }
            set { _parent.SetAttribute(_prefix + Check(name), value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the value for a specified property.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>The value for the specified property name.</returns>
        public String GetDataAttr(String prop)
        {
            return _parent.GetAttribute(_prefix + Check(prop));
        }

        /// <summary>
        /// Checks if the specified property has been set.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        public Boolean HasDataAttr(String prop)
        {
            return _parent.HasAttribute(_prefix + Check(prop));
        }

        /// <summary>
        /// Removes the given property from the list of attributes.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        public void RemoveDataAttr(String prop)
        {
            if(HasDataAttr(prop))
                this[prop] = null;
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public void SetDataAttr(String prop, String value)
        {
            _parent.SetAttribute(_prefix + Check(prop), value);
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
                throw new DOMException(ErrorCode.SyntaxError);

            if (name.IndexOf(Specification.Semicolon) >= 0)
                throw new DOMException(ErrorCode.SyntaxError);

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i].IsUppercaseAscii())
                    throw new DOMException(ErrorCode.SyntaxError);
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
                if (attr.Name.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
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
