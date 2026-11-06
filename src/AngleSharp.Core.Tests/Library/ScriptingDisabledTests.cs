namespace AngleSharp.Core.Tests.Library;

using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://html.spec.whatwg.org/multipage/scripting.html#prepare-the-script-element
/// A document with scripting disabled, e.g., the one of a DOMParser, marks its scripts as
/// started without running them. The script service of the context does not change that.
/// </summary>
[TestFixture]
public class ScriptingDisabledTests
{
    private const String Script = "<!doctype html><body><script>//...</script>";

    private static IBrowsingContext CreateContext(Action onRun) =>
        BrowsingContext.New(Configuration.Default.WithScripts(
            new CallbackScriptEngine(_ => onRun(), MimeTypeNames.DefaultJavaScript)));

    private static IHtmlDocument ParseWithoutScripting(IBrowsingContext context, String source) =>
        new HtmlParser(new HtmlParserOptions { IsScripting = false }, context).ParseDocument(source);

    private static void InsertScript(IDocument document)
    {
        var script = document.CreateElement<IHtmlScriptElement>();
        script.Text = "//...";
        document.Body.AppendChild(script);
    }

    [Test]
    public void ParserWithScriptingRunsScript()
    {
        var runs = 0;
        var context = CreateContext(() => runs++);
        new HtmlParser(new HtmlParserOptions { IsScripting = true }, context).ParseDocument(Script);
        Assert.AreEqual(1, runs);
    }

    [Test]
    public void ParserWithoutScriptingDoesNotRunScript()
    {
        var runs = 0;
        var document = ParseWithoutScripting(CreateContext(() => runs++), Script);
        Assert.AreEqual(0, runs);
        Assert.AreEqual("//...", document.Scripts[0].Text);
    }

    [Test]
    public async Task AsyncParserWithoutScriptingDoesNotRunScript()
    {
        var runs = 0;
        var parser = new HtmlParser(new HtmlParserOptions { IsScripting = false }, CreateContext(() => runs++));
        await parser.ParseDocumentAsync(Script);
        Assert.AreEqual(0, runs);
    }

    [Test]
    public void ParserWithoutScriptingDoesNotFetchExternalScript()
    {
        var requests = 0;
        var context = BrowsingContext.New(Configuration.Default
            .WithScripts(new CallbackScriptEngine(_ => { }, MimeTypeNames.DefaultJavaScript))
            .WithMockRequester(_ => requests++));
        ParseWithoutScripting(context, "<!doctype html><script src='https://example.com/a.js'></script>");
        Assert.AreEqual(0, requests);
    }

    [Test]
    public void ScriptInsertedIntoDocumentWithScriptingRuns()
    {
        var runs = 0;
        var context = CreateContext(() => runs++);
        var document = new HtmlParser(new HtmlParserOptions { IsScripting = true }, context).ParseDocument("<!doctype html><body>");
        InsertScript(document);
        Assert.AreEqual(1, runs);
    }

    [Test]
    public void ScriptInsertedAfterParsingWithoutScriptingDoesNotRun()
    {
        var runs = 0;
        var document = ParseWithoutScripting(CreateContext(() => runs++), "<!doctype html><body>");
        InsertScript(document);
        Assert.AreEqual(0, runs);
    }

    [Test]
    public void ScriptInsertedIntoClonedDocumentWithoutScriptingDoesNotRun()
    {
        var runs = 0;
        var document = ParseWithoutScripting(CreateContext(() => runs++), "<!doctype html><body>");
        InsertScript((IDocument)document.Clone());
        Assert.AreEqual(0, runs);
    }

    [Test]
    public void ParsedScriptMovedToDocumentWithScriptingDoesNotRun()
    {
        var runs = 0;
        var context = CreateContext(() => runs++);
        var source = ParseWithoutScripting(context, Script);
        var target = new HtmlParser(new HtmlParserOptions { IsScripting = true }, context).ParseDocument("<!doctype html><body>");
        target.Body.AppendChild(target.Adopt(source.Scripts[0]));
        Assert.AreEqual(0, runs);
    }

    [Test]
    public void NoScriptIsMarkupWithoutScripting()
    {
        var document = ParseWithoutScripting(CreateContext(() => { }), "<!doctype html><body><noscript><b>x</b></noscript>");
        Assert.IsNotNull(document.QuerySelector("noscript > b"));
    }

    [Test]
    public void InnerHtmlParsesNoScriptAsMarkupWithoutScripting()
    {
        var document = ParseWithoutScripting(CreateContext(() => { }), "<!doctype html><body>");
        document.Body.InnerHtml = "<noscript><b>x</b></noscript>";
        Assert.IsNotNull(document.QuerySelector("noscript > b"));
    }

    [Test]
    public void InnerHtmlParsesNoScriptAsTextWithScripting()
    {
        var context = CreateContext(() => { });
        var document = new HtmlParser(new HtmlParserOptions { IsScripting = true }, context).ParseDocument("<!doctype html><body>");
        document.Body.InnerHtml = "<noscript><b>x</b></noscript>";
        Assert.IsNull(document.QuerySelector("noscript > b"));
    }

    private static Task<IDocument> CreateFromFactory(IBrowsingContext context, Boolean disabled)
    {
        var response = new DefaultResponse
        {
            Address = new Url("https://example.com/page"),
            Content = new MemoryStream(Encoding.UTF8.GetBytes(Script)),
            Headers = new Dictionary<String, String> { { HeaderNames.ContentType, MimeTypeNames.Html } },
        };
        var options = new CreateDocumentOptions(response) { IsScriptingDisabled = disabled };
        return context.GetService<IDocumentFactory>().CreateAsync(context, options, default);
    }

    [Test]
    public async Task DocumentFactoryRunsScriptByDefault()
    {
        var runs = 0;
        await CreateFromFactory(CreateContext(() => runs++), false);
        Assert.AreEqual(1, runs);
    }

    [Test]
    public async Task DocumentFactoryWithScriptingDisabledDoesNotRunScript()
    {
        var runs = 0;
        var context = CreateContext(() => runs++);
        var document = await CreateFromFactory(context, true);
        InsertScript(document);
        Assert.AreEqual(0, runs);
        Assert.AreEqual("https://example.com/page", document.Url);
        Assert.AreEqual(2, document.Scripts.Length);
    }
}
