using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS selector as specified in
    /// http://dev.w3.org/csswg/selectors4/
    /// </summary>
    public abstract class Selector : ICSSObject
    {
        #region Properties

        /// <summary>
        /// Gets the specifity of the given selector.
        /// </summary>
        public abstract Int32 Specifity
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given object is matched by this selector.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <returns>True if the selector matches the given element, otherwise false.</returns>
        public abstract Boolean Match(Element element);

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a valid CSS string representing this selector.
        /// </summary>
        /// <returns>The CSS to create this selector.</returns>
        public abstract String ToCss();

        #endregion
    }
}
