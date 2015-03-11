namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    abstract class CssProperty : ICssProperty
    {
        #region Fields

        readonly PropertyFlags _flags;
        readonly String _name;
        readonly CssStyleDeclaration _rule;

        Boolean _important;
        ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="rule">The parent style declaration.</param>
        /// <param name="flags">The property flags, if any.</param>
        internal CssProperty(String name, CssStyleDeclaration rule, PropertyFlags flags = PropertyFlags.None)
        {
            _rule = rule;
            _name = name;
            _flags = flags;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the assigned rule.
        /// </summary>
        internal CssStyleDeclaration Rule
        {
            get { return _rule; }
        }

        /// <summary>
        /// Gets if the property has a value.
        /// </summary>
        internal Boolean HasValue
        {
            get { return _value != null; }
        }

        /// <summary>
        /// Gets if the property supports hashless colors in quirksmode.
        /// </summary>
        internal Boolean CanBeHashless
        {
            get { return _flags.HasFlag(PropertyFlags.Hashless); }
        }

        /// <summary>
        /// Gets if the property supports unitless lengths in quirksmode.
        /// </summary>
        internal Boolean CanBeUnitless
        {
            get { return _flags.HasFlag(PropertyFlags.Unitless); }
        }

        /// <summary>
        /// Gets if the property can be inherited.
        /// </summary>
        internal Boolean CanBeInherited
        {
            get { return _flags.HasFlag(PropertyFlags.Inherited); }
        }

        /// <summary>
        /// Gets if the property is actually only a shorthand.
        /// </summary>
        internal Boolean IsShorthand
        {
            get { return _flags.HasFlag(PropertyFlags.Shorthand); }
        }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        internal ICssValue Value
        {
            get { return _value ?? CssValue.Initial; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        public Boolean IsInherited
        {
            get { return (_flags.HasFlag(PropertyFlags.Inherited) && IsInitial) || _value == CssValue.Inherit; }
        }

        /// <summary>
        /// Gets if the property can be animated, in general.
        /// </summary>
        public Boolean IsAnimatable
        {
            get { return _flags.HasFlag(PropertyFlags.Animatable); }
        }

        /// <summary>
        /// Gets if the property is currently in its initial state.
        /// </summary>
        public Boolean IsInitial
        {
            get { return _value == null || _value == CssValue.Initial; }
        }

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

        /// <summary>
        /// Gets a CSS code representation of the property.
        /// </summary>
        public String CssText
        {
            get { return Serialize(_name, SerializeValue(), _important); }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Tries to set the given value and returns the status.
        /// </summary>
        /// <param name="value">The value that should be set.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        internal Boolean TrySetValue(ICssValue value)
        {
            if (value == CssValue.Inherit || value == CssValue.Initial || value == null)
            {
                Reset();
                _value = value;
                return true;
            }
            else if (IsValid(value))
            {
                _value = value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets the property to its initial state.
        /// </summary>
        internal virtual void Reset()
        {
            _value = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the default value for the given element.
        /// </summary>
        /// <param name="element">The element to use.</param>
        /// <returns>The value for the given element.</returns>
        protected abstract Object GetDefault(IElement element);

        /// <summary>
        /// Computes the value for the given element.
        /// </summary>
        /// <param name="element">The element to use.</param>
        /// <returns>The value for the given element.</returns>
        protected abstract Object Compute(IElement element);

        /// <summary>
        /// Notified once the value changed.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        protected abstract Boolean IsValid(ICssValue value);

        #endregion

        #region String representation

        /// <summary>
        /// Serializes the current value of the CSS property.
        /// </summary>
        /// <returns></returns>
        internal virtual String SerializeValue()
        {
            return Value.CssText;
        }

        /// <summary>
        /// Serializes the full CSS declaration.
        /// </summary>
        /// <param name="name">The name of the declaration.</param>
        /// <param name="value">The value of the declaration.</param>
        /// <param name="important">True if the important flag is set.</param>
        /// <returns>The string representation of the declaration.</returns>
        internal static String Serialize(String name, String value, Boolean important)
        {
            return String.Concat(name, ": ", String.Concat(value, important ? " !important" : String.Empty, ";"));
        }

        #endregion
    }
}
