namespace AngleSharp.DOM.Css
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Utilities for getting style information. See:
    /// http://dev.w3.org/csswg/cssom/#getstyleutils
    /// </summary>
    [DomNoInterfaceObject]
    [DomName("GetStyleUtil")]
    public interface IGetStyleUtils
    {
        /// <summary>
        /// Gets the cascaded style of the element.
        /// </summary>
        [DomName("cascadedStyle")]
        ICssStyleDeclaration CascadedStyle { get; }

        /// <summary>
        /// Gets the default style of the element.
        /// </summary>
        [DomName("defaultStyle")]
        ICssStyleDeclaration DefaultStyle { get; }

        /// <summary>
        /// Gets the raw computed style.
        /// </summary>
        [DomName("rawComputedStyle")]
        ICssStyleDeclaration RawComputedStyle { get; }

        /// <summary>
        /// Gets the used style of the element.
        /// </summary>
        [DomName("usedStyle")]
        ICssStyleDeclaration UsedStyle { get; }
    }
}
