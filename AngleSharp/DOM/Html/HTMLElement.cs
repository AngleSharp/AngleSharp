namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    class HTMLElement : Element, IHtmlElement
    {
        #region Fields

        StringMap _dataset;
        CSSStyleDeclaration _style;
        IHtmlMenuElement _menu;
        ISettableTokenList _dropZone;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a standard HTML element.
        /// </summary>
        internal HTMLElement(String name, NodeFlags flags = NodeFlags.None)
            : base(name, flags | NodeFlags.HtmlMember)
        {
            NamespaceUri = Namespaces.Html; 
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the element is hidden.
        /// </summary>
        public Boolean IsHidden
        {
            get { return GetAttribute(AttributeNames.Hidden) != null; }
            set { SetAttribute(AttributeNames.Hidden, value ? String.Empty : null); }
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
                    var id = GetAttribute(AttributeNames.ContextMenu);

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
            get { return _dropZone ?? (_dropZone = new SettableTokenList(this, AttributeNames.DropZone)); }
        }

        /// <summary>
        /// Gets or sets if the element is draggable.
        /// </summary>
        public Boolean IsDraggable
        {
            get { return GetAttribute(AttributeNames.Draggable).ToBoolean(false); }
            set { SetAttribute(AttributeNames.Draggable, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the access key assigned to the element.
        /// </summary>
        public String AccessKey
        {
            get { return GetAttribute(AttributeNames.AccessKey) ?? String.Empty; }
            set { SetAttribute(AttributeNames.AccessKey, value); }
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
            get { return GetAttribute(AttributeNames.Lang) ?? (ParentElement as IHtmlElement != null ? (ParentElement as IHtmlElement).Language : Owner.Options.Language); }
            set { SetAttribute(AttributeNames.Lang, value); }
        }

        /// <summary>
        /// Gets or sets the value of the title attribute.
        /// </summary>
        public String Title
        {
            get { return GetAttribute(AttributeNames.Title); }
            set { SetAttribute(AttributeNames.Title, value); }
        }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        public String Direction
        {
            get { return GetAttribute(AttributeNames.Dir); }
            set { SetAttribute(AttributeNames.Dir, value); }
        }

        /// <summary>
        /// Gets or sets if spell-checking is activated.
        /// </summary>
        public Boolean IsSpellChecked
        {
            get { return GetAttribute(AttributeNames.Spellcheck).ToBoolean(false); }
            set { SetAttribute(AttributeNames.Spellcheck, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the position of the element in the tabbing order.
        /// </summary>
        public Int32 TabIndex
        {
            get { return GetAttribute(AttributeNames.TabIndex).ToInteger(0); }
            set { SetAttribute(AttributeNames.TabIndex, value.ToString()); }
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
        public CSSStyleDeclaration Style
        {
            get { return _style ?? (_style = new CSSStyleDeclaration(this)); }
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
            get { return GetAttribute(AttributeNames.ContentEditable); }
            set { SetAttribute(AttributeNames.ContentEditable, value); }
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
            get { return GetAttribute(AttributeNames.Translate).ToEnum(SimpleChoice.Yes) == SimpleChoice.Yes; }
            set { SetAttribute(AttributeNames.Translate, value ? "yes" : "no"); }
        }

        #endregion

        #region Methods

        public void DoSpellCheck()
        {
            //TODO Perform spellcheck on the element.
        }

        public void DoClick()
        {
            //TODO Perform click on the element.
        }

        public void DoFocus()
        {
            //TODO Focus the element.
        }

        public void DoBlur()
        {
            //TODO Remove focus.
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = HtmlElementFactory.Create(NodeName, Owner);
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

            while (!(parent is IHtmlFormElement))
            {
                if (parent == null)
                    break;

                parent = parent.ParentElement;
            }

            if (parent == null && Owner == null)
                return null;
            
            if (parent == null)
            {
                var formid = GetAttribute(AttributeNames.Form);

                if (parent == null && !String.IsNullOrEmpty(formid))
                    parent = Owner.GetElementById(formid) as IHtmlFormElement;
                else
                    return null;
            }

            return parent as IHtmlFormElement;
        }

        /// <summary>
        /// Called if an attribute changed, has been added or removed.
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        protected override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Style, StringComparison.Ordinal))
                Style.Update(GetAttribute(AttributeNames.Style));
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}
