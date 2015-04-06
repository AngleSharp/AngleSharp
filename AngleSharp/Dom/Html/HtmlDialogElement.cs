namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the object for HTML dialog elements.
    /// </summary>
    sealed class HtmlDialogElement : HtmlElement, IHtmlDialogElement
    {
        #region Fields

        String _returnValue;

        #endregion

        #region ctor

        public HtmlDialogElement(Document owner, String prefix = null)
            : base(owner, Tags.Dialog, prefix)
        {
        }

        #endregion

        #region Properties

        public Boolean Open
        {
            get { return GetOwnAttribute(AttributeNames.Open) != null; }
            set { SetOwnAttribute(AttributeNames.Open, value ? String.Empty : null); }
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
