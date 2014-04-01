namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The base class for all compound
    /// properties, e.g. those which have
    /// members that can be set individually.
    /// </summary>
    abstract class CSSCompoundProperty : CSSProperty
    {
        public CSSCompoundProperty(String name)
            : base(name)
        {
        }
    }
}
