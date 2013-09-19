using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests.Xml
{
    /// <summary>
    /// (Conformance) Tests taken from
    /// http://www.w3.org/XML/Test/xmlconf-20031210.html
    /// </summary>
    [TestClass]
    public class XmlValidDocuments
    {
        /// <summary>
        /// Tests with a xml document consisting of prolog followed by element then Misc.
        /// There is an output test associated with this input file. Here the section(s) 2.1
        /// apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 1.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP01Ibm01v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes"" ?>
<!-- Above is XMLDecl -->
<!DOCTYPE animal [
<!ELEMENT animal (cat|tiger|leopard)+>
<!ELEMENT cat EMPTY>
<!ELEMENT tiger (#PCDATA)>
<!ELEMENT leopard ANY>
<!ELEMENT small EMPTY>
<!ELEMENT big EMPTY>
<!ATTLIST tiger color CDATA #REQUIRED>
]>
<!-- Above is DTD -->
<?music ""Here is a PI"" ?> 
<animal>
   <cat/>
   <tiger color=""white"">This is a white tiger in Mirage!!</tiger>
   <cat/>
   <leopard>
      <small/>
      <big/>
   </leopard>
</animal>
<!-- Above is element animal -->

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that although whitespace can be used to set apart
        /// markup for greater readability it is not necessary. There is an output
        /// test associated with this input file. Here the section(s) 2.10 apply. This
        /// test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa084()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [<!ELEMENT doc (#PCDATA)>]><doc></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that extra whitespace is not intended for inclusion in 
        /// the delivered version of the document. There is an output test associated
        /// with this input file. Here the section(s) 2.10 apply. This test is taken 
        /// from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa093()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>


</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that a line break within CDATA will be normalized. There
        /// is an output test associated with this input file. Here the section(s) 2.11
        /// apply. This test is taken from the collection James Clark XMLTEST cases, 
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa116()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><![CDATA[
]]></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// A combination of carriage return line feed in an external entity must be
        /// normalized to a single newline. There is an output test associated with 
        /// this input file. Here the section(s) 2.11 apply. This test is taken from 
        /// the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidExtSa001()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e SYSTEM ""001.ent"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// A carriage return (also CRLF) in an external entity must be normalized
        /// to a single newline. There is an output test associated with this input 
        /// file. Here the section(s) 2.11 apply. This test is taken from the collection 
        /// James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidExtSa002()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e SYSTEM ""002.ent"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// A carriage return (also CRLF) in an external entity must be normalized
        /// to a single newline. There is an output test associated with this input
        /// file. Here the section(s) 2.11 apply. This test is taken from the 
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidExtSa004()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e SYSTEM ""004.ent"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// A carriage return (also CRLF) in an external entity must be normalized 
        /// to a single newline. There is an output test associated with this input 
        /// file. Here the section(s) 2.11 apply. This test is taken from the 
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidExtSa009()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e SYSTEM ""009.ent"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// This tests normalization of end-of-line characters (CRLF) within 
        /// entities to LF, primarily as an output test. There is an output test 
        /// associated with this input file. Here the section(s) 2.11, 3.3.3 apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa108()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e ""
"">
<!ATTLIST doc a CDATA #IMPLIED>
]>
<doc a=""x&e;y""></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests definition of an internal entity holding a carriage return
        /// character reference, which must not be normalized before reporting 
        /// to the application. Line break normalization only occurs when 
        /// parsing external parsed entities. There is an output test associated 
        /// with this input file. Here the section(s) 2.11, 4.5 apply. This test 
        /// is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa068()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e ""&#13;"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates the use of optional character and content particles
        /// within mixed element content. The test also shows the use of an external 
        /// entity and that a carriage control line feed in an external entity must 
        /// be normalized to a single newline. There is an output test associated with 
        /// this input file. Here the section(s) 2.11 3.2.1 3.2.2 4.2.2 [48] [51] [75] 
        /// apply. This test is taken from the collection James Clark XMLTEST cases, 
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidExtSa006()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA|e)*>
<!ELEMENT e EMPTY>
<!ENTITY e SYSTEM ""006.ent"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates the use of a public identifier with and external entity.
        /// The test also show that a carriage control line feed combination in an
        /// external entity must be normalized to a single newline. There is an output 
        /// test associated with this input file. Here the section(s) 2.11 4.2.2 [75] 
        /// apply. This test is taken from the collection James Clark XMLTEST cases, 
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidExtSa011()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e PUBLIC ""a not very interesting file"" ""011.ent"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests LanguageID with Langcode - Subcode. There is an output test associated
        /// with this input file. Here the section(s) 2.12 apply. This test is taken from
        /// the collection IBM XML Conformance Test Suite - Production 33.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP33Ibm33v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE book [
   <!ELEMENT book ANY>
   <!ATTLIST book xml:lang CDATA #REQUIRED>
]>
<book xml:lang=""en-US"">It is written in English</book>
<!-- Tests LanguageID with Langcode - Subcode -->", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Duplicate Test as ibm33v01.xml. There is an output test associated with this
        /// input file. Here the section(s) 2.12 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 34.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP34Ibm34v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE book [
   <!ELEMENT book ANY>
   <!ATTLIST book xml:lang CDATA #REQUIRED>
]>
<book xml:lang=""en-US"">It is written in English</book>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests ISO639Code. There is an output test associated with this input file.
        /// Here the section(s) 2.12 apply. This test is taken from the collection IBM
        /// XML Conformance Test Suite - Production 35.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP35Ibm35v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE book [
   <!ELEMENT book ANY>
   <!ATTLIST book xml:lang CDATA #REQUIRED>
]>
<book xml:lang=""en"">It is written in English</book>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests IanaCode. There is an output test associated with this input file.
        /// Here the section(s) 2.12 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 36.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP36Ibm36v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE book [
   <!ELEMENT book ANY>
   <!ATTLIST book xml:lang CDATA #REQUIRED>
]>
<book xml:lang=""i-BS-ABCD"">It is written in English</book>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests UserCode. There is an output test associated with this input file.
        /// Here the section(s) 2.12 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 37.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP37Ibm37v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE book [
   <!ELEMENT book ANY>
   <!ATTLIST book xml:lang CDATA #REQUIRED>
]>
<book xml:lang=""x-uk-eng"">It is written in English</book>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests SubCode. There is an output test associated with this input file. Here
        /// the section(s) 2.12 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 38.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP38Ibm38v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE book [
   <!ELEMENT book ANY>
   <!ATTLIST book xml:lang CDATA #REQUIRED>
]>
<book xml:lang=""en-USa"">It is written in English</book>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests a lowercase ISO language code. There is an output test associated with
        /// this input file. Here the section(s) 2.12 [35] apply. This test is taken from
        /// the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidVLang01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!ATTLIST root xml:lang CDATA #IMPLIED>
]>
<root xml:lang=""en""/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests a ISO language code with a subcode. There is an output test associated
        /// with this input file. Here the section(s) 2.12 [35] apply. This test is taken
        /// from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidVLang02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!ATTLIST root xml:lang CDATA #IMPLIED>
]>
<root xml:lang=""en-IN""/>

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests an uppercase ISO language code. There is an output test associated with
        /// this input file. Here the section(s) 2.12 [35] apply. This test is taken from
        /// the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidVLang05()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!ATTLIST root xml:lang CDATA #IMPLIED>
]>
<root xml:lang=""DE""/>

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests a IANA language code with a subcode. There is an output test associated
        /// with this input file. Here the section(s) 2.12 [36] apply. This test is taken
        /// from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidVLang03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!ATTLIST root xml:lang CDATA #IMPLIED>
]>
<root xml:lang=""i-klingon-whorf""/>

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests a user language code with a subcode. There is an output test associated
        /// with this input file. Here the section(s) 2.12 [37] apply. This test is taken
        /// from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidVLang04()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!ATTLIST root xml:lang CDATA #IMPLIED>
]>
<root xml:lang=""x-dialect-valleygirl""/>

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests a user language code. There is an output test associated with this input file.
        /// Here the section(s) 2.12 [37] apply. This test is taken from the collection Sun
        /// Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidVLang06()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!ATTLIST root xml:lang CDATA #IMPLIED>
]>
<root xml:lang=""X-Java""/>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests Char with 3 characters - 2 boundaries plus 1 in the middle - for each range
        /// plus #x20 #x9 #xD #xA Here the section(s) 2.2 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 2.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP02Ibm02v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<!DOCTYPE book [
<!ELEMENT book ANY>
<!-- This test case covers     legal character ranges plus
        discrete legal characters for production 02. -->
<?NAME_09-	_0A-
_0D-
_20- _D7FF-퟿_6c0f-氏_E000-_FFFD-�_effe-_010000-𐀀_10FFFF-􏿿_08ffff-򏿿 This is a PI target ?>
]>
<book/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// various Misc items where they can occur Here the section(s) 2.2 [1] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP01pass2()
        {
            var document = DocumentBuilder.Xml(@"<?PI before document element?>
<!-- comment after document element-->
<?PI before document element?>
<!-- comment after document element-->
<?PI before document element?>
<!-- comment after document element-->
<?PI before document element?>
<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a ANY>
<!ELEMENT b ANY>
<!ELEMENT c ANY>
]>
<doc>
<a><b><c/></b></a>
</doc>
<!-- comment after document element-->
<?PI after document element?>
<!-- comment after document element-->
<?PI after document element?>
<!-- comment after document element-->
<?PI after document element?>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that characters outside of normal ascii range can be used
        /// as element content. There is an output test associated with this input file.
        /// Here the section(s) 2.2 [2] apply. This test is taken from the collection
        /// James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa049()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>£</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that characters outside of normal ascii range can be used as
        /// element content. There is an output test associated with this input file. Here the
        /// section(s) 2.2 [2] apply. This test is taken from the collection James Clark
        /// XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa050()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>เจมส์</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// The document is encoded in UTF-16 and uses some name characters well outside
        /// of the normal ASCII range. There is an output test associated with this input
        /// file. Here the section(s) 2.2 [2] apply. This test is taken from the collection
        /// James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa051()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE เจมส์ [
<!ELEMENT เจมส์  (#PCDATA)>
]>
<เจมส์></เจมส์>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// The document is encoded in UTF-8 and the text inside the root
        /// element uses two non-ASCII characters, encoded in UTF-8 and each of which
        /// expands to a Unicode surrogate pair. There is an output test associated with this
        /// input file. Here the section(s) 2.2 [2] apply. This test is taken from the
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa052()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>𐀀􏿽</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests all 4 legal white space characters - #x20 #x9 #xD #xA. Here the
        /// section(s) 2.3 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 3.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP03Ibm03v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>

<!DOCTYPE book [

<!ELEMENT book ANY>

<!-- This test case covers 0 legal character ranges plus

     4 discrete legal characters for production 03. -->

<?NAME_20- _09-	_0D-
_0A-

 This is a PI target ?>

]>

<book/>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Empty EntityValue is legal. There is an output test associated with
        /// this input file. Here the section(s) 2.3 apply. This test is taken
        /// from the collection IBM XML Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP09Ibm09v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
 	<!ENTITY FullName """">
]>

<student>My Name is &FullName;. </student>













 ", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests a normal EnitityValue. There is an output test associated with
        /// this input file. Here the section(s) 2.3 apply. This test is taken
        /// from the collection IBM XML Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP09Ibm09v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
 	<!ENTITY FullName ""SnowMan"">
]>

<student>My Name is &FullName;. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EnitityValue referencing a General Entity. There is an output test associated
        /// with this input file. Here the section(s) 2.3 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP09Ibm09v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?> 
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
<!-- testing entity value with Reference -->
	<!ENTITY RealName ""SnowMan""> 
 	<!ENTITY FullName ""&RealName;"">
]>

<student>My Name is &FullName;. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty AttValue with double quotes as the delimiters. There is
        /// an output test associated with this input file. Here the section(s)
        /// 2.3 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst ""Snow"">
	<!ENTITY mymiddle ""Y"">
	<!ENTITY mylast """">
]>
<!-- testing AttValue with empty char inside double quote -->
<student first="""" last="""">My Name is Snow &mylast; Man. </student>






", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty AttValue with single quotes as the delimiters. There is an output
        /// test associated with this input file. Here the section(s) 2.3 apply. This test
        /// is taken from the collection IBM XML Conformance Test Suite - Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst ""Snow"">
	<!ENTITY mymiddle ""Y"">
	<!ENTITY mylast ''>
]>
<!-- testing AttValue with empty char inside single quote -->
<student first='' last=''>My Name is Snow &mylast; Man. </student>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test AttValue with double quotes as the delimiters and single quote inside.
        /// There is an output test associated with this input file. Here the section(s)
        /// 2.3 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst 'Snow'>
	<!ENTITY mymiddle 'I'>
	<!ENTITY mylast ""Man'"">
]>
<!-- testing AttValue string with a single quote inside -->
<student first=""Snow'"" last=""Man"">My Name is &myfirst; &mylast;. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test AttValue with single quotes as the delimiters and double quote inside.
        /// There is an output test associated with this input file. Here the section(s)
        /// 2.3 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst 'Snow'>
	<!ENTITY mymiddle 'I'>
	<!ENTITY mylast 'Man""'>
]>
<!-- testing AttValue string with a double quote inside -->
<student first='Snow""' last='Man'>My Name is &myfirst; &mylast;. </student>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
        }

        /// <summary>
        /// Test AttValue with a GE reference and double quotes as the delimiters. There
        /// is an output test associated with this input file. Here the section(s) 2.3 apply.
        /// This test is taken from the collection IBM XML Conformance Test Suite - Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v05()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst ""Snow"">
	<!ENTITY mymiddle ""Y"">
	<!ENTITY mylast ""&myfirst; Man"">
]>
<!-- testing AttValue with a reference in double quote -->
<student first=""&myfirst;"" last=""mylast;"">My Name is &mylast;. </student>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test AttValue with a GE reference and single quotes as the delimiters. There is
        /// an output test associated with this input file. Here the section(s) 2.3 apply.
        /// This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v06()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst ""Snow"">
	<!ENTITY mymiddle ""Y"">
	<!ENTITY mylast '&myfirst; Man'>
]>
<!-- testing AttValue with a reference in single quote -->
<student first='&myfirst;' last='&mylast;'>My Name is &mylast;. </student>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// testing AttValue with mixed references and text content in double quotes. There is
        /// an output test associated with this input file. Here the section(s) 2.3 apply.
        /// This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v07()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst ""Snow"">
	<!ENTITY mymiddle ""Y"">
	<!ENTITY mylast ""Man &myfirst; and &myfirst; mymiddle;."">
]>
<!-- testing AttValue with references combination in double quotes -->
<student first=""Full Name &myfirst; &#x31; and &mylast; &mylast; &#x63;"" last=""&mylast;"" >My first Name is &myfirst; and my last name is &mylast;. </student>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty systemliteral using the double quotes. There is an output test associated with
        /// this input file. Here the section(s) 2.3 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 11.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP11Ibm11v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
   <!ELEMENT student (#PCDATA)>
   <!ENTITY unref SYSTEM """">
]>

<!-- testing systemliteral with nothing between the double quotes -->
<student>My Name is SnowMan. </student>








 ", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// testing AttValue with mixed references and text content in single quotes. There is
        /// an output test associated with this input file. Here the section(s) 2.3 apply. This
        /// test is taken from the collection IBM XML Conformance Test Suite - Production 10.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP10Ibm10v08()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student
		first CDATA #REQUIRED
		middle CDATA #IMPLIED
		last CDATA #REQUIRED > 
	<!ENTITY myfirst ""Snow"">
	<!ENTITY mymiddle ""I"">
	<!ENTITY mylast 'Man &myfirst; and &myfirst; mymiddle;.'>
]>
<!-- testing AttValue with references combination in single quote -->
<student first='Full Name &myfirst; and &#x22;&mylast;&#x22; &mylast;' last='&mylast;'>My first Name is &myfirst; and my last name is &mylast;. </student>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test
        /// is taken from the collection IBM XML Conformance Test Suite - Production 11.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP11Ibm11v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
   <!ELEMENT student (#PCDATA)>
   <!ENTITY unref SYSTEM ''>
]>

<!-- testing systemliteral with nothing between the single quotes -->
<student>My Name is SnowMan. </student>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Makes sure that PUBLIC identifiers may have some strange characters. NOTE:
        /// The XML editors have said that the XML specification errata will specify that
        /// parameter entity expansion does not occur in PUBLIC identifiers, so that the
        /// '%' character will not flag a malformed parameter entity reference. There is an
        /// output test associated with this input file. Here the section(s) 2.3 [12] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa100()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ENTITY e PUBLIC "";!*#@$_%"" ""100.xml"">
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }
        /// <summary>
        /// system literals may not contain URI fragments. Here the section(s) 2.3,
        /// 4.2.2 [11] apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP11pass1()
        {
            var document = DocumentBuilder.Xml(@"<!--Inability to resolve a notation should not be reported as an error-->
<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION not1 SYSTEM ""a%a&b&#0<!ELEMENT<!--<?</>?>/\''"">
<!NOTATION not2 SYSTEM 'a
	b""""""'>
<!NOTATION not3 SYSTEM """">
<!NOTATION not4 SYSTEM ''>
]>
<doc/>
");
        }

        /// <summary>
        /// valid public IDs. Here the section(s) 2.3 [12] apply. This test is taken from
        /// the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP12pass1()
        {
            var document = DocumentBuilder.Xml(@"<!--Inability to resolve a notation should not be reported as an error-->
<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION not1 PUBLIC ""a b
cdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"">
<!NOTATION not2 PUBLIC '0123456789-()+,./:=?;!*#@$_%'>
<!NOTATION not3 PUBLIC ""0123456789-()+,.'/:=?;!*#@$_%"">
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Uses a legal XML 1.0 name consisting of a single colon character (disallowed
        /// by the latest XML Namespaces draft). There is an output test associated with
        /// this input file. Here the section(s) 2.3 [4] apply. This test is taken from
        /// the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa012()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc : CDATA #IMPLIED>
]>
<doc :=""v1""></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// The document is encoded in UTF-8 and the name of the root element type uses
        /// non-ASCII characters. There is an output test associated with this input file.
        /// Here the section(s) 2.3 [5] apply. This test is taken from the collection
        /// James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa063()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE เจมส์ [
<!ELEMENT เจมส์ (#PCDATA)>
]>
<เจมส์></เจมส์>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// various satisfactions of the Names production in a NAMES attribute Here the
        /// section(s) 2.3 [6] apply. This test is taken from the collection OASIS/NIST
        /// TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP06pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (a|refs)*>
<!ELEMENT a EMPTY>
<!ELEMENT refs EMPTY>
<!ATTLIST refs refs IDREFS #REQUIRED>
<!ATTLIST a id ID #REQUIRED>
]>
<doc>
<a id=""A1""/><a id=""A2""/><a id=""A3""/>
<refs refs=""A1 A2 A3""/>
<refs refs=""A1
A2	A3""/>
<refs refs=""A1""/>
</doc>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// various valid Nmtoken 's in an attribute list declaration. Here the
        /// section(s) 2.3 [7] apply. This test is taken from the collection OASIS/NIST
        /// TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP07pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att (0|35a|A|-a|:a|a:|.|_a) #IMPLIED>
]>
<doc/>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// various satisfaction of an NMTOKENS attribute value. Here the section(s) 2.3
        /// [8] apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP08pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (A*)>
<!ELEMENT A EMPTY>
<!ATTLIST A att NMTOKENS #IMPLIED>
]>
<doc>
<A att=""abc""/><A att=""abc def . :""/><A att=""
abc
def
""/>
</doc>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demostrates that extra whitespace is normalized into a single space
        /// character. There is an output test associated with this input file. Here the
        /// section(s) 2.3 2.10 apply. This test is taken from the collection James Clark
        /// XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa092()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (a)*>
<!ELEMENT a EMPTY>
]>
<doc>
<a/>
    <a/>	<a/>


</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that an attribute can have a null value. There is an output
        /// test associated with this input file. Here the section(s) 2.3 3.1 [10][40][41]
        /// apply. This test is taken from the collection James Clark XMLTEST cases,
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa109()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a CDATA #IMPLIED>
]>
<doc a=""""></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
        }

        /// <summary>
        /// Test demonstrates that the Attribute in a Start-tag can consist of numerals
        /// along with special characters. There is an output test associated with this
        /// input file. Here the section(s) 2.3 3.1 [13] [40] apply. This test is taken
        /// from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa013()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc _.-0123456789 CDATA #IMPLIED>
]>
<doc _.-0123456789=""v1""></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that all lower case letters are valid for the Attribute in a
        /// Start-tag. There is an output test associated with this input file. Here the
        /// section(s) 2.3 3.1 [13] [40] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa014()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc abcdefghijklmnopqrstuvwxyz CDATA #IMPLIED>
]>
<doc abcdefghijklmnopqrstuvwxyz=""v1""></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that all upper case letters are valid for the Attribute in a
        /// Start-tag. There is an output test associated with this input file. Here the
        /// section(s) 2.3 3.1 [13] [40] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa015()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc ABCDEFGHIJKLMNOPQRSTUVWXYZ CDATA #IMPLIED>
]>
<doc ABCDEFGHIJKLMNOPQRSTUVWXYZ=""v1""></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that PubidChar can be used for element content. There is an
        /// output test associated with this input file. Here the section(s) 2.3 3.1 [43]
        /// apply. This test is taken from the collection James Clark XMLTEST cases,
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa009()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>&#x20;</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Testing CharData with empty string. There is an output test associated with this
        /// input file. Here the section(s) 2.4 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 14.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP14Ibm14v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student first CDATA #REQUIRED
			  last  CDATA #IMPLIED>
]>

<!-- testing chardata with empty string -->
<student first=""Snow""></student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Testing CharData with white space character. There is an output test associated
        /// with this input file. Here the section(s) 2.4 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 14.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP14Ibm14v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student first CDATA #REQUIRED
			  last  CDATA #IMPLIED>
]>

<!-- testing chardata with white space -->
<student first=""Eric""> &#x0A; &#x09; &#x0D;&#x20;</student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Testing CharData with a general text string. There is an output test associated with
        /// this input file. Here the section(s) 2.4 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 14.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP14Ibm14v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)>
	<!ATTLIST student first CDATA #REQUIRED
			  last  CDATA #IMPLIED>
]>

<!-- testing chardata with a string of sample legal char except '<' and '&' nor does it contain sequence ""]]>"" -->
<student first=""Snow"" last=""Man"">This is a test</student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid use of character data, comments, processing instructions and CDATA sections
        /// within the start and end tag. Here the section(s) 2.4 2.5 2.6 2.7 [15] [16] [18]
        /// apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP43pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE elem
[
<!ELEMENT elem (#PCDATA|elem)*>
<!ENTITY ent ""<elem>CharData</elem>"">
]>
<elem>
CharData&#32;
<!--comment-->
<![CDATA[
<elem>
CharData&#32;
<!--comment-->
<?pi?>&ent;&quot;
CharData
</elem>
]]>
<![CDATA[
<elem>
CharData&#32;
<!--comment-->
<?pi?>&ent;&quot;
CharData
</elem>
]]>
<?pi?>&ent;&quot;
CharData
</elem>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that character data is valid element content. There is an output
        /// test associated with this input file. Here the section(s) 2.4 3.1 [14] [43] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa048()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>]</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates character references can be used for element content. There is an
        /// output test associated with this input file. Here the section(s) 2.4 3.1 [43] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa008()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>&amp;&lt;&gt;&quot;&apos;</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Comments may contain any legal XML characters; only the string "--" is disallowed. There
        /// is an output test associated with this input file. Here the section(s) 2.5 apply. This
        /// test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa119()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc ANY>
]>
<doc><!-- -á --></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty comment. There is an output test associated with this input file. Here the
        /// section(s) 2.5 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 15.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP15Ibm15v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>
<!--* Tests empty comment *-->
<!---->
<student>My Name is SnowMan. </student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests comment with regular text. There is an output test associated with this input file.
        /// Here the section(s) 2.5 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 15.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP15Ibm15v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- Student's name -->
<student>My Name is SnowMan. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests comment with one dash inside. There is an output test associated with this input file.
        /// Here the section(s) 2.5 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 15.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP15Ibm15v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- student file-1 -->
<student>My Name is SnowMan. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests comment with more comprehensive content There is an output test associated with this
        /// input file. Here the section(s) 2.5 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 15.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP15Ibm15v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!--student phone number 408-398 (387)-4758 -->
<student>My Name is SnowMan. </student>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Comments don't get parameter entity expansion. There is an output test associated with this
        /// input file. Here the section(s) 2.5 [15] apply. This test is taken from the collection Sun
        /// Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidDtd01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT y (#PCDATA|x|x)*>
    <!-- element types can't repeat in mixed content -->
    <!ELEMENT root ANY>
]>

<root/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that comments are valid element content. There is an output test associated
        /// with this input file. Here the section(s) 2.5 3.1 [15] [43] apply. This test is taken from
        /// the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa021()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><!-- a comment --></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that comments are valid element content and that all characters before
        /// the double-hypen right angle combination are considered part of thecomment. There is an
        /// output test associated with this input file. Here the section(s) 2.5 3.1 [15] [43] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa022()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><!-- a comment ->--></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests PI definition with only PItarget name and nothing else. There is an output test
        /// associated with this input file. Here the section(s) 2.6 apply. This test is taken from
        /// the collection IBM XML Conformance Test Suite - Production 16.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP16Ibm16v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<?MyInstruct?>
<student>My Name is SnowMan. </student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests PI definition with only PItarget name and a white space. There is an output test
        /// associated with this input file. Here the section(s) 2.6 apply. This test is taken from
        /// the collection IBM XML Conformance Test Suite - Production 16.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP16Ibm16v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<?MyInstruct ?>
<student>My Name is SnowMan. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests PI definition with PItarget name and text that contains question mark and right
        /// angle. There is an output test associated with this input file. Here the section(s) 2.6
        /// apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 16.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP16Ibm16v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<?MyInstruct AVOID ? BEFORE > IN PI ?>
<student>My Name is SnowMan. </student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests PITarget name. There is an output test associated with this input file. Here the
        /// section(s) 2.6 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 17.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP17Ibm17v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<?MyInstruct This is a test ?>
<student>My Name is SnowMan. </student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid comment and that it may appear anywhere in the document
        /// including at the end. There is an output test associated with this input file. Here
        /// the section(s) 2.6 [15] apply. This test is taken from the collection James Clark
        /// XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa037()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
<!-- comment -->

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid comment and that it may appear anywhere in the document
        /// including the beginning. There is an output test associated with this input file.
        /// Here the section(s) 2.6 [15] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa038()
        {
            var document = DocumentBuilder.Xml(@"<!-- comment -->
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid processing instruction. There is an output test associated
        /// with this input file. Here the section(s) 2.6 [16] apply. This test is taken from the
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa036()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
<?pi data?>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid processing instruction and that it may appear at the beginning
        /// of the document. There is an output test associated with this input file. Here the
        /// section(s) 2.6 [16] apply. This test is taken from the collection James Clark XMLTEST
        /// cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa039()
        {
            var document = DocumentBuilder.Xml(@"<?pi data?>
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that extra whitespace within a processing instruction willnormalized
        /// into s single space character. There is an output test associated with this input file.
        /// Here the section(s) 2.6 2.10 [16] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa055()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<?pi  data?>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that extra whitespace within a processing instruction is converted
        /// into a single space character. There is an output test associated with this input file.
        /// Here the section(s) 2.6 2.10 [16] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa098()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><?pi x
y?></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that Processing Instructions are valid element content. There is an
        /// output test associated with this input file. Here the section(s) 2.6 3.1 [16] [43]
        /// apply. This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa016()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><?pi?></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that Processing Instructions are valid element content and there
        /// can be more than one. There is an output test associated with this input file. Here
        /// the section(s) 2.6 3.1 [16] [43] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa017()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><?pi some data ? > <??></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests CDSect with CDStart CData CDEnd. There is an output test associated with this
        /// input file. Here the section(s) 2.7 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 18.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP18Ibm18v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- testing CDSect with CDStart CData CDEnd -->

<student>My Name is SnowMan. <![CDATA[This is <normal> text]]> </student>

", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("My Name is SnowMan. This is <normal> text ", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Tests CDStart. There is an output test associated with this input file. Here the section(s)
        /// 2.7 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 19.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP19Ibm19v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- testing CDStart -->
<student>My Name is SnowMan. <![CDATA[This is a test]]> </student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("My Name is SnowMan. This is a test ", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Tests CDATA with empty string. There is an output test associated with this input file.
        /// Here the section(s) 2.7 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 20.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP20Ibm20v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- testing CData with empty string -->

<student>My Name is SnowMan. <![CDATA[]]></student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("My Name is SnowMan. ", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Tests CDATA with regular content. There is an output test associated with this input file.
        /// Here the section(s) 2.7 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 20.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP20Ibm20v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- testing CData with legal chars -->

<student>My Name is SnowMan. <![CDATA[<testing>This is a test</testing>]]></student>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("My Name is SnowMan. <testing>This is a test</testing>", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Tests CDEnd. There is an output test associated with this input file. Here the section(s)
        /// 2.7 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 21.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP21Ibm21v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student [
	<!ELEMENT student (#PCDATA)> 
]>

<!-- testing CDEnd --> 

<student>My Name is SnowMan. <![CDATA[This is a test]]> </student>


", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("My Name is SnowMan. This is a test ", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Test demonstrates that all text within a valid CDATA section is considered text and not
        /// recognized as markup. There is an output test associated with this input file. Here the
        /// section(s) 2.7 [20] apply. This test is taken from the collection James Clark XMLTEST
        /// cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa114()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e ""<![CDATA[&foo;]]>"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("&foo;", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Test demonstrates that CDATA sections are valid element content. There is an output test
        /// associated with this input file. Here the section(s) 2.7 3.1 [18] [43] apply. This test is
        /// taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa018()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><![CDATA[<foo>]]></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual("<foo>", document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Test demonstrates that CDATA sections are valid element content and that ampersands may occur in
        /// their literal form. There is an output test associated with this input file. Here the section(s)
        /// 2.7 3.1 [18] [43] apply. This test is taken from the collection James Clark XMLTEST cases,
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa019()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><![CDATA[<&]]></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstractes that CDATA sections are valid element content and that everyting between the
        /// CDStart and CDEnd is recognized as character data not markup. There is an output test associated
        /// with this input file. Here the section(s) 2.7 3.1 [18] [43] apply. This test is taken from the
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa020()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc><![CDATA[<&]>]]]></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with XMLDecl and doctypedecl. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with doctypedecl. There is an output test associated with this input file. Here the
        /// section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with Misc doctypedecl. There is an output test associated with this input file. Here
        /// the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<!-- This is a Misc -->
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with doctypedecl Misc. There is an output test associated with this input file. Here
        /// the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v04()
        {
            var document = DocumentBuilder.Xml(@"<!-- This is a Misc -->
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with XMLDecl Misc doctypedecl. There is an output test associated with this input
        /// file. Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v05()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!-- This is a Misc -->
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with XMLDecl doctypedecl Misc. There is an output test associated with this input
        /// file. Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v06()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<!-- This is a Misc -->
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests prolog with XMLDecl Misc doctypedecl Misc. There is an output test associated with this
        /// input file. Here the section(s) 2.8 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 22.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP22Ibm22v07()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!-- This is a Misc -->
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<!-- This is a Misc -->
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests XMLDecl with VersionInfo only. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 23.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP23Ibm23v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests XMLDecl with VersionInfo EncodingDecl. There is an output test associated with this input
        /// file. Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 23.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP23Ibm23v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' encoding='UTF-8' ?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests XMLDecl with VersionInfo SDDecl. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 23.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP23Ibm23v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' standalone='yes' ?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests XMLDecl with VerstionInfo and a trailing whitespace char. There is an output test associated
        /// with this input file. Here the section(s) 2.8 apply. This test is taken from the collection IBM
        /// XML Conformance Test Suite - Production 23.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP23Ibm23v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' ?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests XMLDecl with VersionInfo EncodingDecl SDDecl. There is an output test associated with this
        /// input file. Here the section(s) 2.8 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 23.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP23Ibm23v05()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests XMLDecl with VersionInfo EncodingDecl SDDecl and a trailing whitespace. There is an output
        /// test associated with this input file. Here the section(s) 2.8 apply. This test is taken from
        /// the collection IBM XML Conformance Test Suite - Production 23.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP23Ibm23v06()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests VersionInfo with single quote. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 24.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP24Ibm24v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests VersionInfo with double quote. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance
        /// Test Suite - Production 24.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP24Ibm24v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EQ with =. There is an output test associated with this input file. Here the section(s) 2.8
        /// apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 25.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP25Ibm25v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EQ with = and spaces on both sides. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 25.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP25Ibm25v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version ='1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EQ with = and space in front of it. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 25.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP25Ibm25v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version= '1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EQ with = and space after it. There is an output test associated with this input file. Here
        /// the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 25.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP25Ibm25v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version = '1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests VersionNum 1.0. There is an output test associated with this input file. Here the section(s)
        /// 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 26.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP26Ibm26v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' ?>
<!DOCTYPE doc [
   <!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests Misc with comment. There is an output test associated with this input file. Here the section(s)
        /// 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 27.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP27Ibm27v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' ?>
<!DOCTYPE doc [
   <!ELEMENT doc EMPTY>
]>
<doc/>
<!-- This is a comment in Misc -->", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests Misc with PI. There is an output test associated with this input file. Here the section(s) 2.8 apply.
        /// This test is taken from the collection IBM XML Conformance Test Suite - Production 27.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP27Ibm27v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' ?>
<!DOCTYPE doc [
   <!ELEMENT doc EMPTY>
]>
<doc/>
<?sound ""This is a PI in Misc ?>", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests Misc with white spaces. There is an output test associated with this input file. Here the
        /// section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 27.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP27Ibm27v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' ?>
<!DOCTYPE doc [
   <!ELEMENT doc ANY>
]>
<doc>S is in the following Misc</doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests doctypedecl with internal DTD only. There is an output test associated with this input file.
        /// Here the section(s) 2.8 apply. This test is taken from the collection IBM XML Conformance Test
        /// Suite - Production 28.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP28Ibm28v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding='UTF-8'?>
<!DOCTYPE animal [
   <!ELEMENT animal EMPTY>
]>
<!-- This a valid test file for production [28] --> 
<animal/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests markupdecl with combinations of elementdecl, AttlistDecl,EntityDecl, NotationDecl, PI
        /// and comment. There is an output test associated with this input file. Here the section(s) 2.8
        /// apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 29.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP29Ibm29v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!DOCTYPE animal [
   <!ELEMENT animal (cat|tiger|leopard)+>
   <!NOTATION animal_class SYSTEM ""ibm29v01.txt"">
   <!ELEMENT cat ANY>
   <!ENTITY forcat ""This is a small cat"">
   <!ELEMENT tiger (#PCDATA)>
   <!ELEMENT leopard ANY>
   <!ELEMENT small EMPTY>
   <!ELEMENT big EMPTY>
   <!ATTLIST tiger color CDATA #REQUIRED>
   <?sound ""This is a PI"" ?>
   <!-- This is a comment -->
    
]>
<animal>
   <cat>&forcat;</cat>
   <tiger color=""white"">This is a white tiger in Mirage!!</tiger>
   <cat/>
   <leopard>
      <small/>
      <big/>
   </leopard>
</animal>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests WFC: PE in internal subset as a positive test. There is an output test associated with
        /// this input file. Here the section(s) 2.8 apply. This test is taken from the collection IBM
        /// XML Conformance Test Suite - Production 29.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP29Ibm29v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!DOCTYPE animal [
   <!ELEMENT animal (cat|tiger|leopard)+>
   <!NOTATION animal_class SYSTEM ""ibm29v01.txt"">
   <!ELEMENT cat ANY>
   <!ENTITY forcat ""This is a small cat"">
   <!ELEMENT tiger (#PCDATA)>
   <!ENTITY % make_leopard_element ""<!ELEMENT leopard ANY>"">
   %make_leopard_element; 
   <!ELEMENT small EMPTY>
   <!ELEMENT big EMPTY>
   <!ATTLIST tiger color CDATA #REQUIRED>
   <?sound ""This is a PI"" ?>
   <!-- This is a comment -->
    
]>
<animal>
   <cat>&forcat;</cat>
   <tiger color=""white"">This is a white tiger in Mirage!!</tiger>
   <cat/>
   <leopard>
      <small/>
      <big/>
   </leopard>
</animal>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests extSubset with extSubsetDecl only in the dtd file. There is an output test associated
        /// with this input file. Here the section(s) 2.8 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 30.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP30Ibm30v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE animal [
  <!ELEMENT animal EMPTY>
]>
<animal/>
<!-- tests extSubset with extSubsetDecl only in the dtd file -->
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests extSubsetDecl with combinations of markupdecls, conditionalSects, PEReferences
        /// and white spaces. There is an output test associated with this input file. Here the section(s)
        /// 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 31.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP31Ibm31v01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE animal [
  <!ENTITY % rootElement ""<!ELEMENT animal ANY>"">
  %rootElement;
  <!-- Following is a makupdecl -->
  <!ENTITY % make_tiger_element ""<!ELEMENT tiger EMPTY>"">
  %make_tiger_element;
]>
<animal>
   <tiger/>
</animal>
<!-- tests extSubsetDecl with combinations of markupdecls, conditionalSects, PEReferences and white spaces -->
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// XML decl and doctypedecl Here the section(s) 2.8 [22] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP22pass4()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!--comment--> <?pi?>
<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
]>

<!--comment--> <?pi?>

<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// just doctypedecl Here the section(s) 2.8 [22] apply. This test is taken from the
        /// collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP22pass5()
        {
            var document = DocumentBuilder.Xml(@"<!--comment--> <?pi?>
<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
]>

<!--comment--> <?pi?>

<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// S between decls is not required Here the section(s) 2.8 [22] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP22pass6()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?><!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
]><doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that both a EncodingDecl and SDDecl are valid within the prolog.
        /// There is an output test associated with this input file. Here the section(s) 2.8 [23]
        /// apply. This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa033()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' encoding=""UTF-8"" standalone='yes'?>
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid prolog that uses double quotes as delimeters around the
        /// VersionNum. There is an output test associated with this input file. Here the
        /// section(s) 2.8 [24] apply. This test is taken from the collection James Clark XMLTEST
        /// cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa028()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid prolog that uses single quotes as delimters around the
        /// VersionNum. There is an output test associated with this input file. Here the
        /// section(s) 2.8 [24] apply. This test is taken from the collection James Clark
        /// XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa029()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0'?>
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid prolog that contains whitespace on both sides of
        /// the equal sign in the VersionInfo. There is an output test associated with
        /// this input file. Here the section(s) 2.8 [25] apply. This test is taken from
        /// the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa030()
        {
            var document = DocumentBuilder.Xml(@"<?xml version = ""1.0""?>
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid types of markupdecl. Here the section(s) 2.8 [29] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP29pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<?Pi?><!--comment-->
<!ELEMENT doc EMPTY>
<?Pi?><!--comment-->
<!ATTLIST doc att CDATA #IMPLIED>
<?Pi?><!--comment-->
<!ENTITY % ent """">
<?Pi?><!--comment-->
<!NOTATION not PUBLIC ""some notation"">
<?Pi?><!--comment-->
]>
<doc/>
", new DocumentOptions(validating : true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid doctypedecl with Parameter entity reference. The declaration of a parameter
        /// entity must precede any reference to it. Here the section(s) 2.8 4.1 [28] [69] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP28pass3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ENTITY % eldecl ""<!ELEMENT doc EMPTY>"">
%eldecl;
]>
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid PEReferences. Here the section(s) 4.1 [69] apply. This test is taken from
        /// the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP69pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY % pe ""<!---->"">
%pe;<!---->%pe;
]>
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests whether entities may be declared more than once, with the first declaration being
        /// the binding one. There is an output test associated with this input file. Here the section(s) 4.2 apply
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa086()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e """">
<!ENTITY e ""<foo>"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual(1, document.DocumentElement.ChildNodes.Length);

            var text = document.DocumentElement.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(String.Empty, text.TextContent);
        }

        /// <summary>
        /// Test demonstrates a valid SDDecl within the prolog. There is an output test associated with this
        /// input file. Here the section(s) 2.9 [32] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa032()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' standalone='yes'?>
<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// A document may be marked 'standalone' if any optional whitespace is defined within the internal
        /// DTD subset. There is an output test associated with this input file. Here the section(s) 2.9
        /// [32] apply. This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidSa01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' standalone='yes'?>

<!DOCTYPE root [
    <!ELEMENT root (child)*>
    <!ELEMENT child (#PCDATA)>
]>

<root>
    <child>
    The whitespace around this element would be
    invalid as standalone were the DTD external.
    </child>
</root>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// A document may be marked 'standalone' if any attributes that need normalization are defined within
        /// the internal DTD subset. There is an output test associated with this input file. Here the section(s)
        /// 2.9 [32] apply. This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidSa02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version='1.0' standalone='yes'?>

<!DOCTYPE attributes [
    <!ELEMENT attributes EMPTY>

    <!--
	2.9 gives validity constraints applying to attributes
	in standalone docs:  no external defaults or decls
	causing normalization.

	3.3.3 describes the normalization rules
    -->

    <!ATTLIST attributes
	token		(a|b|c)		""a""
	notation	(nonce|foo|bar)	#IMPLIED
	nmtoken		NMTOKEN		#IMPLIED
	nmtokens	NMTOKENS	#IMPLIED
	id		ID		#IMPLIED
	idref		IDREF		#IMPLIED
	idrefs		IDREFS		#IMPLIED
	entity		ENTITY		#IMPLIED
	entities	ENTITIES	#IMPLIED
	cdata		CDATA		#IMPLIED
	>
    
    <!ENTITY number ""42"">
    <!ENTITY internal "" internal&number; "">

    <!NOTATION nonce SYSTEM ""file:/dev/null"">
    <!NOTATION foo PUBLIC ""-//public id//foo"" ""file:/dev/null"">
    <!NOTATION bar SYSTEM ""file:/dev/tty"">

    <!ENTITY unparsed-1 PUBLIC ""-//some public//ID"" ""file:/dev/console""
			NDATA nonce>
    <!ENTITY unparsed-2 SYSTEM ""scheme://host/data""
			NDATA foo>
]>

<attributes
    notation =	"" nonce ""
    nmtoken =	"" this-gets-normalized ""
    nmtokens =	"" this	
 also	 gets normalized ""
    id =	""	&internal; ""
    idref =	"" &internal;
    ""
    idrefs =	"" &internal;  &internal;    &internal;""
    entity =	"" unparsed-1 ""
    entities =	""unparsed-1 unparsed-2""
    cdata =	""nothing happens to this one!""
    />
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests clauses 1, 3, and 4 of the Element Valid validity constraint. There is an output test associated
        /// with this input file. Here the section(s) 3 apply. This test is taken from the collection Sun
        /// Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        public void XmlValidElement()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root ANY>
<!ELEMENT empty EMPTY>
<!ELEMENT mixed1 (#PCDATA)>
<!ELEMENT mixed2 (#PCDATA)*>
<!ELEMENT mixed3 (#PCDATA|empty)*>
]>

<root>
    <empty/>

    <mixed1/>
    <mixed1></mixed1>

    <mixed2/>
    <mixed2></mixed2>

    <mixed3/>
    <mixed3></mixed3>

    <mixed1>allowed</mixed1>
    <mixed1><![CDATA[<allowed>]]></mixed1>

    <mixed2>also</mixed2>
    <mixed2><![CDATA[<% illegal otherwise %>]]></mixed2>

    <mixed3>moreover</mixed3>

    <mixed1>allowed &amp; stuff</mixed1>

    <mixed2>also</mixed2>

    <mixed3>moreover <empty></empty> </mixed3>
    <mixed3>moreover <empty/> </mixed3>
    <mixed3><empty/> </mixed3>
    <mixed3><empty/> too</mixed3>

</root>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests element with EmptyElemTag and STag content Etag, also tests the VC: Element Valid
        /// with elements that have children, Mixed and ANY contents There is an output test associated
        /// with this input file. Here the section(s) 3 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 39.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP39Ibm39v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>
  <!ELEMENT d ((e,e)|f)+ >
  <!ELEMENT e ANY>
  <!ELEMENT f EMPTY>
]>
<root><a/><b>
   <c></c> 
   content of b element
   <c>
      <d><e>no more children</e><e><f/></e><f/></d>
   </c>
</b></root>
<!--* test P39's syntax and Element Valid VC *-->
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests STag with possible combinations of its fields, also tests WFC: Unique Att Spec.
        /// There is an output test associated with this input file. Here the section(s) 3.1 apply.
        /// This test is taken from the collection IBM XML Conformance Test Suite - Production 40.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP40Ibm40v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (#PCDATA|b)* >
  <!ELEMENT b (#PCDATA) >
  <!ATTLIST b attr1 CDATA #IMPLIED>
  <!ATTLIST b attr2 CDATA #IMPLIED>
  <!ATTLIST b attr3 CDATA #IMPLIED>
]>
<root>
  <b>without white space</b>
  <b > with a white space</b>
  <b attr1=""value1"">one attribute</b>
  <b attr1=""value1"" attr2=""value2"" attr3 = ""value3"">one attribute</b>
</root>
<!--* testing P40 *-->
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests Attribute with Name Eq AttValue and VC: Attribute Value Type There is an output
        /// test associated with this input file. Here the section(s) 3.1 apply. This test is taken
        /// from the collection IBM XML Conformance Test Suite - Production 41.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP41Ibm41v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (#PCDATA|b)* >
  <!ELEMENT b (#PCDATA) >
  <!ATTLIST b attr1 CDATA #REQUIRED>
  <!ATTLIST b attr2 (abc|def) ""abc"">
  <!ATTLIST b attr3 CDATA #FIXED ""fixed"">
]>
<root>
  <b attr1=""value1"" attr2=""def"" attr3=""fixed"">Name eq AttValue</b>
</root>
<!--* testing P41 *-->
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests ETag with possible combinations of its fields There is an output test associated
        /// with this input file. Here the section(s) 3.1 apply. This test is taken from the collection
        /// IBM XML Conformance Test Suite - Production 42.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP42Ibm42v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>
]>
<root><a/><b>
   <c></c > : End tag with a space inside
   content of b element
</b></root>
<!--* test P42 *-->

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests content with all possible constructs: element, CharData, Reference, CDSect, Comment.
        /// There is an output test associated with this input file. Here the section(s) 3.1 apply. This test
        /// is taken from the collection IBM XML Conformance Test Suite - Production 43.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP43Ibm43v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>
  <!ENTITY inContent ""<b>General entity reference in element content</b>"">
]>
<!--* content: element|CharData|Reference|CDSect|PI|CDSect|PI|Comment *-->
<root><a/><b>
<!-- there is an empty element in the above line -->
   <c></c> 
   CharData: content of b element
   %paaa; : PE reference should not be recognized in element content 
   <c>
<?PIcontent anyProcessingInstruction?>
<!-- Comment content -->
    &inContent;
    Charater reference: &#x41;
    CDSect in content: <![CDATA[ <html>markups<head>HEAD</head><body>nothing</body></html> ]]>
   </c>
</b>
</root>
<!--* test P43 *-->
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EmptyElemTag with possible combinations of its fields There is an output test
        /// associated with this input file. Here the section(s) 3.1 apply. This test is taken from
        /// the collection IBM XML Conformance Test Suite - Production 44.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP44Ibm44v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (#PCDATA|b)* >
  <!ELEMENT b EMPTY >
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
<!--* testing P44 *-->



", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that whitespace is permitted after the tag name in a Start-tag. There
        /// is an output test associated with this input file. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa002()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc ></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid attribute specification within a Start-tag that contains whitespace
        /// on both sides of the equal sign. There is an output test associated with this input file. Here
        /// the section(s) 3.1 [40] apply. This test is taken from the collection James Clark XMLTEST cases, 
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa005()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a1 CDATA #IMPLIED>
]>
<doc a1 = ""v1""></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that whitespace is valid after the Attribute in a Start-tag. There is an output
        /// test associated with this input file. Here the section(s) 3.1 [40] apply. This test is taken from the
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa010()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a1 CDATA #IMPLIED>
]>
<doc a1=""v1"" ></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates mutliple Attibutes within the Start-tag. There is an output test
        /// associated with this input file. Here the section(s) 3.1 [40] apply. This test is taken
        /// from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa011()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a1 CDATA #IMPLIED a2 CDATA #IMPLIED>
]>
<doc a1=""v1"" a2=""v2""></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that extra whitespace within an Attribute of a Start-tag is normalized to
        /// a single space character. There is an output test associated with this input file. Here the
        /// section(s) 3.1 [40] apply. This test is taken from the collection James Clark XMLTEST cases,
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa104()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a CDATA #IMPLIED>
]>
<doc a=""x	y""></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that extra whitespace within Start-tags and End-tags are nomalized into
        /// single spaces. There is an output test associated with this input file. Here the section(s)
        /// 3.1 [40] [42] apply. This test is taken from the collection James Clark XMLTEST cases,
        /// 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa054()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>


<doc
></doc
>


", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates a valid attribute specification within a Start-tag. There is an output test
        /// associated with this input file. Here the section(s) 3.1 [41] apply. This test is taken from
        /// the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa004()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a1 CDATA #IMPLIED>
]>
<doc a1=""v1""></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that the AttValue within a Start-tag can use a single quote as a delimter.
        /// There is an output test associated with this input file. Here the section(s) 3.1 [41] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa006()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a1 CDATA #IMPLIED>
]>
<doc a1='v1'></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that whitespace is permitted after the tag name in an End-tag. There is an output
        /// test associated with this input file. Here the section(s) 3.1 [42] apply. This test is taken from
        /// the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa003()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc></doc >
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that Entity References are valid element content. There is an output test associated
        /// with this input file. Here the section(s) 3.1 [43] apply. This test is taken from the collection James
        /// Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa023()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
<!ENTITY e """">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that extra whitespace is normalized into single space character. There is an output test
        /// associated with this input file. Here the section(s) 3.1 [43] apply. This test is taken from the collection
        /// James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa047()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>X
Y</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Empty-element tag must be used for element which are declared EMPTY. Here the section(s) 3.1 [43] [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP28pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE 

doc

[
<!ELEMENT doc EMPTY>
]>
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates the correct syntax for an Empty element tag. There is an output test associated with this
        /// input file. Here the section(s) 3.1 [44] apply. This test is taken from the collection James Clark XMLTEST
        /// cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa034()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that whitespace is permissible after the name in an Empty element tag. There is an output
        /// test associated with this input file. Here the section(s) 3.1 [44] apply. This test is taken from the
        /// collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa035()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc />
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that the empty-element tag must be use for an elements that are declared EMPTY.
        /// There is an output test associated with this input file. Here the section(s) 3.1 [44] apply. This
        /// test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa044()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (e*)>
<!ELEMENT e EMPTY>
<!ATTLIST e a1 CDATA ""v1"" a2 CDATA ""v2"" a3 CDATA #IMPLIED>
]>
<doc>
<e a3=""v3""/>
<e a1=""w1""/>
<e a2=""w2"" a3=""v3""/>
</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates that Entity References are valid element content and also demonstrates a valid
        /// Entity Declaration. There is an output test associated with this input file. Here the section(s)
        /// 3.1 4.1 [43] [66] apply. This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa024()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (foo)>
<!ELEMENT foo (#PCDATA)>
<!ENTITY e ""&#60;foo></foo>"">
]>
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Test demonstrates numeric character references can be used for element content. There is
        /// an output test associated with this input file. Here the section(s) 3.1 4.6 [43] apply.
        /// This test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidSa007()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc (#PCDATA)>
]>
<doc>&#32;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests both P45 elementDecl and P46 contentspec with possible combinations of their constructs.
        /// There is an output test associated with this input file. Here the section(s) 3.2 apply. This
        /// test is taken from the collection IBM XML Conformance Test Suite - Production 45.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP45Ibm45v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (#PCDATA|b)* >
  <!--* P45 no space before the end bracket *-->
  <!ELEMENT b EMPTY>
  <!ELEMENT unique ANY>
  <!ELEMENT unique- ANY>
  <!ELEMENT unique_ ANY>
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
<!--* !!! testing both P45 and p46 *-->

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests all possible children,cp,choice,seq patterns in P47,P48,P49,P50 There is an output test
        /// associated with this input file. Here the section(s) 3.2.1 apply. This test is taken from the
        /// collection IBM XML Conformance Test Suite - Production 47.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP47Ibm47v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE root [
  <!ELEMENT root (a,b)>
  <!ELEMENT a EMPTY>
  <!ELEMENT b (#PCDATA|c)* >
  <!ELEMENT c ANY>
  <!ELEMENT d ANY>
  <!ELEMENT e ANY>
  <!ELEMENT f ANY>
  <!--* test all possible children,cp,choice,seq patterns in P47,P48,P49,P50 *-->
  <!ELEMENT child0 (a)>
  <!ELEMENT child1 (a|b|c)>
  <!ELEMENT child2 (a ,b,b?,a*,c,c,a,a,b+,c ) >
  <!ELEMENT child3 (a+|b)? >
  <!ELEMENT child4 (a, (b|c)+, (a|d)?, (e|f)* )?>
  <!ELEMENT child5 ( (a,b) | c? | ((d|e),b,c) )* >
  <!ELEMENT child5_1 ( (a,b)* | (c,b)? | (d,a)+ | ((e|f),b,c) )* >
  <!ELEMENT child6 (a,b,c)*>
  <!ELEMENT child7 ((a,b)|c*|((d|e),b,c) )+ >
  <!ELEMENT child8 ( a, (b|c), (a|b), b)+>  
]>
<root><a/><b>
   <c></c >
   content of b element
</b></root>
<!--* a valid test: tests P47,P48,P49,P50*-->

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }
    }
}
