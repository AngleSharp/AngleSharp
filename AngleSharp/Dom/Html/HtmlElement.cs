namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    class HtmlElement : Element, IHtmlElement
    {
        #region Fields

        StringMap _dataset;
        IHtmlMenuElement _menu;
        SettableTokenList _dropZone;

        #endregion

        #region Handlers

        event DomEventHandler IGlobalEventHandlers.Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        event DomEventHandler IGlobalEventHandlers.CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        event DomEventHandler IGlobalEventHandlers.CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        event DomEventHandler IGlobalEventHandlers.CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        event DomEventHandler IGlobalEventHandlers.KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        event DomEventHandler IGlobalEventHandlers.KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        event DomEventHandler IGlobalEventHandlers.KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        event DomEventHandler IGlobalEventHandlers.LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        event DomEventHandler IGlobalEventHandlers.LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        event DomEventHandler IGlobalEventHandlers.RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        event DomEventHandler IGlobalEventHandlers.TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        event DomEventHandler IGlobalEventHandlers.VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        #endregion

        #region ctor

        static HtmlElement()
        {
            RegisterCallback<HtmlElement>(AttributeNames.Style, (element, value) => element.UpdateStyle(value));
            RegisterCallback<HtmlElement>(AttributeNames.DropZone, (element, value) => element.TryUpdate(element._dropZone, value));
        }

        public HtmlElement(Document owner, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
        {
        }

        #endregion

        #region Properties

        public Boolean IsHidden
        {
            get { return this.HasOwnAttribute(AttributeNames.Hidden); }
            set { this.SetOwnAttribute(AttributeNames.Hidden, value ? String.Empty : null); }
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

        #endregion

        #region Methods

        public void DoSpellCheck()
        {
            var spellcheck = Owner.Options.GetSpellCheck(Language);

            if (spellcheck == null)
                return;

            //TODO
            //Go through all elements, finding single text nodes, then split on word boundaries etc.
            //Finally check the single words, one by one.
            //Provide additional information on the Text Nodes which words (if any) have errors.
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
            var node = Factory.HtmlElements.Create(Owner, LocalName, Prefix);
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

        String GetDefaultLanguage()
        {
            var parent = ParentElement as IHtmlElement;
            return parent != null ? parent.Language : Owner.Options.GetLanguage();
        }

        static String Combine(String prefix, String localName)
        {
            return (prefix != null ? String.Concat(prefix, ":", localName) : localName).ToUpperInvariant();
        }

        #endregion
    }
}
