namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the object for HTML dialog elements.
    /// </summary>
    sealed class HTMLDialogElement : HTMLElement, IHtmlDialogElement
    {
        #region Fields

        String _returnValue;

        #endregion

        #region ctor

        internal HTMLDialogElement()
        {
            _name = Tags.Dialog;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Properties

        public Boolean Open
        {
            get { return GetAttribute(AttributeNames.Open) != null; }
            set { SetAttribute(AttributeNames.Open, value ? String.Empty : null); }
        }

        public String ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; }
        }

        public void Show(IElement anchor = null)
        {
            Open = true;
            //TODO
        }

        public void ShowModal(IElement anchor = null)
        {
            Open = true;
            //TODO
        }

        public void Close(String returnValue = null)
        {
            Open = false;
            ReturnValue = returnValue;
        }

        #endregion
    }
}
