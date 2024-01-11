namespace AngleSharp.Common;

using System;

static class StringOrMemoryExtensions
{
    public static Boolean Is(this StringOrMemory str, StringOrMemory other) => str == other;

    public static Boolean Is(this StringOrMemory str, String other) => str == other;

    public static Boolean Isi(this StringOrMemory str, StringOrMemory other) =>
        str.Memory.Span.Equals(other.Memory.Span, StringComparison.OrdinalIgnoreCase);

    public static Boolean Isi(this StringOrMemory str, String other) =>
        str.Memory.Span.Equals(other.AsSpan(), StringComparison.OrdinalIgnoreCase);

    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b, StringOrMemory c, StringOrMemory d) =>
        str.Is(a) || str.Is(b) || str.Is(c) || str.Is(d);

    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b, StringOrMemory c) =>
        str.Is(a) || str.Is(b) || str.Is(c);

    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b) => str.Is(a) || str.Is(b);

    public static Boolean StartsWith(this StringOrMemory str, String test, StringComparison comparison) =>
        str.Memory.Span.StartsWith(test.AsSpan(), comparison);

    public static Boolean Equals(this StringOrMemory str, String test, StringComparison comparison) =>
        str.Memory.Span.Equals(test.AsSpan(), comparison);
}