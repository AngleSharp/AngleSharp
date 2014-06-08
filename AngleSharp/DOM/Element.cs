namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an element node.
    /// </summary>
    public class Element : Node, IElement
    {
        #region Fields

        String _prefix;
        TokenList _classList;
        StringMap _dataset;
        CSSStyleDeclaration _style;
        HTMLCollection _elements;
        AttrContainer _attributes;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new element node.
        /// </summary>
        internal Element()
        {
            _type = NodeType.Element;
            _elements = new HTMLCollection(this, deep: false);
            _attributes = new AttrContainer();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the namespace prefix of the specified node, or null if no prefix is specified.
        /// </summary>
        [DOM("prefix")]
        public override String Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        [DOM("childElementCount")]
        public Int32 ChildElementCount
        {
            get { return _elements.Length; }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        [DOM("children")]
        public HTMLCollection Children
        {
            get { return _elements; }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        [DOM("textContent")]
        public override String TextContent
        {
            get
            {
                var sb = Pool.NewStringBuilder();

                for (int i = 0; i < _children.Length; i++)
                    if (_children[i].NodeType != NodeType.Comment && _children[i].NodeType != NodeType.ProcessingInstruction)
                        sb.Append(_children[i].TextContent);

                return sb.ToPool();
            }
            set { base.TextContent = value; }
        }

        /// <summary>
        /// Gets or sets whether or not the element is editable. This enumerated
        /// attribute can have the values true, false and inherited.
        /// </summary>
        [DOM("contentEditable")]
        public ContentEditableMode ContentEditable
        {
            get { return ToEnum(GetAttribute("contenteditable"), ContentEditableMode.Inherited); }
            set { SetAttribute("contenteditable", value.ToString()); }
        }

        /// <summary>
        /// Gets if the element is currently contenteditable.
        /// </summary>
        [DOM("isContentEditable")]
        public Boolean IsContentEditable
        {
            get
            {
                var value = ContentEditable;

                if (value == ContentEditableMode.True)
                    return true;
                else if (value == ContentEditableMode.Inherited && ParentElement != null)
                    return ParentElement.IsContentEditable;

                return false;
            }
        }

        /// <summary>
        /// Gets the list of class names.
        /// </summary>
        [DOM("classList")]
        public ITokenList ClassList
        {
            get { return _classList ?? (_classList = new TokenList(this, AttributeNames.Class, ClassName)); }
        }

        /// <summary>
        /// Gets or sets the value of the class attribute.
        /// </summary>
        [DOM("className")]
        public String ClassName
        {
            get { return GetAttribute("class"); }
            set { SetAttribute("class", value); }
        }

        /// <summary>
        /// Gets an object representing the declarations of an element's style attributes.
        /// </summary>
        [DOM("style")]
        public CSSStyleDeclaration Style
        {
            get { return _style ?? (_style = new CSSStyleDeclaration(this)); }
        }

        /// <summary>
        /// Gets or sets the value of the id attribute.
        /// </summary>
        [DOM("id")]
        public String Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }

        /// <summary>
        /// Gets or sets the value of the lang attribute.
        /// </summary>
        [DOM("lang")]
        public String Lang
        {
            get { return GetAttribute("lang") ?? (ParentElement != null ? ParentElement.Lang : _owner.Options.Language); }
            set { SetAttribute("lang", value); }
        }

        /// <summary>
        /// Gets or sets the value of the title attribute.
        /// </summary>
        [DOM("title")]
        public String Title
        {
            get { return GetAttribute("title"); }
            set { SetAttribute("title", value); }
        }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        [DOM("dir")]
        public DirectionMode Dir
        {
            get { return ToEnum(GetAttribute("dir"), DirectionMode.Ltr); }
            set { SetAttribute("dir", value.ToString()); }
        }

        /// <summary>
        /// Gets the tagname of the element.
        /// </summary>
        [DOM("tagName")]
        public String TagName
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets if spell-checking is activated.
        /// </summary>
        [DOM("spellcheck")]
        public Boolean Spellcheck
        {
            get { return ToBoolean(GetAttribute("spellcheck"), false); }
            set { SetAttribute("spellcheck", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the position of the element in the tabbing order.
        /// </summary>
        [DOM("tabIndex")]
        public Int32 TabIndex
        {
            get { return ToInteger(GetAttribute("tabindex"), 0); }
            set { SetAttribute("tabindex", value.ToString()); }
        }

        /// <summary>
        /// Gets access to all the custom data attributes (data-*) set on the element. It is a map of DOMString,
        /// one entry for each custom data attribute.
        /// </summary>
        [DOM("dataset")]
        public IStringMap Dataset
        {
            get { return _dataset ?? (_dataset = new StringMap("data-", this)); }
        }

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list of nodes, 
        /// null if the current element is the first element in that list.
        /// </summary>
        [DOM("previousElementSibling")]
        public Element PreviousElementSibling
        {
            get
            {
                if (_parent == null)
                    return null;

                var found = false;

                for (int i = _parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (_parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && _parent.ChildNodes[i] is Element)
                        return (Element)_parent.ChildNodes[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the element immediately following in this node's parent's list of nodes,
        /// or null if the current element is the last element in that list.
        /// </summary>
        [DOM("nextElementSibling")]
        public Element NextElementSibling
        {
            get
            {
                if (_parent == null)
                    return null;

                var n = _parent.ChildNodes.Length;
                var found = false;

                for (int i = 0; i < n; i++)
                {
                    if (_parent.ChildNodes[i] == this)
                        found = true;
                    else if(found && _parent.ChildNodes[i] is Element)
                        return (Element)_parent.ChildNodes[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        [DOM("firstElementChild")]
        public Element FirstElementChild
        {
            get 
            {
                var n = _children.Length;

                for (int i = 0; i < n; i++)
                {
                    if (_children[i] is Element)
                        return (Element)_children[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        [DOM("lastElementChild")]
        public Element LastElementChild
        {
            get
            {
                for (int i = _children.Length - 1; i >= 0; i--)
                {
                    if (_children[i] is Element)
                        return (Element)_children[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the HTML syntax describing the element's descendants.
        /// </summary>
        [DOM("innerHTML")]
        public String InnerHTML
        {
            get { return _children.ToHtml(); }
            set
            {
                var n = _children.Length - 1;

                for (int i = n; i >= 0; i--)
                    RemoveChild(_children[i]);

                var nodes = DocumentBuilder.HtmlFragment(value, this);
                n = nodes.Length;

                for (int i = 0; i < n; i++)
                {
                    nodes[i].OwnerDocument = null;
                    nodes[i].ParentNode = null;
                    AppendChild(nodes[i]);
                }
            }
        }

        /// <summary>
        /// Gets or sets the HTML syntax describing the element including its descendants. 
        /// </summary>
        [DOM("outerHTML")]
        public String OuterHTML
        {
            get { return this.ToHtml(); }
            set
            {
                if (_parent != null)
                {
                    if (_owner != null && _owner.DocumentElement == this)
                        throw new DOMException(ErrorCode.NoModificationAllowed);

                    var pos = _parent.IndexOf(this);

                    var nodes = DocumentBuilder.HtmlFragment(value, this);
                    var n = nodes.Length;

                    for (int i = 0; i < n; i++)
                    {
                        nodes[i].OwnerDocument = null;
                        nodes[i].ParentNode = null;
                        _parent.InsertChild(pos++, nodes[i]);
                    }

                    _parent.RemoveChild(this);
                }
                else
                    throw new DOMException(ErrorCode.NotSupported);
            }
        }
        
        /// <summary>
        /// Gets the sequence of associated attributes.
        /// </summary>
        [DOM("attributes")]
        public AttrContainer Attributes
        {
            get { return _attributes; }
        }

        #endregion

        #region Design Properties

        /// <summary>
        /// Gets if the element is being hovered.
        /// </summary>
        [DOM("isHovered")]
        public Boolean IsHovered
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the element has currently focus.
        /// </summary>
        [DOM("isFocused")]
        public Boolean IsFocused
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the width of the left border of this element.
        /// </summary>
        [DOM("clientLeft")]
        public Int32 ClientLeft
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the height of the top border of this element.
        /// </summary>
        [DOM("clientTop")]
        public Int32 ClientTop
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the inner width of this element.
        /// </summary>
        [DOM("clientWidth")]
        public Int32 ClientWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the inner height of this element.
        /// </summary>
        [DOM("clientHeight")]
        public Int32 ClientHeight
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the element from which all offset calculations are currently computed.
        /// </summary>
        [DOM("offsetParent")]
        public Element OffsetParent
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the distance from this element's left border to its offsetParent's left border.
        /// </summary>
        [DOM("offsetLeft")]
        public Int32 OffsetLeft
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the distance from this element's top border to its offsetParent's top border.
        /// </summary>
        [DOM("offsetTop")]
        public Int32 OffsetTop
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the width of this element, relative to the layout.
        /// </summary>
        [DOM("offsetWidth")]
        public Int32 OffsetWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the height of this element, relative to the layout.
        /// </summary>
        [DOM("offsetHeight")]
        public Int32 OffsetHeight
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the left scroll offset of an element.
        /// </summary>
        [DOM("scrollLeft")]
        public Int32 ScrollLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the top scroll offset of an element.
        /// </summary>
        [DOM("scrollTop")]
        public Int32 ScrollTop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the scroll view width of an element.
        /// </summary>
        [DOM("scrollWidth")]
        public Int32 ScrollWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the scroll view height of an element.
        /// </summary>
        [DOM("scrollHeight")]
        public Int32 ScrollHeight
        {
            get;
            internal set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Normalizes namespace declaration attributes and prefixes as part of the normalize document
        /// method of the document node.
        /// </summary>
        /// <returns>The current element.</returns>
        [DOM("normalizeNamespaces")]
        public Element NormalizeNamespaces()
        {
            var declarations = new List<String>();

            for (int i = 0; i < _attributes.Count; i++)
            {
                var attr = _attributes[i];

                if (attr.Prefix == Namespaces.Declaration && IsValidNamespaceDeclaration(attr.LocalName, attr.Value))
                    declarations.Add(attr.Value);
            }

            if (_ns != null)
            {
                if ((_prefix != null || !IsDefaultNamespace(_ns)) && (_prefix == null || LookupNamespaceURI(_prefix) != _ns))
                    SetAttribute(Namespaces.XmlNS, Namespaces.DeclarationFor(_prefix), _ns);
            }
            else if (LocalName != null)
            {
                //TODO
            }

            for (int i = 0; i < _attributes.Count; i++)
            {
                //TODO
                //http://www.w3.org/TR/DOM-Level-3-Core/namespaces-algorithms.html#isDefaultNamespaceAlgo
            }

            for (int i = 0; i < _children.Length; i++)
            {
                var child = _children[i] as Element;

                if (child != null)
                    child.NormalizeNamespaces();
            }

            return this;
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        [DOM("querySelector")]
        public Element QuerySelector(String selectors)
        {
            return _children.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A collection of HTML elements.</returns>
        [DOM("querySelectorAll")]
        public HTMLCollection QuerySelectorAll(String selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        [DOM("getElementsByClassName")]
        public HTMLCollection GetElementsByClassName(String classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        [DOM("getElementsByTagName")]
        public HTMLCollection GetElementsByTagName(String tagName)
        {
            return _children.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceURI">The namespace URI of elements to look for.</param>
        /// <param name="tagName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        [DOM("getElementsByTagNameNS")]
        public HTMLCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return _children.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = new Element();
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        [DOM("lookupNamespaceURI")]
        public override String LookupNamespaceURI(String prefix)
        {
            if (!String.IsNullOrEmpty(_ns) && Prefix == prefix)
                return _ns;

            for (int i = 0; i < _attributes.Count; i++)
            {
                var attr = _attributes[i];

                if ((attr.Prefix == Namespaces.Declaration && attr.LocalName == prefix) || (attr.LocalName == Namespaces.Declaration && prefix == null))
                {
                    if (!String.IsNullOrEmpty(attr.Value))
                        return attr.Value;

                    return null;
                }
            }

            if (_parent != null)
                _parent.LookupNamespaceURI(prefix);

            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default
        /// namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element
        /// will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        [DOM("isDefaultNamespace")]
        public override Boolean IsDefaultNamespace(String namespaceURI)
        { 
            if (String.IsNullOrEmpty(Prefix))
                return _ns == namespaceURI;

            var ns = GetAttribute(Namespaces.Declaration);

             if (!String.IsNullOrEmpty(ns))
                 return ns == namespaceURI;

             if (_parent != null)
                  return _parent.IsDefaultNamespace(namespaceURI);

            return false;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="name">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DOM("hasAttribute")]
        public Boolean HasAttribute(String name)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DOM("hasAttributeNS")]
        public Boolean HasAttribute(String namespaceUri, String localName)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase) && _attributes[i].NamespaceUri == namespaceUri)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        [DOM("getAttribute")]
        public String GetAttribute(String name)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return _attributes[i].Value;
            }

            return null;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        [DOM("getAttributeNS")]
        public String GetAttribute(String namespaceUri, String localName)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].LocalName == localName && _attributes[i].NamespaceUri == namespaceUri)
                    return _attributes[i].Value;
            }

            return null;
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        /// <returns>The current element.</returns>
        [DOM("setAttribute")]
        public void SetAttribute(String name, String value)
        {
            if (value == null)
            {
                RemoveAttribute(name);
                return;
            }

            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    _attributes[i].Value = value;
                    return;
                }
            }

            _attributes.Add(new Attr(name, value) { Parent = this });
            OnAttributeChanged(name);
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        [DOM("setAttributeNS")]
        public void SetAttribute(String namespaceUri, String name, String value)
        {
            if (value == null)
            {
                RemoveAttribute(namespaceUri, name);
                return;
            }

            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    _attributes[i].Value = value;
                    return;
                }
            }

            _attributes.Add(new Attr(name, value, namespaceUri) { Parent = this });
            OnAttributeChanged(name);
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="name">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        [DOM("removeAttribute")]
        public void RemoveAttribute(String name)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    _attributes.RemoveAt(i);
                    OnAttributeChanged(name);
                    break;
                }
            }
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        [DOM("removeAttributeNS")]
        public void RemoveAttribute(String namespaceUri, String localName)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase) && _attributes[i].NamespaceUri == namespaceUri)
                {
                    var name = _attributes[i].Name;
                    _attributes.RemoveAt(i);
                    OnAttributeChanged(name);
                    break;
                }
            }
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        [DOM("lookupPrefix")]
        public override String LookupPrefix(String namespaceURI)
        {
            return LookupNamespacePrefix(namespaceURI, this);
        }

        /// <summary>
        /// Prepends nodes to the current node.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        /// <returns>The current element.</returns>
        [DOM("prepend")]
        public Element Prepend(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                InsertChild(0, node);
            }

            return this;
        }

        /// <summary>
        /// Appends nodes to current node.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        /// <returns>The current element.</returns>
        [DOM("append")]
        public Element Append(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                AppendChild(node);
            }

            return this;
        }

        /// <summary>
        /// Inserts nodes before the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current element.</returns>
        [DOM("before")]
        public Element Before(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.InsertBefore(node, this);
            }

            return this;
        }

        /// <summary>
        /// Inserts nodes after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        [DOM("after")]
        public Element After(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.InsertBefore(node, NextSibling);
            }

            return this;
        }

        /// <summary>
        /// Replaces the current node with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        /// <returns>The current element.</returns>
        [DOM("replace")]
        public Element Replace(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.ReplaceChild(node, this);
            }

            return this;
        }

        /// <summary>
        /// Removes the current element from the parent.
        /// </summary>
        /// <returns>The current element.</returns>
        [DOM("remove")]
        public Element Remove()
        {
            if (_parent != null)
                _parent.RemoveChild(this);

            return this;
        }

        public override Boolean IsEqualNode(Node otherNode)
        {
            var otherElement = otherNode as Element;

            if (otherElement != null)
            {
                if (_attributes.Count != otherElement._attributes.Count)
                    return false;

                for (int i = 0; i < _attributes.Count; i++)
                {
                    if (!otherElement._attributes.Any(m => m.Name == _attributes[i].Name && m.Value == _attributes[i].Value))
                        return false;
                }

                return base.IsEqualNode(otherNode);
            }

            return false;
        }

        /// <summary>
        /// Inserts new HTML elements specified by the given HTML string at
        /// a position relative to the current element specified by the position.
        /// </summary>
        /// <param name="position">The relation to the current element.</param>
        /// <param name="html">The HTML code to generate elements for.</param>
        [DOM("insertAdjacentHTML")]
        public void insertAdjacentHTML(AdjacentPosition position, String html)
        {
            var nodeParent = position == AdjacentPosition.BeforeBegin || position == AdjacentPosition.AfterEnd ? this : _parent;
            var nodes = new DocumentFragment(DocumentBuilder.HtmlFragment(html, nodeParent));

            switch (position)
            {
                case AdjacentPosition.BeforeBegin:
                    ParentNode.InsertBefore(nodes, this);
                    break;

                case AdjacentPosition.AfterEnd:
                    ParentNode.InsertChild(ParentNode.IndexOf(this) + 1, nodes);
                    break;

                case AdjacentPosition.AfterBegin:
                    InsertChild(0, nodes);
                    break;

                case AdjacentPosition.BeforeEnd:
                    AppendChild(nodes);
                    break;
            }
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            var sb = Pool.NewStringBuilder();

            sb.Append(Specification.LessThan).Append(_name);

            foreach (var attribute in _attributes)
                sb.Append(Specification.Space).Append(attribute.ToString());

            sb.Append(Specification.GreaterThan);

            foreach (var child in _children)
                sb.Append(child.ToHtml());

            sb.Append(Specification.LessThan).Append(Specification.Solidus).Append(_name);
            sb.Append(Specification.GreaterThan);

            return sb.ToPool();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Copies the attributes from the source element to the target element.
        /// Each attribute will be recreated on the target.
        /// </summary>
        /// <param name="source">The source of the attributes.</param>
        /// <param name="target">The target where to create the attributes.</param>
        protected static void CopyAttributes(Element source, Element target)
        {
            for (int i = 0; i < source._attributes.Count; i++)
                target.SetAttribute(source._attributes[i].Name, source._attributes[i].Value);
        }

        /// <summary>
        /// Firing a simple event named e means that a trusted event with the name e,
        /// which does not bubble (except where otherwise stated) and is not cancelable
        /// (except where otherwise stated), and which uses the Event interface, must
        /// be created and dispatched at the given target.
        /// </summary>
        /// <param name="eventName">The name of the event to be fired.</param>
        protected void FireSimpleEvent(String eventName)
        {
            //TODO
            //http://www.w3.org/html/wg/drafts/html/master/webappapis.html#fire-a-simple-event
        }

        /// <summary>
        /// Gets the hyperreference of the given URL -
        /// transforming the given (relative) URL to an absolute URL
        /// if required.
        /// </summary>
        /// <param name="url">The given URL.</param>
        /// <returns>The absolute URL.</returns>
        protected String HyperRef(String url)
        {
            if (url == null || Location.IsAbsolute(url))
                return url;

            return Location.MakeAbsolute(BaseURI, url);
        }

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Class, StringComparison.Ordinal))
            {
                if (_classList != null)
                    _classList.Update(ClassName);
            }
            else if (name.Equals(AttributeNames.Style, StringComparison.Ordinal))
                Style.Update(GetAttribute(AttributeNames.Style));
            else
                base.OnAttributeChanged(name);
        }

        /// <summary>
        /// Converts the given value to an integer (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted integer.</returns>
        static protected Int32 ToInteger(String value, Int32 defaultValue = 0)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            Int32 converted;

            if (Int32.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to an unsigned integer (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted unsigned integer.</returns>
        static protected UInt32 ToInteger(String value, UInt32 defaultValue = 0)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            UInt32 converted;

            if (UInt32.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to an enumeration value (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted enum value.</returns>
        static protected T ToEnum<T>(String value, T defaultValue) where T : struct
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            T converted = default(T);

            if (Enum.TryParse(value, true, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to a boolean (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted boolean.</returns>
        static protected Boolean ToBoolean(String value, Boolean defaultValue = false)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            Boolean converted;

            if (Boolean.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        #endregion
    }
}
