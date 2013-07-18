using System;

namespace AngleSharp.Xml
{
    sealed class XmlNotationDeclaration : XmlBaseDeclaration
    {
        #region Members

        String _publicIdentifier;
        String _systemIdentifier;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlNotationDeclaration()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the declaration is an external id (the system identifier
        /// has been set directly or indirectly).
        /// </summary>
        public Boolean IsExternalId
        {
            get { return !String.IsNullOrEmpty(_systemIdentifier); }
        }

        /// <summary>
        /// Gets if the declaration is an external id (the system identifier
        /// has been set directly or indirectly).
        /// </summary>
        public Boolean IsPublicId
        {
            get { return !String.IsNullOrEmpty(_publicIdentifier) && String.IsNullOrEmpty(_systemIdentifier); }
        }

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicIdentifier
        {
            get { return _publicIdentifier ?? String.Empty; }
            set { _publicIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        public String SystemIdentifier
        {
            get { return _systemIdentifier ?? String.Empty; }
            set { _systemIdentifier = value; }
        }

        #endregion
    }
}
