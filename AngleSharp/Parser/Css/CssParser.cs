namespace AngleSharp.Parser.Css
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class CssParser : IParser
    {
		#region Fields
		
		CssSelectorConstructor selector;
        CssValueBuilder value;
		Boolean skipExceptions;
        CssTokenizer tokenizer;
		CSSProperty property;
        Boolean started;
        Boolean quirks;
        CSSStyleSheet sheet;
        Stack<CSSRule> open;
		StringBuilder buffer;
		CssState state;
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
        public CssParser(CSSStyleSheet stylesheet, String source)
            : this(stylesheet, new SourceManager(source, stylesheet.Options.DefaultEncoding()))
        { }

        /// <summary>
        /// Creates a new CSS parser instance with the specified stylesheet
        /// based on the given stream.
        /// </summary>
        /// <param name="stylesheet">The stylesheet to be constructed.</param>
        /// <param name="stream">The stream to use as source.</param>
        public CssParser(CSSStyleSheet stylesheet, Stream stream)
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
            skipExceptions = true;
            tokenizer = new CssTokenizer(source);

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ParseError != null)
                    ParseError(this, ev);
            };

            quirks = stylesheet.Options.UseQuirksMode;
            started = false;
            sheet = stylesheet;
            open = new Stack<CSSRule>();
			SwitchTo(CssState.Data);
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
        public CSSStyleSheet Result
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

		#region States

		/// <summary>
		/// The general state.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean Data(CssToken token)
		{
			if (token.Type == CssTokenType.AtKeyword)
			{
				switch (((CssKeywordToken)token).Data)
				{
					case RuleNames.MEDIA:
					{
						AddRule(new CSSMediaRule());
						SwitchTo(CssState.InMediaList);
						break;
					}
					case RuleNames.PAGE:
					{
						AddRule(new CSSPageRule());
						SwitchTo(CssState.InSelector);
						break;
					}
					case RuleNames.IMPORT:
					{
						AddRule(new CSSImportRule());
						SwitchTo(CssState.BeforeImport);
						break;
					}
					case RuleNames.FONT_FACE:
					{
						AddRule(new CSSFontFaceRule());
						SwitchTo(CssState.InDeclaration);
						break;
					}
					case RuleNames.CHARSET:
					{
						AddRule(new CSSCharsetRule());
						SwitchTo(CssState.BeforeCharset);
						break;
					}
					case RuleNames.NAMESPACE:
					{
						AddRule(new CSSNamespaceRule());
						SwitchTo(CssState.BeforeNamespacePrefix);
						break;
					}
					case RuleNames.SUPPORTS:
					{
						buffer = Pool.NewStringBuilder();
						AddRule(new CSSSupportsRule());
						SwitchTo(CssState.InCondition);
						break;
					}
					case RuleNames.KEYFRAMES:
					{
						AddRule(new CSSKeyframesRule());
						SwitchTo(CssState.BeforeKeyframesName);
						break;
					}
					case RuleNames.DOCUMENT:
					{
						AddRule(new CSSDocumentRule());
						SwitchTo(CssState.BeforeDocumentFunction);
						break;
					}
					default: 
					{
						buffer = Pool.NewStringBuilder();
						AddRule(new CSSUnknownRule());
						SwitchTo(CssState.InUnknown);
						InUnknown(token);
						break;
					}
				}

				return true;
			}
			else if (token.Type == CssTokenType.CurlyBracketClose)
			{
				return CloseRule();
			}
			else
			{
				AddRule(new CSSStyleRule());
				SwitchTo(CssState.InSelector);
				InSelector(token);
				return true;
			}
		}

		/// <summary>
		/// State that is called once in the head of an unknown @ rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InUnknown(CssToken token)
		{
			switch (token.Type)
			{
				case CssTokenType.Semicolon:
					CurrentRuleAs<CSSUnknownRule>().SetInstruction(buffer.ToPool());
					SwitchTo(CssState.Data);
					return CloseRule();
				case CssTokenType.CurlyBracketOpen:
					CurrentRuleAs<CSSUnknownRule>().SetCondition(buffer.ToPool());
					SwitchTo(CssState.Data);
					break;
				default:
					buffer.Append(token.ToValue());
					break;
			}

			return true;
		}

		/// <summary>
		/// State that is called once we are in a CSS selector.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InSelector(CssToken token)
		{
			if (token.Type == CssTokenType.CurlyBracketOpen)
			{
				var rule = CurrentRule as ICssSelector;

				if (rule != null)
					rule.Selector = selector.Result;

				SwitchTo(CurrentRule is CSSStyleRule ? CssState.InDeclaration : CssState.Data);
			}
			else if (token.Type == CssTokenType.CurlyBracketClose)
				return false;
			else
				selector.Apply(token);

			return true;
		}

		/// <summary>
		/// Called before the property name has been detected.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InDeclaration(CssToken token)
		{
			if (token.Type == CssTokenType.CurlyBracketClose)
			{
                CloseProperty(value.ToValue());
				SwitchTo(CurrentRule is CSSKeyframeRule ? CssState.KeyframesData : CssState.Data);
				return CloseRule();
			}
			else if (token.Type == CssTokenType.Ident)
			{
                AddDeclaration(((CssKeywordToken)token).Data);
				SwitchTo(CssState.AfterProperty);
				return true;
			}

			return false;
		}

        // ----------------------------------------------
        //TODO:

        //UNITLESS in QUIRKSMODE:
        //  border-top-width
        //  border-right-width
        //  border-bottom-width
        //  border-left-width
        //  border-width
        //  bottom
        //  font-size
        //  height
        //  left
        //  letter-spacing
        //  margin
        //  margin-right
        //  margin-left
        //  margin-top
        //  margin-bottom
        //  padding
        //  padding-top
        //  padding-bottom
        //  padding-left
        //  padding-right
        //  right
        //  top
        //  width
        //  word-spacing

        //HASHLESS in QUIRKSMODE:
        //  background-color
        //  border-color
        //  border-top-color
        //  border-right-color
        //  border-bottom-color
        //  border-left-color
        //  color

        //
        // ----------------------------------------------

		/// <summary>
		/// After instruction rules a semicolon is required.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean AfterInstruction(CssToken token)
		{
			if (token.Type == CssTokenType.Semicolon)
			{
				SwitchTo(CssState.Data);
				return CloseRule();
			}

			return false;
		}

		/// <summary>
		/// In the condition text of a supports rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InCondition(CssToken token)
		{
			switch (token.Type)
			{
				case CssTokenType.CurlyBracketOpen:
					CurrentRuleAs<CSSSupportsRule>().ConditionText = buffer.ToPool();
					SwitchTo(CssState.Data);
					break;
				default:
					buffer.Append(token.ToValue());
					break;
			}

			return true;
		}

		/// <summary>
		/// Called before a prefix has been found for the namespace rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforePrefix(CssToken token)
		{
			if (token.Type == CssTokenType.Ident)
			{
				CurrentRuleAs<CSSNamespaceRule>().Prefix = ((CssKeywordToken)token).Data;
				SwitchTo(CssState.AfterNamespacePrefix);
				return true;
			}

			SwitchTo(CssState.AfterInstruction);
			return AfterInstruction(token);
		}

		/// <summary>
		/// Called before a namespace has been found for the namespace rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeNamespace(CssToken token)
		{
			SwitchTo(CssState.AfterInstruction);

			if (token.Type == CssTokenType.String)
			{
				CurrentRuleAs<CSSNamespaceRule>().NamespaceURI = ((CssStringToken)token).Data;
				return true;
			}

			return AfterInstruction(token);
		}

		/// <summary>
		/// Before a charset string has been found.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeCharset(CssToken token)
		{
			SwitchTo(CssState.AfterInstruction);

			if (token.Type == CssTokenType.String)
			{
				CurrentRuleAs<CSSCharsetRule>().Encoding = ((CssStringToken)token).Data;
				return true;
			}

			return AfterInstruction(token);
		}

		/// <summary>
		/// Before an URL has been found for the import rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeImport(CssToken token)
		{
			if (token.Type == CssTokenType.String || token.Type == CssTokenType.Url)
			{
				CurrentRuleAs<CSSImportRule>().Href = ((CssStringToken)token).Data;
				SwitchTo(CssState.InMediaList);
				return true;
			}

			SwitchTo(CssState.AfterInstruction);
			return false;
		}

		/// <summary>
		/// Called before the property separating colon has been seen.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean AfterProperty(CssToken token)
		{
			if (token.Type == CssTokenType.Colon)
			{
                value.Reset();
				SwitchTo(CssState.BeforeValue);
				return true;
			}
			else if (token.Type == CssTokenType.Semicolon || token.Type == CssTokenType.CurlyBracketClose)
				AfterValue(token);

			return false;
		}

		/// <summary>
		/// Called before any token in the value regime had been seen.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeValue(CssToken token)
		{
			if (token.Type == CssTokenType.Semicolon)
				SwitchTo(CssState.InDeclaration);
			else if (token.Type == CssTokenType.CurlyBracketClose)
				InDeclaration(token);
			else
			{
				SwitchTo(CssState.InSingleValue);
				return InSingleValue(token);
			}

			return false;
		}

		/// <summary>
		/// Called when a value has to be computed.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InSingleValue(CssToken token)
		{
			switch (token.Type)
			{
                case CssTokenType.Dimension: // e.g. "3px"
                case CssTokenType.Percentage: // e.g. "5%"
                    value.AddValue(ToUnit((CssUnitToken)token));
                    return true;
				case CssTokenType.Hash:// e.g. "#ABCDEF"
					return InSingleValueHexColor(((CssKeywordToken)token).Data);
				case CssTokenType.Delim:// e.g. "#"
					return InSingleValueDelim((CssDelimToken)token);
				case CssTokenType.Ident: // e.g. "auto"
					return InSingleValueIdent((CssKeywordToken)token);
				case CssTokenType.String:// e.g. "'i am a string'"
					value.AddValue(new CSSStringValue(((CssStringToken)token).Data));
                    return true;
				case CssTokenType.Url:// e.g. "url('this is a valid URL')"
                    value.AddValue(new CSSPrimitiveValue<Location>(new Location(((CssStringToken)token).Data)));
                    return true;
				case CssTokenType.Number: // e.g. "173"
					value.AddValue(ToNumber((CssNumberToken)token));
                    return true;
				case CssTokenType.Whitespace: // e.g. " "
					SwitchTo(CssState.InValueList);
					return true;
				case CssTokenType.Function: //e.g. rgba(...)
                    value.AddFunction(((CssKeywordToken)token).Data);
					SwitchTo(CssState.InFunction);
					return true;
				case CssTokenType.Comma: // e.g. ","
					SwitchTo(CssState.InValuePool);
					return true;
				case CssTokenType.Semicolon: // e.g. ";"
				case CssTokenType.CurlyBracketClose: // e.g. "}"
					return AfterValue(token);
				default:
					return false;
			}
		}

		/// <summary>
		/// Gathers a value inside a function.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InValueFunction(CssToken token)
		{
			switch (token.Type)
			{
				case CssTokenType.RoundBracketClose:
                    if (value.CloseFunction())
					    SwitchTo(CssState.InSingleValue);

					return true;

				case CssTokenType.Comma:
                    value.NextArgument();
					return true;

				default:
					return InSingleValue(token);
			}
		}

		/// <summary>
		/// Called when a new value is seen from the zero-POV (whitespace seen previously).
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InValueList(CssToken token)
		{
			if (token.Type == CssTokenType.Semicolon || token.Type == CssTokenType.CurlyBracketClose)
				AfterValue(token);
			else if (token.Type == CssTokenType.Comma)
				SwitchTo(CssState.InValuePool);
			else
			{
				SwitchTo(CssState.InSingleValue);
				return InSingleValue(token);
			}

			return true;
		}

		/// <summary>
		/// Called when a new value is seen from the zero-POV (comma seen previously).
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InValuePool(CssToken token)
		{
			if (token.Type == CssTokenType.Semicolon || token.Type == CssTokenType.CurlyBracketClose)
				AfterValue(token);
			else
            {
                value.NextArgument();
				SwitchTo(CssState.InSingleValue);
				return InSingleValue(token);
			}

			return false;
		}

		/// <summary>
		/// Called if a # sign has been found.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InHexValue(CssToken token)
		{
			switch (token.Type)
			{
				case CssTokenType.Number:
				case CssTokenType.Dimension:
				case CssTokenType.Ident:
					var rest = token.ToValue();

					if (buffer.Length + rest.Length <= 6)
					{
						buffer.Append(rest);
						return true;
					}

					break;
			}

			var s = buffer.ToPool();
			InSingleValueHexColor(buffer.ToString());
			SwitchTo(CssState.InSingleValue);
			return InSingleValue(token);
		}

		/// <summary>
		/// Called after the value is known to be over.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean AfterValue(CssToken token)
		{
			if (token.Type == CssTokenType.Semicolon)
			{
                CloseProperty(value.ToValue());
				SwitchTo(CssState.InDeclaration);
				return true;
			}
			else if (token.Type == CssTokenType.CurlyBracketClose)
				return InDeclaration(token);

			return false;
		}

		/// <summary>
		/// Called once an important instruction is expected.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean ValueImportant(CssToken token)
		{
			if (token.Type == CssTokenType.Ident && ((CssKeywordToken)token).Data == "important")
			{
                if (property != null)
                    property.Important = true;

				SwitchTo(CssState.AfterValue);
				return true;
			}

			return AfterValue(token);
		}

		/// <summary>
		/// Before the name of an @keyframes rule has been detected.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeKeyframesName(CssToken token)
		{
			SwitchTo(CssState.BeforeKeyframesData);

			if (token.Type == CssTokenType.Ident)
			{
				CurrentRuleAs<CSSKeyframesRule>().Name = ((CssKeywordToken)token).Data;
				return true;
			}
			else if (token.Type == CssTokenType.CurlyBracketOpen)
			{
				SwitchTo(CssState.KeyframesData);
			}

			return false;
		}

		/// <summary>
		/// Before the curly bracket of an @keyframes rule has been seen.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeKeyframesData(CssToken token)
		{
			if (token.Type == CssTokenType.CurlyBracketOpen)
			{
				SwitchTo(CssState.BeforeKeyframesData);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Called in the @keyframes rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean KeyframesData(CssToken token)
		{
			if (token.Type == CssTokenType.CurlyBracketClose)
			{
				SwitchTo(CssState.Data);
				return CloseRule();
			}
			else
			{
				buffer = Pool.NewStringBuilder();
				return InKeyframeText(token);
			}
		}

		/// <summary>
		/// Called in the text for a frame in the @keyframes rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InKeyframeText(CssToken token)
		{
			if (token.Type == CssTokenType.CurlyBracketOpen)
			{
				var frame = new CSSKeyframeRule();
				frame.KeyText = buffer.ToPool();
				AddRule(frame);
				SwitchTo(CssState.InDeclaration);
				return true;
			}
			else if (token.Type == CssTokenType.CurlyBracketClose)
			{
				buffer.ToPool();
				KeyframesData(token);
				return false;
			}

			buffer.Append(token.ToValue());
			return true;
		}

		/// <summary>
		/// Called before a document function has been found.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BeforeDocumentFunction(CssToken token)
		{
			switch (token.Type)
			{
				case CssTokenType.Url:
					CurrentRuleAs<CSSDocumentRule>().Conditions.Add(Tuple.Create(CSSDocumentRule.DocumentFunction.Url, ((CssStringToken)token).Data));
					break;
				case CssTokenType.UrlPrefix:
					CurrentRuleAs<CSSDocumentRule>().Conditions.Add(Tuple.Create(CSSDocumentRule.DocumentFunction.UrlPrefix, ((CssStringToken)token).Data));
					break;
				case CssTokenType.Domain:
					CurrentRuleAs<CSSDocumentRule>().Conditions.Add(Tuple.Create(CSSDocumentRule.DocumentFunction.Domain, ((CssStringToken)token).Data));
					break;
				case CssTokenType.Function:
					if (String.Compare(((CssKeywordToken)token).Data, "regexp", StringComparison.OrdinalIgnoreCase) == 0)
					{
						SwitchTo(CssState.InDocumentFunction);
						return true;
					}
					SwitchTo(CssState.AfterDocumentFunction);
					return false;
				default:
					SwitchTo(CssState.Data);
					return false;
			}

			SwitchTo(CssState.BetweenDocumentFunctions);
			return true;
		}

		/// <summary>
		/// Called before the argument of a document function has been found.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InDocumentFunction(CssToken token)
		{
			SwitchTo(CssState.AfterDocumentFunction);

			if (token.Type == CssTokenType.String)
			{
				CurrentRuleAs<CSSDocumentRule>().Conditions.Add(Tuple.Create(CSSDocumentRule.DocumentFunction.RegExp, ((CssStringToken)token).Data));
				return true;
			}

			return false;
		}

		/// <summary>
		/// Called after the arguments of a document function has been found.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean AfterDocumentFunction(CssToken token)
		{
			SwitchTo(CssState.BetweenDocumentFunctions);
			return token.Type == CssTokenType.RoundBracketClose;
		}

		/// <summary>
		/// Called after a function has been completed.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean BetweenDocumentFunctions(CssToken token)
		{
			if (token.Type == CssTokenType.Comma)
			{
				SwitchTo(CssState.BeforeDocumentFunction);
				return true;
			}
			else if (token.Type == CssTokenType.CurlyBracketOpen)
			{
				SwitchTo(CssState.Data);
				return true;
			}

			SwitchTo(CssState.Data);
			return false;
		}

		/// <summary>
		/// Before any medium has been found for the @media or @import rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InMediaList(CssToken token)
		{
			if (token.Type == CssTokenType.Semicolon)
			{
				CloseRule();
				SwitchTo(CssState.Data);
				return true;
			}

			buffer = Pool.NewStringBuilder();
			SwitchTo(CssState.InMediaValue);
			return InMediaValue(token);
		}

		/// <summary>
		/// Scans the current medium for the @media or @import rule.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean InMediaValue(CssToken token)
		{
			switch (token.Type)
			{
				case CssTokenType.CurlyBracketOpen:
				case CssTokenType.Semicolon:
				{
					var container = CurrentRule as ICssMedia;
					var s = buffer.ToPool();

					if (container != null)
						container.Media.AppendMedium(s);

					if (CurrentRule is CSSImportRule)
						return AfterInstruction(token);

					SwitchTo(CssState.Data);
					return token.Type == CssTokenType.CurlyBracketClose;
				}
				case CssTokenType.Comma:
				{
					var container = CurrentRule as ICssMedia;

					if (container != null)
						container.Media.AppendMedium(buffer.ToString());

					buffer.Clear();
					return true;
				}
				case CssTokenType.Whitespace:
				{
					buffer.Append(' ');
					return true;
				}
				default:
				{
					buffer.Append(token.ToValue());
					return true;
				}
			}
		}

		#endregion

		#region Substates

		/// <summary>
		/// Called in a value - a delimiter has been found.
		/// </summary>
		/// <param name="token">The current delim token.</param>
		/// <returns>The status.</returns>
		Boolean InSingleValueDelim(CssDelimToken token)
		{
			switch (token.Data)
			{
				case Specification.ExclamationMark:
					SwitchTo(CssState.ValueImportant);
					return true;
				case Specification.Num:
					buffer = Pool.NewStringBuilder();
					SwitchTo(CssState.InHexValue);
					return true;
				case Specification.Solidus:
                    value.InsertDelimiter();
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Called in a value - an identifier has been found.
		/// </summary>
		/// <param name="token">The current keyword token.</param>
		/// <returns>The status.</returns>
		Boolean InSingleValueIdent(CssKeywordToken token)
		{
			if (token.Data == "inherit")
			{
                CloseProperty(CSSValue.Inherit);
				SwitchTo(CssState.AfterValue);
				return true;
			}

            value.AddValue(new CSSIdentifierValue(token.Data));
            return true;
		}

		/// <summary>
		/// Called in a value - a hash (probably hex) value has been found.
		/// </summary>
        /// <param name="color">The value of the token.</param>
		/// <returns>The status.</returns>
		Boolean InSingleValueHexColor(String color)
		{
			Color colorValue;

            if (Color.TryFromHex(color, out colorValue))
            {
                value.AddValue(new CSSPrimitiveValue<Color>(colorValue));
                return true;
            }

			return false;
		}

		#endregion

		#region Rule management

		/// <summary>
		/// Closes a property.
		/// </summary>
		void CloseProperty(CSSValue value)
		{
            if (property != null)
            {
                property.Value = value;
                property = null;
            }
		}

		/// <summary>
		/// Closes the current rule (if any).
		/// </summary>
		/// <returns>The status.</returns>
		Boolean CloseRule()
		{
			if (open.Count > 0)
			{
				open.Pop();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Adds a new rule.
		/// </summary>
		/// <param name="rule">The new rule.</param>
		void AddRule(CSSRule rule)
		{
			rule.ParentStyleSheet = sheet;

			if (open.Count > 0)
			{
				var container = open.Peek() as ICssRules;

				if (container != null)
				{
					container.CssRules.List.Add(rule);
					rule.ParentRule = open.Peek();
				}
			}
			else
				sheet.CssRules.List.Add(rule);

			open.Push(rule);
		}

		/// <summary>
		/// Adds a declaration.
		/// </summary>
		/// <param name="propertyName">The name of the new property.</param>
		void AddDeclaration(String propertyName)
		{
			var rule = CurrentRule as IStyleDeclaration;

            if (rule != null)
            {
                this.property = CSSFactory.Create(propertyName, rule.Style);
                rule.Style.Set(property);
            }
		}

		#endregion

        #region Helpers

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

            return new CSSStringValue(token.ToValue());
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

		/// <summary>
		/// Gets the current rule casted to the given type.
		/// </summary>
		T CurrentRuleAs<T>()
			where T : CSSRule
		{
			if (open.Count > 0)
				return open.Peek() as T;

			return default(T);
		}

		/// <summary>
		/// Switches the current state to the given one.
		/// </summary>
		/// <param name="newState">The state to switch to.</param>
		void SwitchTo(CssState newState)
		{
			switch (newState)
			{
				case CssState.InSelector:
					tokenizer.IgnoreComments = true;
					tokenizer.IgnoreWhitespace = false;
					selector.Reset();
					selector.IgnoreErrors = skipExceptions;
					break;

				case CssState.InHexValue:
				case CssState.InUnknown:
				case CssState.InCondition:
				case CssState.InSingleValue:
				case CssState.InMediaValue:
					tokenizer.IgnoreComments = true;
					tokenizer.IgnoreWhitespace = false;
					break;

				default:
					tokenizer.IgnoreComments = true;
					tokenizer.IgnoreWhitespace = true;
					break;
			}

			state = newState;
		}

		/// <summary>
		/// The kernel that is pulling the tokens into the parser.
		/// </summary>
		void Kernel()
		{
			var tokens = tokenizer.Tokens;

			foreach (var token in tokens)
			{
				if (General(token) == false)
					RaiseErrorOccurred(ErrorCode.InputUnexpected);
			}

			if (property != null)
				General(CssSpecialCharacter.Semicolon);

			selector.ToPool();
		}

		/// <summary>
		/// Examines the token by using the current state.
		/// </summary>
		/// <param name="token">The current token.</param>
		/// <returns>The status.</returns>
		Boolean General(CssToken token)
		{
			switch (state)
			{
				case CssState.Data:
					return Data(token);
				case CssState.InSelector:
					return InSelector(token);
				case CssState.InDeclaration:
					return InDeclaration(token);
				case CssState.AfterProperty:
					return AfterProperty(token);
				case CssState.BeforeValue:
					return BeforeValue(token);
				case CssState.InValuePool:
					return InValuePool(token);
				case CssState.InValueList:
					return InValueList(token);
				case CssState.InSingleValue:
					return InSingleValue(token);
				case CssState.ValueImportant:
					return ValueImportant(token);
				case CssState.AfterValue:
					return AfterValue(token);
				case CssState.InMediaList:
					return InMediaList(token);
				case CssState.InMediaValue:
					return InMediaValue(token);
				case CssState.BeforeImport:
					return BeforeImport(token);
				case CssState.AfterInstruction:
					return AfterInstruction(token);
				case CssState.BeforeCharset:
					return BeforeCharset(token);
				case CssState.BeforeNamespacePrefix:
					return BeforePrefix(token);
				case CssState.AfterNamespacePrefix:
					return BeforeNamespace(token);
				case CssState.InCondition:
					return InCondition(token);
				case CssState.InUnknown:
					return InUnknown(token);
				case CssState.InKeyframeText:
					return InKeyframeText(token);
				case CssState.BeforeDocumentFunction:
					return BeforeDocumentFunction(token);
				case CssState.InDocumentFunction:
					return InDocumentFunction(token);
				case CssState.AfterDocumentFunction:
					return AfterDocumentFunction(token);
				case CssState.BetweenDocumentFunctions:
					return BetweenDocumentFunctions(token);
				case CssState.BeforeKeyframesName:
					return BeforeKeyframesName(token);
				case CssState.BeforeKeyframesData:
					return BeforeKeyframesData(token);
				case CssState.KeyframesData:
					return KeyframesData(token);
				case CssState.InHexValue:
					return InHexValue(token);
				case CssState.InFunction:
					return InValueFunction(token);
				default:
					return false;
			}
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
			var tokens = tokenizer.Tokens;
			var creator = Pool.NewSelectorConstructor();

			foreach (var token in tokens)
				creator.Apply(token);

			var result = creator.Result;
			creator.ToPool();
			return result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS stylesheet.
        /// </summary>
        /// <param name="stylesheet">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSStyleSheet object.</returns>
        public static CSSStyleSheet ParseStyleSheet(String stylesheet, IConfiguration configuration = null)
        {
            var parser = new CssParser(stylesheet, configuration ?? Configuration.Default);
            return parser.Result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSRule object.</returns>
        public static CSSRule ParseRule(String rule, IConfiguration configuration = null)
        {
            var parser = new CssParser(rule, configuration ?? Configuration.Default);
            parser.skipExceptions = false;
			parser.Parse();

			if (parser.sheet.CssRules.Length > 0)
				return parser.sheet.CssRules[0];

			return null;
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
        /// <param name="declarations">The string to parse.</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSProperty object.</returns>
        public static CSSProperty ParseDeclaration(String declarations, IConfiguration configuration = null)
        {
            var parser = new CssParser(declarations, configuration ?? Configuration.Default);
            var rule = new CSSStyleRule();
            parser.AddRule(rule);
			parser.state = CssState.InDeclaration;
            parser.skipExceptions = false;
			parser.Parse();
            return rule.Style.Length != 0 ? rule.Style.Get(0) : null;
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
			var property = new CSSProperty(String.Empty);
			parser.property = property;
            parser.skipExceptions = false;
			parser.state = CssState.BeforeValue;
			parser.Parse();
            return property.Value;
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
			var property = new CSSProperty(String.Empty);
			parser.property = property;
			parser.skipExceptions = false;
			parser.state = CssState.InValueList;
			parser.Parse();

            if (!property.HasValue)
                return new CSSValueList();

            var list = property.Value as CSSValueList ?? new CSSValueList(property.Value);

            for (var i = 0; i < list.Length; i++)
            {
                if (list[i] == CSSValue.Separator)
                {
                    for (var j = list.Length - 1; j >= i; j--)
                        list.Remove(list[j]);

                    break;
                }
            }

            return list;
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
			var property = new CSSProperty(String.Empty);
            var result = new List<CSSValueList>();
			parser.property = property;
			parser.skipExceptions = false;
			parser.state = CssState.InValuePool;
            parser.Parse();

            if (property.HasValue)
            {
                var list = property.Value as CSSValueList ?? new CSSValueList(property.Value);
                var temp = new CSSValueList();

                foreach (var entry in list)
                {
                    if (entry == CSSValue.Separator)
                    {
                        if (temp.Length > 0)
                            result.Add(temp);

                        temp = new CSSValueList();
                    }
                    else
                        temp.Add(entry);
                }

                if (temp.Length > 0)
                    result.Add(temp);

                temp = null;
            }

            return result;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <param name="configuration">Optional: The configuration to use for construction.</param>
        /// <returns>The CSSKeyframeRule object.</returns>
        internal static CSSKeyframeRule ParseKeyframeRule(String rule, IConfiguration configuration = null)
        {
            var parser = new CssParser(rule, configuration);
			var keyframe = new CSSKeyframeRule();
			parser.AddRule(keyframe);
            parser.skipExceptions = false;
			parser.state = CssState.InKeyframeText;
			parser.Parse();
            return keyframe;
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
			parser.skipExceptions = false;

			if (list.ParentRule != null)
				parser.AddRule(list.ParentRule);
			else
				parser.AddRule(new CSSStyleRule(list));

			parser.state = CssState.InDeclaration;
			parser.Parse();
		}

        #endregion

		#region State Enumeration

		/// <summary>
		/// The enumeration with possible state values.
		/// </summary>
		enum CssState
		{
            /// <summary>
            /// The initial state.
            /// </summary>
			Data,
            /// <summary>
            /// In some selector.
            /// </summary>
			InSelector,
            /// <summary>
            /// In a declaration.
            /// </summary>
			InDeclaration,
            /// <summary>
            /// After a property.
            /// </summary>
			AfterProperty,
            /// <summary>
            /// Before the value of a property.
            /// </summary>
			BeforeValue,
            /// <summary>
            /// In a value pool.
            /// </summary>
			InValuePool,
            /// <summary>
            /// In a value list.
            /// </summary>
			InValueList,
            /// <summary>
            /// In a single value.
            /// </summary>
			InSingleValue,
            /// <summary>
            /// In the listing of media.
            /// </summary>
			InMediaList,
            /// <summary>
            /// In a specific media value.
            /// </summary>
			InMediaValue,
            /// <summary>
            /// Before the value of the import.
            /// </summary>
			BeforeImport,
            /// <summary>
            /// Before the charset.
            /// </summary>
			BeforeCharset,
            /// <summary>
            /// Before the prefix of a namespace has been declared.
            /// </summary>
            BeforeNamespacePrefix,
            /// <summary>
            /// After the prefix of a namespace has been declared.
            /// </summary>
            AfterNamespacePrefix,
            /// <summary>
            /// After an instruction command.
            /// </summary>
			AfterInstruction,
            /// <summary>
            /// In a CSS3 condition.
            /// </summary>
			InCondition,
            /// <summary>
            /// Keyframes rule - before the name.
            /// </summary>
            BeforeKeyframesName,
            /// <summary>
            /// Keyframes rule - after the name.
            /// </summary>
            BeforeKeyframesData,
            /// <summary>
            /// Keyframes rule - in the keyframes data.
            /// </summary>
            KeyframesData,
            /// <summary>
            /// Keyframes rule - in the keyframes text.
            /// </summary>
            InKeyframeText,
            /// <summary>
            /// Before a CSS3 document function.
            /// </summary>
            BeforeDocumentFunction,
            /// <summary>
            /// In a CSS3 document function.
            /// </summary>
            InDocumentFunction,
            /// <summary>
            /// After a CSS3 document function.
            /// </summary>
            AfterDocumentFunction,
            /// <summary>
            /// Between CSS3 document functions.
            /// </summary>
            BetweenDocumentFunctions,
            /// <summary>
            /// In an unknown rule.
            /// </summary>
			InUnknown,
            /// <summary>
            /// Triggered by the ! in a CSS value.
            /// </summary>
			ValueImportant,
            /// <summary>
            /// After a value (before important or ending).
            /// </summary>
			AfterValue,
            /// <summary>
            /// In a hex value - e.g. the hash has been found.
            /// </summary>
			InHexValue,
            /// <summary>
            /// In a CSS value function.
            /// </summary>
			InFunction
        }

        #endregion

		#region Event-Helpers

		/// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        void RaiseErrorOccurred(ErrorCode code)
        {
            value.IsFaulted = true;

            if (ParseError != null)
            {
                var pck = new ParseErrorEventArgs((Int32)code, Errors.GetError(code));
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ParseError(this, pck);
            }
        }

        #endregion
    }
}
