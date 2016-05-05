namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The processing instruction token that defines a processing instruction.
    /// </summary>
    sealed class XmlPIToken : XmlToken
    {
        #region Fields

        String _target;
        String _content;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new processing instruction token.
        /// </summary>
        public XmlPIToken(TextPosition position)
            : base(XmlTokenType.ProcessingInstruction, position)
        {
            _target = String.Empty;
            _content = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the target data.
        /// </summary>
        public String Target 
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Gets or sets the content data.
        /// </summary>
        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion
    }
}
