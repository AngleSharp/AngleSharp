namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a group of selectors.
    /// Zero or more selectors separated by commas.
    /// </summary>
    sealed class ListSelector : Selectors, ISelector
    {
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
            for (int i = 0; i < _selectors.Count; i++)
            {
                if (_selectors[i].Match(element))
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
            var sb = Pool.NewStringBuilder();

            if (_selectors.Count > 0)
            {
                sb.Append(_selectors[0].Text);

                for (int i = 1; i < _selectors.Count; i++)
                    sb.Append(Symbols.Comma).Append(_selectors[i].Text);
            }

            return sb.ToPool();
        }

        #endregion
    }
}
