namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class CssSelectorLanguageTests
    {
        [TestCase("html", "en", "fr")]
        [TestCase("inherited", "fr", "en")]
        [TestCase("svg-ordinary", "it", "fr")]
        [TestCase("math-ordinary", "de", "nl")]
        [TestCase("math-xml", "es", "de")]
        public void LanguageSelectorsUseNamespacesAndInheritAcrossForeignContent(String id, String expected, String rejected)
        {
            var document = new HtmlParser().ParseDocument("""
                <!doctype html><html lang="de"><body>
                  <p id="html" lang="en" xml:lang="fr">HTML</p>
                  <svg lang="en" xml:lang="fr">
                    <text id="inherited">French</text>
                    <g lang="it"><text id="svg-ordinary">Italian</text></g>
                  </svg>
                  <math lang="nl"><mi id="math-ordinary">German</mi></math>
                  <math xml:lang="es"><mi id="math-xml">Spanish</mi></math>
                </body></html>
                """);
            var target = document.QuerySelector("#" + id)!;

            Assert.IsTrue(target.Matches(":lang(" + expected + ")"));
            Assert.IsFalse(target.Matches(":lang(" + rejected + ")"));
            Assert.IsTrue(document.QuerySelectorAll(":lang(" + expected + ")").Contains(target));
        }

        [Test]
        public void LanguageSelectorTextCanBeParsedAgain()
        {
            var parser = new CssSelectorParser();
            var selector = parser.ParseSelector(":lang(fr)")!;
            var document = new HtmlParser().ParseDocument("<!doctype html><p lang='fr'>Text</p>");

            Assert.AreEqual(":lang(fr)", selector.Text);
            Assert.IsTrue(document.QuerySelector("p")!.Matches(selector.Text));
        }

        [Test]
        public void EmptyLanguageStopsInheritance()
        {
            var document = new HtmlParser().ParseDocument("<!doctype html><html lang='de'><body><svg xml:lang='fr'><g xml:lang=''><text id='target'>Unknown</text></g></svg></body></html>");
            var target = document.QuerySelector("#target")!;

            Assert.IsFalse(target.Matches(":lang(fr)"));
            Assert.IsFalse(target.Matches(":lang(de)"));
        }

        [TestCase(false, "fr")]
        [TestCase(false, "")]
        [TestCase(true, "fr")]
        [TestCase(true, "")]
        public void UnrelatedLanguageNamespacesDoNotOverrideInheritedOrOrdinaryLanguage(Boolean svg, String unrelatedLanguage)
        {
            var document = new HtmlParser().ParseDocument("<!doctype html><html lang='de'><body></body></html>");
            var target = document.CreateElement(svg ? NamespaceNames.SvgUri : NamespaceNames.HtmlUri, svg ? "g" : "p");
            document.Body.AppendChild(target);
            target.SetAttribute("urn:example", "lang", unrelatedLanguage);

            Assert.IsTrue(target.Matches(":lang(de)"));
            Assert.IsFalse(target.Matches(":lang(fr)"));

            target.SetAttribute(null, "lang", "it");

            Assert.IsTrue(target.Matches(":lang(it)"));
            Assert.IsFalse(target.Matches(":lang(de)"));

            target.SetAttribute(NamespaceNames.XmlUri, "xml:lang", "es");

            Assert.IsTrue(target.Matches(":lang(es)"));
            Assert.IsFalse(target.Matches(":lang(it)"));
        }

        [TestCase("en-US", true)]
        [TestCase("EN", true)]
        [TestCase("english", false)]
        public void LanguageRangeRequiresASubtagBoundary(String language, Boolean expected)
        {
            var document = new HtmlParser().ParseDocument("<!doctype html><p lang='" + language + "'>Text</p>");
            Assert.AreEqual(expected, document.QuerySelector("p")!.Matches(":lang(en)"));
        }
    }
}
