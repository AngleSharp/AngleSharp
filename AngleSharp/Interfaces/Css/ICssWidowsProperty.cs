namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Repesents the widows CSS property.
    /// </summary>
    public interface ICssWidowsProperty : ICssProperty
    {
        /// <summary>
        /// Gets the count value.
        /// </summary>
        Int32 Count { get; }
    }
}
