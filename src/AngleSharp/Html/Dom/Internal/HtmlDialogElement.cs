namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the object for HTML dialog elements.
    /// </summary>
    sealed class HtmlDialogElement : HtmlElement, IHtmlDialogElement
    {
        #region Fields

        private String _returnValue;

        #endregion

        #region ctor

        public HtmlDialogElement(Document owner, String prefix = null)
            : base(owner, TagNames.Dialog, prefix)
        {
        }

        #endregion

        #region Properties

        public Boolean Open
        {
            get => this.GetBoolAttribute(AttributeNames.Open);
            set => this.SetBoolAttribute(AttributeNames.Open, value);
        }

        public String ReturnValue
        {
            get => _returnValue;
            set => _returnValue = value;
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
