namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css.States;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
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
        /// <param name="configuration">The configuration to use.</param>
        public CssParser(String source, IConfiguration configuration = null)
            : this(source, default(CssParserOptions), configuration)
        { }

        /// <summary>
        /// Creates a new CSS parser instance with an new stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">The configuration to use.</param>
        public CssParser(Stream stream, IConfiguration configuration = null)
            : this(stream, default(CssParserOptions), configuration)
        { }

        /// <summary>
        /// Creates a new CSS parser instance with a new stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="options">The options for the parser.</param>
        /// <param name="configuration">The configuration to use.</param>
        public CssParser(String source, CssParserOptions options, IConfiguration configuration = null)
            : this(new CssStyleSheet(options, configuration, new TextSource(source)))
        { }

        /// <summary>
        /// Creates a new CSS parser instance with an new stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="options">The options for the parser.</param>
        /// <param name="configuration">The configuration to use.</param>
        public CssParser(Stream stream, CssParserOptions options, IConfiguration configuration = null)
            : this(new CssStyleSheet(options, configuration, new TextSource(stream, configuration.DefaultEncoding())))
        { }

        /// <summary>
        /// Creates a new CSS parser instance parser with the specified
        /// stylesheet based on the given source manager.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        internal CssParser(CssStyleSheet stylesheet)
        {
            var owner = stylesheet.OwnerNode as Element;
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
            return ParseAsync(default(CssParserOptions));
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <param name="options">
        /// The options to set the desired behavior during parsing.
        /// </param>
        /// <returns>
        /// The task which could be awaited or continued differently.
        /// </returns>
        public Task<ICssStyleSheet> ParseAsync(CssParserOptions options)
        {
            return ParseAsync(options, CancellationToken.None);
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// </summary>
        /// <param name="options">
        /// The options to set the desired behavior during parsing.
        /// </param>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>
        /// The task which could be awaited or continued differently.
        /// </returns>
        public Task<ICssStyleSheet> ParseAsync(CssParserOptions options, CancellationToken cancelToken)
        {
            lock (_syncGuard)
            {
                if (!_started)
                {
                    _started = true;
                    _task = KernelAsync(options, cancelToken);
                }
            }

            return _task;
        }

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        /// <returns>The new stylesheet.</returns>
        public ICssStyleSheet Parse()
        {
            return Parse(default(CssParserOptions));
        }

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        /// <param name="options">
        /// The options to set the desired behavior during parsing.
        /// </param>
        /// <returns>The new stylesheet.</returns>
        public ICssStyleSheet Parse(CssParserOptions options)
        {
            if (!_started)
            {
                _started = true;
                Kernel(options);
            }

            return _sheet;
        }

        #endregion

        #region Helpers

        static CssTokenizer CreateTokenizer(String sourceCode, IConfiguration configuration)
        {
            var events = configuration != null ? configuration.Events : null;
            var source = new TextSource(sourceCode);
            return new CssTokenizer(source, events);
        }

        void Kernel(CssParserOptions options)
        {
            var token = _tokenizer.Get();

            do
            {
                var rule = _tokenizer.CreateRule(token, options);
                _sheet.AddRule(rule);
                token = _tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);
        }

        async Task<ICssStyleSheet> KernelAsync(CssParserOptions options, CancellationToken cancelToken)
        {
            await _sheet.Source.PrefetchAll(cancelToken).ConfigureAwait(false);
            Kernel(options);
            return _sheet;
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public static ISelector ParseSelector(String selectorText)
        {
            var tokenizer = CreateTokenizer(selectorText, default(IConfiguration));
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
        /// Takes a string and transforms it into a keyframe selector object.
        /// </summary>
        public static IKeyframeSelector ParseKeyframeSelector(String keyText)
        {
            var tokenizer = CreateTokenizer(keyText, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssKeyframesState(tokenizer, default(CssParserOptions));
            var selector = state.CreateKeyframeSelector(ref token);
            return token.Type == CssTokenType.Eof ? selector : null;
        }

        #endregion

        #region Internal static methods

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        internal static CssValue ParseValue(String valueText)
        {
            return ParseValue(valueText, default(CssParserOptions));
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        internal static CssValue ParseValue(String valueText, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(valueText, default(IConfiguration));
            var token = default(CssToken);
            var state = new CssUnknownState(tokenizer, options);
            var value = state.CreateValue(ref token);
            return token.Type == CssTokenType.Eof ? value : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        internal static CssRule ParseRule(String ruleText)
        {
            return ParseRule(ruleText, default(CssParserOptions));
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        internal static CssRule ParseRule(String ruleText, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(ruleText, default(IConfiguration));
            var token = tokenizer.Get();
            var rule = tokenizer.CreateRule(token, options);
            return tokenizer.Get().Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (property).
        /// </summary>
        internal static CssProperty ParseDeclaration(String declarationText)
        {
            return ParseDeclaration(declarationText, default(CssParserOptions));
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (property).
        /// </summary>
        internal static CssProperty ParseDeclaration(String declarationText, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(declarationText, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer, options);
            var declaration = state.CreateDeclaration(ref token);
            return token.Type == CssTokenType.Eof ? declaration : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a stream of CSS media.
        /// </summary>
        internal static List<CssMedium> ParseMediaList(String mediaText, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(mediaText, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer, options);
            var list = state.CreateMedia(ref token);
            return token.Type == CssTokenType.Eof ? list : null;
        }

        /// <summary>
        /// Takes a string and transforms it into supports condition.
        /// </summary>
        internal static ICondition ParseCondition(String conditionText, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(conditionText, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssSupportsState(tokenizer, options);
            var condition = state.CreateCondition(ref token);
            return token.Type == CssTokenType.Eof ? condition : null;
        }

        /// <summary>
        /// Takes a string and transforms it into an enumeration of special
        /// document functions and their arguments.
        /// </summary>
        internal static List<IDocumentFunction> ParseDocumentRules(String source, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(source, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssDocumentState(tokenizer, options);
            var conditions = state.CreateFunctions(ref token);
            return token.Type == CssTokenType.Eof ? conditions : null;
        }

        /// <summary>
        /// Takes a valid media string and parses the medium information.
        /// </summary>
        internal static CssMedium ParseMedium(String source, CssParserOptions options)
        {
            var tokenizer = CreateTokenizer(source, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer, options);
            var medium = state.CreateMedium(ref token);
            return token.Type == CssTokenType.Eof ? medium : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        internal static CssKeyframeRule ParseKeyframeRule(String ruleText)
        {
            var tokenizer = CreateTokenizer(ruleText, default(IConfiguration));
            var token = tokenizer.Get();
            var state = new CssKeyframesState(tokenizer, default(CssParserOptions));
            var rule = state.CreateKeyframeRule(token);
            return tokenizer.Get().Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and appends all rules to the given list of
        /// properties.
        /// </summary>
        internal static void AppendDeclarations(CssStyleDeclaration style, String declarations)
        {
            var tokenizer = CreateTokenizer(declarations, default(IConfiguration));
            var state = new CssUnknownState(tokenizer, default(CssParserOptions));
            state.FillDeclarations(style);
        }

        #endregion
    }
}
