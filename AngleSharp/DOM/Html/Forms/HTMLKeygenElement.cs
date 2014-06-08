using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the keygen element.
    /// </summary>
    [DomName("HTMLKeygenElement")]
    public sealed class HTMLKeygenElement : HTMLFormControlElementWithState
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
        [DomName("challenge")]
        public String Challenge
        {
            get { return GetAttribute("challenge"); }
            set { SetAttribute("challenge", value); }
        }

        /// <summary>
        /// Gets or sets the type of key used.
        /// </summary>
        [DomName("keytype")]
        public Encryption Keytype
        {
            get { return ToEnum(GetAttribute("keytype"), Encryption.RSA); }
            set { SetAttribute("keytype", value.ToString()); }
        }

        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        [DomName("type")]
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
