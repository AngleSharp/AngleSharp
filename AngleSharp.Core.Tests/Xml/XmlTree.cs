namespace AngleSharp.Core.Tests.Xml
{
    using AngleSharp.Parser.Xml;
    using NUnit.Framework;

    [TestFixture]
    public class XmlTree
    {
        [Test]
        public void XmlValidDocumentWithoutDocType()
        {
            var xml = (@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<!-- Edited by XMLSpy® -->
<note>
	<to>Tove</to>
	<from>Jani</from>
	<heading>Reminder</heading>
	<body>Don't forget me this weekend!</body>
</note>").ToXmlDocument();

            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual("note", xml.DocumentElement.TagName);
            Assert.AreEqual(4, xml.DocumentElement.ChildElementCount);
            Assert.AreEqual(4, xml.DocumentElement.Children.Length);
            Assert.AreEqual("to", xml.DocumentElement.Children[0].TagName);
            Assert.AreEqual("from", xml.DocumentElement.Children[1].TagName);
            Assert.AreEqual("heading", xml.DocumentElement.Children[2].TagName);
            Assert.AreEqual("body", xml.DocumentElement.Children[3].TagName);
        }

        [Test]
        [ExpectedException(typeof(XmlParseException))]
        public void XmlInvalidDocumentMismatchedEndTag()
        {
            var xml = (@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<!-- Edited by XMLSpy® -->
<note>
	<to>Tove</to>
	<from>Jani</Ffrom>
	<heading>Reminder</heading>
	<body>Don't forget me this weekend!</body>
</note>").ToXmlDocument();
        }

        [Test]
        public void XmlValidDocumentFoodMenuInnerHTML()
        {
            var xml = Assets.food.ToXmlDocument();

            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual("breakfast_menu", xml.DocumentElement.TagName);
            Assert.AreEqual(5, xml.DocumentElement.ChildElementCount);
            Assert.AreEqual(5, xml.DocumentElement.Children.Length);
            Assert.AreEqual("food", xml.DocumentElement.Children[0].TagName);
            Assert.AreEqual(4, xml.DocumentElement.Children[0].Children.Length);
            Assert.AreEqual("name", xml.DocumentElement.Children[0].Children[0].TagName);
            Assert.AreEqual("$5.95", xml.DocumentElement.Children[0].Children[1].InnerHtml);
            Assert.AreEqual("$7.95", xml.DocumentElement.Children[1].Children[1].InnerHtml);
        }

        [Test]
        public void XmlValidDocumentBooksTree()
        {
            var xml = Assets.books.ToXmlDocument();

            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual("catalog", xml.DocumentElement.TagName);
            Assert.AreEqual(12, xml.DocumentElement.ChildElementCount);
            Assert.AreEqual(6, xml.DocumentElement.Children[2].ChildElementCount);
        }
    }
}
