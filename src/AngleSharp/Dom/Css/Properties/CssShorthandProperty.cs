namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;

    /// <summary>
    /// Base class for all shorthand properties
    /// </summary>
    abstract class CssShorthandProperty : CssProperty
    {
        #region ctor

        public CssShorthandProperty(String name, PropertyFlags flags = PropertyFlags.None)
            : base(name, flags | PropertyFlags.Shorthand)
        {
        }

        #endregion

        #region Methods

        public String Stringify(CssProperty[] properties)
        {
            var value = Converter.Construct(properties);
            return value != null ? value.CssText : null;
        }

        public void Export(CssProperty[] properties)
        {
            foreach (var property in properties)
            {
                var value = DeclaredValue.ExtractFor(property.Name);
                property.TrySetValue(value);
            }
        }

        #endregion
    }
}
