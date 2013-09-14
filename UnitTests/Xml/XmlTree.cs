using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.Xml;

namespace UnitTests
{
    [TestClass]
    public class XmlTree
    {
        [TestMethod]
        public void XmlValidDocumentWithoutDocType()
        {
            var xml = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<!-- Edited by XMLSpy® -->
<note>
	<to>Tove</to>
	<from>Jani</from>
	<heading>Reminder</heading>
	<body>Don't forget me this weekend!</body>
</note>");

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

        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlInvalidDocumentMismatchedEndTag()
        {
            var xml = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<!-- Edited by XMLSpy® -->
<note>
	<to>Tove</to>
	<from>Jani</Ffrom>
	<heading>Reminder</heading>
	<body>Don't forget me this weekend!</body>
</note>");
        }

        [TestMethod]
        public void XmlValidDocumentFoodMenuInnerHTML()
        {
            var xml = DocumentBuilder.Xml(Assets.FoodMenu);

            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual("breakfast_menu", xml.DocumentElement.TagName);
            Assert.AreEqual(5, xml.DocumentElement.ChildElementCount);
            Assert.AreEqual(5, xml.DocumentElement.Children.Length);
            Assert.AreEqual("food", xml.DocumentElement.Children[0].TagName);
            Assert.AreEqual(4, xml.DocumentElement.Children[0].Children.Length);
            Assert.AreEqual("name", xml.DocumentElement.Children[0].Children[0].TagName);
            Assert.AreEqual("$5.95", xml.DocumentElement.Children[0].Children[1].InnerHTML);
            Assert.AreEqual("$7.95", xml.DocumentElement.Children[1].Children[1].InnerHTML);
        }

        [TestMethod]
        public void XmlValidDocumentHelloWorldWithDtd()
        {
            var xml = @"<?xml version=""1.0"" standalone=""yes""?>

<!--open the DOCTYPE declaration -
  the open square bracket indicates an internal DTD-->
<!DOCTYPE foo [

<!--define the internal DTD-->
  <!ELEMENT foo (#PCDATA)>

<!--close the DOCTYPE declaration-->
]>

<foo>Hello World.</foo>";

            var doc = DocumentBuilder.Xml(xml);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.DocumentElement);
            Assert.AreEqual("foo", doc.Doctype.Name);
            
            Assert.AreEqual("foo", doc.DocumentElement.TagName);
            Assert.AreEqual("Hello World.", doc.DocumentElement.InnerHTML);
        }

        [TestMethod]
        public void XmlValidDocumentBooksTree()
        {
            var xml = DocumentBuilder.Xml(Assets.Books);

            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual("catalog", xml.DocumentElement.TagName);
            Assert.AreEqual(12, xml.DocumentElement.ChildElementCount);
            Assert.AreEqual(6, xml.DocumentElement.Children[2].ChildElementCount);
        }
    }
}
