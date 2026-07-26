namespace AngleSharp.Text;

using System;
using System.Text;

/// <summary>
/// Detects byte order marks recognized by HTML text sources.
/// </summary>
internal static class ByteOrderMark
{
    internal static Boolean TryDetect(Byte[] bytes, Int32 count, out Encoding encoding, out Int32 length)
        => TryDetect(bytes, 0, count, out encoding, out length);

    internal static Boolean TryDetect(
        Byte[] bytes,
        Int32 offset,
        Int32 count,
        out Encoding encoding,
        out Int32 length) => TryDetect(new ReadOnlySpan<Byte>(bytes, offset, count), out encoding, out length);

    internal static Boolean TryDetect(ReadOnlySpan<Byte> bytes, out Encoding encoding, out Int32 length)
    {
        if (bytes.Length >= 3 && bytes[0] == 0xef && bytes[1] == 0xbb && bytes[2] == 0xbf)
        {
            encoding = TextEncoding.Utf8;
            length = 3;
            return true;
        }

        if (bytes.Length >= 4 && bytes[0] == 0xff && bytes[1] == 0xfe && bytes[2] == 0 && bytes[3] == 0)
        {
            encoding = TextEncoding.Utf32Le;
            length = 4;
            return true;
        }

        if (bytes.Length >= 4 && bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0xfe && bytes[3] == 0xff)
        {
            encoding = TextEncoding.Utf32Be;
            length = 4;
            return true;
        }

        if (bytes.Length >= 2 && bytes[0] == 0xfe && bytes[1] == 0xff)
        {
            encoding = TextEncoding.Utf16Be;
            length = 2;
            return true;
        }

        if (bytes.Length >= 2 && bytes[0] == 0xff && bytes[1] == 0xfe)
        {
            encoding = TextEncoding.Utf16Le;
            length = 2;
            return true;
        }

        if (bytes.Length >= 4 && bytes[0] == 0x84 && bytes[1] == 0x31 && bytes[2] == 0x95 && bytes[3] == 0x33)
        {
            encoding = TextEncoding.Gb18030;
            length = 4;
            return true;
        }

        encoding = TextEncoding.Utf8;
        length = 0;
        return false;
    }
}
