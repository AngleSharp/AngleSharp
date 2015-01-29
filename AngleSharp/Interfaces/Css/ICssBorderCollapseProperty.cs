namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS border-collapse property.
    /// </summary>
    public interface ICssBorderCollapseProperty : ICssProperty
    {
        /// <summary>
        /// Gets the use of the separated-border table rendering model.
        /// Otherwise the collapsed-border table rendering model is used.
        /// </summary>
        Boolean IsSeparated { get; }
    }
}
