namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    class HtmlElement : Element, IHtmlElement
    {
        #region Fields

        CssStyleDeclaration _style;
        StringMap _dataset;
        IHtmlMenuElement _menu;
        SettableTokenList _dropZone;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a standard HTML element.
        /// </summary>
        public HtmlElement(Document owner, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, Combine(prefix, localName), localName, prefix, Namespaces.HtmlUri, flags | NodeFlags.HtmlMember)
        {
            RegisterAttributeObserver(AttributeNames.Style, UpdateStyle);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the element is hidden.
        /// </summary>
        public Boolean IsHidden
        {
            get { return GetOwnAttribute(AttributeNames.Hidden) != null; }
            set { SetOwnAttribute(AttributeNames.Hidden, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the assigned context menu.
        /// </summary>
        public IHtmlMenuElement ContextMenu
        {
            get
            {
                if (_menu == null)
                {
                    var id = GetOwnAttribute(AttributeNames.ContextMenu);

                    if (!String.IsNullOrEmpty(id))
                        return Owner.GetElementById(id) as IHtmlMenuElement;
                }

                return _menu;
            }
            set { _menu = value; }
        }

        /// <summary>
        /// Gets the dropzone for this element.
        /// </summary>
        public ISettableTokenList DropZone
        {
            get
            { 
                if (_dropZone == null)
                {
                    _dropZone = new SettableTokenList(GetOwnAttribute(AttributeNames.DropZone));
                    _dropZone.Changed += (s, ev) => UpdateAttribute(AttributeNames.DropZone, _dropZone.Value);
                }

                return _dropZone;
            }
        }

        /// <summary>
        /// Gets or sets if the element is draggable.
        /// </summary>
        public Boolean IsDraggable
        {
            get { return GetOwnAttribute(AttributeNames.Draggable).ToBoolean(false); }
            set { SetOwnAttribute(AttributeNames.Draggable, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the access key assigned to the element.
        /// </summary>
        public String AccessKey
        {
            get { return GetOwnAttribute(AttributeNames.AccessKey) ?? String.Empty; }
            set { SetOwnAttribute(AttributeNames.AccessKey, value); }
        }

        /// <summary>
        /// Gets the element's assigned access key.
        /// </summary>
        public String AccessKeyLabel
        {
            get { return AccessKey; }
        }

        /// <summary>
        /// Gets or sets the value of the lang attribute.
        /// </summary>
        public String Language
        {
            get { return GetOwnAttribute(AttributeNames.Lang) ?? GetDefaultLanguage(); }
            set { SetOwnAttribute(AttributeNames.Lang, value); }
        }

        /// <summary>
        /// Gets or sets the value of the title attribute.
        /// </summary>
        public String Title
        {
            get { return GetOwnAttribute(AttributeNames.Title); }
            set { SetOwnAttribute(AttributeNames.Title, value); }
        }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        public String Direction
        {
            get { return GetOwnAttribute(AttributeNames.Dir); }
            set { SetOwnAttribute(AttributeNames.Dir, value); }
        }

        /// <summary>
        /// Gets or sets if spell-checking is activated.
        /// </summary>
        public Boolean IsSpellChecked
        {
            get { return GetOwnAttribute(AttributeNames.Spellcheck).ToBoolean(false); }
            set { SetOwnAttribute(AttributeNames.Spellcheck, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the position of the element in the tabbing order.
        /// </summary>
        public Int32 TabIndex
        {
            get { return GetOwnAttribute(AttributeNames.TabIndex).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.TabIndex, value.ToString()); }
        }

        /// <summary>
        /// Gets access to all the custom data attributes (data-*) set on the element. It is a map of DOMString,
        /// one entry for each custom data attribute.
        /// </summary>
        public IStringMap Dataset
        {
            get { return _dataset ?? (_dataset = new StringMap("data-", this)); }
        }

        /// <summary>
        /// Gets an object representing the declarations of an element's style attributes.
        /// </summary>
        public CssStyleDeclaration Style
        {
            get { return _style ?? (_style = CreateStyle()); }
        }

        ICssStyleDeclaration IElementCssInlineStyle.Style
        {
            get { return Style; }
        }

        /// <summary>
        /// Gets or sets whether or not the element is editable. This enumerated
        /// attribute can have the values true, false and inherited.
        /// </summary>
        public String ContentEditable
        {
            get { return GetOwnAttribute(AttributeNames.ContentEditable); }
            set { SetOwnAttribute(AttributeNames.ContentEditable, value); }
        }

        /// <summary>
        /// Gets if the element is currently contenteditable.
        /// </summary>
        public Boolean IsContentEditable
        {
            get
            {
                var value = ContentEditable.ToEnum(ContentEditableMode.Inherited);

                if (value == ContentEditableMode.True)
                    return true;
                
                var parent = ParentElement as IHtmlElement;

                if (value == ContentEditableMode.Inherited && parent != null)
                    return parent.IsContentEditable;

                return false;
            }
        }

        /// <summary>
        /// Gets or sets if the element should be translated.
        /// </summary>
        public Boolean IsTranslated
        {
            get { return GetOwnAttribute(AttributeNames.Translate).ToEnum(SimpleChoice.Yes) == SimpleChoice.Yes; }
            set { SetOwnAttribute(AttributeNames.Translate, value ? Keywords.Yes : Keywords.No); }
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

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = Factory.HtmlElements.Create(Owner, LocalName, Prefix);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Gets the assigned form if any (use only on selected elements).
        /// </summary>
        /// <returns>The parent form OR assigned form if any.</returns>
        protected IHtmlFormElement GetAssignedForm()
        {
            var parent = Parent as INode;

            while (parent is IHtmlFormElement == false)
            {
                if (parent == null)
                    break;

                parent = parent.ParentElement;
            }
            
            if (parent == null)
            {
                var formid = GetOwnAttribute(AttributeNames.Form);
                var owner = Owner;

                if (owner != null && parent == null && !String.IsNullOrEmpty(formid))
                    parent = owner.GetElementById(formid);
                else
                    return null;
            }

            return parent as IHtmlFormElement;
        }

        String GetDefaultLanguage()
        {
            var parent = ParentElement as IHtmlElement;
            return parent != null ? parent.Language : Owner.Options.GetLanguage();
        }

        CssStyleDeclaration CreateStyle()
        {
            if (Owner.Options.IsStyling())
            {
                var style = new CssStyleDeclaration(GetOwnAttribute(AttributeNames.Style));
                style.Changed += (s, ev) => UpdateAttribute(AttributeNames.Style, _style.CssText);
                return style;
            }

            return null;
        }

        void UpdateStyle(String value)
        {
            if (String.IsNullOrEmpty(value))
                Attributes.Remove(Attributes.Get(null, AttributeNames.Style));

            if (_style != null)
                _style.Update(value);
        }

        #endregion

        #region Helpers

        protected Boolean IsClickedCancelled()
        {
            return this.Fire<MouseEvent>(m => m.Init(EventNames.Click, true, true, Owner.DefaultView, 0, 0, 0, 0, 0, false, false, false, false, MouseButton.Primary, this));
        }

        static String Combine(String prefix, String localName)
        {
            return (prefix != null ? String.Concat(prefix, ":", localName) : localName).ToUpperInvariant();
        }

        #endregion
    }
}
