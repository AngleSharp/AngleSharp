namespace AngleSharp.Parser.Css
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Services;
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
        readonly IConfiguration _configuration;

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
            _configuration = configuration ?? Configuration.Default;
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
            get { return _configuration; }
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
            await source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
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
            await source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public ISelector ParseSelector(String selectorText)
        {
            var tokenizer = CreateTokenizer(selectorText);
            var token = tokenizer.Get();
            var creator = GetSelectorCreator();

            while (token.Type != CssTokenType.EndOfFile)
            {
                creator.Apply(token);
                token = tokenizer.Get();
            }
            
            // tokenizer should be disposed
            tokenizer.Dispose();

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

        internal CssSelectorConstructor GetSelectorCreator()
        {
            var attributeSelector = _configuration.GetFactory<IAttributeSelectorFactory>();
            var pseudoClassSelector = _configuration.GetFactory<IPseudoClassSelectorFactory>();
            var pseudoElementSelector = _configuration.GetFactory<IPseudoElementSelectorFactory>();
            return Pool.NewSelectorConstructor(attributeSelector, pseudoClassSelector, pseudoElementSelector);
        }

        internal ICssStyleSheet ParseStylesheet(TextSource source)
        {
            var sheet = new CssStyleSheet(this);
            var tokenizer = new CssTokenizer(source);
            var start = tokenizer.GetCurrentPosition();
            var builder = new CssBuilder(tokenizer, this);
            var end = builder.CreateRules(sheet);
            var range = new TextRange(start, end);
            sheet.SourceCode = new TextView(range, source);
            return sheet;
        }

        internal async Task<CssStyleSheet> ParseStylesheetAsync(CssStyleSheet sheet, TextSource source)
        {
            await source.PrefetchAllAsync(CancellationToken.None).ConfigureAwait(false);
            var tokenizer = new CssTokenizer(source);
            var start = tokenizer.GetCurrentPosition();
            var builder = new CssBuilder(tokenizer, this);
            var document = sheet.GetDocument() as Document;
            var tasks = new List<Task>();
            var end = builder.CreateRules(sheet);
            var range = new TextRange(start, end);
            sheet.SourceCode = new TextView(range, source);
            
            foreach (var rule in sheet.Rules)
            {
                if (rule.Type == CssRuleType.Charset)
                {
                    continue;
                }
                else if (rule.Type != CssRuleType.Import)
                {
                    break;
                }
                else
                {
                    var import = (CssImportRule)rule;
                    tasks.Add(import.LoadStylesheetFromAsync(document));
                }
            }

            await TaskEx.WhenAll(tasks).ConfigureAwait(false);
            return sheet;
        }

        internal CssValue ParseValue(String valueText)
        {
            var tokenizer = CreateTokenizer(valueText);
            var token = default(CssToken);
            var builder = new CssBuilder(tokenizer, this);
            var value = builder.CreateValue(ref token);
            return token.Type == CssTokenType.EndOfFile ? value : null;
        }

        internal CssRule ParseRule(String ruleText)
        {
            return Parse(ruleText, (b, t) => b.CreateRule(t));
        }

        internal CssProperty ParseDeclaration(String declarationText)
        {
            return Parse(declarationText, (b, t) => Tuple.Create(b.CreateDeclaration(ref t), t));
        }

        internal List<CssMedium> ParseMediaList(String mediaText)
        {
            return Parse(mediaText, (b, t) => Tuple.Create(b.CreateMedia(ref t), t));
        }

        internal IConditionFunction ParseCondition(String conditionText)
        {
            return Parse(conditionText, (b, t) => Tuple.Create(b.CreateCondition(ref t), t));
        }

        internal List<DocumentFunction> ParseDocumentRules(String documentText)
        {
            return Parse(documentText, (b, t) => Tuple.Create(b.CreateFunctions(ref t), t));
        }

        internal CssMedium ParseMedium(String mediumText)
        {
            return Parse(mediumText, (b, t) => Tuple.Create(b.CreateMedium(ref t), t));
        }

        internal CssKeyframeRule ParseKeyframeRule(String ruleText)
        {
            return Parse(ruleText, (b, t) => b.CreateKeyframeRule(t));
        }

        internal void AppendDeclarations(CssStyleDeclaration style, String declarations)
        {
            var tokenizer = CreateTokenizer(declarations);
            var builder = new CssBuilder(tokenizer, this);
            builder.FillDeclarations(style);
        }

        #endregion

        #region Helpers

        T Parse<T>(String source, Func<CssBuilder, CssToken, T> create)
        {
            var tokenizer = CreateTokenizer(source);
            var token = tokenizer.Get();
            var builder = new CssBuilder(tokenizer, this);
            var rule = create(builder, token);
            return tokenizer.Get().Type == CssTokenType.EndOfFile ? rule : default(T);
        }

        T Parse<T>(String source, Func<CssBuilder, CssToken, Tuple<T, CssToken>> create)
        {
            var tokenizer = CreateTokenizer(source);
            var token = tokenizer.Get();
            var builder = new CssBuilder(tokenizer, this);
            var pair = create(builder, token);
            return pair.Item2.Type == CssTokenType.EndOfFile ? pair.Item1 : default(T);
        }

        static CssTokenizer CreateTokenizer(String sourceCode)
        {
            var source = new TextSource(sourceCode);
            return new CssTokenizer(source);
        }

        static CssTokenizer CreateTokenizer(Stream sourceCode)
        {
            var source = new TextSource(sourceCode);
            return new CssTokenizer(source);
        }

        #endregion
    }
}
