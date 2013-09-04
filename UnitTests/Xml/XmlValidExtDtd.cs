using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests
{
    //[TestClass]
    public class XmlValidExtDtd
    {
        /// <summary>
        /// Tests EnitityValue referencing a Parameter Entity. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test
        /// is taken from the collection IBM XML Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP09Ibm09v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student  SYSTEM ""ibm09v03.dtd"">
<student>I am a new student with &Name;</student>
");
        }

        /// <summary>
        /// Tests EnitityValue with combination of GE, PE and text, the GE used is declared
        /// in the student.dtd. There is an output test associated with this input file.
        /// Here the section(s) 2.3 apply. This test is taken from the collection IBM XML
        /// Conformance Test Suite - Production 9.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP09Ibm09v05()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student SYSTEM ""student.dtd""[
	<!ELEMENT student (#PCDATA)> 
	<!ENTITY Age ""21"">
	<!ENTITY Status ""freshman"">
 	<!ENTITY % FullName ""first , last , middle"">
]>

<!-- testing entity value with combination reference -->
<student>This is a test of &combine;</student>



");
        }

        /// <summary>
        /// Tests regular systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 11.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP11Ibm11v03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student SYSTEM 'student.dtd'[
]>
<!-- testing systemliteral with a string with ""'"" -->
<student>My Name is SnowMan. </student>
");
        }

        /// <summary>
        /// Tests regular systemliteral using the double quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 11.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP11Ibm11v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student SYSTEM ""student.dtd"" [
]>

<!-- testing systemliteral with a string with '""' -->
<student>My Name is SnowMan. </student>

");
        }

        /// <summary>
        /// Tests empty systemliteral using the double quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP12Ibm12v01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC """" ""student.dtd""[
]>

<!-- testing Pubid Literal with nothing between the double quote -->
<student>My Name is SnowMan. </student>







 ");
        }

        /// <summary>
        /// Tests empty systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP12Ibm12v02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC '' 'student.dtd'[
]>

<!-- testing Pubid Literal with nothing between the single quotes -->
<student>My Name is SnowMan. </student>
");
        }

        /// <summary>
        /// Tests regular systemliteral using the single quotes. There is an output test
        /// associated with this input file. Here the section(s) 2.3 apply. This test is
        /// taken from the collection IBM XML Conformance Test Suite - Production 12.
        /// </summary>
        [TestMethod]
        public void XmlValidIbmValidP12Ibm12v04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
<!DOCTYPE student PUBLIC 'The latest version' 'student.dtd'[
]>

<!-- testing Pubid Literal with a string without  ""'"" inside -->
<student>My Name is SnowMan. </student>
");
        }

        /// <summary>
        /// Expands a general entity which contains a CDATA section with what looks like a
        /// markup declaration (but is just text since it's in a CDATA section). There is an
        /// output test associated with this input file. Here the section(s) 2.7 apply. This
        /// test is taken from the collection James Clark XMLTEST cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        public void XmlValidValidNotSa031()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""031-1.ent"">
<doc>&e;</doc>
");
        }
    }
}
