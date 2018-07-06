namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    class HtmlElement : Element, IHtmlElement
    {
        #region Fields

        private StringMap _dataset;
        private IHtmlMenuElement _menu;
        private SettableTokenList _dropZone;

        #endregion

        #region Events

        public event DomEventHandler Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        public event DomEventHandler Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        public event DomEventHandler Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        public event DomEventHandler CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        public event DomEventHandler CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        public event DomEventHandler Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        public event DomEventHandler Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        public event DomEventHandler CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        public event DomEventHandler DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        public event DomEventHandler Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        public event DomEventHandler DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        public event DomEventHandler DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        public event DomEventHandler DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        public event DomEventHandler DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        public event DomEventHandler DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        public event DomEventHandler DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        public event DomEventHandler Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        public event DomEventHandler DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        public event DomEventHandler Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        public event DomEventHandler Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        public event DomEventHandler Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        public event DomEventHandler Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        public event DomEventHandler Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        public event DomEventHandler Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        public event DomEventHandler KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        public event DomEventHandler KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        public event DomEventHandler KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        public event DomEventHandler Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        public event DomEventHandler LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        public event DomEventHandler LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        public event DomEventHandler Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        public event DomEventHandler MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        public event DomEventHandler MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        public event DomEventHandler MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        public event DomEventHandler MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        public event DomEventHandler MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        public event DomEventHandler MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        public event DomEventHandler MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        public event DomEventHandler MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        public event DomEventHandler Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        public event DomEventHandler Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        public event DomEventHandler Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        public event DomEventHandler Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        public event DomEventHandler RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        public event DomEventHandler Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        public event DomEventHandler Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        public event DomEventHandler Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        public event DomEventHandler Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        public event DomEventHandler Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        public event DomEventHandler Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        public event DomEventHandler Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        public event DomEventHandler Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        public event DomEventHandler Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        public event DomEventHandler Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        public event DomEventHandler TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        public event DomEventHandler Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        public event DomEventHandler VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        public event DomEventHandler Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        #endregion

        #region ctor

        public HtmlElement(Document owner, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
        {
        }

        #endregion

        #region Properties

        public Boolean IsHidden
        {
            get { return this.GetBoolAttribute(AttributeNames.Hidden); }
            set { this.SetBoolAttribute(AttributeNames.Hidden, value); }
        }

        public IHtmlMenuElement ContextMenu
        {
            get
            {
                if (_menu == null)
                {
                    var id = this.GetOwnAttribute(AttributeNames.ContextMenu);

                    if (!String.IsNullOrEmpty(id))
                    {
                        return Owner.GetElementById(id) as IHtmlMenuElement;
                    }
                }

                return _menu;
            }
            set { _menu = value; }
        }

        public ISettableTokenList DropZone
        {
            get
            { 
                if (_dropZone == null)
                {
                    _dropZone = new SettableTokenList(this.GetOwnAttribute(AttributeNames.DropZone));
                    _dropZone.Changed += value => UpdateAttribute(AttributeNames.DropZone, value);
                }

                return _dropZone;
            }
        }

        public Boolean IsDraggable
        {
            get { return this.GetOwnAttribute(AttributeNames.Draggable).ToBoolean(false); }
            set { this.SetOwnAttribute(AttributeNames.Draggable, value.ToString()); }
        }

        public String AccessKey
        {
            get { return this.GetOwnAttribute(AttributeNames.AccessKey) ?? String.Empty; }
            set { this.SetOwnAttribute(AttributeNames.AccessKey, value); }
        }

        public String AccessKeyLabel
        {
            get { return AccessKey; }
        }

        public String Language
        {
            get { return this.GetOwnAttribute(AttributeNames.Lang) ?? GetDefaultLanguage(); }
            set { this.SetOwnAttribute(AttributeNames.Lang, value); }
        }

        public String Title
        {
            get { return this.GetOwnAttribute(AttributeNames.Title); }
            set { this.SetOwnAttribute(AttributeNames.Title, value); }
        }

        public String Direction
        {
            get { return this.GetOwnAttribute(AttributeNames.Dir); }
            set { this.SetOwnAttribute(AttributeNames.Dir, value); }
        }

        public Boolean IsSpellChecked
        {
            get { return this.GetOwnAttribute(AttributeNames.Spellcheck).ToBoolean(false); }
            set { this.SetOwnAttribute(AttributeNames.Spellcheck, value.ToString()); }
        }

        public Int32 TabIndex
        {
            get { return this.GetOwnAttribute(AttributeNames.TabIndex).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.TabIndex, value.ToString()); }
        }

        public IStringMap Dataset
        {
            get { return _dataset ?? (_dataset = new StringMap("data-", this)); }
        }

        public String ContentEditable
        {
            get { return this.GetOwnAttribute(AttributeNames.ContentEditable); }
            set { this.SetOwnAttribute(AttributeNames.ContentEditable, value); }
        }

        public Boolean IsContentEditable
        {
            get
            {
                var value = ContentEditable.ToEnum(ContentEditableMode.Inherited);

                if (value != ContentEditableMode.True)
                {
                    var parent = ParentElement as IHtmlElement;

                    if (value == ContentEditableMode.Inherited && parent != null)
                    {
                        return parent.IsContentEditable;
                    }

                    return false;
                }

                return true;
            }
        }

        public Boolean IsTranslated
        {
            get { return this.GetOwnAttribute(AttributeNames.Translate).ToEnum(SimpleChoice.Yes) == SimpleChoice.Yes; }
            set { this.SetOwnAttribute(AttributeNames.Translate, value ? Keywords.Yes : Keywords.No); }
        }

        public String InnerText
        {
            get
            {
                bool? hidden = null;
                if (Owner == null)
                {
                    hidden = true;
                }
                if (!hidden.HasValue)
                {
                    var css = this.ComputeCurrentStyle();
                    if (!String.IsNullOrEmpty(css?.Display))
                    {
                        hidden = css.Display == "none";
                    }
                }
                if (!hidden.HasValue)
                {
                    hidden = IsHidden;
                }
                if (hidden.Value)
                {
                    return TextContent;
                }

                var sb = Pool.NewStringBuilder();
                var requiredLineBreakCounts = new Dictionary<Int32, Int32>();

                InnerTextCollection(this, sb, requiredLineBreakCounts, ParentElement?.ComputeCurrentStyle());

                // Remove any runs of consecutive required line break count items at the start or end of results.
                requiredLineBreakCounts.Remove(0);
                requiredLineBreakCounts.Remove(sb.Length);

                var offset = 0;
                foreach (var keyval in requiredLineBreakCounts.OrderBy(kv => kv.Key)) // SortedDictionary would be nicer
                {
                    var index = keyval.Key + offset;
                    sb.Insert(index, new String(Symbols.LineFeed, keyval.Value));
                    offset += keyval.Value;
                }

                return sb.ToPool();
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    ReplaceAll(null, false);
                }
                else
                {
                    var fragment = new DocumentFragment(Owner);

                    var sb = Pool.NewStringBuilder();
                    for (var i = 0; i < value.Length; i++)
                    {
                        var c = value[i];

                        if (c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                        {
                            if (c == Symbols.CarriageReturn && i + 1 < value.Length && value[i + 1] == Symbols.LineFeed)
                            {
                                continue; // ignore carriage return if the next char is a line feed
                            }

                            if (sb.Length > 0)
                            {
                                fragment.AppendChild(new TextNode(Owner, sb.ToPool()));
                                sb = Pool.NewStringBuilder();
                            }
                            fragment.AppendChild(new HtmlBreakRowElement(Owner));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }

                    var remaining = sb.ToPool();
                    if (remaining.Length > 0)
                    {
                        fragment.Append(new TextNode(Owner, remaining));
                    }

                    ReplaceAll(fragment, false);
                }
            }
        }

        private static void InnerTextCollection(INode node, StringBuilder sb, Dictionary<Int32, Int32> requiredLineBreakCounts, ICssStyleDeclaration parentStyle)
        {
            if (!HasCssBox(node))
            {
                return;
            }

            var elementCss = (node as IElement)?.ComputeCurrentStyle();

            bool? elementHidden = null;
            if (elementCss != null)
            {
                if (!String.IsNullOrEmpty(elementCss.Display))
                {
                    elementHidden = elementCss.Display == "none";
                }
                if (!String.IsNullOrEmpty(elementCss.Visibility) && elementHidden != true)
                {
                    elementHidden = elementCss.Visibility != "visible";
                }
            }
            if (!elementHidden.HasValue)
            {
                elementHidden = (node as IHtmlElement)?.IsHidden ?? false;
            }
            if (elementHidden.Value)
            {
                return;
            }

            var startIndex = sb.Length;

            foreach (var child in node.ChildNodes)
            {
                InnerTextCollection(child, sb, requiredLineBreakCounts, elementCss);
            }

            if (node is IText)
            {
                var textElement = (IText)node;

                ProcessText(textElement.Data, sb, parentStyle);
            }
            else if (node is IHtmlBreakRowElement)
            {
                sb.Append(Symbols.LineFeed);
            }
            else if ((node is IHtmlTableCellElement && String.IsNullOrEmpty(elementCss.Display)) || elementCss.Display == "table-cell")
            {
                var nextSibling = node.NextSibling as IElement;
                if (nextSibling != null)
                {
                    var nextSiblingCss = nextSibling.ComputeCurrentStyle();
                    if (nextSibling is IHtmlTableCellElement && String.IsNullOrEmpty(nextSiblingCss.Display) || nextSiblingCss.Display == "table-cell")
                    {
                        sb.Append(Symbols.Tab);
                    }
                }
            }
            else if ((node is IHtmlTableRowElement && String.IsNullOrEmpty(elementCss.Display)) || elementCss.Display == "table-row")
            {
                var nextSibling = node.NextSibling as IElement;
                if (nextSibling != null)
                {
                    var nextSiblingCss = nextSibling.ComputeCurrentStyle();
                    if (nextSibling is IHtmlTableRowElement && String.IsNullOrEmpty(nextSiblingCss.Display) || nextSiblingCss.Display == "table-row")
                    {
                        sb.Append(Symbols.LineFeed);
                    }
                }
            }
            else if (node is IHtmlParagraphElement)
            {
                var startIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(startIndex, out startIndexCount);
                if (startIndexCount < 2)
                {
                    requiredLineBreakCounts[startIndex] = 2;
                }
                var endIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(sb.Length, out endIndexCount);
                if (endIndexCount < 2)
                {
                    requiredLineBreakCounts[sb.Length] = 2;
                }
            }

            bool? isBlockLevel = null;
            if (elementCss != null)
            {
                if (IsBlockLevelDisplay(elementCss.Display))
                {
                    isBlockLevel = true;
                }
            }
            if (!isBlockLevel.HasValue)
            {
                isBlockLevel = IsBlockLevel(node);
            }
            if (isBlockLevel.Value)
            {
                var startIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(startIndex, out startIndexCount);
                if (startIndexCount < 1)
                {
                    requiredLineBreakCounts[startIndex] = 1;
                }
                var endIndexCount = 0;
                requiredLineBreakCounts.TryGetValue(sb.Length, out endIndexCount);
                if (endIndexCount < 1)
                {
                    requiredLineBreakCounts[sb.Length] = 1;
                }
            }
        }

        private static Boolean HasCssBox(INode node)
        {
            switch (node.NodeName)
            {
                case "CANVAS":
                case "COL":
                case "COLGROUP":
                case "DETAILS":
                case "FRAME":
                case "FRAMESET":
                case "IFRAME":
                case "IMG":
                case "INPUT":
                case "LINK":
                case "METER":
                case "PROGRESS":
                case "TEMPLATE":
                case "TEXTAREA":
                case "VIDEO":
                case "WBR":
                case "SCRIPT":
                case "STYLE":
                case "NOSCRIPT":
                    return false;
                default:
                    return true;
            }
        }

        private static bool IsBlockLevelDisplay(String display)
        {
            // https://www.w3.org/TR/css-display-3/#display-value-summary
            // https://hg.mozilla.org/mozilla-central/file/0acceb224b7d/servo/components/layout/query.rs#l1016
            switch (display)
            {
                case "block":
                case "flow-root":
                case "flex":
                case "grid":
                case "table":
                case "table-caption":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsBlockLevel(INode node)
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Block-level_elements
            switch (node.NodeName)
            {
                case "ADDRESS":
                case "ARTICLE":
                case "ASIDE":
                case "BLOCKQUOTE":
                case "CANVAS":
                case "DD":
                case "DIV":
                case "DL":
                case "DT":
                case "FIELDSET":
                case "FIGCAPTION":
                case "FIGURE":
                case "FOOTER":
                case "FORM":
                case "H1":
                case "H2":
                case "H3":
                case "H4":
                case "H5":
                case "H6":
                case "HEADER":
                case "GROUP":
                case "HR":
                case "LI":
                case "MAIN":
                case "NAV":
                case "NOSCRIPT":
                case "OL":
                case "OPTION": 
                case "OUTPUT":
                case "P":
                case "PRE":
                case "SECTION":
                case "TABLE":
                case "TFOOT":
                case "UL":
                case "VIDEO":
                    return true;
                default:
                    return false;
            }
        }

        private static void ProcessText(String text, StringBuilder sb, ICssStyleDeclaration style)
        {
            var startIndex = sb.Length;
            var whiteSpace = style?.WhiteSpace;
            var textTransform = style?.TextTransform;

            var isWhiteSpace = startIndex > 0 ? Char.IsWhiteSpace(sb[startIndex - 1]) && sb[startIndex - 1] != Symbols.NoBreakSpace : true;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (Char.IsWhiteSpace(c) && c != Symbols.NoBreakSpace)
                {
                    // https://drafts.csswg.org/css-text/#white-space-property
                    switch (whiteSpace)
                    {
                        case "pre":
                        case "pre-wrap":
                            break;
                        case "pre-line":
                            if (c == Symbols.Space || c == Symbols.Tab)
                            {
                                if (!isWhiteSpace)
                                {
                                    c = Symbols.Space;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                        case "nowrap":
                        case "normal":
                        default:
                            if (!isWhiteSpace)
                            {
                                c = Symbols.Space;
                            }
                            else
                            {
                                continue;
                            }
                            break;
                    }

                    isWhiteSpace = true;
                }
                else
                {
                    // https://drafts.csswg.org/css-text/#propdef-text-transform
                    switch (textTransform)
                    {
                        case "uppercase":
                            c = Char.ToUpperInvariant(c);
                            break;
                        case "lowercase":
                            c = Char.ToLowerInvariant(c);
                            break;
                        case "capitalize":
                            if (isWhiteSpace)
                            {
                                c = Char.ToUpperInvariant(c);
                            }
                            break;
                        case "none":
                        default:
                            break;
                    }

                    isWhiteSpace = false;
                }

                sb.Append(c);
            }

            if (isWhiteSpace) // ended with whitespace
            {
                for (var offset = sb.Length - 1; offset >= startIndex; offset--)
                {
                    var c = sb[offset];
                    if (!Char.IsWhiteSpace(c) || c == Symbols.NoBreakSpace)
                    {
                        sb.Remove(offset + 1, sb.Length - 1 - offset);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Methods

        public void DoSpellCheck()
        {
            var spellcheck = Owner.Options.GetSpellCheck(Language);

            if (spellcheck != null)
            {
                //TODO
                //Go through all elements, finding single text nodes, then split
                //on word boundaries etc.
                //Finally check the single words, one by one.
                //Provide additional information on the Text Nodes which words
                //(if any) have errors.
            }
        }

        public virtual void DoClick()
        {
            IsClickedCancelled();
        }

        public virtual void DoFocus()
        {
            //Only certain elements can be focused
        }

        public virtual void DoBlur()
        {
            //Only certain elements can be focused
        }

        public override INode Clone(Boolean deep = true)
        {
            var factory = Owner.Options.GetFactory<IElementFactory<HtmlElement>>();
            var node = factory.Create(Owner, LocalName, Prefix);
            CloneElement(node, deep);
            return node;
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var style = this.GetOwnAttribute(AttributeNames.Style);

            if (style != null)
            {
                UpdateStyle(style);
            }
        }

        internal void UpdateDropZone(String value)
        {
            _dropZone?.Update(value);
        }

        protected Boolean IsClickedCancelled()
        {
            return this.Fire<MouseEvent>(m => m.Init(EventNames.Click, true, true, Owner.DefaultView, 0, 0, 0, 0, 0, false, false, false, false, MouseButton.Primary, this));
        }

        protected IHtmlFormElement GetAssignedForm()
        {
            var parent = Parent as INode;

            while (parent != null && parent is IHtmlFormElement == false)
            {
                parent = parent.ParentElement;
            }
            
            if (parent == null)
            {
                var formid = this.GetOwnAttribute(AttributeNames.Form);
                var owner = Owner;

                if (owner == null || parent != null || String.IsNullOrEmpty(formid))
                {
                    return null;
                }

                parent = owner.GetElementById(formid);
            }

            return parent as IHtmlFormElement;
        }

        #endregion

        #region Helpers

        private String GetDefaultLanguage()
        {
            var parent = ParentElement as IHtmlElement;
            return parent != null ? parent.Language : Owner.Options.GetLanguage();
        }

        private static String Combine(String prefix, String localName)
        {
            return (prefix != null ? String.Concat(prefix, ":", localName) : localName).ToUpperInvariant();
        }

        #endregion
    }
}
