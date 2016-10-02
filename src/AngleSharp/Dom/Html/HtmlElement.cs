namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Threading;

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

        static HtmlElement()
        {
            RegisterCallback<HtmlElement>(AttributeNames.Style, (element, value) => element.UpdateStyle(value));
            RegisterCallback<HtmlElement>(AttributeNames.DropZone, (element, value) => element._dropZone?.Update(value));
            RegisterEventCallback<HtmlElement>(EventNames.Load);
            RegisterEventCallback<HtmlElement>(EventNames.Abort);
            RegisterEventCallback<HtmlElement>(EventNames.Blur);
            RegisterEventCallback<HtmlElement>(EventNames.Cancel);
            RegisterEventCallback<HtmlElement>(EventNames.CanPlay);
            RegisterEventCallback<HtmlElement>(EventNames.CanPlayThrough);
            RegisterEventCallback<HtmlElement>(EventNames.Change);
            RegisterEventCallback<HtmlElement>(EventNames.Click);
            RegisterEventCallback<HtmlElement>(EventNames.CueChange);
            RegisterEventCallback<HtmlElement>(EventNames.DblClick);
            RegisterEventCallback<HtmlElement>(EventNames.Drag);
            RegisterEventCallback<HtmlElement>(EventNames.DragEnd);
            RegisterEventCallback<HtmlElement>(EventNames.DragEnter);
            RegisterEventCallback<HtmlElement>(EventNames.DragExit);
            RegisterEventCallback<HtmlElement>(EventNames.DragLeave);
            RegisterEventCallback<HtmlElement>(EventNames.DragOver);
            RegisterEventCallback<HtmlElement>(EventNames.DragStart);
            RegisterEventCallback<HtmlElement>(EventNames.Drop);
            RegisterEventCallback<HtmlElement>(EventNames.DurationChange);
            RegisterEventCallback<HtmlElement>(EventNames.Emptied);
            RegisterEventCallback<HtmlElement>(EventNames.Ended);
            RegisterEventCallback<HtmlElement>(EventNames.Error);
            RegisterEventCallback<HtmlElement>(EventNames.Focus);
            RegisterEventCallback<HtmlElement>(EventNames.Input);
            RegisterEventCallback<HtmlElement>(EventNames.Invalid);
            RegisterEventCallback<HtmlElement>(EventNames.Keydown);
            RegisterEventCallback<HtmlElement>(EventNames.Keypress);
            RegisterEventCallback<HtmlElement>(EventNames.Keyup);
            RegisterEventCallback<HtmlElement>(EventNames.LoadedData);
            RegisterEventCallback<HtmlElement>(EventNames.LoadedMetaData);
            RegisterEventCallback<HtmlElement>(EventNames.LoadStart);
            RegisterEventCallback<HtmlElement>(EventNames.Mousedown);
            RegisterEventCallback<HtmlElement>(EventNames.Mouseup);
            RegisterEventCallback<HtmlElement>(EventNames.Mouseenter);
            RegisterEventCallback<HtmlElement>(EventNames.Mouseleave);
            RegisterEventCallback<HtmlElement>(EventNames.Mouseover);
            RegisterEventCallback<HtmlElement>(EventNames.Mousemove);
            RegisterEventCallback<HtmlElement>(EventNames.Wheel);
            RegisterEventCallback<HtmlElement>(EventNames.Pause);
            RegisterEventCallback<HtmlElement>(EventNames.Play);
            RegisterEventCallback<HtmlElement>(EventNames.Playing);
            RegisterEventCallback<HtmlElement>(EventNames.Progress);
            RegisterEventCallback<HtmlElement>(EventNames.RateChange);
            RegisterEventCallback<HtmlElement>(EventNames.Reset);
            RegisterEventCallback<HtmlElement>(EventNames.Resize);
            RegisterEventCallback<HtmlElement>(EventNames.Scroll);
            RegisterEventCallback<HtmlElement>(EventNames.Seeked);
            RegisterEventCallback<HtmlElement>(EventNames.Seeking);
            RegisterEventCallback<HtmlElement>(EventNames.Select);
            RegisterEventCallback<HtmlElement>(EventNames.Show);
            RegisterEventCallback<HtmlElement>(EventNames.Stalled);
            RegisterEventCallback<HtmlElement>(EventNames.Submit);
            RegisterEventCallback<HtmlElement>(EventNames.Suspend);
            RegisterEventCallback<HtmlElement>(EventNames.TimeUpdate);
            RegisterEventCallback<HtmlElement>(EventNames.Toggle);
            RegisterEventCallback<HtmlElement>(EventNames.VolumeChange);
            RegisterEventCallback<HtmlElement>(EventNames.Waiting);
        }

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

        protected static void RegisterEventCallback<TElement>(String eventName)
            where TElement : Element
        {
            var name = "on" + eventName;
            RegisterCallback<TElement>(name, (element, value) =>
            {
                var document = element.Owner;
                var configuration = document.Options;
                var engine = configuration.GetScriptEngine(MimeTypeNames.DefaultJavaScript);

                if (engine != null)
                {
                    var function = "undefined";

                    if (!String.IsNullOrEmpty(value))
                    {
                        function = String.Concat("function () { ", value, " }");
                    }
                    
                    var response = VirtualResponse.Create(res => res.Content($"element.{name} = {function};"));
                    var options = new ScriptOptions(document);
                    engine.EvaluateScriptAsync(response, options, CancellationToken.None);
                }
            });
        }

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
