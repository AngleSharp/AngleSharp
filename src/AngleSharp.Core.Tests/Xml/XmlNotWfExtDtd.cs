using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.Xml;

namespace UnitTests
{
    [TestClass]
    public class XmlNotWfExtDtd
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
        /// Text declarations (which optionally begin any external entity) are
        /// required to have "encoding=...". Here the section(s) 4.3.1 [77] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfDtd07()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root SYSTEM ""dtd07.dtd"" [
    <!ELEMENT root EMPTY>
]>
<root/>
");
        }

        /// <summary>
        /// Text declarations (which optionally begin any external entity) are required
        /// to have "encoding=...". Here the section(s) 4.3.1 [77] apply. This test is taken
        /// from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding07()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!--
	reusing this entity; it's got no markup decls,
	so it's legal except for a missing ""encoding=..."".
    -->
    <!ENTITY empty SYSTEM ""dtd07.dtd"">
]>
<root>&empty;</root>
");
        }

        /// <summary>
        /// Only INCLUDE and IGNORE are conditional section keywords. Here the section(s) 3.4 [61] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfCond01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root SYSTEM ""cond.dtd"" [
    <!ENTITY % MAYBE ""CDATA"">
]>

<root/>
");
        }

        /// <summary>
        /// no other types, including TEMP, which is valid in SGML Here the section(s) 3.4 [61] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP61fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p61fail1.dtd"">
<doc/>");
        }

        /// <summary>
        /// INCLUDE must be upper case Here the section(s) 3.4 [62] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP62fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p62fail1.dtd"">
<doc/>");
        }

        /// <summary>
        /// no spaces in terminating delimiter Here the section(s) 3.4 [62] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP62fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p62fail2.dtd"">
<doc/>");
        }

        /// <summary>
        /// IGNORE must be upper case Here the section(s) 3.4 [63] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP63fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p63fail1.dtd"">
<doc/>");
        }

        /// <summary>
        /// delimiters must be balanced Here the section(s) 3.4 [63] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP63fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p63fail2.dtd"">
<doc/>");
        }

        /// <summary>
        /// section delimiters must balance Here the section(s) 3.4 [64] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP64fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p64fail1.dtd"">
<doc/>
");
        }

        /// <summary>
        /// section delimiters must balance Here the section(s) 3.4 [64] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP64fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc SYSTEM ""p64fail2.dtd"">
<doc/>
");
        }
    }
}
