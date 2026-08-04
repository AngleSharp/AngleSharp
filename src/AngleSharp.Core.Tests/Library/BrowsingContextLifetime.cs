namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Browser;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    [TestFixture]
    public class BrowsingContextLifetime
    {
        private static DefaultResponse CreateResponse(Dom.Url address, String content) => new()
        {
            Address = address,
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)),
        };

        private static IConfiguration CreateConfiguration() =>
            Configuration.Default.WithVirtualRequester(req => req.Address.Href switch
            {
                "https://localhost/new-window.html" => CreateResponse(req.Address,
                    // language=html
                    "<html><body><h1>New window</h1></body></html>"),
                "https://localhost/elsewhere.html" => CreateResponse(req.Address,
                    // language=html
                    "<html><body><h1>Elsewhere</h1></body></html>"),
                "https://localhost/" => CreateResponse(req.Address,
                    // language=html
                    "<html><body><a id=\"link\" href=\"new-window.html\" target=\"new-window\">Load window</a></body></html>"),
                _ => new DefaultResponse { Address = req.Address, StatusCode = System.Net.HttpStatusCode.NotFound },
            });

        private static void Collect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        // The strong locals below have to die before the collection runs. A Debug build keeps
        // locals alive until the end of the enclosing method, so the contexts are acquired in
        // separate non-inlined helpers rather than in the test body itself.

        // Returns only a Boolean: probing with FindChild directly in a test body would leave
        // the context reachable from that frame and mask a genuine loss.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Boolean HasChild(IBrowsingContext context, String name) => context.FindChild(name) is not null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference OpenAuxiliaryContext(IBrowsingContext context)
        {
            var child = context.CreateChild("kept-open", Sandboxes.None);
            return new WeakReference(child);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference[] OpenAndCloseAuxiliaryContexts(IBrowsingContext context, Int32 count)
        {
            var references = new WeakReference[count];

            for (var i = 0; i < count; i++)
            {
                var child = context.CreateChild($"cycled-{i}", Sandboxes.None);
                references[i] = new WeakReference(child);
                child.Dispose();
            }

            return references;
        }

        [Test]
        public async Task AuxiliaryContextSurvivesCollectionWhileOpenerIsAlive()
        {
            // Regression test: the opened window used to be reachable only through weak
            // references, so a collection between opening it and looking it up made
            // FindChild return null for a window that logically still exists.
            var context = BrowsingContext.New(CreateConfiguration());
            var doc = await context.OpenAsync("https://localhost/");

            var link = doc.GetElementById("link") as IHtmlAnchorElement;
            Assert.IsNotNull(link);
            link.DoClick();

            await Task.Delay(1000);

            Collect();

            var newWindowContext = context.FindChild("new-window");
            Assert.IsNotNull(newWindowContext);
            Assert.IsNotNull(newWindowContext.Active);
            Assert.AreEqual("https://localhost/new-window.html", newWindowContext.Active.Url);

            GC.KeepAlive(doc);
        }

        [Test]
        public async Task AuxiliaryContextSurvivesTheOpenerNavigatingAway()
        {
            // A window outlives every document that loads into its opener, so navigating
            // the opener must not close it.
            var context = BrowsingContext.New(CreateConfiguration());
            var doc = await context.OpenAsync("https://localhost/");

            var link = doc.GetElementById("link") as IHtmlAnchorElement;
            Assert.IsNotNull(link);
            link.DoClick();

            await Task.Delay(1000);

            Assert.IsTrue(HasChild(context, "new-window"), "The window was not opened.");

            doc = await context.OpenAsync("https://localhost/elsewhere.html");

            Collect();

            var newWindowContext = context.FindChild("new-window");
            Assert.IsNotNull(newWindowContext, "The opened window did not survive the opener navigating away.");
            Assert.AreEqual("https://localhost/new-window.html", newWindowContext.Active.Url);

            GC.KeepAlive(doc);
        }

        [Test]
        public async Task OpenAuxiliaryContextIsRetainedByItsOpener()
        {
            var context = BrowsingContext.New(CreateConfiguration());
            var doc = await context.OpenAsync("https://localhost/");

            var reference = OpenAuxiliaryContext(context);

            Collect();

            Assert.IsTrue(reference.IsAlive, "An open auxiliary context must be kept alive by the document that opened it.");

            GC.KeepAlive(doc);
        }

        [Test]
        public async Task ClosedAuxiliaryContextsAreCollectibleAcrossRepeatedCycles()
        {
            var context = BrowsingContext.New(CreateConfiguration());
            var doc = await context.OpenAsync("https://localhost/");

            var references = OpenAndCloseAuxiliaryContexts(context, 25);

            Collect();

            var alive = references.Count(reference => reference.IsAlive);
            Assert.AreEqual(0, alive, $"{alive} of {references.Length} closed auxiliary contexts were still retained.");

            GC.KeepAlive(doc);
        }
    }
}
