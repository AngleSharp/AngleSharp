namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Css.Values;

    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class CssParser
    {
        #region Fields

        readonly CssSelectorConstructor _selector;
        readonly CssValueBuilder _value;
        readonly CssTokenizer _tokenizer;
        readonly Object _syncGuard;
        readonly CssStyleSheet _sheet;

        Boolean _started;
        Task<ICssStyleSheet> _task;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS parser instance with a new stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="configuration">
        /// [Optional] The configuration to use.
        /// </param>
        public CssParser(String source, IConfiguration configuration = null)
            : this(new CssStyleSheet(configuration, new TextSource(source)))
        { }

        /// <summary>
        /// Creates a new CSS parser instance with an new stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">
        /// [Optional] The configuration to use.
        /// </param>
        public CssParser(Stream stream, IConfiguration configuration = null)
            : this(new CssStyleSheet(configuration, new TextSource(stream, configuration.DefaultEncoding())))
        { }

        /// <summary>
        /// Creates a new CSS parser instance parser with the specified
        /// stylesheet based on the given source manager.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        internal CssParser(CssStyleSheet stylesheet)
        {
            var owner = stylesheet.OwnerNode as Element;
            _selector = new CssSelectorConstructor();
            _value = new CssValueBuilder();
            _syncGuard = new Object();
            _tokenizer = new CssTokenizer(stylesheet.Source, stylesheet.Options.Events);
            _started = false;
            _sheet = stylesheet;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the parser has been started asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return _task != null; }
        }

        /// <summary>
        /// Gets the resulting stylesheet of the parsing.
        /// </summary>
        public ICssStyleSheet Result
        {
            get
            {
                Parse();
                return _sheet;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <returns>
        /// The task which could be awaited or continued differently.
        /// </returns>
        public Task<ICssStyleSheet> ParseAsync()
        {
            return ParseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>
        /// The task which could be awaited or continued differently.
        /// </returns>
        public Task<ICssStyleSheet> ParseAsync(CancellationToken cancelToken)
        {
            lock (_syncGuard)
            {
                if (!_started)
                {
                    _started = true;
                    _task = KernelAsync(cancelToken);
                }
            }

            return _task;
        }

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        public ICssStyleSheet Parse()
        {
            if (!_started)
            {
                _started = true;
                Kernel();
            }

            return _sheet;
        }

        #endregion

        #region Create Rules

        /// <summary>
        /// Called before a medialist has been created.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created media rule.</returns>
        internal static CssMediaRule CreateMediaRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var list = parser.InMediaList(ref token);
            var rule = new CssMediaRule(list);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return parser.SkipDeclarations<CssMediaRule>(token);
            
            parser.FillRules(rule);
            return rule;
        }

        /// <summary>
        /// Called before a page selector has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created page rule.</returns>
        internal static CssPageRule CreatePageRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssPageRule();
            rule.Selector = parser.InSelector(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return parser.SkipDeclarations<CssPageRule>(token);
            
            parser.FillDeclarations(rule.Style);
            return rule;
        }

        /// <summary>
        /// Called before the body of the font-face rule.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created font-face rule.</returns>
        internal static CssFontFaceRule CreateFontFaceRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssFontFaceRule();

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return parser.SkipDeclarations<CssFontFaceRule>(token);
            
            parser.FillDeclarations(rule.Style);
            return rule;
        }

        /// <summary>
        /// Called before a supports condition has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created supports rule.</returns>
        internal static CssSupportsRule CreateSupportsRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssSupportsRule();
            rule.Condition = parser.InCondition(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return parser.SkipDeclarations<CssSupportsRule>(token);

            parser.FillRules(rule);
            return rule;
        }

        /// <summary>
        /// Called before a document function has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated document rule.</returns>
        internal static CssDocumentRule CreateDocumentRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssDocumentRule();
            rule.Conditions.AddRange(parser.InDocumentFunctions(ref token));

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return parser.SkipDeclarations<CssDocumentRule>(token);

            parser.FillRules(rule);
            return rule;
        }

        /// <summary>
        /// Called before a keyframes identifier has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated keyframes rule.</returns>
        internal static CssKeyframesRule CreateKeyframesRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssKeyframesRule();
            rule.Name = parser.InRuleName(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return parser.SkipDeclarations<CssKeyframesRule>(token);

            parser.FillKeyframeRules(rule);
            return rule;
        }

        /// <summary>
        /// Called before a prefix has been found for the namespace rule.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated namespace rule.</returns>
        internal static CssNamespaceRule CreateNamespaceRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssNamespaceRule();
            rule.Prefix = parser.InRuleName(ref token);

            if (token.Type == CssTokenType.Url)
                rule.NamespaceUri = token.Data;

            parser._tokenizer.JumpToNextSemicolon();
            return rule;
        }

        /// <summary>
        /// Before a charset string has been found.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The generated rule.</returns>
        internal static CssCharsetRule CreateCharsetRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssCharsetRule();

            if (token.Type == CssTokenType.String)
                rule.CharacterSet = token.Data;

            parser._tokenizer.JumpToNextSemicolon();
            return rule;
        }

        /// <summary>
        /// Before an URL has been found for the import rule.
        /// </summary>
        /// <param name="parser">The parser to create the rule.</param>
        /// <param name="tokens">The stream of tokens.</param>
        /// <returns>The created rule.</returns>
        internal static CssImportRule CreateImportRule(CssParser parser)
        {
            var token = parser._tokenizer.Get();
            var rule = new CssImportRule();

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = parser._tokenizer.Get();
                rule.Media = parser.InMediaList(ref token);
            }

            parser._tokenizer.JumpToNextSemicolon();
            return rule;
        }

        #endregion

        #region States

        /// <summary>
        /// Creates a rule with the enumeration of tokens.
        /// </summary>
        CssRule CreateRule(CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                {
                    return this.CreateAtRule(token.Data);
                }
                case CssTokenType.CurlyBracketOpen:
                {
                    RaiseErrorOccurred(CssParseError.InvalidBlockStart, token);
                    _tokenizer.SkipUnknownRule();
                    return null;
                }
                case CssTokenType.String:
                case CssTokenType.Url:
                case CssTokenType.CurlyBracketClose:
                case CssTokenType.RoundBracketClose:
                case CssTokenType.SquareBracketClose:
                {
                    RaiseErrorOccurred(CssParseError.InvalidToken, token);
                    _tokenizer.SkipUnknownRule();
                    return null;
                }
                default:
                {
                    return CreateStyleRule(token);
                }
            }
        }

        #endregion

        #region Style

        /// <summary>
        /// Creates a new style rule.
        /// </summary>
        CssStyleRule CreateStyleRule(CssToken token)
        {
            var rule = new CssStyleRule();
            rule.Selector = InSelector(ref token);
            FillDeclarations(rule.Style);
            return rule.Selector != null ? rule : null;
        }

        /// <summary>
        /// State that is called once we are in a CSS selector.
        /// </summary>
        ISelector InSelector(ref CssToken token)
        {
            _tokenizer.State = CssParseMode.Selector;
            _selector.Reset();
            var start = token;

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketOpen, CssTokenType.CurlyBracketClose))
            {
                _selector.Apply(token);
                token = _tokenizer.Get();
            }

            if (_selector.IsValid == false)
                RaiseErrorOccurred(CssParseError.InvalidSelector, start);

            _tokenizer.State = CssParseMode.Data;
            return _selector.Result;
        }

        /// <summary>
        /// Called before the property name has been detected.
        /// </summary>
        CssProperty InDeclaration(CssStyleDeclaration style, ref CssToken token)
        {
            if (token.Type == CssTokenType.Ident)
            {
                var property = default(CssProperty);
                var propertyName = token.Data;
                token = _tokenizer.Get();

                if (token.Type != CssTokenType.Colon)
                {
                    RaiseErrorOccurred(CssParseError.ColonMissing, token);
                }
                else
                {
                    property = style.CreateProperty(propertyName);

                    if (property == null)
                    {
                        RaiseErrorOccurred(CssParseError.UnknownDeclarationName, token);
                        property = new CssUnknownProperty(propertyName, style);
                    }

                    var val = InValue(ref token);

                    if (val == null)
                        RaiseErrorOccurred(CssParseError.ValueMissing, token);
                    else if (property.TrySetValue(val))
                        style.SetProperty(property);

                    property.IsImportant = _value.IsImportant;
                }

                _tokenizer.JumpToEndOfDeclaration();
                token = _tokenizer.Get();
                return property;
            }
            else
            {
                RaiseErrorOccurred(CssParseError.IdentExpected, token);
            }

            return null;
        }

        #endregion

        #region Document Functions

        /// <summary>
        /// Called when the document functions have to been found.
        /// </summary>
        List<CssDocumentRule.IFunction> InDocumentFunctions(ref CssToken token)
        {
            var list = new List<CssDocumentRule.IFunction>();

            do
            {
                var function = InDocumentFunction(token);

                if (function == null)
                    break;
                
                list.Add(function);
                token = _tokenizer.Get();
            }
            while (token.Type == CssTokenType.Comma);

            return list;
        }

        /// <summary>
        /// Called before a document function has been found.
        /// </summary>
        CssDocumentRule.IFunction InDocumentFunction(CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.Url:
                    return new CssDocumentRule.UrlFunction(token.Data);

                case CssTokenType.UrlPrefix:
                    return new CssDocumentRule.UrlPrefixFunction(token.Data);

                case CssTokenType.Domain:
                    return new CssDocumentRule.DomainFunction(token.Data);

                case CssTokenType.Function:
                    if (String.Compare(token.Data, FunctionNames.Regexp, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        var str = ((CssFunctionToken)token).ToCssString();

                        if (str != null)
                            return new CssDocumentRule.RegexpFunction(str);
                    }
                    break;
            }

            return null;
        }

        #endregion

        #region Keyframes

        /// <summary>
        /// Before the curly bracket of an @keyframes rule has been seen.
        /// </summary>
        CssKeyframeRule CreateKeyframeRule(CssToken token)
        {
            var rule = new CssKeyframeRule();
            rule.Key = InKeyframeSelector(ref token);

            if (rule.Key == null)
            {
                _tokenizer.JumpToEndOfDeclaration();
                return null;
            }

            FillDeclarations(rule.Style);
            return rule;
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        KeyframeSelector InKeyframeSelector(ref CssToken token)
        {
            var keys = new List<Percent>();

            while (token.Type != CssTokenType.Eof)
            {
                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;
                    else if (token.Type != CssTokenType.Comma)
                        return null;

                    token = _tokenizer.Get();
                }

                if (token.Type == CssTokenType.Percentage)
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.From))
                    keys.Add(Percent.Zero);
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.To))
                    keys.Add(Percent.Hundred);
                else
                    return null;

                token = _tokenizer.Get();
            }

            return new KeyframeSelector(keys);
        }

        #endregion

        #region Media List

        /// <summary>
        /// Before any medium has been found for the @media or @import rule.
        /// </summary>
        MediaList InMediaList(ref CssToken token)
        {
            var list = new MediaList();

            while (token.Type != CssTokenType.Eof)
            {
                var medium = InMediaValue(ref token);

                if (medium == null)
                    break;

                list.Add(medium);

                if (token.Type != CssTokenType.Comma)
                    break;

                token = _tokenizer.Get();
            }

            if (token.Type != CssTokenType.CurlyBracketOpen)
            {
                if (token.Type == CssTokenType.RoundBracketClose)
                    token = _tokenizer.Get();

                if (token.Type == CssTokenType.CurlyBracketOpen)
                    token = _tokenizer.Get();

                _tokenizer.JumpToEndOfDeclaration();
                token = _tokenizer.Get();
            }
            else if (list.Length == 0)
            {
                _tokenizer.JumpToEndOfDeclaration();
                token = _tokenizer.Get();
            }

            return list;
        }

        /// <summary>
        /// Scans the current medium for the @media or @import rule.
        /// </summary>
        CssMedium InMediaValue(ref CssToken token)
        {
            var medium = new CssMedium();

            if (token.Type == CssTokenType.Ident)
            {
                var identifier = token.Data;

                if (identifier.Equals(Keywords.Not, StringComparison.OrdinalIgnoreCase))
                {
                    medium.IsInverse = true;
                    token = _tokenizer.Get();
                }
                else if (identifier.Equals(Keywords.Only, StringComparison.OrdinalIgnoreCase))
                {
                    medium.IsExclusive = true;
                    token = _tokenizer.Get();
                }
            }

            if (token.Type == CssTokenType.Ident)
            {
                medium.Type = token.Data;
                token = _tokenizer.Get();

                if (token.Type != CssTokenType.Ident || String.Compare(token.Data, Keywords.And, StringComparison.OrdinalIgnoreCase) != 0)
                    return medium;

                token = _tokenizer.Get();
            }

            do
            {
                if (token.Type != CssTokenType.RoundBracketOpen)
                    return null;

                token = _tokenizer.Get();

                if (TrySetConstraint(medium, ref token) == false || token.Type != CssTokenType.RoundBracketClose)
                    return null;

                token = _tokenizer.Get();

                if (token.Type != CssTokenType.Ident || String.Compare(token.Data, Keywords.And, StringComparison.OrdinalIgnoreCase) != 0)
                    break;

                token = _tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);

            return medium;
        }

        Boolean TrySetConstraint(CssMedium medium, ref CssToken token)
        {
            if (token.Type != CssTokenType.Ident)
            {
                _tokenizer.JumpToClosedArguments();
                token = _tokenizer.Get();
                return false;
            }

            _value.Reset();
            var feature = token.Data;
            token = _tokenizer.Get();

            if (token.Type == CssTokenType.Colon)
            {
                _tokenizer.State = CssParseMode.Value;
                token = _tokenizer.Get();

                while (token.Type != CssTokenType.RoundBracketClose || _value.IsReady == false)
                {
                    if (token.Type == CssTokenType.Eof)
                        return false;

                    _value.Apply(token);
                    token = _tokenizer.Get();
                }

                _tokenizer.State = CssParseMode.Data;
                medium.AddConstraint(feature, _value.Result);
            }

            return true;
        }

        #endregion

        #region Value

        /// <summary>
        /// Before the name of a rule has been detected.
        /// </summary>
        String InRuleName(ref CssToken token)
        {
            var name = String.Empty;

            if (token.Type == CssTokenType.Ident)
            {
                name = token.Data;
                token = _tokenizer.Get();
            }

            return name;
        }

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        CssValue InValue(ref CssToken token)
        {
            _tokenizer.State = CssParseMode.Value;
            _value.Reset();
            token = _tokenizer.Get();

            while (token.Type != CssTokenType.Eof)
            {
                if (token.Is(CssTokenType.Semicolon, CssTokenType.CurlyBracketClose) ||
                   (token.Type == CssTokenType.RoundBracketClose && _value.IsReady))
                    break;

                _value.Apply(token);
                token = _tokenizer.Get();
            }

            _tokenizer.State = CssParseMode.Data;
            return _value.Result;
        }

        #endregion

        #region Condition

        /// <summary>
        /// Called before any token in the value regime had been seen.
        /// </summary>
        CssSupportsRule.ICondition InCondition(ref CssToken token)
        {
            var condition = ExtractCondition(ref token);

            if (condition != null)
            {
                if (token.Data.Equals(Keywords.And, StringComparison.OrdinalIgnoreCase))
                {
                    token = _tokenizer.Get();
                    var conditions = Conditions(condition, Keywords.And, ref token);
                    return new CssSupportsRule.AndCondition(conditions);
                }
                else if (token.Data.Equals(Keywords.Or, StringComparison.OrdinalIgnoreCase))
                {
                    token = _tokenizer.Get();
                    var conditions = Conditions(condition, Keywords.Or, ref token);
                    return new CssSupportsRule.OrCondition(conditions);
                }
            }

            return condition;
        }

        CssSupportsRule.ICondition ExtractCondition(ref CssToken token)
        {
            var condition = default(CssSupportsRule.ICondition);

            if (token.Type == CssTokenType.RoundBracketOpen)
            {
                token = _tokenizer.Get();
                condition = InCondition(ref token);

                if (condition != null)
                    condition = new CssSupportsRule.GroupCondition(condition);
                else if (token.Type == CssTokenType.Ident)
                    condition = DeclCondition(ref token);

                if (token.Type == CssTokenType.RoundBracketClose)
                    token = _tokenizer.Get();
            }
            else if (token.Data.Equals(Keywords.Not, StringComparison.OrdinalIgnoreCase))
            {
                token = _tokenizer.Get();
                condition = ExtractCondition(ref token);

                if (condition != null)
                    condition = new CssSupportsRule.NotCondition(condition);
            }

            return condition;
        }

        CssSupportsRule.ICondition DeclCondition(ref CssToken token)
        {
            var name = token.Data;
            var style = new CssStyleDeclaration();
            var property = Factory.Properties.Create(name, style);

            if (property == null)
                property = new CssUnknownProperty(name, style);

            token = _tokenizer.Get();

            if (token.Type == CssTokenType.Colon)
            {
                var result = InValue(ref token);
                property.IsImportant = _value.IsImportant;

                if (result != null)
                    return new CssSupportsRule.DeclarationCondition(property, result);
            }

            return null;
        }

        List<CssSupportsRule.ICondition> Conditions(CssSupportsRule.ICondition condition, String connector, ref CssToken token)
        {
            var list = new List<CssSupportsRule.ICondition>();
            list.Add(condition);

            while (token.Type != CssTokenType.Eof)
            {
                condition = ExtractCondition(ref token);

                if (condition == null)
                    break;

                list.Add(condition);

                if (!token.Data.Equals(connector, StringComparison.OrdinalIgnoreCase))
                    break;

                token = _tokenizer.Get();
            }

            return list;
        }

        #endregion

        #region Helpers

        T SkipDeclarations<T>(CssToken token)
        {
            RaiseErrorOccurred(CssParseError.UnknownAtRule, token);
            _tokenizer.SkipUnknownRule();
            return default(T);
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        void Kernel()
        {
            var token = _tokenizer.Get();

            do
            {
                Consume(token);
                token = _tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        async Task<ICssStyleSheet> KernelAsync(CancellationToken cancelToken)
        {
            var source = _sheet.Source;
            await source.Prefetch(64000, cancelToken).ConfigureAwait(false);
            var token = _tokenizer.Get();

            do
            {
                Consume(token);
                token = _tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);

            return _sheet;
        }

        /// <summary>
        /// Consumes the token by appending a created rule to the sheet.
        /// </summary>
        void Consume(CssToken token)
        {
            var rule = CreateRule(token);

            if (rule != null)
                _sheet.Rules.Add(rule, _sheet, null);
        }

        /// <summary>
        /// Consumes the token by appending a created rule to the sheet.
        /// </summary>
        void Consume(CssToken token, CssGroupingRule parent)
        {
            var rule = CreateRule(token);

            if (rule != null)
                parent.Rules.Add(rule, _sheet, parent);
        }

        /// <summary>
        /// Fills the given parent rule with rules given by the tokens.
        /// </summary>
        void FillRules(CssGroupingRule rule)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                Consume(token, rule);
                token = _tokenizer.Get();
            }
        }

        /// <summary>
        /// Fills the given keyframe rule with rules given by the tokens.
        /// </summary>
        void FillKeyframeRules(CssKeyframesRule parentRule)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateKeyframeRule(token);

                if (rule != null)
                    parentRule.Rules.Add(rule, _sheet, parentRule);

                token = _tokenizer.Get();
            }
        }

        /// <summary>
        /// Fills the given parent style with declarations given by the tokens.
        /// </summary>
        void FillDeclarations(CssStyleDeclaration style)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                InDeclaration(style, ref token);

                if (token.Type == CssTokenType.Semicolon)
                    token = _tokenizer.Get();
            }
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="selectorText">The string to parse.</param>
        /// <returns>The Selector object.</returns>
        public static ISelector ParseSelector(String selectorText)
        {
            var source = new TextSource(selectorText);
            var tokenizer = new CssTokenizer(source, null);
            tokenizer.State = CssParseMode.Selector;
            var creator = Pool.NewSelectorConstructor();
            var token = tokenizer.Get();

            while (token.Type != CssTokenType.Eof)
            {
                creator.Apply(token);
                token = tokenizer.Get();
            }

            return creator.ToPool();
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="keyText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The Selector object.</returns>
        public static IKeyframeSelector ParseKeyframeSelector(String keyText, IConfiguration configuration = null)
        {
            var parser = new CssParser(keyText, configuration ?? Configuration.Default);
            var token = parser._tokenizer.Get();
            var selector = parser.InKeyframeSelector(ref token);
            return token.Type == CssTokenType.Eof ? selector : null;
        }

        #endregion

        #region Internal static methods

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        /// <param name="valueText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The CSSValue object.</returns>
        internal static CssValue ParseValue(String valueText, IConfiguration configuration = null)
        {
            var parser = new CssParser(valueText, configuration ?? Configuration.Default);
            var token = default(CssToken);
            var value = parser.InValue(ref token);
            return token.Type == CssTokenType.Eof ? value : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        /// <param name="ruleText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The CSSRule object.</returns>
        internal static CssRule ParseRule(String ruleText, IConfiguration configuration = null)
        {
            var parser = new CssParser(ruleText, configuration ?? Configuration.Default);
            var rule = parser.CreateRule(parser._tokenizer.Get());
            var token = parser._tokenizer.Get();
            return token.Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and transforms it into CSS declarations.
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The CSSStyleDeclaration object.</returns>
        internal static CssStyleDeclaration ParseDeclarations(String declarations, IConfiguration configuration = null)
        {
            var style = new CssStyleDeclaration();
            AppendDeclarations(style, declarations, configuration);
            return style;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (CSS
        /// property).
        /// </summary>
        /// <param name="declarationText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The CSSProperty object.</returns>
        internal static CssProperty ParseDeclaration(String declarationText, IConfiguration configuration = null)
        {
            var parser = new CssParser(declarationText, configuration ?? Configuration.Default);
            var style = new CssStyleDeclaration();
            var token = parser._tokenizer.Get();
            var declaration = parser.InDeclaration(style, ref token);

            if (token.Type == CssTokenType.Semicolon)
                token = parser._tokenizer.Get();

            return token.Type == CssTokenType.Eof ? declaration : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a stream of CSS media.
        /// </summary>
        /// <param name="mediaText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The stream of media.</returns>
        internal static List<CssMedium> ParseMediaList(String mediaText, IConfiguration configuration = null)
        {
            var parser = new CssParser(mediaText, configuration);
            var list = new List<CssMedium>();
            var token = parser._tokenizer.Get();

            while (token.Type != CssTokenType.Eof)
            {
                var medium = parser.InMediaValue(ref token);

                if (medium == null || token.IsNot(CssTokenType.Comma, CssTokenType.Eof))
                    throw new DomException(DomError.Syntax);

                list.Add(medium);
                token = parser._tokenizer.Get();
            }

            return list;
        }

        /// <summary>
        /// Takes a string and transforms it into supports condition.
        /// </summary>
        /// <param name="conditionText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The parsed condition.</returns>
        internal static CssSupportsRule.ICondition ParseCondition(String conditionText, IConfiguration configuration = null)
        {
            var parser = new CssParser(conditionText, configuration ?? Configuration.Default);
            var token = parser._tokenizer.Get();
            var condition = parser.InCondition(ref token);
            return token.Type == CssTokenType.Eof ? condition : null;
        }

        /// <summary>
        /// Takes a string and transforms it into an enumeration of special
        /// document functions and their arguments.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The iterator over the function-argument tuples.</returns>
        internal static List<CssDocumentRule.IFunction> ParseDocumentRules(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var token = parser._tokenizer.Get();
            var conditions = parser.InDocumentFunctions(ref token);
            return token.Type == CssTokenType.Eof ? conditions : null;
        }

        /// <summary>
        /// Takes a valid media string and parses the medium information.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The CSS medium.</returns>
        internal static CssMedium ParseMedium(String source, IConfiguration configuration = null)
        {
            var parser = new CssParser(source, configuration);
            var token = parser._tokenizer.Get();
            var medium = parser.InMediaValue(ref token);

            if (token.Type != CssTokenType.Eof)
                throw new DomException(DomError.Syntax);

            return medium;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        /// <param name="ruleText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The CSSKeyframeRule object.</returns>
        internal static CssKeyframeRule ParseKeyframeRule(String ruleText, IConfiguration configuration = null)
        {
            var parser = new CssParser(ruleText, configuration);
            var rule = parser.CreateKeyframeRule(parser._tokenizer.Get());
            var token = parser._tokenizer.Get();
            return token.Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and appends all rules to the given list of
        /// properties.
        /// </summary>
        /// <param name="list">The list of css properties to append to.</param>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        internal static void AppendDeclarations(CssStyleDeclaration list, String declarations, IConfiguration configuration = null)
        {
            var parser = new CssParser(declarations, configuration ?? Configuration.Default);
            parser.FillDeclarations(list);
        }

        #endregion

        #region Event-Helpers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        /// <param name="token">The associated token.</param>
        void RaiseErrorOccurred(CssParseError code, CssToken token)
        {
            _tokenizer.RaiseErrorOccurred(code, token.Position);
        }

        #endregion
    }
}
