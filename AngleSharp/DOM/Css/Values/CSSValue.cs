namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS value.
	/// </summary>
    class CSSValue : ICssValue, ICssObject
    {
        #region Fields

        /// <summary>
        /// The type of value.
        /// </summary>
        readonly CssValueType _type;

        #endregion

        #region Special Values

        /// <summary>
        /// Gets the instance for an inherited value.
        /// </summary>
        public static readonly CSSValue Inherit = new CSSInheritValue();

        /// <summary>
        /// Gets the instance for an initial value.
        /// </summary>
        public static readonly CSSValue Initial = new CSSInitialValue();

        /// <summary>
        /// Gets the instance for a slash delimiter value.
        /// </summary>
        internal static readonly CSSValue Delimiter = new CSSDelimiterValue();

        /// <summary>
        /// Gets the instance for a comma separator value.
        /// </summary>
        internal static readonly CSSValue Separator = new CSSSeparatorValue();

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
            return String.Empty;
        }

        #endregion

        #region Nested

        sealed class CSSInheritValue : CSSValue
        {
            #region Singleton

            public CSSInheritValue()
                : base(CssValueType.Inherit)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns a CSS code representation of the stylesheet.
            /// </summary>
            /// <returns>A string that contains the code.</returns>
            public override String ToCss()
            {
                return Keywords.Inherit;
            }

            #endregion
        }

        sealed class CSSInitialValue : CSSValue
        {
            #region Singleton

            public CSSInitialValue()
                : base(CssValueType.Initial)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns a CSS code representation of the stylesheet.
            /// </summary>
            /// <returns>A string that contains the code.</returns>
            public override String ToCss()
            {
                return Keywords.Initial;
            }

            #endregion
        }

        sealed class CSSDelimiterValue : CSSValue
        {
            #region Singleton

            public CSSDelimiterValue()
                : base(CssValueType.Custom)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns a CSS code representation of the stylesheet.
            /// </summary>
            /// <returns>A string that contains the code.</returns>
            public override String ToCss()
            {
                return "/";
            }

            #endregion
        }

        sealed class CSSSeparatorValue : CSSValue
        {
            #region Singleton

            public CSSSeparatorValue()
                : base(CssValueType.Custom)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns a CSS code representation of the stylesheet.
            /// </summary>
            /// <returns>A string that contains the code.</returns>
            public override String ToCss()
            {
                return ",";
            }

            #endregion
        }

        #endregion
    }
}
