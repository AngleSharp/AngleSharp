namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Linq;

    [TestFixture]
    public class ShadowDomTests
    {
        private static Task<IDocument> CreateStandardDom()
        {
            var source = @"<!doctype html>
<div id=host>
    <div slot=example>Example</div>
    <span>Other</span>
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

        [Test]
        public async Task DefaultSlotOfShadowRootShouldContainUnassignedNodes()
        {
            var document = await CreateStandardDom();
            var shadowRoot = document.QuerySelector("#host").AttachShadow(mode: ShadowRootMode.Open);
            var slot = document.CreateElement("slot") as IHtmlSlotElement;
            Assert.IsNotNull(slot);
            shadowRoot.AppendChild(slot);
            Assert.AreEqual(1, shadowRoot.ChildElementCount);
            var nodes = slot.GetDistributedNodes().ToArray();
            Assert.AreEqual(4, nodes.Length);
            Assert.AreEqual(NodeType.Text, nodes[0].NodeType);
            Assert.AreEqual(NodeType.Text, nodes[1].NodeType);
            Assert.AreEqual(NodeType.Element, nodes[2].NodeType);
            Assert.AreEqual(NodeType.Text, nodes[3].NodeType);
            Assert.AreEqual(document.QuerySelector("#host > span"), nodes[2]);
        }

        [Test]
        public async Task NamedSlotOfShadowRootShouldNotContainNoAssignedNodes()
        {
            var document = await CreateStandardDom();
            var shadowRoot = document.QuerySelector("#host").AttachShadow(mode: ShadowRootMode.Open);
            var slot = document.CreateElement("slot") as IHtmlSlotElement;
            Assert.IsNotNull(slot);
            shadowRoot.AppendChild(slot);
            slot.Name = "other";
            Assert.AreEqual(1, shadowRoot.ChildElementCount);
            var nodes = slot.GetDistributedNodes().ToArray();
            Assert.AreEqual(0, nodes.Length);
        }

        [Test]
        public async Task NamedSlotOfShadowRootShouldContainAssignedNodes()
        {
            var document = await CreateStandardDom();
            var shadowRoot = document.QuerySelector("#host").AttachShadow(mode: ShadowRootMode.Open);
            var slot = document.CreateElement("slot") as IHtmlSlotElement;
            Assert.IsNotNull(slot);
            shadowRoot.AppendChild(slot);
            slot.Name = "example";
            Assert.AreEqual(1, shadowRoot.ChildElementCount);
            var nodes = slot.GetDistributedNodes().ToArray();
            Assert.AreEqual(1, nodes.Length);
            Assert.AreEqual(document.QuerySelector("#host > div"), nodes[0]);
        }
    }
}
