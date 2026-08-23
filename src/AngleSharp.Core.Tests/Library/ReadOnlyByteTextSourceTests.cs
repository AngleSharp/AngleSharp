namespace AngleSharp.Core.Tests.Library;

using System;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using NUnit.Framework;

[TestFixture]
public sealed class ReadOnlyByteTextSourceTests
{
    private static readonly Byte[] TwoByteSequence = [0xC3, 0xA9];

#if !NETFRAMEWORK
    [Test]
    public void RedecodeThatKeepsConsumedPrefixSwitchesInPlace()
    {
        using var source = new ReadOnlyByteTextSource(Bytes("abc", TwoByteSequence, "def"));

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
    public void RedecodeThatShiftsConsumedPrefixRequestsRestart()
    {
        using var source = new ReadOnlyByteTextSource(Bytes("<!--", TwoByteSequence, "-->rest"));

        for (var i = 0; i < 5; i++)
        {
            source.ReadCharacter();
        }

        Assert.That(() => source.CurrentEncoding = Encoding.Latin1, Throws.TypeOf<NotSupportedException>());
        Assert.That(source.Index, Is.EqualTo(0));
        Assert.That(source.CurrentEncoding, Is.EqualTo(Encoding.Latin1));
        Assert.That(source.Text, Is.EqualTo("<!--Ã©-->rest"));
    }

    [Test]
    public void RedecodeAfterReadingPastEndRequestsRestart()
    {
        using var source = new ReadOnlyByteTextSource(Bytes("ab", TwoByteSequence));

        for (var i = 0; i < 4; i++)
        {
            source.ReadCharacter();
        }

        Assert.That(() => source.CurrentEncoding = Encoding.Latin1, Throws.TypeOf<NotSupportedException>());
        Assert.That(source.Index, Is.EqualTo(0));
        Assert.That(source.Text, Is.EqualTo("abÃ©"));
    }

    [Test]
    public void ExplicitEncodingCannotBeReplaced()
    {
        using var source = new ReadOnlyByteTextSource(Bytes("abc", TwoByteSequence), Encoding.UTF8);

        source.CurrentEncoding = Encoding.Latin1;

        Assert.That(source.CurrentEncoding, Is.EqualTo(Encoding.UTF8));
        Assert.That(source.Text, Is.EqualTo("abcé"));
    }
#endif

    [Test]
    public void ParserOverloadHonorsReadOnlyMemorySliceAndByteOrderMark()
    {
        var markup = "<!doctype html><title>hé😀終</title><p>body";
        var payload = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(markup)).ToArray();
        var buffer = new Byte[payload.Length + 8];
        payload.CopyTo(buffer, 4);
        var parser = new HtmlParser();

        using var document = parser.ParseDocument(buffer.AsMemory(4, payload.Length));

        Assert.That(document.Title, Is.EqualTo("hé😀終"));
        Assert.That(document.CharacterSet, Is.EqualTo(Encoding.UTF8.WebName));
    }

#if !NETFRAMEWORK
    [Test]
    public void ParserOverloadUsesAuthoritativeEncoding()
    {
        var encoding = Encoding.Latin1;
        var parser = new HtmlParser();

        using var document = parser.ParseDocument(
            encoding.GetBytes("<!doctype html><title>café</title>"),
            encoding);

        Assert.That(document.Title, Is.EqualTo("café"));
        Assert.That(document.CharacterSet, Is.EqualTo(encoding.WebName));
    }
#endif

    [Test]
    public void LateEncodingSwitchMatchesCorrectlyDecodedDocument()
    {
        var bytes = Bytes(
            "<!doctype html><!--",
            TwoByteSequence,
            "--><meta charset=\"iso-8859-1\"><title>t</title><p>body",
            TwoByteSequence);
        var declared = TextEncoding.Resolve("iso-8859-1");
        var parser = new HtmlParser();

        using var expected = parser.ParseDocument(declared.GetString(bytes));
        using var actual = parser.ParseDocument(bytes);

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
        Assert.That(actual.CharacterSet, Is.EqualTo(declared.WebName));
    }

    [Test]
    public void ByteBufferParserMatchesAccumulatingStreamPath()
    {
        var bytes = Bytes(
            "<!doctype html><!--",
            TwoByteSequence,
            "--><meta charset=\"iso-8859-1\"><title>t</title><p>body",
            TwoByteSequence);
        var parser = new HtmlParser();

        using var stream = new MemoryStream(bytes);
        using var expected = parser.ParseDocument(stream);
        using var actual = parser.ParseDocument(bytes);

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
        Assert.That(actual.CharacterSet, Is.EqualTo(expected.CharacterSet));
    }

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
}
