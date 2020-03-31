namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the base class for all HTML form control elements.
    /// </summary>
    abstract class HtmlFormControlElement : HtmlElement, ILabelabelElement, IValidation
    {
        #region Fields

        private readonly NodeList _labels;
        private readonly ValidityState _vstate;
        private String _error;

        #endregion

        #region ctor

        public HtmlFormControlElement(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags | NodeFlags.Special)
        {
            _vstate = new ValidityState();
            _labels = new NodeList();
        }

        #endregion

        #region Properties

        public String Name
        {
            get => this.GetOwnAttribute(AttributeNames.Name);
            set => this.SetOwnAttribute(AttributeNames.Name, value);
        }

        public IHtmlFormElement Form => GetAssignedForm();

        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled) || IsFieldsetDisabled();
            set => this.SetBoolAttribute(AttributeNames.Disabled, value);
        }

        public Boolean Autofocus
        {
            get => this.GetBoolAttribute(AttributeNames.AutoFocus);
            set => this.SetBoolAttribute(AttributeNames.AutoFocus, value);
        }

        public INodeList Labels => _labels;

        public String ValidationMessage => _vstate.IsCustomError ? _error : String.Empty;

        public Boolean WillValidate => !IsDisabled && CanBeValidated();

        public IValidityState Validity
        {
            get { Check(_vstate); return _vstate; }
        }

        #endregion

        #region Methods

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = (HtmlFormControlElement)base.Clone(owner, deep);
            node.SetCustomValidity(_error);
            return node;
        }

        public Boolean CheckValidity()
        {
            return WillValidate && Validity.IsValid;
        }

        public void SetCustomValidity(String error)
        {
            _error = error;
            ResetValidity(_vstate);
        }

        #endregion

        #region Helpers

        protected virtual Boolean IsFieldsetDisabled()
        {
            var fieldSets = this.GetAncestors().OfType<IHtmlFieldSetElement>();

            foreach (var fieldSet in fieldSets)
            {
                if (fieldSet.IsDisabled)
                {
                    var firstLegend = fieldSet.ChildNodes.FirstOrDefault(m => m is IHtmlLegendElement);
                    return !this.IsDescendantOf(firstLegend);
                }
            }

            return false;
        }

        internal virtual void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        { }

        internal virtual void Reset()
        { }

        protected virtual void Check(ValidityState state)
        {
            ResetValidity(state);
        }

        protected void ResetValidity(ValidityState state)
        {
            state.IsCustomError = !String.IsNullOrEmpty(_error);
        }

        protected abstract Boolean CanBeValidated();

        #endregion
    }
}
