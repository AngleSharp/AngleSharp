namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
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

					break;
				}
				case pseudoClassFunctionNot:
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
				case pseudoClassFunctionDir:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = token.Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
				case pseudoClassFunctionLang:
				{
					if (token.Type == CssTokenType.Ident)
						attrValue = token.Data;

					state = State.PseudoClassFunctionEnd;
					return;
				}
				case pseudoClassFunctionContains:
				{
					if (token.Type == CssTokenType.String || token.Type == CssTokenType.Ident)
						attrValue = token.Data;

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
					case pseudoClassFunctionNthChild:
					{
                        var sel = GetChildSelector<NthFirstChildSelector>();

                        if (sel == null)
                            break;

						Insert(sel);
                        return;
					}
					case pseudoClassFunctionNthLastChild:
					{
                        var sel = GetChildSelector<NthLastChildSelector>();

                        if (sel == null)
                            break;

                        Insert(sel);
                        return;
					}
					case pseudoClassFunctionNot:
					{
						var sel = nested.Result;
                        var code = String.Concat(pseudoClassFunctionNot, "(", sel.Text, ")");
						Insert(SimpleSelector.PseudoClass(el => !sel.Match(el), code));
                        return;
					}
					case pseudoClassFunctionDir:
                    {
                        var code = String.Concat(pseudoClassFunctionDir, "(", attrValue, ")");
                        Insert(SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Direction.Equals(attrValue, StringComparison.OrdinalIgnoreCase), code));
                        return;
					}
					case pseudoClassFunctionLang:
                    {
                        var code = String.Concat(pseudoClassFunctionLang, "(", attrValue, ")");
                        Insert(SimpleSelector.PseudoClass(el => el is IHtmlElement && ((IHtmlElement)el).Language.StartsWith(attrValue, StringComparison.OrdinalIgnoreCase), code));
                        return;
					}
					case pseudoClassFunctionContains:
                    {
                        var code = String.Concat(pseudoClassFunctionContains, "(", attrValue, ")");
						Insert(SimpleSelector.PseudoClass(el => el.TextContent.Contains(attrValue), code));
                        return;
					}
                    default:
                        return;
				}
			}

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
			switch (token.Data)
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
					return SimpleSelector.PseudoClass(el => el.IsOnlyChild(), pseudoClassOnlyChild);

				case pseudoClassFirstChild:
					return FirstChildSelector.Instance;

				case pseudoClassLastChild:
					return LastChildSelector.Instance;

				case pseudoClassEmpty:
					return SimpleSelector.PseudoClass(el => el.ChildNodes.Length == 0, pseudoClassEmpty);

				case pseudoClassLink:
					return SimpleSelector.PseudoClass(el => el.IsLink(), pseudoClassLink);

				case pseudoClassVisited:
					return SimpleSelector.PseudoClass(el => el.IsVisited(), pseudoClassVisited);

				case pseudoClassActive:
					return SimpleSelector.PseudoClass(el => el.IsActive(), pseudoClassActive);

				case pseudoClassHover:
					return SimpleSelector.PseudoClass(el => el.IsHovered(), pseudoClassHover);

				case pseudoClassFocus:
					return SimpleSelector.PseudoClass(el => el.IsFocused(), pseudoClassFocus);

				case pseudoClassTarget:
					return SimpleSelector.PseudoClass(el => el.Owner != null && el.Id == el.Owner.Location.Hash, pseudoClassTarget);

				case pseudoClassEnabled:
					return SimpleSelector.PseudoClass(el => el.IsEnabled(), pseudoClassEnabled);

				case pseudoClassDisabled:
					return SimpleSelector.PseudoClass(el => el.IsDisabled(), pseudoClassDisabled);

				case pseudoClassDefault:
					return SimpleSelector.PseudoClass(el => el.IsDefault(), pseudoClassDefault);

				case pseudoClassChecked:
					return SimpleSelector.PseudoClass(el => el.IsChecked(), pseudoClassChecked);

				case pseudoClassIndeterminate:
					return SimpleSelector.PseudoClass(el => el.IsIndeterminate(), pseudoClassIndeterminate);

				case pseudoClassUnchecked:
					return SimpleSelector.PseudoClass(el => el.IsUnchecked(), pseudoClassUnchecked);

				case pseudoClassValid:
					return SimpleSelector.PseudoClass(el => el.IsValid(), pseudoClassValid);

				case pseudoClassInvalid:
					return SimpleSelector.PseudoClass(el => el.IsInvalid(), pseudoClassInvalid);

				case pseudoClassRequired:
					return SimpleSelector.PseudoClass(el => el.IsRequired(), pseudoClassRequired);

				case pseudoClassReadOnly:
					return SimpleSelector.PseudoClass(el => el.IsReadOnly(), pseudoClassReadOnly);

				case pseudoClassReadWrite:
					return SimpleSelector.PseudoClass(el => el.IsEditable(), pseudoClassReadWrite);

				case pseudoClassInRange:
					return SimpleSelector.PseudoClass(el => el.IsInRange(), pseudoClassInRange);

				case pseudoClassOutOfRange:
					return SimpleSelector.PseudoClass(el => el.IsOutOfRange(), pseudoClassOutOfRange);

				case pseudoClassOptional:
					return SimpleSelector.PseudoClass(el => el.IsOptional(), pseudoClassOptional);

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
