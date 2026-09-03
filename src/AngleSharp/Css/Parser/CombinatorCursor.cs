namespace AngleSharp.Css.Parser
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The traversal a combinator performs to get from a candidate element to
    /// the elements its left-hand side may match.
    /// </summary>
    enum CssCombinatorKind : Byte
    {
        /// <summary>
        /// No traversal; used by the concluding selector, which has no
        /// combinator to its right.
        /// </summary>
        None,

        /// <summary>
        /// The parent element (&gt;).
        /// </summary>
        Child,

        /// <summary>
        /// The host of the containing shadow root (&gt;&gt;&gt;).
        /// </summary>
        Deep,

        /// <summary>
        /// All ancestor elements (space, or alternatively &gt;&gt;).
        /// </summary>
        Descendant,

        /// <summary>
        /// The immediately preceding element sibling (+).
        /// </summary>
        AdjacentSibling,

        /// <summary>
        /// All preceding element siblings (~).
        /// </summary>
        Sibling,

        /// <summary>
        /// The element itself (|).
        /// </summary>
        Namespace,

        /// <summary>
        /// The cells sharing the element's table column (||).
        /// </summary>
        Column,
    }

    /// <summary>
    /// Walks the elements a combinator connects to a candidate element.
    /// </summary>
    /// <remarks>
    /// A hand-rolled state machine rather than an iterator method: selector
    /// matching creates one cursor per candidate element per combinator
    /// position, and a compiler-generated iterator would allocate on that path.
    /// </remarks>
    struct CombinatorCursor
    {
        #region Fields

        private readonly CssCombinatorKind _kind;
        private readonly IElement _source;
        private IElement? _current;
        private Boolean _started;

        // Only used by Sibling / Column, which cannot be walked through a
        // single element reference.
        private NodeList? _siblings;
        private List<IElement>? _cells;
        private Int32 _index;

        #endregion

        #region ctor

        public CombinatorCursor(CssCombinatorKind kind, IElement source)
        {
            _kind = kind;
            _source = source;
            _current = null;
            _started = false;
            _siblings = null;
            _cells = null;
            _index = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element the cursor moved to. Only valid after MoveNext
        /// returned true.
        /// </summary>
        public readonly IElement Current => _current!;

        #endregion

        #region Methods

        /// <summary>
        /// Moves to the next element reachable through the combinator.
        /// </summary>
        /// <returns>True if there was another element, otherwise false.</returns>
        public Boolean MoveNext()
        {
            switch (_kind)
            {
                case CssCombinatorKind.Descendant:
                    return MoveAncestor();
                case CssCombinatorKind.Sibling:
                    return MoveSibling();
                case CssCombinatorKind.Column:
                    return MoveColumn();
                default:
                    return MoveSingle();
            }
        }

        #endregion

        #region Helpers

        // The kinds that reach at most one element. The _started check has to come
        // before the lookup: PreviousElementSibling scans the parent's child list,
        // and the terminating call would otherwise pay for it a second time.
        private Boolean MoveSingle()
        {
            if (_started)
            {
                return false;
            }

            _started = true;
            _current = _kind switch
            {
                CssCombinatorKind.Child => _source.ParentElement,
                CssCombinatorKind.Deep => _source.Parent is IShadowRoot shadowRoot ? shadowRoot.Host : null,
                CssCombinatorKind.AdjacentSibling => _source.PreviousElementSibling,
                CssCombinatorKind.Namespace => _source,
                _ => null,
            };

            return _current != null;
        }

        private Boolean MoveAncestor()
        {
            var previous = _started ? _current : _source;
            _started = true;
            _current = previous?.ParentElement;
            return _current != null;
        }

        private Boolean MoveSibling()
        {
            if (!_started)
            {
                _started = true;

                // Walking PreviousElementSibling repeatedly is quadratic, since each step
                // has to locate the element in its parent again. Scan the child list once
                // and then walk it backwards by index.
                if (_source is Node node && node.Parent is Node parent)
                {
                    var children = parent.ChildNodes;
                    _siblings = children;

                    for (var i = 0; i < children.Length; i++)
                    {
                        if (Object.ReferenceEquals(children[i], node))
                        {
                            _index = i;
                            break;
                        }
                    }
                }
                else
                {
                    _current = _source;
                }
            }

            if (_siblings != null)
            {
                while (_index > 0)
                {
                    var child = _siblings[--_index];

                    if (child.NodeType == NodeType.Element)
                    {
                        _current = (Element)child;
                        return true;
                    }
                }

                return false;
            }

            _current = _current?.PreviousElementSibling;
            return _current != null;
        }

        private Boolean MoveColumn()
        {
            if (!_started)
            {
                _started = true;
                _cells = CssCombinator.GetColumnCells(_source);
            }

            if (_cells != null && _index < _cells.Count)
            {
                _current = _cells[_index++];
                return true;
            }

            return false;
        }

        #endregion
    }
}
