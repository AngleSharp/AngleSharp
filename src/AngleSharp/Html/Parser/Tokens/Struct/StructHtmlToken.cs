namespace AngleSharp.Html.Parser.Tokens.Struct;

using System;
using System.Collections.Generic;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Text;

// public ref struct MaybeStructHtmlToken
// {
//     private StructHtmlToken _value;
//     private Boolean _hasValue;
//
//     public MaybeStructHtmlToken(StructHtmlToken value)
//     {
//         _value = value;
//         _hasValue = true;
//     }
//
//     public MaybeStructHtmlToken()
//     {
//         _hasValue = false;
//     }
//
//     public static MaybeStructHtmlToken Null => new();
//
//     public Boolean HasValue => _hasValue;
//
//     public StructHtmlToken Value
//     {
//         get
//         {
//             if (!_hasValue)
//             {
//                 throw new InvalidOperationException("No value present.");
//             }
//
//             return _value;
//         }
//     }
//
//     public override Boolean Equals(Object? other)
//     {
//         if (!_hasValue) return other == null;
//         return false;
//     }
//
//     public override Int32 GetHashCode() => 0;
//     public override String ToString() => "";
//
//     public static implicit operator MaybeStructHtmlToken(StructHtmlToken value) => new(value);
// }

public struct StructHtmlToken
{
    #region Fields

    // token
    private readonly HtmlTokenType _type;
    private readonly TextPosition _position;
    private StringOrMemory _name;

    // tag token

    private StructAttributes _attributes;
    private Boolean _selfClosing;

    // doctype token

    private Boolean _quirks;
    private StringOrMemory? _publicIdentifier;
    private StringOrMemory? _systemIdentifier;

    #endregion

    #region ctor

    /// <summary>
    /// Creates a new HTML token.
    /// </summary>
    /// <param name="type">The exact type of the token.</param>
    /// <param name="position">The token's text position.</param>
    /// <param name="name">The optional name of the token, if any.</param>
    private StructHtmlToken(HtmlTokenType type, TextPosition position, StringOrMemory name = default)
    {
        _type = type;
        _position = position;
        _name = name; // null is rare, default to non-null
    }

    #endregion

    #region Creators

    public static StructHtmlToken Skipped(HtmlTokenType htmlTokenType, TextPosition position) =>
        new(htmlTokenType, position);

    public static StructHtmlToken Doctype(Boolean quirksForced, TextPosition position) =>
        new(HtmlTokenType.Doctype, position)
        {
            _quirks = quirksForced,
        };

    public static StructHtmlToken TagOpen(TextPosition position) =>
        new(HtmlTokenType.StartTag, position);

    public static StructHtmlToken Open(StringOrMemory name)
    {
        return new StructHtmlToken(HtmlTokenType.StartTag, TextPosition.Empty, name);
    }

    public static StructHtmlToken TagClose(TextPosition position)
    {
        return new StructHtmlToken(HtmlTokenType.EndTag, position);
    }

    public static StructHtmlToken Close(StringOrMemory s)
    {
        return new StructHtmlToken(HtmlTokenType.EndTag, TextPosition.Empty, s);
    }

    public static StructHtmlToken Character(StringOrMemory name, TextPosition position)
    {
        return new StructHtmlToken(HtmlTokenType.Character, position, name);
    }

    public static StructHtmlToken Comment(StringOrMemory name, TextPosition position)
    {
        return new StructHtmlToken(HtmlTokenType.Comment, position, name);
    }

    public static StructHtmlToken ProcessingInstruction(StringOrMemory name, TextPosition position) =>
        new(HtmlTokenType.Comment, position, name)
        {
            IsProcessingInstruction = true
        };

    public static StructHtmlToken EndOfFile(TextPosition position) =>
        new(HtmlTokenType.EndOfFile, position);

    #endregion

    #region Properties

    public Boolean IsDoctype => _type == HtmlTokenType.Doctype;

    public Boolean IsTag => _type is HtmlTokenType.StartTag or HtmlTokenType.EndTag;

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
    public Boolean IsMathCompatible =>
        (!IsStartTag("mglyph") && !IsStartTag("malignmark")) || _type == HtmlTokenType.Character;

    /// <summary>
    /// Gets the type of the token.
    /// </summary>
    public HtmlTokenType Type => _type;

    /// <summary>
    /// Indicates that this comment token is a processing instruction.
    /// </summary>
    public Boolean IsProcessingInstruction { get; internal set; }

    #endregion

    #region Html Tag Token Properties

    /// <summary>
    /// Gets or sets the state of the self-closing flag.
    /// </summary>
    public Boolean IsSelfClosing
    {
        get => _selfClosing;
        set => _selfClosing = value;
    }

    /// <summary>
    /// Gets the list of attributes.
    /// </summary>
    public StructAttributes Attributes => _attributes;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the state of the force-quirks flag.
    /// </summary>
    public Boolean IsQuirksForced
    {
        get => _quirks;
        set => _quirks = value;
    }

    /// <summary>
    /// Gets the state of the public identifier.
    /// </summary>
    public Boolean IsPublicIdentifierMissing => _publicIdentifier == null;

    /// <summary>
    /// Gets the state of the system identifier.
    /// </summary>
    public Boolean IsSystemIdentifierMissing => _systemIdentifier == null;

    /// <summary>
    /// Gets or sets the value of the public identifier.
    /// </summary>
    public StringOrMemory PublicIdentifier
    {
        get => _publicIdentifier ?? default;
        set => _publicIdentifier = value;
    }

    /// <summary>
    /// Gets or sets the value of the system identifier.
    /// </summary>
    public StringOrMemory SystemIdentifier
    {
        get => _systemIdentifier ?? default;
        set => _systemIdentifier = value;
    }

    /// <summary>
    /// Gets if the given doctype token represents a limited quirks mode state.
    /// </summary>
    public Boolean IsLimitedQuirks
    {
        get
        {
            var pi = PublicIdentifier;
            var si = SystemIdentifier;
            return (pi.StartsWith("-//W3C//DTD XHTML 1.0 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                    pi.StartsWith("-//W3C//DTD XHTML 1.0 Transitional//", StringComparison.OrdinalIgnoreCase) ||
                    si.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                    si.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// Gets if the given doctype token represents a full quirks mode state.
    /// </summary>
    public Boolean IsFullQuirks
    {
        get
        {
            var pi = PublicIdentifier;
            return IsQuirksForced || Name != "html" ||
                   pi.StartsWith("+//Silmaril//dtd html Pro v0r11 19970101//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//AdvaSoft Ltd//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//AS//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.0 Level 1//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.0 Level 2//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 1//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 2//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.0 Strict//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 2.1E//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 3.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML 3//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Level 0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Level 1//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Level 2//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Level 3//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Strict Level 0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Strict Level 1//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Strict Level 2//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Strict Level 3//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML Strict//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//IETF//DTD HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Metrius//DTD Metrius Presentational//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML Strict//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 Tables//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML Strict//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 Tables//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Netscape Comm. Corp.//DTD HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Netscape Comm. Corp.//DTD Strict HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//O'Reilly and Associates//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//O'Reilly and Associates//DTD HTML Extended 1.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//O'Reilly and Associates//DTD HTML Extended Relaxed 1.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//SoftQuad Software//DTD HoTMetaL PRO 6.0::19990601::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//SoftQuad//DTD HoTMetaL PRO 4.0::19971010::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Spyglass//DTD HTML 2.0 Extended//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//SQ//DTD HTML 2.0 HoTMetaL + extensions//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Sun Microsystems Corp.//DTD HotJava HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//Sun Microsystems Corp.//DTD HotJava Strict HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 3 1995-03-24//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 3.2 Draft//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 3.2S Draft//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 4.0 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML 4.0 Transitional//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML Experimental 19960712//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD HTML Experimental 970421//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3C//DTD W3 HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//W3O//DTD W3 HTML 3.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.Isi("-//W3O//DTD W3 HTML Strict 3.0//EN//") ||
                   pi.StartsWith("-//WebTechs//DTD Mozilla HTML 2.0//", StringComparison.OrdinalIgnoreCase) ||
                   pi.StartsWith("-//WebTechs//DTD Mozilla HTML//", StringComparison.OrdinalIgnoreCase) ||
                   pi.Isi("-/W3C/DTD HTML 4.0 Transitional/EN") ||
                   pi.Isi("HTML") ||
                   SystemIdentifier.Equals("http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd", StringComparison.OrdinalIgnoreCase) ||
                   IsSystemIdentifierMissing && pi.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                   IsSystemIdentifierMissing && pi.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Gets the status if the given doctype token matches one of the popular conditions.
    /// </summary>
    public Boolean IsValid
    {
        get
        {
            if (Name == "html")
            {
                if (!IsPublicIdentifierMissing)
                {
                    var pi = PublicIdentifier;

                    if (pi == "-//W3C//DTD HTML 4.0//EN")
                    {
                        return IsSystemIdentifierMissing || SystemIdentifier == "http://www.w3.org/TR/REC-html40/strict.dtd";
                    }
                    else if (pi == "-//W3C//DTD HTML 4.01//EN")
                    {
                        return IsSystemIdentifierMissing || SystemIdentifier == "http://www.w3.org/TR/html4/strict.dtd";
                    }
                    else if (pi == "-//W3C//DTD XHTML 1.0 Strict//EN")
                    {
                        return SystemIdentifier == "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd";
                    }
                    else if (pi == "-//W3C//DTD XHTML 1.1//EN")
                    {
                        return SystemIdentifier == "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd";
                    }
                }

                return IsSystemIdentifierMissing || SystemIdentifier == "about:legacy-compat";
            }

            return false;
        }
    }

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
    /// Finds out if the current token is a start tag token with the given name.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    /// <returns>True if the token is indeed a start tag token with the given name, otherwise false.</returns>
    public Boolean IsStartTag(string name)
    {
        return _type == HtmlTokenType.StartTag && _name.Is(name);
    }

    #endregion

    #region Html Tag Token Methods

    /// <summary>
    /// Adds a new attribute to the list of attributes. The value will
    /// be set to an empty string.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="position">The starting position of the attribute.</param>
    public void AddAttribute(StringOrMemory name, TextPosition position)
    {
        _attributes.Add(new MemoryHtmlAttributeToken(position, name, StringOrMemory.Empty));
    }

    /// <summary>
    /// Adds a new attribute to the list of attributes.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    public void AddAttribute(StringOrMemory name, StringOrMemory value)
    {
        _attributes.Add(new MemoryHtmlAttributeToken(TextPosition.Empty, name, value));
    }

    /// <summary>
    /// Sets the value of the last added attribute.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public void SetAttributeValue(StringOrMemory value)
    {
        var last = _attributes!.Count - 1;
        var attr = _attributes[last];
        _attributes[last] = new MemoryHtmlAttributeToken(attr.Position, attr.Name, value);
    }

    /// <summary>
    /// Gets the value of the attribute with the given name or an empty
    /// string if the attribute is not available.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>The value of the attribute.</returns>
    public StringOrMemory GetAttribute(StringOrMemory name)
    {
        if (_attributes.Count == 0)
            return StringOrMemory.Empty;

        for (var i = 0; i != _attributes.Count; i++)
        {
            var attr = _attributes[i];
            if (attr.Name.Is(name))
            {
                return attr.Value;
            }
        }

        return StringOrMemory.Empty;
    }

    /// <summary>
    /// Removes attribute with index i.
    /// </summary>
    /// <param name="i">idx</param>
    public void RemoveAttributeAt(Int32 i)
    {
        if (i < _attributes.Count)
        {
            _attributes.RemoveAt(i);
        }
    }

    #endregion

    public HtmlToken ToHtmlToken()
    {
        switch (Type)
        {
            case HtmlTokenType.Doctype:
                var doctype = new HtmlDoctypeToken(_quirks, _position)
                {
                    IsProcessingInstruction = IsProcessingInstruction,
                    Name = _name.String,
                    IsQuirksForced = _quirks,
                };

                if (_publicIdentifier != null)
                {
                    doctype.PublicIdentifier = _publicIdentifier.Value.String;
                }

                if (_systemIdentifier != null)
                {
                    doctype.SystemIdentifier = _systemIdentifier.Value.String;
                }

                return doctype;
            case HtmlTokenType.StartTag:
            case HtmlTokenType.EndTag:
            {
                var token = new HtmlTagToken(Type, _position, _name.String)
                {
                    IsSelfClosing = IsSelfClosing,
                    IsProcessingInstruction = IsProcessingInstruction,
                };

                for (var i = 0; i < _attributes.Count; i++)
                {
                    var attribute = _attributes[i];
                    token.AddAttribute(attribute.Name.String, attribute.Position);
                    token.SetAttributeValue(attribute.Value.String);
                }

                return token;
            }
            default:
                return new HtmlToken(Type, _position, _name.String)
                {
                    IsProcessingInstruction = IsProcessingInstruction
                };
        }
    }



}