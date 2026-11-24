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
            // the contents of templates belong to the inert document of the target (HTML 4.12.3)
            var contents = target.CreateElement<IHtmlTemplateElement>().Content.Owner;

            Assert.AreNotSame(target, contents);
            Assert.AreSame(target, adopted.Owner);
            Assert.AreSame(target, outer.Owner);
            Assert.AreSame(contents, outer.Content.Owner);
            Assert.AreSame(contents, outer.Content.QuerySelector("input").Owner);
            Assert.AreSame(contents, inner.Owner);
            Assert.AreSame(contents, inner.Content.Owner);
            Assert.AreSame(contents, inner.Content.QuerySelector("span").Owner);
            Assert.AreEqual("Nested", inner.Content.TextContent);
        }
    }
}
