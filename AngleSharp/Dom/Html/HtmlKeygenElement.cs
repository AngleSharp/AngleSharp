namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the keygen element.
    /// </summary>
    sealed class HtmlKeygenElement : HtmlFormControlElementWithState, IHtmlKeygenElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML keygen element.
        /// </summary>
        public HtmlKeygenElement(Document owner, String prefix = null)
            : base(owner, TagNames.Keygen, prefix, NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the challenge attribute.
        /// </summary>
        public String Challenge
        {
            get { return this.GetOwnAttribute(AttributeNames.Challenge); }
            set { this.SetOwnAttribute(AttributeNames.Challenge, value); }
        }

        /// <summary>
        /// Gets or sets the type of key used.
        /// </summary>
        public String KeyEncryption
        {
            get { return this.GetOwnAttribute(AttributeNames.Keytype); }
            set { this.SetOwnAttribute(AttributeNames.Keytype, value); }
        }

        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        public String Type
        {
            get { return TagNames.Keygen; }
        }

        #endregion

        #region Methods

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name, Type, Challenge);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type.Is(Type) && state.Name.Is(Name))
            {
                Challenge = state.Value;
            }
        }

        protected override Boolean CanBeValidated()
        {
            return false;
        }

        #endregion
    }
}
