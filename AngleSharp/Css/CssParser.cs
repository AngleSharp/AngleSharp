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
        StringBuilder buffer;
        Stack<CSSRule> open;

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
            buffer = new StringBuilder();
            tokenizer = new CssTokenizer(source);

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ErrorOccurred != null)
                    ErrorOccurred(this, ev);
            };

            started = false;
            sheet = stylesheet;
            open = new Stack<CSSRule>();
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

        /// <summary>
        /// Gets the current rule if any.
        /// </summary>
        internal CSSRule CurrentRule
        {
            get { return open.Count > 0 ? open.Peek() : null; }
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
                AppendRules(tokenizer.Iterator, sheet.CssRules.List);
            }
        }

        #endregion

        #region Stylesheet construction

        /// <summary>
        /// Tries to append rules from the given source to the list of rules.
        /// </summary>
        /// <param name="source">The token iterator (source).</param>
        /// <param name="rules">The list of rules to append to.</param>
        void AppendRules(IEnumerator<CssToken> source, List<CSSRule> rules)
        {
            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Cdc:
                    case CssTokenType.Cdo:
                    case CssTokenType.Whitespace:
                        break;

                    case CssTokenType.AtKeyword:
                        rules.Add(CreateAtRule(source));
                        break;

                    default:
                        rules.Add(CreateStyleRule(source));
                        break;
                }
            }
        }

        /// <summary>
        /// Tries to append declarations from the given source to the list of declarations.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <param name="declarations">The list of declarations to append to.</param>
        void AppendDeclarations(IEnumerator<CssToken> source, List<CSSProperty> declarations)
        {
            while (source.MoveNext())
            {
                switch (source.Current.Type)
                {
                    case CssTokenType.Whitespace:
                    case CssTokenType.Semicolon:
                        break;

                    case CssTokenType.Ident:
                        var tokens = Jump(source, CssTokenType.Semicolon);
                        var it = tokens.GetEnumerator();
                        it.MoveNext();
                        var decl = CreateDeclaration(it);

                        if (decl != null)
                            declarations.Add(decl);

                        break;

                    default:
                        RaiseErrorOccurred(ErrorCode.InvalidCharacter);
                        SkipToNextSemicolon(source);
                        break;
                }
            }
        }

        /// <summary>
        /// Tries to append media labels from the given source to the medialist.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <param name="media">The medialist to append to.</param>
        /// <param name="endToken">The optional token type to finish appending to the list.</param>
        void AppendMediaList(IEnumerator<CssToken> source, MediaList media, CssTokenType endToken = CssTokenType.Semicolon)
        {
            do
            {
                if (source.Current.Type == CssTokenType.Whitespace)
                    continue;
                else if (source.Current.Type == endToken)
                    break;

                do
                {
                    if (source.Current.Type == CssTokenType.Comma || source.Current.Type == endToken)
                        break;
                    else if (source.Current.Type == CssTokenType.Whitespace)
                        continue;

                    buffer.Append(source.Current.ToValue());
                }
                while (source.MoveNext());

                media.AppendMedium(buffer.ToString());
                buffer.Clear();

                if (source.Current.Type == endToken)
                    break;
            }
            while (source.MoveNext());
        }

        /// <summary>
        /// Tries to consume a component value from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        CSSValue CreateValue(IEnumerator<CssToken> source)
        {
            CSSValue value = null;

            switch (source.Current.Type)
            {
                case CssTokenType.String:
                    value = new CSSPrimitiveValue(UnitType.String, ((CssStringToken)source.Current).Data);
                    break;

                case CssTokenType.Url:
                    value = new CSSPrimitiveValue(UnitType.Uri, ((CssStringToken)source.Current).Data);
                    break;

                case CssTokenType.Ident:
                    value = new CSSPrimitiveValue(UnitType.Ident, ((CssKeywordToken)source.Current).Data);
                    break;

                case CssTokenType.Percentage:
                    value = new CSSPrimitiveValue(UnitType.Percentage, ((CssUnitToken)source.Current).Data);
                    break;

                case CssTokenType.Dimension:
                    value = new CSSPrimitiveValue(((CssUnitToken)source.Current).Unit, ((CssUnitToken)source.Current).Data);
                    break;

                case CssTokenType.Number:
                    value = new CSSPrimitiveValue(UnitType.Number, ((CssNumberToken)source.Current).Data);
                    break;

                case CssTokenType.Function:
                    value = CreateFunction(source);
                    break;
            }

            SkipToNextSemicolon(source);
            return value;
        }

        /// <summary>
        /// Tries to consume a hashless component value (quirks-mode) from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        CSSValue CreateValueHashless(IEnumerator<CssToken> source)
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

            return CreateValue(source);
        }

        /// <summary>
        /// Tries to consume a unitless component value (quirks-mode) from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The value or NULL.</returns>
        CSSValue CreateValueUnitless(IEnumerator<CssToken> source)
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

            return CreateValue(source);
        }

        /// <summary>
        /// Tries to consume a function from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The function or NULL.</returns>
        CSSFunction CreateFunction(IEnumerator<CssToken> source)
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
                            value = CreateValueUnitless(source);
                        else
                            value = CreateValue(source);

                        if (value != null)
                            arg.Values.Add(value);
                        else
                            function.IsValid = false;

                        break;

                    default:
                        arg.Values.Add(CreateValue(source));
                        break;
                }
            }

            return function;
        }

        /// <summary>
        /// Creates a new style rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The style rule.</returns>
        CSSStyleRule CreateStyleRule(IEnumerator<CssToken> source)
        {
            var style = new CSSStyleRule();
            var ctor = new CssSelectorConstructor();
            style.ParentStyleSheet = sheet;
            style.ParentRule = CurrentRule;
            open.Push(style);

            do
            {
                if (source.Current.Type == CssTokenType.CurlyBracketOpen)
                {
                    SkipToNextNonWhitespace(source);
                    var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                    AppendDeclarations(tokens.GetEnumerator(), style.Style.List);
                    source.MoveNext();
                    break;
                }

                ctor.PickSelector(source);
            }
            while (source.MoveNext());

            style.Selector = ctor.Result;
            open.Pop();
            return style;
        }

        /// <summary>
        /// Creates a @-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @-rule.</returns>
        CSSRule CreateAtRule(IEnumerator<CssToken> source)
        {
            var name = ((CssKeywordToken)source.Current).Data;
            SkipToNextNonWhitespace(source);

            switch (name)
            {
                case "media": return CreateMediaRule(source);
                case "page": return CreatePageRule(source);
                case "import": return CreateImportRule(source);
                case "font-face": return CreateFontFaceRule(source);
                case "charset": return CreateCharsetRule(source);
                case "namespace": return CreateNamespaceRule(source);
                case "supports": return CreateSupportsRule(source);
                case "keyframes": return CreateKeyframesRule(source);
                default: return CreateUnknownRule(name, source);
            }
        }

        /// <summary>
        /// Creates a new property from the given source.
        /// </summary>
        /// <param name="source">The token iterator starting at the name of the property.</param>
        /// <returns>The new property.</returns>
        CSSProperty CreateDeclaration(IEnumerator<CssToken> source)
        {
            var name = ((CssKeywordToken)source.Current).Data;
            var property = CSSProperty.Factory(name);
            SkipToNextNonWhitespace(source);

            if (source.Current.Type == CssTokenType.Colon)
            {
                SkipToNextNonWhitespace(source);
                property.Value = CreateValue(source);

                if (source.Current.Type == CssTokenType.Delim)
                {
                    if (((CssDelimToken)source.Current).Data == Specification.EM && source.MoveNext())
                        property.Important = source.Current.Type == CssTokenType.Ident && ((CssKeywordToken)source.Current).Data.Equals("important", StringComparison.OrdinalIgnoreCase);
                }
            }

            SkipBehindNextSemicolon(source);
            return property;
        }

        /// <summary>
        /// Creates a new unknown @-rule from the given source.
        /// </summary>
        /// <param name="name">The name of the @-rule.</param>
        /// <param name="source">The token iterator.</param>
        /// <returns>The unknown @-rule.</returns>
        CSSRule CreateUnknownRule(String name, IEnumerator<CssToken> source)
        {
            var rule = new CSSRule();
            var endCurly = 0;
            rule.ParentStyleSheet = sheet;
            rule.ParentRule = CurrentRule;
            open.Push(rule);
            buffer.Append(name).Append(" ");

            do
            {
                if (source.Current.Type == CssTokenType.Semicolon && endCurly == 0)
                {
                    source.MoveNext();
                    break;
                }

                buffer.Append(source.Current.ToString());

                if (source.Current.Type == CssTokenType.CurlyBracketOpen)
                    endCurly++;
                else if (source.Current.Type == CssTokenType.CurlyBracketClose && --endCurly == 0)
                    break;
            }
            while (source.MoveNext());

            source.MoveNext();
            rule.CssText = buffer.ToString();
            buffer.Clear();
            open.Pop();
            return rule;
        }

        /// <summary>
        /// Creates a new @keyframes-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @keyframes-rule.</returns>
        CSSKeyframesRule CreateKeyframesRule(IEnumerator<CssToken> source)
        {
            var keyframes = new CSSKeyframesRule();
            keyframes.ParentStyleSheet = sheet;
            keyframes.ParentRule = CurrentRule;
            open.Push(keyframes);

            if (source.Current.Type == CssTokenType.Ident)
            {
                keyframes.Name = ((CssKeywordToken)source.Current).Data;
                SkipToNextNonWhitespace(source);

                if (source.Current.Type == CssTokenType.CurlyBracketOpen)
                {

                    //TODO
                    //@keyframes IDENTIFIER { ... }
                    //where ... is a reptition of a list of component values and { a list of declarations }
                    //this will insert a list of @keyframe
                }
            }

            open.Pop();
            return keyframes;
        }

        /// <summary>
        /// Creates a new @supports-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @supports-rule.</returns>
        CSSSupportsRule CreateSupportsRule(IEnumerator<CssToken> source)
        {
            var supports = new CSSSupportsRule();
            supports.ParentStyleSheet = sheet;
            supports.ParentRule = CurrentRule;
            open.Push(supports);

            do
            {
                if (source.Current.Type == CssTokenType.CurlyBracketOpen)
                {
                    SkipToNextNonWhitespace(source);
                    var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                    AppendRules(tokens.GetEnumerator(), supports.CssRules.List);
                    source.MoveNext();
                    break;
                }

                buffer.Append(source.Current.ToString());
            }
            while (source.MoveNext());

            supports.ConditionText = buffer.ToString();
            buffer.Clear();
            open.Pop();
            return supports;
        }

        /// <summary>
        /// Creates a new @namespace-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @namespace-rule.</returns>
        CSSNamespaceRule CreateNamespaceRule(IEnumerator<CssToken> source)
        {
            var ns = new CSSNamespaceRule();
            ns.ParentStyleSheet = sheet;

            if (source.Current.Type == CssTokenType.Ident)
            {
                ns.Prefix = source.Current.ToValue();
                SkipToNextNonWhitespace(source);
                
                if (source.Current.Type == CssTokenType.String)
                    ns.NamespaceURI = source.Current.ToValue();
            }

            SkipBehindNextSemicolon(source);
            return ns;
        }

        /// <summary>
        /// Creates a new @charset-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @charset-rule.</returns>
        CSSCharsetRule CreateCharsetRule(IEnumerator<CssToken> source)
        {
            var charset = new CSSCharsetRule();
            charset.ParentStyleSheet = sheet;

            if (source.Current.Type == CssTokenType.String)
                charset.Encoding = ((CssStringToken)source.Current).Data;

            SkipBehindNextSemicolon(source);
            return charset;
        }

        /// <summary>
        /// Creates a new @font-face-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @font-face-rule.</returns>
        CSSFontFaceRule CreateFontFaceRule(IEnumerator<CssToken> source)
        {
            var fontface = new CSSFontFaceRule();
            fontface.ParentStyleSheet = sheet;
            fontface.ParentRule = CurrentRule;
            open.Push(fontface);

            if(source.Current.Type == CssTokenType.CurlyBracketOpen)
            {
                SkipToNextNonWhitespace(source);
                var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                AppendDeclarations(tokens.GetEnumerator(), fontface.CssRules.List);
                source.MoveNext();
            }

            open.Pop();
            return fontface;
        }

        /// <summary>
        /// Creates a new @import-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @import-rule.</returns>
        CSSImportRule CreateImportRule(IEnumerator<CssToken> source)
        {
            var import = new CSSImportRule();
            import.ParentStyleSheet = sheet;
            import.ParentRule = CurrentRule;
            open.Push(import);

            switch (source.Current.Type)
            {
                case CssTokenType.Semicolon:
                    source.MoveNext();
                    break;

                case CssTokenType.String:
                case CssTokenType.Url:
                    import.Href = ((CssStringToken)source.Current).Data;
                    AppendMediaList(source, import.Media, CssTokenType.Semicolon);
                    source.MoveNext();
                    break;

                default:
                    SkipBehindNextSemicolon(source);
                    break;
            }

            open.Pop();
            return import;
        }

        /// <summary>
        /// Creates a new @page-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @page-rule.</returns>
        CSSPageRule CreatePageRule(IEnumerator<CssToken> source)
        {
            var page = new CSSPageRule();
            page.ParentStyleSheet = sheet;
            page.ParentRule = CurrentRule;
            open.Push(page);
            var ctor = new CssSelectorConstructor();

            do
            {
                if (source.Current.Type == CssTokenType.CurlyBracketOpen)
                {
                    SkipToNextNonWhitespace(source);
                    var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                    AppendDeclarations(tokens.GetEnumerator(), page.Style.List);
                    source.MoveNext();
                    break;
                }

                ctor.PickSelector(source);
            }
            while (source.MoveNext());

            page.Selector = ctor.Result;
            open.Pop();
            return page;
        }

        /// <summary>
        /// Creates a new @media-rule from the given source.
        /// </summary>
        /// <param name="source">The token iterator.</param>
        /// <returns>The @media-rule.</returns>
        CSSMediaRule CreateMediaRule(IEnumerator<CssToken> source)
        {
            var media = new CSSMediaRule();
            media.ParentStyleSheet = sheet;
            media.ParentRule = CurrentRule;
            open.Push(media);
            AppendMediaList(source, media.Media, CssTokenType.CurlyBracketOpen);

            if (source.Current.Type == CssTokenType.CurlyBracketOpen)
            {
                SkipToNextNonWhitespace(source);
                var tokens = Jump(source, CssTokenType.CurlyBracketClose);
                AppendRules(tokens.GetEnumerator(), media.CssRules.List);
                source.MoveNext();
            }

            open.Pop();
            return media;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Moves from the current position to the next position that is not a whitespace
        /// token.
        /// </summary>
        /// <param name="source">The iterator to walk through.</param>
        static void SkipToNextNonWhitespace(IEnumerator<CssToken> source)
        {
            while (source.MoveNext())
                if (source.Current.Type != CssTokenType.Whitespace)
                    break;
        }

        /// <summary>
        /// Moves from the current position to the next position that is a semicolon token.
        /// </summary>
        /// <param name="source">The iterator to walk through.</param>
        static void SkipToNextSemicolon(IEnumerator<CssToken> source)
        {
            do
            {
                if (source.Current.Type == CssTokenType.Semicolon)
                    break;
            }
            while (source.MoveNext());
        }

        /// <summary>
        /// Moves from the current position to the next position that is following a
        /// semicolon token.
        /// </summary>
        /// <param name="source">The iterator to walk through.</param>
        static void SkipBehindNextSemicolon(IEnumerator<CssToken> source)
        {
            do
            {
                if (source.Current.Type == CssTokenType.Semicolon)
                {
                    source.MoveNext();
                    break;
                }
            }
            while (source.MoveNext());
        }

        /// <summary>
        /// /Gets the CssFillType of the specified CSS (@) rule.
        /// </summary>
        /// <param name="type">The type of CSS rule like media, style, page, ...</param>
        /// <returns>The corresponding CSS fill type.</returns>
        static CssFillType GetFillType(String type)
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

        /// <summary>
        /// Jumps in the given iterator to the specified token.
        /// </summary>
        /// <param name="source">The iterator to consider.</param>
        /// <param name="token">The type of the final (not consumed) token.</param>
        /// <returns>The (optional) list of skipped tokens.</returns>
        static IEnumerable<CssToken> Jump(IEnumerator<CssToken> source, CssTokenType token)
        {
            do
            {
                if (source.Current.Type == token)
                    yield break;

                yield return source.Current;
            }
            while (source.MoveNext());
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        /// <param name="selector">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The Selector object.</returns>
        public static Selector ParseSelector(String selector, Boolean quirksMode = false)
        {
            var parser = new CssParser(selector);
            parser.IsQuirksMode = quirksMode;
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
            SkipToNextNonWhitespace(it);

            if (it.Current.Type == CssTokenType.Cdo || it.Current.Type == CssTokenType.Cdc)
                throw new DOMException(ErrorCode.SyntaxError);

            return (it.Current.Type == CssTokenType.AtKeyword) ? parser.CreateAtRule(it) : parser.CreateStyleRule(it);
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
            var decl = new CSSStyleDeclaration();
            SkipToNextNonWhitespace(it);
            parser.AppendDeclarations(it, decl.List);
            return decl;
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
            SkipToNextNonWhitespace(it);
            return parser.CreateValue(it);
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

            do
            {
                SkipToNextNonWhitespace(it);
                list.Add(parser.CreateValue(it));
            }
            while (it.MoveNext());

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

            do
            {
                SkipToNextNonWhitespace(it);

                if (it.Current.Type == CssTokenType.Comma)
                {
                    val.Add(new CSSValueList(temp));
                    temp = new List<CSSValue>();
                }
                else
                {
                    temp.Add(parser.CreateValue(it));
                }
            }
            while (it.MoveNext());

            if (temp.Count > 0) val.Add(new CSSValueList(temp));
            return val;
        }

        internal static CSSKeyframeRule ParseKeyframeRule(String rule, Boolean quirksMode = false)
        {
            throw new NotImplementedException();
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
