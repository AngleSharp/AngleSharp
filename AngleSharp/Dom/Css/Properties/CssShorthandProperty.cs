namespace AngleSharp.Dom.Css
{
    using System;
    using System.Linq;
    using AngleSharp.Css;

    /// <summary>
    /// Base class for all shorthand properties
    /// </summary>
    abstract class CssShorthandProperty : CssProperty
    {
        #region Fields

        readonly CssProperty[] _properties;

        #endregion

        #region ctor

        public CssShorthandProperty(String name, PropertyFlags flags = PropertyFlags.None)
            : base(name, flags | PropertyFlags.Shorthand)
        {
            _properties = Factory.Properties.CreateLonghandsFor(name).ToArray();
        }

        #endregion

        #region Properties

        public CssProperty[] Properties
        {
            get { return _properties; }
        }

        #endregion
    }
}
