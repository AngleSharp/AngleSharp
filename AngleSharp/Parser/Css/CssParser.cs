namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class CssParser
    {
        #region Fields

        readonly CssSelectorConstructor selector;
        readonly CssValueBuilder value;
        readonly CssTokenizer tokenizer;
        readonly Object sync;
        readonly CSSStyleSheet sheet;

        Boolean started;
        Task<ICssStyleSheet> task;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ParseError
        {
            add { tokenizer.ErrorOccurred += value; }
            remove { tokenizer.ErrorOccurred -= value; }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS parser instance with a new stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public CssParser(String source, IConfiguration configuration = null)
            : this(new CSSStyleSheet(new TextSource(source)) { Options = configuration })
        { }

        /// <summary>
        /// Creates a new CSS parser instance with an new stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">[Optional] The configuration to use.</param>
        public CssParser(Stream stream, IConfiguration configuration = null)
            : this(new CSSStyleSheet(new TextSource(stream, configuration.DefaultEncoding())) { Options = configuration })
        { }

        /// <summary>
        /// Creates a new CSS parser instance parser with the specified stylesheet
        /// based on the given source manager.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        internal CssParser(CSSStyleSheet stylesheet)
        {
            var owner = stylesheet.OwnerNode as Element;
            selector = new CssSelectorConstructor();
            value = new CssValueBuilder();
            sync = new Object();
            tokenizer = new CssTokenizer(stylesheet.Source)
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
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

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task<ICssStyleSheet> ParseAsync()
        {
            return ParseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task<ICssStyleSheet> ParseAsync(CancellationToken cancelToken)
        {
            lock (sync)
            {
                if (!started)
                {
                    started = true;
                    task = KernelAsync(cancelToken);
                }
            }

            return task;
        }

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        public ICssStyleSheet Parse()
        {
            if (!started)
            {
                started = true;
                Kernel();
            }

            return sheet;
        }

        #endregion

        #region Create Rules

        /// <summary>
        /// Called before a medialist has been created.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created media rule.</returns>
        internal static CSSMediaRule CreateMediaRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var list = tokens.MoveNext() ? parser.InMediaList(tokens) : new MediaList();
            var rule = new CSSMediaRule(list);

            if (tokens.Current.Type != CssTokenType.CurlyBracketOpen)
                return null;

            parser.FillRules(rule, tokens);
            return rule;
        }

        /// <summary>
        /// Called before a page selector has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created page rule.</returns>
        internal static CSSPageRule CreatePageRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSPageRule();

            if (tokens.MoveNext())
                rule.Selector = parser.InSelector(tokens);

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                parser.FillDeclarations(rule.Style, tokens);

            return rule;
        }

        /// <summary>
        /// Called before the body of the font-face rule.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created font-face rule.</returns>
        internal static CSSFontFaceRule CreateFontFaceRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSFontFaceRule();

            if (tokens.MoveNext() && tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                parser.FillDeclarations(rule.Style, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a supports condition has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created supports rule.</returns>
        internal static CSSSupportsRule CreateSupportsRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSSupportsRule();

            if (tokens.MoveNext())
                rule.Condition = parser.InCondition(tokens);

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                parser.FillRules(rule, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a document function has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated document rule.</returns>
        internal static CSSDocumentRule CreateDocumentRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSDocumentRule();

            if (tokens.MoveNext())
                rule.Conditions.AddRange(parser.InDocumentFunctions(tokens));

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                parser.FillRules(rule, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a keyframes identifier has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated keyframes rule.</returns>
        internal static CSSKeyframesRule CreateKeyframesRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSKeyframesRule();

            if (tokens.MoveNext())
                rule.Name = parser.InKeyframesName(tokens);

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                parser.FillRules(rule, tokens);

            return rule;
        }

        /// <summary>
        /// Called before a prefix has been found for the namespace rule.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated namespace rule.</returns>
        internal static CSSNamespaceRule CreateNamespaceRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSNamespaceRule();

            if (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.Ident)
                {
                    rule.Prefix = token.Data;

                    if (tokens.MoveNext())
                        token = tokens.Current;
                }

                if (token.Type == CssTokenType.Url)
                    rule.NamespaceUri = token.Data;

                JumpToNextSemicolon(tokens);
            }

            return rule;
        }

        /// <summary>
        /// Before a charset string has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated rule.</returns>
        internal static CSSCharsetRule CreateCharsetRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSCharsetRule();

            if (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.String)
                {
                    rule.CharacterSet = ((CssStringToken)token).Data;
                    tokens.MoveNext();
                }

                JumpToNextSemicolon(tokens);
            }

            return rule;
        }

        /// <summary>
        /// Before an URL has been found for the import rule.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created rule.</returns>
        internal static CSSImportRule CreateImportRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var import = new CSSImportRule();

            if (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.String || token.Type == CssTokenType.Url)
                {
                    import.Href = ((CssStringToken)token).Data;

                    if (tokens.MoveNext())
                        import.Media = parser.InMediaList(tokens);
                }

                JumpToNextSemicolon(tokens);
            }

            return import;
        }

        /// <summary>
        /// An unidentified @-rule has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created rule.</returns>
        static CSSUnknownRule CreateUnknownRule(CssParser parser, IEnumerator<CssToken> tokens)
        {
            var rule = new CSSUnknownRule(tokens.Current.Data);
            var prelude = Pool.NewStringBuilder();
            var round = 0;
            var square = 0;
            parser.tokenizer.IgnoreWhitespace = false;

            while (tokens.MoveNext())
            {
                var token = tokens.Current;

                if (round <= 0 && square <= 0 && (token.Type == CssTokenType.Semicolon || token.Type == CssTokenType.CurlyBracketOpen))
                    break;
                else if (token.Type == CssTokenType.RoundBracketOpen)
                    round++;
                else if (token.Type == CssTokenType.RoundBracketClose)
                    round--;
                else if (token.Type == CssTokenType.SquareBracketOpen)
                    square++;
                else if (token.Type == CssTokenType.SquareBracketClose)
                    square--;

                prelude.Append(token.ToValue());
            }

            rule.Prelude = prelude.ToPool().Trim();
            parser.tokenizer.IgnoreWhitespace = true;

            if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
                parser.FillRules(rule, tokens);

            return rule;
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

            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                {
                    var rule = this.CreateAtRule(token.Data, tokens);

                    if (rule == null)
                    {
                        RaiseErrorOccurred(ErrorCode.UnknownAtRule);
                        SkipUnknownRule(tokens);
                    }

                    return rule;
                }
                case CssTokenType.CurlyBracketOpen:
                {
                    RaiseErrorOccurred(ErrorCode.InvalidBlockStart);
                    SkipUnknownRule(tokens);
                    return null;
                }
                case CssTokenType.String:
                case CssTokenType.Url:
                case CssTokenType.CurlyBracketClose:
                case CssTokenType.RoundBracketClose:
                case CssTokenType.SquareBracketClose:
                {
                    RaiseErrorOccurred(ErrorCode.InvalidToken);
                    SkipUnknownRule(tokens);
                    return null;
                }
                default:
                {
                    var rule = new CSSStyleRule();
                    rule.Selector = InSelector(tokens);
                    FillDeclarations(rule.Style, tokens);

                    if (rule.Selector == null)
                        return null;

                    return rule;
                }
            }
        }

        #endregion

        #region Style

        /// <summary>
        /// State that is called once we are in a CSS selector.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated selector.</returns>
        ISelector InSelector(IEnumerator<CssToken> tokens)
        {
            tokenizer.IgnoreWhitespace = false;
            selector.Reset();

            do
            {
                var token = tokens.Current;

                if (token.Type == CssTokenType.CurlyBracketOpen || token.Type == CssTokenType.CurlyBracketClose)
                    break;

                selector.Apply(token);
            }
            while (tokens.MoveNext());

            if (selector.IsValid == false)
                RaiseErrorOccurred(ErrorCode.InvalidSelector);

            tokenizer.IgnoreWhitespace = true;
            return selector.Result;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <param name="style">The style to populate.</param>
        /// <returns>The created property.</returns>
        CSSProperty Declaration(IEnumerator<CssToken> tokens, CSSStyleDeclaration style)
        {
            var token = tokens.Current;

            if (token.Type == CssTokenType.Ident)
            {
                var propertyName = token.Data;

                if (!tokens.MoveNext())
                {
                    RaiseErrorOccurred(ErrorCode.ColonMissing);
                }
                else if (tokens.Current.Type != CssTokenType.Colon)
                {
                    RaiseErrorOccurred(ErrorCode.ColonMissing);
                    JumpToEndOfDeclaration(tokens);
                }
                else if (tokens.MoveNext())
                {
                    var property = style.CreateProperty(propertyName);

                    if (property == null)
                    {
                        RaiseErrorOccurred(ErrorCode.UnknownDeclarationName);
                        property = new CSSUnknownProperty(propertyName, style);
                    }

                    var value = InValue(tokens);

                    if (value != null && property.TrySetValue(value))
                        style.SetProperty(property);

                    if (IsImportant(tokens))
                        property.IsImportant = true;

                    JumpToEndOfDeclaration(tokens);
                    return property;
                }
                else
                {
                    RaiseErrorOccurred(ErrorCode.ValueMissing);
                }
            }
            else
                RaiseErrorOccurred(ErrorCode.IdentExpected);

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
            return token.Type == CssTokenType.Ident && token.Data == Keywords.Important;
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
                    if (String.Compare(token.Data, FunctionNames.Regexp, StringComparison.OrdinalIgnoreCase) == 0)
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
                return token.Data;
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
            rule.Key = InKeyframeText(tokens);
            FillDeclarations(rule.Style, tokens);

            if (rule.Key == null)
                return null;

            return rule;
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The text of the keyframe.</returns>
        KeyframeSelector InKeyframeText(IEnumerator<CssToken> tokens)
        {
            var keys = new List<Percent>();

            do
            {
                var token = tokens.Current;

                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;
                    else if (token.Type != CssTokenType.Comma || !tokens.MoveNext())
                        return null;

                    token = tokens.Current;
                }

                if (token.Type == CssTokenType.Percentage)
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.From))
                    keys.Add(Percent.Zero);
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.To))
                    keys.Add(Percent.Hundred);
                else
                    return null;
            } while (tokens.MoveNext());

            return new KeyframeSelector(keys);
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

                if (tokens.Current.Type == CssTokenType.CurlyBracketOpen)
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
        CssMedium InMediaValue(IEnumerator<CssToken> tokens)
        {
            var medium = GetMedium(tokens);
            var token = tokens.Current;

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;

                if (!tokens.MoveNext())
                    return medium;
                
                token = tokens.Current;

                if (token.Type != CssTokenType.Ident || String.Compare(token.Data, Keywords.And, StringComparison.OrdinalIgnoreCase) != 0 || !tokens.MoveNext())
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

                if (token.Type != CssTokenType.Ident || String.Compare(token.Data, Keywords.And, StringComparison.OrdinalIgnoreCase) != 0)
                    break;
            }
            while (tokens.MoveNext()) ;

            return medium;
        }

        Tuple<String, ICssValue> GetConstraint(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;

            if (token.Type != CssTokenType.Ident)
            {
                JumpToClosedArguments(tokens);
                return null;
            }

            value.Reset();
            var feature = token.Data;
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

        static CssMedium GetMedium(IEnumerator<CssToken> tokens)
        {
            var token = tokens.Current;
            var medium = new CssMedium();

            if (token.Type == CssTokenType.Ident)
            {
                var ident = token.Data;

                if (String.Compare(ident, Keywords.Not, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    tokens.MoveNext();
                    medium.IsInverse = true;
                }
                else if (String.Compare(ident, Keywords.Only, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    tokens.MoveNext();
                    medium.IsExclusive = true;
                }
            }

            return medium;
        }

        #endregion

        #region Value

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The computed value.</returns>
        ICssValue InValue(IEnumerator<CssToken> tokens)
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
                    return TakeValue(GetColorFromHexValue(token.Data), tokens);
                case CssTokenType.Delim:// e.g. "#"
                    return GetValueFromDelim(token.Data[0], tokens);
                case CssTokenType.Ident: // e.g. "auto"
                    value.AddValue(token.ToIdentifier());
                    return tokens.MoveNext();
                case CssTokenType.String:// e.g. "'i am a string'"
                    value.AddValue(new CssString(token.Data));
                    return tokens.MoveNext();
                case CssTokenType.Url:// e.g. "url('this is a valid URL')"
                    value.AddValue(new CssUrl(token.Data));
                    return tokens.MoveNext();
                case CssTokenType.Number: // e.g. "173"
                    value.AddValue(((CssNumberToken)token).ToNumber());
                    return tokens.MoveNext();
                case CssTokenType.Function: // e.g. "rgba(...)"
                    return GetValueFunction(tokens);
                case CssTokenType.Comma: // e.g. ","
                    value.NextArgument();
                    return tokens.MoveNext();
                case CssTokenType.Whitespace: // e.g. " "
                    return tokens.MoveNext();
                case CssTokenType.Semicolon: // e.g. ";", "}"
                case CssTokenType.CurlyBracketClose:
                    break;
                default: // everything else is unexpected
                    RaiseErrorOccurred(ErrorCode.InputUnexpected);
                    value.IsFaulted = true;
                    break;
            }

            return false;
        }

        Boolean TakeValue(ICssValue val, IEnumerator<CssToken> tokens)
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
                value.AddValue(CssValue.Delimiter);
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
            var name = tokens.Current.Data;
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
                value.AddValue(color.Value);

            return alive;
        }

        /// <summary>
        /// Called in a value - a hash (probably hex) value has been found.
        /// </summary>
        /// <param name="hexColor">The value of the token.</param>
        /// <returns>The generated value.</returns>
        static Color? GetColorFromHexValue(String hexColor)
        {
            Color colorValue;

            if (Color.TryFromHex(hexColor, out colorValue))
                return colorValue;

            return null;
        }

        #endregion

        #region Condition

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The computed value.</returns>
        CSSSupportsRule.ICondition InCondition(IEnumerator<CssToken> tokens)
        {
            if (tokens.Current.Type == CssTokenType.RoundBracketOpen && tokens.MoveNext())
            {
                var token = tokens.Current;
                var condition = (CSSSupportsRule.ICondition)null;

                if (token.Type == CssTokenType.RoundBracketOpen)
                    condition = GroupCondition(tokens);
                else if (token.Type == CssTokenType.Ident)
                    condition = IdentCondition(tokens);

                if (condition != null && tokens.Current.Type == CssTokenType.RoundBracketClose && tokens.MoveNext())
                    return condition;
            }

            while (tokens.Current.Type != CssTokenType.CurlyBracketOpen && tokens.MoveNext()) ;
            return null;
        }

        CSSSupportsRule.ICondition IdentCondition(IEnumerator<CssToken> tokens)
        {
            var name = tokens.Current.Data;

            if (!tokens.MoveNext())
                return null;

            if (name.Equals(Keywords.Not, StringComparison.OrdinalIgnoreCase))
                return new CSSSupportsRule.NotCondition(InCondition(tokens));

            return DeclCondition(tokens, name);
        }

        CSSSupportsRule.ICondition DeclCondition(IEnumerator<CssToken> tokens, String name)
        {
            var style = new CSSStyleDeclaration();
            var property = CssPropertyFactory.Create(name, style);

            if (property == null)
                property = new CSSUnknownProperty(name, style);

            if (tokens.Current.Type != CssTokenType.Colon || !tokens.MoveNext())
                return null;

            value.Reset();

            while (GetSingleValue(tokens))
            {
                if (tokens.Current.Type == CssTokenType.RoundBracketClose)
                    break;
            }

            var result = value.ToValue();

            if (result == null)
                return null;

            if (property.IsImportant = IsImportant(tokens))
                tokens.MoveNext();

            return new CSSSupportsRule.DeclarationCondition(property, result);
        }

        CSSSupportsRule.ICondition GroupCondition(IEnumerator<CssToken> tokens)
        {
            var condition = InCondition(tokens);

            if (tokens.Current.Type == CssTokenType.Ident)
            {
                if (tokens.Current.Data.Equals(Keywords.And, StringComparison.OrdinalIgnoreCase))
                    return new CSSSupportsRule.AndCondition(Conditions(tokens, condition, Keywords.And));
                else if (tokens.Current.Data.Equals(Keywords.Or, StringComparison.OrdinalIgnoreCase))
                    return new CSSSupportsRule.OrCondition(Conditions(tokens, condition, Keywords.Or));
                else
                    condition = null;
            }

            return condition;
        }

        IEnumerable<CSSSupportsRule.ICondition> Conditions(IEnumerator<CssToken> tokens, CSSSupportsRule.ICondition start, String connector)
        {
            yield return start;

            while (tokens.MoveNext())
            {
                var condition = InCondition(tokens);

                if (condition == null)
                    break;

                yield return condition;

                if (!tokens.Current.Data.Equals(connector, StringComparison.OrdinalIgnoreCase))
                    break;
            }
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

                sheet.Rules.Add(rule, sheet, null);
            }
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        async Task<ICssStyleSheet> KernelAsync(CancellationToken cancelToken)
        {
            var source = sheet.Source;
            var tokens = tokenizer.Tokens.GetEnumerator();

            while (true)
            {
                if (source.Length - source.Index < 1024)
                    await source.Prefetch(8192, cancelToken).ConfigureAwait(false);

                if (!tokens.MoveNext())
                    break;

                var rule = CreateRule(tokens);

                if (rule == null)
                    continue;

                sheet.Rules.Add(rule, sheet, null);
            }

            return sheet;
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

                parentRule.Rules.Add(rule, sheet, parentRule);
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

                parentRule.Rules.Add(rule, sheet, parentRule);
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

                Declaration(tokens, style);

                if (tokens.Current.Type == CssTokenType.CurlyBracketClose)
                    break;
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
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        else
                            curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.Function:
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
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        else
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
                    case CssTokenType.Function:
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
                        if (round <= 0 && curly <= 0 && square <= 0)
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
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        else
                            round--;
                        break;
                    case CssTokenType.Function:
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
                        cont = curly > 0 || round > 0 || square > 0;
                        break;
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        cont = curly > 0 || round > 0 || square > 0;
                        break;
                    case CssTokenType.Function:
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
        static ICssValue ToUnit(CssUnitToken token)
        {
            if (token.Type == CssTokenType.Percentage)
                return new Percent(token.Value);

            return CssUnitFactory.Create(token.Value, token.Unit.ToLowerInvariant());
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="selectorText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The Selector object.</returns>
        public static ISelector ParseSelector(String selectorText, IConfiguration configuration = null)
        {
            var source = new TextSource(selectorText);
            var tokenizer = new CssTokenizer(source);
            tokenizer.IgnoreComments = true;
            var tokens = tokenizer.Tokens;
            var creator = Pool.NewSelectorConstructor();

            foreach (var token in tokens)
                creator.Apply(token);

            return creator.ToPool();
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="keyText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The Selector object.</returns>
        public static IKeyframeSelector ParseKeyText(String keyText, IConfiguration configuration = null)
        {
            var parser = new CssParser(keyText, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            var selector = parser.InKeyframeText(tokens);

            if (tokens.MoveNext())
                selector = null;

            return selector;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS stylesheet.
        /// </summary>
        /// <param name="stylesheet">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSStyleSheet object.</returns>
        public static ICssStyleSheet ParseStyleSheet(String stylesheet, IConfiguration configuration = null)
        {
            return new CssParser(stylesheet, configuration ?? Configuration.Default).Parse();
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        /// <param name="ruleText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSRule object.</returns>
        internal static CSSRule ParseRule(String ruleText, IConfiguration configuration = null)
        {
            var parser = new CssParser(ruleText, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            var rule = parser.CreateRule(tokens);

            if (tokens.MoveNext())
                rule = null;

            return rule;
        }

        /// <summary>
        /// Takes a string and transforms it into CSS declarations.
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSStyleDeclaration object.</returns>
        internal static CSSStyleDeclaration ParseDeclarations(String declarations, IConfiguration configuration = null)
        {
            var decl = new CSSStyleDeclaration();
            AppendDeclarations(decl, declarations, configuration);
            return decl;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (CSS property).
        /// </summary>
        /// <param name="declarationText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSProperty object.</returns>
        internal static CSSProperty ParseDeclaration(String declarationText, IConfiguration configuration = null)
        {
            var parser = new CssParser(declarationText, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            var declaration = parser.Declaration(tokens, new CSSStyleDeclaration());

            if (tokens.MoveNext())
                declaration = null;

            return declaration;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        /// <param name="valueText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSValue object.</returns>
        public static ICssValue ParseValue(String valueText, IConfiguration configuration = null)
        {
            var parser = new CssParser(valueText, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            var value = parser.InValue(tokens);

            if (tokens.MoveNext())
                value = null;

            return value;
        }

        /// <summary>
        /// Takes a string and transforms it into a stream of CSS mediums.
        /// </summary>
        /// <param name="mediaText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The stream of medias.</returns>
        internal static IEnumerable<CssMedium> ParseMediaList(String mediaText, IConfiguration configuration = null)
        {
            var parser = new CssParser(mediaText, configuration);
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

        /// <summary>
        /// Takes a string and transforms it into supports condition.
        /// </summary>
        /// <param name="conditionText">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The condition.</returns>
        internal static CSSSupportsRule.ICondition ParseCondition(String conditionText, IConfiguration configuration = null)
        {
            var parser = new CssParser(conditionText, configuration ?? Configuration.Default);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return null;

            var condition = parser.InCondition(tokens);

            if (tokens.MoveNext())
                condition = null;

            return condition;
        }

        #endregion

        #region Internal static methods

        internal static IEnumerable<Tuple<CSSDocumentRule.DocumentFunction, String>> ParseDocumentRules(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();

            if (!tokens.MoveNext())
                return Enumerable.Empty<Tuple<CSSDocumentRule.DocumentFunction, String>>();

            return parser.InDocumentFunctions(tokens);
        }

        /// <summary>
        /// Takes a string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSValueList object.</returns>
        internal static CssValueList ParseValueList(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();
            var value = tokens.MoveNext() ? parser.InValue(tokens) : null;
            var values = value as CssValueList;

            if (values == null)
            {
                values = new CssValueList();

                if (value != null)
                    values.Add(value);
            }

            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] == CssValue.Separator)
                {
                    for (var j = values.Length - 1; j >= i; j--)
                        values.RemoveAt(j);

                    break;
                }
            }

            return values;
        }

        internal static CssMedium ParseMedium(String source, IConfiguration configuration = null)
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
        internal static List<CssValueList> ParseMultipleValues(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var tokens = parser.tokenizer.Tokens.GetEnumerator();
            var value = tokens.MoveNext() ? parser.InValue(tokens) : new CssValueList();
            var values = value as CssValueList;

            if (values == null)
            {
                values = new CssValueList();

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
            tokenizer.RaiseErrorOccurred(code);
        }

        #endregion
    }
}
