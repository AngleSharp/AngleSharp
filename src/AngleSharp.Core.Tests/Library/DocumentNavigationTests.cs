namespace AngleSharp.Core.Tests.Library;

using AngleSharp.Browser.Dom;
using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Dom;
using AngleSharp.Io;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A document created next to the one being shown, e.g., by a DOMParser, is no navigation.
/// </summary>
[TestFixture]
public class DocumentNavigationTests
{
    private const String Address = "https://example.com/other";

    private sealed class CountingHistory : IHistory
    {
        public Int32 Length { get; private set; }
        public Int32 Index => Length - 1;
        public IDocument this[Int32 index] => null;
        public Object State => null;
        public void Go(Int32 delta = 0) { }
        public void Back() { }
        public void Forward() { }
        public void PushState(Object data, String title, String url = null) => Length++;
        public void ReplaceState(Object data, String title, String url = null) { }
    }

    private static IBrowsingContext CreateContext() =>
        BrowsingContext.New(Configuration.Default.With(new CountingHistory()));

    private static Task<IDocument> Create(IBrowsingContext context, String content, String type, Boolean disabled)
    {
        var response = new DefaultResponse
        {
            Address = new Url(Address),
            Content = new MemoryStream(Encoding.UTF8.GetBytes(content)),
            Headers = new Dictionary<String, String> { { HeaderNames.ContentType, type } },
        };
        var options = new CreateDocumentOptions(response) { IsNavigationDisabled = disabled };
        return context.GetService<IDocumentFactory>().CreateAsync(context, options, default);
    }

    [TestCase("text/html")]
    [TestCase("text/plain")]
    public async Task CreatingDocumentNavigatesByDefault(String type)
    {
        var context = CreateContext();
        var live = await context.OpenAsync(m => m.Content("live"));
        var length = context.SessionHistory.Length;
        var document = await Create(context, "other", type, false);
        Assert.AreSame(document, context.Active);
        Assert.AreEqual(length + 1, context.SessionHistory.Length);
        Assert.AreNotSame(live, document);
    }

    [TestCase("text/html")]
    [TestCase("text/plain")]
    public async Task CreatingDocumentWithNavigationDisabledKeepsActiveDocumentAndHistory(String type)
    {
        var context = CreateContext();
        var live = await context.OpenAsync(m => m.Content("live"));
        var length = context.SessionHistory.Length;
        var document = await Create(context, "other", type, true);
        Assert.AreSame(live, context.Active);
        Assert.AreSame(live.DefaultView, context.Current);
        Assert.AreEqual(length, context.SessionHistory.Length);
        Assert.IsTrue(live.HasFocus());
        Assert.IsFalse(document.HasFocus());
        Assert.AreEqual(Address, document.Url);
        Assert.AreSame(context, document.Context);
        Assert.AreEqual("other", document.Body.TextContent);
    }

    [Test]
    public async Task CreatingDocumentWithNavigationDisabledLeavesEmptyContextEmpty()
    {
        var context = BrowsingContext.New(Configuration.Default);
        var document = await Create(context, "<p>other", MimeTypeNames.Html, true);
        Assert.IsNull(context.Active);
        Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
        Assert.AreEqual("other", document.Body.TextContent);
    }

    [Test]
    public async Task NavigationDisabledDoesNotDisableScripting()
    {
        var runs = 0;
        var scripting = new CallbackScriptEngine(_ => runs++, MimeTypeNames.DefaultJavaScript);
        var context = BrowsingContext.New(Configuration.Default.WithScripts(scripting));
        await Create(context, "<script>//...</script>", MimeTypeNames.Html, true);
        Assert.AreEqual(1, runs);
    }

    [Test]
    public async Task ContextNavigatesAsUsualAfterDocumentWithNavigationDisabled()
    {
        var context = BrowsingContext.New(Configuration.Default);
        await context.OpenAsync(m => m.Content("live"));
        await Create(context, "other", MimeTypeNames.Html, true);
        var next = await context.OpenAsync(m => m.Content("next"));
        Assert.AreSame(next, context.Active);
    }
}
