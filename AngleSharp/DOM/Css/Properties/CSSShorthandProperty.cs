namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

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
        }

        #endregion

        #region Properties

        public IEnumerable<CSSProperty> Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        internal sealed override void Reset()
        {
            foreach (var property in _properties)
                property.Reset();
        }

        #endregion
    }
}
