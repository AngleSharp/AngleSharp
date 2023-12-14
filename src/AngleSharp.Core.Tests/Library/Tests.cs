namespace AngleSharp.Core.Tests.Library;

using Common;
using NUnit.Framework;

public class Tests
{
    [Test]
    public void Check1()
    {
        using var b = new ArrayPoolBuffer(128);
        b.Append("Hello World!");
        Assert.AreEqual("Hello World!", b.ToString());
    }

    [Test]
    public void Check2()
    {
        var b = new ArrayPoolBuffer(128);
        b.Append("Hello World!");
        b.Append('!');
        Assert.AreEqual("Hello World!!", b.ToString());
    }

    [Test]
    public void Check3()
    {
        var b = new ArrayPoolBuffer(1024);

        b.Append('<');
        b.Append('s');
        b.Append('c');
        b.Append('r');
        b.Append('i');
        b.Append('p');
        b.Append('t');
        b.Append('>');
        Assert.AreEqual(b.GetData().String, "<script>");
        b.Clear();

        b.Append('<');
        b.Append('!');
        b.Append('-');
        b.Append('-');
        b.Append('.');
        b.Append('.');
        b.Append('.');
        Assert.AreEqual(b.GetData().String, "<!--...");
        b.Clear();

        b.Append('s');
        b.Append('c');
        b.Append('r');
        b.Append('i');
        b.Append('p');
        b.Append('t');
        b.Insert(0, '<');
        b.Insert(1, '/');
        b.Append('>');
        Assert.AreEqual("</script>", b.GetData().String);
        b.Clear();

        b.Append('b');
        b.Append('o');
        b.Append('d');
        b.Append('y');
        b.Insert(0, '<');
        b.Insert(1, '/');
        b.Append('>');
        Assert.AreEqual("</body>", b.GetData().String);
        b.Clear();

        b.Append('n');
        b.Append('o');
        b.Append('f');
        b.Append('r');
        b.Append('a');
        b.Append('m');
        b.Append('e');
        b.Append('s');
        Assert.AreEqual(b.GetData().String, "noframes");
        b.Clear();

        b.Append('h');
        b.Append('t');
        b.Append('m');
        b.Append('l');
        Assert.AreEqual(b.GetData().String, "html");
        b.Clear();

        b.Append('<');
        b.Append('s');
        b.Append('c');
        b.Append('r');
        b.Append('i');
        b.Append('p');
        b.Append('t');
        b.Append('>');
        Assert.AreEqual(b.GetData().String, "<script>");
        b.Clear();

        b.Append('<');
        b.Append('!');
        b.Append('-');
        b.Append('-');
        b.Append('.');
        b.Append('.');
        b.Append('.');
        Assert.AreEqual(b.GetData().String, "<!--...");
        b.Clear();

        b.Append('s');
        b.Append('c');
        b.Append('r');
        b.Append('i');
        b.Append('p');
        b.Append('t');
        b.Insert(0, '<');
        b.Insert(1, '/');
        b.Append('>');
        Assert.AreEqual("</script>", b.GetData().String);
        b.Clear();

        b.Append('b');
        b.Append('o');
        b.Append('d');
        b.Append('y');
        b.Insert(0, '<');
        b.Insert(1, '/');
        b.Append('>');
        Assert.AreEqual("</body>", b.GetData().String);
        b.Clear();

        b.Append('n');
        b.Append('o');
        b.Append('f');
        b.Append('r');
        b.Append('a');
        b.Append('m');
        b.Append('e');
        b.Append('s');
        Assert.AreEqual(b.GetData().String, "noframes");
        b.Clear();

        b.Append('h');
        b.Append('t');
        b.Append('m');
        b.Append('l');
        Assert.AreEqual(b.GetData().String, "html");
        b.Clear();
    }
}