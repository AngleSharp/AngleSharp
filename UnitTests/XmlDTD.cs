using System;
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
        public void TVScheduleDtd()
        {
            var s = new SourceManager(@"<!DOCTYPE TVSCHEDULE [

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
 ]>");

            var t = new XmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("TVSCHEDULE", d.Name);
            Assert.IsTrue(d.IsSystemIdentifierMissing);
            Assert.AreEqual(15, d.InternalSubset.Count);

            Assert.IsTrue(d.InternalSubset[0] is ElementDeclaration);
            var f1 = (d.InternalSubset[0] as ElementDeclaration);
            Assert.AreEqual("TVSCHEDULE", f1.Name);
            Assert.AreEqual(ElementDeclarationEntry.ContentType.Children, f1.Type);
            Assert.IsTrue(f1.Entry is ElementNameDeclarationEntry);
            var g1 = (f1.Entry as ElementNameDeclarationEntry);
            Assert.AreEqual(ElementDeclarationEntry.ElementQuantifier.OneOrMore, g1.Quantifier);
            Assert.AreEqual("CHANNEL", g1.Name);

            Assert.IsTrue(d.InternalSubset[3] is ElementDeclaration);
            var f2 = (d.InternalSubset[3] as ElementDeclaration);
            Assert.AreEqual("DAY", f2.Name);
            Assert.AreEqual(ElementDeclarationEntry.ContentType.Children, f2.Type);
            Assert.IsTrue(f2.Entry is ElementSequenceDeclarationEntry);
            var g2 = (f2.Entry as ElementSequenceDeclarationEntry);
            Assert.AreEqual(ElementDeclarationEntry.ElementQuantifier.One, g2.Quantifier);
            Assert.AreEqual(2, g2.Sequence.Count);
            Assert.AreEqual(ElementDeclarationEntry.ElementQuantifier.One, g2.Sequence[0].Quantifier);
            Assert.AreEqual(ElementDeclarationEntry.ElementQuantifier.OneOrMore, g2.Sequence[1].Quantifier);
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
            Assert.AreEqual(ElementDeclarationEntry.ElementQuantifier.One, g5.Quantifier);
            Assert.AreEqual(ElementDeclarationEntry.ElementQuantifier.OneOrMore, g6.Quantifier);

            Assert.IsTrue(d.InternalSubset[10] is AttributeDeclaration);
            var f7 = (d.InternalSubset[10] as AttributeDeclaration);
            Assert.AreEqual("TVSCHEDULE", f7.Name);
            Assert.AreEqual(1, f7.Count);
            Assert.AreEqual("NAME", f7[0].Name);
            Assert.IsTrue(f7[0].ValueType is AttributeStringType);
            Assert.IsTrue(f7[0].ValueDefault is AttributeRequiredValue);
        }

        [TestMethod]
        public void NewspaperDtd()
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
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("NEWSPAPER", d.Name);
            Assert.IsTrue(d.IsSystemIdentifierMissing);
            Assert.AreEqual(14, d.InternalSubset.Count);
        }

        [TestMethod]
        public void ProductCatalogDtd()
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
            var e = t.Get();
            Assert.AreEqual(XmlTokenType.DOCTYPE, e.Type);
            var d = (XmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("CATALOG", d.Name);
            Assert.IsTrue(d.IsSystemIdentifierMissing);
            Assert.AreEqual(13, d.InternalSubset.Count);
        }
    }
}
