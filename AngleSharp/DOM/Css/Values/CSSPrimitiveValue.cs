namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    sealed class CSSPrimitiveValue : CSSValue
    {
        #region Fields

        readonly ICssObject _value;

        #endregion

        #region ctor

        CSSPrimitiveValue()
            : base(CssValueType.Primitive)
        {
        }

        public CSSPrimitiveValue(ICssObject value)
            : this()
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the primitive container.
        /// </summary>
        public ICssObject Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _value.ToCss();
        }

        #endregion
    }
}
