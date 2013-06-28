using System;
using System.Collections.Generic;
using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using System.Diagnostics;

namespace AngleSharp.Css
{
    /// <summary>
    /// Class for construction for CSS selectors as specified in
    /// http://www.w3.org/html/wg/drafts/html/master/selectors.html.
    /// </summary>
    [DebuggerStepThrough]
    class CssSelectorConstructor
    {
        #region Constants

        static readonly string NTH_CHILD_ODD = "odd";
        static readonly string NTH_CHILD_EVEN = "even";
        static readonly string NTH_CHILD_N = "n";

        const string PSEUDOCLASS_ROOT = "root";
        const string PSEUDOCLASS_FIRSTOFTYPE = "first-of-type";
        const string PSEUDOCLASS_LASTOFTYPE = "last-of-type";
        const string PSEUDOCLASS_ONLYCHILD = "only-child";
        const string PSEUDOCLASS_FIRSTCHILD = "first-child";
        const string PSEUDOCLASS_LASTCHILD = "last-child";
        const string PSEUDOCLASS_EMPTY = "empty";
        const string PSEUDOCLASS_LINK = "link";
        const string PSEUDOCLASS_VISITED = "visited";
        const string PSEUDOCLASS_ACTIVE = "active";
        const string PSEUDOCLASS_HOVER = "hover";
        const string PSEUDOCLASS_FOCUS = "focus";
        const string PSEUDOCLASS_TARGET = "target";
        const string PSEUDOCLASS_ENABLED = "enabled";
        const string PSEUDOCLASS_DISABLED = "disabled";
        const string PSEUDOCLASS_CHECKED = "checked";
        const string PSEUDOCLASS_UNCHECKED = "unchecked";
        const string PSEUDOCLASS_INDETERMINATE = "indeterminate";
        const string PSEUDOCLASS_DEFAULT = "default";

        const string PSEUDOCLASS_VALID = "valid";
        const string PSEUDOCLASS_INVALID = "invalid";
        const string PSEUDOCLASS_REQUIRED = "required";
        const string PSEUDOCLASS_INRANGE = "in-range";
        const string PSEUDOCLASS_OUTOFRANGE = "out-of-range";
        const string PSEUDOCLASS_OPTIONAL = "optional";
        const string PSEUDOCLASS_READONLY = "read-only";
        const string PSEUDOCLASS_READWRITE = "read-write";

        const string PSEUDOCLASSFUNCTION_DIR = "dir";
        const string PSEUDOCLASSFUNCTION_NTHCHILD = "nth-child";
        const string PSEUDOCLASSFUNCTION_NTHLASTCHILD = "nth-last-child";
        const string PSEUDOCLASSFUNCTION_NOT = "not";
        const string PSEUDOCLASSFUNCTION_LANG = "lang";
        const string PSEUDOCLASSFUNCTION_CONTAINS = "contains";

        const string PSEUDOELEMENT_BEFORE = "before";
        const string PSEUDOELEMENT_AFTER = "after";
        const string PSEUDOELEMENT_SELECTION = "selection";
        const string PSEUDOELEMENT_FIRSTLINE = "first-line";
        const string PSEUDOELEMENT_FIRSTLETTER = "first-letter";

        #endregion

        #region Members

        Selector temp;
        ListSelector group;
        Boolean hasCombinator;
        Boolean ignoreErrors;
        CssCombinator combinator;
        ComplexSelector complex;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new constructor object.
        /// </summary>
        public CssSelectorConstructor()
        {
            combinator = CssCombinator.Descendent;
            hasCombinator = false;
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
                else if (temp != null)
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
        /// <param name="tokens">The stream of tokens to consider.</param>
        public void PickSelector(IEnumerator<CssToken> tokens)
        {
            switch (tokens.Current.Type)
            {
                //Begin of attribute [A]
                case CssTokenType.SquareBracketOpen:
                    OnAttribute(tokens);
                    break;

                //Begin of Pseudo :P
                case CssTokenType.Colon:
                    OnColon(tokens);
                    break;

                //Begin of ID #I
                case CssTokenType.Hash:
                    Insert(SimpleSelector.Id(((CssKeywordToken)tokens.Current).Data));
                    break;

                //Begin of Type E
                case CssTokenType.Ident:
                    Insert(SimpleSelector.Type(((CssKeywordToken)tokens.Current).Data));
                    break;

                //Whitespace could be significant
                case CssTokenType.Whitespace:
                    Insert(CssCombinator.Descendent);
                    break;

                //Various
                case CssTokenType.Delim:
                    OnDelim(tokens);
                    break;

                case CssTokenType.Comma:
                    InsertOr();
                    break;

                default:
                    if (!ignoreErrors) throw new DOMException(ErrorCode.SyntaxError);
                    break;
            }
        }

        /// <summary>
        /// Inserts a comma.
        /// </summary>
        public void InsertOr()
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
        public void Insert(Selector selector)
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
        public void Insert(CssCombinator cssCombinator)
        {
            hasCombinator = true;

            if (cssCombinator != CssCombinator.Descendent)
                combinator = cssCombinator;
        }

        /// <summary>
        /// Invoked once a delimiter has been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        public void OnDelim(IEnumerator<CssToken> tokens)
        {
            var chr = ((CssDelimToken)tokens.Current).Data;

            switch (chr)
            {
                case Specification.COMMA:
                    InsertOr();
                    break;

                case Specification.GT:
                    Insert(CssCombinator.Child);
                    break;

                case Specification.PLUS:
                    Insert(CssCombinator.AdjacentSibling);
                    break;

                case Specification.TILDE:
                    Insert(CssCombinator.Sibling);
                    break;

                case Specification.ASTERISK:
                    Insert(SimpleSelector.All);
                    break;

                case Specification.FS:
                    if (tokens.MoveNext() && tokens.Current.Type == CssTokenType.Ident)
                    {
                        var cls = (CssKeywordToken)tokens.Current;
                        Insert(SimpleSelector.Class(cls.Data));
                    }
                    else if (!ignoreErrors) 
                        throw new DOMException(ErrorCode.SyntaxError);
                    else
                        return;

                    break;
            }
        }

        /// <summary>
        /// Invoked once a square bracket has been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        public void OnAttribute(IEnumerator<CssToken> tokens)
        {
            var selector = GetAttributeSelector(tokens);

            if (selector != null)
                Insert(selector);
        }

        /// <summary>
        /// Invoked once a colon has been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        public void OnColon(IEnumerator<CssToken> tokens)
        {
            var selector = GetPseudoSelector(tokens);
            
            if(selector != null)
                Insert(selector);
        }

        /// <summary>
        /// Picks a simple selector from the stream of tokens.
        /// </summary>
        /// <param name="tokens">The stream of tokens to consider.</param>
        /// <returns>The created selector.</returns>
        public SimpleSelector GetSimpleSelector(IEnumerator<CssToken> tokens)
        {
            while (tokens.MoveNext())
            {
                switch (tokens.Current.Type)
                {
                    //Begin of attribute [A]
                    case CssTokenType.SquareBracketOpen:
                        {
                            var sel = GetAttributeSelector(tokens);
                            if (sel != null) return sel;
                        }
                        break;

                    //Begin of Pseudo :P
                    case CssTokenType.Colon:
                        {
                            var sel = GetPseudoSelector(tokens);
                            if (sel != null) return sel;
                        }
                        break;

                    //Begin of ID #I
                    case CssTokenType.Hash:
                        return SimpleSelector.Id(((CssKeywordToken)tokens.Current).Data);

                    //Begin of Type E
                    case CssTokenType.Ident:
                        return SimpleSelector.Type(((CssKeywordToken)tokens.Current).Data);

                    //Various
                    case CssTokenType.Delim:
                        if (((CssDelimToken)tokens.Current).Data == Specification.FS && tokens.MoveNext() && tokens.Current.Type == CssTokenType.Ident)
                            return SimpleSelector.Class(((CssKeywordToken)tokens.Current).Data);
                        break;

                    //All others are being ignored
                    case CssTokenType.Whitespace:
                    case CssTokenType.Comma:
                    default:
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Invoked once a colonhas been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        /// <returns>The created selector.</returns>
        public SimpleSelector GetPseudoSelector(IEnumerator<CssToken> tokens)
        {
            SimpleSelector sel = null;

            if (tokens.MoveNext())
            {
                if (tokens.Current.Type == CssTokenType.Colon)
                    sel = GetPseudoElement(tokens);
                else if (tokens.Current.Type == CssTokenType.Function)
                    sel = GetPseudoClassFunction(tokens);
                else if (tokens.Current.Type == CssTokenType.Ident)
                    sel = GetPseudoClassIdentifier(tokens);
            }

            if (sel == null && !ignoreErrors)
                throw new DOMException(ErrorCode.SyntaxError);

            return sel;
        }

        /// <summary>
        /// Invoked once a colon with an identifier has been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        /// <returns>The created selector.</returns>
        public SimpleSelector GetPseudoClassIdentifier(IEnumerator<CssToken> tokens)
        {
            switch (((CssKeywordToken)tokens.Current).Data)
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
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && !((HTMLAnchorElement)el).IsVisited;
                        else if (el is HTMLAreaElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && !((HTMLAreaElement)el).IsVisited;
                        else if (el is HTMLLinkElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && !((HTMLLinkElement)el).IsVisited;

                        return false;
                    }, PSEUDOCLASS_LINK);

                case PSEUDOCLASS_VISITED:
                    return SimpleSelector.PseudoClass(el =>
                    {
                        if (el is HTMLAnchorElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && ((HTMLAnchorElement)el).IsVisited;
                        else if (el is HTMLAreaElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && ((HTMLAreaElement)el).IsVisited;
                        else if (el is HTMLLinkElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && ((HTMLLinkElement)el).IsVisited;

                        return false;
                    }, PSEUDOCLASS_VISITED);

                case PSEUDOCLASS_ACTIVE:
                    return SimpleSelector.PseudoClass(el => 
                    {
                        if (el is HTMLAnchorElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && ((HTMLAnchorElement)el).IsActive;
                        else if (el is HTMLAreaElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && ((HTMLAreaElement)el).IsActive;
                        else if (el is HTMLLinkElement)
                            return !string.IsNullOrEmpty(el.GetAttribute("href")) && ((HTMLLinkElement)el).IsActive;
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
                            return string.IsNullOrEmpty(el.GetAttribute("disabled")) && ((HTMLMenuItemElement)el).IsActive;

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
                            return !string.IsNullOrEmpty(el.GetAttribute("href"));
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
                            return string.IsNullOrEmpty(el.GetAttribute("disabled"));

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
                            return !string.IsNullOrEmpty(el.GetAttribute("disabled"));

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
                            return !string.IsNullOrEmpty(el.GetAttribute("selected"));

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
                            return string.IsNullOrEmpty(el.GetAttribute("value"));

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
                            return ((HTMLFormElement)el).IsValid;

                        return false;
                    }, PSEUDOCLASS_VALID);

                case PSEUDOCLASS_INVALID:
                    return SimpleSelector.PseudoClass(el =>
                    {
                        if (el is IValidation)
                            return !((IValidation)el).CheckValidity();
                        else if (el is HTMLFormElement)
                            return !((HTMLFormElement)el).IsValid;
                        else if (el is HTMLFieldSetElement)
                            return ((HTMLFieldSetElement)el).IsInvalid;

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

        /// <summary>
        /// Invoked once a colon with a function has been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        /// <returns>The created selector.</returns>
        public SimpleSelector GetPseudoClassFunction(IEnumerator<CssToken> tokens)
        {
            var name = ((CssKeywordToken)tokens.Current).Data;
            var args = new List<CssToken>();

            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == CssTokenType.RoundBracketClose)
                    break;
                else
                    args.Add(tokens.Current);
            }

            if (args.Count == 0)
                return null;

            switch (name)
            {
                case PSEUDOCLASSFUNCTION_NTHCHILD:
                    return GetArguments<NthChildSelector>(args.GetEnumerator());

                case PSEUDOCLASSFUNCTION_NTHLASTCHILD:
                    return GetArguments<NthLastChildSelector>(args.GetEnumerator());

                case PSEUDOCLASSFUNCTION_DIR:
                    if (args.Count == 1 && args[0].Type == CssTokenType.Ident)
                    {
                        var dir = ((CssKeywordToken)args[0]).Data;
                        var code = string.Format("{0}({1})", PSEUDOCLASSFUNCTION_DIR, dir);
                        var dirCode = dir == "ltr" ? DirectionMode.Ltr : DirectionMode.Rtl;
                        return SimpleSelector.PseudoClass(el => el.Dir == dirCode, code);
                    }

                    break;

                case PSEUDOCLASSFUNCTION_LANG:
                    if (args.Count == 1 && args[0].Type == CssTokenType.Ident)
                    {
                        var lang = ((CssKeywordToken)args[0]).Data;
                        var code = string.Format("{0}({1})", PSEUDOCLASSFUNCTION_LANG, lang);
                        return SimpleSelector.PseudoClass(el => el.Lang.Equals(lang, StringComparison.InvariantCultureIgnoreCase), code);
                    }

                    break;

                case PSEUDOCLASSFUNCTION_CONTAINS:
                    if (args.Count == 1 && args[0].Type == CssTokenType.String)
                    {
                        var str = ((CssStringToken)args[0]).Data;
                        var code = string.Format("{0}({1})", PSEUDOCLASSFUNCTION_CONTAINS, str);
                        return SimpleSelector.PseudoClass(el => el.TextContent.Contains(str), code);
                    }
                    else if (args.Count == 1 && args[0].Type == CssTokenType.Ident)
                    {
                        var str = ((CssKeywordToken)args[0]).Data;
                        var code = string.Format("{0}({1})", PSEUDOCLASSFUNCTION_CONTAINS, str);
                        return SimpleSelector.PseudoClass(el => el.TextContent.Contains(str), code);
                    }

                    break;

                case PSEUDOCLASSFUNCTION_NOT:
                    {
                        var sel = GetSimpleSelector(args.GetEnumerator());
                        if (sel != null)
                        {
                            var code = string.Format("{0}({1})", PSEUDOCLASSFUNCTION_NOT, sel.ToCss());
                            return SimpleSelector.PseudoClass(el => !sel.Match(el), code);
                        }
                    }
                    break;
            }

            if (!ignoreErrors) 
                throw new DOMException(ErrorCode.SyntaxError);

            return null;
        }

        /// <summary>
        /// Invoked once two colons has been found in the token enumerator.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        /// <returns>The created selector.</returns>
        public SimpleSelector GetPseudoElement(IEnumerator<CssToken> tokens)
        {
            if (tokens.MoveNext() && tokens.Current.Type == CssTokenType.Ident)
            {
                var data = ((CssKeywordToken)tokens.Current).Data;

                switch (data)
                {
                    case PSEUDOELEMENT_BEFORE:
                        return SimpleSelector.PseudoElement(MatchBefore, PSEUDOELEMENT_BEFORE);
                    case PSEUDOELEMENT_AFTER:
                        return SimpleSelector.PseudoElement(MatchAfter, PSEUDOELEMENT_AFTER);
                    case PSEUDOELEMENT_SELECTION:
                        return SimpleSelector.PseudoElement(el => true, PSEUDOELEMENT_SELECTION);
                    case PSEUDOELEMENT_FIRSTLINE:
                        return SimpleSelector.PseudoElement(MatchFirstLine, PSEUDOELEMENT_FIRSTLINE);
                    case PSEUDOELEMENT_FIRSTLETTER:
                        return SimpleSelector.PseudoElement(MatchFirstLetter, PSEUDOELEMENT_FIRSTLETTER);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the attribute selector for the specified sequence of tokens.
        /// </summary>
        /// <param name="tokens">The token source.</param>
        /// <returns>The created selector.</returns>
        public SimpleSelector GetAttributeSelector(IEnumerator<CssToken> tokens)
        {
            var values = new List<string>();
            CssToken op = null;

            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == CssTokenType.SquareBracketClose)
                    break;
                else if (tokens.Current.Type == CssTokenType.Ident)
                    values.Add(((CssKeywordToken)tokens.Current).Data);
                else if (tokens.Current.Type == CssTokenType.String)
                    values.Add(((CssStringToken)tokens.Current).Data);
                else if (tokens.Current.Type == CssTokenType.Number)
                    values.Add(((CssNumberToken)tokens.Current).Data.ToString());
                else if (op == null && (tokens.Current is CssMatchToken || tokens.Current.Type == CssTokenType.Delim))
                    op = tokens.Current;
                else if (tokens.Current.Type != CssTokenType.Whitespace)
                {
                    if (!ignoreErrors) throw new DOMException(ErrorCode.SyntaxError);
                    return null;
                }
            }

            if ((op == null || values.Count != 2) && (op != null || values.Count != 1))
            {
                if (!ignoreErrors) throw new DOMException(ErrorCode.SyntaxError);
                return null;
            }

            if (op == null)
                return SimpleSelector.AttrAvailable(values[0]);

            switch (op.ToValue())
            {
                case "=":
                    return SimpleSelector.AttrMatch(values[0], values[1]);
                case "~=":
                    return SimpleSelector.AttrList(values[0], values[1]);
                case "|=":
                    return SimpleSelector.AttrHyphen(values[0], values[1]);
                case "^=":
                    return SimpleSelector.AttrBegins(values[0], values[1]);
                case "$=":
                    return SimpleSelector.AttrEnds(values[0], values[1]);
                case "*=":
                    return SimpleSelector.AttrContains(values[0], values[1]);
                case "!=":
                    return SimpleSelector.AttrNotMatch(values[0], values[1]);
            }

            if (!ignoreErrors) throw new DOMException(ErrorCode.SyntaxError);
            return null;
        }

        /// <summary>
        /// Takes string and transforms it into the arguments for the nth-child function.
        /// </summary>
        /// <param name="it">The token source..</param>
        /// <returns>The function.</returns>
        T GetArguments<T>(IEnumerator<CssToken> it) where T : NthChildSelector, new()
        {
            var f = new T();
            var repr = string.Empty;

            while (it.MoveNext())
            {
                switch (it.Current.Type)
                {
                    case CssTokenType.Ident:
                    case CssTokenType.Number:
                    case CssTokenType.Dimension:
                    case CssTokenType.Whitespace:
                        repr += it.Current.ToValue();
                        break;

                    case CssTokenType.Delim:
                        var chr = ((CssDelimToken)it.Current).Data;

                        if (chr == Specification.PLUS || chr == Specification.DASH)
                        {
                            repr += chr;
                            break;
                        }

                        goto default;

                    default:
                        if (!ignoreErrors) throw new DOMException(ErrorCode.SyntaxError);
                        return f;
                }
            }

            repr = repr.Trim();

            if (repr.Equals(NTH_CHILD_ODD, StringComparison.OrdinalIgnoreCase))
            {
                f.step = 2;
                f.offset = 1;
            }
            else if (repr.Equals(NTH_CHILD_EVEN, StringComparison.OrdinalIgnoreCase))
            {
                f.step = 2;
                f.offset = 0;
            }
            else if (!int.TryParse(repr, out f.offset))
            {
                var index = repr.IndexOf(NTH_CHILD_N, StringComparison.OrdinalIgnoreCase);

                if (repr.Length > 0 && index != -1)
                {
                    var first = repr.Substring(0, index).Replace(" ", "");
                    var second = repr.Substring(index + 1).Replace(" ", "");

                    if (first == string.Empty || (first.Length == 1 && first[0] == Specification.PLUS))
                        f.step = 1;
                    else if (first.Length == 1 && first[0] == Specification.DASH)
                        f.step = -1;
                    else if (!int.TryParse(first, out f.step))
                        throw new DOMException(ErrorCode.SyntaxError);

                    if (second == string.Empty)
                        f.offset = 0;
                    else if (!int.TryParse(second, out f.offset) && !ignoreErrors) 
                        throw new DOMException(ErrorCode.SyntaxError);
                }
                else if (!ignoreErrors) 
                    throw new DOMException(ErrorCode.SyntaxError);
            }

            return f;
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

        class NthChildSelector : SimpleSelector
        {
            public int step;
            public int offset;

            public override int Specifity
            {
                get { return 10; }
            }

            public override bool Match(Element element)
            {
                var parent = element.ParentNode;

                if (parent == null)
                    return false;

                var n = 1;

                for (int i = 0; i < parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] == element)
                        return step == 0 ? n == offset : (n - offset) % step == 0;
                    else if (parent.ChildNodes[i] is Element)
                        n++;
                }

                return true;
            }

            public override string ToCss()
            {
                return string.Format(":{0}({1}n+{2})", CssSelectorConstructor.PSEUDOCLASSFUNCTION_NTHCHILD, step, offset);
            }
        }

        class NthLastChildSelector : NthChildSelector
        {
            public override bool Match(Element element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                var n = 1;

                for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == element)
                        return step == 0 ? n == offset : (n - offset) % step == 0;
                    else if (parent.ChildNodes[i] is Element)
                        n++;
                }

                return true;
            }

            public override string ToCss()
            {
                return string.Format(":{0}({1}n+{2})", CssSelectorConstructor.PSEUDOCLASSFUNCTION_NTHLASTCHILD, step, offset);
            }
        }

        class FirstChildSelector : SimpleSelector
        {
            private FirstChildSelector()
            { }

            static FirstChildSelector instance;

            public static FirstChildSelector Instance
            {
                get { return instance ?? (instance = new FirstChildSelector()); }
            }

            public override int Specifity
            {
                get { return 10; }
            }

            public override bool Match(Element element)
            {
                var parent = element.ParentNode;

                if (parent == null)
                    return false;

                for (int i = 0; i <= parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] == element)
                        return true;
                    else if (parent.ChildNodes[i] is Element)
                        return false;
                }

                return false;
            }

            public override string ToCss()
            {
                return ":" + CssSelectorConstructor.PSEUDOCLASS_FIRSTCHILD;
            }
        }

        class LastChildSelector : SimpleSelector
        {
            private LastChildSelector()
            { }

            static LastChildSelector instance;

            public static LastChildSelector Instance
            {
                get { return instance ?? (instance = new LastChildSelector()); }
            }

            public override int Specifity
            {
                get { return 10; }
            }

            public override bool Match(Element element)
            {
                var parent = element.ParentElement;

                if (parent == null)
                    return false;

                for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == element)
                        return true;
                    else if (parent.ChildNodes[i] is Element)
                        return false;
                }

                return false;
            }

            public override string ToCss()
            {
                return ":" + CssSelectorConstructor.PSEUDOCLASS_LASTCHILD;
            }
        }

        #endregion
    }
}
