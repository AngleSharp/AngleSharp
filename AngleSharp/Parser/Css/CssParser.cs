namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css.States;

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

        #region Helpers

        static CssTokenizer CreateTokenizer(String source, IConfiguration configuration)
        {
            var events = configuration != null ? configuration.Events : null;
            return new CssTokenizer(new TextSource(source), events);
        }

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        /// <param name="token">The associated token.</param>
        void RaiseErrorOccurred(CssParseError code, CssToken token)
        {
            _tokenizer.RaiseErrorOccurred(code, token.Position);
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        void Kernel()
        {
            var token = _tokenizer.Get();

            do
            {
                var rule = _tokenizer.CreateRule(token);
                _sheet.AddRule(rule);
                token = _tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        async Task<ICssStyleSheet> KernelAsync(CancellationToken cancelToken)
        {
            await _sheet.Source.PrefetchAll(cancelToken).ConfigureAwait(false);
            Kernel();
            return _sheet;
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
            var tokenizer = CreateTokenizer(keyText, configuration);
            var token = tokenizer.Get();
            var state = new CssKeyframesState(tokenizer);
            var selector = state.CreateKeyframeSelector(ref token);
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
            var tokenizer = CreateTokenizer(valueText, configuration);
            var token = default(CssToken);
            var state = new CssUnknownState(tokenizer);
            var value = state.CreateValue(ref token);
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
            var tokenizer = CreateTokenizer(ruleText, configuration);
            var token = tokenizer.Get();
            var rule = tokenizer.CreateRule(token);
            return tokenizer.Get().Type == CssTokenType.Eof ? rule : null;
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
            var tokenizer = CreateTokenizer(declarationText, configuration);
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer);
            var declaration = state.CreateDeclaration(ref token);
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
            var tokenizer = CreateTokenizer(mediaText, configuration);
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer);
            var list = state.CreateMediaList(ref token);
            return token.Type == CssTokenType.Eof ? list : null;
        }

        /// <summary>
        /// Takes a string and transforms it into supports condition.
        /// </summary>
        /// <param name="conditionText">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        /// <returns>The parsed condition.</returns>
        internal static ICondition ParseCondition(String conditionText, IConfiguration configuration = null)
        {
            var tokenizer = CreateTokenizer(conditionText, configuration);
            var token = tokenizer.Get();
            var state = new CssSupportsState(tokenizer);
            var condition = state.CreateCondition(ref token);
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
        internal static List<IDocumentFunction> ParseDocumentRules(String source, IConfiguration configuration = null)
        {
            var tokenizer = CreateTokenizer(source, configuration);
            var token = tokenizer.Get();
            var state = new CssDocumentState(tokenizer);
            var conditions = state.CreateFunctions(ref token);
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
            var tokenizer = CreateTokenizer(source, configuration);
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer);
            var medium = state.CreateMedium(ref token);
            return token.Type == CssTokenType.Eof ? medium : null;
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
            var tokenizer = CreateTokenizer(ruleText, configuration);
            var token = tokenizer.Get();
            var state = new CssKeyframesState(tokenizer);
            var rule = state.CreateKeyframeRule(token);
            return tokenizer.Get().Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and appends all rules to the given list of
        /// properties.
        /// </summary>
        /// <param name="style">The style declarations.</param>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">
        /// Optional: The configuration to use for construction.
        /// </param>
        internal static void AppendDeclarations(CssStyleDeclaration style, String declarations, IConfiguration configuration = null)
        {
            var tokenizer = CreateTokenizer(declarations, configuration);
            var state = new CssUnknownState(tokenizer);
            state.FillDeclarations(style);
        }

        #endregion
    }
}
