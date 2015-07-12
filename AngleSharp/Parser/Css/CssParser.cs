namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css.States;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates an instance of the CSS parser front-end.
    /// </summary>
    public class CssParser
    {
        #region Fields

        readonly CssParserOptions _options;
        readonly IConfiguration _config;

        internal static readonly CssParser Default = new CssParser();

        #endregion

        #region ctor
        
        /// <summary>
        /// Creates a new parser with the default options and configuration.
        /// </summary>
        public CssParser()
            : this(Configuration.Default)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public CssParser(CssParserOptions options)
            : this(options, Configuration.Default)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        public CssParser(IConfiguration configuration)
            : this(default(CssParserOptions), configuration)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and configuration.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="configuration">The configuration to use.</param>
        public CssParser(CssParserOptions options, IConfiguration configuration)
        {
            _options = options;
            _config = configuration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        public CssParserOptions Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Gets the specified configuration.
        /// </summary>
        public IConfiguration Config
        {
            get { return _config; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Takes a string and transforms it to a stylesheet.
        /// </summary>
        public ICssStyleSheet ParseStylesheet(String content)
        {
            var source = new TextSource(content);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a stream and transforms it to a stylesheet.
        /// </summary>
        public ICssStyleSheet ParseStylesheet(Stream content)
        {
            var source = new TextSource(content);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a string and transforms it to a stylesheet.
        /// </summary>
        public Task<ICssStyleSheet> ParseStylesheetAsync(String content)
        {
            return ParseStylesheetAsync(content, CancellationToken.None);
        }

        /// <summary>
        /// Takes a string and transforms it to a stylesheet.
        /// </summary>
        public async Task<ICssStyleSheet> ParseStylesheetAsync(String content, CancellationToken cancelToken)
        {
            var source = new TextSource(content);
            await source.PrefetchAll(cancelToken).ConfigureAwait(false);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a stream and transforms it to a stylesheet.
        /// </summary>
        public Task<ICssStyleSheet> ParseStylesheetAsync(Stream content)
        {
            return ParseStylesheetAsync(content, CancellationToken.None);
        }

        /// <summary>
        /// Takes a stream and transforms it to a stylesheet.
        /// </summary>
        public async Task<ICssStyleSheet> ParseStylesheetAsync(Stream content, CancellationToken cancelToken)
        {
            var source = new TextSource(content);
            await source.PrefetchAll(cancelToken).ConfigureAwait(false);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public ISelector ParseSelector(String selectorText)
        {
            var tokenizer = CreateTokenizer(selectorText, _config);
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
        public IKeyframeSelector ParseKeyframeSelector(String keyText)
        {
            var tokenizer = CreateTokenizer(keyText, _config);
            var token = tokenizer.Get();
            var state = new CssKeyframesState(tokenizer, this);
            var selector = state.CreateKeyframeSelector(ref token);
            return token.Type == CssTokenType.Eof ? selector : null;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Takes a text source and transforms it into a CSS sheet.
        /// </summary>
        internal ICssStyleSheet ParseStylesheet(TextSource source)
        {
            var sheet = new CssStyleSheet(this);
            return ParseStylesheet(sheet, source);
        }

        /// <summary>
        /// Takes a text source and populate the provided CSS sheet.
        /// </summary>
        internal ICssStyleSheet ParseStylesheet(CssStyleSheet sheet, TextSource source)
        {
            var tokenizer = CreateTokenizer(source, _config);
            var token = tokenizer.Get();

            do
            {
                var rule = tokenizer.CreateRule(token, this);
                sheet.AddRule(rule);
                token = tokenizer.Get();
            }
            while (token.Type != CssTokenType.Eof);

            return sheet;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        internal CssValue ParseValue(String valueText)
        {
            var tokenizer = CreateTokenizer(valueText, _config);
            var token = default(CssToken);
            var state = new CssUnknownState(tokenizer, this);
            var value = state.CreateValue(ref token);
            return token.Type == CssTokenType.Eof ? value : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        internal CssRule ParseRule(String ruleText)
        {
            var tokenizer = CreateTokenizer(ruleText, _config);
            var token = tokenizer.Get();
            var rule = tokenizer.CreateRule(token, this);
            return tokenizer.Get().Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (property).
        /// </summary>
        internal CssProperty ParseDeclaration(String declarationText)
        {
            var tokenizer = CreateTokenizer(declarationText, _config);
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer, this);
            var declaration = state.CreateDeclaration(ref token);
            return token.Type == CssTokenType.Eof ? declaration : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a stream of CSS media.
        /// </summary>
        internal List<CssMedium> ParseMediaList(String mediaText)
        {
            var tokenizer = CreateTokenizer(mediaText, _config);
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer, this);
            var list = state.CreateMedia(ref token);
            return token.Type == CssTokenType.Eof ? list : null;
        }

        /// <summary>
        /// Takes a string and transforms it into supports condition.
        /// </summary>
        internal ICondition ParseCondition(String conditionText)
        {
            var tokenizer = CreateTokenizer(conditionText, _config);
            var token = tokenizer.Get();
            var state = new CssSupportsState(tokenizer, this);
            var condition = state.CreateCondition(ref token);
            return token.Type == CssTokenType.Eof ? condition : null;
        }

        /// <summary>
        /// Takes a string and transforms it into an enumeration of special
        /// document functions and their arguments.
        /// </summary>
        internal List<IDocumentFunction> ParseDocumentRules(String source)
        {
            var tokenizer = CreateTokenizer(source, _config);
            var token = tokenizer.Get();
            var state = new CssDocumentState(tokenizer, this);
            var conditions = state.CreateFunctions(ref token);
            return token.Type == CssTokenType.Eof ? conditions : null;
        }

        /// <summary>
        /// Takes a valid media string and parses the medium information.
        /// </summary>
        internal CssMedium ParseMedium(String source)
        {
            var tokenizer = CreateTokenizer(source, _config);
            var token = tokenizer.Get();
            var state = new CssUnknownState(tokenizer, this);
            var medium = state.CreateMedium(ref token);
            return token.Type == CssTokenType.Eof ? medium : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        internal CssKeyframeRule ParseKeyframeRule(String ruleText)
        {
            var tokenizer = CreateTokenizer(ruleText, _config);
            var token = tokenizer.Get();
            var state = new CssKeyframesState(tokenizer, this);
            var rule = state.CreateKeyframeRule(token);
            return tokenizer.Get().Type == CssTokenType.Eof ? rule : null;
        }

        /// <summary>
        /// Takes a string and appends all rules to the given list of
        /// properties.
        /// </summary>
        internal void AppendDeclarations(CssStyleDeclaration style, String declarations)
        {
            var tokenizer = CreateTokenizer(declarations, _config);
            var state = new CssUnknownState(tokenizer, this);
            state.FillDeclarations(style);
        }

        #endregion

        #region Helpers

        static CssTokenizer CreateTokenizer(String sourceCode, IConfiguration configuration)
        {
            var source = new TextSource(sourceCode);
            return CreateTokenizer(source, configuration);
        }

        static CssTokenizer CreateTokenizer(Stream sourceCode, IConfiguration configuration)
        {
            var source = new TextSource(sourceCode);
            return CreateTokenizer(source, configuration);
        }

        static CssTokenizer CreateTokenizer(TextSource source, IConfiguration configuration)
        {
            var events = configuration != null ? configuration.Events : null;
            return new CssTokenizer(source, events);
        }

        #endregion
    }
}
