namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the keygen element.
    /// </summary>
    sealed class HTMLKeygenElement : HTMLFormControlElementWithState, IHtmlKeygenElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML keygen element.
        /// </summary>
        internal HTMLKeygenElement()
        {
            _name = Tags.Keygen;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the challenge attribute.
        /// </summary>
        public String Challenge
        {
            get { return GetAttribute(AttributeNames.Challenge); }
            set { SetAttribute(AttributeNames.Challenge, value); }
        }

        /// <summary>
        /// Gets or sets the type of key used.
        /// </summary>
        public String KeyEncryption
        {
            get { return GetAttribute(AttributeNames.Keytype); }
            set { SetAttribute(AttributeNames.Keytype, value); }
        }

        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        public String Type
        {
            get { return _name; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration of possible keytype values.
        /// </summary>
        public enum Encryption : ushort
        {
            /// <summary>
            /// The RSA encryption.
            /// </summary>
            RSA
        }

        #endregion
    }
}
