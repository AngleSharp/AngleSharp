namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS orphans property.
    /// </summary>
    public interface ICssOrphansProperty : ICssProperty
    {
        /// <summary>
        /// Gets the minimum number of lines in a block container
        /// that must be left at the bottom of the page. 
        /// </summary>
        Int32 Count { get; }
    }
}
