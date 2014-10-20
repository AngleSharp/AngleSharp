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

        public CSSShorthandProperty(String name, CSSStyleDeclaration rule, IEnumerable<CSSProperty> properties, PropertyFlags flags)
            : base(name, rule, flags | PropertyFlags.Shorthand)
        {
            _properties = properties;
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

        #endregion
    }
}
