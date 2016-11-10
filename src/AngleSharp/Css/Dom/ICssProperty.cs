namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Common interface of all CSS properties.
    /// </summary>
    [DomName("CSSProperty")]
    [DomNoInterfaceObject]
    public interface ICssProperty : IStyleFormattable
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        [DomName("important")]
        Boolean IsImportant { get; set; }

        /// <summary>
        /// Gets if the property is declared as being inherited.
        /// </summary>
        Boolean IsInherited { get; }

        /// <summary>
        /// Gets if the property is in its initial value.
        /// </summary>
        Boolean IsInitial { get; }

        /// <summary>
        /// Gets if the property can be inherited.
        /// </summary>
        Boolean CanBeInherited { get; }
    }
}
