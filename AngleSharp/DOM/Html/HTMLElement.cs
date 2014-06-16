namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    public class HTMLElement : Element, IHtmlElement
    {
        #region Fields

        StringMap _dataset;
        CSSStyleDeclaration _style;
        HTMLMenuElement _menu;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a standard HTML element.
        /// </summary>
        internal HTMLElement()
        {
            NamespaceUri = Namespaces.Html; 
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if this node is in the HTML namespace.
        /// </summary>
        internal protected override Boolean IsInHtml
        {
            get { return true; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the element is hidden.
        /// </summary>
        public Boolean IsHidden
        {
            get { return GetAttribute("hidden") != null; }
            set { SetAttribute("hidden", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the assigned context menu.
        /// </summary>
        public HTMLMenuElement ContextMenu
        {
            get
            {
                if (_menu != null)
                {
                    var id = GetAttribute("contextmenu");

                    if (!String.IsNullOrEmpty(id))
                        return _owner.GetElementById(id) as HTMLMenuElement;
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
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets if the element is draggable.
        /// </summary>
        public Boolean IsDraggable
        {
            get { return ToBoolean(GetAttribute("draggable"), false); }
            set { SetAttribute("draggable", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the access key assigned to the element.
        /// </summary>
        public String AccessKey
        {
            get { return GetAttribute("accesskey") ?? String.Empty; }
            set { SetAttribute("accesskey", value); }
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
        public String Lang
        {
            get { return GetAttribute("lang") ?? (ParentElement as IHtmlElement != null ? (ParentElement as IHtmlElement).Lang : _owner.Options.Language); }
            set { SetAttribute("lang", value); }
        }

        /// <summary>
        /// Gets or sets the value of the title attribute.
        /// </summary>
        public String Title
        {
            get { return GetAttribute("title"); }
            set { SetAttribute("title", value); }
        }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        public String Dir
        {
            get { return GetAttribute("dir"); }
            set { SetAttribute("dir", value); }
        }

        /// <summary>
        /// Gets or sets if spell-checking is activated.
        /// </summary>
        public Boolean Spellcheck
        {
            get { return ToBoolean(GetAttribute("spellcheck"), false); }
            set { SetAttribute("spellcheck", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the position of the element in the tabbing order.
        /// </summary>
        public Int32 TabIndex
        {
            get { return ToInteger(GetAttribute("tabindex"), 0); }
            set { SetAttribute("tabindex", value.ToString()); }
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

        /// <summary>
        /// Gets or sets whether or not the element is editable. This enumerated
        /// attribute can have the values true, false and inherited.
        /// </summary>
        public String ContentEditable
        {
            get { return GetAttribute("contenteditable"); }
            set { SetAttribute("contenteditable", value); }
        }

        /// <summary>
        /// Gets if the element is currently contenteditable.
        /// </summary>
        public Boolean IsContentEditable
        {
            get
            {
                var value = ToEnum<ContentEditableMode>(ContentEditable, ContentEditableMode.Inherited);

                if (value == ContentEditableMode.True)
                    return true;

                var parent = ParentElement as IHtmlElement;

                if (value == ContentEditableMode.Inherited && parent != null)
                    return parent.IsContentEditable;

                return false;
            }
        }

        public Boolean IsTranslated
        {
            get;
            set;
        }

        #endregion

        #region Methods

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
            var node = HTMLFactory.Create(_name, _owner);
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
        protected HTMLFormElement GetAssignedForm()
        {
            var par = _parent;

            while (!(par is HTMLFormElement))
            {
                if (par == null)
                    break;

                par = par.ParentElement;
            }

            if (par == null && _owner == null)
                return null;
            
            if (par == null)
            {
                var formid = GetAttribute("form");

                if (par == null && !String.IsNullOrEmpty(formid))
                    par = _owner.GetElementById(formid) as HTMLFormElement;
                else
                    return null;
            }

            return par as HTMLFormElement;
        }

        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Style, StringComparison.Ordinal))
                Style.Update(GetAttribute(AttributeNames.Style));
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}
