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
        public XmlEntityToken(TextPosition position, String value, Boolean numeric = false, Boolean hex = false)
            : base(XmlTokenType.Entity, position)
        {
            IsNumeric = numeric;
            IsHex = hex;
            Value = value;
        }

        #endregion

        #region Properties

        public Boolean IsNumeric
        {
            get;
            private set;
        }

        public Boolean IsHex
        {
            get;
            private set;
        }

        public String Value
        {
            get;
            private set;
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
                    throw XmlParseError.CharacterReferenceInvalidNumber.At(Position);

                return num.ConvertFromUtf32();
            }
            else
            {
                var entity = default(String);

                if (!String.IsNullOrEmpty(Value) && entities.TryGetValue(Value, out entity))
                    return entity;

                throw XmlParseError.CharacterReferenceInvalidCode.At(Position);
            }
        }

        #endregion
    }
}
