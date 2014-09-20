namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Common interface of all CSS properties.
    /// </summary>
    [DomName("CSSProperty")]
    public interface ICssProperty
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        [DomName("value")]
        ICssValue Value { get; }

        /// <summary>
        /// Gets if the !important flag has been set.
        /// </summary>
        [DomName("important")]
        Boolean IsImportant { get; }

        /// <summary>
        /// Gets if the property can be inherited.
        /// </summary>
        Boolean IsInherited { get; }
    }
}
