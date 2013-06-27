using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// A list of selectors, which is the basis for CompoundSelector and SelectorGroup.
    /// </summary>
    abstract class Selectors : Selector, IEnumerable<Selector>
    {
        #region Members

        protected List<Selector> selectors;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new selector group.
        /// </summary>
        public Selectors()
        {
            selectors = new List<Selector>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specifity index for this chain of selectors.
        /// </summary>
        public override Int32 Specifity
        {
            get 
            {
                int sum = 0;

                for (int i = 0; i < selectors.Count; i++)
                    sum += selectors[i].Specifity;

                return sum;
            }
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
        public Selector this[Int32 index]
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
        /// <returns>The current group.</returns>
        public Selectors AppendSelector(Selector selector)
        {
            selectors.Add(selector);
            return this;
        }

        /// <summary>
        /// Removes a selector from the group of selectors.
        /// </summary>
        /// <param name="selector">The selector to remove.</param>
        /// <returns>The current group.</returns>
        public Selectors RemoveSelector(Selector selector)
        {
            selectors.Remove(selector);
            return this;
        }

        /// <summary>
        /// Clears the list of selectors.
        /// </summary>
        /// <returns>The current group of selectors.</returns>
        public Selectors ClearSelectors()
        {
            selectors.Clear();
            return this;
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Gets the enumerator of selectors.
        /// </summary>
        /// <returns>The specific enumerator.</returns>
        public IEnumerator<Selector> GetEnumerator()
        {
            return selectors.GetEnumerator();
        }

        /// <summary>
        /// Gets the non specified enumerator.
        /// </summary>
        /// <returns>The ocmmon enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)selectors).GetEnumerator();
        }

        #endregion
    }
}
