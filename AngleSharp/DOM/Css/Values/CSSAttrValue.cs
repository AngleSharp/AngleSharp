namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/attr
    /// </summary>
    sealed class CSSAttrValue : CSSValue
    {
        #region Fields

        readonly String _name;

        #endregion

        #region ctor

        public CSSAttrValue(String name)
            : base(CssValueType.Primitive)
        {
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

        #region Methods

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Attr,  _name);
        }

        #endregion
    }
}
