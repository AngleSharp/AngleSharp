namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a group of selectors.
    /// Zero or more selectors separated by commas.
    /// </summary>
    sealed class ListSelector : Selectors, ISelector
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
        internal static ListSelector Create(params ISelector[] selectors)
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
        public Boolean Match(IElement element)
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
        protected override String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            if (selectors.Count > 0)
            {
                sb.Append(selectors[0].Text);

                for (int i = 1; i < selectors.Count; i++)
                    sb.Append(Symbols.Comma).Append(selectors[i].Text);
            }

            return sb.ToPool();
        }

        #endregion
    }
}
