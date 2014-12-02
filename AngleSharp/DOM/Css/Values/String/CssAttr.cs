namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Wraps a string as a CSS attribute value.
    /// </summary>
    sealed class CssAttr : ICssValue
    {
        #region Fields

        readonly String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Wraps the given string.
        /// </summary>
        /// <param name="name">The name of the attribute to consider.</param>
        public CssAttr(String name)
        {
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the stored value.
        /// </summary>
        public String Value
        {
            get { return _name; }
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return FunctionNames.Build(FunctionNames.Attr, _name); }
        }

        #endregion
    }
}
