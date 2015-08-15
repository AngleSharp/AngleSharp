namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class ShadowDomTests
    {
        static Task<IDocument> CreateStandardDom()
        {
            var source = @"<!doctype html>
<div id=host>
    <div slot=example>Example</div>
    <span>Other</div>
</div>";
            return BrowsingContext.New().OpenAsync(response =>
                response.Address("http://localhost").
                         Content(source));
        }

        [Test]
        public async Task AttachShadowTreeToOrdinaryElement()
        {
            var document = await CreateStandardDom();
            var shadowRoot = document.QuerySelector("#host").AttachShadow(mode: ShadowRootMode.Open);
            Assert.AreEqual(shadowRoot, document.QuerySelector("#host").ShadowRoot);
        }

        [Test]
        public async Task AppendChildrenToAttachedShadowRoot()
        {
            var document = await CreateStandardDom();
            var shadowRoot = document.QuerySelector("#host").AttachShadow(mode: ShadowRootMode.Open);
            var div = document.CreateElement("div");
            var span = document.CreateElement("span");
            shadowRoot.AppendChild(div);
            shadowRoot.AppendChild(span);
            Assert.AreEqual(2, shadowRoot.ChildElementCount);
        }
    }
}
