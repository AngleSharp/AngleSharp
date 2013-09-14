using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.Xml;

namespace UnitTests
{
    /// <summary>
    /// (Conformance) Tests taken from
    /// http://www.w3.org/XML/Test/xmlconf-20031210.html
    [TestClass]
    public class XmlNotWfDocuments
    {
        /// <summary>
        /// Illegal character " " in encoding name Here the section(s) 4.3.3 [81] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding01()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding="" utf-8""?>
<root/>
");
        }

        /// <summary>
        /// Illegal character "/" in encoding name Here the section(s) 4.3.3 [81] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding02()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""a/b""?>
<root/>

");
        }

        /// <summary>
        /// Illegal character reference in encoding name Here the section(s) 4.3.3 [81] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding03()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""just&#41;word""?>
<root/>

");
        }

        /// <summary>
        /// Illegal character ":" in encoding name Here the section(s) 4.3.3 [81] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding04()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf:8""?>
<root/>

");
        }

        /// <summary>
        /// Illegal character "@" in encoding name Here the section(s) 4.3.3 [81] apply. 
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding05()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""@import(sys-encoding)""?>
<root/>

");
        }

        /// <summary>
        /// Illegal character "+" in encoding name Here the section(s) 4.3.3 [81] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfEncoding06()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""XYZ+999""?>

<!-- WF ... but illegal encoding name, also a fatal error --> 

<root/>
");
        }

        /// <summary>
        /// Value is required Here the section(s) 4.2 [73] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP73fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge >
]>
<doc/>
");
        }

        /// <summary>
        /// No NDataDecl without value Here the section(s) 4.2 [73] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP73fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge NDATA unknot>
]>
<doc/>
");
        }

        /// <summary>
        /// No NData Decls on parameter entities. Here the section(s) 4.2 [74] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP74fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY % pe SYSTEM ""nop.ent"" NDATA unknot>
]>
<doc/>
");
        }

        /// <summary>
        /// value is required Here the section(s) 4.2 [74] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP74fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY % pe>
]>
<doc/>
");
        }

        /// <summary>
        /// only one value Here the section(s) 4.2 [74] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP74fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY % pe ""<!--decl1-->"" SYSTEM ""nop.ent"">
]>
<doc/>
");
        }

        /// <summary>
        /// S is required after name Here the section(s) 4.2 [72] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP72fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY % pe""<!--replacement decl-->"">
]>
<doc/>
");
        }

        /// <summary>
        /// Entity name is a name, not an NMToken Here the section(s) 4.2 [72] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP72fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY % .pe ""<!--replacement decl-->"">
]>
<doc/>
");
        }

        /// <summary>
        /// No typed replacement text. Here the section(s) 4.2 [73] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP73fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge CDATA ""replacement text"">
]>
<doc/>
");
        }

        /// <summary>
        /// Only one replacement value. Here the section(s) 4.2 [73] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP73fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge ""replacement text"" ""more text"">
]>
<doc/>
");
        }

        /// <summary>
        /// No NDataDecl on replacement text. Here the section(s) 4.2 [73] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP73fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge ""replacement text"" NDATA unknot>
]>
<doc/>
");
        }

        /// <summary>
        /// S is required after '%'. Here the section(s) 4.2 [72] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP72fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY %pe ""<!--replacement decl-->"">
]>
<doc/>
");
        }

        /// <summary>
        /// Notation name is required. Here the section(s) 4.2.2 [76] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP76fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge SYSTEM ""nop.ent"" NDATA>
]>
<doc/>
");
        }

        /// <summary>
        /// notation names are Names Here the section(s) 4.2.2 [76] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP76fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!--error should be reported here, not at <!Notation-->
<!ENTITY ge SYSTEM ""nop.ent"" NDATA -unknot>
<!NOTATION -unknot PUBLIC ""Unknown"">
]>
<doc/>
");
        }

        /// <summary>
        /// This is neither Here the section(s) 4.2 [70] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP70fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY & bad ""replacement text"">
]>
<doc/>
");
        }

        /// <summary>
        /// S is required before EntityDef Here the section(s) 4.2 [71] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP71fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY ge""replacement text"">
]>
<doc/>
");
        }

        /// <summary>
        /// Entity name is a Name, not an NMToken Here the section(s) 4.2 [71] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP71fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY -ge ""replacement text"">
]>
<doc/>
");
        }

        /// <summary>
        /// "SYSTEM" implies only one literal Here the section(s) 4.2.2 [75] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP75fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY ent SYSTEM ""PublicID"" ""nop.ent"">
]>
<doc/>
");
        }

        /// <summary>
        /// only one keyword Here the section(s) 4.2.2 [75] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP75fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY ent PUBLIC ""PublicID"" SYSTEM ""nop.ent"">
]>
<doc/>
");
        }

        /// <summary>
        /// "PUBLIC" requires two literals (contrast with SGML) Here the section(s)
        /// 4.2.2 [75] apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP75fail6()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY ent PUBLIC ""PublicID"">
]>
<doc/>
");
        }

        /// <summary>
        /// S is required before "NDATA" Here the section(s) 4.2.2 [76] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP76fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge SYSTEM ""nop.ent""NDATA unknot>
]>
<doc/>
");
        }

        /// <summary>
        /// "NDATA" is upper-case Here the section(s) 4.2.2 [76] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP76fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!NOTATION unknot PUBLIC ""Unknown"">
<!ENTITY ge SYSTEM ""nop.ent"" ndata unknot>
]>
<doc/>
");
        }

        /// <summary>
        /// S required after "PUBLIC". Here the section(s) 4.2.2 [75] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP75fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY ent PUBLIC""PublicID"" ""nop.ent"">
]>
<doc/>
");
        }

        /// <summary>
        /// S required after "SYSTEM" Here the section(s) 4.2.2 [75] apply
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP75fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY ent SYSTEM""nop.ent"">
]>
<doc/>
");
        }

        /// <summary>
        /// S required between literals Here the section(s) 4.2.2 [75] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP75fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ENTITY ent PUBLIC ""PublicID""""nop.ent"">
]>
<doc/>
");
        }

        /// <summary>
        /// terminating ';' is required Here the section(s) 4.1 [69] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP69fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY % pe ""<!---->"">
%pe<!---->
]>
<doc/>
");
        }

        /// <summary>
        /// no S after '%' Here the section(s) 4.1 [69] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP69fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY % pe ""<!---->"">
% pe;
]>
<doc/>
");
        }

        /// <summary>
        /// no S before ';' Here the section(s) 4.1 [69] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP69fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY % pe ""<!---->"">
%pe ;
]>
<doc/>
");
        }

        /// <summary>
        /// PUBLIC literal must be quoted Here the section(s) 4.2.2 [75] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfDtd04()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
    <!-- PUBLIC id must be quoted -->
    <!ENTITY foo PUBLIC -//BadCorp//DTD-foo-1.0//EN ""elvis.ent"">
]>

<root/>
");
        }

        /// <summary>
        /// SYSTEM identifier must be quoted Here the section(s) 4.2.2 [75] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfDtd05()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
    <!-- SYSTEM id must be quoted -->
    <!ENTITY foo SYSTEM elvis.ent>
]>

<root/>
");
        }

        /// <summary>
        /// terminating ';' is required Here the section(s) 4.1 [66] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP66fail1()
        {
            var document = DocumentBuilder.Xml(@"<doc>&#65</doc>");
        }

        /// <summary>
        /// no S after '&#' Here the section(s) 4.1 [66] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP66fail2()
        {
            var document = DocumentBuilder.Xml(@"<doc>&# 65;</doc>");
        }

        /// <summary>
        /// no hex digits in numeric reference Here the section(s) 4.1 [66] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP66fail3()
        {
            var document = DocumentBuilder.Xml(@"<doc>&#A;</doc>");
        }

        /// <summary>
        /// only hex digits in hex references Here the section(s) 4.1 [66] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP66fail4()
        {
            var document = DocumentBuilder.Xml(@"<doc>&#x4G;</doc>");
        }

        /// <summary>
        /// No references to non-characters. Here the section(s) 4.1 [66] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP66fail5()
        {
            var document = DocumentBuilder.Xml(@"<doc>&#5;</doc>");
        }

        /// <summary>
        /// no references to non-characters Here the section(s) 4.1 [66] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP66fail6()
        {
            var document = DocumentBuilder.Xml(@"<doc>&#xd802;&#xdc02;</doc>");
        }

        /// <summary>
        /// terminating ';' is required Here the section(s) 4.1 [68] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP68fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY ent ""replacement text"">
]>
<doc>
&ent
</doc>
");
        }

        /// <summary>
        /// no S after '&' Here the section(s) 4.1 [68] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP68fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY ent ""replacement text"">
]>
<doc>
& ent;
</doc>
");
        }

        /// <summary>
        /// no S before ';' Here the section(s) 4.1 [68] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP68fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY ent ""replacement text"">
]>
<doc>
&ent ;
</doc>
");
        }

        /// <summary>
        /// PE name immediately after "%" Here the section(s) 4.1 [69] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfDtd02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
    <!-- correct PE ref syntax -->
    <!ENTITY % foo ""<!ATTLIST root>"">
    % foo;
]>

<root/>
");
        }

        /// <summary>
        /// PE name immediately followed by ";" Here the section(s) 4.1 [69] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfDtd03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
    <!-- correct PE ref syntax -->
    <!ENTITY % foo ""<!ATTLIST root>"">
    %foo
    ;
]>

<root/>

");
        }

        /// <summary>
        /// Must have keyword in conditional sections Here the section(s) 3.4 [61] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfCond02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>
    <!-- correct PE ref syntax -->
    <!ENTITY % foo ""<!ATTLIST root>"">
    %foo
    ;
]>

<root/>

");
        }

        /// <summary>
        /// SGML-ism: omitted end tag for EMPTY content Here the section(s) 3 [39] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  omitted end tag -->
]>

<root>
");
        }

        /// <summary>
        /// start-tag requires end-tag Here the section(s) 3 [39] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP39fail1()
        {
            var document = DocumentBuilder.Xml(@"<doc>content");
        }

        /// <summary>
        /// end-tag requires start-tag Here the section(s) 3 [39] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP39fail2()
        {
            var document = DocumentBuilder.Xml(@"<doc>content</a></doc>");
        }

        /// <summary>
        /// XML documents contain one or more elements. Here the section(s) 3 [39] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP39fail3()
        {
            var document = DocumentBuilder.Xml(@"");
        }

        /// <summary>
        /// A name is required. Here the section(s) 3.3 [52] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP52fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST  >
]>
<doc/>");
        }

        /// <summary>
        /// A name is required. Here the section(s) 3.3 [52] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP52fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST>
]>
<doc/>");
        }

        /// <summary>
        /// S is required before default. Here the section(s) 3.3 [53] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP53fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA#IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// S is required before type. Here the section(s) 3.3 [53] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP53fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att(a|b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Type is required. Here the section(s) 3.3 [53] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP53fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Default is required. Here the section(s) 3.3 [53] apply. This test is taken from
        /// the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP53fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA>
]>
<doc/>
");
        }

        /// <summary>
        /// Name is requried. Here the section(s) 3.3 [53] apply. This test is taken from
        /// the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP53fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc (a|b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Comma doesn't separate enumerations, unlike in SGML. Here the section(s) 3.3.1 [59]
        /// apply. This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	choice	(a,b,c)	""a""
	>

]>

<root/>

");
        }

        /// <summary>
        /// at least one required Here the section(s) 3.3.1 [59] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP59fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att () #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// separator must be "," Here the section(s) 3.3.1 [59] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP59fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att (a,b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// values are unquoted Here the section(s) 3.3.1 [59] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP59fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att (""a"") #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// keywords must be upper case Here the section(s) 3.3.2 [60] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP60fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA #implied>
]>
<doc/>
");
        }

        /// <summary>
        /// S is required after #FIXED Here the section(s) 3.3.2 [60] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP60fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA #FIXED""value"">
]>
<doc/>
");
        }

        /// <summary>
        /// only #FIXED has both keyword and value Here the section(s) 3.3.2 [60] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP60fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA #REQUIRED ""value"">
]>
<doc att=""value""/>
");
        }

        /// <summary>
        /// #FIXED required value Here the section(s) 3.3.2 [60] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP60fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA #FIXED>
]>
<doc/>
");
        }

        /// <summary>
        /// only one default type Here the section(s) 3.3.2 [60] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP60fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att CDATA #IMPLIED #REQUIRED>
]>
<doc att=""value""/>
");
        }

        /// <summary>
        /// ATTLIST declarations apply to only one element, unlike SGML Here the section(s) 3.3 [52] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml04()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  multiple attlist types -->

    <!ELEMENT root EMPTY>
    <!ELEMENT branch EMPTY>

    <!ATTLIST (root|branch)
	TreeType CDATA #REQUIRED
	>
]>

<root/>
");
        }

        /// <summary>
        /// ATTLIST declarations are never global, unlike in SGML Here the section(s) 3.3 [52] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml06()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- Web-SGML-ism:  global attlist types -->

    <!ELEMENT root EMPTY>

    <!ATTLIST #ALL
	TreeType CDATA #REQUIRED
	>
]>

<root/>
");
        }

        /// <summary>
        /// SGML's #CURRENT is not allowed. Here the section(s) 3.3.1 [56] apply. This test is
        /// taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist08()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute default -->

    <!ATTLIST root
	language	CDATA	#CURRENT
	>

]>

<root/>
");
        }

        /// <summary>
        /// SGML's #CONREF is not allowed. Here the section(s) 3.3.1 [56] apply. This test
        /// is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist09()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  illegal attribute default -->

    <!ATTLIST root
	language	CDATA	#CONREF
	>

]>

<root language=""Dutch""/>

");
        }

        /// <summary>
        /// no IDS type Here the section(s) 3.3.1 [56] apply. This test is taken from
        /// the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP56fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att IDS #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// no NUMBER type Here the section(s) 3.3.1 [56] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP56fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att NUMBER #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// no NAME type Here the section(s) 3.3.1 [56] apply. This test is taken from
        /// the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP56fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att NAME #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// no ENTITYS type - types must be upper case Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP56fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att ENTITYS #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// types must be upper case Here the section(s) 3.3.1 [56] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP56fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att id #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// no keyword for NMTOKEN enumeration Here the section(s) 3.3.1 [57] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP57fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att NMTOKEN (a|b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// at least one value required Here the section(s) 3.3.1 [58] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!NOTATION b SYSTEM ""b"">
<!ATTLIST doc att NOTATION () #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Separator must be '|' Here the section(s) 3.3.1 [58] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!NOTATION b SYSTEM ""b"">
<!ATTLIST doc att NOTATION (a,b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Notations are NAMEs, not NMTOKENs -- note: Leaving the invalid notation undeclared would
        /// cause a validating parser to fail without checking the name syntax, so the notation is
        /// declared with an invalid name. A parser that reports error positions should report an error
        /// at the AttlistDecl on line 6, before reaching the notation declaration. Here the section(s)
        /// 3.3.1 [58] apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!--should fail at this AttlistDecl, before NOTATION decl-->
<!ATTLIST doc att NOTATION (a|0b) #IMPLIED>



<!NOTATION 0b SYSTEM ""0b"">
]>
<doc/>
");
        }

        /// <summary>
        /// NOTATION must be upper case Here the section(s) 3.3.1 [58] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!NOTATION b SYSTEM ""b"">
<!ATTLIST doc att notation (a|b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// S after keyword is required Here the section(s) 3.3.1 [58] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!NOTATION b SYSTEM ""b"">
<!ATTLIST doc att NOTATION(a|b) #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Parentheses are require Here the section(s) 3.3.1 [58] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail6()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!ATTLIST doc att NOTATION a #IMPLIED>
]>
<doc/>
");
        }
        /// <summary>
        /// Values are unquoted Here the section(s) 3.3.1 [58] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail7()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!ATTLIST doc att NOTATION ""a"" #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Values are unquoted Here the section(s) 3.3.1 [58] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP58fail8()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!NOTATION a SYSTEM ""a"">
<!ATTLIST doc att NOTATION (""a"") #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Don't pass unknown attribute types Here the section(s) 3.3.1 [54] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP54fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att DUNNO #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// Must be upper case Here the section(s) 3.3.1 [55] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP55fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ATTLIST doc att cdata #IMPLIED>
]>
<doc/>
");
        }

        /// <summary>
        /// SGML's NUTOKEN is not allowed. Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	number	NUTOKEN	""1""
	>

]>

<root/>
");
        }

        /// <summary>
        /// SGML's NUTOKENS attribute type is not allowed. Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	number	NUTOKENS	""1 2 3""
	>

]>

<root/>

");
        }

        /// <summary>
        /// SGML's NUMBER attribute type is not allowed. Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist04()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	number	NUMBER	""1""
	>

]>

<root/>

");
        }

        /// <summary>
        /// SGML's NUMBERS attribute type is not allowed. Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist05()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	numbers	NUMBERS	""1 2 3 4""
	>

]>

<root/>

");
        }

        /// <summary>
        /// SGML's NAME attribute type is not allowed. Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist06()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	number	NAME	""Elvis""
	>

]>

<root/>

");
        }

        /// <summary>
        /// SGML's NAMES attribute type is not allowed. Here the section(s) 3.3.1 [56] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist07()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!ELEMENT root EMPTY>

    <!-- SGML-ism:  illegal attribute types -->

    <!ATTLIST root
	number	NAMES	""The King""
	>

]>

<root/>

");
        }

        /// <summary>
        /// occurrence on #PCDATA group must be *. Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)+>
]>
<doc/>");
        }

        /// <summary>
        /// #PCDATA must come first. Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ELEMENT a (doc|#PCDATA)*>
]>
<doc/>");
        }

        /// <summary>
        /// occurrence on #PCDATA group must be *. Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ELEMENT a (#PCDATA|doc)?>
]>
<doc/>");
        }

        /// <summary>
        /// Only '|' connectors. Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ELEMENT a (#PCDATA|doc,a?)*>
]>
<doc/>");
        }

        /// <summary>
        /// Only '|' connectors and occurrence on #PCDATA group must be *. Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail6()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ELEMENT a (#PCDATA,doc,a?)*>
]>
<doc/>");
        }

        /// <summary>
        /// No nested groups. Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail7()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ELEMENT a (#PCDATA|(doc|a))*>
]>
<doc/>");
        }

        /// <summary>
        /// ELEMENT declarations apply to only one element, unlike SGML. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml05()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  multiple element types -->

    <!ELEMENT root EMPTY>
    <!ELEMENT leaves EMPTY>
    <!ELEMENT branch EMPTY>

    <!ELEMENT (bush|tree) (root,leaves,branch)>
]>

<root/>

");
        }

        /// <summary>
        /// SGML Tag minimization specifications are not allowed. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml07()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  omitted tag minimzation spec -->
    <!ELEMENT root - o EMPTY>
]>

<root/>
");
        }

        /// <summary>
        /// SGML Tag minimization specifications are not allowed. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml08()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  omitted tag minimzation spec -->
    <!ELEMENT root - - EMPTY>
]>

<root/>

");
        }

        /// <summary>
        /// SGML Content model exception specifications are not allowed. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml09()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  exception spec -->

    <!ELEMENT footnote (para*) -footnote>
]>

<root/>

");
        }

        /// <summary>
        /// SGML Content model exception specifications are not allowed. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml10()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  exception spec -->
    <!ELEMENT section (header,(para|section))* +(annotation|todo)>
]>

<root/>

");
        }

        /// <summary>
        /// ELEMENT must be upper case. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP45fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!element doc EMPTY>
]>
<doc/>");
        }

        /// <summary>
        /// S before contentspec is required. Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP45fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc(#PCDATA)>
]>
<doc/>");
        }

        /// <summary>
        /// only one content spec Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP45fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT (doc|a) (#PCDATA)>
]>
<doc/>");
        }

        /// <summary>
        /// no comments in declarations (contrast with SGML) Here the section(s) 3.2 [45] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP45fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA) --bad comment-->
]>
<doc/>");
        }

        /// <summary>
        /// CDATA is not a valid content model spec Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml11()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  CDATA content type -->
    <!ELEMENT ROOT CDATA>
]>

<root/>

");
        }

        /// <summary>
        /// RCDATA is not a valid content model spec Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml12()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  RCDATA content type -->
    <!ELEMENT ROOT RCDATA>
]>

<root/>


");
        }

        /// <summary>
        /// No parens on declared content. Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP46fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (#EMPTY)>
]>
<doc/>");
        }

        /// <summary>
        /// No inclusions (contrast with SGML). Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP46fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (#PCDATA) +(doc)>
]>
<doc/>");
        }

        /// <summary>
        /// No exclusions (contrast with SGML). Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP46fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (#PCDATA) -(doc)>
]>
<doc/>");
        }

        /// <summary>
        /// No space before occurrence. Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP46fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc) +>
]>
<doc/>");
        }

        /// <summary>
        /// single group Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP46fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (#PCDATA)(doc)>
]>
<doc/>");
        }

        /// <summary>
        /// can't be both declared and modeled Here the section(s) 3.2 [46] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP46fail6()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a EMPTY (doc)>
]>
<doc/>");
        }

        /// <summary>
        /// Illegal comment in Empty element tag. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP44fail3()
        {
            var document = DocumentBuilder.Xml(@"<doc --bad comment--/>");
        }

        /// <summary>
        /// Whitespace required between attributes. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP44fail4()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""att2=""val2""/>");
        }

        /// <summary>
        /// Duplicate attribute name is illegal. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP44fail5()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val"" att=""val""/>");
        }

        /// <summary>
        /// SGML Unordered content models not allowed Here the section(s) 3.2.1 [47] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml13()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- SGML-ism:  unordered content type -->
    <!ELEMENT ROOT (a & b & c)>
    <!ELEMENT a EMPTY>
    <!ELEMENT b EMPTY>
    <!ELEMENT c EMPTY>
]>

<root><b/><c/><a/></root>


");
        }
        /// <summary>
        /// Invalid operator '|' must match previous operator ',' Here the section(s) 3.2.1 [47] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP47fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc,a?|a?)>
]>
<doc/>");
        }

        /// <summary>
        /// Illegal character '-' in Element-content model Here the section(s) 3.2.1 [47] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP47fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc)->
]>
<doc/>");
        }

        /// <summary>
        /// Optional character must follow a name or list Here the section(s) 3.2.1 [47] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP47fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a *(doc)>
]>
<doc/>");
        }

        /// <summary>
        /// Illegal space before optional character Here the section(s) 3.2.1 [47] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP47fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc) ?>
]>
<doc/>");
        }

        /// <summary>
        /// No whitespace before "?" in content model Here the section(s) 3.2.1 [48] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfContent01()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- no whitespace before '?', '*', '+' -->
    <!ELEMENT root ((root) ?)>
]>
<root/>
");
        }

        /// <summary>
        /// No whitespace before "*" in content model Here the section(s) 3.2.1 [48] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfContent02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- no whitespace before '?', '*', '+' -->
    <!ELEMENT root ((root) *)>
]>
<root/>

");
        }
        /// <summary>
        /// No whitespace before "+" in content model Here the section(s) 3.2.1 [48] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfContent03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
    <!-- no whitespace before '?', '*', '+' -->
    <!ELEMENT root (root +)>
]>
<root/>

");
        }

        /// <summary>
        /// Illegal space before optional character Here the section(s) 3.2.1 [48] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP48fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc *)>
]>
<doc/>");
        }

        /// <summary>
        /// Illegal space before optional character Here the section(s) 3.2.1 [48] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP48fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a ((doc|a?) +)>
]>
<doc/>");
        }

        /// <summary>
        /// connectors must match Here the section(s) 3.2.1 [49] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP49fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc|a?,a?)>
<doc/>");
        }

        /// <summary>
        /// connectors must match Here the section(s) 3.2.1 [50] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP50fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc ANY>
<!ELEMENT a (doc,a?|a?)>
<doc/>");
        }

        /// <summary>
        /// occurrence on #PCDATA group must be * Here the section(s) 3.2.2 [51] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP51fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)?>
]>
<doc/>");
        }

        /// <summary>
        /// quote types must match. Here the section(s) 2.9 [32] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP32fail1()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" standalone='yes""?>
<doc/>
");
        }
        /// <summary>
        /// quote types must match. Here the section(s) 2.9 [32] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP32fail2()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" standalone=""yes'?>
<doc/>
");
        }
        /// <summary>
        /// initial S is required. Here the section(s) 2.9 [32] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP32fail3()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""standalone=""yes""?>
<doc/>
");
        }
        /// <summary>
        /// quotes are required. Here the section(s) 2.9 [32] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP32fail4()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" standalone=yes?>
<doc/>
");
        }
        /// <summary>
        /// yes or no must be lower case. Here the section(s) 2.9 [32] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP32fail5()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" standalone=""YES""?>
<doc/>
");
        }

        /// <summary>
        /// Whitespace required between attributes. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist10()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root ANY>
<!ATTLIST root att1 CDATA #IMPLIED>
<!ATTLIST root att2 CDATA #IMPLIED>
]>
<root att1=""value1""att2=""value2"">
    <!-- whitespace required between attributes -->
</root>
");
        }

        /// <summary>
        /// S is required between attributes. Here the section(s) 3.1 [40] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP40fail1()
        {
            var document = DocumentBuilder.Xml(@"<doc att=""val""att2=""val2""></doc>");
        }

        /// <summary>
        /// tags start with names, not nmtokens. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP40fail2()
        {
            var document = DocumentBuilder.Xml(@"<3notname></3notname>");
        }

        /// <summary>
        /// tags start with names, not nmtokens. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP40fail3()
        {
            var document = DocumentBuilder.Xml(@"<3notname></notname>");
        }

        /// <summary>
        /// no space before name. Here the section(s) 3.1 [40] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP40fail4()
        {
            var document = DocumentBuilder.Xml(@"< doc></doc>");
        }

        /// <summary>
        /// quotes are required (contrast with SGML) Here the section(s) 3.1 [41] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP41fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc att (val|val2)>
]>
<doc att=val></doc>");
        }
        /// <summary>
        /// attribute name is required (contrast with SGML). Here the section(s) 3.1 [41] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP41fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc att (val|val2)>
]>
<doc val></doc>");
        }
        /// <summary>
        /// Eq required. Here the section(s) 3.1 [41] apply. This test is taken from the
        /// collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP41fail3()
        {
            var document = DocumentBuilder.Xml(@"<doc att ""val""></doc>");
        }

        /// <summary>
        /// EOF in middle of incomplete ETAG Here the section(s) 3.1 [42] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfElement00()
        {
            var document = DocumentBuilder.Xml(@"<root>
    Incomplete end tag.
</ro");
        }

        /// <summary>
        /// EOF in middle of incomplete ETAG. Here the section(s) 3.1 [42] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfElement01()
        {
            var document = DocumentBuilder.Xml(@"<root>
    Incomplete end tag.
</root");
        }

        /// <summary>
        /// no space before name. Here the section(s) 3.1 [42] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP42fail1()
        {
            var document = DocumentBuilder.Xml(@"<doc></ doc>");
        }

        /// <summary>
        /// cannot end with "/>". Here the section(s) 3.1 [42] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP42fail2()
        {
            var document = DocumentBuilder.Xml(@"<doc></doc/>");
        }

        /// <summary>
        /// no NET (contrast with SGML), Here the section(s) 3.1 [42] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP42fail3()
        {
            var document = DocumentBuilder.Xml(@"<doc/doc/");
        }

        /// <summary>
        /// Illegal markup (&lt;%@ ... %&gt;). Here the section(s) 3.1 [43] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfElement02()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE html [ <!ELEMENT html ANY> ]>
<html>
    <% @ LANGUAGE=""VBSCRIPT"" %>
</html>
");
        }

        /// <summary>
        /// Illegal markup (&lt;% ... %&gt;). Here the section(s) 3.1 [43] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfElement03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE html [ <!ELEMENT html ANY> ]>
<html>
    <% document.println (""hello, world""); %>
</html>

");
        }

        /// <summary>
        /// Illegal markup (&lt;!ELEMENT ... >). Here the section(s) 3.1 [43] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfElement04()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [ <!ELEMENT root ANY> ]>
<root>
    <!ELEMENT foo EMPTY>
</root>
");
        }

        /// <summary>
        /// no non-comment declarations. Here the section(s) 3.1 [43] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP43fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE elem
[
<!ELEMENT elem (#PCDATA|elem)*>
<!ENTITY ent ""<elem>CharData</elem>"">
]>
<elem>
<!ENTITY badent ""bad"">
</elem>
");
        }

        /// <summary>
        /// no conditional sections. Here the section(s) 3.1 [43] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP43fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE elem
[
<!ELEMENT elem (#PCDATA|elem)*>
<!ENTITY ent ""<elem>CharData</elem>"">
]>
<elem>
<![IGNORE[This was valid in SGML, but not XML]]>
</elem>
");
        }

        /// <summary>
        /// no conditional sections. Here the section(s) 3.1 [43] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP43fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE elem
[
<!ELEMENT elem (#PCDATA|elem)*>
<!ENTITY ent ""<elem>CharData</elem>"">
]>
<elem>
<![INCLUDE[This was valid in SGML, but not XML]]>
</elem>
");
        }

        /// <summary>
        /// Whitespace required between attributes. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfAttlist11()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root ANY>
<!ATTLIST root att1 CDATA #IMPLIED>
<!ATTLIST root att2 CDATA #IMPLIED>
]>
<root att1=""value1""att2=""value2""/>
    <!-- whitespace required between attributes -->
");
        }

        /// <summary>
        /// Illegal space before Empty element tag. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP44fail1()
        {
            var document = DocumentBuilder.Xml(@"< doc/>");
        }

        /// <summary>
        /// Illegal space after Empty element tag. Here the section(s) 3.1 [44] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP44fail2()
        {
            var document = DocumentBuilder.Xml(@"<doc/ >");
        }

        /// <summary>
        /// no S after "&lt;!". Here the section(s) 4.2 [71] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP71fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<! ENTITY ge ""replacement text"">
]>
<doc/>
");
        }

        /// <summary>
        /// S is required after "&lt;!ENTITY". Here the section(s) 4.2 [71] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP71fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITYge ""replacement text"">
]>
<doc/>
");
        }

        /// <summary>
        /// S is required after "&lt;!ENTITY". Here the section(s) 4.2 [72] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP72fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc (#PCDATA)>
<!ENTITY% pe ""<!--replacement decl-->"">
]>
<doc/>
");
        }

        /// <summary>
        /// incomplete character reference. Here the section(s) 2.3 [9] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP09fail3()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ENTITY % ent1 ""asdf&#65"">
]>
<doc/>");
        }

        /// <summary>
        /// quote types must match. Here the section(s) 2.3 [9] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP09fail4()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ENTITY % ent1 'a"">
]>
<doc/>");
        }

        /// <summary>
        /// quote types must match. Here the section(s) 2.3 [9] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP09fail5()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
<!ENTITY % ent1 ""a'>
]>
<doc/>");
        }

        /// <summary>
        /// '&lt;' excluded. Here the section(s) 2.4 [14] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP14fail1()
        {
            var document = DocumentBuilder.Xml(@"<doc>< </doc>
");
        }

        /// <summary>
        /// '&' excluded. Here the section(s) 2.4 [14] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP14fail2()
        {
            var document = DocumentBuilder.Xml(@"<doc>& </doc>
");
        }

        /// <summary>
        /// "]]>" excluded. Here the section(s) 2.4 [14] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP14fail3()
        {
            var document = DocumentBuilder.Xml(@"<doc>a]]>b</doc>
");
        }

        /// <summary>
        /// Comments may not contain "--". Here the section(s) 2.5 [15] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml03()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [ <!ELEMENT root EMPTY> ]>

    <!-- SGML-ism:  -- inside comment -->
<root/>
");
        }

        /// <summary>
        /// comments can't end in '-'. Here the section(s) 2.5 [15] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP15fail1()
        {
            var document = DocumentBuilder.Xml(@"<!--a--->
<doc/>");
        }

        /// <summary>
        /// one comment per comment (contrasted with SGML). Here the section(s) 2.5 [15]
        /// apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP15fail2()
        {
            var document = DocumentBuilder.Xml(@"<!-- -- -- -->
<doc/>");
        }

        /// <summary>
        /// can't include 2 or more adjacent '-'s. Here the section(s) 2.5 [15] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP15fail3()
        {
            var document = DocumentBuilder.Xml(@"<!-- --- -->
<doc/>");
        }

        /// <summary>
        /// No space between PI target name and data. Here the section(s) 2.6 [16] apply.
        /// This test is taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfPi()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE root [
<!ELEMENT root EMPTY>
<!-- space before PI data and ?> -->
<?bad-pi+?>
]>
<root/>
");
        }

        /// <summary>
        /// "xml" is an invalid PITarget. Here the section(s) 2.6 [16] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP16fail1()
        {
            var document = DocumentBuilder.Xml(@"<?pitarget?>
<?xml?>
<doc/>");
        }

        /// <summary>
        /// a PITarget must be present. Here the section(s) 2.6 [16] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP16fail2()
        {
            var document = DocumentBuilder.Xml(@"<??>
<doc/>");
        }

        /// <summary>
        /// no space before "CDATA". Here the section(s) 2.7 [18] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP18fail1()
        {
            var document = DocumentBuilder.Xml(@"<doc><![ CDATA[a]]></doc>");
        }

        /// <summary>
        /// no space after "CDATA". Here the section(s) 2.7 [18] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP18fail2()
        {
            var document = DocumentBuilder.Xml(@"<doc><![CDATA [a]]></doc>");
        }

        /// <summary>
        /// CDSect's can't nest. Here the section(s) 2.7 [18] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP18fail3()
        {
            var document = DocumentBuilder.Xml(@"<doc>
<![CDATA[
<![CDATA[XML doesn't allow CDATA sections to nest]]>
]]>
</doc>");
        }

        /// <summary>
        /// This refers to an undefined parameter entity reference within a markup declaration
        /// in the internal DTD subset, violating the PEs in Internal Subset WFC. Here the
        /// section(s) 2.8 apply. This test is taken from the collection James Clark XMLTEST
        /// cases, 18-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfValidSa094()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ENTITY % e ""foo"">
<!ELEMENT doc (#PCDATA)>
<!ATTLIST doc a1 CDATA ""%e;"">
]>
<doc></doc>
");
        }

        /// <summary>
        /// XML declaration must be at the very beginning of a document; it"s not a
        /// processing instruction. Here the section(s) 2.8  apply. This test is
        /// taken from the collection Sun Microsystems XML Tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfSgml02()
        {
            var document = DocumentBuilder.Xml(@" <?xml version=""1.0""?>
    <!-- SGML-ism:  XML PI not at beginning -->
<!DOCTYPE root [ <!ELEMENT root EMPTY> ]>
<root/>
");
        }

        /// <summary>
        /// prolog must start with XML decl. Here the section(s) 2.8 [22] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP22fail1()
        {
            var document = DocumentBuilder.Xml(@"
<?xml version=""1.0""?>
<doc/>
");
        }

        /// <summary>
        /// prolog must start with XML decl. Here the section(s) 2.8 [22] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP22fail2()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
]>
<?xml version=""1.0""?>
<doc/>
");
        }

        /// <summary>
        /// "xml" must be lower-case. Here the section(s) 2.8 [23] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP23fail1()
        {
            var document = DocumentBuilder.Xml(@"<?XML version=""1.0""?>
<doc/>
");
        }

        /// <summary>
        /// VersionInfo must be supplied. Here the section(s) 2.8 [23] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP23fail2()
        {
            var document = DocumentBuilder.Xml(@"<?xml encoding=""UTF-8""?>
<doc/>
");
        }

        /// <summary>
        /// VersionInfo must come first. Here the section(s) 2.8 [23] apply. This
        /// test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP23fail3()
        {
            var document = DocumentBuilder.Xml(@"<?xml encoding=""UTF-8"" version=""1.0""?>
<doc/>
");
        }

        /// <summary>
        /// SDDecl must come last. Here the section(s) 2.8 [23] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP23fail4()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" standalone=""yes"" encoding=""UTF-8""?>
<doc/>
");
        }

        /// <summary>
        /// no SGML-type PIs. Here the section(s) 2.8 [23] apply. This test is taken
        /// from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP23fail5()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"">
<doc/>
");
        }

        /// <summary>
        /// XML declarations must be correctly terminated. Here the section(s) 2.8 [23] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP39fail4()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"">
");
        }

        /// <summary>
        /// XML declarations must be correctly terminated. Here the section(s) 2.8 [23] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP39fail5()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"">
<!DOCTYPE doc
[
<!ELEMENT doc EMPTY>
]>

<!--comment-->
<?pi?>
");
        }

        /// <summary>
        /// quote types must match. Here the section(s) 2.8 [24] apply.
        /// This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP24fail1()
        {
            var document = DocumentBuilder.Xml(@"<?xml version = '1.0""?>
<doc/>
");
        }

        /// <summary>
        /// quote types must match. Here the section(s) 2.8 [24] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP24fail2()
        {
            var document = DocumentBuilder.Xml(@"<?xml version = ""1.0'?>
<doc/>
");
        }

        /// <summary>
        /// Comment is illegal in VersionInfo. Here the section(s) 2.8 [25] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP25fail1()
        {
            var document = DocumentBuilder.Xml(@"<?xml version <!--bad comment--> =""1.0""?>
<doc/>
");
        }

        /// <summary>
        /// Illegal character in VersionNum. Here the section(s) 2.8 [26] apply. This test
        /// is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP26fail1()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0?""?>
<doc/>
");
        }

        /// <summary>
        /// Illegal character in VersionNum. Here the section(s) 2.8 [26] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP26fail2()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0^""?>
<doc/>
");
        }

        /// <summary>
        /// References aren't allowed in Misc, even if they would resolve to valid Misc.
        /// Here the section(s) 2.8 [27] apply. This test is taken from the collection
        /// OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP27fail1()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
&#32;
<doc/>
");
        }

        /// <summary>
        /// only declarations in DTD. Here the section(s) 2.8 [28] apply. This test is
        /// taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP28fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
<doc/>
]>
");
        }

        /// <summary>
        /// A processor must not pass unknown declaration types. Here the section(s) 2.8
        /// [29] apply. This test is taken from the collection OASIS/NIST TESTS, 1-Nov-1998.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlSyntaxException))]
        public void XmlNotWfOP29fail1()
        {
            var document = DocumentBuilder.Xml(@"<!DOCTYPE doc [
<!ELEMENT doc EMPTY>
<!DUNNO should not pass unknown declaration types>
]>
<doc/>
");
        }
    }
}
