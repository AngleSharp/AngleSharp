using System;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    public sealed class DOMStringMap : DOMCollection
    {
        #region Members

        Func<string, string> getter;
        Action<string, string> setter;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new map of tokens.
        /// </summary>
        private DOMStringMap()
        {
        }

        /// <summary>
        /// Creates a bound DOMStringMap from the given properties.
        /// </summary>
        /// <param name="getter">The access to the getter property part.</param>
        /// <param name="setter">The access to the setter property part.</param>
        /// <returns>The DOMStringMap.</returns>
        internal static DOMStringMap From(Func<string, string> getter, Action<string, string> setter)
        {
            var map = new DOMStringMap();

            map.getter = getter;
            map.setter = setter;

            return map;
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets or sets the value for a specified property.
        /// </summary>
        /// <param name="name">The name of the custom attribute property.</param>
        /// <returns>The value of the custom attribute property.</returns>
        public string this[string name]
        {
            get { return getter(Check(name)); }
            set { setter(Check(name), value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the value for a specified property.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>The value for the specified property name.</returns>
        public string GetDataAttr(string prop)
        {
            return this[prop];
        }

        /// <summary>
        /// Checks if the specified property has been set.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        public bool HasDataAttr(string prop)
        {
            return this[prop] != null;
        }

        /// <summary>
        /// Removes the given property from the list of attributes.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>The current DOMStringMap.</returns>
        public DOMStringMap RemoveDataAttr(string prop)
        {
            if(HasDataAttr(prop))
                this[prop] = null;

            return this;
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        /// <param name="prop">The name of the property.</param>
        /// <returns>The current DOMStringMap.</returns>
        public DOMStringMap SetDataAttr(string prop, string value)
        {
            this[prop] = value;
            return this;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Checks if the given name follows the rules (shall not start with xml, only lowercase, no semicolon).
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The name again.</returns>
        string Check(string name)
        {
            if (name.StartsWith("xml", StringComparison.InvariantCultureIgnoreCase))
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

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the stringmap.
        /// </summary>
        /// <returns>There is no HTML code to return.</returns>
        public override string ToHtml()
        {
            return string.Empty;
        }

        #endregion
    }
}
