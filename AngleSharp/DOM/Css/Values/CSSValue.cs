namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS value.
	/// </summary>
	[DOM("CSSValue")]
    public class CSSValue : ICssObject
    {
        #region Fields

        /// <summary>
        /// The type of value.
        /// </summary>
        protected CssValueType _type;

        /// <summary>
        /// The CSS text representation of the value.
        /// </summary>
        protected String _text;

        static CSSValue _inherited;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        internal CSSValue()
        {
            _type = CssValueType.Custom;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance for an inherited value.
        /// </summary>
        public static CSSValue Inherit
        {
            get { return _inherited ?? (_inherited = new CSSValue { _text = "inherit", _type = CssValueType.Inherit }); }
        }

        /// <summary>
        /// Gets a code defining the type of the value as defined above.
		/// </summary>
		[DOM("cssValueType")]
        public CssValueType CssValueType
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets a string representation of the current value.
        /// </summary>
		[DOM("cssText")]
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
            return _text;
        }

        #endregion
    }
}
