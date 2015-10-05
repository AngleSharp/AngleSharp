namespace AngleSharp.Core.Tests.Xml
{
    using AngleSharp.Parser.Xml;
    using AngleSharp.Xml;
    using NUnit.Framework;

    [TestFixture]
    public class XmlTokenization
    {
        static XmlTokenizer CreateTokenizer(TextSource source)
        {
            return new XmlTokenizer(source, null, XmlEntityService.Resolver);
        }

        [Test]
        public void EmptyXmlDocumentTokenization()
        {
            var s = new TextSource("");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.IsInstanceOf<XmlEndOfFileToken>(e);
        }

        [Test]
        public void OneCommentInXmlDocument()
        {
            var c = "My comment";
            var s = new TextSource("<!--" + c + "-->");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Comment, e.Type);
            Assert.AreEqual(c, ((XmlCommentToken)e).Data);
        }

        [Test]
        public void ValidXmlDeclarationOnlyVersion()
        {
            var s = new TextSource("<?xml version=\"1.0\"?>");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Declaration, e.Type);
            Assert.AreEqual("1.0", ((XmlDeclarationToken)e).Version);
        }

        [Test]
        public void ValidXmlDeclarationVersionAndEncoding()
        {
            var s = new TextSource("<?xml version=\"1.1\" encoding=\"utf-8\" ?>");
            var t = CreateTokenizer(s);
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
            var t = CreateTokenizer(s);
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
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Doctype, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("root_element", d.Name);
            Assert.IsFalse(d.IsSystemIdentifierMissing);
            Assert.AreEqual("DTD_location", d.SystemIdentifier);
        }

        [Test]
        public void XmlTokenizerStringToken()
        {
            var s = new TextSource("teststring\r");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.Character, e.Type);
            var x = (XmlCharacterToken)e;
            Assert.AreEqual("teststring\n", x.Data);
        }

        [Test]
        public void XmlTokenizerStringAndEntityToken()
        {
            var s = new TextSource("test&amp;string\r");
            var t = CreateTokenizer(s);
            var test1 = t.Get();
            var entity = t.Get();
            var test2 = t.Get();
            var end = t.Get();
            Assert.AreEqual(XmlTokenType.Character, test1.Type);
            Assert.AreEqual(XmlTokenType.Entity, entity.Type);
            Assert.AreEqual(XmlTokenType.Character, test2.Type);
            Assert.AreEqual(XmlTokenType.EndOfFile, end.Type);
            Assert.AreEqual("test", ((XmlCharacterToken)test1).Data);
            Assert.AreEqual("amp", ((XmlEntityToken)entity).Value);
            Assert.AreEqual("string\n", ((XmlCharacterToken)test2).Data);
            Assert.AreEqual(XmlTokenType.EndOfFile, end.Type);
        }

        [Test]
        public void XmlTokenizerStringAndTagToken()
        {
            var s = new TextSource("<foo>test</bar>");
            var t = CreateTokenizer(s);
            var foo = t.Get();
            var test = t.Get();
            var bar = t.Get();
            var end = t.Get();

            Assert.AreEqual(XmlTokenType.StartTag, foo.Type);
            Assert.AreEqual(XmlTokenType.EndTag, bar.Type);
            Assert.AreEqual("foo", ((XmlTagToken)foo).Name);
            Assert.AreEqual("bar", ((XmlTagToken)bar).Name);
            Assert.AreEqual("test", ((XmlCharacterToken)test).Data);
            Assert.AreEqual(XmlTokenType.EndOfFile, end.Type);
        }

        [Test]
        public void XmlTokenizerSelfClosingTagWithAttribute()
        {
            var s = new TextSource("<foo bar=\"quz\" />");
            var t = CreateTokenizer(s);
            var foo = t.Get() as XmlTagToken;

            Assert.IsNotNull(foo);
            Assert.AreEqual(XmlTokenType.StartTag, foo.Type);
            Assert.IsTrue(foo.IsSelfClosing);
            Assert.AreEqual("foo", foo.Name);
            Assert.AreEqual(1, foo.Attributes.Count);
            Assert.AreEqual("bar", foo.Attributes[0].Key);
            Assert.AreEqual("quz", foo.Attributes[0].Value);
        }

        [Test]
        public void XmlTokenizerTagWithAttributeContainingEntity()
        {
            var s = new TextSource("<foo bar=\"&quot;quz&quot;\">");
            var t = CreateTokenizer(s);
            var foo = t.Get() as XmlTagToken;

            Assert.IsNotNull(foo);
            Assert.AreEqual(XmlTokenType.StartTag, foo.Type);
            Assert.IsFalse(foo.IsSelfClosing);
            Assert.AreEqual("foo", foo.Name);
            Assert.AreEqual(1, foo.Attributes.Count);
            Assert.AreEqual("bar", foo.Attributes[0].Key);
            Assert.AreEqual("\"quz\"", foo.Attributes[0].Value);
        }
    }
}
