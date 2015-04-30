namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The processing instruction token that defines a processing instruction.
    /// </summary>
    sealed class XmlPIToken : XmlToken
    {
        #region ctor

        /// <summary>
        /// Creates a new processing instruction token.
        /// </summary>
        public XmlPIToken(TextPosition position)
            : base(XmlTokenType.ProcessingInstruction, position)
        {
            Target = String.Empty;
            Content = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the target data.
        /// </summary>
        public String Target 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the content data.
        /// </summary>
        public String Content
        {
            get;
            set;
        }

        #endregion
    }
}
