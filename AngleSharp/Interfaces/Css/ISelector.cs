namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents a CSS selector for matching elements.
    /// More information: http://dev.w3.org/csswg/selectors4/
    /// </summary>
    public interface ISelector
    {
        /// <summary>
        /// Gets the specifity of the given selector.
        /// </summary>
        Priority Specifity { get; }

        /// <summary>
        /// Determines if the given object is matched by this selector.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <returns>
        /// True if the selector matches the given element, otherwise false.
        /// </returns>
        Boolean Match(IElement element);

        /// <summary>
        /// Gets the string representation of the selector.
        /// </summary>
        String Text { get; }
    }
}
