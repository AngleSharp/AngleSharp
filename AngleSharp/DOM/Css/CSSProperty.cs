namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Fore more information about CSS properties
    /// see http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    [DOM("CSSProperty")]
    public class CSSProperty : ICssObject
    {
        #region Fields

        protected Boolean _inherited;
        readonly String _name;
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

        #region Internal Properties

        /// <summary>
        /// Gets if the property has a value.
        /// </summary>
        internal Boolean HasValue
        {
            get { return _value != null; }
        }

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        internal Boolean IsInherited
        {
            get { return _inherited; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        [DOM("value")]
        public CSSValue Value
        {
            get { return _value ?? CSSValue.Inherit; }
            set { _value = CheckValue(value) ?? _value; }
        }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        [DOM("important")]
        public Boolean Important
        {
            get { return _important; }
            set { _important = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notified once the value changed.
        /// </summary>
        protected virtual CSSValue CheckValue(CSSValue value)
        {
            return value;
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
