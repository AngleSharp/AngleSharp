namespace AngleSharp.Core.Tests.Library;

using System.IO;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using NUnit.Framework;

[TestFixture]
public sealed class HandleTreeConstructionTests
{
    private const string Markup =
        "<table>before<tr><td><svg viewbox='0 0 1 1'><foreignObject><p>inside</p></foreignObject></svg></td></tr>after</table>";

    [Test]
    public void HandleBackendMatchesStandardTreeConstruction()
    {
        var parser = new HtmlParser();
        var factory = CreateFactory();
        using var expected = parser.ParseDocument(Markup);
        using var source = new TextSource(Markup);
        using var actual = parser.ParseDocument<Document, ConstructableDomNode>(source, factory);

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
    }

    [Test]
    public async Task HandleBackendSupportsAsynchronousStreamParsing()
    {
        var parser = new HtmlParser();
        var factory = CreateFactory();
        using var expected = parser.ParseDocument(Markup);
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(Markup));
        using var actual = await parser.ParseDocumentAsync<Document, ConstructableDomNode>(
            stream,
            HtmlStreamSourceMode.Streaming,
            factory,
            Encoding.UTF8);

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
    }

    private static ConstructableDomTreeFactory<Document, Element> CreateFactory() =>
        new(HtmlDomConstructionFactory.Instance);
}
