namespace AngleSharp.Html.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Text;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    public class HtmlElement : Element, IHtmlElement
    {
        #region Fields

        private StringMap _dataset;
        private IHtmlMenuElement _menu;
        private SettableTokenList _dropZone;

        #endregion

        #region Events

        /// <inheritdoc />
        public event DomEventHandler Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        #endregion

        #region ctor

        /// <inheritdoc />
        public HtmlElement(Document owner, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
        {
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public Boolean IsHidden
        {
            get => this.GetBoolAttribute(AttributeNames.Hidden);
            set => this.SetBoolAttribute(AttributeNames.Hidden, value);
        }

        /// <inheritdoc />
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
            set => _menu = value;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Boolean IsDraggable
        {
            get => this.GetOwnAttribute(AttributeNames.Draggable).ToBoolean(false);
            set => this.SetOwnAttribute(AttributeNames.Draggable, value.ToString());
        }

        /// <inheritdoc />
        public String AccessKey
        {
            get => this.GetOwnAttribute(AttributeNames.AccessKey) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.AccessKey, value);
        }

        /// <inheritdoc />
        public String AccessKeyLabel => AccessKey;

        /// <inheritdoc />
        public String Language
        {
            get => this.GetOwnAttribute(AttributeNames.Lang) ?? GetDefaultLanguage();
            set => this.SetOwnAttribute(AttributeNames.Lang, value);
        }

        /// <inheritdoc />
        public String Title
        {
            get => this.GetOwnAttribute(AttributeNames.Title);
            set => this.SetOwnAttribute(AttributeNames.Title, value);
        }

        /// <inheritdoc />
        public String Direction
        {
            get => this.GetOwnAttribute(AttributeNames.Dir);
            set => this.SetOwnAttribute(AttributeNames.Dir, value);
        }

        /// <inheritdoc />
        public Boolean IsSpellChecked
        {
            get => this.GetOwnAttribute(AttributeNames.Spellcheck).ToBoolean(false);
            set => this.SetOwnAttribute(AttributeNames.Spellcheck, value.ToString());
        }

        /// <inheritdoc />
        public Int32 TabIndex
        {
            get => this.GetOwnAttribute(AttributeNames.TabIndex).ToInteger(0);
            set => this.SetOwnAttribute(AttributeNames.TabIndex, value.ToString());
        }

        /// <inheritdoc />
        public IStringMap Dataset => _dataset ?? (_dataset = new StringMap("data-", this));

        /// <inheritdoc />
        public String ContentEditable
        {
            get => this.GetOwnAttribute(AttributeNames.ContentEditable);
            set => this.SetOwnAttribute(AttributeNames.ContentEditable, value);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Boolean IsTranslated
        {
            get => this.GetOwnAttribute(AttributeNames.Translate).ToEnum(SimpleChoice.Yes) == SimpleChoice.Yes;
            set => this.SetOwnAttribute(AttributeNames.Translate, value ? Keywords.Yes : Keywords.No);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override IElement ParseSubtree(String html) => this.ParseHtmlSubtree(html);

        /// <inheritdoc />
        public void DoSpellCheck()
        {
            var spellcheck = Context.GetSpellCheck(Language);

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

        /// <inheritdoc />
        public virtual void DoClick() => IsClickedCancelled();

        /// <inheritdoc />
        public virtual void DoFocus()
        {
            //Only certain elements can be focused
        }

        /// <inheritdoc />
        public virtual void DoBlur()
        {
            //Only certain elements can be focused
        }

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            var factory = Context.GetFactory<IElementFactory<Document, HtmlElement>>();
            var node = factory.Create(owner, LocalName, Prefix);
            CloneElement(node, owner, deep);
            return node;
        }

        #endregion

        #region Internal Methods

        internal void UpdateDropZone(String value) => _dropZone?.Update(value);

        #endregion

        #region Helpers

        /// <inheritdoc />
        protected Task<Boolean> IsClickedCancelled() =>
            Owner.QueueTaskAsync(_ =>
                this.Fire<MouseEvent>(m =>
                    m.Init(EventNames.Click, true, true, Owner.DefaultView, 0, 0, 0, 0, 0, false, false, false, false, MouseButton.Primary, this)));

        /// <inheritdoc />
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

                if (owner == null || String.IsNullOrEmpty(formid))
                {
                    return null;
                }

                parent = owner.GetElementById(formid);
            }

            return parent as IHtmlFormElement;
        }

        private String GetDefaultLanguage() => ParentElement is IHtmlElement parent ? parent.Language : Context.GetLanguage();

        private static String Combine(String prefix, String localName) => (prefix != null ? String.Concat(prefix, ":", localName) : localName).ToUpperInvariant();

        #endregion
    }
}
