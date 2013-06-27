using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a group of selectors.
    /// Zero or more selectors separated by commas.
    /// </summary>
    internal class ListSelector : Selectors
    {
        #region ctor

        /// <summary>
        /// Creates a new selector group.
        /// </summary>
        public ListSelector()
        {
        }

        /// <summary>
        /// Creates a new selector group with the given selectors.
        /// </summary>
        /// <param name="selectors">The selectors.</param>
        /// <returns>The created list selector.</returns>
        internal static ListSelector Create(params Selector[] selectors)
        {
            var list = new ListSelector();

            for (int i = 0; i < selectors.Length; i++)
                list.selectors.Add(selectors[i]);

            return list;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the selector group is invalid.
        /// </summary>
        public Boolean IsInvalid 
        { 
            get; 
            internal set; 
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
                if (selectors[i].Match(element))
                    return true;
            }

            return false;
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

            if (selectors.Count > 0)
            {
                sb.Append(selectors[0].ToCss());

                for (int i = 1; i < selectors.Count; i++)
                    sb.Append(',').Append(selectors[i].ToCss());
            }

            return sb.ToString();
        }

        #endregion
    }
}
