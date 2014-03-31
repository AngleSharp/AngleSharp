namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an angle in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
    /// </summary>
    sealed class CSSAngleValue : CSSPrimitiveValue
    {
        #region Fields

        Angle _value;

        #endregion

        #region ctor

        public CSSAngleValue(Angle value)
        {
            _text = value.ToString();
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS angle.
        /// </summary>
        public Angle Angle
        {
            get { return _value; }
        }

        #endregion
    }
}
