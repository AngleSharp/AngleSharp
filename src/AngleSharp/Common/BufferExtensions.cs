namespace AngleSharp.Common;

using System;

/// <summary>
///
/// </summary>
public static class BufferExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="test"></param>
    /// <returns></returns>
    public static Boolean Isi(this IBuffer buffer, ReadOnlySpan<Char> test)
    {
        return buffer.HasText(test, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="test"></param>
    /// <returns></returns>
    public static Boolean Is(this IBuffer buffer, ReadOnlySpan<Char> test)
    {
        return buffer.HasText(test, StringComparison.Ordinal);
    }
}