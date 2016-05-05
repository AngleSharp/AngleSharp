using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests
{
    [TestClass]
    public class XmlValidExtDtd
    {
        [TestInitialize]
        public void SetUp()
        {
            Configuration.RegisterHttpRequester<DtdRequester>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Configuration.UnregisterHttpRequester<DtdRequester>();
        }

        /// <summary>
        /// Tests EnitityValue referencing a Parameter Entity. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test
        /// is taken from the collection IBM XML Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP09Ibm09v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student  SYSTEM ""ibm09v03.dtd"">
<student>I am a new student with &Name;</student>
");

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Testing PubidChar with all legal PubidChar in a PubidLiteral. There is an
        /// output test associated with this input file. Here the section(s) 2.3 apply.
        /// This test is taken from the collection IBM XML Conformance Test Suite -
        /// Production 13.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP13Ibm13v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC ""#x20 #xD #xA abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ -'()+,./:=?;!*#@$_% "" ""student.dtd"">

<!-- testing Pubid char with all legal pubidchar in a string -->
<student>My Name is SnowMan. </student>








 ", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests regular systemliteral using the double quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP12Ibm12v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC ""The big ' in it"" ""student.dtd"">

<!-- testing Pubid Literal with a string with ""'"" inside -->
<student>My Name is SnowMan. </student>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// valid EntityValue's. Except for entity references, markup is not recognized.
        /// Here the section(s) 2.3 [9] apply. This test is taken from the collection
        /// OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP09pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p09pass1.dtd"">
<doc/>", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Constructs an &lt;!ATTLIST...&gt; declaration from several PEs. There is an output test
        /// associated with this input file. Here the section(s) 2.8, 4.1 [69] apply. This test
        /// is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidNotSa024()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""023.ent"">
<doc></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.DocumentElement.Attributes["a1"]);
            Assert.AreEqual("v1", document.DocumentElement.Attributes["a1"].Value);
        }

        /// <summary>
        /// Test demonstrates the use of a parameter entity reference within an attribute
        /// list declaration. There is an output test associated with this input file. Here
        /// the section(s) 2.3 4.1 [10] [69] apply. This test is taken from the collection
        /// James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidNotSa023()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""023.ent"">
<doc></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.DocumentElement.Attributes["a1"]);
            Assert.AreEqual("v1", document.DocumentElement.Attributes["a1"].Value);
        }

        /// <summary>
        /// Tests doctypedecl with external subset and combinations of different markup declarations
        /// and PEReferences. There is an output test associated with this input file. Here the section(s)
        /// 2.8 apply. This test is taken from the collection IBM XML Conformance Test Suite - Production 28.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP28Ibm28v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!DOCTYPE animal SYSTEM ""ibm28v02.dtd"" [
   <!NOTATION animal_class SYSTEM ""ibm28v02.txt"">
   <!ENTITY forcat ""This is a small cat"">
   <!ELEMENT tiger (#PCDATA)>
   <!ENTITY % make_small ""<!ELEMENT small EMPTY>"">
   <!ENTITY % make_leopard_element ""<!ELEMENT leopard ANY>"">
   <!ENTITY % make_attlist ""<!ATTLIST tiger color CDATA #REQUIRED>"">
   %make_leopard_element; 
   <!ELEMENT cat ANY>
   %make_small;
   <!ENTITY % make_big ""<!ELEMENT big EMPTY>"">
   %make_big;
   %make_attlist;
   <?sound ""This is a PI"" ?>
   <!-- This is a valid test file for p28 -->
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
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests extSubset with TextDecl and extSubsetDecl in the dtd file. There is an output test
        /// associated with this input file. Here the section(s) 2.8 apply. This test is taken from
        /// the collection IBM XML Conformance Test Suite - Production 30.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP30Ibm30v02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE animal SYSTEM ""ibm30v02.dtd"">
<animal/>
<!-- tests extSubset with TextDecl and extSubsetDecl in the dtd file -->
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests EnitityValue with combination of GE, PE and text, the GE used is declared
        /// in the student.dtd. There is an output test associated with this input file.
        /// Here the section(s) 2.3 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP09Ibm09v05()
        {
            var fullname = "first , last , middle";
            var age = "21";
            var status = "freshman";

            var combine = String.Format("This is a test of My Name is {0} and my age is {1} Again {0} {0} and my status is \n\t\t{2} {2} and {0} {1} {0} {2} That is all.",
                fullname, age, status);

            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student SYSTEM ""student2.dtd""[
	<!ELEMENT student (#PCDATA)> 
	<!ENTITY Age ""21"">
	<!ENTITY Status ""freshman"">
 	<!ENTITY % FullName ""first , last , middle"">
]>

<!-- testing entity value with combination reference -->
<student>This is a test of &combine;</student>



", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
            Assert.AreEqual(combine, document.DocumentElement.TextContent);
        }

        /// <summary>
        /// Tests regular systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 11.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP11Ibm11v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student SYSTEM 'student.dtd'[
]>
<!-- testing systemliteral with a string with ""'"" -->
<student>My Name is SnowMan. </student>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests regular systemliteral using the double quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 11.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP11Ibm11v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student SYSTEM ""student.dtd"" [
]>

<!-- testing systemliteral with a string with '""' -->
<student>My Name is SnowMan. </student>

", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty systemliteral using the double quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP12Ibm12v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC """" ""student.dtd""[
]>

<!-- testing Pubid Literal with nothing between the double quote -->
<student>My Name is SnowMan. </student>







 ", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests empty systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP12Ibm12v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC '' 'student.dtd'[
]>

<!-- testing Pubid Literal with nothing between the single quotes -->
<student>My Name is SnowMan. </student>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Tests regular systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlIbmValidP12Ibm12v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC 'The latest version' 'student.dtd'[
]>

<!-- testing Pubid Literal with a string without  ""'"" inside -->
<student>My Name is SnowMan. </student>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Expands a general entity which contains a CDATA section with what looks like a
        /// markup declaration (but is just text since it's in a CDATA section). There is an
        /// output test associated with this input file. Here the section(s) 2.7 apply. This
        /// test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidNotSa031()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""031-1.ent"">
<doc>&e;</doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// external subset can be empty. Here the section(s) 2.8 [31] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP31pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p31pass1.dtd"" [<!ELEMENT doc EMPTY>]>
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid doctypedecl with EXternalID as Enternal Entity. The external entity contains a parameter
        /// entity reference and condtional sections. Here the section(s) 2.8 3.4 4.2.2 [31] [62] [63] [75]
        /// apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP31pass2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p31pass2.dtd"">
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid doctypedecl with ExternalID as an External Entity. A parameter entity reference is also used.
        /// Here the section(s) 2.8 4.1 [28] [69] apply. This test is taken from the collection OASIS/NIST
        /// TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP28pass5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p28pass5.dtd""[
<!--comment-->
<!ENTITY % rootdecl ""<!ELEMENT doc (a)>"">
<!ELEMENT a EMPTY>
]>
<doc><a/></doc>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid doctypedecl with ExternalID as an External Entity declaration. Here the section(s) 2.8 4.2.2
        /// [28] [75] apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP28pass4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p28pass4.dtd"">
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid doctypedecl with ExternalID as an External Entity. The external entity has an element declaration.
        /// Here the section(s) 2.8 4.2.2 [30] [75] apply. This test is taken from the collection OASIS/NIST TESTS,
        /// 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP30pass1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p30pass1.dtd"">
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }

        /// <summary>
        /// Valid doctypedecl with ExternalID as an Enternal Entity. The external entity begins with a Text Declaration.
        /// Here the section(s) 2.8 4.2.2 4.3.1 [30] [75] [77] apply. This test is taken from the collection OASIS/NIST
        /// TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidOP30pass2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p30pass2.dtd"">
<doc/>
", new DocumentOptions(validating: true));

            Assert.IsNotNull(document);
            Assert.IsTrue(document.IsValid);
        }
    }
}
