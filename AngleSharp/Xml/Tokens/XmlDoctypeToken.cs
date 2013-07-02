using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The DOCTYPE token.
    /// </summary>
    sealed class XmlDoctypeToken : XmlToken
    {
        #region Members

        String name;
        String publicIdentifier;
        String systemIdentifier;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new DOCTYPE token.
        /// </summary>
        public XmlDoctypeToken()
        {
            name = null;
            publicIdentifier = null;
            systemIdentifier = null;
            _type = XmlTokenType.DOCTYPE;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the state of the name.
        /// </summary>
        public Boolean IsNameMissing
        {
            get { return name == null; }
        }

        /// <summary>
        /// Gets the state of the public identifier.
        /// </summary>
        public Boolean IsPublicIdentifierMissing
        {
            get { return publicIdentifier == null; }
        }

        /// <summary>
        /// Gets the state of the system identifier.
        /// </summary>
        public Boolean IsSystemIdentifierMissing
        {
            get { return systemIdentifier == null; }
        }

        /// <summary>
        /// Gets or sets the name of the DOCTYPE token.
        /// </summary>
        public String Name
        {
            get { return name ?? string.Empty; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicIdentifier
        {
            get { return publicIdentifier ?? string.Empty; }
            set { publicIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        public String SystemIdentifier
        {
            get { return systemIdentifier ?? string.Empty; }
            set { systemIdentifier = value; }
        }

        #endregion
    }
}
