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

        readonly Stack<CssCombinator> _combinators;
        readonly List<CssToken> _tokens;

		State _state;
        ISelector _temp;
		ListSelector _group;
		ComplexSelector _complex;
		String _attrName;
		String _attrValue;
		String _attrOp;
        String _attrNs;
        Boolean _valid;
        Boolean _ready;

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
            _combinators = new Stack<CssCombinator>();
            _tokens = new List<CssToken>();
			Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the stored selector is valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return _valid && _ready; }
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

            if (_complex != null)
            {
                _complex.ConcludeSelector(_temp);
                _temp = _complex;
                _complex = null;
            }

            if (_group == null || _group.Length == 0)
                return _temp ?? SimpleSelector.All;
            else if (_temp == null && _group.Length == 1)
                return _group[0];

            if (_temp != null)
            {
                _group.Add(_temp);
                _temp = null;
            }

            return _group;
        }

        /// <summary>
        /// Tries to serialize the current selector.
        /// </summary>
        /// <returns>The approximate selector text.</returns>
        String Serialize()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var token in _tokens)
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

            _tokens.Add(token);

			switch (_state)
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
                    _valid = false;
                    break;
            }
        }

		/// <summary>
		/// Resets the current state.
		/// </summary>
		/// <returns>The current instance.</returns>
		public CssSelectorConstructor Reset()
		{
			_attrName = null;
			_attrValue = null;
            _attrNs = null;
			_attrOp = String.Empty;
			_state = State.Data;
			_combinators.Clear();
            _tokens.Clear();
			_temp = null;
			_group = null;
			_complex = null;
            _valid = true;
            IsNested = false;
            _ready = true;
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
					_attrName = null;
					_attrValue = null;
					_attrOp = String.Empty;
                    _attrNs = null;
					_state = State.Attribute;
                    _ready = false;
					break;

				//Begin of Pseudo :P
				case CssTokenType.Colon:
					_state = State.PseudoClass;
                    _ready = false;
                    break;

                //Begin of ID #I
                case CssTokenType.Hash:
					Insert(SimpleSelector.Id(token.Data));
                    _ready = true;
                    break;

                //Begin of Type E
                case CssTokenType.Ident:
					Insert(SimpleSelector.Type(token.Data));
                    _ready = true;
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
                    _ready = false;
                    break;

                default:
                    _valid = false;
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
                _state = State.AttributeOperator;
                _attrName = token.Data;
            }
            else if (token.Type == CssTokenType.Delim && token.ToValue() == "|")
            {
                _state = State.Attribute;
                _attrNs = String.Empty;
            }
            else if (token.Type == CssTokenType.Delim && token.ToValue() == "*")
            {
                _state = State.AttributeOperator;
                _attrName = token.ToValue();
            }
            else
            {
                _state = State.Data;
                _valid = false;
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
                _state = State.AttributeValue;
                OnAttributeEnd(token);
            }
            else if (token.Type == CssTokenType.Match || token.Type == CssTokenType.Delim)
            {
                _state = State.AttributeValue;
                _attrOp = token.ToValue();

                if (_attrOp == "|")
                {
                    _attrNs = _attrName;
                    _attrName = null;
                    _attrOp = String.Empty;
                    _state = State.Attribute;
                }
            }
            else
            {
                _state = State.AttributeEnd;
                _valid = false;
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
                _state = State.AttributeEnd;
                _attrValue = token.Data;
            }
            else
            {
                _state = State.Data;
                _valid = false;
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

			_state = State.Data;
            _ready = true;

            if (token.Type == CssTokenType.SquareBracketClose)
            {
                var selector = CreateAttrSelector();
                Insert(selector);
            }
            else            
                _valid = false;
        }

        /// <summary>
        /// Creates an attribute selector using the current state.
        /// </summary>
        /// <returns>The created selector.</returns>
        SimpleSelector CreateAttrSelector()
        {
            switch (_attrOp)
            {
                case "=":
                    return SimpleSelector.AttrMatch(_attrName, _attrValue, _attrNs);
                case "~=":
                    return SimpleSelector.AttrList(_attrName, _attrValue, _attrNs);
                case "|=":
                    return SimpleSelector.AttrHyphen(_attrName, _attrValue, _attrNs);
                case "^=":
                    return SimpleSelector.AttrBegins(_attrName, _attrValue, _attrNs);
                case "$=":
                    return SimpleSelector.AttrEnds(_attrName, _attrValue, _attrNs);
                case "*=":
                    return SimpleSelector.AttrContains(_attrName, _attrValue, _attrNs);
                case "!=":
                    return SimpleSelector.AttrNotMatch(_attrName, _attrValue, _attrNs);
                default:
                    return SimpleSelector.AttrAvailable(_attrName, _attrNs);
            }
        }

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnPseudoClass(CssToken token)
		{
			_state = State.Data;
            _ready = true;

            if (token.Type == CssTokenType.Colon)
            {
                _state = State.PseudoElement;
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
            
            _valid = false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnPseudoElement(CssToken token)
        {
            _state = State.Data;
            _ready = true;

            if (token.Type == CssTokenType.Ident)
            {
                var sel = Factory.PseudoElementSelector.Create(token.Data);

                if (sel != null)
                {
                    if (IsNested)
                        _valid = false;

                    Insert(sel);
                    return;
                }
            }

            _valid = false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnClass(CssToken token)
		{
			_state = State.Data;
            _ready = true;

            if (token.Type == CssTokenType.Ident)
                Insert(SimpleSelector.Class(token.Data));
            else
                _valid = false;
		}

		#endregion

		#region Insertations

		/// <summary>
        /// Inserts a comma.
        /// </summary>
        void InsertOr()
        {
            if (_temp != null)
            {
                if (_group == null)
                    _group = new ListSelector();

                if (_complex != null)
                {
                    _complex.ConcludeSelector(_temp);
                    _group.Add(_complex);
                    _complex = null;
                }
                else
                    _group.Add(_temp);

                _temp = null;
            }
        }

        /// <summary>
        /// Inserts the given selector.
        /// </summary>
        /// <param name="selector">The selector to insert.</param>
        void Insert(ISelector selector)
        {
            if (_temp != null)
            {
                if (_combinators.Count == 0)
                {
                    var compound = _temp as CompoundSelector;

                    if (compound == null)
                    {
                        compound = new CompoundSelector();
                        compound.Add(_temp);
                    }

                    compound.Add(selector);
                    _temp = compound;
                }
                else
                {
                    if (_complex == null)
                        _complex = new ComplexSelector();

                    var combinator = GetCombinator();
                    _complex.AppendSelector(_temp, combinator);
                    _temp = selector;
                }
            }
            else
            {
                _combinators.Clear();
                _temp = selector;
            }
        }

        CssCombinator GetCombinator()
        {
            //Remove all trailing whitespaces
            while (_combinators.Count > 1 && _combinators.Peek() == CssCombinator.Descendent)
                _combinators.Pop();

            if (_combinators.Count > 1)
            {
                var last = _combinators.Pop();
                var previous = _combinators.Pop();

                //Care about combinator combinations, such as >>, >>> and ||
                if (last == CssCombinator.Child && previous == CssCombinator.Child)
                {
                    if (_combinators.Count == 0 || _combinators.Peek() != CssCombinator.Child)
                        last = CssCombinator.Descendent;
                    else if (_combinators.Pop() == CssCombinator.Child)
                        last = CssCombinator.Deep;
                }
                else if (last == CssCombinator.Namespace && previous == CssCombinator.Namespace)
                {
                    last = CssCombinator.Column;
                }
                else
                {
                    _combinators.Push(previous);
                }

                //Remove all leading whitespaces, invalid if mixed
                while (_combinators.Count > 0)
                    _valid = _combinators.Pop() == CssCombinator.Descendent && _valid;

                return last;
            }

            return _combinators.Pop();
        }

        /// <summary>
        /// Inserts the given combinator.
        /// </summary>
        /// <param name="cssCombinator">The combinator to insert.</param>
        void Insert(CssCombinator cssCombinator)
        {
            _combinators.Push(cssCombinator);
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
                    _ready = false;
                    break;

                case Symbols.GreaterThan:
					Insert(CssCombinator.Child);
                    _ready = false;
                    break;

                case Symbols.Plus:
					Insert(CssCombinator.AdjacentSibling);
                    _ready = false;
                    break;

                case Symbols.Tilde:
					Insert(CssCombinator.Sibling);
                    _ready = false;
                    break;

                case Symbols.Asterisk:
					Insert(SimpleSelector.All);
                    _ready = true;
                    break;

                case Symbols.Dot:
					_state = State.Class;
                    _ready = false;
                    break;

                case Symbols.Pipe:
                    if (_combinators.Count > 0 && _combinators.Peek() == CssCombinator.Descendent)
                        Insert(SimpleSelector.Type(String.Empty));

                    Insert(CssCombinator.Namespace);
                    _ready = false;
                    break;

                default:
                    _valid = false;
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
                _ready = false;

                foreach (var token in arguments)
                {
                    if (function.Finished(token))
                    {
                        var sel = function.Produce();

                        if (IsNested && function is NotFunctionState)
                            sel = null;

                        _ready = true;
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
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
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
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
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
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
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
                return SimpleSelector.PseudoClass(el => el is IHtmlElement && value.Isi(((IHtmlElement)el).Direction), code);
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
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
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
                    valid = valid && dim.Unit.Isi("n") && Int32.TryParse(token.Data, out step);
                    step *= sign;
                    sign = 1;
                    state = ParseState.Offset;
                    return false;
                }
                else if (token.Type == CssTokenType.Ident && token.Data.Isi("n"))
                {
                    step = sign;
                    sign = 1;
                    state = ParseState.Offset;
                    return false;
                }
                else if (state == ParseState.Initial && token.Type == CssTokenType.Ident && token.Data.Isi("-n"))
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
                if (token.Type != CssTokenType.RoundBracketClose || nested._state != State.Data)
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

                if (token.Data.Isi("of"))
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

                if (token.Data.Isi("odd"))
                {
                    state = ParseState.BeforeOf;
                    step = 2;
                    offset = 1;
                    return false;
                }
                else if (token.Data.Isi("even"))
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
