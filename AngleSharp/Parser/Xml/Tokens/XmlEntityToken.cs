namespace AngleSharp.Parser.Xml
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;

    /// <summary>
    /// The entity token that defines an entity.
    /// </summary>
    sealed class XmlEntityToken : XmlToken
    {
        #region Fields

        String _value;

        #endregion

        #region Entities

        static readonly Dictionary<String, String> entities = new Dictionary<String, String>
        {
            { "amp", "&" },
            { "lt", "<" },
            { "gt", ">" },
            { "apos", "'" },
            { "quot", "\"" }
        };

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityToken()
        {
            _type = XmlTokenType.Entity;
        }

        #endregion

        #region Properties

        public Boolean IsNumeric
        {
            get;
            set;
        }

        public Boolean IsHex
        {
            get;
            set;
        }

        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resolves the given entity token.
        /// </summary>
        /// <returns>The string that is contained in the entity token.</returns>
        public String GetEntity()
        {
            if (IsNumeric)
            {
                var num = IsHex ? Value.FromHex() : Value.FromDec();

                if (!num.IsValidAsCharRef())
                    throw XmlParseError.CharacterReferenceInvalidNumber.Throw();

                return num.ConvertFromUtf32();
            }
            else
            {
                var entity = default(String);

                if (!String.IsNullOrEmpty(Value) && entities.TryGetValue(Value, out entity))
                    return entity;

                throw XmlParseError.CharacterReferenceInvalidCode.Throw();
            }
        }

        #endregion
    }
}
