namespace AngleSharp.Css.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Class for construction for CSS selectors as specified in
    /// http://www.w3.org/html/wg/drafts/html/master/selectors.html.
    /// </summary>
    sealed class CssSelectorConstructor
    {
        #region Fields

        private static readonly Dictionary<String, Func<CssSelectorConstructor, FunctionState>> pseudoClassFunctions = new Dictionary<String, Func<CssSelectorConstructor, FunctionState>>(StringComparer.OrdinalIgnoreCase)
        {
            { PseudoClassNames.NthChild, ctx => new ChildFunctionState((step, offset, kind) => new FirstChildSelector(step, offset, kind), ctx, withOptionalSelector: true) },
            { PseudoClassNames.NthLastChild, ctx => new ChildFunctionState((step, offset, kind) => new LastChildSelector(step, offset, kind), ctx, withOptionalSelector: true) },
            { PseudoClassNames.NthOfType, ctx => new ChildFunctionState((step, offset, kind) => new FirstTypeSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthLastOfType, ctx => new ChildFunctionState((step, offset, kind) => new LastTypeSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthColumn, ctx => new ChildFunctionState((step, offset, kind) => new FirstColumnSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthLastColumn, ctx => new ChildFunctionState((step, offset, kind) => new LastColumnSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.Not, ctx => new NotFunctionState(ctx) },
            { PseudoClassNames.Dir, ctx => new DirFunctionState() },
            { PseudoClassNames.Lang, ctx => new LangFunctionState() },
            { PseudoClassNames.Contains, ctx => new ContainsFunctionState() },
            { PseudoClassNames.Has, ctx => new HasFunctionState(ctx) },
            { PseudoClassNames.Matches, ctx => new MatchesFunctionState(ctx) },
            { PseudoClassNames.HostContext, ctx => new HostContextFunctionState(ctx) },
        };

        private readonly CssTokenizer _tokenizer;
        private readonly Stack<CssCombinator> _combinators;
        private readonly IAttributeSelectorFactory _attributeSelector;
        private readonly IPseudoElementSelectorFactory _pseudoElementSelector;
        private readonly IPseudoClassSelectorFactory _pseudoClassSelector;

        private State _state;
        private ISelector _temp;
		private ListSelector _group;
		private ComplexSelector _complex;
		private String _attrName;
		private String _attrValue;
        private Boolean _attrInsensitive;
		private String _attrOp;
        private String _attrNs;
        private Boolean _valid;
        private Boolean _nested;
        private Boolean _ready;
        private FunctionState _function;
        private Boolean _invoked;

        #endregion

        #region ctor

        public CssSelectorConstructor(CssTokenizer tokenizer, IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector, Boolean invoked = false)
        {
            _tokenizer = tokenizer;
            _invoked = invoked;
            _combinators = new Stack<CssCombinator>();
            _attributeSelector = attributeSelector;
            _pseudoClassSelector = pseudoClassSelector;
            _pseudoElementSelector = pseudoElementSelector;
            _attrOp = String.Empty;
            _attrInsensitive = false;
            _state = State.Data;
            _valid = true;
            _ready = true;
        }

        #endregion

        #region Properties

        public Boolean IsValid => _invoked && _valid && _ready;

        public Boolean IsNested => _nested;

        #endregion

        #region Methods

        public ISelector Parse()
        {
            var token = _tokenizer.Get();

            while (token.Type != CssTokenType.EndOfFile)
            {
                Apply(token);
                token = _tokenizer.Get();
            }

            return GetResult();
        }

        private ISelector GetResult()
        {
            if (IsValid)
            {
                if (_complex != null)
                {
                    _complex.ConcludeSelector(_temp);
                    _temp = _complex;
                    _complex = null;
                }

                if (_group == null || _group.Length == 0)
                {
                    return _temp ?? AllSelector.Instance;
                }
                else if (_temp == null && _group.Length == 1)
                {
                    return _group[0];
                }

                if (_temp != null)
                {
                    _group.Add(_temp);
                    _temp = null;
                }

                return _group;
            }

            return null;
        }

        private void Apply(CssSelectorToken token)
        {
            _invoked = true;

            switch (_state)
            {
                case State.Data:
                    OnData(token);
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
                case State.Function:
                    OnFunctionState(token);
                    break;
                default:
                    _valid = false;
                    break;
            }
        }

        #endregion

        #region States

        private void OnData(CssSelectorToken token)
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
					Insert(new IdSelector(token.Data));
                    _ready = true;
                    break;

                //Begin of Class .c
                case CssTokenType.Class:
                    Insert(new ClassSelector(token.Data));
                    _ready = true;
                    break;

                //Begin of Type E
                case CssTokenType.Ident:
					Insert(new TypeSelector(token.Data));
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

                case CssTokenType.Column:
                    Insert(CssCombinator.Column);
                    _ready = false;
                    break;

                case CssTokenType.Descendent:
                    Insert(CssCombinator.Descendent);
                    _ready = false;
                    break;

                case CssTokenType.Deep:
                    Insert(CssCombinator.Deep);
                    _ready = false;
                    break;

                default:
                    _valid = false;
                    break;
            }
		}

        private void OnAttribute(CssSelectorToken token)
		{
			if (token.Type != CssTokenType.Whitespace)
            {
                if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String)
                {
                    _state = State.AttributeOperator;
                    _attrName = token.Data;
                }
                else if (token.Type == CssTokenType.Delim && token.Data.Is(CombinatorSymbols.Pipe))
                {
                    _state = State.Attribute;
                    _attrNs = String.Empty;
                }
                else if (token.Type == CssTokenType.Delim && token.Data.Is("*"))
                {
                    _state = State.AttributeOperator;
                    _attrName = "*";
                }
                else
                {
                    _state = State.Data;
                    _valid = false;
                }
            }
		}

        private void OnAttributeOperator(CssSelectorToken token)
		{
            if (token.Type != CssTokenType.Whitespace)
            {
                if (token.Type == CssTokenType.SquareBracketClose)
                {
                    _state = State.AttributeValue;
                    OnAttributeEnd(token);
                }
                else if (token.Type == CssTokenType.Match || token.Type == CssTokenType.Delim)
                {
                    _state = State.AttributeValue;
                    _attrOp = token.Data;

                    if (_attrOp == CombinatorSymbols.Pipe)
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
		}

        private void OnAttributeValue(CssSelectorToken token)
		{
            if (token.Type != CssTokenType.Whitespace)
            {
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
		}

		private void OnAttributeEnd(CssSelectorToken token)
        {
            if (!_attrInsensitive && token.Type == CssTokenType.Ident && token.Data == "i")
            {
                _attrInsensitive = true;
            }
            else if (token.Type != CssTokenType.Whitespace)
            {
                _state = State.Data;
                _ready = true;

                if (token.Type == CssTokenType.SquareBracketClose)
                {
                    var selector = _attributeSelector.Create(_attrOp, _attrName, _attrValue, _attrNs, _attrInsensitive);
                    _attrInsensitive = false;
                    Insert(selector);
                }
                else
                {
                    _valid = false;
                }
            }
        }

        private void OnPseudoClass(CssSelectorToken token)
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
                if (pseudoClassFunctions.TryGetValue(token.Data, out var creator))
                {
                    _state = State.Function;
                    _function = creator.Invoke(this);
                    _ready = false;
                    return;
                }
            }
            else if (token.Type == CssTokenType.Ident)
            {
                var sel = _pseudoClassSelector.Create(token.Data);

                if (sel != null)
                {
                    Insert(sel);
                    return;
                }
            }

            _valid = false;
		}

        private void OnPseudoElement(CssSelectorToken token)
        {
            _state = State.Data;
            _ready = true;

            if (token.Type == CssTokenType.Ident)
            {
                var sel = _pseudoElementSelector.Create(token.Data);

                if (sel != null)
                {
                    _valid = _valid && !_nested;
                    Insert(sel);
                    return;
                }
            }

            _valid = false;
		}

		#endregion

		#region Insertations

        private void InsertOr()
        {
            if (_temp != null)
            {
                if (_group == null)
                {
                    _group = new ListSelector();
                }

                if (_complex != null)
                {
                    _complex.ConcludeSelector(_temp);
                    _group.Add(_complex);
                    _complex = null;
                }
                else
                {
                    _group.Add(_temp);
                }

                _temp = null;
            }
        }

        private void Insert(ISelector selector)
        {
            if (_temp != null)
            {
                if (_combinators.Count == 0)
                {
                    if (!(_temp is CompoundSelector compound))
                    {
                        compound = new CompoundSelector { _temp };
                    }

                    compound.Add(selector);
                    _temp = compound;
                }
                else
                {
                    if (_complex == null)
                    {
                        _complex = new ComplexSelector();
                    }

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

        private CssCombinator GetCombinator()
        {
            //Remove all trailing whitespaces
            while (_combinators.Count > 1 && _combinators.Peek() == CssCombinator.Descendent)
            {
                _combinators.Pop();
            }

            if (_combinators.Count > 1)
            {
                var last = _combinators.Pop();

                //Remove all leading whitespaces, invalid if mixed
                while (_combinators.Count > 0)
                {
                    _valid = _combinators.Pop() == CssCombinator.Descendent && _valid;
                }

                return last;
            }

            return _combinators.Pop();
        }

        private void Insert(CssCombinator cssCombinator) => _combinators.Push(cssCombinator);

		#endregion

		#region Substates

		private void OnDelim(CssSelectorToken token)
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
					Insert(AllSelector.Instance);
                    _ready = true;
                    break;

                case Symbols.Pipe:
                    if (_combinators.Count > 0 && _combinators.Peek() == CssCombinator.Descendent)
                    {
                        Insert(new TypeSelector(String.Empty));
                    }

                    Insert(CssCombinator.Namespace);
                    _ready = false;
                    break;

                default:
                    _valid = false;
                    break;
            }
		}

        private void OnFunctionState(CssSelectorToken token)
        {
            if (_function.Finished(token))
            {
                var sel = _function.Produce();

                if (_nested && _function is NotFunctionState)
                {
                    sel = null;
                }

                _function = null;
                _state = State.Data;
                _ready = true;

                if (sel != null)
                {
                    Insert(sel);
                    return;
                }

                _valid = false;
            }
        }

        private CssSelectorConstructor CreateChild() => new CssSelectorConstructor(_tokenizer, _attributeSelector, _pseudoClassSelector, _pseudoElementSelector, true);

        #endregion

        #region State-Machine

        /// <summary>
        /// The various parsing states.
        /// </summary>
        private enum State : byte
		{
			Data,
            Function,
			Attribute,
			AttributeOperator,
			AttributeValue,
			AttributeEnd,
			PseudoClass,
			PseudoElement
		}

        #endregion

        #region Function States

        private abstract class FunctionState
        {
            public Boolean Finished(CssSelectorToken token)
            {
                return OnToken(token);
            }

            public abstract ISelector Produce();

            protected abstract Boolean OnToken(CssSelectorToken token);
        }

        private sealed class NotFunctionState : FunctionState
        {
            private readonly CssSelectorConstructor _selector;

            public NotFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild();
                _selector._nested = true;
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _selector._state != State.Data)
                {
                    _selector.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _selector.IsValid;
                var sel = _selector.GetResult();

                if (valid)
                {
                    var code = PseudoClassNames.Not.CssFunction(sel.Text);
                    return new PseudoClassSelector(el => !sel.Match(el), code);
                }

                return null;
            }
        }

        private sealed class HasFunctionState : FunctionState
        {
            private readonly CssSelectorConstructor _nested;
            private Boolean _firstToken = true;
            private Boolean _matchSiblings = false;

            public HasFunctionState(CssSelectorConstructor parent)
            {
                _nested = parent.CreateChild();
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
                {
                    if (_firstToken && token.Type == CssTokenType.Delim)
                    {
                        // Roughly equivalent to inserting an implicit :scope
                        _nested.Insert(ScopePseudoClassSelector.Instance);
                        _nested.Apply(CssSelectorToken.Whitespace);
                        _matchSiblings = true;
                    }

                    _firstToken = false;
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _nested.IsValid;
                var sel = _nested.GetResult();
                var selText = sel.Text;
                var matchSiblings = _matchSiblings || selText.Contains(":" + PseudoClassNames.Scope);

                if (valid)
                {
                    var code = PseudoClassNames.Has.CssFunction(selText);

                    return new PseudoClassSelector(el =>
                    {
                        var elements = default(IEnumerable<IElement>);

                        if (matchSiblings)
                        {
                            elements = el.ParentElement?.Children;
                        }
                        else
                        {
                            elements = el.Children;
                        }

                        if (elements == null)
                        {
                            elements = Enumerable.Empty<IElement>();
                        }

                        return sel.MatchAny(elements, el) != null;
                    }, code);
                }

                return null;
            }
        }

        private sealed class MatchesFunctionState : FunctionState
        {
            private readonly CssSelectorConstructor _selector;

            public MatchesFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild();
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _selector._state != State.Data)
                {
                    _selector.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _selector.IsValid;
                var sel = _selector.GetResult();

                if (valid)
                {
                    var code = PseudoClassNames.Matches.CssFunction(sel.Text);
                    return new PseudoClassSelector(el => sel.Match(el), code);
                }

                return null;
            }
        }

        private sealed class DirFunctionState : FunctionState
        {
            private Boolean _valid;
            private String _value;

            public DirFunctionState()
            {
                _valid = true;
                _value = null;
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Ident)
                {
                    _value = token.Data;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                {
                    return true;
                }
                else if (token.Type != CssTokenType.Whitespace)
                {
                    _valid = false;
                }

                return false;
            }

            public override ISelector Produce()
            {
                if (_valid && _value != null)
                {
                    var code = PseudoClassNames.Dir.CssFunction(_value);
                    return new PseudoClassSelector(el => el is IHtmlElement && _value.Isi(((IHtmlElement)el).Direction), code);
                }

                return null;
            }
        }

        private sealed class LangFunctionState : FunctionState
        {
            private Boolean valid;
            private String value;

            public LangFunctionState()
            {
                valid = true;
                value = null;
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Ident)
                {
                    value = token.Data;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                {
                    return true;
                }
                else if (token.Type != CssTokenType.Whitespace)
                {
                    valid = false;
                }

                return false;
            }

            public override ISelector Produce()
            {
                if (valid && value != null)
                {
                    var code = PseudoClassNames.Lang.CssFunction(value);
                    return new PseudoClassSelector(el => el is IHtmlElement && ((IHtmlElement)el).Language.StartsWith(value, StringComparison.OrdinalIgnoreCase), code);
                }

                return null;
            }
        }

        private sealed class ContainsFunctionState : FunctionState
        {
            private Boolean _valid;
            private String _value;

            public ContainsFunctionState()
            {
                _valid = true;
                _value = null;
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String)
                {
                    _value = token.Data;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                {
                    return true;
                }
                else if (token.Type != CssTokenType.Whitespace)
                {
                    _valid = false;
                }

                return false;
            }

            public override ISelector Produce()
            {
                if (_valid && _value != null)
                {
                    var code = PseudoClassNames.Contains.CssFunction(_value);
                    return new PseudoClassSelector(el => el.TextContent.Contains(_value), code);
                }

                return null;
            }
        }

        private sealed class HostContextFunctionState : FunctionState
        {
            private readonly CssSelectorConstructor _selector;

            public HostContextFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild();
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _selector._state != State.Data)
                {
                    _selector.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector Produce()
            {
                var valid = _selector.IsValid;
                var sel = _selector.GetResult();

                if (valid)
                {
                    var code = PseudoClassNames.HostContext.CssFunction(sel.Text);
                    return new PseudoClassSelector(el =>
                    {
                        var shadowRoot = el.Parent as IShadowRoot;
                        var host = shadowRoot?.Host;

                        while (host != null)
                        {
                            if (sel.Match(host))
                            {
                                return true;
                            }

                            host = host.ParentElement;
                        }

                        return false;
                    }, code);
                }

                return null;
            }
        }

        private sealed class ChildFunctionState : FunctionState
        {
            private readonly CssSelectorConstructor _parent;

            private Boolean _valid;
            private Int32 _step;
            private Int32 _offset;
            private Int32 _sign;
            private ParseState _state;
            private CssSelectorConstructor _nested;
            private Boolean _allowOf;
            private Func<Int32, Int32, ISelector, ISelector> _creator;

            public ChildFunctionState(Func<Int32, Int32, ISelector, ISelector> creator, CssSelectorConstructor parent, Boolean withOptionalSelector = true)
            {
                _creator = creator;
                _parent = parent;
                _allowOf = withOptionalSelector;
                _valid = true;
                _sign = 1;
                _state = ParseState.Initial;
            }

            public override ISelector Produce()
            {
                var invalid = !_valid || (_nested != null && !_nested.IsValid);

                if (!invalid)
                {
                    var sel = _nested?.GetResult() ?? AllSelector.Instance;
                    return _creator.Invoke(_step, _offset, sel);
                }

                return null;
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                switch (_state)
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

            private Boolean OnAfterInitialSign(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Number)
                {
                    return OnOffset(token);
                }

                if (token.Type == CssTokenType.Dimension)
                {
                    var integral = token.Data.Remove(token.Data.Length - 1);
                    _valid = _valid && token.Data.EndsWith("n", StringComparison.OrdinalIgnoreCase) && Int32.TryParse(integral, NumberStyles.Integer, CultureInfo.InvariantCulture, out _step);
                    _step *= _sign;
                    _sign = 1;
                    _state = ParseState.Offset;
                    return false;
                }
                else if (token.Type == CssTokenType.Ident && token.Data.Isi("n"))
                {
                    _step = _sign;
                    _sign = 1;
                    _state = ParseState.Offset;
                    return false;
                }
                else if (_state == ParseState.Initial && token.Type == CssTokenType.Ident && token.Data.Isi("-n"))
                {
                    _step = -1;
                    _state = ParseState.Offset;
                    return false;
                }

                _valid = false;
                return token.Type == CssTokenType.RoundBracketClose;
            }

            private Boolean OnAfter(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
                {
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            private Boolean OnBeforeOf(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                {
                    return false;
                }

                if (token.Data.Isi(Keywords.Of))
                {
                    _valid = _allowOf;
                    _state = ParseState.AfterOf;
                    _nested = _parent.CreateChild();
                    return false;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                {
                    return true;
                }

                _valid = false;
                return false;
            }

            private Boolean OnOffset(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                {
                    return false;
                }

                if (token.Type == CssTokenType.Number)
                {
                    _valid = _valid && Int32.TryParse(token.Data, NumberStyles.Integer, CultureInfo.InvariantCulture, out _offset);
                    _offset *= _sign;
                    _state = ParseState.BeforeOf;
                    return false;
                }

                return OnBeforeOf(token);
            }

            private Boolean OnInitial(CssSelectorToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                {
                    return false;
                }

                if (token.Data.Isi(Keywords.Odd))
                {
                    _state = ParseState.BeforeOf;
                    _step = 2;
                    _offset = 1;
                    return false;
                }
                else if (token.Data.Isi(Keywords.Even))
                {
                    _state = ParseState.BeforeOf;
                    _step = 2;
                    _offset = 0;
                    return false;
                }
                else if (token.Type == CssTokenType.Delim && token.Data.IsOneOf("+", "-"))
                {
                    _sign = token.Data == "-" ? -1 : +1;
                    _state = ParseState.AfterInitialSign;
                    return false;
                }

                return OnAfterInitialSign(token);

            }

            private enum ParseState : byte
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
