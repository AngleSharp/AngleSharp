using System;
using AngleSharp.Css;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS value.
    /// </summary>
    public class CSSValue
    {
        #region Members

        protected CssValue _type;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        internal CSSValue()
        {
            _type = CssValue.Custom;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a code defining the type of the value as defined above.
        /// </summary>
        public CssValue CssValueType
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets a string representation of the current value.
        /// </summary>
        public string CssText
        {
            get;
            set;
        }

        #endregion

        #region Factory

        [ThreadStatic]
        static StringBuilder str;

        internal static CSSValue Create(CssComponentValue value)
        {
            return new CSSValue() { CssText = value.ToString() };
        }

        internal static CSSValue Create(IEnumerable<CssComponentValue> value)
        {
            (str ?? (str = new StringBuilder())).Clear();

            foreach (var val in value)
                str.Append(val);

            return new CSSValue() { CssText = str.ToString() };
        }

        #endregion
    }
}
