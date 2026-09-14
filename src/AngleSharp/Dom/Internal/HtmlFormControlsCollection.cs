namespace AngleSharp.Dom
{
    using AngleSharp.Html;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Buffers;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A specialized collection containing elements of type HTMLFormControlElement.
    /// </summary>
    /// <remarks>
    /// The collection is live: it keeps no materialized result and no deferred query, only the
    /// root to walk and the form to match against, so every read runs <see cref="Enumerator"/>
    /// over the tree as it is at that moment. All four members - the length, the indexed read,
    /// the named read and enumeration - drive that one walk and that one predicate.
    /// </remarks>
    sealed class HtmlFormControlsCollection : IHtmlFormControlsCollection
    {
        #region Fields

        private readonly IElement _form;
        private readonly Element _root;

        #endregion

        #region ctor

        public HtmlFormControlsCollection(IElement form, IElement? root = null)
        {
            _form = form;
            // The walk uses the internal node links, and both call sites - a form element and a
            // fieldset element - hand in a node of this DOM.
            _root = (Element)(root ?? form.Owner!.DocumentElement)!;
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get
            {
                var count = 0;

                foreach (var _ in this)
                {
                    count++;
                }

                return count;
            }
        }

        #endregion

        #region HtmlFormControlElement Implementation

        public HtmlFormControlElement this[Int32 index]
        {
            get
            {
                if (index >= 0)
                {
                    var current = 0;

                    foreach (var element in this)
                    {
                        if (current++ == index)
                        {
                            return element;
                        }
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public HtmlFormControlElement? this[String id]
        {
            get
            {
                // The rule of CollectionExtensions.GetElementById, run on the walk instead of on
                // a sequence: an id match always wins, even over a name match seen earlier, so
                // the first name match is only returned once the walk ends without an id match.
                var nameMatch = default(HtmlFormControlElement);

                foreach (var element in this)
                {
                    if (element.Id.Is(id))
                    {
                        return element;
                    }

                    if (nameMatch is null && element.GetAttribute(null, AttributeNames.Name).Is(id))
                    {
                        nameMatch = element;
                    }
                }

                return nameMatch;
            }
        }

        public Enumerator GetEnumerator() => new(_root, _form);

        #endregion

        #region IHtmlCollection Implementation

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if NET8_0_OR_GREATER
        IHtmlElement IReadOnlyList<IHtmlElement>.this[Int32 index] => this[index];
#else
        IHtmlElement IHtmlCollection<IHtmlElement>.this[Int32 index] => this[index];
#endif

        IHtmlElement? IHtmlCollection<IHtmlElement>.this[String id] => this[id];

        IEnumerator<IHtmlElement> IEnumerable<IHtmlElement>.GetEnumerator() => GetEnumerator();

        #endregion

        #region Enumerator

        /// <summary>
        /// Depth-first pre-order walk over the descendants of the collection's root, yielding
        /// the controls the collection's form owns.
        /// </summary>
        /// <remarks>
        /// Same technique as <see cref="ElementTreeEnumerator"/> - parent links plus a stack of
        /// child indices, so the stack stores value types and is bounded by the depth of the
        /// tree rather than its width - but a separate walker for three reasons: the root itself
        /// is never a member of its own collection (a fieldset is itself a form control), the
        /// element it yields is typed and filtered, and the index stack is rented rather than
        /// allocated. The renting is what keeps a read of this live collection free of
        /// allocations; it is affordable here because every caller of the walk is either in this
        /// file or a <c>foreach</c> over one of the interface enumerators above, and both
        /// dispose it.
        /// </remarks>
        public struct Enumerator : IEnumerator<HtmlFormControlElement>
        {
            private const Int32 InitialDepth = 16;

            private readonly IElement _form;
            private Element _current;
            private HtmlFormControlElement? _match;
            private Int32[]? _indices;
            private Int32 _depth;
            private Boolean _finished;

            public Enumerator(Element root, IElement form)
            {
                _form = form;
                _current = root;
                _match = null;
                _indices = null;
                _depth = 0;
                _finished = false;
            }

            public readonly HtmlFormControlElement Current => _match!;

            readonly Object IEnumerator.Current => _match!;

            public Boolean MoveNext()
            {
                while (MoveNextElement())
                {
                    if (_current is HtmlFormControlElement control && IsOwnedBy(control, _form))
                    {
                        _match = control;
                        return true;
                    }
                }

                _match = null;
                return false;
            }

            public void Dispose()
            {
                var indices = _indices;
                _indices = null;
                _finished = true;

                if (indices is not null)
                {
                    ArrayPool<Int32>.Shared.Return(indices);
                }
            }

            public readonly void Reset() => throw new NotSupportedException();

            private static Boolean IsOwnedBy(HtmlFormControlElement control, IElement form)
            {
                if (!Object.ReferenceEquals(control.Form, form))
                {
                    return false;
                }

                // An <input type=image> is a listed element, but it is a submit button rather
                // than a member of form.elements.
                return control is not IHtmlInputElement input || !input.Type.Is(InputTypeNames.Image);
            }

            private Boolean MoveNextElement()
            {
                if (_finished)
                {
                    return false;
                }

                var child = GetElementAtOrAfter(_current, 0, out var childIndex);

                if (child is not null)
                {
                    Push(childIndex);
                    _current = child;
                    return true;
                }

                while (_depth > 0)
                {
                    // Only elements are ever descended into, so the parent is always an element.
                    var parent = (Element)_current.Parent!;
                    var sibling = GetElementAtOrAfter(parent, _indices![_depth - 1] + 1, out var siblingIndex);

                    if (sibling is not null)
                    {
                        _indices[_depth - 1] = siblingIndex;
                        _current = sibling;
                        return true;
                    }

                    _depth--;
                    _current = parent;
                }

                _finished = true;
                return false;
            }

            private void Push(Int32 index)
            {
                var indices = _indices;

                if (indices is null)
                {
                    indices = _indices = ArrayPool<Int32>.Shared.Rent(InitialDepth);
                }
                else if (_depth == indices.Length)
                {
                    var larger = ArrayPool<Int32>.Shared.Rent(indices.Length * 2);
                    Array.Copy(indices, larger, _depth);
                    ArrayPool<Int32>.Shared.Return(indices);
                    indices = _indices = larger;
                }

                indices[_depth++] = index;
            }

            private static Element? GetElementAtOrAfter(Element element, Int32 start, out Int32 index)
            {
                var children = element.ChildNodes;
                var n = children.Length;

                for (var i = start; i < n; i++)
                {
                    var node = children[i];

                    if (node.NodeType == NodeType.Element)
                    {
                        index = i;
                        return (Element)node;
                    }
                }

                index = -1;
                return null;
            }
        }

        #endregion
    }
}
