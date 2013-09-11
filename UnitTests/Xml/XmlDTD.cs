using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.Xml;
using AngleSharp.DTD;

namespace UnitTests
{
    [TestClass]
    public class XmlDTD
    {
        [TestMethod]
        public void TVScheduleDtdSubset()
        {
            var dtd = @"<!ELEMENT TVSCHEDULE (CHANNEL+)>
 <!ELEMENT CHANNEL (BANNER,DAY+)>
 <!ELEMENT BANNER (#PCDATA)>
 <!ELEMENT DAY (DATE,(HOLIDAY|PROGRAMSLOT+)+)>
 <!ELEMENT HOLIDAY (#PCDATA)>
 <!ELEMENT DATE (#PCDATA)>
 <!ELEMENT PROGRAMSLOT (TIME,TITLE,DESCRIPTION?)>
 <!ELEMENT TIME (#PCDATA)>
 <!ELEMENT TITLE (#PCDATA)> 
 <!ELEMENT DESCRIPTION (#PCDATA)>

 <!ATTLIST TVSCHEDULE NAME CDATA #REQUIRED>
 <!ATTLIST CHANNEL CHAN CDATA #REQUIRED>
 <!ATTLIST PROGRAMSLOT VTR CDATA #IMPLIED>
 <!ATTLIST TITLE RATING CDATA #IMPLIED>
 <!ATTLIST TITLE LANGUAGE CDATA #IMPLIED>";
            var text = "<!DOCTYPE TVSCHEDULE [" + dtd + "]>";
            var s = new SourceManager(text);

            var t = new XmlTokenizer(s);
            t.DTD.Reset();
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("TVSCHEDULE", d.Name);
            Assert.IsTrue(d.IsSystemIdentifierMissing);
            Assert.AreEqual(15, t.DTD.Count);

            //Unfortunately C# counts newlines with 2 characters since \r\n is used
            Assert.AreEqual(dtd.Replace("\r\n", "\n"), d.InternalSubset);
            //This is annoying but meh - what can we do? W3C specifies we need to use
            //\n for newlines and omit \r completely.
        }

        [TestMethod]
        public void TVScheduleDtdComplete()
        {
            var dtd = @"<!DOCTYPE TVSCHEDULE [

 <!ELEMENT TVSCHEDULE (CHANNEL+)>
 <!ELEMENT CHANNEL (BANNER,DAY+)>
 <!ELEMENT BANNER (#PCDATA)>
 <!ELEMENT DAY (DATE,(HOLIDAY|PROGRAMSLOT+)+)>
 <!ELEMENT HOLIDAY (#PCDATA)>
 <!ELEMENT DATE (#PCDATA)>
 <!ELEMENT PROGRAMSLOT (TIME,TITLE,DESCRIPTION?)>
 <!ELEMENT TIME (#PCDATA)>
 <!ELEMENT TITLE (#PCDATA)> 
 <!ELEMENT DESCRIPTION (#PCDATA)>

 <!ATTLIST TVSCHEDULE NAME CDATA #REQUIRED>
 <!ATTLIST CHANNEL CHAN CDATA #REQUIRED>
 <!ATTLIST PROGRAMSLOT VTR CDATA #IMPLIED>
 <!ATTLIST TITLE RATING CDATA #IMPLIED>
 <!ATTLIST TITLE LANGUAGE CDATA #IMPLIED>
 ]>";
            var s = new SourceManager(dtd);

            var t = new XmlTokenizer(s);
            t.DTD.Reset();
            var e = t.Get();
            Assert.IsTrue(t.DTD[0] is ElementDeclaration);

            var f1 = (t.DTD[0] as ElementDeclaration);
            Assert.AreEqual("TVSCHEDULE", f1.Name);
            Assert.AreEqual(ElementContentType.Children, f1.Entry.Type);
            Assert.IsTrue(f1.Entry is ElementChoiceDeclarationEntry);

            var g1 = (f1.Entry as ElementChoiceDeclarationEntry);
            Assert.AreEqual(ElementQuantifier.One, g1.Quantifier);
            Assert.AreEqual(1, g1.Choice.Count);
            Assert.IsTrue(g1.Choice[0] is ElementNameDeclarationEntry);

            var h1 = (g1.Choice[0] as ElementNameDeclarationEntry);
            Assert.AreEqual(ElementQuantifier.OneOrMore, h1.Quantifier);
            Assert.AreEqual("CHANNEL", h1.Name);

            Assert.IsTrue(t.DTD[3] is ElementDeclaration);

            var f2 = (t.DTD[3] as ElementDeclaration);
            Assert.AreEqual("DAY", f2.Name);
            Assert.AreEqual(ElementContentType.Children, f2.Entry.Type);
            Assert.IsTrue(f2.Entry is ElementSequenceDeclarationEntry);

            var g2 = (f2.Entry as ElementSequenceDeclarationEntry);
            Assert.AreEqual(ElementQuantifier.One, g2.Quantifier);
            Assert.AreEqual(2, g2.Sequence.Count);
            Assert.AreEqual(ElementQuantifier.One, g2.Sequence[0].Quantifier);
            Assert.AreEqual(ElementQuantifier.OneOrMore, g2.Sequence[1].Quantifier);
            Assert.IsTrue(g2.Sequence[0] is ElementNameDeclarationEntry);
            Assert.IsTrue(g2.Sequence[1] is ElementChoiceDeclarationEntry);

            var g3 = (g2.Sequence[0] as ElementNameDeclarationEntry);
            var g4 = (g2.Sequence[1] as ElementChoiceDeclarationEntry);
            Assert.AreEqual("DATE", g3.Name);
            Assert.AreEqual(2, g4.Choice.Count);
            Assert.IsTrue(g4.Choice[0] is ElementNameDeclarationEntry);
            Assert.IsTrue(g4.Choice[1] is ElementNameDeclarationEntry);

            var g5 = (g4.Choice[0] as ElementNameDeclarationEntry);
            var g6 = (g4.Choice[1] as ElementNameDeclarationEntry);
            Assert.AreEqual("HOLIDAY", g5.Name);
            Assert.AreEqual("PROGRAMSLOT", g6.Name);
            Assert.AreEqual(ElementQuantifier.One, g5.Quantifier);
            Assert.AreEqual(ElementQuantifier.OneOrMore, g6.Quantifier);

            Assert.IsTrue(t.DTD[10] is AttributeDeclaration);
            var f7 = (t.DTD[10] as AttributeDeclaration);
            Assert.AreEqual("TVSCHEDULE", f7.Name);
            Assert.AreEqual(1, f7.Count);
            Assert.AreEqual("NAME", f7[0].Name);
            Assert.IsInstanceOfType(f7[0].Type, typeof(AttributeStringType));
            Assert.IsInstanceOfType(f7[0].Default, typeof( AttributeRequiredValue));
        }

        [TestMethod]
        public void NewspaperDtdComplete()
        {
            var s = new SourceManager(@"<!DOCTYPE NEWSPAPER [

 <!ELEMENT NEWSPAPER (ARTICLE+)>
 <!ELEMENT ARTICLE (HEADLINE,BYLINE,LEAD,BODY,NOTES)>
 <!ELEMENT HEADLINE (#PCDATA)>
 <!ELEMENT BYLINE (#PCDATA)>
 <!ELEMENT LEAD (#PCDATA)>
 <!ELEMENT BODY (#PCDATA)>
 <!ELEMENT NOTES (#PCDATA)>

 <!ATTLIST ARTICLE AUTHOR CDATA #REQUIRED>
 <!ATTLIST ARTICLE EDITOR CDATA #IMPLIED>
 <!ATTLIST ARTICLE DATE CDATA #IMPLIED>
 <!ATTLIST ARTICLE EDITION CDATA #IMPLIED>

 <!ENTITY NEWSPAPER ""Vervet Logic Times"">
 <!ENTITY PUBLISHER ""Vervet Logic Press"">
 <!ENTITY COPYRIGHT 'Copyright 1998 Vervet Logic Press'>

 ]>");
            var t = new XmlTokenizer(s);
            t.DTD.Reset();
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("NEWSPAPER", d.Name);
            Assert.IsTrue(d.IsSystemIdentifierMissing);
            Assert.AreEqual(14, t.DTD.Count);
        }

        [TestMethod]
        public void ProductCatalogDtdComplete()
        {
            var s = new SourceManager(@"<!DOCTYPE CATALOG [

 <!ENTITY AUTHOR ""John Doe"">
 <!ENTITY COMPANY ""JD Power Tools, Inc."">
 <!ENTITY EMAIL ""jd@jd-tools.com"">

 <!ELEMENT CATALOG (PRODUCT+)>

 <!ELEMENT PRODUCT
 (SPECIFICATIONS+,OPTIONS?,PRICE+,NOTES?)>
 <!ATTLIST PRODUCT
 NAME CDATA #IMPLIED
 CATEGORY (HandTool|Table|Shop-Professional) ""HandTool""
 PARTNUM CDATA #IMPLIED
 PLANT (Pittsburgh|Milwaukee|Chicago) ""Chicago""
 INVENTORY (InStock|Backordered|Discontinued) ""InStock"">

 <!ELEMENT SPECIFICATIONS (#PCDATA)>
 <!ATTLIST SPECIFICATIONS
 WEIGHT CDATA #IMPLIED
 POWER CDATA #IMPLIED>

 <!ELEMENT OPTIONS (#PCDATA)>
 <!ATTLIST OPTIONS
 FINISH (Metal|Polished|Matte) ""Matte""
 ADAPTER (Included|Optional|NotApplicable) ""Included""
 CASE (HardShell|Soft|NotApplicable) ""HardShell"">

 <!ELEMENT PRICE (#PCDATA)>
 <!ATTLIST PRICE
 MSRP CDATA #IMPLIED
 WHOLESALE CDATA #IMPLIED
 STREET CDATA #IMPLIED
 SHIPPING CDATA #IMPLIED>

 <!ELEMENT NOTES (#PCDATA)>

 ]>");
            var t = new XmlTokenizer(s);
            t.DTD.Reset();
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("CATALOG", d.Name);
            Assert.IsTrue(d.IsSystemIdentifierMissing);
            Assert.AreEqual(13, t.DTD.Count);
        }
    }
}
