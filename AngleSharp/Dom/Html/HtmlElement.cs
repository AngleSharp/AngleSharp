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

        #region ctor

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
                    CreateBindings(_dropZone, AttributeNames.DropZone);
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
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var style = this.GetOwnAttribute(AttributeNames.Style);
            RegisterAttributeObserver(AttributeNames.Style, UpdateStyle);

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
