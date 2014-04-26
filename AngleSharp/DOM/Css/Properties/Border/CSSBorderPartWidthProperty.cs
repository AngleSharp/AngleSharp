namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The abstract base class for all border-width sub properties.
    /// </summary>
    public abstract class CSSBorderPartWidthProperty : CSSProperty
    {
        #region Fields

        Length _width;

        #endregion

        #region ctor

        protected CSSBorderPartWidthProperty(String name)
            : base(name)
        {
            _inherited = false;
            _width = Length.Medium;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the thickness of the given border.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            //UNITLESS in QUIRKSMODE
            var length = value.ToBorderWidth();

            if (length.HasValue)
                _width = length.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
