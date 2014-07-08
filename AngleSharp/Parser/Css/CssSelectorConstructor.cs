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

        const String pseudoClassRoot = "root";
        const String pseudoClassFirstOfType = "first-of-type";
        const String pseudoClassLastOfType = "last-of-type";
        const String pseudoClassOnlyChild = "only-child";
        const String pseudoClassFirstChild = "first-child";
        const String pseudoClassLastChild = "last-child";
        const String pseudoClassEmpty = "empty";
        const String pseudoClassLink = "link";
        const String pseudoClassVisited = "visited";
        const String pseudoClassActive = "active";
        const String pseudoClassHover = "hover";
        const String pseudoClassFocus = "focus";
        const String pseudoClassTarget = "target";
        const String pseudoClassEnabled = "enabled";
        const String pseudoClassDisabled = "disabled";
        const String pseudoClassChecked = "checked";
        const String pseudoClassUnchecked = "unchecked";
        const String pseudoClassIndeterminate = "indeterminate";
        const String pseudoClassDefault = "default";

        const String pseudoClassValid = "valid";
        const String pseudoClassInvalid = "invalid";
        const String pseudoClassRequired = "required";
        const String pseudoClassInRange = "in-range";
        const String pseudoClassOutOfRange = "out-of-range";
        const String pseudoClassOptional = "optional";
        const String pseudoClassReadOnly = "read-only";
        const String pseudoClassReadWrite = "read-write";

        const String pseudoClassFunctionDir = "dir";
        const String pseudoClassFunctionNthChild = "nth-child";
        const String pseudoClassFunctionNthLastChild = "nth-last-child";
        const String pseudoClassFunctionNot = "not";
        const String pseudoClassFunctionLang = "lang";
        const String pseudoClassFunctionContains = "contains";

        const String pseudoElementBefore = "before";
        const String pseudoElementAfter = "after";
        const String pseudoElementSelection = "selection";
        const String pseudoElementFirstLine = "first-line";
        const String pseudoElementFirstLetter = "first-letter";

        #endregion

        #region Fields

		State state;
        Selector temp;
		ListSelector group;
		ComplexSelector complex;
        Boolean hasCombinator;
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
        /// <returns>True if no error occurred, otherwise false.</returns>
        public Boolean Apply(CssToken token)
        {
			switch (state)
			{
				case State.Data:
					return OnData(token);
				case State.Class:
					return OnClass(token);
				case State.Attribute:
					return OnAttribute(token);
				case State.AttributeOperator:
					return OnAttributeOperator(token);
				case State.AttributeValue:
					return OnAttributeValue(token);
				case State.AttributeEnd:
					return OnAttributeEnd(token);
				case State.PseudoClass:
					return OnPseudoClass(token);
				case State.PseudoClassFunction:
					return OnPseudoClassFunction(token);
				case State.PseudoClassFunctionEnd:
					return OnPseudoClassFunctionEnd(token);
				case State.PseudoElement:
					return OnPseudoElement(token);
                default:
                    return false;
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
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnData(CssToken token)
		{
			switch (token.Type)
			{
				//Begin of attribute [A]
				case CssTokenType.SquareBracketOpen:
					attrName = null;
					attrValue = null;
					attrOp = String.Empty;
					state = State.Attribute;
					return true;

				//Begin of Pseudo :P
				case CssTokenType.Colon:
					state = State.PseudoClass;
					return true;

				//Begin of ID #I
				case CssTokenType.Hash:
					Insert(SimpleSelector.Id(((CssKeywordToken)token).Data));
					return true;

				//Begin of Type E
				case CssTokenType.Ident:
					Insert(SimpleSelector.Type(((CssKeywordToken)token).Data));
					return true;

				//Whitespace could be significant
				case CssTokenType.Whitespace:
					Insert(CssCombinator.Descendent);
					return true;

				//Various
				case CssTokenType.Delim:
					return OnDelim(token);

				case CssTokenType.Comma:
					InsertOr();
					return true;
			}

            return false;
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnAttribute(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return true;

			state = State.AttributeOperator;

			if (token.Type == CssTokenType.Ident)
				attrName = ((CssKeywordToken)token).Data;
            else if (token.Type == CssTokenType.String)
                attrName = ((CssStringToken)token).Data;
            else
            {
                state = State.Data;
                return false;
            }

            return true;
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnAttributeOperator(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return true;

			state = State.AttributeValue;

			if (token.Type == CssTokenType.SquareBracketClose)
				return OnAttributeEnd(token);
            else if (token is CssMatchToken || token.Type == CssTokenType.Delim)
                attrOp = token.ToValue();
            else
            {
                state = State.AttributeEnd;
                return false;
            }

            return true;
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnAttributeValue(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return true;

			state = State.AttributeEnd;

			if (token.Type == CssTokenType.Ident)
				attrValue = ((CssKeywordToken)token).Data;
			else if (token.Type == CssTokenType.String)
				attrValue = ((CssStringToken)token).Data;
            else if (token.Type == CssTokenType.Number)
                attrValue = ((CssNumberToken)token).Data.ToString();
            else
            {
                state = State.Data;
                return false;
            }

            return true;
		}

		/// <summary>
		/// Invoked once a square bracket has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnAttributeEnd(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return true;

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

                return true;
            }
            
            return false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnPseudoClass(CssToken token)
		{
			state = State.Data;

            if (token.Type == CssTokenType.Colon)
            {
                state = State.PseudoElement;
                return true;
            }
            else if (token.Type == CssTokenType.Function)
            {
                attrName = ((CssKeywordToken)token).Data;
                attrValue = String.Empty;
                state = State.PseudoClassFunction;

                if (nested != null)
                    nested.Reset();

                return true;
            }
            else if (token.Type == CssTokenType.Ident)
            {
                var sel = GetPseudoSelector(token);

                if (sel == null)
                    return false;

                Insert(sel);
                return true;
            }
            
            return false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnPseudoElement(CssToken token)
		{
            if (token.Type == CssTokenType.Ident)
            {
                var data = ((CssKeywordToken)token).Data;

                switch (data)
                {
                    case pseudoElementBefore:
                        Insert(SimpleSelector.PseudoElement(MatchBefore, pseudoElementBefore));
                        return true;
                    case pseudoElementAfter:
                        Insert(SimpleSelector.PseudoElement(MatchAfter, pseudoElementAfter));
                        return true;
                    case pseudoElementSelection:
                        Insert(SimpleSelector.PseudoElement(el => true, pseudoElementSelection));
                        return true;
                    case pseudoElementFirstLine:
                        Insert(SimpleSelector.PseudoElement(MatchFirstLine, pseudoElementFirstLine));
                        return true;
                    case pseudoElementFirstLetter:
                        Insert(SimpleSelector.PseudoElement(MatchFirstLetter, pseudoElementFirstLetter));
                        return true;
                    default:
                        Insert(SimpleSelector.PseudoElement(el => false, data));
                        return false;
                }
            }
            
            return false;
		}

		/// <summary>
		/// Invoked once a colon has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnClass(CssToken token)
		{
			state = State.Data;

            if (token.Type == CssTokenType.Ident)
            {
                Insert(SimpleSelector.Class(((CssKeywordToken)token).Data));
                return true;
            }
            
            return false;
		}

		/// <summary>
		/// Invoked once a pseudo class has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnPseudoClassFunction(CssToken token)
		{
			if (token.Type == CssTokenType.Whitespace)
				return true;

			switch (attrName)
			{
				case pseudoClassFunctionNthChild:
				case pseudoClassFunctionNthLastChild:
				{
					switch (token.Type)
					{
						case CssTokenType.Ident:
						case CssTokenType.Number:
						case CssTokenType.Dimension:
							attrValue += token.ToValue();
							return true;

						case CssTokenType.Delim:
							var chr = ((CssDelimToken)token).Data;

							if (chr == Specification.Plus || chr == Specification.Minus)
							{
								attrValue += chr;
								return true;
							}

							break;
					}

					break;
				}
				case pseudoClassFunctionNot:
				{
					if (nested == null)
						nested = new CssSelectorConstructor();

					if (token.Type != CssTokenType.RoundBracketClose || nested.state != State.Data)
					{
						nested.Apply(token);
						return true;
					}

					break;
				}
				case pseudoClassFunctionDir:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return true;
				}
				case pseudoClassFunctionLang:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return true;
				}
				case pseudoClassFunctionContains:
				{
					if (token.Type == CssTokenType.String)
						attrValue = ((CssStringToken)token).Data;
					else if (token.Type == CssTokenType.Ident)
						attrValue = ((CssKeywordToken)token).Data;

					state = State.PseudoClassFunctionEnd;
					return true;
				}
			}

			return OnPseudoClassFunctionEnd(token);
		}

		/// <summary>
		/// Invoked once a pseudo class has been found in the token enumerator.
		/// </summary>
        /// <param name="token">The token.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnPseudoClassFunctionEnd(CssToken token)
		{
			state = State.Data;

			if (token.Type == CssTokenType.RoundBracketClose)
			{
				switch (attrName)
				{
					case pseudoClassFunctionNthChild:
					{
                        var sel = GetChildSelector<NthFirstChildSelector>();

                        if (sel == null)
                            return false;

						Insert(sel);
                        return true;
					}
					case pseudoClassFunctionNthLastChild:
					{
                        var sel = GetChildSelector<NthLastChildSelector>();

                        if (sel == null)
                            return false;

						Insert(sel);
                        return true;
					}
					case pseudoClassFunctionNot:
					{
						var sel = nested.Result;
                        var code = String.Concat(pseudoClassFunctionNot, "(", sel.ToCss(), ")");
						Insert(SimpleSelector.PseudoClass(el => !sel.Match(el), code));
                        return true;
					}
					case pseudoClassFunctionDir:
                    {
                        var code = String.Concat(pseudoClassFunctionDir, "(", attrValue, ")");
                        Insert(SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Dir.Equals(attrValue, StringComparison.OrdinalIgnoreCase), code));
                        return true;
					}
					case pseudoClassFunctionLang:
                    {
                        var code = String.Concat(pseudoClassFunctionLang, "(", attrValue, ")");
                        Insert(SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Lang.StartsWith(attrValue, StringComparison.OrdinalIgnoreCase), code));
                        return true;
					}
					case pseudoClassFunctionContains:
                    {
                        var code = String.Concat(pseudoClassFunctionContains, "(", attrValue, ")");
						Insert(SimpleSelector.PseudoClass(el => el.TextContent.Contains(attrValue), code));
                        return true;
					}
                    default:
                        return true;
				}
			}
            
            return false;
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
        /// <returns>True if no error occurred, otherwise false.</returns>
		Boolean OnDelim(CssToken token)
		{
			switch (((CssDelimToken)token).Data)
			{
				case Specification.Comma:
					InsertOr();
					return true;

				case Specification.GreaterThan:
					Insert(CssCombinator.Child);
					return true;

				case Specification.Plus:
					Insert(CssCombinator.AdjacentSibling);
					return true;

				case Specification.Tilde:
					Insert(CssCombinator.Sibling);
					return true;

				case Specification.Asterisk:
					Insert(SimpleSelector.All);
					return true;

				case Specification.Dot:
					state = State.Class;
					return true;
			}

            return false;
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

		/// <summary>
		/// Invoked once a colon with an identifier has been found in the token enumerator.
		/// </summary>
		/// <returns>The created selector.</returns>
		SimpleSelector GetPseudoSelector(CssToken token)
		{
			switch (((CssKeywordToken)token).Data)
			{
				case pseudoClassRoot:
					return SimpleSelector.PseudoClass(el => el.Owner.DocumentElement == el, pseudoClassRoot);

				case pseudoClassFirstOfType:
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
					}, pseudoClassFirstOfType);

				case pseudoClassLastOfType:
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
					}, pseudoClassLastOfType);

				case pseudoClassOnlyChild:
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
					}, pseudoClassOnlyChild);

				case pseudoClassFirstChild:
					return FirstChildSelector.Instance;

				case pseudoClassLastChild:
					return LastChildSelector.Instance;

				case pseudoClassEmpty:
					return SimpleSelector.PseudoClass(el => el.ChildNodes.Length == 0, pseudoClassEmpty);

				case pseudoClassLink:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLAnchorElement)el).IsVisited;
						else if (el is HTMLAreaElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLAreaElement)el).IsVisited;
						else if (el is HTMLLinkElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLLinkElement)el).IsVisited;

						return false;
					}, pseudoClassLink);

				case pseudoClassVisited:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)el).IsVisited;
						else if (el is HTMLAreaElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)el).IsVisited;
						else if (el is HTMLLinkElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)el).IsVisited;

						return false;
					}, pseudoClassVisited);

				case pseudoClassActive:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)el).IsActive;
						else if (el is HTMLAreaElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)el).IsActive;
						else if (el is HTMLLinkElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)el).IsActive;
						else if (el is HTMLButtonElement)
							return !((HTMLButtonElement)el).IsDisabled && ((HTMLButtonElement)el).IsActive;
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
					}, pseudoClassActive);

				case pseudoClassHover:
					return SimpleSelector.PseudoClass(el => el.IsHovered, pseudoClassHover);

				case pseudoClassFocus:
					return SimpleSelector.PseudoClass(el => el.IsFocused, pseudoClassFocus);

				case pseudoClassTarget:
					return SimpleSelector.PseudoClass(el => el.Owner != null && el.Id == el.Owner.Location.Hash, pseudoClassTarget);

				case pseudoClassEnabled:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLAnchorElement || el is HTMLAreaElement || el is HTMLLinkElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href));
						else if (el is HTMLButtonElement)
							return !((HTMLButtonElement)el).IsDisabled;
						else if (el is HTMLInputElement)
                            return !((HTMLInputElement)el).IsDisabled;
						else if (el is HTMLSelectElement)
                            return !((HTMLSelectElement)el).IsDisabled;
						else if (el is HTMLTextAreaElement)
                            return !((HTMLTextAreaElement)el).IsDisabled;
						else if (el is HTMLOptionElement)
							return !((HTMLOptionElement)el).IsDisabled;
						else if (el is HTMLOptGroupElement || el is HTMLMenuItemElement || el is HTMLFieldSetElement)
							return String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled));

						return false;
					}, pseudoClassEnabled);

				case pseudoClassDisabled:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLButtonElement)
                            return ((HTMLButtonElement)el).IsDisabled;
						else if (el is HTMLInputElement)
                            return ((HTMLInputElement)el).IsDisabled;
						else if (el is HTMLSelectElement)
                            return ((HTMLSelectElement)el).IsDisabled;
						else if (el is HTMLTextAreaElement)
                            return ((HTMLTextAreaElement)el).IsDisabled;
						else if (el is HTMLOptionElement)
							return ((HTMLOptionElement)el).IsDisabled;
						else if (el is HTMLOptGroupElement || el is HTMLMenuItemElement || el is HTMLFieldSetElement)
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled));

						return false;
					}, pseudoClassDisabled);

				case pseudoClassDefault:
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
							return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Selected));

						return false;
					}, pseudoClassDefault);

				case pseudoClassChecked:
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
							return ((HTMLOptionElement)el).IsSelected;

						return false;
					}, pseudoClassChecked);

				case pseudoClassIndeterminate:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
						{
							var inp = (HTMLInputElement)el;
							return inp.Type == HTMLInputElement.InputType.Checkbox && inp.Indeterminate;
						}
						else if (el is HTMLProgressElement)
							return String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Value));

						return false;
					}, pseudoClassIndeterminate);

				case pseudoClassUnchecked:
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
							return !((HTMLOptionElement)el).IsSelected;

						return false;
					}, pseudoClassUnchecked);

				case pseudoClassValid:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
							return ((IValidation)el).CheckValidity();
						else if (el is HTMLFormElement)
							return ((HTMLFormElement)el).CheckValidity();

						return false;
					}, pseudoClassValid);

				case pseudoClassInvalid:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
							return !((IValidation)el).CheckValidity();
						else if (el is HTMLFormElement)
							return !((HTMLFormElement)el).CheckValidity();

						return false;
					}, pseudoClassInvalid);

				case pseudoClassRequired:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return ((HTMLInputElement)el).IsRequired;
						else if (el is HTMLSelectElement)
							return ((HTMLSelectElement)el).IsRequired;
						else if (el is HTMLTextAreaElement)
                            return ((HTMLTextAreaElement)el).IsRequired;

						return false;
					}, pseudoClassRequired);

				case pseudoClassReadOnly:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return !((HTMLInputElement)el).IsMutable;
						else if (el is HTMLTextAreaElement)
							return !((HTMLTextAreaElement)el).IsMutable;
                        else if (el is IHtmlElement)
						    return !((IHtmlElement)el).IsContentEditable;

                        return true;
					}, pseudoClassReadOnly);

				case pseudoClassReadWrite:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
							return ((HTMLInputElement)el).IsMutable;
						else if (el is HTMLTextAreaElement)
                            return ((HTMLTextAreaElement)el).IsMutable;
                        else if (el is IHtmlElement)
                            return ((IHtmlElement)el).IsContentEditable;

						return false;
					}, pseudoClassReadWrite);

				case pseudoClassInRange:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
						{
							var state = ((IValidation)el).Validity;
							return !state.RangeOverflow && !state.RangeUnderflow;
						}

						return false;
					}, pseudoClassInRange);

				case pseudoClassOutOfRange:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is IValidation)
						{
							var state = ((IValidation)el).Validity;
							return state.RangeOverflow || state.RangeUnderflow;
						}

						return false;
					}, pseudoClassOutOfRange);

				case pseudoClassOptional:
					return SimpleSelector.PseudoClass(el =>
					{
						if (el is HTMLInputElement)
                            return !((HTMLInputElement)el).IsRequired;
						else if (el is HTMLSelectElement)
							return !((HTMLSelectElement)el).IsRequired;
						else if (el is HTMLTextAreaElement)
                            return !((HTMLTextAreaElement)el).IsRequired;

						return false;
					}, pseudoClassOptional);

				// LEGACY STYLE OF DEFINING PSEUDO ELEMENTS - AS PSEUDO CLASS!
				case pseudoElementBefore:
					return SimpleSelector.PseudoClass(MatchBefore, pseudoElementBefore);

				case pseudoElementAfter:
					return SimpleSelector.PseudoClass(MatchAfter, pseudoElementAfter);

				case pseudoElementFirstLine:
					return SimpleSelector.PseudoClass(MatchFirstLine, pseudoElementFirstLine);

				case pseudoElementFirstLetter:
					return SimpleSelector.PseudoClass(MatchFirstLetter, pseudoElementFirstLetter);
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
                var parent = element.Parent;

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
                return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthChild, step, offset);
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
                return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthLastChild, step, offset);
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
                var parent = element.Parent;

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
                return ":" + CssSelectorConstructor.pseudoClassFirstChild;
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
                return ":" + CssSelectorConstructor.pseudoClassLastChild;
            }
        }

        #endregion
	}
}
