#if NET8_0_OR_GREATER
namespace AngleSharp.Core.Tests.Library;

using System;
using System.IO;
using System.Text;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using NUnit.Framework;

/// <summary>
/// Covers the encoding-restart contract of <see cref="ReadOnlyByteTextSource"/>.
/// Because the source retains every byte it can always re-decode, but a re-decode
/// changes character offsets, so the already-consumed prefix has to be validated
/// exactly the way <c>WritableTextSource</c> validates it.
/// </summary>
[TestFixture]
public sealed class ReadOnlyByteTextSourceTests
{
    /// <summary>
    /// UTF-8 bytes for "é". Decoded as UTF-8 this is one character, decoded as a
    /// single-byte encoding it is two ("Ã©"), which is what shifts the offsets.
    /// </summary>
    private static readonly Byte[] TwoByteSequence = [0xC3, 0xA9];

    private static Byte[] Bytes(params Object[] parts)
    {
        using var buffer = new MemoryStream();

        foreach (var part in parts)
        {
            var chunk = part switch
            {
                String text => Encoding.ASCII.GetBytes(text),
                Byte[] raw => raw,
                _ => throw new ArgumentOutOfRangeException(nameof(parts)),
            };
            buffer.Write(chunk, 0, chunk.Length);
        }

        return buffer.ToArray();
    }

    [Test]
    public void RedecodeThatKeepsTheConsumedPrefixIsAnInstantSwitch()
    {
        // Pure ASCII before the switch, so both decodings agree on the prefix.
        var source = new ReadOnlyByteTextSource(Bytes("abc", TwoByteSequence, "def"));

        for (var i = 0; i < 3; i++)
        {
            source.ReadCharacter();
        }

        source.CurrentEncoding = Encoding.Latin1;

        Assert.That(source.Index, Is.EqualTo(3));
        Assert.That(source.Text, Is.EqualTo("abcÃ©def"));
        Assert.That(source.ReadCharacter(), Is.EqualTo('Ã'));
    }

    [Test]
    public void RedecodeThatShiftsTheConsumedPrefixRequestsARestart()
    {
        var source = new ReadOnlyByteTextSource(Bytes("<!--", TwoByteSequence, "-->rest"));

        // Consume "<!--é" — five characters under the initial UTF-8 decode. Under
        // Latin-1 the same five bytes are six characters, so the offset cannot carry.
        for (var i = 0; i < 5; i++)
        {
            source.ReadCharacter();
        }

        Assert.That(() => source.CurrentEncoding = Encoding.Latin1, Throws.TypeOf<NotSupportedException>());

        // The parser recovers by re-tokenizing, so the source must be rewound and
        // already carry the new decoding.
        Assert.That(source.Index, Is.EqualTo(0));
        Assert.That(source.CurrentEncoding, Is.EqualTo(Encoding.Latin1));
        Assert.That(source.Text, Is.EqualTo("<!--Ã©-->rest"));
    }

    [Test]
    public void RedecodeAfterReadingPastTheEndRequestsARestart()
    {
        // "ab" + a two-byte sequence decodes to 3 characters as UTF-8 but 4 as Latin-1,
        // so an index of 4 is inside the new buffer yet past the old one. That is the
        // only shape where the new decoding's length alone does not bound the compare.
        var source = new ReadOnlyByteTextSource(Bytes("ab", TwoByteSequence));

        for (var i = 0; i < 4; i++)
        {
            source.ReadCharacter();
        }

        Assert.That(source.Index, Is.EqualTo(4));

        Assert.That(() => source.CurrentEncoding = Encoding.Latin1, Throws.TypeOf<NotSupportedException>());
        Assert.That(source.Index, Is.EqualTo(0));
        Assert.That(source.Text, Is.EqualTo("abÃ©"));
    }

    [Test]
    public void RedecodeWithAnIndexBeyondTheContentRequestsARestart()
    {
        var source = new ReadOnlyByteTextSource(Bytes("abc"));

        // The index is publicly settable and unchecked, so it can point past every
        // buffer involved. That must not read pooled residue or throw out of range.
        source.Index = Int32.MaxValue - 1;

        Assert.That(() => source.CurrentEncoding = Encoding.Latin1, Throws.TypeOf<NotSupportedException>());
        Assert.That(source.Index, Is.EqualTo(0));
    }

    [Test]
    public void RedecodeIsRejectedOnceTheEncodingIsCertain()
    {
        var source = new ReadOnlyByteTextSource(Bytes("abc", TwoByteSequence), Encoding.UTF8);

        source.CurrentEncoding = Encoding.Latin1;

        Assert.That(source.CurrentEncoding, Is.EqualTo(Encoding.UTF8));
        Assert.That(source.Text, Is.EqualTo("abcé"));
    }

    [Test]
    public void LateEncodingSwitchMatchesTheCorrectlyDecodedDocument()
    {
        // A non-ASCII byte ahead of the declaration is what makes the switch shift
        // offsets; the parser has to end up with the same tree either way.
        var bytes = Bytes(
            "<!doctype html><!--",
            TwoByteSequence,
            "--><meta charset=\"iso-8859-1\"><title>t</title><p>body",
            TwoByteSequence);

        var declared = TextEncoding.Resolve("iso-8859-1");
        var parser = new HtmlParser();

        using var expected = parser.ParseDocument(declared.GetString(bytes));
        using var actual = parser.ParseDocument(new TextSource(new ReadOnlyByteTextSource(bytes)));

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
    }

    [Test]
    public void LateEncodingSwitchMatchesTheAccumulatingStreamPath()
    {
        var bytes = Bytes(
            "<!doctype html><!--",
            TwoByteSequence,
            "--><meta charset=\"iso-8859-1\"><title>t</title><p>body",
            TwoByteSequence);

        var parser = new HtmlParser();

        using var stream = new MemoryStream(bytes);
        using var expected = parser.ParseDocument(stream);
        using var actual = parser.ParseDocument(new TextSource(new ReadOnlyByteTextSource(bytes)));

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
        Assert.That(actual.CharacterSet, Is.EqualTo(expected.CharacterSet));
    }
}
#endif
