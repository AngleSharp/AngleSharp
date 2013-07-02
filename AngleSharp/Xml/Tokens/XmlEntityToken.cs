using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The entity token that defines an entity.
    /// </summary>
    sealed class XmlEntityToken : XmlToken
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityToken()
        {
            _type = XmlTokenType.Entity;
        }

        #endregion
    }
}
