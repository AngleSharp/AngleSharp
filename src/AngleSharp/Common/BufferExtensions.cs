namespace AngleSharp.Common;

using System;

internal static class BufferExtensions
{
    public static Boolean Isi(this IBuffer buffer, ReadOnlySpan<Char> test) => buffer.HasText(test, StringComparison.OrdinalIgnoreCase);
    public static Boolean Isi(this IBuffer buffer, Int32 start, Int32 length, ReadOnlySpan<Char> test) => buffer.HasTextAt(test, start, length, StringComparison.OrdinalIgnoreCase);
    public static Boolean Is(this IBuffer buffer, ReadOnlySpan<Char> test) => buffer.HasText(test);
    public static Boolean Is(this IBuffer buffer, Int32 start, Int32 length, ReadOnlySpan<Char> test) => buffer.HasTextAt(test, start, length);

    public static Boolean Isi(this IBuffer buffer, ReadOnlyMemory<Char> test) => buffer.HasText(test.Span, StringComparison.OrdinalIgnoreCase);
    public static Boolean Isi(this IBuffer buffer, Int32 start, Int32 length, ReadOnlyMemory<Char> test) => buffer.HasTextAt(test.Span, start, length, StringComparison.OrdinalIgnoreCase);
    public static Boolean Is(this IBuffer buffer, ReadOnlyMemory<Char> test) => buffer.HasText(test.Span);
    public static Boolean Is(this IBuffer buffer, Int32 start, Int32 length, ReadOnlyMemory<Char> test) => buffer.HasTextAt(test.Span, start, length);

}