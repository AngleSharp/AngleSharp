using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a compound selector.
    /// Chain of simple selectors which are not separated by
    /// a combinator.
    /// </summary>
    internal class CompoundSelector : Selectors
    {
        #region ctor

        /// <summary>
        /// Creates a new compound selector.
        /// </summary>
        public CompoundSelector()
        {
        }

        /// <summary>
        /// Creates a new compound selector with the given selectors.
        /// </summary>
        /// <param name="selectors">The selectors.</param>
        /// <returns>The new compound selector.</returns>
        internal static CompoundSelector Create(params SimpleSelector[] selectors)
        {
            var compound = new CompoundSelector();

            for (int i = 0; i < selectors.Length; i++)
                compound.selectors.Add(selectors[i]);

            return compound;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given object is matched by this selector.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <returns>True if the selector matches the given element, otherwise false.</returns>
        public override Boolean Match(Element element)
        {
            for (int i = 0; i < selectors.Count; i++)
            {
                if (!selectors[i].Match(element))
                    return false;
            }

            return true;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a valid CSS string representing this selector.
        /// </summary>
        /// <returns>The CSS to create this selector.</returns>
        public override String ToCss()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < selectors.Count; i++)
                sb.Append(selectors[i].ToCss());

            return sb.ToString();
        }

        #endregion
    }
}
