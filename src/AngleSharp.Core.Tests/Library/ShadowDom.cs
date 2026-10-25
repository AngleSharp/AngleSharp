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

        private static Task<IDocument> ParseAsync(string body) =>
            BrowsingContext.New().OpenAsync(response => response.Address("http://localhost").Content("<!doctype html>" + body));

        [Test]
        public async Task DeclarativeShadowRootIsAttachedToTheParent()
        {
            var document = await ParseAsync("<div id=host><template shadowrootmode=\"open\"><span id=inner>In</span><slot></slot></template><p>Light</p></div>");
            var host = document.QuerySelector("#host");

            Assert.IsNotNull(host.ShadowRoot);
            Assert.AreEqual(ShadowRootMode.Open, host.ShadowRoot.Mode);
            Assert.AreEqual("span", host.ShadowRoot.FirstElementChild?.LocalName);
            Assert.IsNull(host.QuerySelector("template"));
            Assert.AreEqual(1, host.Children.Length);
            Assert.AreEqual("p", host.Children[0].LocalName);
        }

        [Test]
        public async Task DeclarativeShadowRootCanBeClosed()
        {
            var document = await ParseAsync("<custom-element id=host><template shadowrootmode=\"closed\"><b>In</b></template></custom-element>");
            var host = document.QuerySelector("#host");

            Assert.IsNotNull(host.ShadowRoot);
            Assert.AreEqual(ShadowRootMode.Closed, host.ShadowRoot.Mode);
        }

        [TestCase("<div id=host><template shadowrootmode=\"invalid\"><b>In</b></template></div>")]
        [TestCase("<div id=host><template><b>In</b></template></div>")]
        [TestCase("<button id=host><template shadowrootmode=\"open\"><b>In</b></template></button>")]
        public async Task OtherTemplatesStayTemplates(string body)
        {
            var document = await ParseAsync(body);
            var host = document.QuerySelector("#host");

            Assert.IsNull(host.ShadowRoot);
            Assert.IsInstanceOf<IHtmlTemplateElement>(host.FirstElementChild);
        }

        [Test]
        public async Task OnlyTheFirstDeclarativeShadowRootIsAttached()
        {
            var document = await ParseAsync("<div id=host><template shadowrootmode=\"open\"><b>First</b></template><template shadowrootmode=\"open\"><i>Second</i></template></div>");
            var host = document.QuerySelector("#host");

            Assert.AreEqual("First", host.ShadowRoot.TextContent);
            Assert.IsInstanceOf<IHtmlTemplateElement>(host.FirstElementChild);
        }

        [Test]
        public async Task FragmentParsingDoesNotAttachDeclarativeShadowRoots()
        {
            var document = await ParseAsync("<div id=host></div>");
            var host = document.QuerySelector("#host");

            host.InnerHtml = "<template shadowrootmode=\"open\"><b>In</b></template>";

            Assert.IsNull(host.ShadowRoot);
            Assert.IsInstanceOf<IHtmlTemplateElement>(host.FirstElementChild);
        }

        [TestCase(":host", "#host", true)]
        [TestCase(":host(.a)", "#host", true)]
        [TestCase(":host(.x)", "#host", false)]
        [TestCase(":host(.a) b", "b", true)]
        [TestCase(":host(.x) b", "b", false)]
        [TestCase(":host > i", "i", true)]
        [TestCase(":host > b", "b", false)]
        [TestCase(":host(:not([loaded])) b", "b", true)]
        [TestCase("div b", "b", false)]
        [TestCase("body :host b", "b", false)]
        [TestCase("i b", "b", true)]
        public async Task ShadowTreeSelectorsReachTheHostOnlyThroughHost(string selector, string target, bool expected)
        {
            var document = await ParseAsync("<div id=host class=a><template shadowrootmode=\"open\"><i><b>In</b></i></template></div>");
            var host = document.QuerySelector("#host");
            var element = target == "#host" ? host : host.ShadowRoot.QuerySelector(target);
            var parser = document.Context.GetService<AngleSharp.Css.Parser.ICssSelectorParser>();

            Assert.AreEqual(expected, parser.ParseSelector(selector).Match(element, host));
        }
    }
}
