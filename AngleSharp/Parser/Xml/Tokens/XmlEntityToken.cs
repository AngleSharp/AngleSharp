namespace AngleSharp.Parser.Xml
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The entity token that defines an entity.
    /// </summary>
    sealed class XmlEntityToken : XmlToken
    {
        #region Entities

        static readonly Dictionary<String, String> DefaultEntities = new Dictionary<String, String>
        {
            { "amp", "&" },
            { "lt", "<" },
            { "gt", ">" },
            { "apos", "'" },
            { "quot", "\"" }
        };

        #endregion

        #region Fields

        readonly Boolean _numeric;
        readonly Boolean _hex;
        readonly String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityToken(TextPosition position, String value, Boolean numeric = false, Boolean hex = false)
            : base(XmlTokenType.Entity, position)
        {
            _numeric = numeric;
            _hex = hex;
            _value = value;
        }

        #endregion

        #region Properties

        public Boolean IsNumeric
        {
            get { return _numeric; }
        }

        public Boolean IsHex
        {
            get { return _hex; }
        }

        public String Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resolves the given entity token.
        /// </summary>
        /// <returns>The string that is contained in the entity token.</returns>
        public String GetEntity()
        {
            if (_numeric)
            {
                var num = _numeric ? _value.FromHex() : _value.FromDec();

                if (!num.IsValidAsCharRef())
                    throw XmlParseError.CharacterReferenceInvalidNumber.At(Position);

                return num.ConvertFromUtf32();
            }
            else
            {
                var entity = default(String);

                if (!String.IsNullOrEmpty(_value) && DefaultEntities.TryGetValue(_value, out entity))
                    return entity;

                throw XmlParseError.CharacterReferenceInvalidCode.At(Position);
            }
        }

        #endregion
    }
}
