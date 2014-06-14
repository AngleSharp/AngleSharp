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
        public DirectionMode Dir
        {
            get { return ToEnum(GetAttribute("dir"), DirectionMode.Ltr); }
            set { SetAttribute("dir", value.ToString()); }
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
        public ContentEditableMode ContentEditable
        {
            get { return ToEnum(GetAttribute("contenteditable"), ContentEditableMode.Inherited); }
            set { SetAttribute("contenteditable", value.ToString()); }
        }

        /// <summary>
        /// Gets if the element is currently contenteditable.
        /// </summary>
        public Boolean IsContentEditable
        {
            get
            {
                var value = ContentEditable;

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
