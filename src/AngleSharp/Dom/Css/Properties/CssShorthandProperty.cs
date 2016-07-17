namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

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
            return Converter.Construct(properties)?.CssText;
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
