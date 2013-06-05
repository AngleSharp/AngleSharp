using System;
using AngleSharp.Css;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS value.
    /// </summary>
    public abstract class CSSValue
    {
        #region Members

        protected CssValue _type;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        public CSSValue()
        {
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

        internal static CSSValue Create(CssComponentValue value)
        {
            //TODO
            return null;
        }

        internal static CSSValue Create(IEnumerable<CssComponentValue> value)
        {
            //foreach (var val in value)
            //{
            //    Console.Write(val);
            //    Console.Write(",");
            //}

            //TODO
            return null;
        }

        #endregion
    }
}
