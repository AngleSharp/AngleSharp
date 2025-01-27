namespace AngleSharp.Core.Tests.Html;

using AngleSharp.Html.Dom;
using AngleSharp.Io;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

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
                // language=html
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
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
                // language=html
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/iframe.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <iframe id="inner-iframe" name="inner-iframe"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
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
    public async Task AnchorTargetsIframeNestedInUnnamedIframeInDocument()
    {
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/inner-iframe.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/iframe.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <iframe id="inner-iframe" name="inner-iframe"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <iframe id="iframe" src="iframe.html"></iframe>
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
                // language=html
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/source-iframe.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <a href="inner-iframe.html" id="link" target="inner-iframe">Load frame</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/target-iframe.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <iframe id="inner-iframe" name="inner-iframe"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
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
                // language=html
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/new-window.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <a href="inner-iframe.html" id="frame-link" target="iframe">Load frame</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
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
                // language=html
                """
                <html>
                <body>
                    <h1>iframe content</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/iframe.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <a href="inner-iframe.html" id="frame-link" target="_top">Load frame</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/new-window.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                    <iframe id="iframe" name="iframe" src="iframe.html"></iframe>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
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
                // language=html
                """
                <html>
                <body>
                  <h1>Next Page</h1>
                </body>
                </html>
                """u8
            ),
            "https://localhost/new-window.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                <body>
                  <a id="window-link" href="next-page.html" target="_parent">Load next page</a>
                </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
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

    // I have not been able to find any standard which dictates what is supposed to happen
    // when multiple iframes within a single root browsing context have the same name as the
    // target of a navigation element.  Testing with a FireFox based browser, the behavior
    // appears to be a depth first search in the current browsing context of iframes in
    // document order until one is found, and, if not, then the same search is repeated
    // in the parent browsing context, or opened as a new window if none was found within
    // the root browsing context.

    [Test]
    public async Task AnchorTargetsFirstAvailableIframeWhenNamesCollide()
    {
        // In this test, with two iframes on the page, when only the second iframe had a nested iframe
        // named 'destination,' a link targeting the 'destination' iframe opens in the nested iframe
        // within the second iframe.  However, on the same page, if the first iframe has a nested iframe
        // named 'destination' loaded in, even after the initial page load, or even after the nested
        // within the second iframe has loaded, the link will target within the first iframe.
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/destination.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <h2>Page here</h2>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/frame-1.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='destination' name='destination'></iframe>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/frame-2.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='destination' name='destination'></iframe>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='frame-1' name='frame-1'></iframe>
                    <iframe id='frame-2' src='frame-2.html'></iframe>
                    <a id='load-frame-1' href='frame-1.html' target='frame-1'>Load frame 1</a>
                    <a id='link' href='destination.html' target='destination'>Open link</a>
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

        var iframe2 = doc.GetElementById("frame-2") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe2?.ContentDocument);

        var iframe2Destination = iframe2.ContentDocument.GetElementById("destination") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe2Destination?.ContentDocument);

        var iframe2DestinationHeaders = iframe2Destination.ContentDocument.GetElementsByTagName("h2");
        Assert.IsNotNull(iframe2DestinationHeaders);
        Assert.AreEqual(1, iframe2DestinationHeaders.Length);
        Assert.AreEqual("Page here", iframe2DestinationHeaders[0].InnerHtml);

        var loadIframe1Link = doc.GetElementById("load-frame-1") as IHtmlAnchorElement;
        Assert.IsNotNull(loadIframe1Link);
        loadIframe1Link.DoClick();

        await Task.Delay(1000);
        link.DoClick();

        await Task.Delay(1000);

        var iframe1 = doc.GetElementById("frame-1") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe1?.ContentDocument);

        var iframe1Destination = iframe1.ContentDocument.GetElementById("destination") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe1Destination?.ContentDocument);

        var iframe1DestinationHeaders = iframe1Destination.ContentDocument.GetElementsByTagName("h2");
        Assert.IsNotNull(iframe1DestinationHeaders);
        Assert.AreEqual(1, iframe1DestinationHeaders.Length);
        Assert.AreEqual("Page here", iframe1DestinationHeaders[0].InnerHtml);
    }

    [Test]
    public async Task AnchorTargetsOnlyFirstAvailableIframeWhenNamesCollide()
    {
        // In this test, with two iframes on the page, when both contain a nested iframe named 'destination',
        // a link targeting the 'destination' iframe opens within the first iframe, and ignores the second
        // iframe.
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/destination.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <h2>Page here</h2>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/frame-1.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='destination' name='destination'></iframe>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/frame-2.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='destination' name='destination'></iframe>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='frame-1' src='frame-1.html'></iframe>
                    <iframe id='frame-2' src='frame-2.html'></iframe>
                    <a id='link' href='destination.html' target='destination'>Open link</a>
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

        var iframe1 = doc.GetElementById("frame-1") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe1?.ContentDocument);

        var iframe1Destination = iframe1.ContentDocument.GetElementById("destination") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe1Destination?.ContentDocument);

        var iframe1DestinationHeaders = iframe1Destination.ContentDocument.GetElementsByTagName("h2");
        Assert.IsNotNull(iframe1DestinationHeaders);
        Assert.AreEqual(1, iframe1DestinationHeaders.Length);
        Assert.AreEqual("Page here", iframe1DestinationHeaders[0].InnerHtml);

        var iframe2 = doc.GetElementById("frame-2") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe2?.ContentDocument);

        var iframe2Destination = iframe2.ContentDocument.GetElementById("destination") as IHtmlInlineFrameElement;
        if (iframe2Destination?.ContentDocument is null)
        {
            Assert.Pass("iframe-2 content document is null");
        }

        var iframe2DestinationHeaders = iframe2Destination.ContentDocument.GetElementsByTagName("h2");
        Assert.That(iframe2DestinationHeaders, Is.Null.Or.Empty);
    }

    [Test]
    public async Task AnchorTargetsFirstLoadedIframeWhenNamesCollide()
    {
        // In this test, with one iframe on the page at load, and one more iframe inserted to an earlier
        // position after load, with both having a nested iframe named 'destination', a link targeting
        // 'destination' opens within the iframe that was loaded into the document first, regardless
        // of document order.
        var context = BrowsingContext.New(Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
        {
            "https://localhost/destination.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <h2>Page here</h2>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/frame-1.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='destination' name='destination'></iframe>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/frame-2.html" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='destination' name='destination'></iframe>
                  </body>
                </html>
                """u8
            ),
            "https://localhost/" => CreateResponse(req.Address,
                // language=html
                """
                <html>
                  <body>
                    <iframe id='frame-2' src='frame-2.html'></iframe>
                    <a id='link' href='destination.html' target='destination'>Open link</a>
                  </body>
                </html>
                """u8
            ),
            _ => CreateNotFoundResponse(req.Address),
        }));

        var doc = await context.OpenAsync("https://localhost/");
        var iframe1 = doc.CreateElement("iframe") as IHtmlInlineFrameElement;
        iframe1.Id = "frame-1";
        iframe1.Source = "frame-1.html";
        var iframe2 = doc.GetElementById("frame-2") as IHtmlInlineFrameElement;
        doc.Body.InsertBefore(iframe1, iframe2);

        var link = doc.GetElementById("link") as IHtmlAnchorElement;
        Assert.IsNotNull(link);
        link.DoClick();

        await Task.Delay(1000);

        Assert.IsNotNull(iframe2?.ContentDocument);

        var iframe2Destination = iframe2.ContentDocument.GetElementById("destination") as IHtmlInlineFrameElement;
        Assert.IsNotNull(iframe2Destination?.ContentDocument);

        var iframe2DestinationHeaders = iframe2Destination.ContentDocument.GetElementsByTagName("h2");
        Assert.IsNotNull(iframe2DestinationHeaders);
        Assert.AreEqual(1, iframe2DestinationHeaders.Length);
        Assert.AreEqual("Page here", iframe2DestinationHeaders[0].InnerHtml);

        Assert.IsNotNull(iframe2?.ContentDocument);

        var iframe1Destination = iframe1.ContentDocument.GetElementById("destination") as IHtmlInlineFrameElement;
        if (iframe1Destination?.ContentDocument is null)
        {
            Assert.Pass("iframe-1 content document is null");
        }

        var iframe1DestinationHeaders = iframe1Destination.ContentDocument.GetElementsByTagName("h2");
        Assert.That(iframe1DestinationHeaders, Is.Null.Or.Empty);
    }
}
