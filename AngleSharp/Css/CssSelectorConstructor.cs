namespace AngleSharp.Css
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Class for construction for CSS selectors as specified in
    /// http://www.w3.org/html/wg/drafts/html/master/selectors.html.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssSelectorConstructor
    {
        #region Constants

        static readonly String NTH_CHILD_ODD = "odd";
        static readonly String NTH_CHILD_EVEN = "even";
        static readonly String NTH_CHILD_N = "n";

        const String PSEUDOCLASS_ROOT = "root";
        const String PSEUDOCLASS_FIRSTOFTYPE = "first-of-type";
        const String PSEUDOCLASS_LASTOFTYPE = "last-of-type";
        const String PSEUDOCLASS_ONLYCHILD = "only-child";
        const String PSEUDOCLASS_FIRSTCHILD = "first-child";
        const String PSEUDOCLASS_LASTCHILD = "last-child";
        const String PSEUDOCLASS_EMPTY = "empty";
        const String PSEUDOCLASS_LINK = "link";
        const String PSEUDOCLASS_VISITED = "visited";
        const String PSEUDOCLASS_ACTIVE = "active";
        const String PSEUDOCLASS_HOVER = "hover";
        const String PSEUDOCLASS_FOCUS = "focus";
        const String PSEUDOCLASS_TARGET = "target";
        const String PSEUDOCLASS_ENABLED = "enabled";
        const String PSEUDOCLASS_DISABLED = "disabled";
        const String PSEUDOCLASS_CHECKED = "checked";
        const String PSEUDOCLASS_UNCHECKED = "unchecked";
        const String PSEUDOCLASS_INDETERMINATE = "indeterminate";
        const String PSEUDOCLASS_DEFAULT = "default";

        const String PSEUDOCLASS_VALID = "valid";
        const String PSEUDOCLASS_INVALID = "invalid";
        const String PSEUDOCLASS_REQUIRED = "required";
        const String PSEUDOCLASS_INRANGE = "in-range";
        const String PSEUDOCLASS_OUTOFRANGE = "out-of-range";
        const String PSEUDOCLASS_OPTIONAL = "optional";
        const String PSEUDOCLASS_READONLY = "read-only";
        const String PSEUDOCLASS_READWRITE = "read-write";

        const String PSEUDOCLASSFUNCTION_DIR = "dir";
        const String PSEUDOCLASSFUNCTION_NTHCHILD = "nth-child";
        const String PSEUDOCLASSFUNCTION_NTHLASTCHILD = "nth-last-child";
        const String PSEUDOCLASSFUNCTION_NOT = "not";
        const String PSEUDOCLASSFUNCTION_LANG = "lang";
        const String PSEUDOCLASSFUNCTION_CONTAINS = "contains";

        const String PSEUDOELEMENT_BEFORE = "before";
        const String PSEUDOELEMENT_AFTER = "after";
        const String PSEUDOELEMENT_SELECTION = "selection";
        const String PSEUDOELEMENT_FIRSTLINE = "first-line";
        const String PSEUDOELEMENT_FIRSTLETTER = "first-letter";

        #endregion

        #region Members

		State state;
        Selector temp;
		ListSelector group;
		ComplexSelector complex;
        Boolean hasCombinator;
        Boolean ignoreErrors;
		CssCombinator combinator;
		CssSelectorConstructor nested;
		String attrName;
		String attrValue;
		String attrOp;

        #endregion

        #region ctor

		/// <summary>
		/// Creates a new constructor object.
		/// </summary>
        public CssSelectorConstructor()
        {
			Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if errors should be ignored.
        /// </summary>
        public Boolean IgnoreErrors
        {
            get { return ignoreErrors; }
            set { ignoreErrors = value; }
        }

        /// <summary>
        /// Gets the currently formed selector.
        /// </summary>
        public Selector Result
        {
            get
            {
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
                    group.AppendSelector(temp);
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
				case State.PseudoClassFunctionEnd:
					OnPseudoClassFunctionEnd(token);
					break;
				case State.PseudoElement:
					OnPseudoElement(token);
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
			attrOp = String.Empty;
			state = State.Data;
			combinator = CssCombinator.Descendent;
			hasCombinator = false;
			ignoreErrors = false;
			temp = null;
			group = null;
			complex = null;
			return this;
		}

		#endregion

		#region States

		/// <summary>
		/// General state.
		/// </summary>
		/// <param name="token">The token to examine.</param>
		void OnData(CssToken token)
		{
			switch (token.Type)
			{
				//Begin of attribute [A]
				case CssTokenType.SquareBracketOpen:
					attrName = null;
					attrValue = null;
					attrOp = String.Empty;
					state = State.Attribute;
					return;

				//Begin of Pseudo :P
				case CssTokenType.Colon:
					state = State.PseudoClass;
					return;

				//Begin of ID #I
				case CssTokenType.Hash:
					Insert(SimpleSelector.Id(((CssKeywordToken)token).Data));
					return;

				//Begin of Type E
				case CssTokenType.Ident:
					Insert(SimpleSelector.Type(((CssKeywordToken)token).Data));
					return;

				//Whitespace could be significant
				case CssTokenType.Whitespace:
					Insert(CssCombinator.Descendent);
					return;

				//Various
				case CssTokenType.Delim:
					OnDelim(token);
					return;

				case CssTokenType.Comma:
					InsertOr();
					return;
			}

			if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnAttribute(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;

			state = State.AttributeOperator;

			if (token.Type == CssTokenType.Ident)
				attrName = ((CssKeywordToken)token).Data;
			else if (token.Type == CssTokenType.String)
				attrName = ((CssStringToken)token).Data;
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
			else
				state = State.Data;
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnAttributeOperator(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;

			state = State.AttributeValue;

			if (token.Type == CssTokenType.SquareBracketClose)
				OnAttributeEnd(token);
			else if (token is CssMatchToken || token.Type == CssTokenType.Delim)
				attrOp = token.ToValue();
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
			else
				state = State.AttributeEnd;
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnAttributeValue(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;

			state = State.AttributeEnd;

			if (token.Type == CssTokenType.Ident)
				attrValue = ((CssKeywordToken)token).Data;
			else if (token.Type == CssTokenType.String)
				attrValue = ((CssStringToken)token).Data;
			else if (token.Type == CssTokenType.Number)
				attrValue = ((CssNumberToken)token).Data.ToString();
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
			else
				state = State.Data;
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

			if (token.Type == CssTokenType.SquareBracketClose)
			{
				switch (attrOp)
				{
					case "=":
						Insert(SimpleSelector.AttrMatch(attrName, attrValue));
						break;
					case "~=":
						Insert(SimpleSelector.AttrList(attrName, attrValue));
						break;
					case "|=":
						Insert(SimpleSelector.AttrHyphen(attrName, attrValue));
						break;
					case "^=":
						Insert(SimpleSelector.AttrBegins(attrName, attrValue));
						break;
					case "$=":
						Insert(SimpleSelector.AttrEnds(attrName, attrValue));
						break;
					case "*=":
						Insert(SimpleSelector.AttrContains(attrName, attrValue));
						break;
					case "!=":
						Insert(SimpleSelector.AttrNotMatch(attrName, attrValue));
						break;
					default:
						Insert(SimpleSelector.AttrAvailable(attrName));
						break;
				}
			}
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnPseudoClass(CssToken token)
		{
			state = State.Data;

			if (token.Type == CssTokenType.Colon)
				state = State.PseudoElement;
			else if (token.Type == CssTokenType.Function)
			{
				attrName = ((CssKeywordToken)token).Data;
				attrValue = String.Empty;
				state = State.PseudoClassFunction;

				if (nested != null)
					nested.Reset();
			}
			else if (token.Type == CssTokenType.Ident)
			{
				var sel = GetPseudoSelector(token);

				if (sel != null)
					Insert(sel); 
				else if (!ignoreErrors)
					throw new DOMException(ErrorCode.SyntaxError);
			}
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnPseudoElement(CssToken token)
		{
			if (token.Type == CssTokenType.Ident)
			{
				var data = ((CssKeywordToken)token).Data;

				switch (data)
				{
					case PSEUDOELEMENT_BEFORE:
						Insert(SimpleSelector.PseudoElement(MatchBefore, PSEUDOELEMENT_BEFORE));
						break;
					case PSEUDOELEMENT_AFTER:
						Insert(SimpleSelector.PseudoElement(MatchAfter, PSEUDOELEMENT_AFTER));
						break;
					case PSEUDOELEMENT_SELECTION:
						Insert(SimpleSelector.PseudoElement(el => true, PSEUDOELEMENT_SELECTION));
						break;
					case PSEUDOELEMENT_FIRSTLINE:
						Insert(SimpleSelector.PseudoElement(MatchFirstLine, PSEUDOELEMENT_FIRSTLINE));
						break;
					case PSEUDOELEMENT_FIRSTLETTER:
						Insert(SimpleSelector.PseudoElement(MatchFirstLetter, PSEUDOELEMENT_FIRSTLETTER));
						break;
					default: 
						if (!ignoreErrors)
							throw new DOMException(ErrorCode.SyntaxError);

						Insert(SimpleSelector.PseudoElement(el => false, data));
						break;
				}
			}
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnClass(CssToken token)
		{
			state = State.Data;

			if (token.Type == CssTokenType.Ident)
				Insert(SimpleSelector.Class(((CssKeywordToken)token).Data));
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
		}

		/// <summary>
		/// Invoked once a pseudo class has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnPseudoClassFunction(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return;

			switch (attrName)
			{
				case PSEUDOCLASSFUNCTION_NTHCHILD:
				case PSEUDOCLASSFUNCTION_NTHLASTCHILD:
				{
					switch (token.Type)
					{
						case CssTokenType.Ident:
						case CssTokenType.Number:
						case CssTokenType.Dimension:
							attrValue += token.ToValue();
							return;

						case CssTokenType.Delim:
							var chr = ((CssDelimToken)token).Data;

							if (chr == Specification.PLUS || chr == Specification.MINUS)
							{
								attrValue += chr;
								return;
							}

							break;
					}

					break;
				}
				case PSEUDOCLASSFUNCTION_NOT:
				{
					if (nested == null)
						nested = new CssSelectorConstructor();

					if (token.Type != CssTokenType.RoundBracketClose || nested.state != State.Data)
					{
						nested.Apply(token);
						return;
					}

					break;
				}
				case PSEUDOCLASSFUNCTION_DIR:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
				case PSEUDOCLASSFUNCTION_LANG:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
				case PSEUDOCLASSFUNCTION_CONTAINS:
				{
					if (token.Type == CssTokenType.String)
						attrValue = ((CssStringToken)token).Data;
					else if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
			}

			OnPseudoClassFunctionEnd(token);
		}

		/// <summary>
		/// Invoked once a pseudo class has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnPseudoClassFunctionEnd(CssToken token)
		{
			state = State.Data;

			if (token.Type == CssTokenType.RoundBracketClose)
			{
				switch (attrName)
				{
					case PSEUDOCLASSFUNCTION_NTHCHILD:
					{
						Insert(GetChildSelector<NthFirstChildSelector>());
						break;
					}
					case PSEUDOCLASSFUNCTION_NTHLASTCHILD:
					{
						Insert(GetChildSelector<NthLastChildSelector>());
						break;
					}
					case PSEUDOCLASSFUNCTION_NOT:
					{
						var sel = nested.Result;
						var code = String.Format("{0}({1})", PSEUDOCLASSFUNCTION_NOT, sel.ToCss());
						Insert(SimpleSelector.PseudoClass(el => !sel.Match(el), code));
						break;
					}
					case PSEUDOCLASSFUNCTION_DIR:
					{
						var code = String.Format("{0}({1})", PSEUDOCLASSFUNCTION_DIR, attrValue);
						var dirCode = attrValue == "ltr" ? DirectionMode.Ltr : DirectionMode.Rtl;
						Insert(SimpleSelector.PseudoClass(el => el.Dir == dirCode, code));
						break;
					}
					case PSEUDOCLASSFUNCTION_LANG:
					{
						var code = String.Format("{0}({1})", PSEUDOCLASSFUNCTION_LANG, attrValue);
						Insert(SimpleSelector.PseudoClass(el => el.Lang.StartsWith(attrValue, StringComparison.OrdinalIgnoreCase), code));
						break;
					}
					case PSEUDOCLASSFUNCTION_CONTAINS:
					{
						var code = String.Format("{0}({1})", PSEUDOCLASSFUNCTION_CONTAINS, attrValue);
						Insert(SimpleSelector.PseudoClass(el => el.TextContent.Contains(attrValue), code));
						break;
					}
				}
			}
			else if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
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
                    group.AppendSelector(complex);
                    complex = null;
                }
                else
                    group.AppendSelector(temp);

                temp = null;
            }
        }

        /// <summary>
        /// Inserts the given selector.
        /// </summary>
        /// <param name="selector">The selector to insert.</param>
        void Insert(Selector selector)
        {
            if (temp != null)
            {
                if (!hasCombinator)
                {
                    var compound = temp as CompoundSelector;

                    if (compound == null)
                    {
                        compound = new CompoundSelector();
                        compound.AppendSelector(temp);
                    }

                    compound.AppendSelector(selector);
                    temp = compound;
                }
                else
                {
                    if (complex == null)
                        complex = new ComplexSelector();

                    complex.AppendSelector(temp, combinator);
                    combinator = CssCombinator.Descendent;
                    hasCombinator = false;
                    temp = selector;
                }
            }
            else
            {
                combinator = CssCombinator.Descendent;
                hasCombinator = false;
                temp = selector;
            }
        }

        /// <summary>
        /// Inserts the given combinator.
        /// </summary>
        /// <param name="cssCombinator">The combinator to insert.</param>
        void Insert(CssCombinator cssCombinator)
        {
            hasCombinator = true;

            if (cssCombinator != CssCombinator.Descendent)
                combinator = cssCombinator;
        }

		#endregion

		#region Substates

		/// <summary>
		/// Invoked once a delimiter has been found in the token enumerator.
		/// </summary>
		/// <param name="token">The token.</param>
		void OnDelim(CssToken token)
		{
			switch (((CssDelimToken)token).Data)
			{
				case Specification.COMMA:
					InsertOr();
					return;

				case Specification.GT:
					Insert(CssCombinator.Child);
					return;

				case Specification.PLUS:
					Insert(CssCombinator.AdjacentSibling);
					return;

				case Specification.TILDE:
					Insert(CssCombinator.Sibling);
					return;

				case Specification.ASTERISK:
					Insert(SimpleSelector.All);
					return;

				case Specification.DOT:
					state = State.Class;
					return;
			}

			if (!ignoreErrors)
				throw new DOMException(ErrorCode.SyntaxError);
		}

        /// <summary>
        /// Takes string and transforms it into the arguments for the nth-child function.
        /// </summary>
        /// <returns>The function.</returns>
        SimpleSelector GetChildSelector<T>() where T : NthChildSelector, new()
        {
			var b = new NthFirstChildSelector();
            var selector = new T();

            if (attrValue.Equals(NTH_CHILD_ODD, StringComparison.OrdinalIgnoreCase))
            {
                selector.step = 2;
                selector.offset = 1;
            }
			else if (attrValue.Equals(NTH_CHILD_EVEN, StringComparison.OrdinalIgnoreCase))
            {
                selector.step = 2;
                selector.offset = 0;
            }
			else if (!Int32.TryParse(attrValue, out selector.offset))
            {
				var index = attrValue.IndexOf(NTH_CHILD_N, StringComparison.OrdinalIgnoreCase);

				if (attrValue.Length > 0 && index != -1)
                {
					var first = attrValue.Substring(0, index).Replace(" ", "");
					var second = attrValue.Substring(index + 1).Replace(" ", "");

                    if (first == String.Empty || (first.Length == 1 && first[0] == Specification.PLUS))
                        selector.step = 1;
                    else if (first.Length == 1 && first[0] == Specification.MINUS)
                        selector.step = -1;
                    else if (!Int32.TryParse(first, out selector.step))
                        throw new DOMException(ErrorCode.SyntaxError);

                    if (second == String.Empty)
                        selector.offset = 0;
					else if (!Int32.TryParse(second, out selector.offset) && !ignoreErrors) 
                        throw new DOMException(ErrorCode.SyntaxError);
                }
                else if (!ignoreErrors) 
                    throw new DOMException(ErrorCode.SyntaxError);
            }

            return selector;
        }

		/// <summary>
		/// Invoked once a colon with an identifier has been found in the token enumerator.
		/// </summary>
		/// <returns>The created selector.</returns>
		SimpleSelector GetPseudoSelector(CssToken token)
		{
			switch (((CssKeywordToken)token).Data)
			{
				case PSEUDOCLASS_ROOT:
					return SimpleSelector.PseudoClass(el => el.OwnerDocument.DocumentElement == el, PSEUDOCLASS_ROOT);

				case PSEUDOCLASS_FIRSTOFTYPE:
					return SimpleSelector.PseudoClass(el =>
					{
						var parent = el.ParentElement;

						if (parent == null)
							return true;

						for (int i = 0; i < parent.ChildNodes.Length; i++)
						{
							if (parent.ChildNodes[i].NodeName == el.NodeName)
								return parent.ChildNodes[i] == el;
						}

						return false;
					}, PSEUDOCLASS_FIRSTOFTYPE);

				case PSEUDOCLASS_LASTOFTYPE:
					return SimpleSelector.PseudoClass(el =>
					{
						var parent = el.ParentElement;

						if (parent == null)
							return true;

						for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
						{
							if (parent.ChildNodes[i].NodeName == el.NodeName)
								return parent.ChildNodes[i] == el;
						}

						return false;
					}, PSEUDOCLASS_LASTOFTYPE);

				case PSEUDOCLASS_ONLYCHILD:
					return SimpleSelector.PseudoClass(el =>
					{
						var parent = el.ParentElement;

						if (parent == null)
							return false;

						var elements = 0;

						for (int i = 0; i < parent.ChildNodes.Length; i++)
						{
							if (parent.ChildNodes[i] is Element && ++elements == 2)
								return false;
						}

						return true;
					}, PSEUDOCLASS_ONLYCHILD);

				case PSEUDOCLASS_FIRSTCHILD:
					return FirstChildSelector.Instance;

				case PSEUDOCLASS_LASTCHILD:
					return LastChildSelector.Instance;

				case PSEUDOCLASS_EMPTY:
					return SimpleSelector.PseudoClass(el => el.ChildNodes.Length == 0, PSEUDOCLASS_EMPTY);

				case PSEUDOCLASS_LINK:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && !((HTMLAnchorElement)el).IsVisited;
						else if (el is HTMLAreaElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && !((HTMLAreaElement)el).IsVisited;
						else if (el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && !((HTMLLinkElement)el).IsVisited;

						return false;
					}, PSEUDOCLASS_LINK);

				case PSEUDOCLASS_VISITED:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && ((HTMLAnchorElement)el).IsVisited;
						else if (el is HTMLAreaElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && ((HTMLAreaElement)el).IsVisited;
						else if (el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && ((HTMLLinkElement)el).IsVisited;

						return false;
					}, PSEUDOCLASS_VISITED);

				case PSEUDOCLASS_ACTIVE:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && ((HTMLAnchorElement)el).IsActive;
						else if (el is HTMLAreaElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && ((HTMLAreaElement)el).IsActive;
						else if (el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF)) && ((HTMLLinkElement)el).IsActive;
						else if (el is HTMLButtonElement)
							return !((HTMLButtonElement)el).Disabled && ((HTMLButtonElement)el).IsActive;
						else if (el is HTMLInputElement)
						{
							var inp = (HTMLInputElement)el;
							return (inp.Type == HTMLInputElement.InputType.Submit || inp.Type == HTMLInputElement.InputType.Image ||
								inp.Type == HTMLInputElement.InputType.Reset || inp.Type == HTMLInputElement.InputType.Button) &&
								inp.IsActive;
						}
						else if (el is HTMLMenuItemElement)
							return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.DISABLED)) && ((HTMLMenuItemElement)el).IsActive;

						return false;
					}, PSEUDOCLASS_ACTIVE);

				case PSEUDOCLASS_HOVER:
					return SimpleSelector.PseudoClass(el => el.IsHovered, PSEUDOCLASS_HOVER);

				case PSEUDOCLASS_FOCUS:
					return SimpleSelector.PseudoClass(el => el.IsFocused, PSEUDOCLASS_FOCUS);

				case PSEUDOCLASS_TARGET:
					return SimpleSelector.PseudoClass(el => el.OwnerDocument != null && el.Id == el.OwnerDocument.Location.Hash, PSEUDOCLASS_TARGET);

				case PSEUDOCLASS_ENABLED:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement || el is HTMLAreaElement || el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.HREF));
						else if (el is HTMLButtonElement)
							return !((HTMLButtonElement)el).Disabled;
						else if (el is HTMLInputElement)
							return !((HTMLInputElement)el).Disabled;
						else if (el is HTMLSelectElement)
							return !((HTMLSelectElement)el).Disabled;
						else if (el is HTMLTextAreaElement)
							return !((HTMLTextAreaElement)el).Disabled;
						else if (el is HTMLOptionElement)
							return !((HTMLOptionElement)el).Disabled;
						else if (el is HTMLOptGroupElement || el is HTMLMenuItemElement || el is HTMLFieldSetElement)
							return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.DISABLED));

						return false;
					}, PSEUDOCLASS_ENABLED);

				case PSEUDOCLASS_DISABLED:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLButtonElement)
							return ((HTMLButtonElement)el).Disabled;
						else if (el is HTMLInputElement)
							return ((HTMLInputElement)el).Disabled;
						else if (el is HTMLSelectElement)
							return ((HTMLSelectElement)el).Disabled;
						else if (el is HTMLTextAreaElement)
							return ((HTMLTextAreaElement)el).Disabled;
						else if (el is HTMLOptionElement)
							return ((HTMLOptionElement)el).Disabled;
						else if (el is HTMLOptGroupElement || el is HTMLMenuItemElement || el is HTMLFieldSetElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.DISABLED));

						return false;
					}, PSEUDOCLASS_DISABLED);

				case PSEUDOCLASS_DEFAULT:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLButtonElement)
						{
							var bt = (HTMLButtonElement)el;
							var form = bt.Form;

							if (form != null)//TODO Check if button is form def. button
								return true;
						}
						else if (el is HTMLInputElement)
						{
							var input = (HTMLInputElement)el;

							if (input.Type == HTMLInputElement.InputType.Submit || input.Type == HTMLInputElement.InputType.Image)
							{
								var form = input.Form;

								if (form != null)//TODO Check if input is form def. button
									return true;
							}
							else
							{
								//TODO input that are checked and can be checked ...
							}
						}
						else if (el is HTMLOptionElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.SELECTED));

						return false;
					}, PSEUDOCLASS_DEFAULT);

				case PSEUDOCLASS_CHECKED:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
						{
							var inp = (HTMLInputElement)el;
							return (inp.Type == HTMLInputElement.InputType.Checkbox || inp.Type == HTMLInputElement.InputType.Radio)
								&& inp.Checked;
						}
						else if (el is HTMLMenuItemElement)
						{
							var mi = (HTMLMenuItemElement)el;
							return (mi.Type == HTMLMenuItemElement.ItemType.Checkbox || mi.Type == HTMLMenuItemElement.ItemType.Radio)
								&& mi.Checked;
						}
						else if (el is HTMLOptionElement)
							return ((HTMLOptionElement)el).Selected;

						return false;
					}, PSEUDOCLASS_CHECKED);

				case PSEUDOCLASS_INDETERMINATE:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
						{
							var inp = (HTMLInputElement)el;
							return inp.Type == HTMLInputElement.InputType.Checkbox && inp.Indeterminate;
						}
						else if (el is HTMLProgressElement)
							return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.VALUE));

						return false;
					}, PSEUDOCLASS_INDETERMINATE);

				case PSEUDOCLASS_UNCHECKED:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
						{
							var inp = (HTMLInputElement)el;
							return (inp.Type == HTMLInputElement.InputType.Checkbox || inp.Type == HTMLInputElement.InputType.Radio)
								&& !inp.Checked;
						}
						else if (el is HTMLMenuItemElement)
						{
							var mi = (HTMLMenuItemElement)el;
							return (mi.Type == HTMLMenuItemElement.ItemType.Checkbox || mi.Type == HTMLMenuItemElement.ItemType.Radio)
								&& !mi.Checked;
						}
						else if (el is HTMLOptionElement)
							return !((HTMLOptionElement)el).Selected;

						return false;
					}, PSEUDOCLASS_UNCHECKED);

				case PSEUDOCLASS_VALID:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
							return ((IValidation)el).CheckValidity();
						else if (el is HTMLFormElement)
							return ((HTMLFormElement)el).CheckValidity();

						return false;
					}, PSEUDOCLASS_VALID);

				case PSEUDOCLASS_INVALID:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
							return !((IValidation)el).CheckValidity();
						else if (el is HTMLFormElement)
							return !((HTMLFormElement)el).CheckValidity();

						return false;
					}, PSEUDOCLASS_INVALID);

				case PSEUDOCLASS_REQUIRED:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return ((HTMLInputElement)el).Required;
						else if (el is HTMLSelectElement)
							return ((HTMLSelectElement)el).Required;
						else if (el is HTMLTextAreaElement)
							return ((HTMLTextAreaElement)el).Required;

						return false;
					}, PSEUDOCLASS_REQUIRED);

				case PSEUDOCLASS_READONLY:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return !((HTMLInputElement)el).IsMutable;
						else if (el is HTMLTextAreaElement)
							return !((HTMLTextAreaElement)el).IsMutable;

						return !el.IsContentEditable;
					}, PSEUDOCLASS_READONLY);

				case PSEUDOCLASS_READWRITE:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return ((HTMLInputElement)el).IsMutable;
						else if (el is HTMLTextAreaElement)
							return ((HTMLTextAreaElement)el).IsMutable;

						return el.IsContentEditable;
					}, PSEUDOCLASS_READWRITE);

				case PSEUDOCLASS_INRANGE:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
						{
							var state = ((IValidation)el).Validity;
							return !state.RangeOverflow && !state.RangeUnderflow;
						}

						return false;
					}, PSEUDOCLASS_INRANGE);

				case PSEUDOCLASS_OUTOFRANGE:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
						{
							var state = ((IValidation)el).Validity;
							return state.RangeOverflow || state.RangeUnderflow;
						}

						return false;
					}, PSEUDOCLASS_OUTOFRANGE);

				case PSEUDOCLASS_OPTIONAL:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return !((HTMLInputElement)el).Required;
						else if (el is HTMLSelectElement)
							return !((HTMLSelectElement)el).Required;
						else if (el is HTMLTextAreaElement)
							return !((HTMLTextAreaElement)el).Required;

						return false;
					}, PSEUDOCLASS_OPTIONAL);

				// LEGACY STYLE OF DEFINING PSEUDO ELEMENTS - AS PSEUDO CLASS!
				case PSEUDOELEMENT_BEFORE:
					return SimpleSelector.PseudoClass(MatchBefore, PSEUDOELEMENT_BEFORE);

				case PSEUDOELEMENT_AFTER:
					return SimpleSelector.PseudoClass(MatchAfter, PSEUDOELEMENT_AFTER);

				case PSEUDOELEMENT_FIRSTLINE:
					return SimpleSelector.PseudoClass(MatchFirstLine, PSEUDOELEMENT_FIRSTLINE);

				case PSEUDOELEMENT_FIRSTLETTER:
					return SimpleSelector.PseudoClass(MatchFirstLetter, PSEUDOELEMENT_FIRSTLETTER);
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
			PseudoClassFunction,
			PseudoClassFunctionEnd,
			PseudoElement
		}

		#endregion

		#region Selections

		/// <summary>
        /// Matches the ::before pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchBefore(Element element)
        {
            //TODO
            return true;
        }

        /// <summary>
        /// Matches the ::after pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchAfter(Element element)
        {
            //TODO
            return true;
        }

        /// <summary>
        /// Matches the ::first-line pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchFirstLine(Element element)
        {
            //TODO
            return true;
        }

        /// <summary>
        /// Matches the ::first-letter pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchFirstLetter(Element element)
        {
            //TODO
            return true;
        }

        #endregion

		#region Nested

		/// <summary>
		/// The abstract basis for nth-child and nth-lastchild selector.
		/// </summary>
		abstract class NthChildSelector : SimpleSelector
		{
			public Int32 step;
			public Int32 offset;

			public override Int32 Specifity
			{
				get { return 10; }
			}
		}

		/// <summary>
		/// The nth-child selector.
		/// </summary>
        sealed class NthFirstChildSelector : NthChildSelector
        {
			public override Boolean Match(Element element)
            {
                var parent = element.ParentNode;

                if (parent == null)
                    return false;

                var n = 1;

                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] == element)
                        return step == 0 ? n == offset : (n - offset) % step == 0;
                    else if (parent.ChildNodes[i] is Element)
                        n++;
                }

                return true;
            }

            public override String ToCss()
            {
                return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.PSEUDOCLASSFUNCTION_NTHCHILD, step, offset);
            }
        }

		/// <summary>
		/// The nth-lastchild selector.
		/// </summary>
		sealed class NthLastChildSelector : NthChildSelector
        {
            public override Boolean Match(Element element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                var n = 1;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == element)
                        return step == 0 ? n == offset : (n - offset) % step == 0;
                    else if (parent.ChildNodes[i] is Element)
                        n++;
                }

                return true;
            }

            public override String ToCss()
            {
                return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.PSEUDOCLASSFUNCTION_NTHLASTCHILD, step, offset);
            }
        }

		/// <summary>
		/// The first child selector.
		/// </summary>
        sealed class FirstChildSelector : SimpleSelector
        {
            FirstChildSelector()
            { }

            static FirstChildSelector instance;

            public static FirstChildSelector Instance
            {
                get { return instance ?? (instance = new FirstChildSelector()); }
            }

            public override Int32 Specifity
            {
                get { return 10; }
            }

			public override Boolean Match(Element element)
            {
                var parent = element.ParentNode;

                if (parent == null)
                    return false;

                for (var i = 0; i <= parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] == element)
                        return true;
                    else if (parent.ChildNodes[i] is Element)
                        return false;
                }

                return false;
            }

            public override String ToCss()
            {
                return ":" + CssSelectorConstructor.PSEUDOCLASS_FIRSTCHILD;
            }
        }

		/// <summary>
		/// The last child selector.
		/// </summary>
        sealed class LastChildSelector : SimpleSelector
        {
            LastChildSelector()
            { }

            static LastChildSelector instance;

            public static LastChildSelector Instance
            {
                get { return instance ?? (instance = new LastChildSelector()); }
            }

            public override Int32 Specifity
            {
                get { return 10; }
            }

			public override Boolean Match(Element element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == element)
                        return true;
                    else if (parent.ChildNodes[i] is Element)
                        return false;
                }

                return false;
            }

            public override String ToCss()
            {
                return ":" + CssSelectorConstructor.PSEUDOCLASS_LASTCHILD;
            }
        }

        #endregion
	}
}
