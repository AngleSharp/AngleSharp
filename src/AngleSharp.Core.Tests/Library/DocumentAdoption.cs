namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DocumentAdoptionTests
    {
        [TestCase("template")]
        [TestCase("section")]
        public void AdoptingTemplatesUpdatesTheirContentDocuments(String selector)
        {
            var source = "<section><template id=outer><input><template id=inner><span>Nested</span></template></template></section>".ToHtmlDocument();
            var target = "<p>Target</p>".ToHtmlDocument();
            var outer = source.QuerySelector<IHtmlTemplateElement>("#outer");
            var inner = outer.Content.QuerySelector<IHtmlTemplateElement>("#inner");
            var adopted = source.QuerySelector(selector);

            target.AdoptNode(adopted);

            Assert.AreSame(target, adopted.Owner);
            Assert.AreSame(target, outer.Owner);
            Assert.AreSame(target, outer.Content.Owner);
            Assert.AreSame(target, outer.Content.QuerySelector("input").Owner);
            Assert.AreSame(target, inner.Owner);
            Assert.AreSame(target, inner.Content.Owner);
            Assert.AreSame(target, inner.Content.QuerySelector("span").Owner);
            Assert.AreEqual("Nested", inner.Content.TextContent);
        }
    }
}
