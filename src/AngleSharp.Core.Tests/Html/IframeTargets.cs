namespace AngleSharp.Core.Tests.Html;

using AngleSharp.Html.Dom;
using AngleSharp.Io;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

file static class HtmlExtensions
{
    public static String ToHtmlDataUri(this String html)
    {
        return $"data:text/html;charset=UTF-8;base64,{Convert.ToBase64String(Encoding.UTF8.GetBytes(html))}";
    }
}

[TestFixture]
public class IframeTargets
{
    private static DefaultResponse CreateResponse(Dom.Url address, ReadOnlySpan<Byte> content)
    {
        return new()
        {
            Address = address,
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new MemoryStream(content.ToArray()),
        };
    }

    private static DefaultResponse CreateNotFoundResponse(Dom.Url address)
    {
        return new()
        {
            Address = address,
            StatusCode = System.Net.HttpStatusCode.NotFound,
        };
    }

    [Test]
    public async Task AnchorTargetsIframeInDocument()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="iframe" name="iframe"></iframe>
                    <a href="iframe.html" id="link" target="iframe">Load frame</a>
                </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");
        var link = doc.GetElementById("link") as IHtmlAnchorElement;
        Assert.IsNotNull(link);
        link.DoClick();

        await Task.Delay(1000);

        var iframe = doc.GetElementById("iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe?.ContentDocument);

        var headerTexts = iframe.ContentDocument.GetElementsByTagName("h1");
        Assert.IsNotNull(headerTexts);
        Assert.AreEqual(1, headerTexts.Length);
        Assert.AreEqual("iframe content", headerTexts[0].InnerHtml);
    }

    [Test]
    public async Task AnchorTargetsNestedIframeInDocument()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/inner-iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="inner-iframe" name="inner-iframe"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="iframe" name="iframe" src="iframe.html"></iframe>
                    <a href="inner-iframe.html" id="link" target="inner-iframe">Load frame</a>
                </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");
        var link = doc.GetElementById("link") as IHtmlAnchorElement;
        Assert.IsNotNull(link);
        link.DoClick();

        await Task.Delay(1000);

        var iframe = doc.GetElementById("iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe?.ContentDocument);

        var innerIframe = iframe.ContentDocument.GetElementById("inner-iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(innerIframe?.ContentDocument);

        var headerTexts = innerIframe.ContentDocument.GetElementsByTagName("h1");
        Assert.IsNotNull(headerTexts);
        Assert.AreEqual(1, headerTexts.Length);
        Assert.AreEqual("iframe content", headerTexts[0].InnerHtml);
    }

    [Test]
    public async Task AnchorTargetsSiblingIframeInIframe()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/inner-iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/source-iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <a href="inner-iframe.html" id="link" target="inner-iframe">Load frame</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/target-iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="inner-iframe" name="inner-iframe"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="source-iframe" name="source-iframe" src="source-iframe.html"></iframe>
                    <iframe id="target-iframe" name="target-iframe" src="target-iframe.html"></iframe>
                </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");

        var sourceIframe = doc.GetElementById("source-iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(sourceIframe?.ContentDocument);

        var link = sourceIframe.ContentDocument.GetElementById("link") as IHtmlAnchorElement;
        Assert.IsNotNull(link);
        link.DoClick();

        await Task.Delay(1000);

        var targetIframe = doc.GetElementById("target-iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(targetIframe?.ContentDocument);

        var innerIframe = targetIframe.ContentDocument.GetElementById("inner-iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(innerIframe?.ContentDocument);

        var headerTexts = innerIframe.ContentDocument.GetElementsByTagName("h1");
        Assert.IsNotNull(headerTexts);
        Assert.AreEqual(1, headerTexts.Length);
        Assert.AreEqual("iframe content", headerTexts[0].InnerHtml);
    }

    [Test]
    public async Task AnchorTargetsIframeInDifferentWindow()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/inner-iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/new-window.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <a href="inner-iframe.html" id="frame-link" target="iframe">Load frame</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="iframe" name="iframe"></iframe>
                    <a href="new-window.html" id="window-link" target="new-window">Load window</a>
                </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");

        var newWindowLink = doc.GetElementById("window-link") as IHtmlAnchorElement;
        Assert.IsNotNull(newWindowLink);
        newWindowLink.DoClick();

        await Task.Delay(1000);

        var newWindowDoc = context.FindChild("new-window")?.Active;
        Assert.IsNotNull(newWindowDoc);

        var frameLink = newWindowDoc.GetElementById("frame-link") as IHtmlAnchorElement;
        Assert.IsNotNull(frameLink);
        frameLink.DoClick();

        await Task.Delay(1000);

        var iframe = doc.GetElementById("iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe?.ContentDocument);

        var headerTexts = iframe.ContentDocument.GetElementsByTagName("h1");
        Assert.IsNotNull(headerTexts);
        Assert.AreEqual(1, headerTexts.Length);
        Assert.AreEqual("iframe content", headerTexts[0].InnerHtml);
    }

    [Test]
    public async Task AnchorTargetsCurrentWindowWithTop()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/inner-iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/iframe.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <a href="inner-iframe.html" id="frame-link" target="_top">Load frame</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/new-window.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <iframe id="iframe" name="iframe" src="iframe.html"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                """
                <html>
                <body>
                    <a href="new-window.html" id="window-link" target="new-window">Load window</a>
                </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");

        var newWindowLink = doc.GetElementById("window-link") as IHtmlAnchorElement;
        Assert.IsNotNull(newWindowLink);
        newWindowLink.DoClick();

        await Task.Delay(1000);

        var newWindow = context.FindChild("new-window");
        Assert.IsNotNull(newWindow);

        var iframe = newWindow.Active.GetElementById("iframe") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe?.ContentDocument);

        var frameLink = iframe.ContentDocument.GetElementById("frame-link") as IHtmlAnchorElement;
        Assert.IsNotNull(frameLink);
        frameLink.DoClick();

        await Task.Delay(1000);

        Assert.IsNotNull(newWindow.Active);

        // Make sure the new window has navigated after clicking the frame link
        var headerTexts = newWindow.Active.GetElementsByTagName("h1");
        Assert.IsNotNull(headerTexts);
        Assert.AreEqual(1, headerTexts.Length);
        Assert.AreEqual("iframe content", headerTexts[0].InnerHtml);

        // Make sure the original window has not navigated after clicking the frame link
        Assert.AreEqual("Load window", context.Active?.GetElementById("window-link")?.InnerHtml);
    }
    [Test]
    public async Task AnchorTargetsSelfAsParentInNewWindow()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/next-page.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                  <h1>Next Page</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/new-window.html" => CreateResponse(req.Address,
                """
                <html>
                <body>
                  <a id="window-link" href="next-page.html" target="_parent">Load next page</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                """
                <html>
                <body>
                  <a id="link" href="new-window.html" target="new-window">Load window</a>
                </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");

        var link = doc.GetElementById("link") as IHtmlAnchorElement;
        Assert.IsNotNull(link);
        link.DoClick();

        await Task.Delay(1000);

        var newWindowContext = context.FindChild("new-window");
        Assert.IsNotNull(newWindowContext?.Active);

        var windowLink = newWindowContext.Active.GetElementById("window-link") as IHtmlAnchorElement;
        Assert.IsNotNull(windowLink);
        windowLink.DoClick();

        await Task.Delay(1000);

        var headerTexts = newWindowContext.Active.GetElementsByTagName("h1");
        Assert.IsNotNull(headerTexts);
        Assert.AreEqual(1, headerTexts.Length);
        Assert.AreEqual("Next Page", headerTexts[0].InnerHtml);

        // Make sure the original window has not navigated after clicking the frame link
        Assert.AreEqual("Load window", context.Active?.GetElementById("link")?.InnerHtml);

    }
}
