namespace AngleSharp.Parser.Css
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

        static readonly String nthChildOdd = "odd";
        static readonly String nthChildEven = "even";
        static readonly String nthChildN = "n";

        const String pseudoclassRoot = "root";
        const String pseudoclassFirstOfType = "first-of-type";
        const String pseudoclassLastOfType = "last-of-type";
        const String pseudoclassOnlyChild = "only-child";
        const String pseudoclassFirstChild = "first-child";
        const String pseudoclassLastChild = "last-child";
        const String pseudoclassEmpty = "empty";
        const String pseudoclassLink = "link";
        const String pseudoclassVisited = "visited";
        const String pseudoclassActive = "active";
        const String pseudoclassHover = "hover";
        const String pseudoclassFocus = "focus";
        const String pseudoclassTarget = "target";
        const String pseudoclassEnabled = "enabled";
        const String pseudoclassDisabled = "disabled";
        const String pseudoclassChecked = "checked";
        const String pseudoclassUnchecked = "unchecked";
        const String pseudoclassIndeterminate = "indeterminate";
        const String pseudoclassDefault = "default";

        const String pseudoclassValid = "valid";
        const String pseudoclassInvalid = "invalid";
        const String pseudoclassRequired = "required";
        const String pseudoclassInRange = "in-range";
        const String pseudoclassOutOfRange = "out-of-range";
        const String pseudoclassOptional = "optional";
        const String pseudoclassReadOnly = "read-only";
        const String pseudoclassReadWrite = "read-write";

        const String pseudoclassfunctionDir = "dir";
        const String pseudoclassfunctionNthChild = "nth-child";
        const String pseudoclassfunctionNthLastChild = "nth-last-child";
        const String pseudoclassfunctionNot = "not";
        const String pseudoclassfunctionLang = "lang";
        const String pseudoclassfunctionContains = "contains";

        const String pseudoelementBefore = "before";
        const String pseudoelementAfter = "after";
        const String pseudoelementSelection = "selection";
        const String pseudoelementFirstLine = "first-line";
        const String pseudoelementFirstLetter = "first-letter";

        #endregion

        #region Fields

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
					case pseudoelementBefore:
						Insert(SimpleSelector.PseudoElement(MatchBefore, pseudoelementBefore));
						break;
					case pseudoelementAfter:
						Insert(SimpleSelector.PseudoElement(MatchAfter, pseudoelementAfter));
						break;
					case pseudoelementSelection:
						Insert(SimpleSelector.PseudoElement(el => true, pseudoelementSelection));
						break;
					case pseudoelementFirstLine:
						Insert(SimpleSelector.PseudoElement(MatchFirstLine, pseudoelementFirstLine));
						break;
					case pseudoelementFirstLetter:
						Insert(SimpleSelector.PseudoElement(MatchFirstLetter, pseudoelementFirstLetter));
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
				case pseudoclassfunctionNthChild:
				case pseudoclassfunctionNthLastChild:
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

							if (chr == Specification.Plus || chr == Specification.Minus)
							{
								attrValue += chr;
								return;
							}

							break;
					}

					break;
				}
				case pseudoclassfunctionNot:
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
				case pseudoclassfunctionDir:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
				case pseudoclassfunctionLang:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
				case pseudoclassfunctionContains:
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
					case pseudoclassfunctionNthChild:
					{
						Insert(GetChildSelector<NthFirstChildSelector>());
						break;
					}
					case pseudoclassfunctionNthLastChild:
					{
						Insert(GetChildSelector<NthLastChildSelector>());
						break;
					}
					case pseudoclassfunctionNot:
					{
						var sel = nested.Result;
						var code = String.Format("{0}({1})", pseudoclassfunctionNot, sel.ToCss());
						Insert(SimpleSelector.PseudoClass(el => !sel.Match(el), code));
						break;
					}
					case pseudoclassfunctionDir:
					{
						var code = String.Format("{0}({1})", pseudoclassfunctionDir, attrValue);
						var dirCode = attrValue == "ltr" ? DirectionMode.Ltr : DirectionMode.Rtl;
						Insert(SimpleSelector.PseudoClass(el => el.Dir == dirCode, code));
						break;
					}
					case pseudoclassfunctionLang:
					{
						var code = String.Format("{0}({1})", pseudoclassfunctionLang, attrValue);
						Insert(SimpleSelector.PseudoClass(el => el.Lang.StartsWith(attrValue, StringComparison.OrdinalIgnoreCase), code));
						break;
					}
					case pseudoclassfunctionContains:
					{
						var code = String.Format("{0}({1})", pseudoclassfunctionContains, attrValue);
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
				case Specification.Comma:
					InsertOr();
					return;

				case Specification.GreaterThan:
					Insert(CssCombinator.Child);
					return;

				case Specification.Plus:
					Insert(CssCombinator.AdjacentSibling);
					return;

				case Specification.Tilde:
					Insert(CssCombinator.Sibling);
					return;

				case Specification.Asterisk:
					Insert(SimpleSelector.All);
					return;

				case Specification.Dot:
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

            if (attrValue.Equals(nthChildOdd, StringComparison.OrdinalIgnoreCase))
            {
                selector.step = 2;
                selector.offset = 1;
            }
			else if (attrValue.Equals(nthChildEven, StringComparison.OrdinalIgnoreCase))
            {
                selector.step = 2;
                selector.offset = 0;
            }
			else if (!Int32.TryParse(attrValue, out selector.offset))
            {
				var index = attrValue.IndexOf(nthChildN, StringComparison.OrdinalIgnoreCase);

				if (attrValue.Length > 0 && index != -1)
                {
					var first = attrValue.Substring(0, index).Replace(" ", "");
					var second = attrValue.Substring(index + 1).Replace(" ", "");

                    if (first == String.Empty || (first.Length == 1 && first[0] == Specification.Plus))
                        selector.step = 1;
                    else if (first.Length == 1 && first[0] == Specification.Minus)
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
				case pseudoclassRoot:
					return SimpleSelector.PseudoClass(el => el.OwnerDocument.DocumentElement == el, pseudoclassRoot);

				case pseudoclassFirstOfType:
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
					}, pseudoclassFirstOfType);

				case pseudoclassLastOfType:
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
					}, pseudoclassLastOfType);

				case pseudoclassOnlyChild:
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
					}, pseudoclassOnlyChild);

				case pseudoclassFirstChild:
					return FirstChildSelector.Instance;

				case pseudoclassLastChild:
					return LastChildSelector.Instance;

				case pseudoclassEmpty:
					return SimpleSelector.PseudoClass(el => el.ChildNodes.Length == 0, pseudoclassEmpty);

				case pseudoclassLink:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLAnchorElement)el).IsVisited;
						else if (el is HTMLAreaElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLAreaElement)el).IsVisited;
						else if (el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLLinkElement)el).IsVisited;

						return false;
					}, pseudoclassLink);

				case pseudoclassVisited:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)el).IsVisited;
						else if (el is HTMLAreaElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)el).IsVisited;
						else if (el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)el).IsVisited;

						return false;
					}, pseudoclassVisited);

				case pseudoclassActive:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)el).IsActive;
						else if (el is HTMLAreaElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)el).IsActive;
						else if (el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)el).IsActive;
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
							return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled)) && ((HTMLMenuItemElement)el).IsActive;

						return false;
					}, pseudoclassActive);

				case pseudoclassHover:
					return SimpleSelector.PseudoClass(el => el.IsHovered, pseudoclassHover);

				case pseudoclassFocus:
					return SimpleSelector.PseudoClass(el => el.IsFocused, pseudoclassFocus);

				case pseudoclassTarget:
					return SimpleSelector.PseudoClass(el => el.OwnerDocument != null && el.Id == el.OwnerDocument.Location.Hash, pseudoclassTarget);

				case pseudoclassEnabled:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement || el is HTMLAreaElement || el is HTMLLinkElement)
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href));
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
							return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled));

						return false;
					}, pseudoclassEnabled);

				case pseudoclassDisabled:
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
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled));

						return false;
					}, pseudoclassDisabled);

				case pseudoclassDefault:
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
							return !string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Selected));

						return false;
					}, pseudoclassDefault);

				case pseudoclassChecked:
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
					}, pseudoclassChecked);

				case pseudoclassIndeterminate:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
						{
							var inp = (HTMLInputElement)el;
							return inp.Type == HTMLInputElement.InputType.Checkbox && inp.Indeterminate;
						}
						else if (el is HTMLProgressElement)
							return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Value));

						return false;
					}, pseudoclassIndeterminate);

				case pseudoclassUnchecked:
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
					}, pseudoclassUnchecked);

				case pseudoclassValid:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
							return ((IValidation)el).CheckValidity();
						else if (el is HTMLFormElement)
							return ((HTMLFormElement)el).CheckValidity();

						return false;
					}, pseudoclassValid);

				case pseudoclassInvalid:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
							return !((IValidation)el).CheckValidity();
						else if (el is HTMLFormElement)
							return !((HTMLFormElement)el).CheckValidity();

						return false;
					}, pseudoclassInvalid);

				case pseudoclassRequired:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return ((HTMLInputElement)el).Required;
						else if (el is HTMLSelectElement)
							return ((HTMLSelectElement)el).Required;
						else if (el is HTMLTextAreaElement)
							return ((HTMLTextAreaElement)el).Required;

						return false;
					}, pseudoclassRequired);

				case pseudoclassReadOnly:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return !((HTMLInputElement)el).IsMutable;
						else if (el is HTMLTextAreaElement)
							return !((HTMLTextAreaElement)el).IsMutable;

						return !el.IsContentEditable;
					}, pseudoclassReadOnly);

				case pseudoclassReadWrite:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return ((HTMLInputElement)el).IsMutable;
						else if (el is HTMLTextAreaElement)
							return ((HTMLTextAreaElement)el).IsMutable;

						return el.IsContentEditable;
					}, pseudoclassReadWrite);

				case pseudoclassInRange:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
						{
							var state = ((IValidation)el).Validity;
							return !state.RangeOverflow && !state.RangeUnderflow;
						}

						return false;
					}, pseudoclassInRange);

				case pseudoclassOutOfRange:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
						{
							var state = ((IValidation)el).Validity;
							return state.RangeOverflow || state.RangeUnderflow;
						}

						return false;
					}, pseudoclassOutOfRange);

				case pseudoclassOptional:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return !((HTMLInputElement)el).Required;
						else if (el is HTMLSelectElement)
							return !((HTMLSelectElement)el).Required;
						else if (el is HTMLTextAreaElement)
							return !((HTMLTextAreaElement)el).Required;

						return false;
					}, pseudoclassOptional);

				// LEGACY STYLE OF DEFINING PSEUDO ELEMENTS - AS PSEUDO CLASS!
				case pseudoelementBefore:
					return SimpleSelector.PseudoClass(MatchBefore, pseudoelementBefore);

				case pseudoelementAfter:
					return SimpleSelector.PseudoClass(MatchAfter, pseudoelementAfter);

				case pseudoelementFirstLine:
					return SimpleSelector.PseudoClass(MatchFirstLine, pseudoelementFirstLine);

				case pseudoelementFirstLetter:
					return SimpleSelector.PseudoClass(MatchFirstLetter, pseudoelementFirstLetter);
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
                return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoclassfunctionNthChild, step, offset);
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
                return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoclassfunctionNthLastChild, step, offset);
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
                return ":" + CssSelectorConstructor.pseudoclassFirstChild;
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
                return ":" + CssSelectorConstructor.pseudoclassLastChild;
            }
        }

        #endregion
	}
}
