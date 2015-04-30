namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The DOCTYPE token.
    /// </summary>
    sealed class XmlDoctypeToken : XmlToken
    {
        #region Fields

        String _name;
        String _publicIdentifier;
        String _systemIdentifier;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new DOCTYPE token.
        /// </summary>
        public XmlDoctypeToken(TextPosition position)
            : base(XmlTokenType.Doctype, position)
        {
            _name = null;
            _publicIdentifier = null;
            _systemIdentifier = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the state of the name.
        /// </summary>
        public Boolean IsNameMissing
        {
            get { return _name == null; }
        }

        /// <summary>
        /// Gets the state of the public identifier.
        /// </summary>
        public Boolean IsPublicIdentifierMissing
        {
            get { return _publicIdentifier == null; }
        }

        /// <summary>
        /// Gets the state of the system identifier.
        /// </summary>
        public Boolean IsSystemIdentifierMissing
        {
            get { return _systemIdentifier == null; }
        }

        /// <summary>
        /// Gets or sets the name of the DOCTYPE token.
        /// </summary>
        public String Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicIdentifier
        {
            get { return _publicIdentifier ?? string.Empty; }
            set { _publicIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        public String SystemIdentifier
        {
            get { return _systemIdentifier ?? string.Empty; }
            set { _systemIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the internal subset.
        /// </summary>
        public String InternalSubset
        {
            get;
            set;
        }

        #endregion
    }
}
