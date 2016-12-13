namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a CSS selector for matching elements.
    /// More information: http://dev.w3.org/csswg/selectors4/
    /// </summary>
    public interface ISelector
    {
        /// <summary>
        /// Determines if the given object is matched by this selector.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <param name="scope">The selector scope.</param>
        /// <returns>
        /// True if the selector matches the given element, otherwise false.
        /// </returns>
        Boolean Match(IElement element, IElement scope);

        /// <summary>
        /// Gets the string representation of the selector.
        /// </summary>
        String Text { get; }

        /// <summary>
        /// Gets the specificity of the given selector.
        /// </summary>
        Priority Specificity { get; }

        /// <summary>
        /// Accepts a selector visitor to expose more information.
        /// </summary>
        /// <param name="visitor">The visitor for showing around.</param>
        void Accept(ISelectorVisitor visitor);
    }
}
