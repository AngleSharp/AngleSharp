namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
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
            var creator = Pool.NewSelectorConstructor();
            var token = tokenizer.Get();

            while (token.Type != CssTokenType.EndOfFile)
            {
                creator.Apply(token);
                token = tokenizer.Get();
            }

            var valid = creator.IsValid;
            var result = creator.ToPool();
            return valid || _options.IsToleratingInvalidSelectors ? result : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a keyframe selector object.
        /// </summary>
        public IKeyframeSelector ParseKeyframeSelector(String keyText)
        {
            return Parse(keyText, (b, t) => Tuple.Create(b.CreateKeyframeSelector(ref t), t));
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
            var builder = new CssBuilder(tokenizer, this);
            builder.CreateRules(sheet);
            sheet.ParseTree = builder.Container;
            return sheet;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        internal CssValue ParseValue(String valueText)
        {
            var tokenizer = CreateTokenizer(valueText, _config);
            var token = default(CssToken);
            var builder = new CssBuilder(tokenizer, this);
            var value = builder.CreateValue(ref token);
            return token.Type == CssTokenType.EndOfFile ? value : null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        internal CssRule ParseRule(String ruleText)
        {
            return Parse(ruleText, (b, t) => b.CreateRule(t));
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (property).
        /// </summary>
        internal CssProperty ParseDeclaration(String declarationText)
        {
            return Parse(declarationText, (b, t) => Tuple.Create(b.CreateDeclaration(ref t), t));
        }

        /// <summary>
        /// Takes a string and transforms it into a stream of CSS media.
        /// </summary>
        internal List<CssMedium> ParseMediaList(String mediaText)
        {
            return Parse(mediaText, (b, t) => Tuple.Create(b.CreateMedia(ref t), t));
        }

        /// <summary>
        /// Takes a string and transforms it into supports condition.
        /// </summary>
        internal IConditionFunction ParseCondition(String conditionText)
        {
            return Parse(conditionText, (b, t) => Tuple.Create(b.CreateCondition(ref t), t));
        }

        /// <summary>
        /// Takes a string and transforms it into an enumeration of special
        /// document functions and their arguments.
        /// </summary>
        internal List<DocumentFunction> ParseDocumentRules(String documentText)
        {
            return Parse(documentText, (b, t) => Tuple.Create(b.CreateFunctions(ref t), t));
        }

        /// <summary>
        /// Takes a valid media string and parses the medium information.
        /// </summary>
        internal CssMedium ParseMedium(String mediumText)
        {
            return Parse(mediumText, (b, t) => Tuple.Create(b.CreateMedium(ref t), t));
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        internal CssKeyframeRule ParseKeyframeRule(String ruleText)
        {
            return Parse(ruleText, (b, t) => b.CreateKeyframeRule(t));
        }

        /// <summary>
        /// Takes a string and appends all rules to the given list of
        /// properties.
        /// </summary>
        internal void AppendDeclarations(CssStyleDeclaration style, String declarations)
        {
            var tokenizer = CreateTokenizer(declarations, _config);
            var builder = new CssBuilder(tokenizer, this);
            builder.FillDeclarations(style);
        }

        #endregion

        #region Helpers

        T Parse<T>(String source, Func<CssBuilder, CssToken, T> create)
        {
            var tokenizer = CreateTokenizer(source, _config);
            var token = tokenizer.Get();
            var builder = new CssBuilder(tokenizer, this);
            var rule = create(builder, token);
            return tokenizer.Get().Type == CssTokenType.EndOfFile ? rule : default(T);
        }

        T Parse<T>(String source, Func<CssBuilder, CssToken, Tuple<T, CssToken>> create)
        {
            var tokenizer = CreateTokenizer(source, _config);
            var token = tokenizer.Get();
            var builder = new CssBuilder(tokenizer, this);
            var pair = create(builder, token);
            return pair.Item2.Type == CssTokenType.EndOfFile ? pair.Item1 : default(T);
        }

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
