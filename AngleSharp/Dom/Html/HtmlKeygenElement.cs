namespace AngleSharp.Dom.Html
{
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
            : base(owner, Tags.Keygen, prefix, NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the challenge attribute.
        /// </summary>
        public String Challenge
        {
            get { return GetOwnAttribute(AttributeNames.Challenge); }
            set { SetOwnAttribute(AttributeNames.Challenge, value); }
        }

        /// <summary>
        /// Gets or sets the type of key used.
        /// </summary>
        public String KeyEncryption
        {
            get { return GetOwnAttribute(AttributeNames.Keytype); }
            set { SetOwnAttribute(AttributeNames.Keytype, value); }
        }

        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        public String Type
        {
            get { return Tags.Keygen; }
        }

        #endregion

        #region Methods

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name, Type, Challenge);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type == Type && state.Name == Name)
                Challenge = state.Value;
        }

        protected override Boolean CanBeValidated()
        {
            return false;
        }

        #endregion
    }
}
