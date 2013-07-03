using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    [DOM("DOMStringMap")]
    public sealed class DOMStringMap : IEnumerable<KeyValuePair<String, String>>
    {
        #region Constant

        const String PREFIX = "data-";

        #endregion

        #region Members

        Element _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new map of tokens.
        /// </summary>
        internal DOMStringMap(Element parent)
        {
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
            get { return _parent.GetAttribute(PREFIX + Check(name)); }
            set { _parent.SetAttribute(PREFIX + Check(name), value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the value for a specified property.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>The value for the specified property name.</returns>
        [DOM("getDataAttr")]
        public String GetDataAttr(String prop)
        {
            return _parent.GetAttribute(PREFIX + Check(prop));
        }

        /// <summary>
        /// Checks if the specified property has been set.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        [DOM("hetDataAttr")]
        public bool HasDataAttr(String prop)
        {
            return _parent.HasAttribute(PREFIX + Check(prop));
        }

        /// <summary>
        /// Removes the given property from the list of attributes.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>The current DOMStringMap.</returns>
        [DOM("removeDataAttr")]
        public DOMStringMap RemoveDataAttr(String prop)
        {
            if(HasDataAttr(prop))
                this[prop] = null;

            return this;
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <returns>The current DOMStringMap.</returns>
        [DOM("setDataAttr")]
        public DOMStringMap SetDataAttr(String prop, String value)
        {
            _parent.SetAttribute(PREFIX + Check(prop), value);
            return this;
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
            if (name.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
                throw new DOMException(ErrorCode.SyntaxError);

            if (name.Contains(Specification.SC))
                throw new DOMException(ErrorCode.SyntaxError);

            for (int i = 0; i < name.Length; i++)
            {
                if (Specification.IsUppercaseAscii(name[i]))
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
                if (attr.Name.StartsWith(PREFIX, StringComparison.OrdinalIgnoreCase))
                    yield return new KeyValuePair<String, String>(attr.Name.Remove(0, PREFIX.Length), attr.Value);
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
