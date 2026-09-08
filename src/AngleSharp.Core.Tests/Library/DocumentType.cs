namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DocumentTypeTests
    {
        // https://dom.spec.whatwg.org/#valid-doctype-name
        // https://github.com/web-platform-tests/wpt/blob/master/dom/nodes/DOMImplementation-createDocumentType.html
        [TestCase("html")]
        [TestCase("HTML")]
        [TestCase("")]
        [TestCase("1foo")]
        [TestCase("edi:{")]
        [TestCase(":")]
        [TestCase(":a")]
        [TestCase("a:")]
        [TestCase("a:b:c")]
        [TestCase("/")]
        [TestCase("a/b")]
        [TestCase("a<b")]
        [TestCase("a=b")]
        [TestCase("a\vb")]
        [TestCase("a\u00a0b")]
        [TestCase("a\u0085b")]
        [TestCase("a\u2028b")]
        [TestCase("\ud800")]
        [TestCase("\udc00")]
        [TestCase("\ud83d\ude00")]
        public void CreateDocumentTypeAcceptsValidDoctypeNames(String name)
        {
            var document = "".ToHtmlDocument();
            var doctype = document.Implementation.CreateDocumentType(name, "public", "system");

            Assert.AreEqual(name, doctype.Name);
            Assert.AreEqual(name, doctype.NodeName);
            Assert.AreEqual("public", doctype.PublicIdentifier);
            Assert.AreEqual("system", doctype.SystemIdentifier);
            Assert.AreSame(document, doctype.Owner);
            Assert.AreEqual(NodeType.DocumentType, doctype.NodeType);
            Assert.IsNull(doctype.Parent);
        }

        [TestCase('\t')]
        [TestCase('\n')]
        [TestCase('\f')]
        [TestCase('\r')]
        [TestCase(' ')]
        [TestCase('\0')]
        [TestCase('>')]
        public void CreateDocumentTypeRejectsForbiddenCharacters(Char forbidden)
        {
            var document = "".ToHtmlDocument();

            foreach (var name in new[] { forbidden.ToString(), forbidden + "html", "ht" + forbidden + "ml", "html" + forbidden })
            {
                var exception = Assert.Throws<DomException>(() => document.Implementation.CreateDocumentType(name, "", ""));
                Assert.AreEqual((Int32)DomError.InvalidCharacter, exception.Code);
            }
        }

        [Test]
        public void CreateDocumentTypeRejectsNullName()
        {
            var document = "".ToHtmlDocument();
            var exception = Assert.Throws<ArgumentNullException>(() => document.Implementation.CreateDocumentType(null, "", ""));

            Assert.AreEqual("qualifiedName", exception.ParamName);
        }
    }
}
