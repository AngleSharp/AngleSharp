using System;
using System.Text;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents an element node.
    /// </summary>
    [DOM("Element")]
    public class Element : Node, IElement
    {
        #region Members

        DOMTokenList _classList;
        DOMStringMap _dataset;
        CSSStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new element node.
        /// </summary>
        internal Element()
        {
            _type = NodeType.Element;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        [DOM("childElementCount")]
        public Int32 ChildElementCount
        {
            get
            {
                var count = 0;

                for (int i = 0; i < _children.Length; i++)
                {
                    if (_children[i] is Element)
                        count++;
                }

                return count;
            }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        [DOM("children")]
        public HTMLCollection Children
        {
            get
            {
                var list = new HTMLCollection();

                for (int i = 0; i < _children.Length; i++)
                {
                    if (_children[i] is HTMLElement)
                        list.Add((HTMLElement)_children[i]);
                }

                return list;
            }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        [DOM("textContent")]
        public override String TextContent
        {
            get
            {
                var sb = new StringBuilder();

                for (int i = 0; i < _children.Length; i++)
                    if (_children[i].NodeType != NodeType.Comment && _children[i].NodeType != NodeType.ProcessingInstruction)
                        sb.Append(_children[i].TextContent);

                return sb.ToString();
            }
            set
            {
                base.TextContent = value;
            }
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
        public DOMTokenList ClassList
        {
            get { return _classList ?? (_classList = new DOMTokenList(this, "class")); }
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
            get { return GetAttribute("lang") ?? (ParentElement != null ? ParentElement.Lang : LocalSettings.Language); }
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
        public DOMStringMap Dataset
        {
            get { return _dataset ?? (_dataset = new DOMStringMap(this)); }
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

                //TODO Fragment Mode has security consideration ??? i.e. Scripting should be TURNED OFF
                var nodes = DocumentBuilder.HtmlFragment(value, this);
                n = nodes.Length;

                for (int i = 0; i < n; i++)
                    AppendChild(nodes[i]);
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
                    //TODO Fragment Mode has security consideration ??? i.e. Scripting should be TURNED OFF
                    var nodes = DocumentBuilder.HtmlFragment(value, this);
                    var n = nodes.Length;

                    for (int i = 0; i < n; i++)
                        _parent.InsertChild(pos++, nodes[i]);

                    _parent.RemoveChild(this);
                }
                else
                    throw new DOMException(ErrorCode.NotSupported);
            }
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
        /// Normalizes namespace declaration attributes and prefixes as part of the normlize document
        /// method of the document node.
        /// </summary>
        /// <returns>The current element.</returns>
        [DOM("normalizeNamespaces")]
        public Element NormalizeNamespaces()
        {
            var declarations = new List<string>();

            for (int i = 0; i < _attributes.Length; i++)
            {
                var attr = _attributes[i];

                if (attr.Prefix == Namespaces.Declaration)
                {
                    if (IsValidNamespaceDeclaration(attr.LocalName, attr.NodeValue))
                    {
                        declarations.Add(attr.NodeValue);
                    }
                    else
                    {
                        //TODO
                        //Report an error ...
                    }
                }
            }

            if (_ns != null)
            {
                //TODO
                //http://www.w3.org/TR/DOM-Level-3-Core/namespaces-algorithms.html#isDefaultNamespaceAlgo
            }
            else
            {
                //TODO
                //http://www.w3.org/TR/DOM-Level-3-Core/namespaces-algorithms.html#isDefaultNamespaceAlgo
            }

            for (int i = 0; i < _attributes.Length; i++)
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

            if (HasAttributes)
            {
                for (int i = 0; i < _attributes.Length; i++)
                {
                    var attr = _attributes[i];

                    if ((attr.Prefix == Namespaces.Declaration && attr.LocalName == prefix) || (attr.LocalName == Namespaces.Declaration && prefix == null))
                    {
                        if (!String.IsNullOrEmpty(attr.NodeValue))
                            return attr.NodeValue;

                        return null;
                    }
                }
            }

            if (_parent != null)
                _parent.LookupNamespaceURI(prefix);

            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        [DOM("isDefaultNamespace")]
        public override Boolean IsDefaultNamespace(String namespaceURI)
        { 
            if (string.IsNullOrEmpty(Prefix))
                return _ns == namespaceURI;

            var ns = GetAttribute(Namespaces.Declaration);

             if (!string.IsNullOrEmpty(ns))
                 return ns == namespaceURI;

             if (_parent != null)
                  return _parent.IsDefaultNamespace(namespaceURI);

            return false;
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        /// <returns>The current element.</returns>
        [DOM("setAttribute")]
        public Element SetAttribute(String name, String value)
        {
            var oldAttr = value == null ? _attributes.RemoveNamedItem(name) : _attributes.SetNamedItem(new Attr(name, value));

            if (oldAttr != null)
                oldAttr.ParentNode = null;

            return this;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="attrName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        [DOM("getAttribute")]
        public String GetAttribute(String attrName)
        {
            var attr = _attributes[attrName];

            if (attr == null)
                return null;

            return attr.NodeValue;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="attrName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DOM("hasAttribute")]
        public Boolean HasAttribute(String attrName)
        {
            return _attributes[attrName] != null;
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="attrName">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        [DOM("removeAttribute")]
        public Element RemoveAttribute(String attrName)
        {
            var node = _attributes.RemoveNamedItem(attrName);
            node.ParentNode = null;
            return this;
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="namespaceURI">A string specifying the namespace of the attribute.</param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        /// <returns>The current element.</returns>
        [DOM("setAttributeNS")]
        public Element SetAttributeNS(String namespaceURI, String name, String value)
        {
            var oldAttr = value == null ? _attributes.RemoveNamedItem(name) : _attributes.SetNamedItem(new Attr(name, value, namespaceURI));

            if (oldAttr != null)
                oldAttr.ParentNode = null;

            return this;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="namespaceURI">A string specifying the namespace of the attribute.</param>
        /// <param name="localAttrName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        [DOM("getAttributeNS")]
        public String GetAttributeNS(String namespaceURI, String localAttrName)
        {
            var attr = _attributes.GetNamedItemNS(namespaceURI, localAttrName);

            if (attr == null)
                return null;

            return attr.NodeValue;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="namespaceURI">A string specifying the namespace of the attribute.</param>
        /// <param name="attrName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DOM("hasAttributeNS")]
        public Boolean HasAttributeNS(String namespaceURI, String attrName)
        {
            return _attributes.GetNamedItemNS(namespaceURI, attrName) != null;
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="namespaceURI">A string specifying the namespace of the attribute.</param>
        /// <param name="localAttrName">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        [DOM("removeAttributeNS")]
        public Element RemoveAttributeNS(String namespaceURI, String localAttrName)
        {
            var node = _attributes.RemoveNamedItemNS(namespaceURI, localAttrName);
            node.ParentNode = null;
            return this;
        }

        /// <summary>
        /// Adds a new Attr node.
        /// </summary>
        /// <param name="attr">Is the Attr node to set on the element.</param>
        /// <returns>The replaced attribute node, if any, returned by this function.</returns>
        [DOM("setAttributeNode")]
        public Attr SetAttributeNode(Attr attr)
        {
            if (attr.ParentNode != null)
                throw new DOMException(ErrorCode.InUse);

            attr.ParentNode = this;
            var oldAttr = _attributes.SetNamedItem(attr);

            if (oldAttr != null)
                oldAttr.ParentNode = null;

            return oldAttr;
        }

        /// <summary>
        /// Returns the value of the named attribute.
        /// </summary>
        /// <param name="attrName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute.</returns>
        [DOM("getAttributeNode")]
        public Attr GetAttributeNode(String attrName)
        {
            return _attributes[attrName];
        }

        /// <summary>
        /// Removes an attribute.
        /// </summary>
        /// <param name="attr">The Attr node that needs to be removed..</param>
        /// <returns>The removed Attr node..</returns>
        [DOM("removeAttributeNode")]
        public Attr RemoveAttributeNode(Attr attr)
        {
            var node = _attributes.RemoveNamedItem(attr.NodeName);
            node.ParentNode = null;
            return node;
        }

        /// <summary>
        /// Adds a new namespaced Attr node.
        /// </summary>
        /// <param name="namespaceURI">A string specifying the namespace of the attribute.</param>
        /// <param name="attr">Is the Attr node to set on the element.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute.</returns>
        [DOM("setAttributeNodeNS")]
        public Attr SetAttributeNodeNS(String namespaceURI, Attr attr)
        {
            return SetAttributeNode(attr);
        }

        /// <summary>
        /// Returns the value of the named attribute.
        /// </summary>
        /// <param name="namespaceURI">A string specifying the namespace of the attribute.</param>
        /// <param name="attrName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute.</returns>
        [DOM("getAttributeNodeNS")]
        public Attr GetAttributeNodeNS(String namespaceURI, String attrName)
        {
            return _attributes.GetNamedItemNS(namespaceURI, attrName);
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

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            var sb = new StringBuilder();

            sb.Append('<').Append(_name);
            sb.Append(_attributes.ToHtml());
            sb.Append(">");

            foreach (var child in _children)
                sb.Append(child.ToHtml());

            sb.Append("</").Append(_name).Append('>');
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the element.
        /// </summary>
        /// <returns>A string containing some information about the element.</returns>
        public override String ToString()
        {
            return String.Format("<{0}{1}>", _name, _attributes.ToHtml());
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals("class", StringComparison.Ordinal))
                ClassList.Update(ClassName);
            else if (name.Equals("style", StringComparison.Ordinal))
                Style.Update(GetAttribute("style"));
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

            if (uint.TryParse(value, out converted))
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
