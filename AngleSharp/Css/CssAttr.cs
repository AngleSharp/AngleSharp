namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Wraps a string as a CSS attribute value.
    /// </summary>
    sealed class CssAttr : ICssObject
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

        #region Casts

        /// <summary>
        /// Defines an explicit cast from a string to a CssAttr.
        /// </summary>
        /// <param name="str">The string to wrap.</param>
        /// <returns>The wrapped string.</returns>
        public static explicit operator CssAttr(String str)
        {
            return new CssAttr(str);
        }

        /// <summary>
        /// Defines an implicit cast from a CssAttr to a string.
        /// </summary>
        /// <param name="str">The string to unwrap.</param>
        /// <returns>The original string.</returns>
        public static implicit operator String(CssAttr str)
        {
            return str._name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the CSS standard represenation of the contained string.
        /// </summary>
        /// <returns>A string that contains the CSS code to create the value.</returns>
        public String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Attr, _name);
        }

        #endregion
    }
}
