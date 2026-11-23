namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The collection of the child elements of a node (children), which always tells what the
    /// node has now. Its elements are only gathered again once the child list changed, which the
    /// version of that list tells, as script asks for the length and an item for every step of a
    /// loop, e.g. Array.prototype.slice.call(parent.children); changes elsewhere (text, attributes,
    /// other nodes) don't touch it. Each gathering gives a new list, so that what goes over the
    /// collection may change the children.
    /// </summary>
    sealed class ChildElementCollection : IHtmlCollection<IElement>
    {
        private readonly Node _parent;
        private IElement[]? _elements;
        private NodeList? _list;
        private Int32 _version;

        public ChildElementCollection(Node parent)
        {
            _parent = parent;
        }

        public IElement this[Int32 index] => Elements.GetItemByIndex(index);

        public IElement? this[String id] => Elements.GetElementById(id);

        public Int32 Length => Elements.Length;

        public IEnumerator<IElement> GetEnumerator() => ((IEnumerable<IElement>)Elements).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();

        private IElement[] Elements
        {
            get
            {
                var list = _parent.ChildNodes;
                var version = list.Version;

                if (_elements is null || !ReferenceEquals(list, _list) || version != _version)
                {
                    var elements = new List<IElement>();

                    for (var i = 0; i < list.Length; i++)
                    {
                        if (list[i] is IElement element)
                        {
                            elements.Add(element);
                        }
                    }

                    _elements = elements.ToArray();
                    _list = list;
                    _version = version;
                }

                return _elements;
            }
        }
    }
}
