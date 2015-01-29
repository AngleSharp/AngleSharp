namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A list of selectors, which is the basis for CompoundSelector and SelectorGroup.
    /// </summary>
    abstract class Selectors : IEnumerable<ISelector>
    {
        #region Fields

        protected readonly List<ISelector> selectors;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new selector group.
        /// </summary>
        public Selectors()
        {
            selectors = new List<ISelector>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specifity index for this chain of selectors.
        /// </summary>
        public Priority Specifity
        {
            get 
            {
                var sum = new Priority();

                for (int i = 0; i < selectors.Count; i++)
                    sum += selectors[i].Specifity;

                return sum;
            }
        }

        /// <summary>
        /// Gets the string representation of the selector.
        /// </summary>
        public String Text
        {
            get { return ToCss(); }
        }

        /// <summary>
        /// Gets the number of selectors in this group.
        /// </summary>
        public Int32 Length 
        {
            get { return selectors.Count; } 
        }

        /// <summary>
        /// Gets or sets a selector in this group.
        /// </summary>
        /// <param name="index">The index of the selector.</param>
        /// <returns>The selector at the given index.</returns>
        public ISelector this[Int32 index]
        {
            get { return selectors[index]; }
            set { selectors[index] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Appends a selector to the group of selectors.
        /// </summary>
        /// <param name="selector">The selector to append.</param>
        public void Add(ISelector selector)
        {
            selectors.Add(selector);
        }

        /// <summary>
        /// Removes a selector from the group of selectors.
        /// </summary>
        /// <param name="selector">The selector to remove.</param>
        /// <returns>The current group.</returns>
        public void Remove(ISelector selector)
        {
            selectors.Remove(selector);
        }

        /// <summary>
        /// Clears the list of selectors.
        /// </summary>
        /// <returns>The current group of selectors.</returns>
        public void Clear()
        {
            selectors.Clear();
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Gets the enumerator of selectors.
        /// </summary>
        /// <returns>The specific enumerator.</returns>
        public IEnumerator<ISelector> GetEnumerator()
        {
            return selectors.GetEnumerator();
        }

        /// <summary>
        /// Gets the non specified enumerator.
        /// </summary>
        /// <returns>The ocmmon enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a valid CSS string representing this selector.
        /// </summary>
        /// <returns>The CSS to create this selector.</returns>
        protected abstract String ToCss();

        #endregion
    }
}
