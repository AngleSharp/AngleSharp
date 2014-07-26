namespace AngleSharp.DOM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A collection of CSS elements.
    /// </summary>
    sealed class StyleSheetList : IStyleSheetList
    {
        #region Fields

        readonly INode _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new stylesheet class.
        /// </summary>
        /// <param name="parent">The parent responsible for this list.</param>
        internal StyleSheetList(INode parent)
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
        public IStyleSheet this[Int32 index]
        {
            get { return GetStyleSheets(_parent).Skip(index).FirstOrDefault(); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements in the list of stylesheets.
        /// </summary>
        public Int32 Length
        {
            get { return GetStyleSheets(_parent).Count(); }
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

        #region Helpers

        static IEnumerable<IStyleSheet> GetStyleSheets(INode parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is IElement)
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
    }
}
