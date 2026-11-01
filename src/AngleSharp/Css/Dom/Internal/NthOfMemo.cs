namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Remembers, while an :nth-child(An+B of S) is being matched, where each sibling stands
    /// among those matching the S of every such clause nested inside it.
    /// </summary>
    /// <remarks>
    /// Matching E against :nth-child(An+B of S) evaluates S on the siblings before E. When S
    /// holds another such clause, every one of those evaluations walks the siblings again, so a
    /// query cost about siblings^(depth + 1): three levels on 1,000 siblings took minutes. The
    /// tree does not change while the outermost clause matches, so each nested clause counts a
    /// parent's children once and answers every later sibling from that count. The state is per
    /// thread because Match has no context to carry it, and it is emptied when the outermost
    /// clause returns, so it never outlives one match or keeps a document alive. An unnested
    /// clause only moves the counter; the dictionary appears once nesting does.
    /// </remarks>
    static class NthOfMemo
    {
        private const Int32 MaxRetained = 4096;

        [ThreadStatic]
        private static Int32 _matching;

        [ThreadStatic]
        private static Dictionary<Key, Int32>? _positions;

        public static Boolean IsNested => _matching > 0;

        public static void Enter() => _matching++;

        public static void Leave()
        {
            if (--_matching == 0 && _positions is { Count: > 0 } positions)
            {
                // Emptied, so it holds no element, and kept for the next match on this thread,
                // unless a wide parent grew it past what is worth holding on to
                if (positions.Count > MaxRetained)
                {
                    _positions = null;
                }
                else
                {
                    positions.Clear();
                }
            }
        }

        /// <summary>
        /// Gets the 1-based position of the element among its siblings matching the clause's
        /// selector, counted from the start or the end, or 0 if it does not match it.
        /// </summary>
        public static Int32 GetPosition<T>(ChildSelector clause, T nodes, IElement element, IElement? scope, Boolean fromEnd)
            where T : INodeListAccessor
        {
            var positions = _positions ??= new Dictionary<Key, Int32>(KeyComparer.Instance);

            if (!positions.TryGetValue(new Key(clause, element, scope), out var position))
            {
                var kind = clause.Kind;
                var length = nodes.Length;
                var k = 0;

                for (var n = 0; n < length; n++)
                {
                    if (nodes.TryGetElement(fromEnd ? length - 1 - n : n, out var child))
                    {
                        var matched = kind.Match(child, scope);

                        if (matched)
                        {
                            k += 1;
                        }

                        positions[new Key(clause, child, scope)] = matched ? k : 0;
                    }
                }

                positions.TryGetValue(new Key(clause, element, scope), out position);
            }

            return position;
        }

        private readonly struct Key(ChildSelector clause, IElement element, IElement? scope)
        {
            public readonly ChildSelector Clause = clause;
            public readonly IElement Element = element;
            public readonly IElement? Scope = scope;
        }

        // Identity, whatever the DOM types do with Equals
        private sealed class KeyComparer : IEqualityComparer<Key>
        {
            public static readonly KeyComparer Instance = new();

            public Boolean Equals(Key x, Key y) =>
                ReferenceEquals(x.Clause, y.Clause) && ReferenceEquals(x.Element, y.Element) && ReferenceEquals(x.Scope, y.Scope);

            public Int32 GetHashCode(Key key) => unchecked(
                (RuntimeHelpers.GetHashCode(key.Clause) * 31 + RuntimeHelpers.GetHashCode(key.Element)) * 31 +
                (key.Scope is null ? 0 : RuntimeHelpers.GetHashCode(key.Scope)));
        }
    }
}
