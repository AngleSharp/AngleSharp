namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a compound selector.
    /// Chain of simple selectors which are not separated by
    /// a combinator.
    /// </summary>
    sealed class CompoundSelector : Selectors, ISelector
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
                compound._selectors.Add(selectors[i]);

            return compound;
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
            for (int i = 0; i < _selectors.Count; i++)
            {
                if (!_selectors[i].Match(element))
                    return false;
            }

            return true;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a valid CSS string representing this selector.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The CSS to create this selector.</returns>
        public override String ToCss(IStyleFormatter formatter)
        {
            var sb = Pool.NewStringBuilder();

            for (int i = 0; i < _selectors.Count; i++)
                sb.Append(_selectors[i].Text);

            return sb.ToPool();
        }

        #endregion
    }
}
