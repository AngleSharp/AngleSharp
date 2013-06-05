using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    class CSSPrimitiveValue : CSSValue
    {
        #region Properties 

        /// <summary>
        /// Gets the type of the value as defined by the CssValue constants.
        /// </summary>
        public CssValue PrimitiveType
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public CSSPrimitiveValue SetFloatValue(UnitType unitType, float value)
        {
            //TODO
            return this;
        }

        public float GetFloatValue(UnitType unitType)
        {
            //TODO
            return 0f;
        }

        public CSSPrimitiveValue SetStringValue(UnitType unitType, string value)
        {
            //TODO
            return this;
        }

        public string GetStringValue()
        {
            //TODO
            return string.Empty;
        }

        public Counter GetCounterValue()
        {
            //TODO
            throw new NotImplementedException();
        }

        public Rect GetRectValue()
        {
            //TODO
            throw new NotImplementedException();
        }

        public RGBColor GetRGBColorValue()
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion
    }
}
