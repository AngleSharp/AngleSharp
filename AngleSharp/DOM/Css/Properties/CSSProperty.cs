using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Fore more information about CSS properties
    /// see http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    public sealed class CSSProperty : ICSSObject
    {
        #region Members

        String _name;
        CSSValue _value;
        Boolean _important;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name"></param>
        internal CSSProperty(String name)
        {
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        public CSSValue Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        public Boolean Important
        {
            get { return _important; }
            set { _important = value; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the property.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var value = _name + ":" + _value.ToCss();

            if (_important)
                value += " !important";

            return value;
        }

        #endregion
    }
}
