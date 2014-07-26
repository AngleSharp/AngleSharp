namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    sealed class HTMLStyleElement : HTMLElement, IHtmlStyleElement
    {
        #region Fields

        IStyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an HTML style element.
        /// </summary>
        internal HTMLStyleElement()
        {
            _name = Tags.Style;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the style is scoped.
        /// </summary>
        public Boolean IsScoped
        {
            get { return GetAttribute(AttributeNames.Scoped) != null; }
            set { SetAttribute(AttributeNames.Scoped, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the associated style sheet.
        /// </summary>
        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { if (_sheet != null) return _sheet.IsDisabled; else return false; }
            set { if (_sheet != null) _sheet.IsDisabled = value; }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Methods

        public override INode AppendChild(INode child)
        {
            var node = base.AppendChild(child);
            OnChildrenChanged();
            return node;
        }

        public override INode InsertBefore(INode newElement, INode referenceElement)
        {
            var node = base.InsertBefore(newElement, referenceElement);
            OnChildrenChanged();
            return node;
        }

        public override INode InsertChild(int index, INode child)
        {
            var node = base.InsertChild(index, child);
            OnChildrenChanged();
            return node;
        }

        public override INode RemoveChild(INode child)
        {
            var node = base.RemoveChild(child);
            OnChildrenChanged();
            return node;
        }

        public override INode ReplaceChild(INode newChild, INode oldChild)
        {
            var node = base.ReplaceChild(newChild, oldChild);
            OnChildrenChanged();
            return node;
        }

        /// <summary>
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public override String ToText()
        {
            return String.Empty;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Internal methods

        void OnChildrenChanged()
        {
            var owner = Owner;

            if (owner == null)
                return;

            _sheet = owner.Options.ParseStyling(source: TextContent, owner: this, type: Type);
        }

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Media, StringComparison.Ordinal))
            {
                if (_sheet != null)
                    _sheet.Media.MediaText = Media;
            }
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}
