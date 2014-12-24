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

        static String pseudoClassValid = "valid";
        static String pseudoClassInvalid = "invalid";
        static String pseudoClassRequired = "required";
        static String pseudoClassInRange = "in-range";
        static String pseudoClassOutOfRange = "out-of-range";
        static String pseudoClassOptional = "optional";
        static String pseudoClassReadOnly = "read-only";
        static String pseudoClassReadWrite = "read-write";

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
        ISelector temp;
		ListSelector group;
		ComplexSelector complex;
        Boolean hasCombinator;
		CssCombinator combinator;
		CssSelectorConstructor nested;
		String attrName;
		String attrValue;
		String attrOp;
        Boolean valid;

        #endregion

        #region Initialization

        static Dictionary<String, ISelector> pseudoSelectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase);

        static CssSelectorConstructor()
        {
            pseudoSelectors.Add(pseudoClassRoot, SimpleSelector.PseudoClass(el => el.Owner.DocumentElement == el, pseudoClassRoot));
            pseudoSelectors.Add(pseudoClassFirstOfType, SimpleSelector.PseudoClass(el => el.IsFirstOfType(), pseudoClassFirstOfType));
            pseudoSelectors.Add(pseudoClassLastOfType, SimpleSelector.PseudoClass(el => el.IsLastOfType(), pseudoClassLastOfType));
            pseudoSelectors.Add(pseudoClassOnlyChild, SimpleSelector.PseudoClass(el => el.IsOnlyChild(), pseudoClassOnlyChild));
            pseudoSelectors.Add(pseudoClassFirstChild, FirstChildSelector.Instance);
            pseudoSelectors.Add(pseudoClassLastChild, LastChildSelector.Instance);
            pseudoSelectors.Add(pseudoClassEmpty, SimpleSelector.PseudoClass(el => el.ChildNodes.Length == 0, pseudoClassEmpty));
            pseudoSelectors.Add(pseudoClassLink, SimpleSelector.PseudoClass(el => el.IsLink(), pseudoClassLink));
            pseudoSelectors.Add(pseudoClassVisited, SimpleSelector.PseudoClass(el => el.IsVisited(), pseudoClassVisited));
            pseudoSelectors.Add(pseudoClassActive, SimpleSelector.PseudoClass(el => el.IsActive(), pseudoClassActive));
            pseudoSelectors.Add(pseudoClassHover, SimpleSelector.PseudoClass(el => el.IsHovered(), pseudoClassHover));
            pseudoSelectors.Add(pseudoClassFocus, SimpleSelector.PseudoClass(el => el.IsFocused(), pseudoClassFocus));
            pseudoSelectors.Add(pseudoClassTarget, SimpleSelector.PseudoClass(el => el.Owner != null && el.Id == el.Owner.Location.Hash, pseudoClassTarget));
            pseudoSelectors.Add(pseudoClassEnabled, SimpleSelector.PseudoClass(el => el.IsEnabled(), pseudoClassEnabled));
            pseudoSelectors.Add(pseudoClassDisabled, SimpleSelector.PseudoClass(el => el.IsDisabled(), pseudoClassDisabled));
            pseudoSelectors.Add(pseudoClassDefault, SimpleSelector.PseudoClass(el => el.IsDefault(), pseudoClassDefault));
            pseudoSelectors.Add(pseudoClassChecked, SimpleSelector.PseudoClass(el => el.IsChecked(), pseudoClassChecked));
            pseudoSelectors.Add(pseudoClassIndeterminate, SimpleSelector.PseudoClass(el => el.IsIndeterminate(), pseudoClassIndeterminate));
            pseudoSelectors.Add(pseudoClassUnchecked, SimpleSelector.PseudoClass(el => el.IsUnchecked(), pseudoClassUnchecked));
            pseudoSelectors.Add(pseudoClassValid, SimpleSelector.PseudoClass(el => el.IsValid(), pseudoClassValid));
            pseudoSelectors.Add(pseudoClassInvalid, SimpleSelector.PseudoClass(el => el.IsInvalid(), pseudoClassInvalid));
            pseudoSelectors.Add(pseudoClassRequired, SimpleSelector.PseudoClass(el => el.IsRequired(), pseudoClassRequired));
            pseudoSelectors.Add(pseudoClassReadOnly, SimpleSelector.PseudoClass(el => el.IsReadOnly(), pseudoClassReadOnly));
            pseudoSelectors.Add(pseudoClassReadWrite, SimpleSelector.PseudoClass(el => el.IsEditable(), pseudoClassReadWrite));
            pseudoSelectors.Add(pseudoClassInRange, SimpleSelector.PseudoClass(el => el.IsInRange(), pseudoClassInRange));
            pseudoSelectors.Add(pseudoClassOutOfRange, SimpleSelector.PseudoClass(el => el.IsOutOfRange(), pseudoClassOutOfRange));
            pseudoSelectors.Add(pseudoClassOptional, SimpleSelector.PseudoClass(el => el.IsOptional(), pseudoClassOptional));
            // LEGACY STYLE OF DEFINING PSEUDO ELEMENTS - AS PSEUDO CLASS!
            pseudoSelectors.Add(pseudoElementBefore, SimpleSelector.PseudoClass(MatchBefore, pseudoElementBefore));
            pseudoSelectors.Add(pseudoElementAfter, SimpleSelector.PseudoClass(MatchAfter, pseudoElementAfter));
            pseudoSelectors.Add(pseudoElementFirstLine, SimpleSelector.PseudoClass(MatchFirstLine, pseudoElementFirstLine));
            pseudoSelectors.Add(pseudoElementFirstLetter, SimpleSelector.PseudoClass(MatchFirstLetter, pseudoElementFirstLetter));
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
                switch (attrOp)
                {
                    case "=":
                        Insert(SimpleSelector.AttrMatch(attrName, attrValue));
                        return;
                    case "~=":
                        Insert(SimpleSelector.AttrList(attrName, attrValue));
                        return;
                    case "|=":
                        Insert(SimpleSelector.AttrHyphen(attrName, attrValue));
                        return;
                    case "^=":
                        Insert(SimpleSelector.AttrBegins(attrName, attrValue));
                        return;
                    case "$=":
                        Insert(SimpleSelector.AttrEnds(attrName, attrValue));
                        return;
                    case "*=":
                        Insert(SimpleSelector.AttrContains(attrName, attrValue));
                        return;
                    case "!=":
                        Insert(SimpleSelector.AttrNotMatch(attrName, attrValue));
                        return;
                    default:
                        Insert(SimpleSelector.AttrAvailable(attrName));
                        return;
                }
            }
            
            valid = false;
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
                var data = token.Data;

                switch (data)
                {
                    case pseudoElementBefore:
                        Insert(SimpleSelector.PseudoElement(MatchBefore, pseudoElementBefore));
                        break;
                    case pseudoElementAfter:
                        Insert(SimpleSelector.PseudoElement(MatchAfter, pseudoElementAfter));
                        break;
                    case pseudoElementSelection:
                        Insert(SimpleSelector.PseudoElement(el => true, pseudoElementSelection));
                        break;
                    case pseudoElementFirstLine:
                        Insert(SimpleSelector.PseudoElement(MatchFirstLine, pseudoElementFirstLine));
                        break;
                    case pseudoElementFirstLetter:
                        Insert(SimpleSelector.PseudoElement(MatchFirstLetter, pseudoElementFirstLetter));
                        break;
                    default:
                        Insert(SimpleSelector.PseudoElement(el => false, data));
                        valid = false;
                        break;
                }
            }
            else
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

            if (attrName.Equals(pseudoClassFunctionNthChild, StringComparison.OrdinalIgnoreCase) || attrName.Equals(pseudoClassFunctionNthLastChild, StringComparison.OrdinalIgnoreCase))
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
                else if (attrName.Equals(pseudoClassFunctionNot, StringComparison.OrdinalIgnoreCase))
                {
                    var sel = nested.Result;
                    var code = String.Concat(pseudoClassFunctionNot, "(", sel.Text, ")");
                    Insert(SimpleSelector.PseudoClass(el => !sel.Match(el), code));
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

            if (pseudoSelectors.TryGetValue(token.Data, out selector))
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
                var parent = element.Parent;

                if (parent == null)
                    return false;

                var n = 1;

                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] == element)
                        return step == 0 ? n == offset : (n - offset) % step == 0;
                    else if (parent.ChildNodes[i] is IElement)
                        n++;
                }

                return true;
            }

            public String Text
            {
                get { return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthChild, step, offset); }
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

                var n = 1;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == element)
                        return step == 0 ? n == offset : (n - offset) % step == 0;
                    else if (parent.ChildNodes[i] is IElement)
                        n++;
                }

                return true;
            }

            public String Text
            {
                get { return String.Format(":{0}({1}n+{2})", CssSelectorConstructor.pseudoClassFunctionNthLastChild, step, offset); }
            }
        }

		/// <summary>
		/// The first child selector.
		/// </summary>
        sealed class FirstChildSelector : ISelector
        {
            FirstChildSelector()
            { }

            static FirstChildSelector instance;

            public static FirstChildSelector Instance
            {
                get { return instance ?? (instance = new FirstChildSelector()); }
            }
            
            public String Text
            {
                get { return ":" + CssSelectorConstructor.pseudoClassFirstChild; }
            }

            public Priority Specifity
            {
                get { return Priority.OneClass; }
            }

			public Boolean Match(IElement element)
            {
                var parent = element.Parent;

                if (parent == null)
                    return false;

                for (var i = 0; i <= parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] == element)
                        return true;
                    else if (parent.ChildNodes[i] is IElement)
                        return false;
                }

                return false;
            }
        }

		/// <summary>
		/// The last child selector.
		/// </summary>
        sealed class LastChildSelector : ISelector
        {
            LastChildSelector()
            { }

            static LastChildSelector instance;

            public static LastChildSelector Instance
            {
                get { return instance ?? (instance = new LastChildSelector()); }
            }

            public String Text
            {
                get { return ":" + CssSelectorConstructor.pseudoClassLastChild; }
            }

            public Priority Specifity
            {
                get { return Priority.OneClass; }
            }

			public Boolean Match(IElement element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == element)
                        return true;
                    else if (parent.ChildNodes[i] is IElement)
                        return false;
                }

                return false;
            }
        }

        #endregion
	}
}
