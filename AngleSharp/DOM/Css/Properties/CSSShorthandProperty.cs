namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for all shorthand properties
    /// </summary>
    abstract class CSSShorthandProperty : CSSProperty
    {
        #region Fields

        readonly IEnumerable<CSSProperty> _properties;

        #endregion

        #region ctor

        public CSSShorthandProperty(String name, CSSStyleDeclaration rule, PropertyFlags flags = PropertyFlags.None)
            : base(name, rule, flags | PropertyFlags.Shorthand)
        {
            _properties = CssPropertyFactory.CreateLonghandsFor(name, rule);
            Reset();
        }

        #endregion

        #region Properties

        public IEnumerable<CSSProperty> Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        protected TProperty Get<TProperty>()
        {
            return _properties.OfType<TProperty>().FirstOrDefault();
        }

        internal sealed override void Reset()
        {
            foreach (var property in _properties)
                property.Reset();
        }

        /// <summary>
        /// Serializes the shorthand with only the given properties.
        /// </summary>
        /// <param name="properties">The properties to use.</param>
        /// <returns>The serialized value or an empty string, if serialization is not possible.</returns>
        internal String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            //TODO
            return String.Empty;
        }

        #endregion
    }
}
