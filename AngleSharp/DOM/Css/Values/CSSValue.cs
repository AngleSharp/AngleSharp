namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS value.
	/// </summary>
    public class CSSValue : ICssValue, ICssObject
    {
        #region Fields

        /// <summary>
        /// The type of value.
        /// </summary>
        readonly CssValueType _type;

        /// <summary>
        /// Gets the instance for a slash delimiter value.
        /// </summary>
        internal static readonly CSSValue Delimiter = new CSSValue();

        /// <summary>
        /// Gets the instance for a comma separator value.
        /// </summary>
        internal static readonly CSSValue Separator = new CSSValue();

        /// <summary>
        /// Gets the instance for an inherited value.
        /// </summary>
        public static readonly CSSValue Inherit = new CSSValue(CssValueType.Inherit);

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        /// <param name="type">The type of of the value.</param>
        internal CSSValue(CssValueType type = CssValueType.Custom)
        {
            _type = type;
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
            get { return ToCss(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public virtual String ToCss()
        {
            return this == Inherit ? Keywords.Inherit : (this == Separator ? "," : (this == Delimiter ? "/" : String.Empty));
        }

        #endregion
    }
}
