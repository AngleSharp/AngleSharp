namespace AngleSharp.Common;

using System;

internal static class BufferExtensions
{
    #region String

    public static Boolean Is(this IMutableCharBuffer buffer, String test) => buffer.HasText(test.AsSpan());

    public static Boolean Is(this IMutableCharBuffer buffer, Int32 start, Int32 length, String test) =>
        buffer.HasTextAt(test.AsSpan(), start, length);

    public static Boolean Isi(this IMutableCharBuffer buffer, String test) =>
        buffer.HasText(test.AsSpan(), StringComparison.OrdinalIgnoreCase);

    public static Boolean Isi(this IMutableCharBuffer buffer, Int32 start, Int32 length, String test) =>
        buffer.HasTextAt(test.AsSpan(), start, length, StringComparison.OrdinalIgnoreCase);

    #endregion

    #region StringOrMemory

    public static Boolean Is(this IMutableCharBuffer buffer, StringOrMemory test) => buffer.HasText(test.Memory.Span);

    public static Boolean Is(this IMutableCharBuffer buffer, Int32 start, Int32 length, StringOrMemory test) =>
        buffer.HasTextAt(test.Memory.Span, start, length);

    public static Boolean Isi(this IMutableCharBuffer buffer, StringOrMemory test) =>
        buffer.HasText(test.Memory.Span, StringComparison.OrdinalIgnoreCase);

    public static Boolean Isi(this IMutableCharBuffer buffer, Int32 start, Int32 length, StringOrMemory test) =>
        buffer.HasTextAt(test.Memory.Span, start, length, StringComparison.OrdinalIgnoreCase);

    #endregion

    #region ReadOnlyMemory<Char>

    public static Boolean Isi(this IMutableCharBuffer buffer, ReadOnlyMemory<Char> test) =>
        buffer.HasText(test.Span, StringComparison.OrdinalIgnoreCase);

    public static Boolean Isi(this IMutableCharBuffer buffer, Int32 start, Int32 length, ReadOnlyMemory<Char> test) =>
        buffer.HasTextAt(test.Span, start, length, StringComparison.OrdinalIgnoreCase);

    public static Boolean Is(this IMutableCharBuffer buffer, ReadOnlyMemory<Char> test) => buffer.HasText(test.Span);

    public static Boolean Is(this IMutableCharBuffer buffer, Int32 start, Int32 length, ReadOnlyMemory<Char> test) =>
        buffer.HasTextAt(test.Span, start, length);

    #endregion

    #region ReadOnlySpan<Char>

    public static Boolean Is(this IMutableCharBuffer buffer, ReadOnlySpan<Char> test) => buffer.HasText(test);

    public static Boolean Is(this IMutableCharBuffer buffer, Int32 start, Int32 length, ReadOnlySpan<Char> test) =>
        buffer.HasTextAt(test, start, length);

    public static Boolean Isi(this IMutableCharBuffer buffer, ReadOnlySpan<Char> test) =>
        buffer.HasText(test, StringComparison.OrdinalIgnoreCase);

    public static Boolean Isi(this IMutableCharBuffer buffer, Int32 start, Int32 length, ReadOnlySpan<Char> test) =>
        buffer.HasTextAt(test, start, length, StringComparison.OrdinalIgnoreCase);

    #endregion
}