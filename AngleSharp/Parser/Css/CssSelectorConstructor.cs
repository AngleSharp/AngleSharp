namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
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

        static readonly Dictionary<String, ISelector> pseudoClassSelectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ISelector> pseudoElementSelectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Func<FunctionState>> pseudoClassFunctions = new Dictionary<String, Func<FunctionState>>(StringComparer.OrdinalIgnoreCase);

		State state;
        FunctionState function;
        ISelector temp;
		ListSelector group;
		ComplexSelector complex;
		Stack<CssCombinator> combinators;
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
            pseudoClassSelectors.Add(PseudoClassNames.Root, SimpleSelector.PseudoClass(el => el.Owner.DocumentElement == el, PseudoClassNames.Root));
            pseudoClassSelectors.Add(PseudoClassNames.OnlyType, SimpleSelector.PseudoClass(el => el.IsOnlyOfType(), PseudoClassNames.OnlyType));
            pseudoClassSelectors.Add(PseudoClassNames.FirstOfType, SimpleSelector.PseudoClass(el => el.IsFirstOfType(), PseudoClassNames.FirstOfType));
            pseudoClassSelectors.Add(PseudoClassNames.LastOfType, SimpleSelector.PseudoClass(el => el.IsLastOfType(), PseudoClassNames.LastOfType));
            pseudoClassSelectors.Add(PseudoClassNames.OnlyChild, SimpleSelector.PseudoClass(el => el.IsOnlyChild(), PseudoClassNames.OnlyChild));
            pseudoClassSelectors.Add(PseudoClassNames.FirstChild, SimpleSelector.PseudoClass(el => el.IsFirstChild(), PseudoClassNames.FirstChild));
            pseudoClassSelectors.Add(PseudoClassNames.LastChild, SimpleSelector.PseudoClass(el => el.IsLastChild(), PseudoClassNames.LastChild));
            pseudoClassSelectors.Add(PseudoClassNames.Empty, SimpleSelector.PseudoClass(el => el.ChildElementCount == 0 && el.TextContent.Equals(String.Empty), PseudoClassNames.Empty));
            pseudoClassSelectors.Add(PseudoClassNames.AnyLink, SimpleSelector.PseudoClass(el => el.IsLink() || el.IsVisited(), PseudoClassNames.AnyLink));
            pseudoClassSelectors.Add(PseudoClassNames.Link, SimpleSelector.PseudoClass(el => el.IsLink(), PseudoClassNames.Link));
            pseudoClassSelectors.Add(PseudoClassNames.Visited, SimpleSelector.PseudoClass(el => el.IsVisited(), PseudoClassNames.Visited));
            pseudoClassSelectors.Add(PseudoClassNames.Active, SimpleSelector.PseudoClass(el => el.IsActive(), PseudoClassNames.Active));
            pseudoClassSelectors.Add(PseudoClassNames.Hover, SimpleSelector.PseudoClass(el => el.IsHovered(), PseudoClassNames.Hover));
            pseudoClassSelectors.Add(PseudoClassNames.Focus, SimpleSelector.PseudoClass(el => el.IsFocused, PseudoClassNames.Focus));
            pseudoClassSelectors.Add(PseudoClassNames.Target, SimpleSelector.PseudoClass(el => el.IsTarget(), PseudoClassNames.Target));
            pseudoClassSelectors.Add(PseudoClassNames.Enabled, SimpleSelector.PseudoClass(el => el.IsEnabled(), PseudoClassNames.Enabled));
            pseudoClassSelectors.Add(PseudoClassNames.Disabled, SimpleSelector.PseudoClass(el => el.IsDisabled(), PseudoClassNames.Disabled));
            pseudoClassSelectors.Add(PseudoClassNames.Default, SimpleSelector.PseudoClass(el => el.IsDefault(), PseudoClassNames.Default));
            pseudoClassSelectors.Add(PseudoClassNames.Checked, SimpleSelector.PseudoClass(el => el.IsChecked(), PseudoClassNames.Checked));
            pseudoClassSelectors.Add(PseudoClassNames.Indeterminate, SimpleSelector.PseudoClass(el => el.IsIndeterminate(), PseudoClassNames.Indeterminate));
            pseudoClassSelectors.Add(PseudoClassNames.PlaceholderShown, SimpleSelector.PseudoClass(el => el.IsPlaceholderShown(), PseudoClassNames.PlaceholderShown));
            pseudoClassSelectors.Add(PseudoClassNames.Unchecked, SimpleSelector.PseudoClass(el => el.IsUnchecked(), PseudoClassNames.Unchecked));
            pseudoClassSelectors.Add(PseudoClassNames.Valid, SimpleSelector.PseudoClass(el => el.IsValid(), PseudoClassNames.Valid));
            pseudoClassSelectors.Add(PseudoClassNames.Invalid, SimpleSelector.PseudoClass(el => el.IsInvalid(), PseudoClassNames.Invalid));
            pseudoClassSelectors.Add(PseudoClassNames.Required, SimpleSelector.PseudoClass(el => el.IsRequired(), PseudoClassNames.Required));
            pseudoClassSelectors.Add(PseudoClassNames.ReadOnly, SimpleSelector.PseudoClass(el => el.IsReadOnly(), PseudoClassNames.ReadOnly));
            pseudoClassSelectors.Add(PseudoClassNames.ReadWrite, SimpleSelector.PseudoClass(el => el.IsEditable(), PseudoClassNames.ReadWrite));
            pseudoClassSelectors.Add(PseudoClassNames.InRange, SimpleSelector.PseudoClass(el => el.IsInRange(), PseudoClassNames.InRange));
            pseudoClassSelectors.Add(PseudoClassNames.OutOfRange, SimpleSelector.PseudoClass(el => el.IsOutOfRange(), PseudoClassNames.OutOfRange));
            pseudoClassSelectors.Add(PseudoClassNames.Optional, SimpleSelector.PseudoClass(el => el.IsOptional(), PseudoClassNames.Optional));

            pseudoElementSelectors.Add(PseudoElementNames.Before, SimpleSelector.PseudoElement(el => el.IsPseudo("::" + PseudoElementNames.Before), PseudoElementNames.Before));
            pseudoElementSelectors.Add(PseudoElementNames.After, SimpleSelector.PseudoElement(el => el.IsPseudo("::" + PseudoElementNames.After), PseudoElementNames.After));
            pseudoElementSelectors.Add(PseudoElementNames.Selection, SimpleSelector.PseudoElement(el => false, PseudoElementNames.Selection));
            pseudoElementSelectors.Add(PseudoElementNames.FirstLine, SimpleSelector.PseudoElement(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text, PseudoElementNames.FirstLine));
            pseudoElementSelectors.Add(PseudoElementNames.FirstLetter, SimpleSelector.PseudoElement(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text && el.ChildNodes[0].TextContent.Length > 0, PseudoElementNames.FirstLetter));

            // LEGACY STYLE OF DEFINING PSEUDO ELEMENTS - AS PSEUDO CLASS!
            pseudoClassSelectors.Add(PseudoElementNames.Before, pseudoElementSelectors[PseudoElementNames.Before]);
            pseudoClassSelectors.Add(PseudoElementNames.After, pseudoElementSelectors[PseudoElementNames.After]);
            pseudoClassSelectors.Add(PseudoElementNames.FirstLine, pseudoElementSelectors[PseudoElementNames.FirstLine]);
            pseudoClassSelectors.Add(PseudoElementNames.FirstLetter, pseudoElementSelectors[PseudoElementNames.FirstLetter]);

            pseudoClassFunctions.Add(PseudoClassNames.NthChild, () => new ChildFunctionState<NthFirstChildSelector>());
            pseudoClassFunctions.Add(PseudoClassNames.NthLastChild, () => new ChildFunctionState<NthLastChildSelector>());
            pseudoClassFunctions.Add(PseudoClassNames.NthOfType, () => new ChildFunctionState<NthFirstTypeSelector>());
            pseudoClassFunctions.Add(PseudoClassNames.NthLastOfType, () => new ChildFunctionState<NthLastTypeSelector>());
            pseudoClassFunctions.Add(PseudoClassNames.Not, () => new NotFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Dir, () => new DirFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Lang, () => new LangFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Contains, () => new ContainsFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Has, () => new HasFunctionState());
            pseudoClassFunctions.Add(PseudoClassNames.Matches, () => new MatchesFunctionState());
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new constructor object.
        /// </summary>
        public CssSelectorConstructor()
        {
            combinators = new Stack<CssCombinator>();
			Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the stored selector is valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return valid; }
        }

        /// <summary>
        /// Gets if the stored selector is nested below another selector.
        /// </summary>
        public Boolean IsNested
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the currently formed selector.
        /// </summary>
        public ISelector Result
        {
            get
            {
                if (!valid || !ready)
                    return null;

                if (complex != null)
                {
                    complex.ConcludeSelector(temp);
                    temp = complex;
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
        }

        #endregion

        #region Methods

        /// <summary>
        /// Picks a simple selector from the stream of tokens.
        /// </summary>
        /// <param name="token">The stream of tokens to consider.</param>
        public void Apply(CssToken token)
        {
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
                case State.PseudoClassFunction:
					OnPseudoClassFunction(token);
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
			temp = null;
			group = null;
			complex = null;
            valid = true;
            IsNested = false;
            ready = true;
            function = null;
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
            else if (token is CssMatchToken || token.Type == CssTokenType.Delim)
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
                function = GetPseudoFunction(token);

                if (function != null)
                {
                    ready = false;
                    state = State.PseudoClassFunction;
                    return;
                }
            }
            else if (token.Type == CssTokenType.Ident)
            {
                var sel = GetPseudoSelector(token);

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
                ISelector selector;

                if (pseudoElementSelectors.TryGetValue(token.Data, out selector))
                {
                    if (IsNested)
                        valid = false;

                    Insert(selector);
                    return;
                }

                Insert(SimpleSelector.PseudoElement(el => false, token.Data));
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

		/// <summary>
		/// Invoked once a pseudo class has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
		void OnPseudoClassFunction(CssToken token)
		{
            if (function.Finished(token))
            {
                var sel = function.Produce();

                if (IsNested && function is NotFunctionState)
                    sel = null;

                state = State.Data;
                ready = true;
                function = null;

                if (sel != null)
                    Insert(sel);
                else
                    valid = false;
            }
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
                var combinator = combinators.Pop();

                //Care about combinator combinations, such as >>
                if (combinator == CssCombinator.Child && combinators.Peek() == CssCombinator.Child)
                {
                    combinators.Pop();
                    combinator = CssCombinator.Descendent;
                }

                //Remove all leading whitespaces, invalid if mixed
                while (combinators.Count > 0)
                    valid = combinators.Pop() == CssCombinator.Descendent && valid;

                return combinator;
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
				case Specification.Comma:
					InsertOr();
                    ready = false;
                    break;

                case Specification.GreaterThan:
					Insert(CssCombinator.Child);
                    ready = false;
                    break;

                case Specification.Plus:
					Insert(CssCombinator.AdjacentSibling);
                    ready = false;
                    break;

                case Specification.Tilde:
					Insert(CssCombinator.Sibling);
                    ready = false;
                    break;

                case Specification.Asterisk:
					Insert(SimpleSelector.All);
                    ready = true;
                    break;

                case Specification.Dot:
					state = State.Class;
                    ready = false;
                    break;

                case Specification.Pipe:
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
		/// <returns>The created selector.</returns>
		ISelector GetPseudoSelector(CssToken token)
		{
            ISelector selector;

            if (pseudoClassSelectors.TryGetValue(token.Data, out selector))
                return selector;

			return null;
		}

        /// <summary>
        /// Invoked once a colon with an identifier has been found in the token enumerator.
        /// </summary>
        /// <returns>The created function state.</returns>
        FunctionState GetPseudoFunction(CssToken token)
        {
            Func<FunctionState> creator;

            if (pseudoClassFunctions.TryGetValue(token.Data, out creator))
                return creator();

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
			PseudoClassFunction,
			PseudoClassFunctionEnd,
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
                var sel = _nested.ToPool();

                if (sel == null)
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
                var sel = _nested.ToPool();

                if (sel == null)
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
                var sel = _nested.ToPool();

                if (sel == null)
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

        sealed class ChildFunctionState<T> : FunctionState
            where T : NthChildSelector, ISelector, new()
        {
            Boolean valid;
            String value;

            public ChildFunctionState()
            {
                valid = true;
                value = String.Empty;
            }

            public override ISelector Produce()
            {
                if (!valid)
                    return null;

                var selector = new T();

                if (value.Equals("odd", StringComparison.OrdinalIgnoreCase))
                {
                    selector.step = 2;
                    selector.offset = 1;
                }
                else if (value.Equals("even", StringComparison.OrdinalIgnoreCase))
                {
                    selector.step = 2;
                    selector.offset = 0;
                }
                else if (!Int32.TryParse(value, out selector.offset))
                {
                    var index = value.IndexOf("n", StringComparison.OrdinalIgnoreCase);

                    if (value.Length > 0 && index != -1)
                    {
                        var first = value.Substring(0, index).Replace(" ", "");
                        var second = value.Substring(index + 1).Replace(" ", "");

                        if (first == String.Empty || (first.Length == 1 && first[0] == Specification.Plus))
                            selector.step = 1;
                        else if (first.Length == 1 && first[0] == Specification.Minus)
                            selector.step = -1;
                        else if (!Int32.TryParse(first, out selector.step))
                            throw new DomException(ErrorCode.Syntax);

                        if (second == String.Empty)
                            selector.offset = 0;
                        else if (!Int32.TryParse(second, out selector.offset))
                            return null;
                    }
                    else
                        return null;
                }

                return selector;
            }

            protected override Boolean OnToken(CssToken token)
            {
                switch (token.Type)
                {
                    case CssTokenType.Whitespace:
                        return false;
                    case CssTokenType.Ident:
                    case CssTokenType.Number:
                    case CssTokenType.Dimension:
                        value += token.ToValue();
                        return false;

                    case CssTokenType.Delim:
                        var chr = token.Data[0];

                        if (chr == Specification.Plus || chr == Specification.Minus)
                        {
                            value += token.Data;
                            return false;
                        }

                        break;

                    case CssTokenType.RoundBracketClose:
                        return true;
                }

                valid = false;
                return false;
            }
        }

        #endregion

		#region Nested

        abstract class NthChildSelector
        {
            public Int32 step;
            public Int32 offset;

            public Priority Specifity
            {
                get { return Priority.OneClass; }
            }

            protected String Stringify(String name)
            {
                var a = step.ToString();
                var b = String.Empty;

                if (offset > 0)
                    b = "+" + offset.ToString();
                else if (offset < 0)
                    b = offset.ToString();

                return String.Format(":{0}({1}n{2})", name, a, b);
            }
        }

		/// <summary>
		/// The nth-child selector.
		/// </summary>
        sealed class NthFirstChildSelector : NthChildSelector, ISelector
        {
			public Boolean Match(IElement element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;
                else if (step == 0)
                    return parent.ChildElementCount >= offset && offset > 0 && parent.Children[offset - 1] == element;

                var n = Math.Sign(step);

                for (var i = 0; i < parent.ChildElementCount; i++)
                {
                    if (parent.Children[i] == element)
                    {
                        var diff = i + 1 - offset;
                        return diff == 0 || (Math.Sign(diff) == n && diff % step == 0);
                    }
                }

                return false;
            }

            public String Text
            {
                get { return Stringify(PseudoClassNames.NthChild); }
            }
        }

        /// <summary>
        /// The nth-of-type selector.
        /// </summary>
        sealed class NthFirstTypeSelector : NthChildSelector, ISelector
        {
            public Boolean Match(IElement element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                var n = Math.Sign(step);
                var k = 0;

                for (var i = 0; i < parent.Children.Length; i++)
                {
                    if (parent.Children[i].NodeName != element.NodeName)
                        continue;

                    k++;

                    if (parent.Children[i] == element)
                    {
                        var diff = k - offset;
                        return diff == 0 || (Math.Sign(diff) == n && diff % step == 0);
                    }
                }

                return false;
            }

            public String Text
            {
                get { return Stringify(PseudoClassNames.NthOfType); }
            }
        }

        /// <summary>
        /// The nth-lastchild selector.
        /// </summary>
        sealed class NthLastChildSelector : NthChildSelector, ISelector
        {
            public Boolean Match(IElement element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;
                else if (step == 0)
                    return parent.ChildElementCount >= offset && offset > 0 && parent.Children[parent.ChildElementCount - offset] == element;

                var n = Math.Sign(step);

                for (var i = parent.ChildElementCount - 1; i >= 0; i--)
                {
                    if (parent.Children[i] == element)
                    {
                        var diff = i + 1 - offset;
                        return diff == 0 || (Math.Sign(diff) == n && diff % step == 0);
                    }
                }

                return false;
            }

            public String Text
            {
                get { return Stringify(PseudoClassNames.NthLastChild); }
            }
        }

        /// <summary>
        /// The nth-last-of-type selector.
        /// </summary>
        sealed class NthLastTypeSelector : NthChildSelector, ISelector
        {
            public Boolean Match(IElement element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                var n = Math.Sign(step);
                var k = 0;

                for (var i = parent.Children.Length - 1; i >= 0; i--)
                {
                    if (parent.Children[i].NodeName != element.NodeName)
                        continue;

                    k++;

                    if (parent.Children[i] == element)
                    {
                        var diff = k - offset;
                        return diff == 0 || (Math.Sign(diff) == n && diff % step == 0);
                    }
                }

                return false;
            }

            public String Text
            {
                get { return Stringify(PseudoClassNames.NthLastOfType); }
            }
        }

        #endregion
    }
}
