namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for construction for CSS selectors as specified in
    /// http://www.w3.org/html/wg/drafts/html/master/selectors.html.
    /// </summary>
    sealed class CssSelectorConstructor
    {
        #region Fields

        static readonly Dictionary<String, Func<CssSelectorConstructor, FunctionState>> pseudoClassFunctions = new Dictionary<String, Func<CssSelectorConstructor, FunctionState>>(StringComparer.OrdinalIgnoreCase)
        {
            { PseudoClassNames.NthChild, ctx => new ChildFunctionState<FirstChildSelector>(ctx, withOptionalSelector: true) },
            { PseudoClassNames.NthLastChild, ctx => new ChildFunctionState<LastChildSelector>(ctx, withOptionalSelector: true) },
            { PseudoClassNames.NthOfType, ctx => new ChildFunctionState<FirstTypeSelector>(ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthLastOfType, ctx => new ChildFunctionState<LastTypeSelector>(ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthColumn, ctx => new ChildFunctionState<FirstColumnSelector>(ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthLastColumn, ctx => new ChildFunctionState<LastColumnSelector>(ctx, withOptionalSelector: false) },
            { PseudoClassNames.Not, ctx => new NotFunctionState(ctx) },
            { PseudoClassNames.Dir, ctx => new DirFunctionState() },
            { PseudoClassNames.Lang, ctx => new LangFunctionState() },
            { PseudoClassNames.Contains, ctx => new ContainsFunctionState() },
            { PseudoClassNames.Has, ctx => new HasFunctionState(ctx) },
            { PseudoClassNames.Matches, ctx => new MatchesFunctionState(ctx) },
            { PseudoClassNames.HostContext, ctx => new HostContextFunctionState(ctx) },
        };

        private readonly Stack<CssCombinator> _combinators;

        private State _state;
        private ISelector _temp;
		private ListSelector _group;
		private ComplexSelector _complex;
		private String _attrName;
		private String _attrValue;
		private String _attrOp;
        private String _attrNs;
        private Boolean _valid;
        private Boolean _invoked;
        private Boolean _nested;
        private Boolean _ready;
        private IAttributeSelectorFactory _attributeSelector;
        private IPseudoElementSelectorFactory _pseudoElementSelector;
        private IPseudoClassSelectorFactory _pseudoClassSelector;

        #endregion

        #region ctor

        public CssSelectorConstructor(IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector)
        {
            _combinators = new Stack<CssCombinator>();
			Reset(attributeSelector, pseudoClassSelector, pseudoElementSelector);
        }

        #endregion

        #region Properties

        public Boolean IsValid
        {
            get { return _invoked && _valid && _ready; }
        }

        public Boolean IsNested
        {
            get { return _nested; }
        }

        #endregion

        #region Methods

        public ISelector GetResult()
        {
            if (!IsValid)
            {
                var selector = new UnknownSelector();
                return selector;
            }

            if (_complex != null)
            {
                _complex.ConcludeSelector(_temp);
                _temp = _complex;
                _complex = null;
            }

            if (_group == null || _group.Length == 0)
            {
                return _temp ?? SimpleSelector.All;
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

        public void Apply(CssToken token)
        {
            if (token.Type != CssTokenType.Comment)
            {
                _invoked = true;

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
        }

        public CssSelectorConstructor Reset(IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector)
		{
			_attrName = null;
			_attrValue = null;
            _attrNs = null;
			_attrOp = String.Empty;
			_state = State.Data;
			_combinators.Clear();
			_temp = null;
			_group = null;
			_complex = null;
            _valid = true;
            _nested = false;
            _invoked = false;
            _ready = true;
            _attributeSelector = attributeSelector;
            _pseudoClassSelector = pseudoClassSelector;
            _pseudoElementSelector = pseudoElementSelector;
			return this;
		}

		#endregion

		#region States

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

		void OnAttribute(CssToken token)
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
                else if (token.Type == CssTokenType.Delim && token.Data.Is(Keywords.Asterisk))
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
		}

		void OnAttributeOperator(CssToken token)
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
                    _attrOp = token.ToValue();

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

		void OnAttributeValue(CssToken token)
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

		void OnAttributeEnd(CssToken token)
		{
            if (token.Type != CssTokenType.Whitespace)
            {
                _state = State.Data;
                _ready = true;

                if (token.Type == CssTokenType.SquareBracketClose)
                {
                    var selector = _attributeSelector.Create(_attrOp, _attrName, _attrValue, _attrNs);
                    Insert(selector);
                }
                else
                {
                    _valid = false;
                }
            }
        }

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
                var sel = _pseudoClassSelector.Create(token.Data);

                if (sel != null)
                {
                    Insert(sel);
                    return;
                }
            }
            
            _valid = false;
		}

		void OnPseudoElement(CssToken token)
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

		void OnClass(CssToken token)
		{
			_state = State.Data;
            _ready = true;

            if (token.Type == CssTokenType.Ident)
            {
                Insert(SimpleSelector.Class(token.Data));
            }
            else
            {
                _valid = false;
            }
		}

		#endregion

		#region Insertations

        void InsertOr()
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

        void Insert(ISelector selector)
        {
            if (_temp != null)
            {
                if (_combinators.Count == 0)
                {
                    var compound = _temp as CompoundSelector;

                    if (compound == null)
                    {
                        compound = new CompoundSelector { this._temp };
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

        CssCombinator GetCombinator()
        {
            //Remove all trailing whitespaces
            while (_combinators.Count > 1 && _combinators.Peek() == CssCombinator.Descendent)
            {
                _combinators.Pop();
            }

            if (_combinators.Count > 1)
            {
                var last = _combinators.Pop();
                var previous = _combinators.Pop();

                //Care about combinator combinations, such as >>, >>> and ||
                if (last == CssCombinator.Child && previous == CssCombinator.Child)
                {
                    if (_combinators.Count == 0 || _combinators.Peek() != CssCombinator.Child)
                    {
                        last = CssCombinator.Descendent;
                    }
                    else if (_combinators.Pop() == CssCombinator.Child)
                    {
                        last = CssCombinator.Deep;
                    }
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
                {
                    _valid = _combinators.Pop() == CssCombinator.Descendent && _valid;
                }

                return last;
            }

            return _combinators.Pop();
        }

        void Insert(CssCombinator cssCombinator)
        {
            _combinators.Push(cssCombinator);
        }

		#endregion

		#region Substates

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
                    {
                        Insert(SimpleSelector.Type(String.Empty));
                    }

                    Insert(CssCombinator.Namespace);
                    _ready = false;
                    break;

                default:
                    _valid = false;
                    break;
            }
		}

        ISelector GetPseudoFunction(CssFunctionToken arguments)
        {
            var creator = default(Func<CssSelectorConstructor, FunctionState>);

            if (pseudoClassFunctions.TryGetValue(arguments.Data, out creator))
            {
                using (var function = creator(this))
                {
                    _ready = false;

                    foreach (var token in arguments)
                    {
                        if (function.Finished(token))
                        {
                            var sel = function.Produce();

                            if (_nested && function is NotFunctionState)
                            {
                                sel = null;
                            }

                            _ready = true;
                            return sel;
                        }
                    }
                }
            }

            return null;
        }

        private CssSelectorConstructor CreateChild()
        {
            var ctor = Pool.NewSelectorConstructor(_attributeSelector, _pseudoClassSelector, _pseudoElementSelector);
            ctor._invoked = true;
            return ctor;
        }

        #endregion

		#region State-Machine

		/// <summary>
		/// The various parsing states.
		/// </summary>
		enum State : byte
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

        abstract class FunctionState : IDisposable
        {
            public Boolean Finished(CssToken token)
            {
                return OnToken(token);
            }

            public abstract ISelector Produce();

            protected abstract Boolean OnToken(CssToken token);

            public virtual void Dispose()
            {
            }
        }

        sealed class NotFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _selector;

            public NotFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild();
                _selector._nested = true;
            }

            protected override Boolean OnToken(CssToken token)
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
                    return SimpleSelector.PseudoClass(el => !sel.Match(el), code);
                }

                return null;
            }

            public override void Dispose()
            {
                base.Dispose();
                _selector.ToPool();
            }
        }

        sealed class HasFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _nested;

            public HasFunctionState(CssSelectorConstructor parent)
            {
                _nested = parent.CreateChild();
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
                var sel = _nested.GetResult();

                if (valid)
                {
                    var code = PseudoClassNames.Has.CssFunction(sel.Text);
                    return SimpleSelector.PseudoClass(el => el.ChildNodes.QuerySelector(sel) != null, code);
                }

                return null;
            }

            public override void Dispose()
            {
                base.Dispose();
                _nested.ToPool();
            }
        }

        sealed class MatchesFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _selector;

            public MatchesFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild();
            }

            protected override Boolean OnToken(CssToken token)
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
                    return SimpleSelector.PseudoClass(el => sel.Match(el), code);
                }

                return null;
            }

            public override void Dispose()
            {
                base.Dispose();
                _selector.ToPool();
            }
        }

        sealed class DirFunctionState : FunctionState
        {
            Boolean _valid;
            String _value;

            public DirFunctionState()
            {
                _valid = true;
                _value = null;
            }

            protected override Boolean OnToken(CssToken token)
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
                    return SimpleSelector.PseudoClass(el => el is IHtmlElement && _value.Isi(((IHtmlElement)el).Direction), code);
                }

                return null;
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
                    return SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Language.StartsWith(value, StringComparison.OrdinalIgnoreCase), code);
                }

                return null;
            }
        }

        sealed class ContainsFunctionState : FunctionState
        {
            Boolean _valid;
            String _value;

            public ContainsFunctionState()
            {
                _valid = true;
                _value = null;
            }

            protected override Boolean OnToken(CssToken token)
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
                    return SimpleSelector.PseudoClass(el => el.TextContent.Contains(_value), code);
                }

                return null;
            }
        }

        sealed class HostContextFunctionState : FunctionState
        {
            readonly CssSelectorConstructor _selector;

            public HostContextFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild();
            }

            protected override Boolean OnToken(CssToken token)
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
                    return SimpleSelector.PseudoClass(el =>
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

            public override void Dispose()
            {
                base.Dispose();
                _selector.ToPool();
            }
        }

        sealed class ChildFunctionState<T> : FunctionState
            where T : ChildSelector, ISelector, new()
        {
            readonly CssSelectorConstructor _parent;

            Boolean _valid;
            Int32 _step;
            Int32 _offset;
            Int32 _sign;
            ParseState _state;
            CssSelectorConstructor _nested;
            Boolean _allowOf;

            public ChildFunctionState(CssSelectorConstructor parent, Boolean withOptionalSelector = true)
            {
                _parent = parent;
                _allowOf = withOptionalSelector;
                _valid = true;
                _sign = 1;
                _state = ParseState.Initial;
            }

            public override ISelector Produce()
            {
                var invalid = !_valid || (_nested != null && !_nested.IsValid);
                var sel = _nested?.ToPool() ?? SimpleSelector.All;

                if (invalid)
                {
                    return null;
                }

                return new T().With(_step, _offset, sel);
            }

            protected override Boolean OnToken(CssToken token)
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

            Boolean OnAfterInitialSign(CssToken token)
            {
                if (token.Type == CssTokenType.Number)
                {
                    return OnOffset(token);
                }

                if (token.Type == CssTokenType.Dimension)
                {
                    var dim = (CssUnitToken)token;
                    _valid = _valid && dim.Unit.Isi("n") && Int32.TryParse(token.Data, out _step);
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

            Boolean OnAfter(CssToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _nested._state != State.Data)
                {
                    _nested.Apply(token);
                    return false;
                }

                return true;
            }

            Boolean OnBeforeOf(CssToken token)
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

            Boolean OnOffset(CssToken token)
            {
                if (token.Type == CssTokenType.Whitespace)
                {
                    return false;
                }

                if (token.Type == CssTokenType.Number)
                {
                    _valid = _valid && ((CssNumberToken)token).IsInteger && Int32.TryParse(token.Data, out _offset);
                    _offset *= _sign;
                    _state = ParseState.BeforeOf;
                    return false;
                }

                return OnBeforeOf(token);
            }

            Boolean OnInitial(CssToken token)
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

            enum ParseState : byte
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
