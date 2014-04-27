namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
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
        CSSStyleDeclaration _rule;

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
        /// Gets or sets the assigned rule.
        /// </summary>
        internal CSSStyleDeclaration Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

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
        /// Gets if the property has a valid value, otherwise the property
        /// is ignored.
        /// </summary>
        public Boolean IsLegal
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        [DOM("value")]
        public CSSValue Value
        {
            get { return _value ?? CSSValue.Inherit; }
            set 
            {
                if (IsLegal = IsValid(value)) 
                    _value = value; 
            }
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

        #region Internal Methods

        /// <summary>
        /// Creates a shallow copy of the current object.
        /// </summary>
        /// <returns></returns>
        internal CSSProperty Clone()
        {
            return (CSSProperty)this.MemberwiseClone();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notified once the value changed.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        protected virtual Boolean IsValid(CSSValue value)
        {
            return true;
        }

        #endregion

        #region Helpers

        internal static Boolean CheckSingleProperty(CSSProperty property, Int32 index, CSSValueList arguments)
        {
            if (index < arguments.Length)
            {
                var argument = arguments[index];
                property.Value = argument;
                return property.Value == argument;
            }

            return false;
        }

        internal static Boolean CheckLastProperty(CSSProperty property, Int32 index, CSSValueList arguments)
        {
            if (arguments.Length - index > 1)
            {
                var newList = new CSSValueList();

                while (index < arguments.Length)
                    newList.Add(arguments[index++]);

                property.Value = newList;
                return property.Value == newList;
            }

            return CheckSingleProperty(property, index, arguments);
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
