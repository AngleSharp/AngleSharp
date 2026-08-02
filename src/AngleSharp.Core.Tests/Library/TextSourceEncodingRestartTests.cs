namespace AngleSharp.Core.Tests.Library;

using System;
using System.IO;
using System.Text;
using AngleSharp.Text;
using NUnit.Framework;

/// <summary>
/// Covers the encoding-restart contract shared by the byte-backed text sources.
/// Re-decoding the retained bytes moves character offsets, so the consumed prefix has
/// to be validated against both the old and the new buffer before the read position
/// can be carried over. See also <see cref="ReadOnlyByteTextSourceTests"/>.
/// </summary>
[TestFixture]
public sealed class TextSourceEncodingRestartTests
{
    /// <summary>
    /// UTF-8 bytes for "é": one character as UTF-8, two as a single-byte encoding. So
    /// "ab" plus this sequence is 3 characters decoded as UTF-8 and 4 as Latin-1, which
    /// is the shape where the new buffer is longer than the old one.
    /// </summary>
    private static readonly Byte[] Input = [(Byte)'a', (Byte)'b', 0xC3, 0xA9];

    private static Encoding Latin1 => Encoding.GetEncoding(28591);

    private static MemoryStream Stream() => new(Input, writable: false);

    [Test]
    public void WritableRedecodeAfterReadingPastTheEndRequestsARestart()
    {
        using var source = new WritableTextSource(Stream(), TextEncoding.Utf8, encodingIsCertain: false);

        // Three characters of content, read four times: the index ends up past the old
        // buffer but still inside the longer Latin-1 decoding.
        for (var i = 0; i < 4; i++)
        {
            source.ReadCharacter();
        }

        Assert.That(source.Index, Is.EqualTo(4));
        Assert.That(source.Length, Is.EqualTo(3));

        Assert.That(() => source.CurrentEncoding = Latin1, Throws.TypeOf<NotSupportedException>());
        Assert.That(source.Index, Is.EqualTo(0));
        Assert.That(source.Text, Is.EqualTo("abÃ©"));
    }

    [Test]
    public void WritableRedecodeThatKeepsTheConsumedPrefixIsAnInstantSwitch()
    {
        using var source = new WritableTextSource(Stream(), TextEncoding.Utf8, encodingIsCertain: false);

        // Only the ASCII prefix is consumed, and ASCII decodes identically either way.
        source.ReadCharacter();
        source.ReadCharacter();

        source.CurrentEncoding = Latin1;

        Assert.That(source.Index, Is.EqualTo(2));
        Assert.That(source.Text, Is.EqualTo("abÃ©"));
        Assert.That(source.ReadCharacter(), Is.EqualTo('Ã'));
    }

    [Test]
    public void WritableInstantSwitchToUtf8DoesNotPadWithTheDecodeBuffer()
    {
        // Switching towards a multi-byte encoding is the case where the decode buffer is
        // larger than the decoded text: GetMaxCharCount assumes every byte is one
        // character. Only charLength may be appended, never the whole buffer.
        using var source = new WritableTextSource(Stream(), Latin1, encodingIsCertain: false);

        source.ReadCharacter();
        source.ReadCharacter();

        source.CurrentEncoding = TextEncoding.Utf8;

        Assert.That(source.Text, Is.EqualTo("abé"));
        Assert.That(source.Length, Is.EqualTo(3));
    }

    [Test]
    public void WritableRestartToUtf8DoesNotPadWithTheDecodeBuffer()
    {
        using var source = new WritableTextSource(Stream(), Latin1, encodingIsCertain: false);

        // Consume "abÃ" - the third character differs between the two decodings, so the
        // position cannot carry and the restart path rewrites the whole buffer.
        for (var i = 0; i < 3; i++)
        {
            source.ReadCharacter();
        }

        Assert.That(() => source.CurrentEncoding = TextEncoding.Utf8, Throws.TypeOf<NotSupportedException>());
        Assert.That(source.Text, Is.EqualTo("abé"));
        Assert.That(source.Length, Is.EqualTo(3));
    }

    [Test]
    public void WritableIgnoresADeclaredEncodingWhenAlreadyUtf16()
    {
        using var source = new WritableTextSource(Stream(), TextEncoding.Utf16Le, encodingIsCertain: false);

        source.CurrentEncoding = Latin1;

        Assert.That(source.CurrentEncoding, Is.EqualTo(TextEncoding.Utf16Le));
    }

    [Test]
    public void StreamingIgnoresADeclaredEncodingWhenAlreadyUtf16()
    {
        using var source = new StreamingTextSource(Stream(), TextEncoding.Utf16Le, allowEncodingRestart: true);

        source.CurrentEncoding = Latin1;

        Assert.That(source.CurrentEncoding, Is.EqualTo(TextEncoding.Utf16Le));
    }

    [Test]
    public void StreamingRedecodeThatKeepsTheConsumedPrefixIsAnInstantSwitch()
    {
        using var source = new StreamingTextSource(Stream(), TextEncoding.Utf8, allowEncodingRestart: true);

        source.ReadCharacter();
        source.ReadCharacter();

        source.CurrentEncoding = Latin1;

        Assert.That(source.CurrentEncoding.CodePage, Is.EqualTo(Latin1.CodePage));
        Assert.That(source.Index, Is.EqualTo(2));
    }
}
