namespace AngleSharp.Core.Tests.Library;

using System;
using Common;
using NUnit.Framework;

public class ArrayPoolBufferTests
{
    [Test]
    public void CanAppendAndStrings()
    {
        using var b = new ArrayPoolBuffer(128);
        b.Append("Hello World!".AsSpan());
        Assert.AreEqual("Hello World!", b.GetDataAndClear().String);
    }

    [Test]
    public void CanAppendMultipleTimesAndProduceStrings()
    {
        var b = new ArrayPoolBuffer(128);
        b.Append("Hello World!".AsSpan());
        b.Append('!');
        Assert.AreEqual("Hello World!!", b.GetDataAndClear().String);
    }

    [Test]
    public void CanProduceMultipleStrings()
    {
        var b = new ArrayPoolBuffer(1024);

        for (int i = 0; i < 5; i++)
        {
            b.Append('<');
            b.Append('s');
            b.Append('c');
            b.Append('r');
            b.Append('i');
            b.Append('p');
            b.Append('t');
            b.Append('>');
            Assert.AreEqual(b.GetDataAndClear().String, "<script>");

            b.Append('<');
            b.Append('!');
            b.Append('-');
            b.Append('-');
            b.Append('.');
            b.Append('.');
            b.Append('.');
            Assert.AreEqual(b.GetDataAndClear().String, "<!--...");

            b.Append('s');
            b.Append('c');
            b.Append('r');
            b.Append('i');
            b.Append('p');
            b.Append('t');
            b.Insert(0, '<');
            b.Insert(1, '/');
            b.Append('>');
            Assert.AreEqual("</script>", b.GetDataAndClear().String);

            b.Append('b');
            b.Append('o');
            b.Append('d');
            b.Append('y');
            b.Insert(0, '<');
            b.Insert(1, '/');
            b.Append('>');
            Assert.AreEqual("</body>", b.GetDataAndClear().String);

            b.Append('n');
            b.Append('o');
            b.Append('f');
            b.Append('r');
            b.Append('a');
            b.Append('m');
            b.Append('e');
            b.Append('s');
            Assert.AreEqual(b.GetDataAndClear().String, "noframes");

            b.Append('h');
            b.Append('t');
            b.Append('m');
            b.Append('l');
            Assert.AreEqual(b.GetDataAndClear().String, "html");
        }
    }


    [Test]
    public void CanAppendMultipleTimesWhileDiscarding()
    {
        var b = new ArrayPoolBuffer(16);
        for (int i = 0; i < 1024; i++)
        {
            b.Append('<');
            b.Append('s');
            b.Append('c');
            b.Append('r');
            b.Append('i');
            b.Append('p');
            b.Append('t');
            b.Append('>');
            Assert.True(b.HasText("<script>".AsSpan()));
            b.Discard();

            b.Append('<');
            b.Append('!');
            b.Append('-');
            b.Append('-');
            b.Append('.');
            b.Append('.');
            b.Append('.');
            Assert.True(b.HasText("<!--...".AsSpan()));
            b.Discard();

            b.Append('s');
            b.Append('c');
            b.Append('r');
            b.Append('i');
            b.Append('p');
            b.Append('t');
            b.Insert(0, '<');
            b.Insert(1, '/');
            b.Append('>');
            Assert.True(b.HasText("</script>".AsSpan()));
            b.Discard();

            b.Append('b');
            b.Append('o');
            b.Append('d');
            b.Append('y');
            b.Insert(0, '<');
            b.Insert(1, '/');
            b.Append('>');
            Assert.True(b.HasText("</body>".AsSpan()));
            b.Discard();

            b.Append('n');
            b.Append('o');
            b.Append('f');
            b.Append('r');
            b.Append('a');
            b.Append('m');
            b.Append('e');
            b.Append('s');
            Assert.True(b.HasText("noframes".AsSpan()));
            b.Discard();

            b.Append('h');
            b.Append('t');
            b.Append('m');
            b.Append('l');
            Assert.True(b.HasText("html".AsSpan()));
            b.Discard();
        }
    }
}