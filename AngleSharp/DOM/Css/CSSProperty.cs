namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Fore more information about CSS properties
    /// see http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    public class CSSProperty : ICssProperty, ICssObject
    {
        #region Fields

        readonly PropertyFlags _flags;
        readonly String _name;

        CSSValue _value;
        Boolean _important;
        CSSStyleDeclaration _rule;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="flags">The property flags, if any.</param>
        internal CSSProperty(String name, PropertyFlags flags = PropertyFlags.None)
        {
            _name = name;
            _flags = flags;
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
        /// Gets if the property can be inherited.
        /// </summary>
        internal Boolean IsInherited
        {
            get { return _flags.HasFlag(PropertyFlags.Inherited); }
        }

        /// <summary>
        /// Gets if the property supports hashless colors in quirksmode.
        /// </summary>
        internal Boolean IsHashless
        {
            get { return _flags.HasFlag(PropertyFlags.Hashless); }
        }

        /// <summary>
        /// Gets if the property supports unitless lengths in quirksmode.
        /// </summary>
        internal Boolean IsUnitless
        {
            get { return _flags.HasFlag(PropertyFlags.Unitless); }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        internal CSSValue Value
        {
            get { return _value ?? CSSValue.Inherit; }
            set
            {
                if (IsValid(value))
                    _value = value;
            }
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

        ICssValue ICssProperty.Value
        {
            get { return Value; }
        }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        public Boolean IsImportant
        {
            get { return _important; }
            set { _important = value; }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Tries to set the given value and returns the status.
        /// </summary>
        /// <param name="value">The value that should be set.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        internal Boolean TrySetValue(CSSValue value)
        {
            if (!IsValid(value))
                return false;

            _value = value;
            return true;
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
                return property.TrySetValue(argument);
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

                return property.TrySetValue(newList);
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
            return String.Concat(_name, ": ", _value.ToCss(), _important ? (" !" + Keywords.Important) : String.Empty, ";");
        }

        #endregion
    }
}
