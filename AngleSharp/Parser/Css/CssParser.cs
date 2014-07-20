namespace AngleSharp.Parser.Css
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class CssParser : IParser
    {
        #region Fields

        static readonly String important = "important";
        static readonly String inherit = "inherit";

        CssSelectorConstructor selector;
        CssValueBuilder value;
        CssTokenizer tokenizer;
        Boolean started;
        Boolean quirks;
        CSSStyleSheet sheet;
        Object sync;
        Task task;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ParseError;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS parser instance with a new stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public CssParser(String source, IConfiguration configuration = null)
            : this(new CSSStyleSheet { Options = configuration }, new SourceManager(source, configuration.DefaultEncoding()))
        { }

        /// <summary>
        /// Creates a new CSS parser instance with an new stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public CssParser(Stream stream, IConfiguration configuration = null)
            : this(new CSSStyleSheet { Options = configuration }, new SourceManager(stream, configuration.DefaultEncoding()))
        { }

        /// <summary>
        /// Creates a new CSS parser instance with the specified stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="source">The source code as a string.</param>
        internal CssParser(CSSStyleSheet stylesheet, String source)
            : this(stylesheet, new SourceManager(source, stylesheet.Options.DefaultEncoding()))
        { }

        /// <summary>
        /// Creates a new CSS parser instance with the specified stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="stream">The stream to use as source.</param>
        internal CssParser(CSSStyleSheet stylesheet, Stream stream)
            : this(stylesheet, new SourceManager(stream, stylesheet.Options.DefaultEncoding()))
        { }

        /// <summary>
        /// Creates a new CSS parser instance parser with the specified stylesheet
        /// based on the given source manager.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="source">The source to use.</param>
        internal CssParser(CSSStyleSheet stylesheet, SourceManager source)
        {
            selector = Pool.NewSelectorConstructor();
            value = new CssValueBuilder();
            sync = new Object();
            tokenizer = new CssTokenizer(source);
            tokenizer.IgnoreComments = true;
            tokenizer.IgnoreWhitespace = true;

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ParseError != null)
                    ParseError(this, ev);
            };

            quirks = stylesheet.Options.UseQuirksMode;
            started = false;
            sheet = stylesheet;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the parser has been started asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return task != null; }
        }

        /// <summary>
        /// Gets the resulting stylesheet of the parsing.
        /// </summary>
        public ICssStyleSheet Result
        {
            get
            {
                Parse();
                return sheet;
            }
        }

        /// <summary>
        /// Gets if the quirks-mode is activated.
        /// </summary>
        public Boolean IsQuirksMode
        {
            get { return quirks; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task ParseAsync()
        {
            lock (sync)
            {
                if (!started)
                {
                    started = true;
                    task = Task.Factory.StartNew(() => Kernel());
                }
                else if (task == null)
                    throw new InvalidOperationException("The parser has already run synchronously.");

                return task;
            }
        }

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        public void Parse()
        {
            var run = false;

            lock (sync)
            {
                if (!started)
                {
                    started = true;
                    run = true;
                }
            }

            if (run)
                Kernel();
        }

        #endregion

        #region Create Rules

        /// <summary>
        /// Called before a medialist has been created.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created media rule.</returns>
        CSSMediaRule CreateMediaRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSMediaRule();

            if (tokens.MoveNext())
                rule.Media = InMediaList(tokens);

            if (tokens.Current.Type != CssTokenType.CurlyBracketOpen)
                return null;

            FillRules(rule, tokens);
            return rule;
        }

        /// <summary>
        /// Called before a page selector has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created page rule.</returns>
        CSSPageRule CreatePageRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSPageRule();

            if (tokens.MoveNext())
                rule.Selector = InSelector(tokens);

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                FillDeclarations(rule.Style, tokens);

            return rule;
        }

        /// <summary>
        /// Called before the body of the font-face rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created font-face rule.</returns>
        CSSFontFaceRule CreateFontFaceRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSFontFaceRule();

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                FillDeclarations(rule.Styles, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a supports condition has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created supports rule.</returns>
        CSSSupportsRule CreateSupportsRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSSupportsRule();

            if (tokens.MoveNext())
                rule.ConditionText = Condition(tokens);

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                FillRules(rule, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a document function has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated document rule.</returns>
        CSSDocumentRule CreateDocumentRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSDocumentRule();

            if (tokens.MoveNext())
                rule.Conditions.AddRange(InDocumentFunctions(tokens));

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                FillRules(rule, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a keyframes identifier has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated keyframes rule.</returns>
        CSSKeyframesRule CreateKeyframesRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSKeyframesRule();

            if (tokens.MoveNext())
                rule.Name = InKeyframesName(tokens);

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                FillRules(rule, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a prefix has been found for the namespace rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated namespace rule.</returns>
        CSSNamespaceRule CreateNamespaceRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSNamespaceRule();

            if (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.Ident)
                {
                    rule.Prefix = ((CssKeywordToken)token).Data;

                    if (tokens.MoveNext())
                        token = tokens.Current;

                    if (token.Type == CssTokenType.String)
                        rule.NamespaceURI = ((CssStringToken)token).Data;
                }

                JumpToNextSemicolon(tokens);
            }

            return rule;
        }

        /// <summary>
        /// Before a charset string has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated rule.</returns>
        CSSCharsetRule CreateCharsetRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSCharsetRule();

            if (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.String)
                {
                    rule.Encoding = ((CssStringToken)token).Data;
                    tokens.MoveNext();
                }

                JumpToNextSemicolon(tokens);
            }

            return rule;
        }

        /// <summary>
        /// Before an URL has been found for the import rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created rule.</returns>
        CSSImportRule CreateImportRule(IEnumerator<CssToken> tokens)
        {
            var import = new CSSImportRule();

            if (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.String || token.Type == CssTokenType.Url)
                {
                    import.Href = ((CssStringToken)token).Data;

                    if (tokens.MoveNext())
                        import.Media = InMediaList(tokens);
                }

                JumpToNextSemicolon(tokens);
            }

            return import;
        }

        #endregion

        #region States

        /// <summary>
        /// Creates a rule with the enumeration of tokens.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated CSS rule.</returns>
        CSSRule CreateRule(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            if (token.Type == CssTokenType.AtKeyword)
            {
                switch (((CssKeywordToken)token).Data)
                {
                    case RuleNames.Media:
                        return CreateMediaRule(tokens);
                    case RuleNames.Page:
                        return CreatePageRule(tokens);
                    case RuleNames.Import:
                        return CreateImportRule(tokens);
                    case RuleNames.FontFace:
                        return CreateFontFaceRule(tokens);
                    case RuleNames.Charset:
                        return CreateCharsetRule(tokens);
                    case RuleNames.Namespace:
                        return CreateNamespaceRule(tokens);
                    case RuleNames.Supports:
                        return CreateSupportsRule(tokens);
                    case RuleNames.Keyframes:
                        return CreateKeyframesRule(tokens);
                    case RuleNames.Document:
                        return CreateDocumentRule(tokens);
                }

                SkipUnknownRule(tokens);
                return null;
            }
            else if (token.Type == CssTokenType.CurlyBracketClose || token.Type == CssTokenType.RoundBracketClose || token.Type == CssTokenType.SquareBracketClose)
            {
                while (tokens.MoveNext()) ;
                return null;
            }
            else
            {
                var selector = InSelector(tokens);

                if (selector == null)
                {
                    SkipUnknownRule(tokens);
                    return null;
                }

                var rule = new CSSStyleRule { Selector = selector };
                FillDeclarations(rule.Style, tokens);
                return rule;
            }
        }

        /// <summary>
        /// In the condition text of a supports rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The condition text.</returns>
        String Condition(IEnumerator<CssToken> tokens)
        {
            var buffer = Pool.NewStringBuilder();
            tokenizer.IgnoreWhitespace = false;

            do
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.CurlyBracketOpen)
                    break;

                buffer.Append(token.ToValue());
            }
            while (tokens.MoveNext());

            tokenizer.IgnoreWhitespace = true;
            return buffer.ToPool();
        }

        #endregion

        #region Style

        /// <summary>
        /// State that is called once we are in a CSS selector.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated selector.</returns>
        Selector InSelector(IEnumerator<CssToken> tokens)
        {
            tokenizer.IgnoreWhitespace = false;
            selector.Reset();

            do
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.CurlyBracketOpen || token.Type == CssTokenType.CurlyBracketClose)
                    break;

                if (selector.Apply(token) == false)
                {
                    tokenizer.IgnoreWhitespace = true;
                    return null;
                }
            }
            while (tokens.MoveNext());

            return selector.Result;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        /// <param name="style">The style to populate.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created property.</returns>
        CSSProperty Declaration(CSSStyleDeclaration style, IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            if (token.Type == CssTokenType.Ident)
            {
                var propertyName = ((CssKeywordToken)token).Data;

                if (!tokens.MoveNext())
                    return null;

                token = tokens.Current;

                if (token.Type != CssTokenType.Colon)
                {
                    JumpToEndOfDeclaration(tokens);
                    return null;
                }

                if (tokens.MoveNext())
                {
                    var property = CssPropertyFactory.Create(propertyName, style);

                    if (property != null)
                    {
                        if (property.TrySetValue(InValue(tokens)))
                            property.Important = IsImportant(tokens);
                        else if (style != null)
                            property = null;
                    }

                    JumpToEndOfDeclaration(tokens);
                    return property;
                }
            }

            return null;
        }

        /// <summary>
        /// In the important part of a declaration.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>True if the declaration is important, otherwise false.</returns>
        Boolean IsImportant(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;
            return token.Type == CssTokenType.Ident && ((CssKeywordToken)token).Data == important;
        }

        #endregion

        #region Document Functions

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The iteration over all found document functions.</returns>
        IEnumerable<Tuple<CSSDocumentRule.DocumentFunction, String>> InDocumentFunctions(IEnumerator<CssToken> tokens)
        {
            do
            {
                var function = InDocumentFunction(tokens);

                if (function != null)
                    yield return function;
            }
            while (tokens.MoveNext() && tokens.Current.Type == CssTokenType.Comma);
        }

        /// <summary>
        /// Called before a document function has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>A single document function or null if none has been found.</returns>
        Tuple<CSSDocumentRule.DocumentFunction, String> InDocumentFunction(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            switch (token.Type)
            {
                case CssTokenType.Url:
                    return Tuple.Create(CSSDocumentRule.DocumentFunction.Url, ((CssStringToken)token).Data);

                case CssTokenType.UrlPrefix:
                    return Tuple.Create(CSSDocumentRule.DocumentFunction.UrlPrefix, ((CssStringToken)token).Data);

                case CssTokenType.Domain:
                    return Tuple.Create(CSSDocumentRule.DocumentFunction.Domain, ((CssStringToken)token).Data);

                case CssTokenType.Function:
                    if (String.Compare(((CssKeywordToken)token).Data, FunctionNames.Regexp, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (!tokens.MoveNext())
                            break;

                        token = tokens.Current;

                        if (token.Type == CssTokenType.String)
                            return Tuple.Create(CSSDocumentRule.DocumentFunction.RegExp, ((CssStringToken)token).Data);

                        JumpToClosedArguments(tokens);
                    }
                    break;
            }

            return null;
        }

        #endregion

        #region Keyframes

        /// <summary>
        /// Before the name of an @keyframes rule has been detected.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The name of the keyframes.</returns>
        String InKeyframesName(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            if (token.Type == CssTokenType.Ident)
            {
                tokens.MoveNext();
                return ((CssKeywordToken)token).Data;
            }

            return String.Empty;
        }

        /// <summary>
        /// Before the curly bracket of an @keyframes rule has been seen.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated keyframe data.</returns>
        CSSKeyframeRule CreateKeyframeRule(IEnumerator<CssToken> tokens)
        {
            var rule = new CSSKeyframeRule();
            rule.KeyText = InKeyframeText(tokens);
            FillDeclarations(rule.Style, tokens);
            return rule;
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The text of the keyframe.</returns>
        String InKeyframeText(IEnumerator<CssToken> tokens)
        {
            var buffer = Pool.NewStringBuilder();

            do
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.CurlyBracketOpen)
                    break;

                buffer.Append(token.ToValue());
            } while (tokens.MoveNext());

            return buffer.ToPool();
        }

        #endregion

        #region Media List

        /// <summary>
        /// Before any medium has been found for the @media or @import rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated medialist.</returns>
        MediaList InMediaList(IEnumerator<CssToken> tokens)
        {
            var list = new MediaList();

            do
            {
                var medium = InMediaValue(tokens);

                if (medium == null)
                    break;

                list.Add(medium);
            }
            while (tokens.Current.Type == CssTokenType.Comma && tokens.MoveNext());

            if (tokens.Current.Type != CssTokenType.CurlyBracketOpen)
            {
                if (tokens.Current.Type == CssTokenType.RoundBracketClose)
                    tokens.MoveNext();

                JumpToEndOfDeclaration(tokens);
            }
            else if (list.Length == 0 && tokens.MoveNext())
                JumpToEndOfDeclaration(tokens);

            return list;
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The medium.</returns>
        CSSMedium InMediaValue(IEnumerator<CssToken> tokens)
        {
            var medium = GetMedium(tokens);
            var token = tokens.Current;

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = ((CssKeywordToken)token).Data;

                if (!tokens.MoveNext())
                    return medium;
                
                token = tokens.Current;

                if (token.Type != CssTokenType.Ident || String.Compare(((CssKeywordToken)token).Data, "and", StringComparison.OrdinalIgnoreCase) != 0 || !tokens.MoveNext())
                    return medium;
            }

            do
            {
                if (tokens.Current.Type != CssTokenType.RoundBracketOpen)
                    return null;
                else if (!tokens.MoveNext())
                    return medium;

                var pair = GetConstraint(tokens);

                if (pair == null || tokens.Current.Type != CssTokenType.RoundBracketClose || !medium.AddConstraint(pair.Item1, pair.Item2))
                    return null;
                else if (!tokens.MoveNext())
                    return medium;

                token = tokens.Current;

                if (token.Type != CssTokenType.Ident || String.Compare(((CssKeywordToken)token).Data, "and", StringComparison.OrdinalIgnoreCase) != 0)
                    break;
            }
            while (tokens.MoveNext()) ;

            return medium;
        }

        Tuple<String, CSSValue> GetConstraint(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            if (token.Type != CssTokenType.Ident)
            {
                JumpToClosedArguments(tokens);
                return null;
            }

            value.Reset();
            var feature = ((CssKeywordToken)token).Data;
            tokens.MoveNext();
            token = tokens.Current;

            if (token.Type == CssTokenType.Colon)
            {
                tokenizer.IgnoreWhitespace = false;
                tokens.MoveNext();

                while (GetSingleValue(tokens) && tokens.Current.Type != CssTokenType.RoundBracketClose) ;

                tokenizer.IgnoreWhitespace = true;
            }

            return Tuple.Create(feature, value.ToValue());
        }

        static CSSMedium GetMedium(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;
            var medium = new CSSMedium();

            if (token.Type == CssTokenType.Ident)
            {
                var ident = ((CssKeywordToken)token).Data;

                if (String.Compare(ident, "not", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    tokens.MoveNext();
                    medium.IsInverse = true;
                }
                else if (String.Compare(ident, "only", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    tokens.MoveNext();
                    medium.IsExclusive = true;
                }
            }

            return medium;
        }

        #endregion

        #region Value

        //HASHLESS in QUIRKSMODE:
        //  background-color
        //  border-color
        //  border-top-color
        //  border-right-color
        //  border-bottom-color
        //  border-left-color
        //  color

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The computed value.</returns>
        CSSValue InValue(IEnumerator<CssToken> tokens)
        {
            tokenizer.IgnoreWhitespace = false;
            value.Reset();

            while (GetSingleValue(tokens)) ;

            tokenizer.IgnoreWhitespace = true;
            return value.ToValue();
        }

        Boolean GetSingleValue(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            switch (token.Type)
            {
                case CssTokenType.Dimension: // e.g. "3px"
                case CssTokenType.Percentage: // e.g. "5%"
                    return TakeValue(ToUnit((CssUnitToken)token), tokens);
                case CssTokenType.Hash:// e.g. "#ABCDEF"
                    return TakeValue(GetColorFromHexValue(((CssKeywordToken)token).Data), tokens);
                case CssTokenType.Delim:// e.g. "#"
                    return GetValueFromDelim(((CssDelimToken)token).Data, tokens);
                case CssTokenType.Ident: // e.g. "auto"
                    value.AddValue(ToIdentifier(((CssKeywordToken)token).Data));
                    return tokens.MoveNext();
                case CssTokenType.String:// e.g. "'i am a string'"
                    value.AddValue(new CSSStringValue(((CssStringToken)token).Data));
                    return tokens.MoveNext();
                case CssTokenType.Url:// e.g. "url('this is a valid URL')"
                    value.AddValue(new CSSPrimitiveValue<Location>(new Location(((CssStringToken)token).Data)));
                    return tokens.MoveNext();
                case CssTokenType.Number: // e.g. "173"
                    value.AddValue(ToNumber((CssNumberToken)token));
                    return tokens.MoveNext();
                case CssTokenType.Function: //e.g. rgba(...)
                    return GetValueFunction(tokens);
                case CssTokenType.Comma:
                    value.NextArgument();
                    return tokens.MoveNext();
                case CssTokenType.Whitespace:
                    return tokens.MoveNext();
                case CssTokenType.Semicolon:
                case CssTokenType.CurlyBracketClose:
                    return false;
            }

            value.IsFaulted = true;
            return false;
        }

        Boolean TakeValue(CSSValue val, IEnumerator<CssToken> tokens)
        {
            var nxt = tokens.MoveNext();

            if (val == null)
            {
                value.IsFaulted = true;
                return false;
            }

            value.AddValue(val);
            return nxt;
        }

        Boolean GetValueFromDelim(Char delimiter, IEnumerator<CssToken> tokens)
        {
            if (delimiter == Specification.Num && tokens.MoveNext())
                return GetColorFromHexValue(tokens);

            if (delimiter == Specification.Solidus)
            {
                value.AddValue(CSSValue.Delimiter);
                return tokens.MoveNext();
            }

            if (delimiter != Specification.ExclamationMark || !tokens.MoveNext() || !IsImportant(tokens))
                value.IsFaulted = true;

            return false;
        }

        /// <summary>
        /// Gathers a value from a CSS function.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The computed function.</returns>
        Boolean GetValueFunction(IEnumerator<CssToken> tokens)
        {
            var name = ((CssKeywordToken)tokens.Current).Data;
            value.AddFunction(name);

            if (!tokens.MoveNext())
                return false;

            while (GetSingleValue(tokens))
            {
                if (tokens.Current.Type == CssTokenType.RoundBracketClose)
                    break;
            }

            value.CloseFunction();
            return tokens.MoveNext();
        }

        /// <summary>
        /// Called if a # sign has been found.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated value.</returns>
        Boolean GetColorFromHexValue(IEnumerator<CssToken> tokens)
        {
            var buffer = Pool.NewStringBuilder();
            var alive = true;

            do
            {
                var token = tokens.Current;

                if (token.Type != CssTokenType.Number && token.Type != CssTokenType.Dimension && token.Type != CssTokenType.Ident)
                    break;

                var rest = token.ToValue();

                if (buffer.Length + rest.Length > 6)
                    break;

                buffer.Append(rest);
            } while (alive = tokens.MoveNext());

            var color = GetColorFromHexValue(buffer.ToPool());

            if (color != null)
                value.AddValue(color);

            return alive;
        }

        /// <summary>
        /// Called in a value - a hash (probably hex) value has been found.
        /// </summary>
        /// <param name="hexColor">The value of the token.</param>
        /// <returns>The generated value.</returns>
        static CSSPrimitiveValue<Color> GetColorFromHexValue(String hexColor)
        {
            Color colorValue;

            if (Color.TryFromHex(hexColor, out colorValue))
                return new CSSPrimitiveValue<Color>(colorValue);

            return null;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        void Kernel()
        {
            var tokens = tokenizer.Tokens.GetEnumerator();

            while (tokens.MoveNext())
            {
                var rule = CreateRule(tokens);

                if (rule == null)
                    continue;

                rule.ParentStyleSheet = sheet;
                sheet.CssRules.Add(rule);
            }
        }

        /// <summary>
        /// Fills the given parent rule with rules given by the tokens.
        /// </summary>
        /// <param name="parentRule">The parent rule to fill.</param>
        /// <param name="tokens">The stream of tokens.</param>
        void FillRules(CSSGroupingRule parentRule, IEnumerator<CssToken> tokens)
        {
            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == CssTokenType.CurlyBracketClose)
                    break;

                var rule = CreateRule(tokens);

                if (rule == null)
                    continue;

                rule.ParentStyleSheet = sheet;
                rule.ParentRule = parentRule;
                parentRule.CssRules.Add(rule);
            }
        }

        /// <summary>
        /// Fills the given keyframe rule with rules given by the tokens.
        /// </summary>
        /// <param name="parentRule">The parent rule to fill.</param>
        /// <param name="tokens">The stream of tokens.</param>
        void FillRules(CSSKeyframesRule parentRule, IEnumerator<CssToken> tokens)
        {
            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == CssTokenType.CurlyBracketClose)
                    break;

                var rule = CreateKeyframeRule(tokens);

                if (rule == null)
                    continue;

                rule.ParentStyleSheet = sheet;
                rule.ParentRule = parentRule;
                parentRule.CssRules.Add(rule);
            }
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        /// <param name="style">The style to declare.</param>
        /// <param name="tokens">The stream of tokens.</param>
        void FillDeclarations(CSSStyleDeclaration style, IEnumerator<CssToken> tokens)
        {
            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == CssTokenType.CurlyBracketClose)
                    break;

                var property = Declaration(style, tokens);

                if (property == null)
                    continue;

                style.Set(property);
            }
        }

        static void JumpToEndOfDeclaration(IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        goto case CssTokenType.Semicolon;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.Semicolon:
                        if (round == 0 && curly == 0 && square == 0)
                            return;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        static void JumpToNextSemicolon(IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.Semicolon:
                        if (round == 0 && curly == 0 && square == 0)
                            return;

                        break;
                }
            }
            while (tokens.MoveNext());
        }

        static void JumpToClosedArguments(IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        if (round == 0 && curly == 0 && square == 0)
                            return;

                        round--;
                        break;
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        /// <summary>
        /// State that is called once in the head of an unknown @ rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        static void SkipUnknownRule(IEnumerator<CssToken> tokens)
        {
            var curly = 0;
            var round = 0;
            var square = 0;
            var cont = true;

            do
            {
                var token = tokens.Current;

                switch (token.Type)
                {
                    case CssTokenType.Semicolon:
                        cont = curly != 0 || round != 0 || square != 0;
                        break;
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        cont = curly != 0 || round != 0 || square != 0;
                        break;
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                }
            }
            while (cont && tokens.MoveNext());
        }

        /// <summary>
        /// Converts the given unit to a value. Uses number for 0.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>The created value.</returns>
        static CSSValue ToUnit(CssUnitToken token)
        {
            if (token.Type == CssTokenType.Percentage)
                return new CSSPrimitiveValue<Percent>(new Percent(token.Data));

            switch (token.Unit.ToLower())
            {
                case "em": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Em));
                case "cm": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Cm));
                case "ex": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Ex));
                case "in": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.In));
                case "mm": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Mm));
                case "pc": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Pc));
                case "pt": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Pt));
                case "px": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Px));
                case "rem": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Rem));
                case "ch": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Ch));
                case "vw": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Vw));
                case "vh": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Vh));
                case "vmin": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Vmin));
                case "vmax": return new CSSPrimitiveValue<Length>(new Length(token.Data, Length.Unit.Vmax));

                case "ms": return new CSSPrimitiveValue<Time>(new Time(token.Data, Time.Unit.Ms));
                case "s": return new CSSPrimitiveValue<Time>(new Time(token.Data, Time.Unit.S));

                case "dpi": return new CSSPrimitiveValue<Resolution>(new Resolution(token.Data, Resolution.Unit.Dpi));
                case "dpcm": return new CSSPrimitiveValue<Resolution>(new Resolution(token.Data, Resolution.Unit.Dpcm));
                case "dppx": return new CSSPrimitiveValue<Resolution>(new Resolution(token.Data, Resolution.Unit.Dppx));

                case "deg": return new CSSPrimitiveValue<Angle>(new Angle(token.Data, Angle.Unit.Deg));
                case "grad": return new CSSPrimitiveValue<Angle>(new Angle(token.Data, Angle.Unit.Grad));
                case "rad": return new CSSPrimitiveValue<Angle>(new Angle(token.Data, Angle.Unit.Rad));
                case "turn": return new CSSPrimitiveValue<Angle>(new Angle(token.Data, Angle.Unit.Turn));

                case "hz": return new CSSPrimitiveValue<Frequency>(new Frequency(token.Data, Frequency.Unit.Hz));
                case "khz": return new CSSPrimitiveValue<Frequency>(new Frequency(token.Data, Frequency.Unit.Khz));
            }

            return null;
        }

        /// <summary>
        /// Converts the given identifier to a value. Uses inherit for inherit.
        /// </summary>
        /// <param name="identifier">The identifier to consider.</param>
        /// <returns>The created value.</returns>
        static CSSValue ToIdentifier(String identifier)
        {
            if (identifier == inherit)
                return CSSValue.Inherit;

            return new CSSIdentifierValue(identifier);
        }

        /// <summary>
        /// Converts the given number to a value. Uses an allocated value for the 0.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>The created value.</returns>
        static CSSValue ToNumber(CssNumberToken token)
        {
            if (token.Data == 0f)
                return new CSSPrimitiveValue<Number>(Number.Zero);

            return new CSSPrimitiveValue<Number>(new Number(token.Data));
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="selector">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The Selector object.</returns>
        public static Selector ParseSelector(String selector, IConfiguration configuration = null)
        {
            var tokenizer = new CssTokenizer(new SourceManager(selector, configuration.DefaultEncoding()));
            tokenizer.IgnoreComments = true;
            var tokens = tokenizer.Tokens;
            var creator = Pool.NewSelectorConstructor();

            foreach (var token in tokens)
            {
                if (creator.Apply(token) == false)
                    throw new DomException(ErrorCode.Syntax);
            }

            return creator.ToPool();
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS stylesheet.
        /// </summary>
        /// <param name="stylesheet">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSStyleSheet object.</returns>
        public static ICssStyleSheet ParseStyleSheet(String stylesheet, IConfiguration configuration = null)
        {
            var parser = new CssParser(stylesheet, configuration ?? Configuration.Default);
            return parser.Result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSRule object.</returns>
        public static CSSRule ParseRule(String rule, IConfiguration configuration = null)
        {
            var parser = new CssParser(rule, configuration ?? Configuration.Default);
            parser.Parse();

            if (parser.sheet.CssRules.Length == 0)
                return null;

            return parser.sheet.CssRules[0];
        }

        /// <summary>
        /// Takes a string and transforms it into CSS declarations.
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration ParseDeclarations(String declarations, IConfiguration configuration = null)
        {
            var decl = new CSSStyleDeclaration();
            AppendDeclarations(decl, declarations, configuration);
            return decl;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (CSS property).
        /// </summary>
        /// <param name="declaration">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSProperty object.</returns>
        public static CSSProperty ParseDeclaration(String declaration, IConfiguration configuration = null)
        {
            var parser = new CssParser(declaration, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            return parser.Declaration(null, tokens);
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSValue object.</returns>
        public static CSSValue ParseValue(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            return parser.InValue(tokens);
        }

        /// <summary>
        /// Takes a string and transforms it into a stream of CSS mediums.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The stream of medias.</returns>
        public static IEnumerable<CSSMedium> ParseMediaList(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (tokens.MoveNext())
            {
                do
                {
                    var medium = parser.InMediaValue(tokens);

                    if (medium == null)
                        break;

                    yield return medium;
                }
                while (tokens.MoveNext());
            }

            if (tokens.MoveNext())
                throw new DomException(ErrorCode.Syntax);
        }

        #endregion

        #region Internal static methods

        /// <summary>
        /// Takes a string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSValueList object.</returns>
        internal static CSSValueList ParseValueList(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();
            var value = tokens.MoveNext() ? parser.InValue(tokens) : null;
            var values = value as CSSValueList;

            if (values == null)
            {
                values = new CSSValueList();

                if (value != null)
                    values.Add(value);
            }

            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] == CSSValue.Separator)
                {
                    for (var j = values.Length - 1; j >= i; j--)
                        values.Remove(values[j]);

                    break;
                }
            }

            return values;
        }

        internal static CSSMedium ParseMedium(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (tokens.MoveNext())
            {
                var medium = parser.InMediaValue(tokens);

                if (tokens.MoveNext())
                    throw new DomException(ErrorCode.Syntax);

                return medium;
            }

            return null;
        }

        /// <summary>
        /// Takes a comma separated string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSValueList object.</returns>
        internal static List<CSSValueList> ParseMultipleValues(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();
            var value = tokens.MoveNext() ? parser.InValue(tokens) : new CSSValueList();
            var values = value as CSSValueList;

            if (values == null)
            {
                values = new CSSValueList();

                if (value != null)
                    values.Add(value);
            }

            return values.ToList();
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSKeyframeRule object.</returns>
        internal static CSSKeyframeRule ParseKeyframeRule(String rule, IConfiguration configuration = null)
        {
            var parser = new CssParser(rule, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return new CSSKeyframeRule();

            return parser.CreateKeyframeRule(tokens);
        }

        /// <summary>
        /// Takes a string and appends all rules to the given list of properties.
        /// </summary>
        /// <param name="list">The list of css properties to append to.</param>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        internal static void AppendDeclarations(CSSStyleDeclaration list, String declarations, IConfiguration configuration = null)
        {
            var parser = new CssParser(declarations, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();
            parser.FillDeclarations(list, tokens);
        }

        #endregion

        #region Event-Helpers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        void RaiseErrorOccurred(ErrorCode code)
        {
            if (ParseError != null)
            {
                var pck = new ParseErrorEventArgs((Int32)code, code.GetErrorMessage());
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ParseError(this, pck);
            }
        }

        #endregion
    }
}
