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
    sealed class CssSelectorConstructor(CssTokenizer tokenizer, IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector, Boolean invoked = false, Boolean forgiving = false)
    {
        #region Fields

        private static readonly Dictionary<String, Func<CssSelectorConstructor, FunctionState>> pseudoClassFunctions = new(StringComparer.OrdinalIgnoreCase)
        {
            { PseudoClassNames.NthChild, ctx => new ChildFunctionState((step, offset, kind) => new FirstChildSelector(step, offset, kind), ctx, withOptionalSelector: true) },
            { PseudoClassNames.NthLastChild, ctx => new ChildFunctionState((step, offset, kind) => new LastChildSelector(step, offset, kind), ctx, withOptionalSelector: true) },
            { PseudoClassNames.NthOfType, ctx => new ChildFunctionState((step, offset, kind) => new FirstTypeSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthLastOfType, ctx => new ChildFunctionState((step, offset, kind) => new LastTypeSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthColumn, ctx => new ChildFunctionState((step, offset, kind) => new FirstColumnSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.NthLastColumn, ctx => new ChildFunctionState((step, offset, kind) => new LastColumnSelector(step, offset, kind), ctx, withOptionalSelector: false) },
            { PseudoClassNames.Not, ctx => new NotFunctionState(ctx) },
            { PseudoClassNames.Dir, _ => new DirFunctionState() },
            { PseudoClassNames.Lang, _ => new LangFunctionState() },
            { PseudoClassNames.Contains, _ => new ContainsFunctionState() },
            { PseudoClassNames.Has, ctx => new HasFunctionState(ctx) },
            { PseudoClassNames.Is, ctx => new IsFunctionState(ctx) },
            { PseudoClassNames.Matches, ctx => new MatchesFunctionState(ctx) },
            { PseudoClassNames.Where, ctx => new WhereFunctionState(ctx) },
            { PseudoClassNames.HostContext, ctx => new HostContextFunctionState(ctx) },
        };

        private readonly CssTokenizer _tokenizer = tokenizer;
        private readonly Stack<CssCombinator> _combinators = new Stack<CssCombinator>();
        private readonly IAttributeSelectorFactory _attributeSelector = attributeSelector;
        private readonly IPseudoElementSelectorFactory _pseudoElementSelector = pseudoElementSelector;
        private readonly IPseudoClassSelectorFactory _pseudoClassSelector = pseudoClassSelector;

        private State _state = State.Data;
        private ISelector? _temp;
        private ListSelector? _group;
        private ComplexSelector? _complex;
        private String? _attrName;
        private String? _attrValue;
        private Boolean _attrInsensitive = false;
        private String _attrOp = String.Empty;
        private String? _attrNs;
        private Boolean _valid = true;
        private Boolean _nested;
        private Boolean _ready = true;
        private FunctionState? _function;
        private Boolean _invoked = invoked;
        private Boolean _forgiving = forgiving;

        #endregion

        #region Properties

        public Boolean IsValid => _invoked && _valid && _ready;

        public Boolean IsNested => _nested;

        #endregion

        #region Methods

        public ISelector? Parse()
        {
            var token = _tokenizer.Get();

            while (token.Type != CssTokenType.EndOfFile)
            {
                Apply(token);
                token = _tokenizer.Get();
            }

            return GetResult();
        }

        private ISelector? GetResult()
        {
            if (IsValid)
            {
                if (_complex is not null)
                {
                    _complex.ConcludeSelector(_temp!);
                    _temp = _complex;
                    _complex = null;
                }

                if (_group is null || _group.Length == 0)
                {
                    return _temp ?? AllSelector.Instance;
                }
                else if (_temp is null && _group.Length == 1)
                {
                    return _group[0];
                }

                if (_temp is not null)
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
                    Insert(CssCombinator.Descendant);
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

                case CssTokenType.Descendant:
                    Insert(CssCombinator.Descendant);
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
                else if (token.Type == CssTokenType.Delim && token.Data is "*")
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
                if (token.Type is CssTokenType.Ident or CssTokenType.String or CssTokenType.Number)
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
            if (!_attrInsensitive && token.Type == CssTokenType.Ident && token.Data is "i")
            {
                _attrInsensitive = true;
            }
            else if (token.Type != CssTokenType.Whitespace)
            {
                _state = State.Data;
                _ready = true;

                if (token.Type == CssTokenType.SquareBracketClose)
                {
                    var selector = _attributeSelector.Create(_attrOp, _attrName!, _attrValue!, _attrNs, _attrInsensitive);
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
                else if (_forgiving)
                {
                    return;
                }
            }
            else if (token.Type == CssTokenType.Ident)
            {
                var sel = _pseudoClassSelector.Create(token.Data);

                if (sel is not null)
                {
                    Insert(sel);
                    return;
                }
                else if (_forgiving)
                {
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

                if (sel is not null)
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
            if (_temp is not null)
            {
                _group ??= [];

                if (_complex is not null)
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
            if (_temp is not null)
            {
                if (_combinators.Count == 0)
                {
                    if (_temp is not CompoundSelector compound)
                    {
                        compound = [_temp];
                    }

                    compound.Add(selector);
                    _temp = compound;
                }
                else
                {
                    _complex ??= new ComplexSelector();

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
            while (_combinators.Count > 1 && _combinators.Peek() == CssCombinator.Descendant)
            {
                _combinators.Pop();
            }

            if (_combinators.Count > 1)
            {
                var last = _combinators.Pop();

                //Remove all leading whitespaces, invalid if mixed
                while (_combinators.Count > 0)
                {
                    _valid = _combinators.Pop() == CssCombinator.Descendant && _valid;
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
                    if (_combinators.Count > 0 && _combinators.Peek() == CssCombinator.Descendant)
                    {
                        Insert(new TypeSelector(String.Empty));
                    }

                    Insert(CssCombinator.Namespace);
                    _ready = false;
                    break;

                case Symbols.Ampersand:
                    // The basic idea here is to also progressively support
                    // AngleSharp.Css without requiring AngleSharp.Css to use 1.1.0
                    // of AngleSharp; instead this architecture also works (without
                    // nested support) for AngleSharp.Css with AngleSharp 1.0.0.
                    Insert(_attributeSelector.Create("&", String.Empty, String.Empty, null, true));
                    _ready = true;
                    break;

                default:
                    _valid = false;
                    break;
            }
        }

        private void OnFunctionState(CssSelectorToken token)
        {
            if (_function!.Finished(token))
            {
                var sel = _function.Produce();

                if (_nested && _function is NotFunctionState)
                {
                    sel = null;
                }

                _function = null;
                _state = State.Data;
                _ready = true;

                if (sel is not null)
                {
                    Insert(sel);
                    return;
                }

                _valid = false;
            }
        }

        private CssSelectorConstructor CreateChild(Boolean forgiving) => new(_tokenizer, _attributeSelector, _pseudoClassSelector, _pseudoElementSelector, true, forgiving);

        #endregion

        #region State-Machine

        /// <summary>
        /// The various parsing states.
        /// </summary>
        private enum State : Byte
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

            public abstract ISelector? Produce();

            protected abstract Boolean OnToken(CssSelectorToken token);

            protected Priority ResolveMostSpecificParameter(ISelector parameter) =>
                parameter is ListSelector list
                    ? list.Max(x => x.Specificity)
                    : parameter.Specificity;
        }

        private sealed class NotFunctionState : FunctionState
        {
            private readonly CssSelectorConstructor _selector;

            public NotFunctionState(CssSelectorConstructor parent)
            {
                _selector = parent.CreateChild(false);
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

            public override ISelector? Produce()
            {
                var valid = _selector.IsValid;
                var sel = _selector.GetResult();

                if (valid)
                {
                    var code = PseudoClassNames.Not.CssFunction(sel!.Text);
                    var specificity = ResolveMostSpecificParameter(sel);
                    return new PseudoClassSelector(el => !sel.Match(el), code, specificity);
                }

                return null;
            }
        }

        private sealed class HasFunctionState(CssSelectorConstructor parent) : FunctionState
        {
            private readonly CssSelectorConstructor _nested = parent.CreateChild(false);
            private Boolean _firstToken = true;
            private Boolean _matchSiblings = false;

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

            public override ISelector? Produce()
            {
                var valid = _nested.IsValid;
                var sel = _nested.GetResult();
                var selText = sel!.Text;
                var matchSiblings = _matchSiblings || selText.Contains(":" + PseudoClassNames.Scope);

                if (valid)
                {
                    var code = PseudoClassNames.Has.CssFunction(selText);
                    var specificity = ResolveMostSpecificParameter(sel);

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

                        elements ??= Array.Empty<IElement>();

                        return sel.MatchAny(elements, el) is not null;
                    }, code, specificity);
                }

                return null;
            }
        }

        /// <summary>
        /// Base implementation for :matches(), :is(), :where().
        /// This is resulting in a "forgiving" list.
        /// </summary>
        private abstract class BaseMatchingFunctionState(CssSelectorConstructor parent) : FunctionState
        {
            private readonly CssSelectorConstructor _selector = parent.CreateChild(true);

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _selector._state != State.Data)
                {
                    _selector.Apply(token);
                    return false;
                }

                return true;
            }

            protected abstract String Name { get; }

            protected abstract Priority DecideSpecificity(ISelector innerSelector);

            public override ISelector? Produce()
            {
                var valid = _selector.IsValid;
                var sel = _selector.GetResult();

                if (valid)
                {
                    var code = Name.CssFunction(sel!.Text);
                    var specificity = DecideSpecificity(sel);
                    return new PseudoClassSelector(sel.Match, code, specificity);
                }

                return null;
            }
        }

        private sealed class MatchesFunctionState(CssSelectorConstructor parent) : BaseMatchingFunctionState(parent)
        {
            protected override String Name => PseudoClassNames.Matches;

            protected override Priority DecideSpecificity(ISelector innerSelector) => ResolveMostSpecificParameter(innerSelector);
        }

        private sealed class IsFunctionState(CssSelectorConstructor parent) : BaseMatchingFunctionState(parent)
        {
            protected override String Name => PseudoClassNames.Is;

            protected override Priority DecideSpecificity(ISelector innerSelector) => ResolveMostSpecificParameter(innerSelector);
        }

        private sealed class WhereFunctionState(CssSelectorConstructor parent) : BaseMatchingFunctionState(parent)
        {
            protected override String Name => PseudoClassNames.Where;

            protected override Priority DecideSpecificity(ISelector innerSelector) => Priority.Zero;
        }

        private sealed class DirFunctionState : FunctionState
        {
            private Boolean _valid;
            private String ?_value;

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

            public override ISelector? Produce()
            {
                if (_valid && _value is not null)
                {
                    var code = PseudoClassNames.Dir.CssFunction(_value);
                    return new PseudoClassSelector(el => el is IHtmlElement htmlEl && _value.Isi(htmlEl.Direction), code);
                }

                return null;
            }
        }

        private sealed class LangFunctionState : FunctionState
        {
            private Boolean valid;
            private String? value;

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

            public override ISelector? Produce()
            {
                if (valid && value is not null)
                {
                    var code = PseudoClassNames.Lang.CssFunction(value);
                    return new PseudoClassSelector(el => el is IHtmlElement htmlEl && htmlEl.Language!.StartsWith(value, StringComparison.OrdinalIgnoreCase), code);
                }

                return null;
            }
        }

        private sealed class ContainsFunctionState : FunctionState
        {
            private Boolean _valid;
            private String? _value;

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

            public override ISelector? Produce()
            {
                if (_valid && _value is not null)
                {
                    var code = PseudoClassNames.Contains.CssFunction(_value);
                    return new PseudoClassSelector(el => el.TextContent.Contains(_value), code);
                }

                return null;
            }
        }

        private sealed class HostContextFunctionState(CssSelectorConstructor parent) : FunctionState
        {
            private readonly CssSelectorConstructor _selector = parent.CreateChild(false);

            protected override Boolean OnToken(CssSelectorToken token)
            {
                if (token.Type != CssTokenType.RoundBracketClose || _selector._state != State.Data)
                {
                    _selector.Apply(token);
                    return false;
                }

                return true;
            }

            public override ISelector? Produce()
            {
                var valid = _selector.IsValid;
                var sel = _selector.GetResult();

                if (valid)
                {
                    var code = PseudoClassNames.HostContext.CssFunction(sel!.Text);
                    return new PseudoClassSelector(el =>
                    {
                        var shadowRoot = el.Parent as IShadowRoot;
                        var host = shadowRoot?.Host;

                        while (host is not null)
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

        private sealed class ChildFunctionState(Func<Int32, Int32, ISelector, ISelector> creator, CssSelectorConstructor parent, Boolean withOptionalSelector = true) : FunctionState
        {
            private readonly CssSelectorConstructor _parent = parent;

            private Boolean _valid = true;
            private Int32 _step;
            private Int32 _offset;
            private Int32 _sign = 1;
            private ParseState _state = ParseState.Initial;
            private CssSelectorConstructor? _nested;
            private readonly Boolean _allowOf = withOptionalSelector;
            private readonly Func<Int32, Int32, ISelector, ISelector> _creator = creator;

            public override ISelector? Produce()
            {
                var invalid = !_valid || (_nested is not null && !_nested.IsValid);

                if (!invalid)
                {
                    var sel = _nested?.GetResult() ?? AllSelector.Instance;
                    return _creator.Invoke(_step, _offset, sel);
                }

                return null;
            }

            protected override Boolean OnToken(CssSelectorToken token)
            {
                return _state switch
                {
                    ParseState.Initial          => OnInitial(token),
                    ParseState.AfterInitialSign => OnAfterInitialSign(token),
                    ParseState.Offset           => OnOffset(token),
                    ParseState.BeforeOf         => OnBeforeOf(token),
                    _                           => OnAfter(token)
                };
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
                if (token.Type != CssTokenType.RoundBracketClose || _nested!._state != State.Data)
                {
                    _nested!.Apply(token);
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
                    _nested = _parent.CreateChild(false);
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
                    _sign = token.Data is "-" ? -1 : +1;
                    _state = ParseState.AfterInitialSign;
                    return false;
                }

                return OnAfterInitialSign(token);
            }

            private enum ParseState : Byte
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
