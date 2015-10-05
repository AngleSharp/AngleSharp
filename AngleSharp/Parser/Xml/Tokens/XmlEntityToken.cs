namespace AngleSharp.Parser.Xml
{
    using AngleSharp.Extensions;
    using AngleSharp.Services;
    using System;

    /// <summary>
    /// The entity token that defines an entity.
    /// </summary>
    sealed class XmlEntityToken : XmlToken
    {
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
        /// <param name="resolver">The resolver to use.</param>
        /// <returns>The string that is contained in the entity token.</returns>
        public String GetEntity(IEntityService resolver)
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
                var entity = resolver.GetSymbol(_value);

                if (String.IsNullOrEmpty(entity))
                    throw XmlParseError.CharacterReferenceInvalidCode.At(Position);

                return entity;
            }
        }

        #endregion
    }
}
