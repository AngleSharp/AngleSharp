using AngleSharp;
using AngleSharp.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class XmlTokenization
    {
        [TestMethod]
        public void EmptyXmlDocumentTokenization()
        {
            var s = new SourceManager("");
            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlToken.EOF, e);
        }

        [TestMethod]
        public void OneCommentInXmlDocument()
        {
            var c = "My comment";
            var s = new SourceManager("<!--" + c + "-->");
            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Comment, e.Type);
            Assert.AreEqual(c, ((XmlCommentToken)e).Data);
        }

        [TestMethod]
        public void ValidXmlDeclarationOnlyVersion()
        {
            var s = new SourceManager("<?xml version=\"1.0\"?>");
            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            Assert.AreEqual("1.0", ((XmlDeclarationToken)e).Version);
        }

        [TestMethod]
        public void ValidXmlDeclarationVersionAndEncoding()
        {
            var s = new SourceManager("<?xml version=\"1.1\" encoding=\"utf-8\" ?>");
            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            var x = (XmlDeclarationToken)e;
            Assert.AreEqual("1.1", x.Version);
            Assert.IsFalse(x.IsEncodingMissing);
            Assert.AreEqual("utf-8", x.Encoding);
        }

        [TestMethod]
        public void ValidXmlDeclarationEverything()
        {
            var s = new SourceManager("<?xml version='1.0' encoding='ISO-8859-1' standalone=\"yes\" ?>");
            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            var x = (XmlDeclarationToken)e;
            Assert.AreEqual("1.0", x.Version);
            Assert.IsFalse(x.IsEncodingMissing);
            Assert.AreEqual("ISO-8859-1", x.Encoding);
            Assert.AreEqual(true, x.Standalone);
        }

        [TestMethod]
        public void OneDoctypeInXmlDocument()
        {
            var s = new SourceManager("<!DOCTYPE root_element SYSTEM \"DTD_location\">");
            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("root_element", d.Name);
            Assert.IsFalse(d.IsSystemIdentifierMissing);
            Assert.AreEqual("DTD_location", d.SystemIdentifier);
        }
    }
}
