namespace AngleSharp.Core.Tests.Html;

using System;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using NUnit.Framework;

[TestFixture]
public sealed class CustomTreeWhitespaceTests
{
    private const String Source =
        "<html><body><span>Hello</span> <span>world</span></body></html>";

    [Test]
    public void CustomTreeDropsWhitespaceOnlyTextNodesByDefault()
    {
        var context = BrowsingContext.New(Configuration.Default);
        var parser = new HtmlParser(new HtmlParserOptions(), context);

        using var document = parser.ParseDocument<Document, Element>(new TextSource(Source));

        Assert.That(document.Body.ChildNodes.Length, Is.EqualTo(2));
        Assert.That(document.Body.TextContent, Is.EqualTo("Helloworld"));
    }

    [Test]
    public void CustomTreeKeepsWhitespaceOnlyTextNodesWhenRequested()
    {
        var options = new HtmlParserOptions { IsKeepingWhitespaceTextNodes = true };
        var context = BrowsingContext.New(Configuration.Default);
        var parser = new HtmlParser(options, context);

        using var document = parser.ParseDocument<Document, Element>(new TextSource(Source));

        Assert.That(document.Body.ChildNodes.Length, Is.EqualTo(3));
        Assert.That(document.Body.ChildNodes[1].NodeType, Is.EqualTo(NodeType.Text));
        Assert.That(document.Body.TextContent, Is.EqualTo("Hello world"));
    }

    [TestCase(false)]
    [TestCase(true)]
    public void StandardDomAlwaysKeepsWhitespaceOnlyTextNodes(Boolean keepWhitespace)
    {
        var options = new HtmlParserOptions { IsKeepingWhitespaceTextNodes = keepWhitespace };
        var parser = new HtmlParser(options);

        using var document = parser.ParseDocument(Source);

        Assert.That(document.Body.ChildNodes.Length, Is.EqualTo(3));
        Assert.That(document.Body.ChildNodes[1].NodeType, Is.EqualTo(NodeType.Text));
        Assert.That(document.Body.TextContent, Is.EqualTo("Hello world"));
    }
}
