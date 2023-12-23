namespace AngleSharp.Benchmarks.UserCode;

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Microsoft.Extensions.ObjectPool;
using ReadOnly.Html;
using Text;

public static class Helpers
{
    public static IEnumerable<IReadOnlyNode> AllDescendants(this IReadOnlyNode node, String tag = null)
    {
        for (var i = 0; i < node.ChildNodes.Length; i++)
        {
            var child = node.ChildNodes[i];
            if (IsTag(child, tag))
                yield return child;

            foreach (var next in child.AllDescendants())
                if (IsTag(next, tag))
                    yield return next;
        }

        static Boolean IsTag(IReadOnlyNode node, String tag)
        {
            if (tag == null) return true;
            return node is IReadOnlyElement e && e.LocalName.Memory.Span.SequenceEqual(tag);
        }
    }

    public static Boolean Id(this IReadOnlyNode node, ReadOnlySpan<Char> id)
    {
        var element = node as IReadOnlyElement;
        if (element == null)
            return false;

        var classAttr = element.Attributes["id"];
        if (classAttr == null)
            return false;

        if (classAttr.Value == id)
            return true;

        return false;
    }

    public static Boolean Class(this IReadOnlyNode node, ReadOnlySpan<Char> className)
    {
        var element = node as IReadOnlyElement;
        if (element == null)
            return false;

        var classAttr = element.Attributes["class"];
        if (classAttr == null)
            return false;

        if (classAttr.Value == className)
            return true;

        foreach (var part in classAttr.Value.Memory.Span.Split(" "))
            if (part.SequenceEqual(className))
                return true;

        return false;
    }

    public static Boolean Attr(this IReadOnlyNode node, StringOrMemory name, String value = null)
    {
        var element = node as IReadOnlyElement;
        var attr = element?.Attributes[name];
        if (attr == null)
        {
            return false;
        }

        return value == null || attr.Value == value;
    }

    public static Boolean Tag(this IReadOnlyNode node, StringOrMemory name)
    {
        var element = node as IReadOnlyElement;
        if (element == null)
        {
            return false;
        }

        return element.LocalName == name;
    }

    public static Boolean TagClass(this IReadOnlyNode node, StringOrMemory tag, StringOrMemory className)
    {
        return node.Tag(tag) && node.Class(className);
    }

    private static readonly ObjectPool<StringBuilder> _sbPool = ObjectPool.Create(new StringBuilderPooledObjectPolicy());

    public static String GetTextContent(this IReadOnlyNode node, Boolean trim = false)
    {
        var sb = _sbPool.Get();
        try
        {
            return node.GetTextContent(sb, trim);
        }
        finally
        {
            _sbPool.Return(sb);
        }
    }

    public static String GetTextContent(this IReadOnlyNode node, StringBuilder sb, Boolean trim = false)
    {
        var text = node.AllDescendants().OfType<IReadOnlyTextNode>();
        foreach (var textNode in text)
        {
            sb.Append(textNode.Content.Memory.Span);
        }

        if (trim && sb.Length > 0)
        {
            var arr = ArrayPool<Char>.Shared.Rent(sb.Length);
            try
            {
                sb.CopyTo(0, arr, 0, sb.Length);
                return arr.AsSpan(0, sb.Length).Trim().ToString();
            }
            finally
            {
                ArrayPool<Char>.Shared.Return(arr);
            }
        }
        var tmp = sb.ToString();
        return tmp;
    }

    private static readonly ObjectPool<Stack<IReadOnlyNode>> _stackPool = new DefaultObjectPool<Stack<IReadOnlyNode>>(new DefaultPooledObjectPolicy<Stack<IReadOnlyNode>>());

    public static IEnumerable<IReadOnlyNode> QueryAll(this IReadOnlyNode node, params Func<IReadOnlyNode, Boolean>[] chain)
    {
        var stack = _stackPool.Get();
        try
        {
            foreach (var result in node.ChainInner(stack, chain.AsMemory()))
            {
                yield return result;
            }
        }
        finally
        {
            stack.Clear();
            _stackPool.Return(stack);
        }
    }

    public static IReadOnlyNode QueryOne(this IReadOnlyNode node, params Func<IReadOnlyNode, Boolean>[] chain)
    {
        return node.QueryAll(chain).FirstOrDefault();
    }

    // public static IEnumerable<IReadOnlyNode> QuerryAll(this IReadOnlyNode node, Stack<IReadOnlyNode> stack, params Func<IReadOnlyNode, Boolean>[] chain)
    // {
    //     if (stack == null)
    //     {
    //         stack = new Stack<IReadOnlyNode>();
    //     }
    //     else
    //     {
    //         stack.Clear();
    //     }
    //
    //     return node.ChainInner(stack, chain.AsMemory()).ToArray();
    // }

    private static Boolean ChainMatches(this IReadOnlyNode node, ReadOnlyMemory<Func<IReadOnlyNode, Boolean>> chain)
    {
        if (chain.Length == 0)
            return false;

        var last = chain.Span[chain.Length - 1];
        if (!last(node))
            return false;

        int i = chain.Length - 2;

        // find that all items in chain match distinct parent nodes
        while (i > 0 && node.Parent != null)
        {
            node = node.Parent;
            if (!chain.Span[i](node))
                i--;
        }

        return i == 0;
    }

    private static IEnumerable<IReadOnlyNode> ChainInner(
        this IReadOnlyNode parent,
        Stack<IReadOnlyNode> stack,
        ReadOnlyMemory<Func<IReadOnlyNode, Boolean>> chain)
    {
        stack.Push(parent);

        while (stack.Count > 0)
        {
            var next = stack.Pop();
            if (next.ChainMatches(chain))
            {
                yield return next;
            }

            var length = next.ChildNodes.Length;
            while (length > 0)
            {
                stack.Push(next.ChildNodes[--length]);
            }
        }
    }
}

internal static class SpanExtensions
{
    /// <summary>
    /// Splits the span by the given sentinel, removing empty segments.
    /// </summary>
    /// <param name="span">The span to split</param>
    /// <param name="sentinel">The sentinel to split the span on.</param>
    /// <returns>An enumerator over the span segments.</returns>
    public static StringSplitEnumerator Split(this ReadOnlySpan<Char> span, ReadOnlySpan<Char> sentinel) =>
        new(span, sentinel);

    /// <summary>
    /// Splits the span by the given sentinel, removing empty segments.
    /// </summary>
    /// <param name="span">The span to split</param>
    /// <param name="sentinel">The sentinel to split the span on.</param>
    /// <returns>An enumerator over the span segments.</returns>
    public static MemStringSplitEnumerator Split(this ReadOnlyMemory<Char> span, ReadOnlySpan<Char> sentinel) =>
        new(span, sentinel);

    internal ref struct StringSplitEnumerator
    {
        private readonly ReadOnlySpan<Char> _sentinel;
        private ReadOnlySpan<Char> _span;

        public StringSplitEnumerator(ReadOnlySpan<Char> span, ReadOnlySpan<Char> sentinel)
        {
            _span = span;
            _sentinel = sentinel;
        }

        public Boolean MoveNext()
        {
            while (true)
            {
                if (_span.Length == 0)
                {
                    return false;
                }

                var index = _span.IndexOf(_sentinel, StringComparison.Ordinal);
                if (index < 0)
                {
                    Current = _span;
                    _span = default;
                }
                else
                {
                    Current = _span[..index];
                    _span = _span[(index + 1)..];
                }

                if (Current.Length == 0)
                {
                    continue;
                }

                return true;
            }
        }

        public ReadOnlySpan<Char> Current { get; private set; }

        public readonly StringSplitEnumerator GetEnumerator() => this;
    }

    internal ref struct MemStringSplitEnumerator
    {
        private readonly ReadOnlySpan<Char> _sentinel;
        private ReadOnlyMemory<Char> _mem;

        public MemStringSplitEnumerator(ReadOnlyMemory<Char> mem, ReadOnlySpan<Char> sentinel)
        {
            _mem = mem;
            _sentinel = sentinel;
        }

        public Boolean MoveNext()
        {
            while (true)
            {
                if (_mem.Length == 0)
                {
                    return false;
                }

                var index = _mem.Span.IndexOf(_sentinel, StringComparison.Ordinal);
                if (index < 0)
                {
                    Current = _mem;
                    _mem = default;
                }
                else
                {
                    Current = _mem[..index];
                    _mem = _mem[(index + 1)..];
                }

                if (Current.Length == 0)
                {
                    continue;
                }

                return true;
            }
        }

        public ReadOnlyMemory<Char> Current { get; private set; }

        public readonly MemStringSplitEnumerator GetEnumerator() => this;
    }
}