namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.Buffers;

    /// <summary>
    /// Represents the :has() pseudo-class, which matches an element if its
    /// argument matches an element relative to it.
    /// </summary>
    /// <remarks>
    /// Matching walks the part of the tree the argument can reach rather than
    /// a fixed region: a relative selector anchored at :scope only reaches
    /// rightwards from the anchor, so <c>:has(&gt; a)</c> only needs the
    /// children and <c>:has(+ a)</c> only the following siblings, instead of
    /// the whole subtree of the parent.
    /// </remarks>
    sealed class HasSelector : ISelector
    {
        #region Fields

        private const Int32 Unbounded = Int32.MaxValue;
        private const Int32 InitialDepth = 16;

        private readonly ISelector _selector;
        private readonly String _code;
        private readonly HasReach _reach;
        private readonly Int32 _maxDepth;
        private readonly Boolean _nextSiblingOnly;

        #endregion

        #region ctor

        public HasSelector(ISelector selector, String code, Priority specificity, Boolean anchored)
        {
            _selector = selector;
            _code = code;
            Specificity = specificity;
            _maxDepth = Unbounded;

            if (!anchored)
            {
                _reach = HasReach.Descendants;
            }
            else if (!TryGetRelativeReach(selector, out _reach, out _maxDepth, out _nextSiblingOnly))
            {
                _reach = HasReach.ParentSubtree;
                _maxDepth = Unbounded;
            }
        }

        #endregion

        #region Properties

        public Priority Specificity { get; }

        public String Text => PseudoClassNames.Separator + _code;

        #endregion

        #region Methods

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(_code);

        public Boolean Match(IElement element, IElement? scope)
        {
            if (element is not Element el)
            {
                return MatchFallback(element);
            }

            switch (_reach)
            {
                case HasReach.Descendants:
                    return el.HasChildNodes && MatchSubtree(el, el, _maxDepth, includeRoot: false);
                case HasReach.FollowingSiblings:
                    return MatchFollowingSiblings(el);
                default:
                    return MatchParentSubtree(el);
            }
        }

        /// <summary>
        /// Checks if the given selector contains a :has() anywhere, which makes
        /// matching it potentially as costly as walking a subtree.
        /// </summary>
        internal static Boolean IsContainedIn(ISelector selector) => selector switch
        {
            HasSelector => true,
            ComplexSelector complex => complex.ContainsHas,
            Selectors selectors => AnyContains(selectors),
            TypeSelector or ClassSelector or IdSelector or AllSelector or BaseAttrSelector => false,
            _ => selector.Text.IndexOf(PseudoClassNames.Separator + PseudoClassNames.Has + "(", StringComparison.OrdinalIgnoreCase) >= 0,
        };

        #endregion

        #region Helpers

        private static Boolean AnyContains(Selectors selectors)
        {
            foreach (var selector in selectors)
            {
                if (IsContainedIn(selector))
                {
                    return true;
                }
            }

            return false;
        }

        // A relative selector (":scope" followed by combinators that only go down or
        // forward) cannot reach anything outside the anchor's subtree or the subtrees of
        // its following siblings. Every other shape keeps the full parent subtree walk.
        private static Boolean TryGetRelativeReach(ISelector selector, out HasReach reach, out Int32 maxDepth, out Boolean nextSiblingOnly)
        {
            reach = HasReach.ParentSubtree;
            maxDepth = Unbounded;
            nextSiblingOnly = false;

            if (selector is not ComplexSelector complex || complex.Length < 2 || !IsScope(complex.GetSelector(0)))
            {
                return false;
            }

            var depth = 0;
            var unbounded = false;
            var last = complex.Length - 1;

            for (var i = 1; i <= last; i++)
            {
                if (ContainsScope(complex.GetSelector(i)))
                {
                    return false;
                }
            }

            for (var i = 0; i < last; i++)
            {
                switch (complex.GetKind(i))
                {
                    case CssCombinatorKind.Child:
                        depth++;
                        break;
                    case CssCombinatorKind.Descendant:
                        unbounded = true;
                        break;
                    case CssCombinatorKind.AdjacentSibling:
                    case CssCombinatorKind.Sibling:
                        break;
                    default:
                        return false;
                }
            }

            var first = complex.GetKind(0);

            if (first == CssCombinatorKind.Child || first == CssCombinatorKind.Descendant)
            {
                reach = HasReach.Descendants;
                maxDepth = unbounded ? Unbounded : depth;
            }
            else
            {
                reach = HasReach.FollowingSiblings;
                maxDepth = unbounded ? Unbounded : depth;
                nextSiblingOnly = last == 1 && first == CssCombinatorKind.AdjacentSibling;
            }

            return true;
        }

        private static Boolean IsScope(ISelector selector) =>
            selector is ScopePseudoClassSelector || selector.Text.Equals(PseudoClassNames.Separator + PseudoClassNames.Scope, StringComparison.OrdinalIgnoreCase);

        private static Boolean ContainsScope(ISelector selector) =>
            selector.Text.IndexOf(PseudoClassNames.Separator + PseudoClassNames.Scope, StringComparison.OrdinalIgnoreCase) >= 0;

        private Boolean MatchSubtree(Element root, Element anchor, Int32 maxDepth, Boolean includeRoot)
        {
            var indices = maxDepth > 0 ? ArrayPool<Int32>.Shared.Rent(InitialDepth) : null;

            try
            {
                var walker = new ElementTreeEnumerator(root, maxDepth, indices);

                if (!includeRoot)
                {
                    walker.MoveNext();
                }

                while (walker.MoveNext())
                {
                    if (_selector.Match(walker.Current, anchor))
                    {
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                // Growing the stack replaces the array; the rented one is ours to return either way.
                if (indices is not null)
                {
                    ArrayPool<Int32>.Shared.Return(indices);
                }
            }
        }

        private Boolean MatchFollowingSiblings(Element element)
        {
            if (element.Parent is not Node parent)
            {
                return false;
            }

            var children = parent.ChildNodes;
            var n = children.Length;
            var i = 0;

            while (i < n && !Object.ReferenceEquals(children[i], element))
            {
                i++;
            }

            for (i++; i < n; i++)
            {
                if (children[i] is Element sibling)
                {
                    if (MatchSubtree(sibling, element, _maxDepth, includeRoot: true))
                    {
                        return true;
                    }

                    if (_nextSiblingOnly)
                    {
                        break;
                    }
                }
            }

            return false;
        }

        private Boolean MatchParentSubtree(Element element)
        {
            if (element.Parent is not Element parent)
            {
                return false;
            }

            var children = parent.ChildNodes;

            for (var i = 0; i < children.Length; i++)
            {
                if (children[i] is Element child && MatchSubtree(child, element, Unbounded, includeRoot: true))
                {
                    return true;
                }
            }

            return false;
        }

        private Boolean MatchFallback(IElement element)
        {
            var elements = _reach == HasReach.Descendants ? element.Children : element.ParentElement?.Children;
            return elements is not null && _selector.MatchAny(elements, element) is not null;
        }

        #endregion

        #region Nested

        private enum HasReach : Byte
        {
            /// <summary>
            /// The descendants of the anchor.
            /// </summary>
            Descendants,
            /// <summary>
            /// The following siblings of the anchor and their descendants.
            /// </summary>
            FollowingSiblings,
            /// <summary>
            /// The anchor's siblings (including itself) and all their descendants.
            /// </summary>
            ParentSubtree,
        }

        #endregion
    }
}
