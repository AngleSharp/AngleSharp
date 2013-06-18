using System;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    public sealed class DOMStringMap
    {
        #region Members

        Func<String, String> getter;
        Action<String, String> setter;

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
        internal static DOMStringMap From(Func<String, String> getter, Action<String, String> setter)
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
        public String this[String name]
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
        public String GetDataAttr(String prop)
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
        public DOMStringMap SetDataAttr(String prop, String value)
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
    }
}
