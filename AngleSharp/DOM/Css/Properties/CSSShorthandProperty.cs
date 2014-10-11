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

        public CSSShorthandProperty(String name, PropertyFlags flags, IEnumerable<CSSProperty> properties)
            : base(name, flags | PropertyFlags.Shorthand)
        {
            _properties = properties;
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
            if (_properties == null)
                return;

            foreach (var property in _properties)
                property.Reset();
        }

        protected sealed override void ChangeRule(CSSStyleDeclaration value)
        {
            base.ChangeRule(value);

            foreach (var property in _properties)
                property.Rule = value;
        }

        #endregion
    }
}
