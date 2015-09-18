namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    abstract class CssProperty : ICssProperty
    {
        #region Fields

        readonly PropertyFlags _flags;
        readonly String _name;

        Boolean _important;
        IPropertyValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="flags">The property flags, if any.</param>
        internal CssProperty(String name, PropertyFlags flags = PropertyFlags.None)
        {
            _name = name;
            _flags = flags;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained child nodes.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        /// <summary>
        /// Gets the serialized value of the property.
        /// </summary>
        public String Value
        {
            get { return _value != null ? _value.CssText : Keywords.Initial; }
        }

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        public Boolean IsInherited
        {
            get { return (_flags.HasFlag(PropertyFlags.Inherited) && IsInitial) || (_value != null && _value.CssText == Keywords.Inherit); }
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
            get { return _value == null || _value.CssText == Keywords.Initial; }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public String Name
        {
            get { return _name; }
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
            get { return ToCss(); }
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
        /// Gets the used value converter.
        /// </summary>
        internal abstract IValueConverter Converter
        {
            get;
        }

        /// <summary>
        /// Gets or sets the declared value, if any.
        /// </summary>
        internal IPropertyValue DeclaredValue
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Tries to set the given value and returns the status.
        /// </summary>
        /// <param name="newValue">The value that should be set.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        internal Boolean TrySetValue(CssValue newValue)
        {
            var value = Converter.Convert(newValue ?? CssValue.Initial);

            if (value != null)
            {
                _value = value;
                return true;
            }

            return false;
        }

        #endregion

        #region String Representation

        public String ToCss()
        {
            return ToCss(CssStyleFormatter.Instance);
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return formatter.Declaration(Name, Value, IsImportant);
        }

        #endregion
    }
}
