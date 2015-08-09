﻿namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Parser.Css;
    using AngleSharp.Services.Styling;
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents an element node.
    /// </summary>
    [DebuggerStepThrough]
    class Element : Node, IElement
    {
        #region Fields

        readonly NamedNodeMap _attributes;
        readonly String _namespace;
        readonly String _prefix;
        readonly String _localName;

        HtmlElementCollection _elements;
        TokenList _classList;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new element node.
        /// </summary>
        public Element(Document owner, String localName, String prefix, String namespaceUri, NodeFlags flags = NodeFlags.None)
            : this(owner, prefix != null ? String.Concat(prefix, ":", localName) : localName, localName, prefix, namespaceUri, flags)
        {
        }

        /// <summary>
        /// Creates a new element node.
        /// </summary>
        public Element(Document owner, String name, String localName, String prefix, String namespaceUri, NodeFlags flags = NodeFlags.None)
            : base(owner, name, NodeType.Element, flags)
        {
            _localName = localName;
            _prefix = prefix;
            _namespace = namespaceUri;
            _attributes = new NamedNodeMap(this);
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the associated attribute container.
        /// </summary>
        internal NamedNodeMap Attributes
        {
            get { return _attributes; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace prefix of the specified node, if any.
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
        }

        /// <summary>
        /// Gets the local part of the qualified name of this node.
        /// </summary>
        public String LocalName
        {
            get { return _localName; }
        }

        /// <summary>
        /// Gets the namespace URI of this node, if any.
        /// </summary>
        public String NamespaceUri
        {
            get { return _namespace; }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        public override String TextContent
        {
            get
            {
                var sb = Pool.NewStringBuilder();

                foreach (var child in this.GetDescendants().OfType<IText>())
                    sb.Append(child.Data);

                return sb.ToPool();
            }
            set
            {
                var node = !String.IsNullOrEmpty(value) ? new TextNode(Owner, value) : null;
                ReplaceAll(node, false);
            }
        }

        /// <summary>
        /// Gets the list of class names.
        /// </summary>
        public ITokenList ClassList
        {
            get
            {
                if (_classList == null)
                {
                    _classList = new TokenList(GetOwnAttribute(AttributeNames.Class));
                    CreateBindings(_classList, AttributeNames.Class);
                }

                return _classList;
            }
        }

        /// <summary>
        /// Gets or sets the value of the class attribute.
        /// </summary>
        public String ClassName
        {
            get { return GetOwnAttribute(AttributeNames.Class); }
            set { SetOwnAttribute(AttributeNames.Class, value); }
        }

        /// <summary>
        /// Gets or sets the value of the id attribute.
        /// </summary>
        public String Id
        {
            get { return GetOwnAttribute(AttributeNames.Id); }
            set { SetOwnAttribute(AttributeNames.Id, value); }
        }

        /// <summary>
        /// Gets the uppercase representation of the tag name.
        /// </summary>
        public String TagName
        {
            get { return NodeName; }
        }

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list
        /// of nodes, null if the current element is the first element in that
        /// list.
        /// </summary>
        public IElement PreviousElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent != null)
                {
                    var found = false;

                    for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
                    {
                        if (parent.ChildNodes[i] == this)
                            found = true;
                        else if (found && parent.ChildNodes[i] is IElement)
                            return (IElement)parent.ChildNodes[i];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the element immediately following in this node's parent's list
        /// of nodes, or null if the current element is the last element in
        /// that list.
        /// </summary>
        public IElement NextElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent != null)
                {
                    var n = parent.ChildNodes.Length;
                    var found = false;

                    for (int i = 0; i < n; i++)
                    {
                        if (parent.ChildNodes[i] == this)
                            found = true;
                        else if (found && parent.ChildNodes[i] is IElement)
                            return (IElement)parent.ChildNodes[i];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public Int32 ChildElementCount
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;
                var count = 0;

                for (int i = 0; i < n; i++)
                {
                    if (children[i].NodeType == NodeType.Element)
                        count++;
                }

                return count;
            }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        public IHtmlCollection<IElement> Children
        {
            get { return _elements ?? (_elements = new HtmlElementCollection(this, deep: false)); }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        public IElement FirstElementChild
        {
            get 
            {
                var children = ChildNodes;
                var n = children.Length;

                for (int i = 0; i < n; i++)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                        return child;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        public IElement LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (int i = children.Length - 1; i >= 0; i--)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                        return child;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the HTML syntax describing the element's descendants.
        /// </summary>
        public String InnerHtml
        {
            get { return ChildNodes.ToHtml(HtmlMarkupFormatter.Instance); }
            set { ReplaceAll(new DocumentFragment(this, value), false); }
        }

        /// <summary>
        /// Gets or sets the HTML syntax describing the element including its
        /// descendants. 
        /// </summary>
        public String OuterHtml
        {
            get { return ToHtml(HtmlMarkupFormatter.Instance); }
            set
            {
                var parent = Parent;

                if (parent != null)
                {
                    if (Owner != null && Owner.DocumentElement == this)
                        throw new DomException(DomError.NoModificationAllowed);

                    parent.InsertChild(parent.IndexOf(this), new DocumentFragment(this, value));
                    parent.RemoveChild(this);
                }
                else
                    throw new DomException(DomError.NotSupported);
            }
        }
        
        /// <summary>
        /// Gets the sequence of associated attributes.
        /// </summary>
        INamedNodeMap IElement.Attributes
        {
            get { return _attributes; }
        }

        /// <summary>
        /// Gets if the element is currently focused.
        /// </summary>
        public Boolean IsFocused
        {
            get
            {
                var owner = Owner;

                if (owner == null)
                    return false;

                return owner.FocusElement == this;
            }
            protected set
            {
                var owner = Owner;

                if (owner == null)
                    return;

                if (value)
                {
                    owner.SetFocus(this);
                    this.Fire<FocusEvent>(m => m.Init(EventNames.Focus, false, false));
                }
                else
                {
                    owner.SetFocus(null);
                    this.Fire<FocusEvent>(m => m.Init(EventNames.Blur, false, false));
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the first element within the document (using depth-first
        /// pre-order traversal of the document's nodes) that matches the
        /// specified group of selectors.
        /// </summary>
        /// <param name="selectors">
        /// A string containing one or more CSS selectors separated by commas.
        /// </param>
        /// <returns>An element object.</returns>
        public IElement QuerySelector(String selectors)
        {
            return ChildNodes.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using
        /// depth-first pre-order traversal of the document's nodes) that match
        /// the specified group of selectors.
        /// </summary>
        /// <param name="selectors">
        /// A string containing one or more CSS selectors separated by commas.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection<IElement> QuerySelectorAll(String selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">
        /// A string representing the list of class names to match; class names
        /// are separated by whitespace.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection<IElement> GetElementsByClassName(String classNames)
        {
            return ChildNodes.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The
        /// complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">
        /// A string representing the name of the elements. The special string
        /// "*" represents all elements.
        /// </param>
        /// <returns>
        /// A NodeList of found elements in the order they appear in the tree.
        /// </returns>
        public IHtmlCollection<IElement> GetElementsByTagName(String tagName)
        {
            return ChildNodes.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the
        /// given namespace. The complete document is searched, including the
        /// root node.
        /// </summary>
        /// <param name="namespaceURI">
        /// The namespace URI of elements to look for.
        /// </param>
        /// <param name="tagName">
        /// Either the local name of elements to look for or the special value
        /// "*", which matches all elements.
        /// </param>
        /// <returns>
        /// A NodeList of found elements in the order they appear in the tree.
        /// </returns>
        public IHtmlCollection<IElement> GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return ChildNodes.GetElementsByTagName(namespaceURI, tagName);
        }

        /// <summary>
        /// Checks if the element is matched by the given selector.
        /// </summary>
        /// <param name="selectors">Represents the selector to test.</param>
        /// <returns>
        /// True if the element would be selected by the specified selector,
        /// otherwise false.
        /// </returns>
        public Boolean Matches(String selectors)
        {
            return CssParser.Default.ParseSelector(selectors).Match(this);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new Element(Owner, LocalName, _prefix, _namespace, Flags);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        /// <summary>
        /// Creates a pseudo element for the current element.
        /// </summary>
        /// <param name="pseudoElement">
        /// The element to create (e.g. ::after).
        /// </param>
        /// <returns>The created element or null, if not possible.</returns>
        public IPseudoElement Pseudo(String pseudoElement)
        {
            return PseudoElement.Create(this, pseudoElement);
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element
        /// has the specified attribute or not.
        /// </summary>
        /// <param name="name">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        public Boolean HasAttribute(String name)
        {
            if (String.Equals(_namespace, Namespaces.HtmlUri, StringComparison.Ordinal))
                name = name.ToLowerInvariant();

            return _attributes.GetNamedItem(name) != null;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element
        /// has the specified attribute or not.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        public Boolean HasAttribute(String namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
                namespaceUri = null;

            return _attributes.GetNamedItem(namespaceUri, localName) != null;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute whose value you want to get.
        /// </param>
        /// <returns>
        /// If the named attribute does not exist, the value returned will be
        /// null, otherwise the attribute's value.
        /// </returns>
        public String GetAttribute(String name)
        {
            if (String.Equals(_namespace, Namespaces.HtmlUri, StringComparison.Ordinal))
                name = name.ToLower();

            var attr = _attributes.GetNamedItem(name);
            return attr != null ? attr.Value : null;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">
        /// The name of the attribute whose value you want to get.
        /// </param>
        /// <returns>
        /// If the named attribute does not exist, the value returned will be
        /// null, otherwise the attribute's value.
        /// </returns>
        public String GetAttribute(String namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
                namespaceUri = null;

            var attr = _attributes.GetNamedItem(namespaceUri, localName);
            return attr != null ? attr.Value : null;
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute
        /// on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        public void SetAttribute(String name, String value)
        {
            if (value != null)
            {
                if (!name.IsXmlName())
                    throw new DomException(DomError.InvalidCharacter);

                if (String.Equals(_namespace, Namespaces.HtmlUri, StringComparison.Ordinal))
                    name = name.ToLowerInvariant();

                SetOwnAttribute(name, value);
            }
            else
                RemoveAttribute(name);
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute
        /// on the specified element.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        public void SetAttribute(String namespaceUri, String name, String value)
        {            
            if (value != null)
            {
                var prefix = default(String);
                var localName = default(String);
                GetPrefixAndLocalName(name, ref namespaceUri, out prefix, out localName);
                _attributes.SetNamedItem(new Attr(prefix, localName, value, namespaceUri));
            }
            else
                RemoveAttribute(namespaceUri, name);
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="name">
        /// Is a string that names the attribute to be removed.
        /// </param>
        /// <returns>The current element.</returns>
        public void RemoveAttribute(String name)
        {
            if (String.Equals(_namespace, Namespaces.HtmlUri, StringComparison.Ordinal))
                name = name.ToLower();

            _attributes.RemoveNamedItemOrDefault(name);
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">
        /// Is a string that names the attribute to be removed.
        /// </param>
        /// <returns>The current element.</returns>
        public void RemoveAttribute(String namespaceUri, String localName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
                namespaceUri = null;

            _attributes.RemoveNamedItemOrDefault(namespaceUri, localName);
        }

        /// <summary>
        /// Prepends nodes to the current node.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        /// <summary>
        /// Appends nodes to current node.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        public override Boolean Equals(INode otherNode)
        {
            var otherElement = otherNode as IElement;

            if (otherElement != null)
            {
                return String.Equals(NamespaceUri, otherElement.NamespaceUri, StringComparison.Ordinal) &&
                    _attributes.AreEqual(otherElement.Attributes) && 
                    base.Equals(otherNode);
            }

            return false;
        }

        /// <summary>
        /// Inserts nodes before the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current element.</returns>
        public void Before(params INode[] nodes)
        {
            this.InsertBefore(nodes);
        }

        /// <summary>
        /// Inserts nodes after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        public void After(params INode[] nodes)
        {
            this.InsertAfter(nodes);
        }

        /// <summary>
        /// Replaces the current node with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        public void Replace(params INode[] nodes)
        {
            this.ReplaceWith(nodes);
        }

        /// <summary>
        /// Removes the current element from the parent.
        /// </summary>
        public void Remove()
        {
            this.RemoveFromParent();
        }

        /// <summary>
        /// Inserts new HTML elements specified by the given HTML string at a
        /// position relative to the current element specified by the position.
        /// </summary>
        /// <param name="position">The relation to the current element.</param>
        /// <param name="html">The HTML code to generate elements for.</param>
        public void Insert(AdjacentPosition position, String html)
        {
            var useThis = position == AdjacentPosition.BeforeBegin || position == AdjacentPosition.AfterEnd;
            var nodeParent = useThis ? this : Parent as Element;
            var nodes = new DocumentFragment(nodeParent, html);

            switch (position)
            {
                case AdjacentPosition.BeforeBegin:
                    Parent.InsertBefore(nodes, this);
                    break;

                case AdjacentPosition.AfterEnd:
                    Parent.InsertChild(Parent.IndexOf(this) + 1, nodes);
                    break;

                case AdjacentPosition.AfterBegin:
                    InsertChild(0, nodes);
                    break;

                case AdjacentPosition.BeforeEnd:
                    AppendChild(nodes);
                    break;
            }
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            var selfClosing = Flags.HasFlag(NodeFlags.SelfClosing);
            var open = formatter.OpenTag(this, selfClosing);
            var children = String.Empty;

            if (!selfClosing)
            {
                var sb = Pool.NewStringBuilder();

                if (Flags.HasFlag(NodeFlags.LineTolerance) && FirstChild is IText)
                {
                    var text = (IText)FirstChild;

                    if (text.Data.Length > 0 && text.Data[0] == Symbols.LineFeed)
                        sb.Append(Symbols.LineFeed);
                }

                foreach (var child in ChildNodes)
                    sb.Append(child.ToHtml(formatter));

                children = sb.ToPool();
            }

            var close = formatter.CloseTag(this, selfClosing);
            return String.Concat(open, children, close);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Faster way of getting the (known) attribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute's value, if any.</returns>
        protected String GetOwnAttribute(String name)
        {
            var attr = _attributes.GetNamedItem(null, name);
            return attr != null ? attr.Value : null;
        }

        /// <summary>
        /// Faster way of checkinf for a (known) attribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>True if the attribute exists, otherwise false.</returns>
        protected Boolean HasOwnAttribute(String name)
        {
            var attr = _attributes.GetNamedItem(null, name);
            return attr != null;
        }

        /// <summary>
        /// Faster way of setting the (known) attribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The attribute's value.</param>
        protected void SetOwnAttribute(String name, String value)
        {
            _attributes.SetNamedItemWithNamespaceUri(new Attr(name, value));
        }

        /// <summary>
        /// Creates the style for the inline style declaration.
        /// </summary>
        /// <returns>The declaration representing the declarations.</returns>
        protected ICssStyleDeclaration CreateStyle()
        {
            var config = Owner.Options;
            var engine = config.GetCssStyleEngine();

            if (engine != null)
            {
                var source = GetOwnAttribute(AttributeNames.Style);
                var options = new StyleOptions { Element = this, Configuration = config };
                var style = engine.ParseInline(source, options);
                var bindable = style as IBindable;

                if (bindable != null)
                    bindable.Changed += value => UpdateAttribute(AttributeNames.Style, value);

                return style;
            }

            return null;
        }

        protected void CreateBindings(IBindable bindable, String attributeName)
        {
            bindable.Changed += value => UpdateAttribute(attributeName, value);
            RegisterAttributeObserver(attributeName, value => bindable.Update(value));
        }

        /// <summary>
        /// Updates an attribute's value without notifying the observers.
        /// </summary>
        /// <param name="name">The name of the attribute to update.</param>
        /// <param name="value">The value of the attribute to set.</param>
        protected void UpdateAttribute(String name, String value)
        {
            var handler = _attributes.RemoveHandler(name);
            SetOwnAttribute(name, value);
            _attributes.SetHandler(name, handler);
        }

        internal void AttributeChanged(String localName, String namespaceUri, String oldValue)
        {
            Owner.QueueMutation(MutationRecord.Attributes(
                target: this,
                attributeName: localName,
                attributeNamespace: namespaceUri,
                previousValue: oldValue));
        }

        /// <summary>
        /// Locates the namespace of the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix of the namespace to find.</param>
        /// <returns>
        /// The url of the namespace or null, if the prefix could not be found.
        /// </returns>
        protected sealed override String LocateNamespace(String prefix)
        {
            return ElementExtensions.LocateNamespace(this, prefix);
        }

        /// <summary>
        /// Locates the prefix of the given namespace.
        /// </summary>
        /// <param name="namespaceUri">The url of the namespace.</param>
        /// <returns>
        /// The prefix or null, if the namespace could not be found.
        /// </returns>
        protected sealed override String LocatePrefix(String namespaceUri)
        {
            return ElementExtensions.LocatePrefix(this, namespaceUri);
        }

        /// <summary>
        /// Copies the attributes from the source element to the target
        /// element. Each attribute will be recreated on the target.
        /// </summary>
        /// <param name="source">The source of the attributes.</param>
        /// <param name="target">
        /// The target where to create the attributes.
        /// </param>
        protected static void CopyAttributes(Element source, Element target)
        {
            foreach (var attribute in source._attributes)
            {
                var attr = new Attr(attribute.Prefix, attribute.LocalName, attribute.Value, attribute.NamespaceUri);
                target._attributes.FastAddItem(attr);
            }
        }

        /// <summary>
        /// Registers an observer for attribute events.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="callback">The callback to invoke.</param>
        protected void RegisterAttributeObserver(String name, Action<String> callback)
        {
            _attributes.AddHandler(name, callback);
        }

        #endregion

        #region Updateable Location

        /// <summary>
        /// Represents an updateable location (url) holder.
        /// </summary>
        protected sealed class BoundLocation : ILocation
        {
            #region Fields

            readonly Element _parent;
            readonly String _attributeName;
            Location _location;
            Url _baseUrl;
            String _value;

            #endregion

            #region ctor

            public BoundLocation(Element parent, String attributeName)
            {
                _parent = parent;
                _attributeName = attributeName;
            }

            #endregion

            #region Properties

            public Location Location
            {
                get
                {
                    var value = _parent.GetOwnAttribute(_attributeName) ?? String.Empty;
                    var baseUrl = _parent.BaseUrl;

                    if (_location == null || 
                        !baseUrl.Equals(_baseUrl) || 
                        !String.Equals(value, _value, StringComparison.Ordinal))
                    {
                        var url = new Url(baseUrl, value);
                        _baseUrl = baseUrl;
                        _value = value;
                        _location = new Location(url);
                    }

                    return _location;
                }
            }

            public String Href
            {
                get { return Location.Href; }
                set { Assign(value); }
            }

            public String Protocol
            {
                get { return Location.Protocol; }
                set { Location.Protocol = value; Reload(); }
            }

            public String Host
            {
                get { return Location.Host; }
                set { Location.Host = value; Reload(); }
            }

            public String HostName
            {
                get { return Location.HostName; }
                set { Location.HostName = value; Reload(); }
            }

            public String Port
            {
                get { return Location.Port; }
                set { Location.Port = value; Reload(); }
            }

            public String PathName
            {
                get { return Location.PathName; }
                set { Location.PathName = value; Reload(); }
            }

            public String Search
            {
                get { return Location.Search; }
                set { Location.Search = value; Reload(); }
            }

            public String Hash
            {
                get { return Location.Hash; }
                set { Location.Hash = value; Reload(); }
            }

            public String UserName
            {
                get { return Location.UserName; }
                set { Location.UserName = value; Reload(); }
            }

            public String Password
            {
                get { return Location.Password; }
                set { Location.Password = value; Reload(); }
            }

            public String Origin
            {
                get { return Location.Origin; }
            }

            #endregion

            #region Methods

            public void Assign(String url)
            {
                _parent.SetOwnAttribute(_attributeName, url);
                _location = Location;
            }

            public void Replace(String url)
            {
                Assign(url);
            }

            public void Reload()
            {
                Assign(Href);
            }

            #endregion
        }

        #endregion
    }
}
