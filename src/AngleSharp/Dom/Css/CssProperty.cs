namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    abstract class CssProperty : CssNode, ICssProperty
    {
        #region Fields

        readonly PropertyFlags _flags;
        readonly String _name;

        Boolean _important;
        IPropertyValue _value;

        #endregion

        #region ctor

        internal CssProperty(String name, PropertyFlags flags = PropertyFlags.None)
        {
            _name = name;
            _flags = flags;
        }

        #endregion

        #region Properties
        
        public String Value
        {
            get { return _value != null ? _value.CssText : Keywords.Initial; }
        }

        public Boolean IsInherited
        {
            get { return (((_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited) && IsInitial) || (_value != null && _value.CssText.Is(Keywords.Inherit)); }
        }

        public Boolean IsAnimatable
        {
            get { return (_flags & PropertyFlags.Animatable) == PropertyFlags.Animatable; }
        }

        public Boolean IsInitial
        {
            get { return _value == null || _value.CssText.Is(Keywords.Initial); }
        }

        public String Name
        {
            get { return _name; }
        }

        public Boolean IsImportant
        {
            get { return _important; }
            set { _important = value; }
        }

        public String CssText
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Internal Properties

        internal Boolean HasValue
        {
            get { return _value != null; }
        }

        internal Boolean CanBeHashless
        {
            get { return (_flags & PropertyFlags.Hashless) == PropertyFlags.Hashless; }
        }

        internal Boolean CanBeUnitless
        {
            get { return (_flags & PropertyFlags.Unitless) == PropertyFlags.Unitless; }
        }

        internal Boolean CanBeInherited
        {
            get { return (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited; }
        }

        internal Boolean IsShorthand
        {
            get { return (_flags & PropertyFlags.Shorthand) == PropertyFlags.Shorthand; }
        }

        internal abstract IValueConverter Converter
        {
            get;
        }

        internal IPropertyValue DeclaredValue
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Internal Methods

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

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Declaration(Name, Value, IsImportant));
        }

        #endregion
    }
}
