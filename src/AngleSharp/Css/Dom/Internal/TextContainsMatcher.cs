namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Tests whether the text content of an element contains a fixed string, without
    /// materializing that text content.
    /// </summary>
    /// <remarks>
    /// The naive implementation is <c>element.TextContent.Contains(value)</c>, which
    /// concatenates every descendant text node into a fresh string for every candidate
    /// element. This streams the text nodes instead and runs Knuth-Morris-Pratt over them,
    /// so a match spanning a node boundary is still found while nothing is allocated. The
    /// prefix table is built once, when the selector is parsed.
    /// </remarks>
    sealed class TextContainsMatcher
    {
        private readonly String _value;
        private readonly Int32[] _prefixes;

        public TextContainsMatcher(String value)
        {
            _value = value;
            _prefixes = BuildPrefixes(value);
        }

        public Boolean Matches(IElement element)
        {
            if (_value.Length == 0)
            {
                return true;
            }

            var matched = 0;
            return Scan(element, ref matched, 0);
        }

        // Recurses for the first levels and continues iteratively below them, so deeply nested
        // documents cannot overflow the stack
        private Boolean Scan(INode node, ref Int32 matched, Int32 depth)
        {
            if (depth == ChildIndexStack.RecursionLimit)
            {
                return ScanIteratively(node, ref matched);
            }

            var children = node.ChildNodes;
            var n = children.Length;

            for (var i = 0; i < n; i++)
            {
                var child = children[i];

                if (child is IText text)
                {
                    if (Feed(text.Data, ref matched))
                    {
                        return true;
                    }
                }
                else if (child.HasChildNodes && Scan(child, ref matched, depth + 1))
                {
                    return true;
                }
            }

            return false;
        }

        private Boolean ScanIteratively(INode root, ref Int32 matched)
        {
            var resume = new ChildIndexStack(stackalloc Int32[ChildIndexStack.InitialDepth]);

            try
            {
                var parent = (Node)root;
                var children = parent.ChildNodes;
                var n = children.Length;
                var i = 0;

                while (true)
                {
                    if (i < n)
                    {
                        var child = children[i++];

                        if (child is IText text)
                        {
                            if (Feed(text.Data, ref matched))
                            {
                                return true;
                            }
                        }
                        else if (child.HasChildNodes)
                        {
                            resume.Push(i);
                            parent = child;
                            children = parent.ChildNodes;
                            n = children.Length;
                            i = 0;
                        }
                    }
                    else if (resume.Count > 0)
                    {
                        i = resume.Pop();
                        parent = parent.Parent!;
                        children = parent.ChildNodes;
                        n = children.Length;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            finally
            {
                resume.Dispose();
            }
        }

        private Boolean Feed(String data, ref Int32 matched)
        {
            var value = _value;
            var prefixes = _prefixes;
            var length = value.Length;
            var k = matched;

            for (var i = 0; i < data.Length; i++)
            {
                var current = data[i];

                while (k > 0 && current != value[k])
                {
                    k = prefixes[k - 1];
                }

                if (current == value[k])
                {
                    k++;

                    if (k == length)
                    {
                        matched = k;
                        return true;
                    }
                }
            }

            matched = k;
            return false;
        }

        private static Int32[] BuildPrefixes(String value)
        {
            var prefixes = new Int32[value.Length];
            var k = 0;

            for (var i = 1; i < value.Length; i++)
            {
                while (k > 0 && value[i] != value[k])
                {
                    k = prefixes[k - 1];
                }

                if (value[i] == value[k])
                {
                    k++;
                }

                prefixes[i] = k;
            }

            return prefixes;
        }
    }
}
