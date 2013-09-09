using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using AngleSharp.DOM;
using AngleSharp.Events;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Collections;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// The CSS parser.
    /// See http://dev.w3.org/csswg/css-syntax/#parsing for more details.
    /// </summary>
    //[DebuggerStepThrough]
    public sealed class CssParser : IParser
    {
		#region Members
		
		CssSelectorConstructor selector;
		Stack<FunctionBuffer> function;
		Boolean skipExceptions;
        CssTokenizer tokenizer;
		Boolean fraction;
        Boolean started;
        Boolean quirks;
        CSSStyleSheet sheet;
        Stack<CSSRule> open;
		CSSProperty property;
		StringBuilder buffer;
		CssState state;
		Object sync;
		Task task;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event ParseErrorEventHandler ErrorOccurred;

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
			selector = Pool.NewSelectorConstructor();
            sync = new Object();
            skipExceptions = true;
            tokenizer = new CssTokenizer(source);

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ErrorOccurred != null)
                    ErrorOccurred(this, ev);
            };

            started = false;
			function = new Stack<FunctionBuffer>();
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
        /// Gets or sets if the quirks-mode is activated.
        /// </summary>
        public Boolean IsQuirksMode
        {
            get { return quirks; }
            set { quirks = value; }
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
                    task = Task.Run(() => Kernel());
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
				var rule = CurrentRule as ISelector;

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
				SwitchTo(CurrentRule is CSSKeyframeRule ? CssState.KeyframesData : CssState.Data);
				return CloseRule();
			}
			else if (token.Type == CssTokenType.Ident)
			{
				AddDeclaration(CSSProperty.Create(((CssKeywordToken)token).Data));
				SwitchTo(CssState.AfterProperty);
				return true;
			}

			return false;
		}

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
				fraction = false;
				SwitchTo(CssState.BeforeValue);
				return true;
			}
			else if (token.Type == CssTokenType.Semicolon)
				SwitchTo(CssState.InDeclaration);
			else if (token.Type == CssTokenType.CurlyBracketClose)
				InDeclaration(token);

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
					return AddValue(new CSSPrimitiveValue(((CssUnitToken)token).Unit, ((CssUnitToken)token).Data));
				case CssTokenType.Hash:// e.g. "#ABCDEF"
					return InSingleValueHexColor(((CssKeywordToken)token).Data);
				case CssTokenType.Delim:// e.g. "#"
					return InSingleValueDelim((CssDelimToken)token);
				case CssTokenType.Ident: // e.g. "auto"
					return InSingleValueIdent((CssKeywordToken)token);
				case CssTokenType.String:// e.g. "'i am a string'"
					return AddValue(new CSSPrimitiveValue(CssUnit.String, ((CssStringToken)token).Data));
				case CssTokenType.Url:// e.g. "url('this is a valid URL')"
					return AddValue(new CSSPrimitiveValue(CssUnit.Uri, ((CssStringToken)token).Data));
				case CssTokenType.Percentage: // e.g. "5%"
					return AddValue(new CSSPrimitiveValue(CssUnit.Percentage, ((CssUnitToken)token).Data));
				case CssTokenType.Number: // e.g. "173"
					return AddValue(new CSSPrimitiveValue(CssUnit.Number, ((CssNumberToken)token).Data));
				case CssTokenType.Whitespace: // e.g. " "
					SwitchTo(CssState.InValueList);
					return true;
				case CssTokenType.Function: //e.g. rgba(...)
					function.Push(new FunctionBuffer(((CssKeywordToken)token).Data));
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
					return AddValue(function.Pop().Done());
				case CssTokenType.Comma:
					function.Peek().Include();
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
			if (token.Type == CssTokenType.Semicolon)
				SwitchTo(CssState.InDeclaration);
			else if (token.Type == CssTokenType.CurlyBracketClose)
				InDeclaration(token);
			else if (token.Type == CssTokenType.Comma)
				SwitchTo(CssState.InValuePool);
			else
			{
				if (property.Value is CSSValuePool)
				{
					var pool = (CSSValuePool)property.Value;
					var item = pool.List[pool.List.Count - 1];

					if (!(item is CSSValueList))
						pool.List[pool.List.Count - 1] = new CSSValueList(item);
				}
				else if (!(property.Value is CSSValueList))
					property.Value = new CSSValueList(property.Value);

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
			if (token.Type == CssTokenType.Semicolon)
				SwitchTo(CssState.InDeclaration);
			else if (token.Type == CssTokenType.CurlyBracketClose)
				InDeclaration(token);
			else
			{
				if (!(property.Value is CSSValuePool))
					property.Value = new CSSValuePool(property.Value);

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
				SwitchTo(CssState.AfterValue);
				property.Important = true;
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
					var container = CurrentRule as IMedia;
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
					var container = CurrentRule as IMedia;

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
				case Specification.EM:
					SwitchTo(CssState.ValueImportant);
					return true;
				case Specification.NUM:
					buffer = Pool.NewStringBuilder();
					SwitchTo(CssState.InHexValue);
					return true;
				case Specification.SOLIDUS:
					fraction = true;
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
				property.Value = CSSValue.Inherit;
				SwitchTo(CssState.AfterValue);
				return true;
			}

			return AddValue(new CSSPrimitiveValue(CssUnit.Ident, token.Data));
		}

		/// <summary>
		/// Called in a value - a hash (probably hex) value has been found.
		/// </summary>
		/// <param name="token">The value of the token.</param>
		/// <returns>The status.</returns>
		Boolean InSingleValueHexColor(String color)
		{
			CSSColor value;

			if (CSSColor.TryFromHex(color, out value))
				return AddValue(new CSSPrimitiveValue(value));

			return false;
		}

		#endregion

		#region Rule management

		/// <summary>
		/// Adds the new value to the current value (or replaces it).
		/// </summary>
		/// <param name="value">The value to add.</param>
		/// <returns>The status.</returns>
		Boolean AddValue(CSSValue value)
		{
			if (function.Count > 0)
				function.Peek().Arguments.Add(value);
			else if (property.Value is CSSValueList)
				((CSSValueList)property.Value).List.Add(value);
			else if (property.Value is CSSValuePool)
				((CSSValuePool)property.Value).List.Add(value);
			else if (property.Value == CSSValue.Inherit)
				property.Value = value;
			else
				return false;

			return true;
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
				var container = open.Peek() as IRules;

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
		/// <param name="property">The new property.</param>
		void AddDeclaration(CSSProperty property)
		{
			this.property = property;
			var rule = CurrentRule as IStyleDeclaration;

			if (rule != null)
				rule.Style.List.Add(property);
		}

		#endregion

		#region Helpers

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
			var status = false;

			foreach (var token in tokens)
			{
				status = false;

				switch (state)
				{
					case CssState.Data:
						status = Data(token);
						break;
					case CssState.InSelector:
						status = InSelector(token);
						break;
					case CssState.InDeclaration:
						status = InDeclaration(token);
						break;
					case CssState.AfterProperty:
						status = AfterProperty(token);
						break;
					case CssState.BeforeValue:
						status = BeforeValue(token);
						break;
					case CssState.InValuePool:
						status = InValuePool(token);
						break;
					case CssState.InValueList:
						status = InValueList(token);
						break;
					case CssState.InSingleValue:
						status = InSingleValue(token);
						break;
					case CssState.ValueImportant:
						status = ValueImportant(token);
						break;
					case CssState.AfterValue:
						status = AfterValue(token);
						break;
					case CssState.InMediaList:
						status = InMediaList(token);
						break;
					case CssState.InMediaValue:
						status = InMediaValue(token);
						break;
					case CssState.BeforeImport:
						status = BeforeImport(token);
						break;
					case CssState.AfterInstruction:
						status = AfterInstruction(token);
						break;
					case CssState.BeforeCharset:
						status = BeforeCharset(token);
						break;
					case CssState.BeforeNamespacePrefix:
						status = BeforePrefix(token);
						break;
					case CssState.AfterNamespacePrefix:
						status = BeforeNamespace(token);
						break;
					case CssState.InCondition:
						status = InCondition(token);
						break;
					case CssState.InUnknown:
						status = InUnknown(token);
						break;
					case CssState.InKeyframeText:
						status = InKeyframeText(token);
						break;
					case CssState.BeforeDocumentFunction:
						status = BeforeDocumentFunction(token);
						break;
					case CssState.InDocumentFunction:
						status = InDocumentFunction(token);
						break;
					case CssState.AfterDocumentFunction:
						status = AfterDocumentFunction(token);
						break;
					case CssState.BetweenDocumentFunctions:
						status = BetweenDocumentFunctions(token);
						break;
					case CssState.BeforeKeyframesName:
						status = BeforeKeyframesName(token);
						break;
					case CssState.BeforeKeyframesData:
						status = BeforeKeyframesData(token);
						break;
					case CssState.KeyframesData:
						status = KeyframesData(token);
						break;
					case CssState.InHexValue:
						status = InHexValue(token);
						break;
					case CssState.InFunction:
						status = InValueFunction(token);
						break;
				}

				if (!status)
					RaiseErrorOccurred(ErrorCode.InputUnexpected);
			}

			if (state == CssState.InHexValue)
				InHexValue(CssToken.Delim(Specification.SC));

			selector.ToPool();
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
			var tokenizer = new CssTokenizer(new SourceManager(selector));
			var tokens = tokenizer.Tokens;
			var selctor = Pool.NewSelectorConstructor();

			foreach (var token in tokens)
				selctor.Apply(token);

			var result = selctor.Result;
			selctor.ToPool();
			return result;
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
            parser.skipExceptions = false;
            parser.IsQuirksMode = quirksMode;
			parser.Parse();

			if(parser.sheet.CssRules.Length > 0)
				return parser.sheet.CssRules[0];

			return null;
        }

        /// <summary>
        /// Takes a string and transforms it into CSS declarations.
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration ParseDeclarations(String declarations, Boolean quirksMode = false)
        {
            var decl = new CSSStyleDeclaration();
            AppendDeclarations(decl, declarations, quirksMode);
            return decl;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS declaration (CSS property).
        /// </summary>
        /// <param name="declarations">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSProperty object.</returns>
        public static CSSProperty ParseDeclaration(String declarations, Boolean quirksMode = false)
        {
            var parser = new CssParser(declarations);
			parser.state = CssState.InDeclaration;
            parser.IsQuirksMode = quirksMode;
            parser.skipExceptions = false;
			parser.Parse();
            return parser.property;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValue object.</returns>
        public static CSSValue ParseValue(String source, Boolean quirksMode = false)
        {
            var parser = new CssParser(source);
			var property = new CSSProperty(String.Empty);
			parser.property = property;
            parser.IsQuirksMode = quirksMode;
            parser.skipExceptions = false;
			parser.state = CssState.BeforeValue;
			parser.Parse();
            return property.Value;
        }

        /// <summary>
        /// Takes a string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValueList object.</returns>
        internal static CSSValueList ParseValueList(String source, Boolean quirksMode = false)
        {
			var parser = new CssParser(source);
			var list = new CSSValueList();
			var property = new CSSProperty(String.Empty);
			property.Value = list;
			parser.property = property;
            parser.IsQuirksMode = quirksMode;
			parser.skipExceptions = false;
			parser.state = CssState.InValueList;
			parser.Parse();
            return list;
        }

        /// <summary>
        /// Takes a comma separated string and transforms it into a list of CSS values.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSValueList object.</returns>
        internal static CSSValuePool ParseMultipleValues(String source, Boolean quirksMode = false)
        {
			var parser = new CssParser(source);
			var pool = new CSSValuePool();
			var property = new CSSProperty(String.Empty);
			property.Value = pool;
			parser.property = property;
            parser.IsQuirksMode = quirksMode;
			parser.skipExceptions = false;
			parser.state = CssState.InValuePool;
			parser.Parse();
			return pool;
        }

        /// <summary>
        /// Takes a string and transforms it into a CSS keyframe rule.
        /// </summary>
        /// <param name="rule">The string to parse.</param>
        /// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
        /// <returns>The CSSKeyframeRule object.</returns>
        internal static CSSKeyframeRule ParseKeyframeRule(String rule, Boolean quirksMode = false)
        {
            var parser = new CssParser(rule);
			var keyframe = new CSSKeyframeRule();
			parser.AddRule(keyframe);
            parser.IsQuirksMode = quirksMode;
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
		/// <param name="quirksMode">Optional: The status of the quirks mode flag (usually not set).</param>
		internal static void AppendDeclarations(CSSStyleDeclaration list, String declarations, Boolean quirksMode = false)
		{
			var parser = new CssParser(declarations);
			parser.IsQuirksMode = quirksMode;
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
			Data,
			InSelector,
			InDeclaration,
			AfterProperty,
			BeforeValue,
			InValuePool,
			InValueList,
			InSingleValue,
			InMediaList,
			InMediaValue,
			BeforeImport,
			BeforeCharset,
			BeforeNamespacePrefix,
			AfterNamespacePrefix,
			AfterInstruction,
			InCondition,
			BeforeKeyframesName,
			BeforeKeyframesData,
			KeyframesData,
			InKeyframeText,
			BeforeDocumentFunction,
			InDocumentFunction,
			AfterDocumentFunction,
			BetweenDocumentFunctions,
			InUnknown,
			ValueImportant,
			AfterValue,
			InHexValue,
			InFunction
		}

		/// <summary>
		/// A buffer for functions.
		/// </summary>
		class FunctionBuffer
		{
			#region Members

			String name;
			List<CSSValue> arguments;
			CSSValue value;

			#endregion

			#region ctor

			internal FunctionBuffer(String name)
			{
				this.arguments = new List<CSSValue>();
				this.name = name;
			}

			#endregion

			#region Properties

			public List<CSSValue> Arguments
			{
				get { return arguments; }
			}

			public CSSValue Value
			{
				get { return value; }
				set { this.value = value; }
			}

			#endregion

			#region Methods

			public void Include()
			{
				if (value != null)
					arguments.Add(value);

				value = null;
			}

			public CSSValue Done()
			{
				Include();
				return CSSFunction.Create(name, arguments);
			}

			#endregion
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
                var pck = new ParseErrorEventArgs((Int32)code, Errors.GetError(code));
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
