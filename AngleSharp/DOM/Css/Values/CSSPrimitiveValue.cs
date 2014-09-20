namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    sealed class CSSPrimitiveValue<T> : CSSValue, ICssPrimitiveValue
        where T : ICssObject
    {
        #region Fields

        readonly T _value;

        #endregion

        #region ctor

        public CSSPrimitiveValue(T value)
            : base(CssValueType.Primitive)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the primitive container.
        /// </summary>
        public T Value
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

        public UnitType Unit
        {
            get { throw new NotImplementedException(); }
        }

        public void SetNumber(UnitType unit, double value)
        {
            throw new NotImplementedException();
        }

        public Double GetNumber(UnitType unit)
        {
            throw new NotImplementedException();
        }

        public void SetString(UnitType unit, string value)
        {
            throw new NotImplementedException();
        }

        public String GetString()
        {
            throw new NotImplementedException();
        }

        public Color GetColor()
        {
            throw new NotImplementedException();
        }
    }
}
