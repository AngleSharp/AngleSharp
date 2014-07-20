namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of CSS elements.
    /// </summary>
    [DomName("StyleSheetList")]
    public sealed class StyleSheetList : IEnumerable<IStyleSheet>
    {
        #region Fields

        Node _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new stylesheet class.
        /// </summary>
        /// <param name="parent">The parent responsible for this list.</param>
        internal StyleSheetList(Node parent)
        {
            _parent = parent;
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the stylesheet at the specified index.
        /// If index is greater than or equal to the number
        /// of style sheets in the list, this returns null.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The stylesheet.</returns>
        [DomName("item")]
        public IStyleSheet this[Int32 index]
        {
            get
            {
                var it = GetEnumerator();
                var i = 0;

                while (it.MoveNext())
                {
                    if (i == index)
                        return it.Current;

                    i++;
                }

                return null;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements in the list of stylesheets.
        /// </summary>
        [DomName("length")]
        public Int32 Length
        {
            get
            {
                var it = GetEnumerator();
                var count = 0;

                while (it.MoveNext())
                    count++;

                return count;
            }
        }

        #endregion

        #region Internal methods

        static IEnumerable<IStyleSheet> GetStyleSheets(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is Element)
                {
                    var linkStyle = child as ILinkStyle;

                    if (linkStyle != null)
                    {
                        var sheet = linkStyle.Sheet;

                        if (sheet != null)
                            yield return sheet;
                    }
                    else
                    {
                        foreach (var sheet in GetStyleSheets(child))
                            yield return sheet;
                    }
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an enumerator that iterates through the stylesheets.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IStyleSheet> GetEnumerator()
        {
            return GetStyleSheets(_parent).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
