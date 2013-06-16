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
        public String CssText
        {
            get;
            set;
        }

        #endregion

        //UNITLESS in QUIRKSMODE:
        //  border-top-width
        //  border-right-width
        //  border-bottom-width
        //  border-left-width
        //  border-width
        //  bottom
        //  font-size
        //  height
        //  left
        //  letter-spacing
        //  margin
        //  margin-right
        //  margin-left
        //  margin-top
        //  margin-bottom
        //  padding
        //  padding-top
        //  padding-bottom
        //  padding-left
        //  padding-right
        //  right
        //  top
        //  width
        //  word-spacing

        //HASHLESS in QUIRKSMODE:
        //  background-color
        //  border-color
        //  border-top-color
        //  border-right-color
        //  border-bottom-color
        //  border-left-color
        //  color
    }
}
