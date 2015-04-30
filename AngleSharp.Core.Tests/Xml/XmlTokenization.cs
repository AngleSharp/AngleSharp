namespace AngleSharp.Core.Tests.Xml
{
    using AngleSharp.Parser.Xml;
    using NUnit.Framework;

    [TestFixture]
    public class XmlTokenization
    {
        [Test]
        public void EmptyXmlDocumentTokenization()
        {
            var s = new TextSource("");
            var t = new XmlTokenizer(s, null);
            var e = t.Get();
            Assert.IsInstanceOf<XmlEndOfFileToken>(e);
        }

        [Test]
        public void OneCommentInXmlDocument()
        {
            var c = "My comment";
            var s = new TextSource("<!--" + c + "-->");
            var t = new XmlTokenizer(s, null);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Comment, e.Type);
            Assert.AreEqual(c, ((XmlCommentToken)e).Data);
        }

        [Test]
        public void ValidXmlDeclarationOnlyVersion()
        {
            var s = new TextSource("<?xml version=\"1.0\"?>");
            var t = new XmlTokenizer(s, null);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            Assert.AreEqual("1.0", ((XmlDeclarationToken)e).Version);
        }

        [Test]
        public void ValidXmlDeclarationVersionAndEncoding()
        {
            var s = new TextSource("<?xml version=\"1.1\" encoding=\"utf-8\" ?>");
            var t = new XmlTokenizer(s, null);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            var x = (XmlDeclarationToken)e;
            Assert.AreEqual("1.1", x.Version);
            Assert.IsFalse(x.IsEncodingMissing);
            Assert.AreEqual("utf-8", x.Encoding);
        }

        [Test]
        public void ValidXmlDeclarationEverything()
        {
            var s = new TextSource("<?xml version='1.0' encoding='ISO-8859-1' standalone=\"yes\" ?>");
            var t = new XmlTokenizer(s, null);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            var x = (XmlDeclarationToken)e;
            Assert.AreEqual("1.0", x.Version);
            Assert.IsFalse(x.IsEncodingMissing);
            Assert.AreEqual("ISO-8859-1", x.Encoding);
            Assert.AreEqual(true, x.Standalone);
        }

        [Test]
        public void OneDoctypeInXmlDocument()
        {
            var s = new TextSource("<!DOCTYPE root_element SYSTEM \"DTD_location\">");
            var t = new XmlTokenizer(s, null);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Doctype, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("root_element", d.Name);
            Assert.IsFalse(d.IsSystemIdentifierMissing);
            Assert.AreEqual("DTD_location", d.SystemIdentifier);
        }
    }
}
