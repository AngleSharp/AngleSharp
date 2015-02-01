using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests
{
    [TestClass]
    public class XmlInvalidDocuments
    {
        /// <summary>
        /// Tests the Element Valid VC (clause 4) by including an undeclared child element.
        /// Here the section(s) 3 apply. This test is taken from the collection Sun
        /// Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidEl01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root ANY>
]>
<root> <undeclared/> </root>

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Tests the Element Valid VC (clause 1) by including elements in an EMPTY content
        /// model. Here the section(s) 3 apply. This test is taken from the collection Sun
        /// Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidEl02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
]>
<root><root/></root>
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Tests the Element Valid VC (clause 3) by including a child element not permitted
        /// by a mixed content model. Here the section(s) 3 apply. This test is taken from the
        /// collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidEl03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root (#PCDATA|root)*>
<!ELEMENT exception (#PCDATA)>
]>
<root>this is ok <exception>this isn't</exception> </root>
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Tests the Element Valid VC (clause 1), using one of the predefined internal
        /// entities inside an EMPTY content model. Here the section(s) 3 apply. This test
        /// is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidEl06()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
    <!-- in case parsers special-case builtin entities incorrectly -->
]>
<root>&amp;</root>

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Element Valid in P39. Element a is declared empty
        /// in DTD, but has content in the document. There is an output test associated
        /// with this input file. Here the section(s) 3 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 39.
        /// </summary>
        [TestMethod]
        public void XmlIbmInvalidP39Ibm39i01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>

]>
<!--* EMPTY element a has content *-->
<root><a>should not have content here</a><b>
   <c></c> 
   content of b element
</b></root>

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Element Valid in P39. root is declared only having element
        /// children in DTD, but have text content in the document. There is an output test
        /// associated with this input file. Here the section(s) 3 apply. This test is taken
        /// from the collection IBM XML Conformance Test Suite - Production 39.
        /// </summary>
        [TestMethod]
        public void XmlIbmInvalidP39Ibm39i02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>

]>
<!--* root element have text content *-->
<root>
 root can't have text content
<a></a><b>
   <c></c> 
   content of b element
</b></root>

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Element Valid in P39. Illegal elements are inserted in b's
        /// content of Mixed type. There is an output test associated with this input file. Here
        /// the section(s) 3 apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 39.
        /// </summary>
        [TestMethod]
        public void XmlIbmInvalidP39Ibm39i03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>
]>
<!--* illgal element in b's Mixed content *-->
<root><a/><b>
   <c></c> 
   content of b element
   <a/>
   could not have 'a' as 'b's content
</b></root>

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Element Valid in P39. Element c has undeclared element
        /// as its content of ANY type There is an output test associated with this input
        /// file. Here the section(s) 3 apply. This test is taken from the collection IBM
        /// XML Conformance Test Suite - Production 39.
        /// </summary>
        [TestMethod]
        public void XmlIbmInvalidP39Ibm39i04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>
  <!ELEMENT f EMPTY>
]>
<!--* element c has undeclared element as its ANY content *-->
<root><a/><b>
   <c><f/></c> 
   content of b element
   <c>
      <d>not declared in dtd</d>
   </c>
</b></root>

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Attribute Value Type in P41. attr1 for Element
        /// b is not declared. There is an output test associated with this input
        /// file. Here the section(s) 3.1 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 41.
        /// </summary>
        [TestMethod]
        public void XmIbmInvalidP41Ibm41i01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (#PCDATA|b)* >
  <!ELEMENT b (#PCDATA) >
  <!ATTLIST b attr2 (abc|def) ""abc"">
  <!ATTLIST b attr3 CDATA #FIXED ""fixed"">
]>
<root>
  <b attr1=""value1"" attr2=""def"" attr3=""fixed"">attr1 not declared</b>
</root>
<!--* testing VC:Attribute Value Type  *-->
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Attribute Value Type in P41. attr3 for Element
        /// b is given a value that does not match the declaration in the DTD. There
        /// is an output test associated with this input file. Here the section(s) 3.1
        /// apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 41.
        /// </summary>
        [TestMethod]
        public void XmlIbmInvalidP41Ibm41i02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (PCDATA|b)* >
  <!ELEMENT b (#PCDATA) >
  <!ATTLIST b attr1 CDATA #REQUIRED>
  <!ATTLIST b attr2 (abc|def) ""abc"">
  <!ATTLIST b attr3 CDATA #FIXED ""fixed"">
]>
<root>
  <b attr1=""value1"" attr2=""abc"" attr3=""shoudbefixed"">attr3 value not fixed</b>
</root>
<!--* testing P41 VC: AtributeValueType*-->
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Elements content can be empty. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP40pass1()
        {
            var document = DocumentBuilder.Xml(@"<doc></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace is valid within a Start-tag. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP40pass2()
        {
            var document = DocumentBuilder.Xml(@"<doc
 
></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace and Multiple Attributes are valid within a Start-tag. Here
        /// the section(s) 3.1 [40] apply. This test is taken from the collection
        /// OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP40pass4()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val"" att2=""val2""
att3=""val3""
></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Attributes are valid within a Start-tag. Here the section(s) 3.1 [40] [41]
        /// apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP40pass3()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Attributes are valid within a Start-tag. Here the section(s) 3.1 [41] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP41pass1()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace is valid within a Start-tags Attribute. Here the section(s)
        /// 3.1 [41] apply. This test is taken from the collection OASIS/NIST TESTS,
        /// 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP41pass2()
        {
            var document = DocumentBuilder.Xml(@"<doc att
 =
  ""val""></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Test shows proper syntax for an End-tag. Here the section(s) 3.1 [42] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP42pass1()
        {
            var document = DocumentBuilder.Xml(@"<doc></doc>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace is valid after name in End-tag. Here the section(s) 3.1 [42] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP42pass2()
        {
            var document = DocumentBuilder.Xml(@"<doc></doc  
>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Valid display of an Empty Element Tag. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP44pass1()
        {
            var document = DocumentBuilder.Xml(@"<doc/>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Empty Element Tags can contain an Attribute. Here the section(s) 3.1 [44]
        /// apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP44pass2()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""/>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace is valid in an Empty Element Tag following the end of
        /// the attribute value. Here the section(s) 3.1 [44] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP44pass3()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""


/>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace is valid after the name in an Empty Element Tag. Here the
        /// section(s) 3.1 [44] apply. This test is taken from the collection
        /// OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP44pass4()
        {
            var document = DocumentBuilder.Xml(@"<doc
  
/>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Whitespace and Multiple Attributes are valid in an Empty Element Tag.
        /// Here the section(s) 3.1 [44] apply. This test is taken from the collection
        /// OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlInvalidOP44pass5()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""
att2=""val2"" att3=""val3""/>");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Tests the Attribute Value Type (declared) VC for the xml:space attribute.
        /// Here the section(s) 3.1 2.10 apply. This test is taken from the collection
        /// Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidInvRequired01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
]>

<root xml:space='preserve'/>

    <!-- all attributes must be declared -->
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// PE name immediately followed by ";" Here the section(s) 4.1 [69] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidDtd03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE violation [
<!ELEMENT violation (a,a,a,b)>
<!ELEMENT a EMPTY>
<!ELEMENT b EMPTY>
    <!-- tests the ""element valid"" constraint for content
	which doesn't match the declared content model.
	(there can be an infinite number of such tests...)
	-->
]>
<violation>
    <a/>
    <a/>
    <b/>
</violation>
");

            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Tests the Attribute Value Type (declared) VC for the xml:lang attribute.
        /// Here the section(s) 3.1 2.12 apply. This test is taken from the collection
        /// Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidInvRequired02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
]>

<root xml:lang='en'/>

    <!-- all attributes must be declared -->

");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// Tests the Unique Element Type Declaration VC. Here the section(s)
        /// 3.2 apply. This test is taken from the collection Sun Microsystems
        /// XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlInvalidEl04()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root ANY>
<!ELEMENT exception (#PCDATA)>
<!ELEMENT exception (#PCDATA)>
]>
<root/>
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }

        /// <summary>
        /// This test violates VC: Unique Element Type Declaration. Element not_unique
        /// has been declared 3 time in the DTD. There is an output test associated with
        /// this input file. Here the section(s) 3.2 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 45.
        /// </summary>
        [TestMethod]
        public void XmlIbmInvalidP45Ibm45i01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (#PCDATA|b)* >
  <!ELEMENT b EMPTY>
  <!ELEMENT not_unique ANY>
  <!ELEMENT not_unique EMPTY>
  <!ELEMENT not_unique (b,b) >
  <!ELEMENT unique. ANY>
  <!ATTLIST b attr1 CDATA #IMPLIED>
  <!ATTLIST b attr2 CDATA #IMPLIED>
  <!ATTLIST b attr3 CDATA #IMPLIED>
]>
<root>
  <b/>without white space
  <b /> with a white space
  <b attr1=""value1"" />
  <b attr1=""value1"" attr2=""value2"" attr3 = ""value3""/>
</root>
<!--* a invalid test: testing P45 VC unique element type decl  *-->
");
            Assert.IsNotNull(document);
            Assert.IsFalse(document.IsValid);
        }
    }
}
