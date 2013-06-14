using System;
using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AngleSharp.Css
{
    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    public class CssParser : IParser
    {
        #region Members

        Boolean started;
        Boolean quirksFlag;
        CssTokenizer tokenizer;
        CSSStyleSheet sheet;
        TaskCompletionSource<Boolean> tcs;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS parser instance with a new stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        public CssParser(String source)
            : this(new CSSStyleSheet(), new SourceManager(source))
        {
        }

        /// <summary>
        /// Creates a new CSS parser instance with an new stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        public CssParser(Stream stream)
            : this(new CSSStyleSheet(), new SourceManager(stream))
        {
        }

        /// <summary>
        /// Creates a new CSS parser instance with the specified stylesheet
        /// based on the given source.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="source">The source code as a string.</param>
        public CssParser(CSSStyleSheet stylesheet, String source)
            : this(stylesheet, new SourceManager(source))
        {
        }

        /// <summary>
        /// Creates a new CSS parser instance with the specified stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="stream">The stream to use as source.</param>
        public CssParser(CSSStyleSheet stylesheet, Stream stream)
            : this(stylesheet, new SourceManager(stream))
        {
        }

        /// <summary>
        /// Creates a new CSS parser instance parser with the specified stylesheet
        /// based on the given source manager.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="source">The source to use.</param>
        internal CssParser(CSSStyleSheet stylesheet, SourceManager source)
        {
            tokenizer = new CssTokenizer(source);

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ErrorOccurred != null)
                    ErrorOccurred(this, ev);
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
            get { return tcs != null; }
        }

        /// <summary>
        /// Gets the resulting stylesheet of the parsing.
        /// </summary>
        public CSSStyleSheet Result
        {
            get 
            {
                Parse();
                return sheet; 
            }
        }

        /// <summary>
        /// Gets or sets if the quirks-mode is activated.
        /// </summary>
        public bool IsQuirksMode
        {
            get { return quirksFlag; }
            set { quirksFlag = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the stylesheet.
        /// WARNING: This method is not yet implemented.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task ParseAsync()
        {
            if (!started)
            {
                started = true;
                tcs = new TaskCompletionSource<bool>();
                //TODO
                return tcs.Task;
            }
            else if (tcs == null)
            {
                var temp = new TaskCompletionSource<bool>();
                temp.SetResult(true);
                return temp.Task;
            }

            return tcs.Task;
        }

        /// <summary>
        /// Parses the given source code.
        /// </summary>
        public void Parse()
        {
            if (!started)
            {
                started = true;
                var rules = ConsumeRules(tokenizer.Iterator);

                for (int i = 0; i < rules.Count; i++)
                    sheet.CssRules.InsertAt(i, Investigate(rules[i]));
            }
        }

        #endregion

        #region Rules

        CSSRule Investigate(CssRule baseRule)
        {
            var result = Create(baseRule);

            if (result == null)
                throw new DOMException(ErrorCode.SyntaxError);

            return result;
        }

        CSSRule Create(CssRule baseRule)
        {
            if (baseRule is CssQualifiedRule)
                return Create((CssQualifiedRule)baseRule);
            else if (baseRule is CssDeclaration)
                return Create((CssDeclaration)baseRule);
            else if (baseRule is CssAtRule)
                return Create((CssAtRule)baseRule);

            return null;
        }

        CSSRule Create(CssDeclaration rule)
        {
            var style = new CSSStyleRule();
            var property = CreateProperty(rule);
            style.Style.AppendProperty(property);
            return style;
        }

        CSSRule Create(CssQualifiedRule rule)
        {
            var stylerule = new CSSStyleRule();
            stylerule.Selector = CssSelectorConstructor.Create(rule.Prelude);

            for (int i = 0; i < rule.Value.Count; i++)
            {
                if (rule.Value[i] is CssDeclaration)
                {
                    var decl = (CssDeclaration)rule.Value[i];
                    var property = CreateProperty(decl);
                    stylerule.Style.AppendProperty(property);
                }
            }

            return stylerule;
        }

        CSSRule Create(CssAtRule rule)
        {
            switch (rule.Name)
            {
                case "media":
                    return CreateMediaRule(rule);

                case "page":
                    return CreatePageRule(rule);

                case "import":
                    return CreateImportRule(rule);

                case "font-face":
                    return CreateFontFaceRule(rule);

                case "charset":
                    return CreateCharsetRule(rule);

                case "namespace":
                    return CreateNamespaceRule(rule);

                case "supports":
                    return CreateSupportsRule(rule);

                case "keyframes":
                    return CreateKeyframesRule(rule);

                default:
                    return CreateUnknownRule(rule);
            }
        }

        static CssFillType GetFillType(string type)
        {
            switch (type)
            {
                case "media":
                case "supports":
                case "keyframes":
                    return CssFillType.Rule;

                case "page":
                case "font-face":
                case "keyframe":
                    return CssFillType.Declaration;

                case "charset":
                case "namespace":
                case "import":
                default:
                    return CssFillType.None;
            }
        }

        CSSProperty CreateProperty(CssDeclaration rule)
        {
            var property = CSSProperty.Factory(rule.Name);
            property.Important = rule.Important;
            property.Value = CSSValue.Create(rule.Value);
            return property;
        }

        void ModifyMediaList(MediaList media, List<CssToken> tokens)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == CssTokenType.Whitespace)
                    continue;

                for (; i < tokens.Count; i++)
                {
                    if (tokens[i].Type == CssTokenType.Comma)
                        break;

                    sb.Append(tokens[i].ToValue());
                }

                media.AppendMedium(sb.ToString());
                sb.Clear();
            }
        }

        CSSRule CreateUnknownRule(CssAtRule rule)
        {
            return new CSSRule
            {
                CssText = rule.ToString()
            };
        }

        CSSRule CreateKeyframesRule(CssAtRule rule)
        {
            var keyframes = new CSSKeyframesRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null && rule.Prelude[i].Preserved.Type == CssTokenType.Ident)
                {
                    keyframes.Name = rule.Prelude[i].Preserved.ToValue();
                    break;
                }
            }

            //rule.Value.Tokens

            //TODO
            //@keyframes IDENTIFIER { ... }
            //where ... is a reptition of a list of component values and { a list of declarations }
            //this will insert a list of @keyframe

            return keyframes;
        }

        CSSRule CreateSupportsRule(CssAtRule rule)
        {
            var support = new CSSSupportsRule();
            var sb = new StringBuilder();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null)
                    sb.Append(rule.Prelude[i].ToString());
            }

            support.ConditionText = sb.ToString();
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var rules = ConsumeRules(it);

            for (int i = 0; i < rules.Count; i++)
                support.CssRules.InsertAt(i, Investigate(rules[i]));

            return support;
        }

        CSSRule CreateNamespaceRule(CssAtRule rule)
        {
            var ns = new CSSNamespaceRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved == null)
                    continue;

                if (rule.Prelude[i].Preserved.Type == CssTokenType.Ident)
                    ns.Prefix = rule.Prelude[i].Preserved.ToValue();
                else if (rule.Prelude[i].Preserved.Type == CssTokenType.String)
                    ns.NamespaceURI = rule.Prelude[i].Preserved.ToValue();
            }

            return ns;
        }

        CSSRule CreateCharsetRule(CssAtRule rule)
        {
            var charset = new CSSCharsetRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null && rule.Prelude[i].Preserved.Type == CssTokenType.String)
                {
                    charset.Encoding = ((CssStringToken)rule.Prelude[i].Preserved).Data;
                    break;
                }
            }

            return charset;
        }

        CSSRule CreateFontFaceRule(CssAtRule rule)
        {
            var fontface = new CSSFontFaceRule();
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var decls = ConsumeDeclarations(it);

            foreach (var decl in decls)
            {
                if (decl is CssDeclaration)
                    fontface.AppendRule(CreateProperty((CssDeclaration)decl));
            }

            return fontface;
        }

        CSSRule CreateImportRule(CssAtRule rule)
        {
            var import = new CSSImportRule();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved == null)
                    continue;

                switch (rule.Prelude[i].Preserved.Type)
                {
                    case CssTokenType.String:
                    case CssTokenType.Url:
                        import.Href = ((CssStringToken)rule.Prelude[i].Preserved).Data;

                        if (++i < rule.Prelude.Count)
                        {
                            var tokens = new List<CssToken>();

                            for (; i < rule.Prelude.Count; i++)
                            {
                                if (rule.Prelude[i].Preserved != null)
                                    tokens.Add(rule.Prelude[i].Preserved);
                            }

                            ModifyMediaList(import.Media, tokens);
                        }

                        break;
                }
            }

            return import;
        }

        CSSRule CreatePageRule(CssAtRule rule)
        {
            var page = new CSSPageRule();
            var selector = string.Empty;

            for (int i = 0; i < rule.Prelude.Count; i++)
                selector += rule.Prelude[i].ToString();

            page.SelectorText = selector;
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var decls = ConsumeDeclarations(it);

            foreach (var decl in decls)
            {
                if (decl is CssDeclaration)
                    page.AppendRule(CreateProperty((CssDeclaration)decl));
            }

            return page;
        }

        CSSRule CreateMediaRule(CssAtRule rule)
        {
            var media = new CSSMediaRule();
            var tokens = new List<CssToken>();

            for (int i = 0; i < rule.Prelude.Count; i++)
            {
                if (rule.Prelude[i].Preserved != null)
                    tokens.Add(rule.Prelude[i].Preserved);
            }

            ModifyMediaList(media.Media, tokens);
            tokens.Clear();
            var it = ((IEnumerable<CssToken>)rule.Value.Tokens).GetEnumerator();
            var rules = ConsumeRules(it);

            for (int i = 0; i < rules.Count; i++)
                media.CssRules.InsertAt(i, Create(rules[i]));

            return media;
        }

        #endregion

        #region Consumers

        /// <summary>
        /// Tries to consume a list of rules from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The list of rules.</returns>
        internal List<CssRule> ConsumeRules(IEnumerator<CssToken> source)
        {
            var rules = new List<CssRule>();

            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Cdc:
                    case CssTokenType.Cdo:
                    case CssTokenType.Whitespace:
                        break;

                    case CssTokenType.AtKeyword:
                        rules.Add(ConsumeAtRule(source));
                        break;

                    default:
                        rules.Add(ConsumeQualifiedRule(source));
                        break;
                }
            }


            return rules;
        }

        /// <summary>
        /// Tries to consume a @-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @-rule or null.</returns>
        internal CssAtRule ConsumeAtRule(IEnumerator<CssToken> source)
        {
            var rule = new CssAtRule();
            var keyword = source.Current as CssKeywordToken;

            if (keyword == null)
                return null;

            rule.Name = keyword.Data;

            while (source.MoveNext())
                if (source.Current.Type != CssTokenType.Whitespace)
                    break;

            do
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Semicolon:
                        return rule;

                    case CssTokenType.CurlyBracketOpen:
                        rule.Value = ConsumeSimpleBlock(source);
                        return rule;

                    default:
                        rule.Prelude.Add(ConsumeComponentValue(source));
                        break;
                }
            }
            while (source.MoveNext());

            return rule;
        }

        /// <summary>
        /// Tries to consume a qualified rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The qualified rule.</returns>
        internal CssQualifiedRule ConsumeQualifiedRule(IEnumerator<CssToken> source)
        {
            var rule = new CssQualifiedRule();

            if (source.Current == null)
                return null;

            do
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.CurlyBracketOpen:
                        source.MoveNext();
                        var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                        var declarations = ConsumeDeclarations(tokens.GetEnumerator());
                        rule.Value.AddRange(declarations);
                        return rule;

                    default:
                        rule.Prelude.Add(ConsumeComponentValue(source));
                        break;
                }

            }
            while (source.MoveNext());

            return rule;
        }

        /// <summary>
        /// Tries to consume a list of declarations from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The list of declarations.</returns>
        internal List<CssNamedRule> ConsumeDeclarations(IEnumerator<CssToken> source)
        {
            var list = new List<CssNamedRule>();

            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Whitespace:
                    case CssTokenType.Semicolon:
                        break;

                    case CssTokenType.AtKeyword:
                        list.Add(ConsumeAtRule(source));
                        break;

                    case CssTokenType.Ident:
                        var tokens = Jump(source, CssTokenType.Semicolon);
                        var it = tokens.GetEnumerator();
                        it.MoveNext();
                        var decl = ConsumeDeclaration(it);

                        if(decl != null)
                            list.Add(decl);

                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                        Jump(source, CssTokenType.Semicolon);
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// Tries to consume a declaration from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The declaration or NULL.</returns>
        internal CssDeclaration ConsumeDeclaration(IEnumerator<CssToken> source)
        {
            var decl = new CssDeclaration();
            var keyword = source.Current as CssKeywordToken;

            if (keyword == null)
                return null;

            decl.Name = keyword.Data;

            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Whitespace:
                        break;

                    case CssTokenType.Colon:
                        while (source.MoveNext())
                        {
                            var value = ConsumeComponentValue(source);
                            decl.Prelude.Add(value);
                        }

                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.InputUnexpected);
                        return null;
                }

            }

            if (decl.Prelude.Count > 1)
            {
                var prev = decl.Prelude[decl.Prelude.Count - 2].Preserved as CssDelimToken;
                var ident = decl.Prelude[decl.Prelude.Count - 1].Preserved as CssKeywordToken;

                if (prev != null && ident != null)
                {
                    decl.Important = prev.Data == Specification.EM && ident.Data.Equals("important", StringComparison.OrdinalIgnoreCase);
                    decl.Prelude.RemoveRange(decl.Prelude.Count - 2, 2);
                }
            }

            return decl;
        }

        /// <summary>
        /// Tries to consume a component value from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        internal CssComponentValue ConsumeComponentValue(IEnumerator<CssToken> source)
        {
            if (source.Current == null)
                return null;

            switch (source.Current.Type)
            {
                case CssTokenType.CurlyBracketOpen:
                case CssTokenType.SquareBracketOpen:
                case CssTokenType.RoundBracketOpen:
                    return new CssComponentValue { Block = ConsumeSimpleBlock(source) };

                case CssTokenType.Function:
                    return new CssComponentValue { Function = ConsumeFunction(source) };

                default:
                    return new CssComponentValue { Preserved = source.Current };
            }
        }

        /// <summary>
        /// Tries to consume a hashless component value (quirks-mode) from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        internal CssComponentValue ConsumeComponentValueHashless(IEnumerator<CssToken> source)
        {
            if (source.Current == null)
                return null;

            //background-color
            //border-color
            //border-top-color
            //border-right-color
            //border-bottom-color
            //border-left-color
            //color

            switch (source.Current.Type)
            {
                case CssTokenType.Number:
                case CssTokenType.Dimension:
                case CssTokenType.Ident:
                    var value = source.Current.ToValue();

                    if ((value.Length == 3 || value.Length == 6) && Specification.IsHex(value))
                        return new CssComponentValue() { Preserved = CssKeywordToken.Hash(value) };

                    break;
            }

            return ConsumeComponentValue(source);
        }

        /// <summary>
        /// Tries to consume a unitless component value (quirks-mode) from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        internal CssComponentValue ConsumeComponentValueUnitless(IEnumerator<CssToken> source)
        {
            if (source.Current == null)
                return null;

            //border-top-width
            //border-right-width
            //border-bottom-width
            //border-left-width
            //border-width
            //bottom
            //font-size
            //height
            //left
            //letter-spacing
            //margin
            //margin-right
            //margin-left
            //margin-top
            //margin-bottom
            //padding
            //padding-top
            //padding-bottom
            //padding-left
            //padding-right
            //right
            //top
            //width
            //word-spacing

            if (source.Current.Type == CssTokenType.Number)
            {
                var value = ((CssNumberToken)source.Current).Data;
                return new CssComponentValue { Preserved = CssUnitToken.Dimension(value, "px") };
            }

            return ConsumeComponentValue(source);
        }

        /// <summary>
        /// Tries to consume a block from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The block or NULL.</returns>
        internal CssBlock ConsumeSimpleBlock(IEnumerator<CssToken> source)
        {
            var block = new CssBlock();

            block.AssociatedToken = source.Current as CssBracketToken;

            if (block.AssociatedToken == null)
                return null;

            block.Tokens.Add(source.Current);

            while (source.MoveNext())
            {
                block.Tokens.Add(source.Current);

                if (source.Current.Type == block.AssociatedToken.Mirror)
                    break;

                var value = ConsumeComponentValue(source);

                if (value != null)
                    block.Value.Add(value);
            }

            return block;
        }

        /// <summary>
        /// Tries to consume a function from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The function or NULL.</returns>
        internal CssFunction ConsumeFunction(IEnumerator<CssToken> source)
        {
            var function = new CssFunction();
            var keyword = source.Current as CssKeywordToken;

            if (keyword == null)
                return null;

            function.Name = keyword.Data;
            var arg = new CssArg();
            function.Arguments.Add(arg);
            function.Tokens.Add(source.Current);

            while (source.MoveNext())
            {
                function.Tokens.Add(source.Current);

                switch (source.Current.Type)
                {
                    case CssTokenType.RoundBracketClose:
                        return function;

                    case CssTokenType.Comma:
                        arg = new CssArg();
                        function.Arguments.Add(arg);
                        break;

                    case CssTokenType.Number:
                        CssComponentValue value;

                        if (quirksFlag && function.Name.Equals("rect", StringComparison.OrdinalIgnoreCase))
                            value = ConsumeComponentValueUnitless(source);
                        else
                            value = ConsumeComponentValue(source);

                        if (value != null)
                            arg.Values.Add(value);
                        else
                            function.IsValid = false;

                        break;

                    default:
                        arg.Values.Add(ConsumeComponentValue(source));
                        break;
                }
            }

            return function;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Jumps in the given iterator to the specified token.
        /// </summary>
        /// <param name="source">The iterator to consider.</param>
        /// <param name="token">The type of the final (not consumed) token.</param>
        /// <returns>The (optional) list of skipped tokens.</returns>
        static List<CssToken> Jump(IEnumerator<CssToken> source, CssTokenType token)
        {
            var tokens = new List<CssToken>();

            do
            {
                if (source.Current.Type == token)
                    break;

                tokens.Add(source.Current);
            }
            while (source.MoveNext());

            return tokens;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="selector">The string to parse.</param>
        /// <returns>The Selector object.</returns>
        public static Selector ParseSelector(String selector)
        {
            var parser = new CssParser(selector);
            var tokens = parser.tokenizer.Iterator;
            var ctor = new CssSelectorConstructor();

            while (tokens.MoveNext())
                ctor.PickSelector(tokens);

            return ctor.Result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS stylesheet.
        /// </summary>
        /// <param name="stylesheet">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSStyleSheet object.</returns>
        public static CSSStyleSheet ParseStyleSheet(String stylesheet, Boolean quirksMode = false)
        {
            var parser = new CssParser(stylesheet);
            parser.IsQuirksMode = quirksMode;
            return parser.Result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSRule object.</returns>
        public static CSSRule ParseRule(String rule, Boolean quirksMode = false)
        {
            var parser = new CssParser(rule);
            parser.IsQuirksMode = quirksMode;
            var it = parser.tokenizer.Iterator;

            while (it.Current != null)
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    break;

                if (!it.MoveNext())
                    break;
            }

            if (it.Current == null)
                throw new DOMException(ErrorCode.SyntaxError);

            if (it.Current.Type == CssTokenType.Cdo || it.Current.Type == CssTokenType.Cdc)
                throw new DOMException(ErrorCode.SyntaxError);

            CSSRule myrule = null;

            if (it.Current.Type == CssTokenType.AtKeyword)
            {
                var atrule = parser.ConsumeAtRule(it);

                if (atrule == null)
                    throw new DOMException(ErrorCode.SyntaxError);

                myrule = parser.Investigate(atrule);
            }
            else
            {
                var qrule = parser.ConsumeQualifiedRule(it);

                if (qrule == null)
                    throw new DOMException(ErrorCode.SyntaxError);

                myrule = parser.Investigate(qrule);
            }

            while (it.MoveNext())
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    break;
            }

            if (it.Current.Type != CssTokenType.Whitespace)
                throw new DOMException(ErrorCode.SyntaxError);

            return myrule;
        }

        /// <summary>
        /// Takes a string and transforms it into CSS declarations.
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration ParseDeclarations(String declarations, Boolean quirksMode = false)
        {
            var parser = new CssParser(declarations);
            parser.IsQuirksMode = quirksMode;
            var it = parser.tokenizer.Iterator;
            var decl = parser.ConsumeDeclarations(it);

            if (decl != null)
                return new CSSStyleDeclaration(decl);

            return null;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValue object.</returns>
        public static CSSValue ParseValue(String value, Boolean quirksMode = false)
        {
            var parser = new CssParser(value);
            parser.IsQuirksMode = quirksMode;
            var it = parser.tokenizer.Iterator;

            while (it.Current != null)
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    break;

                if (!it.MoveNext())
                    throw new DOMException(ErrorCode.SyntaxError);
            }

            var cmp = parser.ConsumeComponentValue(it);

            if (cmp == null)
                throw new DOMException(ErrorCode.SyntaxError);

            while (true)
            {
                if (it.Current.Type != CssTokenType.Whitespace)
                    throw new DOMException(ErrorCode.SyntaxError);

                if (!it.MoveNext())
                    return CSSValue.Create(cmp);
            }
        }

        /// <summary>
        /// Takes a string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="values">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValueList object.</returns>
        internal static CSSValueList ParseValueList(String values, Boolean quirksMode = false)
        {
            var list = new List<CSSValue>();
            var parser = new CssParser(values);
            parser.IsQuirksMode = quirksMode;
            var it = parser.tokenizer.Iterator;

            while (it.Current != null)
            {
                var value = parser.ConsumeComponentValue(it);
                list.Add(CSSValue.Create(value));

                if (!it.MoveNext())
                    break;
            }

            return new CSSValueList(list);
        }

        /// <summary>
        /// Takes a comma separated string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="values">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValueList object.</returns>
        internal static List<CSSValueList> ParseMultipleValues(String values, Boolean quirksMode = false)
        {
            var parser = new CssParser(values);
            parser.IsQuirksMode = quirksMode;
            var it = parser.tokenizer.Iterator;
            var val = new List<CSSValueList>();
            var temp = new List<CSSValue>();

            while (it.Current != null)
            {
                if (it.Current.Type == CssTokenType.Comma)
                {
                    val.Add(new CSSValueList(temp));
                    temp = new List<CSSValue>();
                }
                else
                {
                    var value = parser.ConsumeComponentValue(it);
                    temp.Add(CSSValue.Create(value));
                }

                if (!it.MoveNext())
                    break;
            }

            if (temp.Count > 0)
                val.Add(new CSSValueList(temp));

            return val;
        }

        #endregion

        #region Event-Helpers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        void RaiseErrorOccurred(ErrorCode code)
        {
            if (ErrorOccurred != null)
            {
                var pck = new ParseErrorEventArgs((int)code, Errors.GetError(code));
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
