namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a CSS value.
	/// </summary>
    sealed class CssValue : ICssValue
    {
        #region Fields

        readonly CssValueType _type;
        readonly String _text;

        #endregion

        #region Special Values

        /// <summary>
        /// Gets the instance for an inherited value.
        /// </summary>
        public static readonly CssValue Inherit = new CssValue(Keywords.Inherit, CssValueType.Inherit);

        /// <summary>
        /// Gets the instance for an initial value.
        /// </summary>
        public static readonly CssValue Initial = new CssValue(Keywords.Initial, CssValueType.Initial);

        /// <summary>
        /// Gets the instance for a slash delimiter value.
        /// </summary>
        internal static readonly CssValue Delimiter = new CssValue("/");

        /// <summary>
        /// Gets the instance for a comma separator value.
        /// </summary>
        internal static readonly CssValue Separator = new CssValue(",");

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        /// <param name="text">The text that represents the value.</param>
        /// <param name="type">The type of of the value.</param>
        CssValue(String text, CssValueType type)
        {
            _text = text;
            _type = type;
        }

        /// <summary>
        /// Creates a new custom CSS value.
        /// </summary>
        /// <param name="text">The text that represents the value.</param>
        internal CssValue(String text)
            : this(text, CssValueType.Custom)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a code defining the type of the value as defined above.
		/// </summary>
        public CssValueType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets a string representation of the current value.
        /// </summary>
        public String CssText
        {
            get { return _text; }
        }

        #endregion
    }
}
