namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML ordered list (ol) element.
    /// </summary>
    sealed class HTMLOListElement : HTMLElement, IListScopeElement, IHtmlOrderedListElement
    {
        #region ctor

        internal HTMLOListElement()
        {
            _name = Tags.Ol;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Properties

        public Boolean Reversed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Int32 Start
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public String Type
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
