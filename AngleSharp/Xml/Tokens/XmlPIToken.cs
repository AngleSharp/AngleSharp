using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The processing instruction token that defines a processing instruction.
    /// </summary>
    sealed class XmlPIToken : XmlToken
    {
        #region Members

        String _target;
        String _content;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new processing instruction token.
        /// </summary>
        public XmlPIToken()
        {
            _target = String.Empty;
            _content = String.Empty;
            _type = XmlTokenType.ProcessingInstruction;
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
