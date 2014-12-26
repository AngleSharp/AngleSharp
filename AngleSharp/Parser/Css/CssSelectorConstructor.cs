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
        #region Constants

        static readonly String nthChildOdd = "odd";
        static readonly String nthChildEven = "even";
        static readonly String nthChildN = "n";

        static readonly String pseudoClassRoot = "root";
        static readonly String pseudoClassFirstOfType = "first-of-type";
        static readonly String pseudoClassLastOfType = "last-of-type";
        static readonly String pseudoClassOnlyChild = "only-child";
        static readonly String pseudoClassFirstChild = "first-child";
        static readonly String pseudoClassLastChild = "last-child";
        static readonly String pseudoClassEmpty = "empty";
        static readonly String pseudoClassLink = "link";
        static readonly String pseudoClassVisited = "visited";
        static readonly String pseudoClassActive = "active";
        static readonly String pseudoClassHover = "hover";
        static readonly String pseudoClassFocus = "focus";
        static readonly String pseudoClassTarget = "target";
        static readonly String pseudoClassEnabled = "enabled";
        static readonly String pseudoClassDisabled = "disabled";
        static readonly String pseudoClassChecked = "checked";
        static readonly String pseudoClassUnchecked = "unchecked";
        static readonly String pseudoClassIndeterminate = "indeterminate";
        static readonly String pseudoClassDefault = "default";

        static readonly String pseudoClassValid = "valid";
        static readonly String pseudoClassInvalid = "invalid";
        static readonly String pseudoClassRequired = "required";
        static readonly String pseudoClassInRange = "in-range";
        static readonly String pseudoClassOutOfRange = "out-of-range";
        static readonly String pseudoClassOptional = "optional";
        static readonly String pseudoClassReadOnly = "read-only";
        static readonly String pseudoClassReadWrite = "read-write";

        static readonly String pseudoClassFunctionDir = "dir";
        static readonly String pseudoClassFunctionNthChild = "nth-child";
        static readonly String pseudoClassFunctionNthLastChild = "nth-last-child";
        static readonly String pseudoClassFunctionNthOfType = "nth-of-type";
        static readonly String pseudoClassFunctionNthLastOfType = "nth-last-of-type";
        static readonly String pseudoClassFunctionNot = "not";
        static readonly String pseudoClassFunctionLang = "lang";
        static readonly String pseudoClassFunctionContains = "contains";

        static readonly String pseudoElementBefore = "before";
        static readonly String pseudoElementAfter = "after";
        static readonly String pseudoElementSelection = "selection";
        static readonly String pseudoElementFirstLine = "first-line";
        static readonly String pseudoElementFirstLetter = "first-letter";

        #endregion

        #region Fields

		State state;
        ISelector temp;
		ListSelector group;
		ComplexSelector complex;
        Boolean hasCombinator;
		CssCombinator combinator;
		CssSelectorConstructor nested;
		String attrName;
		String attrValue;
		String attrOp;
        String attrNs;
        Boolean valid;

        #endregion

        #region Initialization

        static Dictionary<String, ISelector> pseudoClassSelectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase);
        static Dictionary<String, ISelector> pseudoElementSelectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase);

        static CssSelectorConstructor()
        {
            pseudoClassSelectors.Add(pseudoClassRoot, SimpleSelector.PseudoClass(el => el.Owner.DocumentElement == el, pseudoClassRoot));
            pseudoClassSelectors.Add(pseudoClassFirstOfType, SimpleSelector.PseudoClass(el => el.IsFirstOfType(), pseudoClassFirstOfType));
            pseudoClassSelectors.Add(pseudoClassLastOfType, SimpleSelector.PseudoClass(el => el.IsLastOfType(), pseudoClassLastOfType));
            pseudoClassSelectors.Add(pseudoClassOnlyChild, SimpleSelector.PseudoClass(el => el.IsOnlyChild(), pseudoClassOnlyChild));
            pseudoClassSelectors.Add(pseudoClassFirstChild, SimpleSelector.PseudoClass(el => el.IsFirstChild(), pseudoClassFirstChild));
            pseudoClassSelectors.Add(pseudoClassLastChild, SimpleSelector.PseudoClass(el => el.IsLastChild(), pseudoClassLastChild));
            pseudoClassSelectors.Add(pseudoClassEmpty, SimpleSelector.PseudoClass(el => el.ChildNodes.Length == 0, pseudoClassEmpty));
            pseudoClassSelectors.Add(pseudoClassLink, SimpleSelector.PseudoClass(el => el.IsLink(), pseudoClassLink));
            pseudoClassSelectors.Add(pseudoClassVisited, SimpleSelector.PseudoClass(el => el.IsVisited(), pseudoClassVisited));
            pseudoClassSelectors.Add(pseudoClassActive, SimpleSelector.PseudoClass(el => el.IsActive(), pseudoClassActive));
            pseudoClassSelectors.Add(pseudoClassHover, SimpleSelector.PseudoClass(el => el.IsHovered(), pseudoClassHover));
            pseudoClassSelectors.Add(pseudoClassFocus, SimpleSelector.PseudoClass(el => el.IsFocused, pseudoClassFocus));
            pseudoClassSelectors.Add(pseudoClassTarget, SimpleSelector.PseudoClass(el => el.IsTarget(), pseudoClassTarget));
            pseudoClassSelectors.Add(pseudoClassEnabled, SimpleSelector.PseudoClass(el => el.IsEnabled(), pseudoClassEnabled));
            pseudoClassSelectors.Add(pseudoClassDisabled, SimpleSelector.PseudoClass(el => el.IsDisabled(), pseudoClassDisabled));
            pseudoClassSelectors.Add(pseudoClassDefault, SimpleSelector.PseudoClass(el => el.IsDefault(), pseudoClassDefault));
            pseudoClassSelectors.Add(pseudoClassChecked, SimpleSelector.PseudoClass(el => el.IsChecked(), pseudoClassChecked));
            pseudoClassSelectors.Add(pseudoClassIndeterminate, SimpleSelector.PseudoClass(el => el.IsIndeterminate(), pseudoClassIndeterminate));
            pseudoClassSelectors.Add(pseudoClassUnchecked, SimpleSelector.PseudoClass(el => el.IsUnchecked(), pseudoClassUnchecked));
            pseudoClassSelectors.Add(pseudoClassValid, SimpleSelector.PseudoClass(el => el.IsValid(), pseudoClassValid));
            pseudoClassSelectors.Add(pseudoClassInvalid, SimpleSelector.PseudoClass(el => el.IsInvalid(), pseudoClassInvalid));
            pseudoClassSelectors.Add(pseudoClassRequired, SimpleSelector.PseudoClass(el => el.IsRequired(), pseudoClassRequired));
            pseudoClassSelectors.Add(pseudoClassReadOnly, SimpleSelector.PseudoClass(el => el.IsReadOnly(), pseudoClassReadOnly));
            pseudoClassSelectors.Add(pseudoClassReadWrite, SimpleSelector.PseudoClass(el => el.IsEditable(), pseudoClassReadWrite));
            pseudoClassSelectors.Add(pseudoClassInRange, SimpleSelector.PseudoClass(el => el.IsInRange(), pseudoClassInRange));
            pseudoClassSelectors.Add(pseudoClassOutOfRange, SimpleSelector.PseudoClass(el => el.IsOutOfRange(), pseudoClassOutOfRange));
            pseudoClassSelectors.Add(pseudoClassOptional, SimpleSelector.PseudoClass(el => el.IsOptional(), pseudoClassOptional));

            // LEGACY STYLE OF DEFINING PSEUDO ELEMENTS - AS PSEUDO CLASS!
            pseudoClassSelectors.Add(pseudoElementBefore, SimpleSelector.PseudoClass(MatchBefore, pseudoElementBefore));
            pseudoClassSelectors.Add(pseudoElementAfter, SimpleSelector.PseudoClass(MatchAfter, pseudoElementAfter));
            pseudoClassSelectors.Add(pseudoElementFirstLine, SimpleSelector.PseudoClass(MatchFirstLine, pseudoElementFirstLine));
            pseudoClassSelectors.Add(pseudoElementFirstLetter, SimpleSelector.PseudoClass(MatchFirstLetter, pseudoElementFirstLetter));

            pseudoElementSelectors.Add(pseudoElementBefore, SimpleSelector.PseudoElement(MatchBefore, pseudoElementBefore));
            pseudoElementSelectors.Add(pseudoElementAfter, SimpleSelector.PseudoElement(MatchAfter, pseudoElementAfter));
            pseudoElementSelectors.Add(pseudoElementSelection, SimpleSelector.PseudoElement(el => true, pseudoElementSelection));
            pseudoElementSelectors.Add(pseudoElementFirstLine, SimpleSelector.PseudoElement(MatchFirstLine, pseudoElementFirstLine));
            pseudoElementSelectors.Add(pseudoElementFirstLetter, SimpleSelector.PseudoElement(MatchFirstLetter, pseudoElementFirstLetter));
        }

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
        /// Gets if the stored selector is valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return valid; }
        }

        /// <summary>
        /// Gets the currently formed selector.
        /// </summary>
        public ISelector Result
        {
            get
            {
                if (!valid)
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
                case State.PseudoClassFunctionEnd:
					OnPseudoClassFunctionEnd(token);
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
			combinator = CssCombinator.Descendent;
			hasCombinator = false;
			temp = null;
			group = null;
			complex = null;
            valid = true;
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
					break;

				//Begin of Pseudo :P
				case CssTokenType.Colon:
					state = State.PseudoClass;
                    break;

                //Begin of ID #I
                case CssTokenType.Hash:
					Insert(SimpleSelector.Id(token.Data));
                    break;

                //Begin of Type E
                case CssTokenType.Ident:
					Insert(SimpleSelector.Type(token.Data));
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

            if (token.Type == CssTokenType.Colon)
            {
                state = State.PseudoElement;
                return;
            }
            else if (token.Type == CssTokenType.Function)
            {
                attrName = token.Data;
                attrValue = String.Empty;
                state = State.PseudoClassFunction;

                if (nested != null)
                    nested.Reset();

                return;
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

            if (token.Type == CssTokenType.Ident)
            {
                ISelector selector;

                if (pseudoElementSelectors.TryGetValue(token.Data, out selector))
                {
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
			if (token.Type == CssTokenType.Whitespace)
				return;

            if (attrName.Equals(pseudoClassFunctionNthChild, StringComparison.OrdinalIgnoreCase) || 
                attrName.Equals(pseudoClassFunctionNthLastChild, StringComparison.OrdinalIgnoreCase) ||
                attrName.Equals(pseudoClassFunctionNthOfType, StringComparison.OrdinalIgnoreCase) ||
                attrName.Equals(pseudoClassFunctionNthLastOfType, StringComparison.OrdinalIgnoreCase))
            {
                switch (token.Type)
                {
                    case CssTokenType.Ident:
                    case CssTokenType.Number:
                    case CssTokenType.Dimension:
                        attrValue += token.ToValue();
                        return;

                    case CssTokenType.Delim:
                        var chr = token.Data[0];

                        if (chr == Specification.Plus || chr == Specification.Minus)
                        {
                            attrValue += token.Data;
                            return;
                        }

                        break;
                }

                OnPseudoClassFunctionEnd(token);
            }
            else if (attrName.Equals(pseudoClassFunctionNot, StringComparison.OrdinalIgnoreCase))
            {
                if (nested == null)
                    nested = new CssSelectorConstructor();

                if (token.Type != CssTokenType.RoundBracketClose || nested.state != State.Data)
                    nested.Apply(token);
                else
                    OnPseudoClassFunctionEnd(token);
            }
            else if (attrName.Equals(pseudoClassFunctionDir, StringComparison.OrdinalIgnoreCase))
            {
                if (token.Type == CssTokenType.Ident)
                    attrValue = token.Data;

                state = State.PseudoClassFunctionEnd;
            }
            else if (attrName.Equals(pseudoClassFunctionLang, StringComparison.OrdinalIgnoreCase))
            {
                if (token.Type == CssTokenType.Ident)
                    attrValue = token.Data;

                state = State.PseudoClassFunctionEnd;
            }
            else if (attrName.Equals(pseudoClassFunctionContains, StringComparison.OrdinalIgnoreCase))
            {
                if (token.Type == CssTokenType.String || token.Type == CssTokenType.Ident)
                    attrValue = token.Data;

                state = State.PseudoClassFunctionEnd;
            }
            else
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
                if (attrName.Equals(pseudoClassFunctionNthChild, StringComparison.OrdinalIgnoreCase))
                {
                    var sel = GetChildSelector<NthFirstChildSelector>();

                    if (sel != null)
                        Insert(sel);
                    else
                        valid = false;
                }
                else if (attrName.Equals(pseudoClassFunctionNthLastChild, StringComparison.OrdinalIgnoreCase))
                {
                    var sel = GetChildSelector<NthLastChildSelector>();

                    if (sel != null)
                        Insert(sel);
                    else
                        valid = false;
                }
                else if (attrName.Equals(pseudoClassFunctionNthOfType, StringComparison.OrdinalIgnoreCase))
                {
                    var sel = GetChildSelector<NthFirstTypeSelector>();

                    if (sel != null)
                        Insert(sel);
                    else
                        valid = false;
                }
                else if (attrName.Equals(pseudoClassFunctionNthLastOfType, StringComparison.OrdinalIgnoreCase))
                {
                    var sel = GetChildSelector<NthLastTypeSelector>();

                    if (sel != null)
                        Insert(sel);
                    else
                        valid = false;
                }
                else if (attrName.Equals(pseudoClassFunctionNot, StringComparison.OrdinalIgnoreCase))
                {
                    var sel = nested.Result;

                    if (sel != null)
                        Insert(SimpleSelector.PseudoClass(el => !sel.Match(el), String.Concat(pseudoClassFunctionNot, "(", sel.Text, ")")));
                    else
                        valid = false;
                }
                else if (attrName.Equals(pseudoClassFunctionDir, StringComparison.OrdinalIgnoreCase))
                {
                    var code = String.Concat(pseudoClassFunctionDir, "(", attrValue, ")");
                    Insert(SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Direction.Equals(attrValue, StringComparison.OrdinalIgnoreCase), code));
                }
                else if (attrName.Equals(pseudoClassFunctionLang, StringComparison.OrdinalIgnoreCase))
                {
                    var code = String.Concat(pseudoClassFunctionLang, "(", attrValue, ")");
                    Insert(SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Language.StartsWith(attrValue, StringComparison.OrdinalIgnoreCase), code));
                }
                else if (attrName.Equals(pseudoClassFunctionContains, StringComparison.OrdinalIgnoreCase))
                {
                    var code = String.Concat(pseudoClassFunctionContains, "(", attrValue, ")");
                    Insert(SimpleSelector.PseudoClass(el => el.TextContent.Contains(attrValue), code));
                }
			}
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
                if (!hasCombinator)
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
		void OnDelim(CssToken token)
		{
			switch (token.Data[0])
			{
				case Specification.Comma:
					InsertOr();
                    break;

                case Specification.GreaterThan:
					Insert(CssCombinator.Child);
                    break;

                case Specification.Plus:
					Insert(CssCombinator.AdjacentSibling);
                    break;

                case Specification.Tilde:
					Insert(CssCombinator.Sibling);
                    break;

                case Specification.Asterisk:
					Insert(SimpleSelector.All);
                    break;

                case Specification.Dot:
					state = State.Class;
                    break;

                case Specification.Pipe:
                    Insert(CssCombinator.Namespace);
                    break;

                default:
                    valid = false;
                    break;
            }
		}

        /// <summary>
        /// Takes string and transforms it into the arguments for the nth-child function.
        /// </summary>
        /// <returns>The function.</returns>
        ISelector GetChildSelector<T>() 
            where T : NthChildSelector, ISelector, new()
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
		ISelector GetPseudoSelector(CssToken token)
		{
            ISelector selector;

            if (pseudoClassSelectors.TryGetValue(token.Data, out selector))
                return selector;

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
        static Boolean MatchBefore(IElement element)
        {
            return element.PreviousElementSibling.IsPseudo(element);
        }

        /// <summary>
        /// Matches the ::after pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchAfter(IElement element)
        {
            return element.NextElementSibling.IsPseudo(element);
        }

        /// <summary>
        /// Matches the ::first-line pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchFirstLine(IElement element)
        {
            return element.HasChildNodes && element.ChildNodes[0].NodeType == NodeType.Text;
        }

        /// <summary>
        /// Matches the ::first-letter pseudo element (or pseudo-class for legacy reasons).
        /// </summary>
        /// <param name="element">The element to match.</param>
        /// <returns>An indicator if the match has been successful.</returns>
        static Boolean MatchFirstLetter(IElement element)
        {
            return element.HasChildNodes && element.ChildNodes[0].NodeType == NodeType.Text && element.ChildNodes[0].TextContent.Length > 0;
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
                get { return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthChild, step, offset); }
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
                get { return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthOfType, step, offset); }
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
                get { return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthLastChild, step, offset); }
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
                get { return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthLastOfType, step, offset); }
            }
        }

        #endregion
    }
}
