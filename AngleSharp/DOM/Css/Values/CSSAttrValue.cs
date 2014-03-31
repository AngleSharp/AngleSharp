namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/attr
    /// </summary>
    sealed class CSSAttrValue : CSSPrimitiveValue
    {
        #region Fields

        String _name;

        #endregion

        #region ctor

        public CSSAttrValue(String name)
        {
            _text = String.Concat("attr(", name, ")");
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        #endregion
    }
}
