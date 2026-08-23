namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    /// <summary>
    /// The tree construction dispatcher processes a token with HTML rules when the adjusted
    /// current node is an HTML integration point. A MathML annotation-xml element is one when
    /// its encoding attribute is an ASCII case-insensitive match for text/html or for
    /// application/xhtml+xml. The html5lib-derived suites are generated from the .dat fixtures
    /// by src/TestGeneration, so these cases live here instead.
    /// </summary>
    [TestFixture]
    public class HtmlIntegrationPointTests
    {
        private static String AnnotationXmlWith(String encoding) =>
            "<math><annotation-xml encoding=\"" + encoding + "\"><style><img src=x></style></annotation-xml></math>";

        [TestCase("text/html")]
        [TestCase("TEXT/HTML")]
        [TestCase("Text/Html")]
        [TestCase("application/xhtml+xml")]
        [TestCase("APPLICATION/XHTML+XML")]
        public void AnnotationXmlWithHtmlEncodingIsAnIntegrationPoint(String encoding)
        {
            var document = AnnotationXmlWith(encoding).ToHtmlDocument();
            var style = document.All.First(m => m.LocalName == "style");

            Assert.AreEqual(NamespaceNames.HtmlUri, style.NamespaceUri);
            // In the HTML namespace style is a raw text element, so the img start tag
            // is character data and no element is constructed from it.
            Assert.IsFalse(document.All.Any(m => m.LocalName == "img"));
        }

        [TestCase("text/html; charset=utf-8")]
        [TestCase("application/xhtml+xml; charset=utf-8")]
        [TestCase("text/xml")]
        [TestCase("")]
        public void AnnotationXmlWithOtherEncodingIsNotAnIntegrationPoint(String encoding)
        {
            var document = AnnotationXmlWith(encoding).ToHtmlDocument();
            var style = document.All.First(m => m.LocalName == "style");

            // The value is matched as a literal string, not parsed as a MIME type, so a
            // parameter makes it something other than text/html.
            Assert.AreEqual(NamespaceNames.MathMlUri, style.NamespaceUri);
        }

        [Test]
        public void AnnotationXmlWithoutEncodingIsNotAnIntegrationPoint()
        {
            var document = "<math><annotation-xml><style><img src=x></style></annotation-xml></math>".ToHtmlDocument();
            var style = document.All.First(m => m.LocalName == "style");

            Assert.AreEqual(NamespaceNames.MathMlUri, style.NamespaceUri);
        }
    }
}
