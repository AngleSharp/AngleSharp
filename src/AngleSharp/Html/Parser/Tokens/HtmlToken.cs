namespace AngleSharp.Html.Parser.Tokens
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using Common;

    /// <summary>
    /// The abstract base class of top-level HTML tokens.
    /// </summary>
    public class HtmlToken
    {
        #region Fields

        private readonly HtmlTokenType _type;
        private readonly TextPosition _position;
        private StringOrMemory _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML token.
        /// </summary>
        /// <param name="type">The exact type of the token.</param>
        /// <param name="position">The token's text position.</param>
        /// <param name="name">The optional name of the token, if any.</param>
        public HtmlToken(HtmlTokenType type, TextPosition position, StringOrMemory name = default)
        {
            _type = type;
            _position = position;
            _name = name; // null is rare, default to non-null
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the character data contains actually a non-space character.
        /// </summary>
        /// <returns>True if the character data contains space character.</returns>
        public Boolean HasContent
        {
            get
            {
                for (var i = 0; i < _name.Length; i++)
                {
                    if (!_name[i].IsSpaceCharacter())
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets the name of a tag token.
        /// </summary>
        public StringOrMemory Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Gets if the character data is empty (null or length equal to zero).
        /// </summary>
        /// <returns>True if the character data is actually NULL or empty.</returns>
        public Boolean IsEmpty => _name.Length == 0;

        /// <summary>
        /// Gets the data of the comment or character token.
        /// </summary>
        public StringOrMemory Data => Name;

        /// <summary>
        /// Gets the position of the token.
        /// </summary>
        public TextPosition Position => _position;

        /// <summary>
        /// Gets if the token can be used with IsHtmlTIP properties.
        /// </summary>
        public Boolean IsHtmlCompatible => _type == HtmlTokenType.StartTag || _type == HtmlTokenType.Character;

        /// <summary>
        /// Gets if the given token is a SVG root start tag.
        /// </summary>
        public Boolean IsSvg => IsStartTag(TagNames.Svg);

        /// <summary>
        /// Gets if the token can be used with IsMathMLTIP properties.
        /// </summary>
        public Boolean IsMathCompatible => (!IsStartTag("mglyph") && !IsStartTag("malignmark")) || _type == HtmlTokenType.Character;

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public HtmlTokenType Type => _type;

        /// <summary>
        /// Indicates that this comment token is a processing instruction.
        /// </summary>
        public Boolean IsProcessingInstruction { get; internal set; }

        #endregion

        #region Methods

        /// <summary>
        /// Removes all ignorable characters from the beginning.
        /// </summary>
        /// <returns>The trimmed characters.</returns>
        public StringOrMemory TrimStart()
        {
            Int32 i;
            for (i = 0; i < _name.Length; i++)
            {
                if (!_name.Memory.Span[i].IsSpaceCharacter())
                {
                    break;
                }
            }

            var t = _name.Memory.Slice(0, i);
            _name = new StringOrMemory(_name.Memory.Slice(i));
            return new StringOrMemory(t);
        }

        private static readonly Char[] Spaces = new Char[]
        {
            Symbols.Space,
            Symbols.Tab,
            Symbols.LineFeed,
            Symbols.CarriageReturn,
            Symbols.FormFeed
        };

        /// <summary>
        /// Removes all ignorable characters from the beginning.
        /// </summary>
        /// <returns>The trimmed characters.</returns>
        public void CleanStart()
        {
            var newData = _name.Memory.TrimStart(Spaces);
            if (newData.Length != _name.Length)
            {
                _name = new StringOrMemory(newData);
            }
        }

        /// <summary>
        /// Removes the new line in the beginning, if any.
        /// </summary>
        public void RemoveNewLine()
        {
            if (_name.Length > 0 && _name[0] == Symbols.LineFeed)
            {
                _name = new StringOrMemory(_name.Memory.Slice(1));
            }
        }

        /// <summary>
        /// Converts the current token to a tag token.
        /// </summary>
        /// <returns>The tag token instance.</returns>
        public HtmlTagToken AsTag()
        {
            return (HtmlTagToken)this;
        }

        /// <summary>
        /// Finds out if the current token is a start tag token with the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>True if the token is indeed a start tag token with the given name, otherwise false.</returns>
        public Boolean IsStartTag(StringOrMemory name)
        {
            return _type == HtmlTokenType.StartTag && _name.Is(name);
        }

        #endregion
    }
}
