namespace AngleSharp.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom.Events;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using Common;
    using Html.Construction;
    using Html.Parser;
    using Html.Parser.Tokens.Struct;
    using System.Threading;

    /// <summary>
    /// Represents an element node.
    /// </summary>
    public abstract class Element : Node, IElement, IConstructableElement, IConstructableElementAttributesByRef
    {
        #region Fields

        private readonly NamedNodeMap _attributes;
        private readonly String _namespace;
        private readonly String? _prefix;
        private readonly String _localName;
        private HtmlCollection<IElement>? _elements;
        private TokenList? _classList;
        private IShadowRoot? _shadowRoot;

        #endregion

        #region ctor

        /// <inheritdoc />
        public Element(Document owner, String localName, String? prefix, String? namespaceUri, NodeFlags flags = NodeFlags.None)
            : this(owner, prefix != null ? String.Concat(prefix, ":", localName) : localName, localName, prefix, namespaceUri!, flags)
        {
        }

        /// <inheritdoc />
        public Element(Document owner, String name, String localName, String? prefix, String namespaceUri, NodeFlags flags = NodeFlags.None)
            : base(owner, name, NodeType.Element, flags)
        {
            _localName = localName;
            _prefix = prefix;
            _namespace = namespaceUri;
            _attributes = new NamedNodeMap(this);
        }

        #endregion

        #region Internal Properties

        internal IBrowsingContext Context => Owner?.Context!;

        internal NamedNodeMap Attributes => _attributes;

        #endregion

        #region Properties

        /// <inheritdoc />
        public IElement? AssignedSlot => ParentElement?.ShadowRoot?.GetAssignedSlot(Slot);

        /// <inheritdoc />
        public String? Slot
        {
            get => this.GetOwnAttribute(AttributeNames.Slot);
            set => this.SetOwnAttribute(AttributeNames.Slot, value);
        }

        /// <inheritdoc />
        public IShadowRoot? ShadowRoot => _shadowRoot;

        /// <inheritdoc />
        public String? Prefix => _prefix;

        /// <inheritdoc />
        public String LocalName => _localName;

        /// <inheritdoc />
        public String? NamespaceUri => _namespace ?? this.GetNamespaceUri();

        /// <inheritdoc />
        public String? GivenNamespaceUri => _namespace;

        /// <inheritdoc />
        public override String TextContent
        {
            get
            {
                var sb = StringBuilderPool.Obtain();
                AppendText(this, sb);
                return sb.ToPool();
            }
            set
            {
                var node = !String.IsNullOrEmpty(value) ? new TextNode(Owner, value) : null;
                ReplaceAll(node, false);
            }
        }

        /// <inheritdoc />
        public ITokenList ClassList
        {
            get
            {
                var classList = Volatile.Read(ref _classList);

                if (classList is null)
                {
                    var list = new TokenList(this.GetOwnAttribute(AttributeNames.Class));
                    list.Changed += value => UpdateAttribute(AttributeNames.Class, value);
                    var current = Interlocked.CompareExchange(ref _classList, list, null);
                    classList = current ?? list;
                }

                return classList;
            }
        }

        /// <inheritdoc />
        public String? ClassName
        {
            get => this.GetOwnAttribute(AttributeNames.Class);
            set => this.SetOwnAttribute(AttributeNames.Class, value);
        }

        /// <inheritdoc />
        public String? Id
        {
            get => this.GetOwnAttribute(AttributeNames.Id);
            set => this.SetOwnAttribute(AttributeNames.Id, value);
        }

        /// <inheritdoc />
        public String TagName => NodeName;

        /// <inheritdoc />
        public ISourceReference? SourceReference { get; set; }

        /// <inheritdoc />
        public IElement? PreviousElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent is not null)
                {
                    var children = parent.ChildNodes;
                    var found = false;

                    // Scanning backwards matters: the preceding element sibling is usually
                    // right next to this node, so the loop normally ends shortly after the
                    // node is located. A forward scan would always pay the full prefix.
                    for (var i = children.Length - 1; i >= 0; i--)
                    {
                        var node = children[i];

                        if (found)
                        {
                            if (node.NodeType == NodeType.Element)
                            {
                                return (Element)node;
                            }
                        }
                        else if (Object.ReferenceEquals(node, this))
                        {
                            found = true;
                        }
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public IElement? NextElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent is not null)
                {
                    var children = parent.ChildNodes;
                    var n = children.Length;
                    var found = false;

                    for (var i = 0; i < n; i++)
                    {
                        var node = children[i];

                        if (found)
                        {
                            if (node.NodeType == NodeType.Element)
                            {
                                return (Element)node;
                            }
                        }
                        else if (Object.ReferenceEquals(node, this))
                        {
                            found = true;
                        }
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public Int32 ChildElementCount
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;
                var count = 0;

                for (var i = 0; i < n; i++)
                {
                    if (children[i].NodeType == NodeType.Element)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        /// <inheritdoc />
        public IHtmlCollection<IElement> Children => _elements ??= new HtmlCollection<IElement>(this, deep: false);

        /// <inheritdoc />
        public IElement? FirstElementChild
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;

                for (var i = 0; i < n; i++)
                {
                    if (children[i] is IElement child)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public IElement? LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (var i = children.Length - 1; i >= 0; i--)
                {

                    if (children[i] is IElement child)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public String InnerHtml
        {
            get => ChildNodes.ToHtml();
            set => ReplaceAll(new DocumentFragment(this, value), false);
        }

        /// <inheritdoc />
        public String OuterHtml
        {
            get => this.ToHtml();
            set
            {
                var parentNode = Parent;

                if (parentNode != null)
                {
                    switch (parentNode.NodeType)
                    {
                        case NodeType.Document:
                            throw new DomException(DomError.NoModificationAllowed);
                        case NodeType.DocumentFragment:
                            parentNode = new Html.Dom.HtmlBodyElement(Owner);
                            break;
                    }
                }

                var parent = parentNode as Element ?? throw new DomException(DomError.NotSupported);
                parent.InsertChild(parent.IndexOf(this), new DocumentFragment(parent, value));
                parent.RemoveChild(this);
            }
        }

        INamedNodeMap IElement.Attributes => _attributes;

        /// <inheritdoc />
        public Boolean IsFocused
        {
            get => Object.ReferenceEquals(Owner?.FocusElement, this);
            protected set
            {
                var document = Owner;
                document?.QueueTask(() =>
                {
                    if (value)
                    {
                        document.SetFocus(this);
                        this.Fire<FocusEvent>(m => m.Init(EventNames.Focus, false, false));
                    }
                    else
                    {
                        document.SetFocus(null);
                        this.Fire<FocusEvent>(m => m.Init(EventNames.Blur, false, false));
                    }
                });
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Takes a given string source and parses it into a subtree
        /// using the current element as context.
        /// Follows the fragment parsing strategy for the given namespace.
        /// </summary>
        /// <param name="source">The source to parse into a subtree.</param>
        /// <returns>The documentElement of the new subtree.</returns>
        public abstract IElement ParseSubtree(String source);

        /// <inheritdoc />
        public IShadowRoot AttachShadow(ShadowRootMode mode = ShadowRootMode.Open)
        {
            if (TagNames.AllNoShadowRoot.Contains(_localName))
            {
                throw new DomException(DomError.NotSupported);
            }

            if (ShadowRoot != null)
            {
                throw new DomException(DomError.InvalidState);
            }

            _shadowRoot = new ShadowRoot(this, mode);
            return _shadowRoot;
        }

        /// <inheritdoc />
        public IElement? QuerySelector(String selectors) => ChildNodes.QuerySelector(selectors, this);

        /// <inheritdoc />
        public IHtmlCollection<IElement> QuerySelectorAll(String selectors) => ChildNodes.QuerySelectorAll(selectors, this);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByClassName(String classNames) => ChildNodes.GetElementsByClassName(classNames);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByTagName(String tagName) => ChildNodes.GetElementsByTagName(tagName);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByTagNameNS(String? namespaceURI, String tagName) => ChildNodes.GetElementsByTagName(namespaceURI, tagName);

        /// <inheritdoc />
        public Boolean Matches(String selectorText)
        {
            var parser = Context.GetService<ICssSelectorParser>()!;
            var sg = parser.ParseSelector(selectorText) ?? throw new DomException(DomError.Syntax);
            return sg.Match(this, this);
        }

        /// <inheritdoc />
        public IElement? Closest(String selectorText)
        {
            var parser = Context.GetService<ICssSelectorParser>()!;
            var sg = parser.ParseSelector(selectorText) ?? throw new DomException(DomError.Syntax);
            var node = (IElement)this;

            while (node != null)
            {
                if (sg.Match(node, node))
                {
                    return node;
                }
                else
                {
                    node = node.ParentElement;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public Boolean HasAttribute(String name)
        {
            if (_namespace.Is(NamespaceNames.HtmlUri))
            {
                name = name.HtmlLower();
            }

            return _attributes.GetNamedItem(name) != null;
        }

        /// <inheritdoc />
        public Boolean HasAttribute(StringOrMemory name)
        {
            if (_namespace.Is(NamespaceNames.HtmlUri))
            {
                name = name.HtmlLower();
            }

            return _attributes.GetNamedItem(name) != null;
        }

        /// <inheritdoc />
        public Boolean HasAttribute(String? namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            return _attributes.GetNamedItem(namespaceUri, localName) != null;
        }

        /// <inheritdoc />
        public String? GetAttribute(String name)
        {
            if (_namespace.Is(NamespaceNames.HtmlUri))
            {
                name = name.HtmlLower();
            }

            return _attributes.GetNamedItem(name)?.Value;
        }

        /// <inheritdoc />
        public String? GetAttribute(String? namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            return _attributes.GetNamedItem(namespaceUri, localName)?.Value;
        }

        /// <inheritdoc />
        public void SetAttribute(String name, String? value)
        {
            if (value != null)
            {
                if (!name.IsXmlName())
                {
                    throw new DomException(DomError.InvalidCharacter);
                }

                if (_namespace.Is(NamespaceNames.HtmlUri))
                {
                    name = name.HtmlLower();
                }

                this.SetOwnAttribute(name, value);
            }
            else
            {
                RemoveAttribute(name);
            }
        }

        /// <inheritdoc />
        public void SetAttribute(String? namespaceUri, String name, String? value)
        {
            if (value != null)
            {
                GetPrefixAndLocalName(name, ref namespaceUri, out var prefix, out var localName);
                _attributes.SetNamedItem(new Attr(prefix, localName, value, namespaceUri));
            }
            else
            {
                RemoveAttribute(namespaceUri, name);
            }
        }

        /// <summary>
        /// Adds an attribute.
        /// </summary>
        /// <param name="attr">The attribute to add.</param>
        public void AddAttribute(Attr attr)
        {
            attr.Container = _attributes;
            _attributes.FastAddItem(attr);
        }

        /// <inheritdoc />
        public Boolean RemoveAttribute(String name)
        {
            if (_namespace.Is(NamespaceNames.HtmlUri))
            {
                name = name.HtmlLower();
            }

            return _attributes.RemoveNamedItemOrDefault(name) != null;
        }

        /// <inheritdoc />
        public Boolean RemoveAttribute(String? namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            return _attributes.RemoveNamedItemOrDefault(namespaceUri, localName) != null;
        }

        /// <inheritdoc />
        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        /// <inheritdoc />
        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        /// <inheritdoc />
        public override Boolean Equals(INode? otherNode)
        {
            if (otherNode is IElement otherElement)
            {
                return NamespaceUri.Is(otherElement.NamespaceUri) &&
                    _attributes.SameAs(otherElement.Attributes) &&
                    base.Equals(otherNode);
            }

            return false;
        }

        /// <inheritdoc />
        public void Before(params INode[] nodes) => this.InsertBefore(nodes);

        /// <inheritdoc />
        public void After(params INode[] nodes) => this.InsertAfter(nodes);

        /// <inheritdoc />
        public void Replace(params INode[] nodes) => this.ReplaceWith(nodes);

        /// <inheritdoc />
        public void Remove() => this.RemoveFromParent();

        /// <inheritdoc />
        public void Insert(AdjacentPosition position, String html)
        {
            var useThis = position == AdjacentPosition.AfterBegin || position == AdjacentPosition.BeforeEnd;
            var context = useThis ? this : Parent as Element ?? throw new DomException("The element has no parent.");
            var nodes = new DocumentFragment(context, html);

            switch (position)
            {
                case AdjacentPosition.BeforeBegin:
                    Parent!.InsertBefore(nodes, this);
                    break;

                case AdjacentPosition.AfterEnd:
                    Parent!.InsertChild(Parent.IndexOf(this) + 1, nodes);
                    break;

                case AdjacentPosition.AfterBegin:
                    InsertChild(0, nodes);
                    break;

                case AdjacentPosition.BeforeEnd:
                    AppendChild(nodes);
                    break;
            }
        }

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new AnyElement(owner, LocalName, _prefix, _namespace, Flags);
            CloneElement(node, owner, deep);
            return node;
        }

        #endregion

        #region Internal Methods

        internal virtual void SetupElement()
        {
            var attrs = _attributes;

            if (attrs.Length > 0)
            {
                var observers = Owner.AttributeObservers;

                foreach (var attr in attrs)
                {
                    var name = attr.LocalName;
                    var value = attr.Value;

                    foreach (var observer in observers)
                    {
                        observer.NotifyChange(this, name, value);
                    }
                }
            }
        }

        internal void AttributeChanged(String localName, String? namespaceUri, String? oldValue, String? newValue)
        {
            if (namespaceUri is null)
            {
                var observers = Owner.AttributeObservers;

                foreach (var observer in observers)
                {
                    observer.NotifyChange(this, localName, newValue);
                }
            }

            Owner.QueueMutation(MutationRecord.Attributes(
                target: this,
                attributeName: localName,
                attributeNamespace: namespaceUri,
                previousValue: oldValue));
        }

        internal void UpdateClassList(String value) => _classList?.Update(value);

        #endregion

        #region Helpers

        /// <summary>
        /// Appends the data of every descendant text node, in tree order. Equivalent to
        /// iterating GetDescendants().OfType&lt;IText&gt;(), without the nested iterators.
        /// </summary>
        private static void AppendText(Node parent, System.Text.StringBuilder sb)
        {
            var children = parent.ChildNodes;
            var n = children.Length;

            for (var i = 0; i < n; i++)
            {
                var child = children[i];

                if (child is IText text)
                {
                    sb.Append(text.Data);
                }
                else if (child.HasChildNodes)
                {
                    AppendText(child, sb);
                }
            }
        }

        /// <inheritdoc />
        protected void UpdateAttribute(String name, String value) => this.SetOwnAttribute(name, value, suppressCallbacks: true);

        /// <inheritdoc />
        protected sealed override String? LocateNamespace(String prefix) => this.LocateNamespaceFor(prefix);

        /// <inheritdoc />
        protected sealed override String? LocatePrefix(String namespaceUri) => this.LocatePrefixFor(namespaceUri);

        /// <inheritdoc />
        protected void CloneElement(Element element, Document owner, Boolean deep)
        {
            CloneNode(element, owner, deep);

            foreach (var attribute in _attributes)
            {
                var attr = new Attr(attribute.Prefix, attribute.LocalName, attribute.Value, attribute.NamespaceUri);
                attr.Container = element._attributes;
                element._attributes.FastAddItem(attr);
            }

            element.SetupElement();
        }

        #endregion

        #region Construction

        StringOrMemory IConstructableElement.LocalName => _localName;

        IConstructableNamedNodeMap IConstructableElement.Attributes => _attributes;

        StringOrMemory IConstructableElement.NamespaceUri => NamespaceUri ?? "";

        void IConstructableElement.SetAttribute(String? ns, StringOrMemory name, StringOrMemory value)
        {
            SetAttribute(ns, name.ToString(), value.ToString());
        }

        void IConstructableElement.SetOwnAttribute(StringOrMemory name, StringOrMemory value)
        {
            this.SetOwnAttribute(name.ToString(), value.ToString());
        }

        StringOrMemory IConstructableElement.GetAttribute(StringOrMemory @namespace, StringOrMemory name)
        {
            var result = GetAttribute(@namespace.ToString(), name.ToString());
            return result ?? StringOrMemory.Empty;
        }

        void IConstructableElement.SetAttributes(StructAttributes tagAttributes)
        {
            SetAttributesCore(in tagAttributes);
        }

        void IConstructableElementAttributesByRef.SetAttributes(in StructAttributes tagAttributes)
        {
            SetAttributesCore(in tagAttributes);
        }

        private void SetAttributesCore(in StructAttributes tagAttributes)
        {
            var container = Attributes;

            for (var i = 0; i < tagAttributes.Count; i++)
            {
                var attribute = tagAttributes[i];
                var item = new Attr(attribute.Name.ToString(), attribute.Value.ToString());
                item.Container = container;
                container.FastAddItem(item);
            }
        }

        void IConstructableElement.SetupElement() => SetupElement();

        void IConstructableElement.AddComment(ref StructHtmlToken token) => this.AddComment(ref token);

        IConstructableNode IConstructableElement.ShallowCopy() => Clone(Owner, false);

        StringOrMemory IConstructableElement.Prefix => Prefix ?? StringOrMemory.Empty;

        #endregion
    }
}
