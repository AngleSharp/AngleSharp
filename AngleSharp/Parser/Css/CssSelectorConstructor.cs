namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Class for construction for CSS selectors as specified in
    /// http://www.w3.org/html/wg/drafts/html/master/selectors.html.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssSelectorConstructor
    {
        #region Fields

        static readonly Dictionary<String, Func<FunctionState>> pseudoClassFunctions = new Dictionary<String, Func<FunctionState>>(StringComparer.OrdinalIgnoreCase);

        readonly Stack<CssCombinator> combinators;
        readonly List<CssToken> tokens;

		State state;
        ISelector temp;
		ListSelector group;
		ComplexSelector complex;
		String attrName;
		String attrValue;
		String attrOp;
        String attrNs;
        Boolean valid;
        Boolean ready;

        #endregion

        #region Initialization

        static CssSelectorConstructor()
        {
            pseudoClassFunctions.Add(PseudoClassNames.NthChild, () => new ChildFunctionState<FirstChildSelector>(withOptionalSelector: true));
            pseudoClassFunctions.Add(PseudoClassNames.NthLastChild, () => new ChildFunctionState<LastChildSelector>(withOptionalSelector: true));
            pseudoClassFunctions.Add(PseudoClassNames.NthOfType, () => new ChildFunctionState<FirstTypeSelector>(withOptionalSelector: false));
            pseudoClassFunctions.Add(PseudoClassNames.NthLastOfType, () => new ChildFunctionState<LastTypeSelector>(withOptionalSelector: false));
            pseudoClassFunctions.Add(PseudoClassNames.NthColumn, () => new ChildFunctionState<FirstColumnSelector>(withOptionalSelector: false));
            pseudoClassFunctions.Add(PseudoClassNames.NthLastColumn, () => new ChildFunctionState<LastColumnSelector>(withOptionalSelector: false));
            pseudoClassFunctions.Add(PseudoClassNames.Not, () => new NotFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Dir, () => new DirFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Lang, () => new LangFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Contains, () => new ContainsFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Has, () => new HasFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Matches, () => new MatchesFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.HostContext, () => new HostContextFunctionState());
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new constructor object.
        /// </summary>
        public CssSelectorConstructor()
        {
            combinators = new Stack<CssCombinator>();
            tokens = new List<CssToken>();
			Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the stored selector is valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return valid && ready; }
        }

        /// <summary>
        /// Gets if the stored selector is nested below another selector.
        /// </summary>
        public Boolean IsNested
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the currently formed selector.
        /// </summary>
        public ISelector GetResult()
        {
            if (!IsValid)
                return new UnknownSelector(Serialize());

            if (complex != null)
            {
                complex.ConcludeSelector(temp);
                temp = complex;
                complex = null;
            }

            if (group == null || group.Length == 0)
                return temp ?? SimpleSelector.All;
            else if (temp == null && group.Length == 1)
                return group[0];

            if (temp != null)
            {
                group.Add(temp);
                temp = null;
            }

            return group;
        }

        /// <summary>
        /// Tries to serialize the current selector.
        /// </summary>
        /// <returns>The approximate selector text.</returns>
        String Serialize()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var token in tokens)
                sb.Append(token.ToValue());

            return sb.ToPool();
        }

        /// <summary>
        /// Picks a simple selector from the stream of tokens.
        /// </summary>
        /// <param name="token">The stream of tokens to consider.</param>
        public void Apply(CssToken token)
        {
            if (token.Type == CssTokenType.Comment)
                return;

            tokens.Add(token);

			switch (state)
			{
				case State.Data:
					OnData(token);
                    break;
				case State.Class:
					OnClass(token);
                    break;
                case State.Attribute:
					OnAttribute(token);
                    break;
                case State.AttributeOperator:
					OnAttributeOperator(token);
                    break;
                case State.AttributeValue:
					OnAttributeValue(token);
                    break;
                case State.AttributeEnd:
					OnAttributeEnd(token);
                    break;
                case State.PseudoClass:
					OnPseudoClass(token);
                    break;
                case State.PseudoElement:
					OnPseudoElement(token);
                    break;
                default:
                    valid = false;
                    break;
            }
        }

		/// <summary>
		/// Resets the current state.
		/// </summary>
		/// <returns>The current instance.</returns>
		public CssSelectorConstructor Reset()
		{
			attrName = null;
			attrValue = null;
            attrNs = null;
			attrOp = String.Empty;
			state = State.Data;
			combinators.Clear();
            tokens.Clear();
			temp = null;
			group = null;
			complex = null;
            valid = true;
            IsNested = false;
            ready = true;
			return this;
		}

		#endregion

		#region States

		/// <summary>
		/// General state.
		/// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		void OnData(CssToken token)
		{
			switch (token.Type)
			{
				//Begin of attribute [A]
				case CssTokenType.SquareBracketOpen:
					attrName = null;
					attrValue = null;
					attrOp = String.Empty;
                    attrNs = null;
					state = State.Attribute;
                    ready = false;
					break;

				//Begin of Pseudo :P
				case CssTokenType.Colon:
					state = State.PseudoClass;
                    ready = false;
                    break;

                //Begin of ID #I
                case CssTokenType.Hash:
					Insert(SimpleSelector.Id(token.Data));
                    ready = true;
                    break;

                //Begin of Type E
                case CssTokenType.Ident:
					Insert(SimpleSelector.Type(token.Data));
                    ready = true;
                    break;

                //Whitespace could be significant
                case CssTokenType.Whitespace:
					Insert(CssCombinator.Descendent);
                    break;

                //Various
                case CssTokenType.Delim:
					OnDelim(token);
                    break;

				case CssTokenType.Comma:
					InsertOr();
                    ready = false;
                    break;

                default:
                    valid = false;
                    break;
            }
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnAttribute(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;
            
			if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String)
            {
                state = State.AttributeOperator;
                attrName = token.Data;
            }
            else if (token.Type == CssTokenType.Delim && token.ToValue() == "|")
            {
                state = State.Attribute;
                attrNs = String.Empty;
            }
            else if (token.Type == CssTokenType.Delim && token.ToValue() == "*")
            {
                state = State.AttributeOperator;
                attrName = token.ToValue();
            }
            else
            {
                state = State.Data;
                valid = false;
            }
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnAttributeOperator(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;

			if (token.Type == CssTokenType.SquareBracketClose)
            {
                state = State.AttributeValue;
                OnAttributeEnd(token);
            }
            else if (token.Type == CssTokenType.Match || token.Type == CssTokenType.Delim)
            {
                state = State.AttributeValue;
                attrOp = token.ToValue();

                if (attrOp == "|")
                {
                    attrNs = attrName;
                    attrName = null;
                    attrOp = String.Empty;
                    state = State.Attribute;
                }
            }
            else
            {
                state = State.AttributeEnd;
                valid = false;
            }
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnAttributeValue(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;
            
			if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String || token.Type == CssTokenType.Number)
            {
                state = State.AttributeEnd;
                attrValue = token.Data;
            }
            else
            {
                state = State.Data;
                valid = false;
            }
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnAttributeEnd(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;

			state = State.Data;
            ready = true;

            if (token.Type == CssTokenType.SquareBracketClose)
            {
                var selector = CreateAttrSelector();
                Insert(selector);
            }
            else            
                valid = false;
        }

        /// <summary>
        /// Creates an attribute selector using the current state.
        /// </summary>
        /// <returns>The created selector.</returns>
        SimpleSelector CreateAttrSelector()
        {
            switch (attrOp)
            {
                case "=":
                    return SimpleSelector.AttrMatch(attrName, attrValue, attrNs);
                case "~=":
                    return SimpleSelector.AttrList(attrName, attrValue, attrNs);
                case "|=":
                    return SimpleSelector.AttrHyphen(attrName, attrValue, attrNs);
                case "^=":
                    return SimpleSelector.AttrBegins(attrName, attrValue, attrNs);
                case "$=":
                    return SimpleSelector.AttrEnds(attrName, attrValue, attrNs);
                case "*=":
                    return SimpleSelector.AttrContains(attrName, attrValue, attrNs);
                case "!=":
                    return SimpleSelector.AttrNotMatch(attrName, attrValue, attrNs);
                default:
                    return SimpleSelector.AttrAvailable(attrName, attrNs);
            }
        }

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnPseudoClass(CssToken token)
		{
			state = State.Data;
            ready = true;

            if (token.Type == CssTokenType.Colon)
            {
                state = State.PseudoElement;
                return;
            }
            else if (token.Type == CssTokenType.Function)
            {
                var sel = GetPseudoFunction(token as CssFunctionToken);

                if (sel != null)
                {
                    Insert(sel);
                    return;
                }
            }
            else if (token.Type == CssTokenType.Ident)
            {
                var sel = Factory.PseudoClassSelector.Create(token.Data);

                if (sel != null)
                {
                    Insert(sel);
                    return;
                }
            }
            
            valid = false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnPseudoElement(CssToken token)
        {
            state = State.Data;
            ready = true;

            if (token.Type == CssTokenType.Ident)
            {
                var sel = Factory.PseudoElementSelector.Create(token.Data);

                if (sel != null)
                {
                    if (IsNested)
                        valid = false;

                    Insert(sel);
                    return;
                }
            }

            valid = false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnClass(CssToken token)
		{
			state = State.Data;
            ready = true;

            if (token.Type == CssTokenType.Ident)
                Insert(SimpleSelector.Class(token.Data));
            else
                valid = false;
		}

		#endregion

		#region Insertations

		/// <summary>
        /// Inserts a comma.
        /// </summary>
        void InsertOr()
        {
            if (temp != null)
            {
                if (group == null)
                    group = new ListSelector();

                if (complex != null)
                {
                    complex.ConcludeSelector(temp);
                    group.Add(complex);
                    complex = null;
                }
                else
                    group.Add(temp);

                temp = null;
            }
        }

        /// <summary>
        /// Inserts the given selector.
        /// </summary>
        /// <param name="selector">The selector to insert.</param>
        void Insert(ISelector selector)
        {
            if (temp != null)
            {
                if (combinators.Count == 0)
                {
                    var compound = temp as CompoundSelector;

                    if (compound == null)
                    {
                        compound = new CompoundSelector();
                        compound.Add(temp);
                    }

                    compound.Add(selector);
                    temp = compound;
                }
                else
                {
                    if (complex == null)
                        complex = new ComplexSelector();

                    var combinator = GetCombinator();
                    complex.AppendSelector(temp, combinator);
                    temp = selector;
                }
            }
            else
            {
                combinators.Clear();
                temp = selector;
            }
        }

        CssCombinator GetCombinator()
        {
            //Remove all trailing whitespaces
            while (combinators.Count > 1 && combinators.Peek() == CssCombinator.Descendent)
                combinators.Pop();

            if (combinators.Count > 1)
            {
                var last = combinators.Pop();
                var previous = combinators.Pop();

                //Care about combinator combinations, such as >>, >>> and ||
                if (last == CssCombinator.Child && previous == CssCombinator.Child)
                {
                    if (combinators.Count == 0 || combinators.Peek() != CssCombinator.Child)
                        last = CssCombinator.Descendent;
                    else if (combinators.Pop() == CssCombinator.Child)
                        last = CssCombinator.Deep;
                }
                else if (last == CssCombinator.Namespace && previous == CssCombinator.Namespace)
                {
                    last = CssCombinator.Column;
                }
                else
                {
                    combinators.Push(previous);
                }

                //Remove all leading whitespaces, invalid if mixed
                while (combinators.Count > 0)
                    valid = combinators.Pop() == CssCombinator.Descendent && valid;

                return last;
            }

            return combinators.Pop();
        }

        /// <summary>
        /// Inserts the given combinator.
        /// </summary>
        /// <param name="cssCombinator">The combinator to insert.</param>
        void Insert(CssCombinator cssCombinator)
        {
            combinators.Push(cssCombinator);
        }

		#endregion

		#region Substates

		/// <summary>
		/// Invoked once a delimiter has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		void OnDelim(CssToken token)
		{
			switch (token.Data[0])
			{
				case Symbols.Comma:
					InsertOr();
                    ready = false;
                    break;

                case Symbols.GreaterThan:
					Insert(CssCombinator.Child);
                    ready = false;
                    break;

                case Symbols.Plus:
					Insert(CssCombinator.AdjacentSibling);
                    ready = false;
                    break;

                case Symbols.Tilde:
					Insert(CssCombinator.Sibling);
                    ready = false;
                    break;

                case Symbols.Asterisk:
					Insert(SimpleSelector.All);
                    ready = true;
                    break;

                case Symbols.Dot:
					state = State.Class;
                    ready = false;
                    break;

                case Symbols.Pipe:
                    if (combinators.Count > 0 && combinators.Peek() == CssCombinator.Descendent)
                        Insert(SimpleSelector.Type(String.Empty));

                    Insert(CssCombinator.Namespace);
                    ready = false;
                    break;

                default:
                    valid = false;
                    break;
            }
		}

        /// <summary>
        /// Invoked once a colon with an identifier has been found in the token enumerator.
        /// </summary>
        /// <returns>The created function state.</returns>
        ISelector GetPseudoFunction(CssFunctionToken arguments)
        {
            Func<FunctionState> creator;

            if (pseudoClassFunctions.TryGetValue(arguments.Data, out creator))
            {
                var function = creator();
                ready = false;

                foreach (var token in arguments)
                {
                    if (function.Finished(token))
                    {
                        var sel = function.Produce();

                        if (IsNested && function is NotFunctionState)
                            sel = null;

                        ready = true;
                        return sel;
                    }
                }
            }

            return null;
        }

        #endregion

		#region State-Machine

		/// <summary>
		/// The various parsing states.
		/// </summary>
		enum State
		{
			Data,
			Attribute,
			AttributeOperator,
			AttributeValue,
			AttributeEnd,
			Class,
			PseudoClass,
			PseudoElement
		}

        #endregion

        #region Function States

        abstract class FunctionState
        {
            public Boolean Finished(CssToken token)
            {
                return OnToken(token);
            }

            public abstract ISelector Produce();

            protected abstract Boolean OnToken(CssToken token);
        }

        sealed class NotFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _nested;

            public NotFunctionState()
            {
                _nested = Pool.NewSelectorConstructor();
                _nested.IsNested = true;
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested.state != State.Data)
                {
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _nested.IsValid;
                var sel = _nested.ToPool();

                if (!valid)
                    return null;

                return SimpleSelector.PseudoClass(el => !sel.Match(el), String.Concat(PseudoClassNames.Not, "(", sel.Text, ")"));
            }
        }

        sealed class HasFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _nested;

            public HasFunctionState()
            {
                _nested = Pool.NewSelectorConstructor();
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested.state != State.Data)
                {
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _nested.IsValid;
                var sel = _nested.ToPool();

                if (!valid)
                    return null;

                return SimpleSelector.PseudoClass(el => el.ChildNodes.QuerySelector(sel) != null, String.Concat(PseudoClassNames.Has, "(", sel.Text, ")"));
            }
        }

        sealed class MatchesFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _nested;

            public MatchesFunctionState()
            {
                _nested = Pool.NewSelectorConstructor();
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested.state != State.Data)
                {
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _nested.IsValid;
                var sel = _nested.ToPool();

                if (!valid)
                    return null;

                return SimpleSelector.PseudoClass(el => sel.Match(el), String.Concat(PseudoClassNames.Matches, "(", sel.Text, ")"));
            }
        }

        sealed class DirFunctionState : FunctionState
        {
            Boolean valid;
            String value;

            public DirFunctionState()
            {
                valid = true;
                value = null;
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type == CssTokenType.Ident)
                    value = token.Data;
                else if (token.Type == CssTokenType.RoundBracketClose)
                    return true;
                else if (token.Type != CssTokenType.Whitespace)
                    valid = false;

                return false;
            }

            public override ISelector Produce()
            {
                if (!valid || value == null)
                    return null;

                var code = String.Concat(PseudoClassNames.Dir, "(", value, ")");
                return SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Direction.Equals(value, StringComparison.OrdinalIgnoreCase), code);
            }
        }

        sealed class LangFunctionState : FunctionState
        {
            Boolean valid;
            String value;

            public LangFunctionState()
            {
                valid = true;
                value = null;
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type == CssTokenType.Ident)
                    value = token.Data;
                else if (token.Type == CssTokenType.RoundBracketClose)
                    return true;
                else if (token.Type != CssTokenType.Whitespace)
                    valid = false;

                return false;
            }

            public override ISelector Produce()
            {
                if (!valid || value == null)
                    return null;

                var code = String.Concat(PseudoClassNames.Lang, "(", value, ")");
                return SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Language.StartsWith(value, StringComparison.OrdinalIgnoreCase), code);
            }
        }

        sealed class ContainsFunctionState : FunctionState
        {
            Boolean valid;
            String value;

            public ContainsFunctionState()
            {
                valid = true;
                value = null;
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String)
                    value = token.Data;
                else if (token.Type == CssTokenType.RoundBracketClose)
                    return true;
                else if (token.Type != CssTokenType.Whitespace)
                    valid = false;

                return false;
            }

            public override ISelector Produce()
            {
                if (!valid || value == null)
                    return null;

                var code = String.Concat(PseudoClassNames.Contains, "(", value, ")");
                return SimpleSelector.PseudoClass(el => el.TextContent.Contains(value), code);
            }
        }

        sealed class HostContextFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _nested;

            public HostContextFunctionState()
            {
                _nested = Pool.NewSelectorConstructor();
            }

            protected override Boolean OnToken(CssToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested.state != State.Data)
                {
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _nested.IsValid;
                var sel = _nested.ToPool();

                if (!valid)
                    return null;

                return SimpleSelector.PseudoClass(el =>
                {
                    var host = default(IElement);

                    if (el.Parent is IShadowRoot)
                        host = ((IShadowRoot)el.Parent).Host;

                    while (host != null)
                    {
                        if (sel.Match(host))
                            return true;

                        host = host.ParentElement;
                    }

                    return false;
                }, String.Concat(PseudoClassNames.HostContext, "(", sel.Text, ")"));
            }
        }

        sealed class ChildFunctionState<T> : FunctionState
            where T : ChildSelector, ISelector, new()
        {
            Boolean valid;
            Int32 step;
            Int32 offset;
            Int32 sign;
            ParseState state;
            CssSelectorConstructor nested;
            Boolean allowOf;

            public ChildFunctionState(Boolean withOptionalSelector = true)
            {
                allowOf = withOptionalSelector;
                valid = true;
                sign = 1;
                state = ParseState.Initial;
            }

            public override ISelector Produce()
            {
                var invalid = !valid || (nested != null && !nested.IsValid);
                var sel = nested != null ? nested.ToPool() : SimpleSelector.All;

                if (invalid)
                    return null;

                return new T().With(step, offset, sel);
            }

            protected override Boolean OnToken(CssToken token)
            {
                switch (state)
                {
                    case ParseState.Initial:
                        return OnInitial(token);
                    case ParseState.AfterInitialSign:
                        return OnAfterInitialSign(token);
                    case ParseState.Offset:
                        return OnOffset(token);
                    case ParseState.BeforeOf:
                        return OnBeforeOf(token);
                    default:
                        return OnAfter(token);
                }
            }

            Boolean OnAfterInitialSign(CssToken token)
            {
                if (token.Type == CssTokenType.Number)
                    return OnOffset(token);

                if (token.Type == CssTokenType.Dimension)
                {
                    var dim = (CssUnitToken)token;
                    valid = valid && dim.Unit.Equals("n", StringComparison.OrdinalIgnoreCase) && Int32.TryParse(token.Data, out step);
                    step *= sign;
                    sign = 1;
                    state = ParseState.Offset;
                    return false;
                }
                else if (token.Type == CssTokenType.Ident && token.Data.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    step = sign;
                    sign = 1;
                    state = ParseState.Offset;
                    return false;
                }
                else if (state == ParseState.Initial && token.Type == CssTokenType.Ident && token.Data.Equals("-n", StringComparison.OrdinalIgnoreCase))
                {
                    step = -1;
                    state = ParseState.Offset;
                    return false;
                }

                valid = false;
                return token.Type == CssTokenType.RoundBracketClose;
            }

            Boolean OnAfter(CssToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || nested.state != State.Data)
                {
                    nested.Apply(token);
                    return false;
                }

                return true;
            }

            Boolean OnBeforeOf(CssToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                    return false;

                if (token.Data.Equals("of", StringComparison.OrdinalIgnoreCase))
                {
                    valid = allowOf;
                    state = ParseState.AfterOf;
                    nested = Pool.NewSelectorConstructor();
                    return false;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                    return true;

                valid = false;
                return false;
            }

            Boolean OnOffset(CssToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                    return false;

                if (token.Type == CssTokenType.Number)
                {
                    valid = valid && ((CssNumberToken)token).IsInteger && Int32.TryParse(token.Data, out offset);
                    offset *= sign;
                    state = ParseState.BeforeOf;
                    return false;
                }

                return OnBeforeOf(token);
            }

            Boolean OnInitial(CssToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                    return false;

                if (token.Data.Equals("odd", StringComparison.OrdinalIgnoreCase))
                {
                    state = ParseState.BeforeOf;
                    step = 2;
                    offset = 1;
                    return false;
                }
                else if (token.Data.Equals("even", StringComparison.OrdinalIgnoreCase))
                {
                    state = ParseState.BeforeOf;
                    step = 2;
                    offset = 0;
                    return false;
                }
                else if (token.Type == CssTokenType.Delim && (token.Data == "+" || token.Data == "-"))
                {
                    sign = token.Data == "-" ? -1 : +1;
                    state = ParseState.AfterInitialSign;
                    return false;
                }

                return OnAfterInitialSign(token);

            }

            enum ParseState
            {
                Initial,
                AfterInitialSign,
                Offset,
                BeforeOf,
                AfterOf
            }
        }

        #endregion
    }
}
