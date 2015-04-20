using System;
using NUnit.Framework;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace AngleSharp.Core.Tests
{
    [TestFixture]
    public class CssW3CSelectorTests
    {
        #region Already included tests

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-1.xml
        /// </summary>
        [Test]
        public void GroupsOfSelectors()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li>The background of this list item should be green</li>
  <li>The background of this second list item should be also green</li>
</ul>
<p xmlns=""http://www.w3.org/1999/xhtml"">The background of this paragraph should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("li,p");
	        Assert.AreEqual(3, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-2.xml
        /// </summary>
        [Test]
        public void TypeElementSelectors()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml"">This address element should have a green background.</address>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-3.xml
        /// </summary>
        [Test]
        public void UniversalSelector()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
<span class=""t1"">This paragraph, and all textual contents in the document, 
      should be green.</span>
</p>
<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""t1"">This item should be green.</li>
</ul>
<foo xmlns=""http://www.example.org/a"">And this element, part of a non-HTML namespace,
      should be green too</foo>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*");
	        Assert.AreEqual(8, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul,p");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*.t1");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-3a.xml
        /// </summary>
        [Test]
        public void UniversalSelectorNoNamespaces()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
<span class=""t1"">This paragraph, and all textual contents in the document, 
      should be green.</span>
</p>
<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""t1"">This item should be green.</li>
</ul>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*");
	        Assert.AreEqual(7, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul,p");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*.t1");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-4.xml
        /// </summary>
        [Test]
        public void OmittedUniversalSelector()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" id=""foo"">This paragraph should have a green background</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("#foo");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-5.xml
        /// </summary>
        [Test]
        public void AttributeExistenceSelector()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""title"">This paragraph should have a green background because its TITLE
      attribute is set.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p[title]");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-6.xml
        /// </summary>
        [Test]
        public void AttributeValueSelector()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml"" title=""foo"">
<span title=""b"">This line should </span>
  <span title=""aa"">have a green background.
</span>
</address>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address[title=foo]");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("span[title=a]");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-7.xml
        /// </summary>
        [Test]
        public void AttributeMultivalueSelectorA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""a b c"">This paragraph should have green background because CLASS
  contains b</p>
<address xmlns=""http://www.w3.org/1999/xhtml"" title=""tot foo bar"">
<span class=""a c"">This address should also</span>
  <span class=""a bb c"">have green background because the selector in the last
    rule does not apply to the inner SPANs.</span>
</address>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p[class~=b]");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("address[title~=foo]");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("span[class~=b]");
	        Assert.AreEqual(0, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-7b.xml
        /// </summary>
        [Test]
        public void AttributeMultivalueSelectorB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""hello world"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*");
	        Assert.AreEqual(4, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-8.xml
        /// </summary>
        [Test]
        public void AttributeValueSelectorsHyphenSeparatedAttributes()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-gb"">This paragraph should have green background because its language is en-gb</p>
<address xmlns=""http://www.w3.org/1999/xhtml"" lang=""fi"">
<span lang=""en-us"">This address should also</span>
  <span lang=""en-fr"">have green background because the language of the inner SPANs
     is not French.</span>
</address>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p[lang|=en]");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("address[lang=fi]");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("span[lang|=fr]");
	        Assert.AreEqual(0, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-9.xml
        /// </summary>
        [Test]
        public void SubstringMatchingAttributeSelectorBeginning()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""foobar"">This paragraph should have a green background<br></br>
because its title attribute begins with foo</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p[title^=foo]");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-10.xml
        /// </summary>
        [Test]
        public void SubstringMatchingAttributeSelectorEnd()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""foobar"">This paragraph should have a green background because
its title attribute ends with bar</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p[title$=bar]");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-11.xml
        /// </summary>
        [Test]
        public void SubstringMatchingAttributeSelectorContains()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""foobarufoo"">This paragraph should have a green background because
its title attribute contains bar</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p[title*=bar]");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-13.xml
        /// </summary>
        [Test]
        public void ClassSelectors()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""t1"">This list item should have green background because its class is t1</li>
  <li class=""t2"">This list item should have green background because its class is t2</li>
  <li class=""t2"">
<span class=""t33"">This list item should have green background because 
        the inner SPAN does not match SPAN.t3</span>
</li>
</ul>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("li");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("li.t2");
	        Assert.AreEqual(2, selector3.Length);
	        var selector4 = doc.QuerySelectorAll(".t3");
	        Assert.AreEqual(0, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-14.xml
        /// </summary>
        [Test]
        public void MoreThanOneClassSelectorA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t1 t2"">This paragraph
should have a green background and a green thick solid border because
it carries both classes t1 and t2.</p>

<div xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">This line
should be green.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p.t1");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p.t2");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("div");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("div.teST");
	        Assert.AreEqual(0, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("div.te");
	        Assert.AreEqual(0, selector6.Length);
	        var selector7 = doc.QuerySelectorAll("div.st");
	        Assert.AreEqual(0, selector7.Length);
	        var selector8 = doc.QuerySelectorAll("div.te.st");
	        Assert.AreEqual(0, selector8.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-14b.xml
        /// </summary>
        [Test]
        public void MoreThanOneClassSelectorB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t1"">This line should be green.</p>
  <p xmlns=""http://www.w3.org/1999/xhtml"" class=""t1 t2"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1.fail");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".fail.t1");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll(".t2.fail");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll(".fail.t2");
	        Assert.AreEqual(0, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-14c.xml
        /// </summary>
        [Test]
        public void MoreThanOneClassSelectorC()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t1 t2"">This line should be green.</p>
  <div xmlns=""http://www.w3.org/1999/xhtml"" class=""t3"">This line should be green.</div>
  <address xmlns=""http://www.w3.org/1999/xhtml"" class=""t4 t5 t6"">This line should be green.</address>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p.t1.t2");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("div.t1");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("address.t5.t5");
	        Assert.AreEqual(1, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-14d.xml
        /// </summary>
        [Test]
        public void NegatedMoreThanOneClassSelectorA()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t1 t2"">This line should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll(".t1:not(.t2)");
            Assert.AreEqual(0, selector2.Length);
            var selector3 = doc.QuerySelectorAll(":not(.t2).t1");
            Assert.AreEqual(0, selector3.Length);
            var selector4 = doc.QuerySelectorAll(".t2:not(.t1)");
            Assert.AreEqual(0, selector4.Length);
            var selector5 = doc.QuerySelectorAll(":not(.t1).t2");
            Assert.AreEqual(0, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-14e.xml
        /// </summary>
        [Test]
        public void NegatedMoreThanOneClassSelectorB()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t1 t2"">This line should be green.</p>
  <div xmlns=""http://www.w3.org/1999/xhtml"" class=""t3"">This line should be green.</div>
  <address xmlns=""http://www.w3.org/1999/xhtml"" class=""t4 t5 t6"">This line should be green.</address>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p:not(.t1):not(.t2)");
            Assert.AreEqual(0, selector2.Length);
            var selector3 = doc.QuerySelectorAll("div");
            Assert.AreEqual(1, selector3.Length);
            var selector4 = doc.QuerySelectorAll("div:not(.t1)");
            Assert.AreEqual(1, selector4.Length);
            var selector5 = doc.QuerySelectorAll("address");
            Assert.AreEqual(1, selector5.Length);
            var selector6 = doc.QuerySelectorAll("address:not(.t5):not(.t5)");
            Assert.AreEqual(0, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-15.xml
        /// </summary>
        [Test]
        public void IdSelectors()
        {
            var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li id=""t1"">This list item should have a green background. because its ID is t1</li>
  <li id=""t2"">This list item should have a green background. because its ID is t2</li>
  <li id=""t3""><span id=""t44"">This list item should have a green background. because the inner SPAN does not match #t4</span></li>
</ul>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("li");
            Assert.AreEqual(3, selector1.Length);
            var selector2 = doc.QuerySelectorAll("#t1");
            Assert.AreEqual(1, selector2.Length);
            var selector3 = doc.QuerySelectorAll("li#t2");
            Assert.AreEqual(1, selector3.Length);
            var selector4 = doc.QuerySelectorAll("li#t3");
            Assert.AreEqual(1, selector4.Length);
            var selector5 = doc.QuerySelectorAll("#t4");
            Assert.AreEqual(0, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-15b.xml
        /// </summary>
        [Test]
        public void MultipleIDSelectors()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" id=""test"">This line should be green.</p>
  <div xmlns=""http://www.w3.org/1999/xhtml"" id=""pass"">This line should be green.</div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("#test#fail");
            Assert.AreEqual(0, selector2.Length);
            var selector3 = doc.QuerySelectorAll("#fail#test");
            Assert.AreEqual(0, selector3.Length);
            var selector4 = doc.QuerySelectorAll("#fail");
            Assert.AreEqual(0, selector4.Length);
            var selector5 = doc.QuerySelectorAll("div");
            Assert.AreEqual(1, selector5.Length);
            var selector6 = doc.QuerySelectorAll("#pass#pass");
            Assert.AreEqual(1, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-15c.xml
        /// </summary>
        [Test]
        public void MultipleIds()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""warning"">This test requires support for two or more of XHTML, xml:id, and DOM3 Core.</p>
<div xmlns=""http://www.w3.org/1999/xhtml"" id=""Aone"" xml:id=""Atwo"" title=""Athree"">This line should be green.</div>
  <p xmlns=""http://www.w3.org/1999/xhtml"" id=""Bone"">This line should be green.</p>
  <p xmlns=""http://www.w3.org/1999/xhtml"" xml:id=""Ctwo"">This line should be green.</p>
  <p xmlns=""http://www.w3.org/1999/xhtml"" title=""Dthree"">This line should be green.</p>
 <script xmlns=""http://www.w3.org/1999/xhtml"" type=""text/javascript"">
  document.getElementsByTagNameNS('http://www.w3.org/1999/xhtml', 'div')[0].setIdAttribute('title', true);
  document.getElementsByTagNameNS('http://www.w3.org/1999/xhtml', 'p')[3].setIdAttribute('title', true);
 </script>
 <!-- This test could also be done using a custom DOCTYPE with an internal subset, which would
      then work in any XHTML UA. However, that requires massive changes to the generator scripts.
      Better, if we need such a test, would be to special-case it and have 15d be a separate file. -->";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll(".warning");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("div");
            Assert.AreEqual(1, selector2.Length);
            var selector3 = doc.QuerySelectorAll("#Aone#Atwo,#Aone#Athree,#Atwo#Athree");
            Assert.AreEqual(0, selector3.Length);
            var selector4 = doc.QuerySelectorAll("p");
            Assert.AreEqual(4, selector4.Length);
            var selector5 = doc.QuerySelectorAll("#Bone#Btwo,#Bone#Bthree,#Btwo#Bthree");
            Assert.AreEqual(0, selector5.Length);
            var selector6 = doc.QuerySelectorAll("#Cone#Ctwo,#Cone#Cthree,#Ctwo#Cthree");
            Assert.AreEqual(0, selector6.Length);
            var selector7 = doc.QuerySelectorAll("#Done#Dtwo,#Done#Dthree,#Dtwo#Dthree");
            Assert.AreEqual(0, selector7.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-22.xml
        /// </summary>
        [Test]
        public void LangPseudoClass()
        {
            var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li lang=""en-GB"">This list item should be green because its language is
        British English</li>
  <li lang=""en-GB-wa"">This list item should be green because its language
        is British English (Wales)</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li lang=""en-US"">This list item should NOT be green because its language
       is US English</li>
  <li lang=""fr"">This list item should NOT be green because its language is
       French</li>
</ol>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("ul li");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("li:lang(en-GB)");
            Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-177a.xml
        /// </summary>
        [Test]
        [ExpectedException(typeof(DomException))]
        public void ParsingColonVsColonColonA()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">When you select this text, it shouldn't go red.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p:selection");
            Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-177b.xml
        /// </summary>
        [Test]
        [ExpectedException(typeof(DomException))]
        public void ParsingColonVsColonColonB()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p>This line should be green.</p>
 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("div");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p::first-child");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-178.xml
        /// </summary>
        [Test]
        public void ParsingNotAndPseudoElements()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p>This line should be green.</p>
 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("div");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p:not(:first-line)");
            Assert.AreEqual(0, selector2.Length);
            var selector3 = doc.QuerySelectorAll("p:not(:after)");
            Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-182.xml
        /// </summary>
        [Test]
        [ExpectedException(typeof(DomException))]
        public void NamespacesAndInSelectors()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
<foo:bar xmlns:foo=""http://www.example.org/"">
This text should be green.
</foo:bar>
</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("foo:bar");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-183.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingOfClassSelectors()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class="".test"">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""foo"">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""foo quux"">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""foo  quux"">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class="" bar "">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(6, selector1.Length);
            var selector2 = doc.QuerySelectorAll("test");
            Assert.AreEqual(0, selector2.Length);
            var selector3 = doc.QuerySelectorAll(".fooquux");
            Assert.AreEqual(0, selector3.Length);
            var selector4 = doc.QuerySelectorAll(".bar");
            Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-184a.xml
        /// </summary>
        [Test]
        public void EndsWithAttributeSelectorWithEmptyValue()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class="""">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p[class$=\"\"]");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-184b.xml
        /// </summary>
        [Test]
        public void StartsWithAttributeSelectorWithEmptyValue()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class="""">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p[class^='']");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-184c.xml
        /// </summary>
        [Test]
        public void ContainsAttributeSelectorWithEmptyValue()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class="""">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p[class*=\"\"]");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-184d.xml
        /// </summary>
        [Test]
        public void NegatedEndsWithAttributeSelectorWithEmptyValue()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class="""">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p:not([class$=\"\"])");
            Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-184e.xml
        /// </summary>
        [Test]
        public void NegatedStartsWithAttributeSelectorWithEmptyValue()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class="""">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p:not([class^=\"\"])");
            Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-184f.xml
        /// </summary>
        [Test]
        public void NegatedContainsAttributeSelectorWithEmptyValue()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class="""">This text should be green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("p");
            Assert.AreEqual(2, selector1.Length);
            var selector2 = doc.QuerySelectorAll("p:not([class*=\"\"])");
            Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-d1.xml
        /// </summary>
        [Test]
        public void NegatedDynamicHandlingOfEmpty()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">

  <script type=""text/javascript"">
   
  </script>

  <p> The following bar should be green. </p>

  <div id=""test""></div>

 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("#test");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("#test:not(:empty)");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-d1b.xml
        /// </summary>
        [Test]
        public void DynamicHandlingOfEmpty()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">

  <script type=""text/javascript"">
   
  </script>

  <p> The following two bars should be green. </p>

  <div id=""test1""></div>
  <div id=""test2""></div>

 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("#test1");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("#test1:empty");
            Assert.AreEqual(1, selector2.Length);
            var selector3 = doc.QuerySelectorAll("#test2");
            Assert.AreEqual(1, selector3.Length);
            var selector4 = doc.QuerySelectorAll("#test2:empty");
            Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-d2.xml
        /// </summary>
        [Test]
        public void DynamicHandlingOfCombinators()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">

  
  <script type=""text/javascript"">
   
  </script>
  

  

  <p> The following bar should be green. </p>

  <div id=""stub""></div>
  <div></div>
  <div><div><!-- <div/> --><div><div id=""test""></div></div></div></div>

 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("#test");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("#stub~div div+div div");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-d3.xml
        /// </summary>
        [Test]
        public void DynamicHandlingOfAttributeSelectors()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">

  <script type=""text/javascript"">
   
  </script>

  <p> The following block should be green. </p>

  <!-- root of selector -->
  <stub xmlns=""""></stub>

  <!-- middle part of selector does not match this -->
  <t xmlns="""" attribute=""fake""></t>

  <!-- middle part of selector matches this once attribute is fixed -->
  <t xmlns="""" attribute=""start mid dle end""></t>

  <!-- subject of selector -->
  <t xmlns="""" test=""test""></t>

 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("[test]");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("stub~start:not(mid)dleend~t");
            Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-d4.xml
        /// </summary>
        [Test]
        public void DynamicUpdatingOfFirstChildAndLastChild()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">

  <script type=""text/javascript"">
   
  </script>

  <div><p id=""two"">This line should be unstyled. (2)</p><p id=""three"">This line should have a green background. (3)</p><p>This line should be unstyled. (4 moving to 1)</p></div>

 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("#two:first-child");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("#three:last-child");
            Assert.AreEqual(0, selector2.Length);
        }

        #endregion

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-16.xml
        /// </summary>
        [Test]
        public void LinkPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
<a href=""http://unvisited.example.org/css3-modsel-16/"">This link should have green background.</a>
(Don't follow this link.)
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p.test a");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p.test *:link");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-17.xml
        /// </summary>
        [Test]
        public void VisitedPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
<a href=""http://www.w3.org/"">You should see a green background assigned by the anchor.</a>
(Note: You must have visited http://www.w3.org/ for this test to be valid.)
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p.test a");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p.test *:visited");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-18.xml
        /// </summary>
        [Test]
        public void HoverPseudoClassA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The background color of this paragraph should turn to green when
   the mouse pointer hovers either its text (<strong>here</strong>) or its whitespace background, <strong>here</strong>:</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">The background color of <a href=""#foo"">this anchor (<strong>here</strong>)</a> should turn to green when the pointing device hovers over it.</address>
<table xmlns=""http://www.w3.org/1999/xhtml"">
 <tbody>
  <tr>
   <td>The cells in</td>
   <td>this table</td>
   <td>should go</td>
  </tr>
  <tr>
   <td>green when</td>
   <td>you hover</td>
   <td>the pointing</td>
  </tr>
  <tr>
   <td>device over</td>
   <td>them (<strong>here</strong>).</td>
   <td></td>
  </tr>
  <tr>
   <td>The rows in</td>
   <td>this table</td>
   <td>should go</td>
  </tr>
  <tr>
   <td>dark green</td>
   <td>when the</td>
   <td>pointing device</td>
  </tr>
  <tr>
   <td>is over the</td>
   <td>cells <strong>there</strong>:</td>
   <td></td> <!-- remove this cell to make an evil test; row should still go green, but cell should not -->
  </tr>
  <tr>
   <td>And <strong>here</strong>:</td>
   <td></td>
   <td>(blank cells).</td>
  </tr>
 </tbody>
</table>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:hover");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("a:hover");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("tr:hover");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("td:hover");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table");
	        Assert.AreEqual(1, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-18a.xml
        /// </summary>
        [Test]
        public void HoverPseudoClassOnLinksA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""a"">The background color of <a href=""#foo"">this anchor (<strong>here</strong>)</a> should turn to green when the pointing device hovers over it.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""b"">The background color of <a href=""#foo"">this anchor (<strong>here</strong>)</a> should <strong>remain green when you hover it</strong>.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""c"">The background color of <a href=""http://link.example.com/"">this anchor (<strong>here</strong>)</a> should <strong>remain green when the pointing device hovers over it</strong> (do not follow that link).</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".a a:hover");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".b a:hover");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll(".b a:link");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll(".c :link");
	        Assert.AreEqual(1, selector5.Length);
	        var selector6 = doc.QuerySelectorAll(".c :visited:hover");
	        Assert.AreEqual(0, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-18b.xml
        /// </summary>
        [Test]
        public void HoverPseudoClassB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p>The background color of this paragraph should turn to green when the mouse pointer hovers over any of the following:<br></br><strong>This text.</strong></p>
  <p>This text.</p>
  <table><tr><td><table><tr><td><dl><dt>This text.</dt><dd>This text.</dd></dl></td></tr></table></td></tr><tr><td>This text.</td></tr></table>
  <p><sub>This text.</sub></p>
  <p>...and anything else between the top of the first paragraph and the bottom of this paragraph.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div:hover p:first-child");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-18c.xml
        /// </summary>
        [Test]
        public void HoverPseudoClassOnLinksB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml""><a href=""#foo"">Hover <strong>here</strong> and the background of <span>this text should go green</span>.</a></p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(":link,:visited");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(":link:hover span");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-19.xml
        /// </summary>
        [Test]
        public void ActivePseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The background color of <a href=""#foo"">the anchor</a>
   should turn to green when it is activated and come back to
   normal when it is released.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("a:active");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-19b.xml
        /// </summary>
        [Test]
        public void ActivePseudoClassOnControls()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml""><button>Activating (e.g. holding the mouse button down on) this button should make it go green.</button></p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("button:active");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-20.xml
        /// </summary>
        [Test]
        public void FocusPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The background color of <a href=""#foo"">anchors</a>
  in this page should turn <a href=""#foo"">to green</a> when they have the
  <a href=""#foo"">focus</a>.</p>";
	        var doc = source.ToHtmlDocument();

            var anchors = doc.QuerySelectorAll("a");
            Assert.AreEqual(3, anchors.Length);

            var anchor = anchors[0] as IHtmlAnchorElement;
            Assert.IsNotNull(anchor);

            var selector1 = doc.QuerySelectorAll("a:focus");
	        Assert.AreEqual(0, selector1.Length);

            anchor.DoFocus();

            var selector2 = doc.QuerySelectorAll("a:focus");
            Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-21.xml
        /// </summary>
        [Test]
        public void TargetPseudoClassA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" id=""first"">This paragraph should be unstyled.
       The background of the following paragraph should become green when
       you follow <a href=""#second"">this link</a>.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" id=""second"">This paragraph should initially be unstyled.
       It should become green when you select the link above. When you select
       <a href=""#third"">this link</a>, it should return to being unstyled and the 
       background of the paragraph below should become green.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" id=""third"">This paragraph should initially be unstyled.
       It should become green when you select the link above. When you follow
       <a href=""#missing"">this link</a>, the three paragraphs
       should all return to being unstyled.</p>";
	        var doc = source.ToHtmlDocument();

            var paragraphs = doc.QuerySelectorAll("p");
            Assert.AreEqual(3, paragraphs.Length);
	        
	        var selector1 = doc.QuerySelectorAll("p:target");
	        Assert.AreEqual(0, selector1.Length);

            doc.Location.Hash = "#second";

            var selector2 = doc.QuerySelectorAll("p:target");
            Assert.AreEqual(1, selector2.Length);
            Assert.AreSame(paragraphs[1], selector2[0]);

            doc.Location.Hash = "#third";

            var selector3 = doc.QuerySelectorAll("p:target");
            Assert.AreEqual(1, selector3.Length);
            Assert.AreSame(paragraphs[2], selector3[0]);

            doc.Location.Hash = "#missing";

            var selector4 = doc.QuerySelectorAll("p:target");
            Assert.AreEqual(0, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-21b.xml
        /// </summary>
        [Test]
        public void TargetPseudoClassB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:target");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-23.xml
        /// </summary>
        [Test]
        public void EnabledPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
 <button>A button (enabled) with green background</button>
 <br></br>
 <input type=""text"" size=""36"" value=""a text area (enabled) with green background""></input>
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("button");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("input");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("button:enabled");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("input:enabled");
	        Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-24.xml
        /// </summary>
        [Test]
        public void DisabledPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
 <button disabled=""disabled"">A button (disabled) with green background</button>
 <br></br>
 <input disabled=""disabled"" type=""text"" size=""36"" value=""a text area (disabled) with green background""></input>
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("button");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("input");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("button:disabled");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("input:disabled");
	        Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-25.xml
        /// </summary>
        [Test]
        public void CheckedPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
<input type=""checkbox"" checked=""checked""></input> <span>Everything in this paragraph should have a green background</span></p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("input,span");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("input:checked,input:checked+span");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-27.xml
        /// </summary>
        [Test]
        public void RootPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The background of the document should be green</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("html");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*:root");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-27a.xml
        /// </summary>
        [Test]
        public void ImpossibleRulesRootFirstChildEtc()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be green (there should be no red on this page).</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(":root:first-child");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(":root:last-child");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(":root:only-child");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll(":root:nth-child(0n+1)");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll(":root:nth-child(1n+0)");
	        Assert.AreEqual(0, selector5.Length);
	        var selector6 = doc.QuerySelectorAll(":root:nth-last-child(0n+1)");
	        Assert.AreEqual(0, selector6.Length);
	        var selector7 = doc.QuerySelectorAll(":root:nth-last-child(1n+0)");
	        Assert.AreEqual(0, selector7.Length);
	        var selector8 = doc.QuerySelectorAll(":root:first-of-type");
	        Assert.AreEqual(0, selector8.Length);
	        var selector9 = doc.QuerySelectorAll(":root:last-of-type");
	        Assert.AreEqual(0, selector9.Length);
	        var selector10 = doc.QuerySelectorAll(":root");
	        Assert.AreEqual(1, selector10.Length);
	        var selector11 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector11.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-27b.xml
        /// </summary>
        [Test]
        public void ImpossibleRulesHtmlRoot()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be green (there should be no red on this page).</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("* html");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("* :root");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-28.xml
        /// </summary>
        [Test]
        public void NthChildPseudoClassA()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""red"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""red"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""red"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""red"">This second list item should have a green background</li>
  <li>Third list item</li>
  <li class=""red"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""red"">This sixth list item should have a green background</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr class=""red"">
<td>Green row : 1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr>
<td>5.1</td>
<td>5.2</td>
<td>5.3</td>
</tr>
  <tr>
<td>6.1</td>
<td>6.2</td>
<td>6.3</td>
</tr>
</table>

<table class=""t2"" border=""1"">
  <tr>
<td class=""red"">green cell</td>
<td>1.2</td>
<td>1.3</td>
      <td class=""red"">green cell</td>
<td>1.5</td>
<td>1.6</td>
      <td class=""red"">green cell</td>
<td>1.8</td>
</tr>
  <tr>
<td class=""red"">green cell</td>
<td>2.2</td>
<td>2.3</td>
      <td class=""red"">green cell</td>
<td>2.5</td>
<td>2.6</td>
      <td class=""red"">green cell</td>
<td>2.8</td>
</tr>
  <tr>
<td class=""red"">green cell</td>
<td>3.2</td>
<td>3.3</td>
      <td class=""red"">green cell</td>
<td>3.5</td>
<td>3.6</td>
      <td class=""red"">green cell</td>
<td>3.8</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(19, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul li:nth-child(2n+1)");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol li:nth-child(2n+0)");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:nth-child(-1n+4)");
	        Assert.AreEqual(4, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:nth-child(3n+1)");
	        Assert.AreEqual(9, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-28b.xml
        /// </summary>
        [Test]
        public void NthChildPseudoClassB()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""green"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""green"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""green"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""green"">This second list item should have a green background</li>
  <li>Third list item</li>
  <li class=""green"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""green"">This sixth list item should have a green background</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr class=""green"">
<td>Green row : 1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr>
<td>5.1</td>
<td>5.2</td>
<td>5.3</td>
</tr>
  <tr>
<td>6.1</td>
<td>6.2</td>
<td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td class=""green"">green cell</td>
<td>1.2</td>
<td>1.3</td>
      <td class=""green"">green cell</td>
<td>1.5</td>
<td>1.6</td>
      <td class=""green"">green cell</td>
<td>1.8</td>
</tr>
  <tr>
<td class=""green"">green cell</td>
<td>2.2</td>
<td>2.3</td>
      <td class=""green"">green cell</td>
<td>2.5</td>
<td>2.6</td>
      <td class=""green"">green cell</td>
<td>2.8</td>
</tr>
  <tr>
<td class=""green"">green cell</td>
<td>3.2</td>
<td>3.3</td>
      <td class=""green"">green cell</td>
<td>3.5</td>
<td>3.6</td>
      <td class=""green"">green cell</td>
<td>3.8</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(19, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul li:nth-child(2n+1)");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol li:nth-child(2n+0)");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:nth-child(-1n+4)");
	        Assert.AreEqual(4, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:nth-child(3n+1)");
	        Assert.AreEqual(9, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-29.xml
        /// </summary>
        [Test]
        public void NthLastChildPseudoClassA()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""red"">This second list item should have a green background</li>
  <li>Third list item</li>
  <li class=""red"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""red"">This sixth list item should have a green background</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""red"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""red"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""red"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr>
<td>1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr>
<td>2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 5.1</td>
<td>5.2</td>
      <td>5.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 6.1</td>
<td>6.2</td>
      <td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td>1.1</td>
<td class=""red"">green cell</td>
<td>1.3</td>
      <td>1.4</td>
<td class=""red"">green cell</td>
<td>1.6</td>
      <td>1.7</td>
<td class=""red"">green cell</td>
</tr>
  <tr>
<td>2.1</td>
<td class=""red"">green cell</td>
<td>2.3</td>
      <td>2.4</td>
<td class=""red"">green cell</td>
<td>2.6</td>
      <td>2.7</td>
<td class=""red"">green cell</td>
</tr>
  <tr>
<td>3.1</td>
<td class=""red"">green cell</td>
<td>3.3</td>
      <td>3.4</td>
<td class=""red"">green cell</td>
<td>3.6</td>
      <td>3.7</td>
<td class=""red"">green cell</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(19, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul li:nth-last-child(2n+1)");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol li:nth-last-child(2n+0)");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:nth-last-child(-1n+4)");
	        Assert.AreEqual(4, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:nth-last-child(3n+1)");
	        Assert.AreEqual(9, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-29b.xml
        /// </summary>
        [Test]
        public void NthLastChildPseudoClassB()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""green"">This second list item should have a green background</li>
  <li>Third list item</li>
  <li class=""green"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""green"">This sixth list item should have a green background</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""green"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""green"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""green"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr>
<td>1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr>
<td>2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 5.1</td>
<td>5.2</td>
      <td>5.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 6.1</td>
<td>6.2</td>
      <td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td>1.1</td>
<td class=""green"">green cell</td>
<td>1.3</td>
      <td>1.4</td>
<td class=""green"">green cell</td>
<td>1.6</td>
      <td>1.7</td>
<td class=""green"">green cell</td>
</tr>
  <tr>
<td>2.1</td>
<td class=""green"">green cell</td>
<td>2.3</td>
      <td>2.4</td>
<td class=""green"">green cell</td>
<td>2.6</td>
      <td>2.7</td>
<td class=""green"">green cell</td>
</tr>
  <tr>
<td>3.1</td>
<td class=""green"">green cell</td>
<td>3.3</td>
      <td>3.4</td>
<td class=""green"">green cell</td>
<td>3.6</td>
      <td>3.7</td>
<td class=""green"">green cell</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(19, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul li:nth-last-child(2n+1)");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol li:nth-last-child(2n+0)");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:nth-last-child(-1n+4)");
	        Assert.AreEqual(4, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:nth-last-child(3n+1)");
	        Assert.AreEqual(9, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-30.xml
        /// </summary>
        [Test]
        public void NthOfTypePseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph is here only to fill space in the DOM</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">And this address too..</address>
<p xmlns=""http://www.w3.org/1999/xhtml"">So does this paragraph !</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">But this one should have green background</p>
<dl xmlns=""http://www.w3.org/1999/xhtml"">
  <dt class=""red"">First definition term that should have green background</dt>
    <dd class=""red"">First definition that should have green background</dd>
  <dt>Second definition term</dt>
    <dd>Second definition</dd>
  <dt>Third definition term</dt>
    <dd>Third definition</dd>
  <dt class=""red"">Fourth definition term that should have green background</dt>
    <dd class=""red"">Fourth definition that should have green background</dd>
  <dt>Fifth definition term</dt>
    <dd>Fifth definition</dd>
  <dt>Sixth definition term</dt>
    <dd>Sixth definition</dd>
</dl>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("dl");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-31.xml
        /// </summary>
        [Test]
        public void NthLastOfTypePseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">This paragraph should have green background</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">But this address is here only to fill space in the dom..</address>
<p xmlns=""http://www.w3.org/1999/xhtml"">So does this paragraph !</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">And so does this one too.</p>
<dl xmlns=""http://www.w3.org/1999/xhtml"">
  <dt>First definition term</dt>
    <dd>First definition</dd>
  <dt>Second definition term</dt>
    <dd>Second definition</dd>
  <dt class=""red"">Third definition term that should have green background</dt>
    <dd class=""red"">Third definition that should have green background</dd>
  <dt>Fourth definition term</dt>
    <dd>Fourth definition</dd>
  <dt>Fifth definition term</dt>
    <dd>Fifth definition</dd>
  <dt class=""red"">Sixth definition term that should have green background</dt>
    <dd class=""red"">Sixth definition that should have green background</dd>
</dl>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("dl");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-32.xml
        /// </summary>
        [Test]
        public void FirstChildPseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
<table class=""t1"" border=""1"">
  <tr>
    <td class=""red"">green cell</td>
    <td>1.2</td>
    <td>1.3</td>
  </tr>
  <tr>
    <td class=""red"">green cell</td>
    <td>2.2</td>
    <td>2.3</td>
  </tr>
  <tr>
    <td class=""red"">green cell</td>
    <td>3.2</td>
    <td>3.3</td>
  </tr>
</table>
</div>
<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph contains some text
          <span>and a span that should have a green background</span>
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 td:first-child");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p *:first-child");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-33.xml
        /// </summary>
        [Test]
        public void LastChildPseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
<table class=""t1"" border=""1"">
  <tr>
    <td>1.1</td>
    <td>1.2</td>
    <td class=""red"">green cell</td>
  </tr>
  <tr>
    <td>2.1</td>
    <td>2.2</td>
    <td class=""red"">green cell</td>
  </tr>
  <tr>
    <td>3.1</td>
    <td>3.2</td>
    <td class=""red"">green cell</td>
  </tr>
</table>
</div>
<p xmlns=""http://www.w3.org/1999/xhtml"">
<span>This paragraph contains a span that should
     have a green background</span> and some text after it.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 td:last-child");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p *:last-child");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-34.xml
        /// </summary>
        [Test]
        public void FirstOfTypePseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">This div contains 3 addresses:
<address class=""red"">A first address that should have a green background</address>
<address>A second address with normal background</address>
<address>A third address with normal background</address>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("address:first-of-type");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-35.xml
        /// </summary>
        [Test]
        public void LastOfTypePseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
<address>A first address with normal background</address>
<address>A second address with normal background</address>
<address class=""red"">A third address that should have a green background</address>
This div contains 3 addresses above this sentence.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("address:last-of-type");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-36.xml
        /// </summary>
        [Test]
        public void OnlyChildPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should have normal background</p>
<div xmlns=""http://www.w3.org/1999/xhtml"">This div contains only one paragraph
    <p class=""red"">This paragraph should have green background</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:only-child");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.testText div p");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-37.xml
        /// </summary>
        [Test]
        public void OnlyOfTypePseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""t1"">
<p>This paragraph should have normal background</p>
<address class=""red"">But this address should have green background</address>
<p>This paragraph should have normal background</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-38.xml
        /// </summary>
        [Test]
        public void FirstLinePseudoElement()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This very long paragraph should have a
      first line with green background. This very long paragraph should have a first
      line with green background.  This very long paragraph should have a first line
      with green background. This very long paragraph should have a first line with
      green background. This very long paragraph should have a first line with green
      background. This very long paragraph should have a first line with green background.
      This very long paragraph should have a first line with green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:first-line");
	        Assert.AreEqual(1, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-39.xml
        /// </summary>
        [Test]
        public void FirstLetterPseudoElementA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This very long paragraph
should have a big first letter T with a green background. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:first-letter");
	        Assert.AreEqual(1, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-39a.xml
        /// </summary>
        [Test]
        public void FirstLetterPseudoElementWithBeforePseudoElementA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">his very long paragraph should
have a big green first letter T.  Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:first-letter");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:before");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-39b.xml
        /// </summary>
        [Test]
        public void FirstLetterPseudoElementB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This very long paragraph
should have a big first letter T with a green background.  Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p::first-letter");
	        Assert.AreEqual(1, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-39c.xml
        /// </summary>
        [Test]
        public void FirstLetterPseudoElementWithBeforePseudoElementB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">his very long paragraph should
have a big green first letter T. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text. Dummy text. Dummy
text. Dummy text. Dummy text. Dummy text.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p::first-letter");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p::before");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-41.xml
        /// </summary>
        [Test]
        public void BeforePseudoElementA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">You should see before this paragraph the words GENERATED CONTENT over green background</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p::before");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-41a.xml
        /// </summary>
        [Test]
        public void BeforePseudoElementB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">You should see before this paragraph the words GENERATED CONTENT over green background</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:before");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-42.xml
        /// </summary>
        [Test]
        public void AfterPseudoElementA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">You should see after this paragraph the words GENERATED CONTENT over green background</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p::after");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-42a.xml
        /// </summary>
        [Test]
        public void AfterPseudoElementB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">You should see after this paragraph the words GENERATED CONTENT over green background</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:after");
	        Assert.AreEqual(0, selector1.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-43.xml
        /// </summary>
        [Test]
        public void DescendantCombinatorA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""t1"">
  <p class=""red"">This paragraph should have a green background</p>
  <table>
   <tbody>
    <tr>
     <td>
      <p class=""red"">This paragraph should have a green background</p>
     </td>
    </tr>
   </tbody>
  </table>
 </div>
 <table xmlns=""http://www.w3.org/1999/xhtml"">
  <tbody>
   <tr>
    <td>
     <p class=""white"">This paragraph should be unstyled.</p>
    </td>
   </tr>
  </tbody>
 </table>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".white");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.t1 p");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-43b.xml
        /// </summary>
        [Test]
        public void DescendantCombinatorB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""t1"">
  <p class=""white"">This paragraph should be unstyled</p>
  <table>
   <tbody>
    <tr>
     <td>
      <p class=""white"">This paragraph should be unstyled</p>
     </td>
    </tr>
   </tbody>
  </table>
 </div>
 <table xmlns=""http://www.w3.org/1999/xhtml"">
  <tbody>
   <tr>
    <td>
     <p class=""green"">This paragraph should have a green background</p>
    </td>
   </tr>
  </tbody>
 </table>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".white");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.t1 p");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-44.xml
        /// </summary>
        [Test]
        public void ChildCombinatorA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p class=""red test"">This paragraph should have a green background</p>
  <div>
   <p class=""red test"">This paragraph should have a green background</p>
  </div>
 </div>
 <table xmlns=""http://www.w3.org/1999/xhtml"">
  <tbody>
   <tr>
    <td>
     <p class=""white test"">This paragraph should be unstyled.</p>
    </td>
   </tr>
  </tbody>
 </table>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".white");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div p.test");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-44b.xml
        /// </summary>
        [Test]
        public void ChildCombinatorB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p class=""white test"">This paragraph should be unstyled.</p>
  <div>
   <p class=""white test"">This paragraph should be unstyled.</p>
  </div>
 </div>
 <table xmlns=""http://www.w3.org/1999/xhtml"">
  <tbody>
   <tr>
    <td>
     <p class=""green test"">This paragraph should have a green background.</p>
    </td>
   </tr>
  </tbody>
 </table>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".white");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div p.test");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-44c.xml
        /// </summary>
        [Test]
        public void ChildCombinatorAndClasses()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml""> This should be unstyled. </div>
  <div xmlns=""http://www.w3.org/1999/xhtml"" class=""control""> This should have a green background. </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".fail div");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".control");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-44d.xml
        /// </summary>
        [Test]
        public void ChildCombinatiorAndIds()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml""> This should be unstyled. </div>
  <p xmlns=""http://www.w3.org/1999/xhtml""> This should have a green background. </p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("#fail div");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-45.xml
        /// </summary>
        [Test]
        public void DirectAdjacentCombinatorA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p>This paragraph should be unstyled.</p>
  <p class=""red"">But this one should have a green background.</p>
  <p class=""red"">And this one should also have a green background.</p>
  <address>This address is only here to fill some space between two paragraphs.</address>
  <p>This paragraph should be unstyled.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub p+p");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-45b.xml
        /// </summary>
        [Test]
        public void DirectAdjacentCombinatorB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p class=""green"">This paragraph should have a green background.</p>
  <p class=""white"">But this one should be unstyled.</p>
  <p class=""white"">And this one should also be unstyled.</p>
  <address class=""green"">This address is only here to fill some space between two paragraphs and should have a green background.</address>
  <p class=""green"">This paragraph should have a green background too.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".white");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub p+p");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-45c.xml
        /// </summary>
        [Test]
        public void DirectAdjacentCombinatorAndClasses()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml""> This should be unstyled. </div>
  <div xmlns=""http://www.w3.org/1999/xhtml"" class=""control""> This should have a green background. </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".fail+div");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".control");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-46.xml
        /// </summary>
        [Test]
        public void IndirectAdjacentCombinatorA()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p>This paragraph should be unstyled.</p>
  <p class=""red"">But this one should have a green background</p>
  <p class=""red"">And this one should also have a green background</p>
  <address>This address is only here to fill some space between two paragraphs</address>
 <p class=""red"">This paragraph should have a green background</p>
 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll(".red");
            Assert.AreEqual(3, selector1.Length);
            var selector2 = doc.QuerySelectorAll("div.stub p~p");
            Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-46b.xml
        /// </summary>
        [Test]
        public void IndirectAdjacentCombinatorB()
        {
            var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p>This paragraph should be unstyled.</p>
  <p class=""green"">But this one should have a green background</p>
  <p class=""green"">And this one should also have a green background</p>
  <address>This address is only here to fill some space between two paragraphs</address>
  <p class=""green"">This paragraph should have a green background</p>
 </div>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll(".green");
            Assert.AreEqual(3, selector1.Length);
            var selector2 = doc.QuerySelectorAll("div.stub p~p");
            Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-47.xml
        /// </summary>
        [Test]
        public void NegatedTypeElementSelector()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p>
   <span>The text in this paragraph should have a green background</span>
  </p>
  <address>This address should have a green background</address>
  <q xmlns=""http://www.example.org/a"">This element in another namespace should have a green background.</q>
  <r xmlns="""">This element without a namespace should have a green background.</r>
  <p>This paragraph should be unstyled.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub span,div.stub address,div.stub *|q,div.stub *|r");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address, *|q, *|r");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *:not(p)");
	        Assert.AreEqual(4, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-48.xml
        /// </summary>
        [Test]
        public void NegatedUniversalSelector()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<address>This address should have a green background</address>
<s xmlns=""http://www.example.org/b"">This paragraph should have a green background</s>
<t xmlns="""">This paragraph should have a green background</t>
<u xmlns=""http://www.example.org/a"">This paragraph should have a green background</u>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *|*");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > *|*:not(*)");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-49.xml
        /// </summary>
        [Test]
        public void NegatedOmittedUniversalSelectorIsForbidden()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<address>This address should have a green background</address>
<s xmlns=""http://www.example.org/b"">This paragraph should have a green background</s>
<t xmlns="""">This paragraph should have a green background</t>
<u xmlns=""http://www.example.org/a"">This paragraph should have a green background</u>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *|*");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > *|*:not()");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-50.xml
        /// </summary>
        [Test]
        public void NegatedAttributeExistenceSelector()
        {
            var config = new Configuration().WithCss();
            var source = @"<style>@namespace a url(http://www.example.org/a);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""1"">
  <r>This text should be in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"">This text should be in green characters</s>
</div>";
	        var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("a|*");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *|*:not([test])");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-51.xml
        /// </summary>
        [Test]
        public void NegatedAttributeValueSelector()
        {
            var config = new Configuration().WithCss();
            var source = @"<style>@namespace a url(http://www.example.org/a);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""1"">
  <r test=""11"">This text should be</r>
  <r>in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"">This text should be in green characters</s>
<p>This text should be in green characters</p>
</div>";
	        var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("div.stub p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > a|*");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([test='1'])");
	        Assert.AreEqual(4, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-52.xml
        /// </summary>
        [Test]
        public void NegatedAttributeSpaceSeparatedValueSelector()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""bar foo tut"">
  <r test=""tut foofoo bar"">This text should be</r>
  <r>in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"" test=""bar tut"">This text should be in green characters</s>
<t xmlns=""http://www.example.org/a"">This text should be in green characters</t>
<p class=""tit foo1 tut"">This text should be in green characters</p>
<u xmlns=""http://www.example.org/b"" test=""tit foo2 tut"">This text should be in green characters</u>
</div>";
            var config = new Configuration().WithCss();
            var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("div.stub p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > a|*, div.stub > b|*");
	        Assert.AreEqual(4, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([test~='foo'])");
	        Assert.AreEqual(6, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("div.stub *|p:not([class~='foo'])");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("div.stub b|*[test~='foo2']");
	        Assert.AreEqual(1, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-53.xml
        /// </summary>
        [Test]
        public void NegatedAttributeDashSeparatedValueSelector()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""foo-bar"">
  <r test=""foo-bartut"">This text should be</r>
  <r>in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"" test=""bar tut"">This text should be in green characters</s>
<t xmlns=""http://www.example.org/a"">This text should be in green characters</t>
<p class=""en-uk"">This text should be in green characters</p>
<u xmlns=""http://www.example.org/b"" test=""foo2-bar-lol"">This text should be in green characters</u>
</div>";
            var config = new Configuration().WithCss();
            var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("div.stub p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > a|*, div.stub > b|*");
	        Assert.AreEqual(4, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([test|='foo-bar'])");
	        Assert.AreEqual(6, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("div.stub *|p:not([lang|='en-us'])");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("div.stub b|*[test|='foo2-bar']");
	        Assert.AreEqual(1, selector5.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-54.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnBeginning()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p>This paragraph should be in green characters.</p>
<p title=""on chante?"">This paragraph should be in green characters.</p>
<p title=""si on chantait"">
     <span title=""si il chantait"">This paragraph should be in green characters.</span>
</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not([title^='si on'])");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-55.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnEnd()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p>This paragraph should be in green characters.</p>
<p title=""on chante?"">This paragraph should be in green characters.</p>
<p title=""si on chantait"">
     <span title=""si il chante"">This paragraph should be in green characters.</span>
</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not([title$='tait'])");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-56.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnMiddle()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p>This paragraph should be in green characters.</p>
<p title=""on chante?"">This paragraph should be in green characters.</p>
<p title=""si on chantait"">
     <span title=""si il chante"">This paragraph should be in green characters.</span>
</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not([title*=' on'])");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-57.xml
        /// </summary>
        [Test]
        public void NegatedAttributeExistenceSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <q xmlns=""http://www.example.org/a"" a:title=""a paragraph"">This a:q element should be unstyled.</q>
  <p title=""a paragraph"">This paragraph should have a green background.</p>
  <r xmlns=""http://www.example.org/b"" b:title=""a paragraph"">This b:r element should have a green background.</r>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|p, *|r");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *:not([a|title])");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-57b.xml
        /// </summary>
        [Test]
        public void NegatedAttributeExistenceSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <q xmlns=""http://www.example.org/a"" a:title=""a paragraph"">This a:q element should be unstyled.</q>
  <p title=""a paragraph"">This paragraph should have a green background.</p>
  <r xmlns=""http://www.example.org/b"" b:title=""a paragraph"">This b:r element should have a green background.</r>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|p, *|r");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *:not([a|title])");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-59.xml
        /// </summary>
        [Test]
        public void NegatedClassSelector()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p>This paragraph should be in green characters.</p>
<p class=""bar foofoo tut"">This paragraph should be in green characters.</p>
<p class=""bar foo tut"">
     <span class=""tut foo2"">This paragraph should be in green characters.</span>
</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not(.foo)");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-60.xml
        /// </summary>
        [Test]
        public void NegatedIdSelector()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p>This paragraph should be in green characters.</p>
<p id=""foo2"">This paragraph should be in green characters.</p>
<p id=""foo"">
     <span>This paragraph should be in green characters.</span>
</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not(#foo)");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-61.xml
        /// </summary>
        [Test]
        public void NegatedLinkPseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<a href=""http://www.w3.org/"">This anchor should have a green background</a>
(Note: You must have visited http://www.w3.org/ for this test to be valid.)
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not(:link)");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-62.xml
        /// </summary>
        [Test]
        public void NegatedVisitedPseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<a href=""http://unvisited.example.org/css3-modsel-62/"">This anchor should have a green background</a>
(Don't follow this link.)
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub > *");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not(:visited)");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-63.xml
        /// </summary>
        [Test]
        public void NegatedHoverPseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p> <span>The color of this text should be green when the pointing device hovers over it.</span> </p>
  <p> <a href=""http://dummy.example.org/dummy"">The color of this text should be green when the pointing device hovers over it.</a> </p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub *");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > * > *:not(:hover)");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-64.xml
        /// </summary>
        [Test]
        public void NegatedActivePseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
  <p> <a href=""http://dummy.example.org/dummy"">This text should turn green while it is active.</a> </p>
  <p> <button>This text should turn green while it is active.</button> </p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub *");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub > * > *:not(:active)");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-65.xml
        /// </summary>
        [Test]
        public void NegatedFocusPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The background color of all <a href=""#foo"">anchors</a>
  should become <a href=""#foo""> green</a> when they have the
  <a href=""#foo"">focus</a>.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("a:not(:focus)");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("a");
	        Assert.AreEqual(3, selector2.Length);
            var anchor = selector2[0] as IHtmlAnchorElement;
            Assert.IsNotNull(anchor);
            anchor.DoFocus();
            var selector3 = doc.QuerySelectorAll("a:not(:focus)");
            Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-66.xml
        /// </summary>
        [Test]
        public void NegatedTargetPseudoClassA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" id=""first"">This paragraph should be unstyled.
       The background of the following paragraph should become blue when
       you follow <a href=""#second"">this link</a>.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" id=""second"">This paragraph should initially be unstyled.
       It should become blue when you select the link above. When you select
       <a href=""#third"">this link</a>, it should return to being unstyled and the 
       background of the paragraph below should become blue.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" id=""third"">This paragraph should initially be unstyled.
       It should become blue when you select the link above. When you follow
       <a href=""#missing"">this link</a>, the three paragraphs
       should all return to being unstyled.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:target)");
	        Assert.AreEqual(3, selector2.Length);

            doc.Location.Hash = "#first";

            var selector3 = doc.QuerySelectorAll("p:not(:target)");
            Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-66b.xml
        /// </summary>
        [Test]
        public void NegatedTargetPseudoClassB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:target)");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-67.xml
        /// </summary>
        [Test]
        public void NegatedLangPseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" lang=""en"" class=""stub"">
<p>This paragraph should have a green background because the
   enclosing div is in english.</p>
<p lang=""en"">This paragraph should have a green background because
   it is in english.</p>
<div lang=""fr"">
  <p lang=""en"">This paragraph should have a green background
     because it is in english.</p>
</div>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub *");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not(:lang(fr))");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-68.xml
        /// </summary>
        [Test]
        public void NegatedEnabledPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
 <button disabled=""disabled"">A button (disabled) with green background</button>
 <br></br>
 <input disabled=""disabled"" type=""text"" size=""36"" value=""a text area (disabled) with green background""></input>
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("button");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("input");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("button:not(:enabled)");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("input:not(:enabled)");
	        Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-69.xml
        /// </summary>
        [Test]
        public void NegatedDisabledPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
 <button>A button (enabled) with green background</button>
 <br></br>
 <input type=""text"" size=""36"" value=""a text area (enabled) with green background""></input>
</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("button");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("input");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("button:not(:disabled)");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("input:not(:disabled)");
	        Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-70.xml
        /// </summary>
        [Test]
        public void NegatedCheckedPseudoClass()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">
<input type=""checkbox""></input> <span>Everything in this paragraph should have a green background</span></p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("input,span");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("input:not(:checked),input:not(:checked)+span");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-72.xml
        /// </summary>
        [Test]
        public void NegatedRootPseudoClassA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p>This paragraph should have a green background and there should be no red anywhere.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:not(:root)");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div *");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-72b.xml
        /// </summary>
        [Test]
        public void NegatedRootPseudoClassB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p>This paragraph should have a green background and there should be no red anywhere.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("html:not(:root),test:not(:root)");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-73.xml
        /// </summary>
        [Test]
        public void NegatedNthChildPseudoClassA()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""red"">This second list item should have a green background</li>
  <li>Third list</li>
  <li class=""red"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""red"">This sixth list item should have a green background</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""red"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""red"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""red"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr>
<td>1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr>
<td>2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr>
<td>3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr>
<td>4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 5.1</td>
<td>5.2</td>
<td>5.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 6.1</td>
<td>6.2</td>
<td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td>1.1</td>
<td class=""red"">green cell</td>
<td class=""red"">green cell</td>
      <td>1.4</td>
<td class=""red"">green cell</td>
<td class=""red"">green cell</td>
      <td>1.7</td>
<td class=""red"">green cell</td>
</tr>
  <tr>
<td>2.1</td>
<td class=""red"">green cell</td>
<td class=""red"">green cell</td>
      <td>2.4</td>
<td class=""red"">green cell</td>
<td class=""red"">green cell</td>
      <td>2.7</td>
<td class=""red"">green cell</td>
</tr>
  <tr>
<td>3.1</td>
<td class=""red"">green cell</td>
<td class=""red"">green cell</td>
      <td>3.4</td>
<td class=""red"">green cell</td>
<td class=""red"">green cell</td>
      <td>3.7</td>
<td class=""red"">green cell</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(23, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul > li:not(:nth-child(odd))");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol > li:not(:nth-child(even))");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:not(:nth-child(-1n+4))");
	        Assert.AreEqual(2, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:not(:nth-child(3n+1))");
	        Assert.AreEqual(15, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("table.t1 td,table.t2 td");
	        Assert.AreEqual(42, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-73b.xml
        /// </summary>
        [Test]
        public void NegatedNthChildPseudoClassB()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""green"">This second list item should have a green background</li>
  <li>Third list</li>
  <li class=""green"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""green"">This sixth list item should have a green background</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""green"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""green"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""green"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr>
<td>1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr>
<td>2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr>
<td>3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr>
<td>4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 5.1</td>
<td>5.2</td>
<td>5.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 6.1</td>
<td>6.2</td>
<td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td>1.1</td>
<td class=""green"">green cell</td>
<td class=""green"">green cell</td>
      <td>1.4</td>
<td class=""green"">green cell</td>
<td class=""green"">green cell</td>
      <td>1.7</td>
<td class=""green"">green cell</td>
</tr>
  <tr>
<td>2.1</td>
<td class=""green"">green cell</td>
<td class=""green"">green cell</td>
      <td>2.4</td>
<td class=""green"">green cell</td>
<td class=""green"">green cell</td>
      <td>2.7</td>
<td class=""green"">green cell</td>
</tr>
  <tr>
<td>3.1</td>
<td class=""green"">green cell</td>
<td class=""green"">green cell</td>
      <td>3.4</td>
<td class=""green"">green cell</td>
<td class=""green"">green cell</td>
      <td>3.7</td>
<td class=""green"">green cell</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(23, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul > li:not(:nth-child(odd))");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol > li:not(:nth-child(even))");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:not(:nth-child(-1n+4))");
	        Assert.AreEqual(2, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:not(:nth-child(3n+1))");
	        Assert.AreEqual(15, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("table.t1 td,table.t2 td");
	        Assert.AreEqual(42, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-74.xml
        /// </summary>
        [Test]
        public void NegatedNthLastChildPseudoClassA()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""red"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""red"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""red"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""red"">This second list item should have a green background</li>
  <li>Third list item</li>
  <li class=""red"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""red"">This sixth list item should have a green background</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr class=""red"">
<td>Green row : 1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr class=""red"">
<td>Green row : 2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr>
<td>3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr>
<td>4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr>
<td>5.1</td>
<td>5.2</td>
      <td>5.3</td>
</tr>
  <tr>
<td>6.1</td>
<td>6.2</td>
      <td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td class=""red"">green cell</td>
<td>1.2</td>
<td class=""red"">green cell</td>
      <td class=""red"">green cell</td>
<td>1.5</td>
<td class=""red"">green cell</td>
      <td class=""red"">green cell</td>
<td>1.8</td>
</tr>
  <tr>
<td class=""red"">green cell</td>
<td>2.2</td>
<td class=""red"">green cell</td>
      <td class=""red"">green cell</td>
<td>2.5</td>
<td class=""red"">green cell</td>
      <td class=""red"">green cell</td>
<td>2.8</td>
</tr>
  <tr>
<td class=""red"">green cell</td>
<td>3.2</td>
<td class=""red"">green cell</td>
      <td class=""red"">green cell</td>
<td>3.5</td>
<td class=""red"">green cell</td>
      <td class=""red"">green cell</td>
<td>3.8</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(23, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul > li:not(:nth-last-child(odd))");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol > li:not(:nth-last-child(even))");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:not(:nth-last-child(-n+4))");
	        Assert.AreEqual(2, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:not(:nth-last-child(3n+1))");
	        Assert.AreEqual(15, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("table.t1 td,table.t2 td");
	        Assert.AreEqual(42, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-74b.xml
        /// </summary>
        [Test]
        public void NegatedNthLastChildPseudoClassB()
        {
	        var source = @"<ul xmlns=""http://www.w3.org/1999/xhtml"">
  <li class=""green"">This first list item should have a green background</li>
  <li>Second list item</li>
  <li class=""green"">This third list item should have a green background</li>
  <li>Fourth list item</li>
  <li class=""green"">This fifth list item should have a green background</li>
  <li>Sixth list item</li>
</ul>
<ol xmlns=""http://www.w3.org/1999/xhtml"">
  <li>First list item</li>
  <li class=""green"">This second list item should have a green background</li>
  <li>Third list item</li>
  <li class=""green"">This fourth list item should have a green background</li>
  <li>Fifth list item</li>
  <li class=""green"">This sixth list item should have a green background</li>
</ol>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<table border=""1"" class=""t1"">
  <tr class=""green"">
<td>Green row : 1.1</td>
<td>1.2</td>
     <td>1.3</td>
</tr>
  <tr class=""green"">
<td>Green row : 2.1</td>
<td>2.2</td>
     <td>2.3</td>
</tr>
  <tr>
<td>3.1</td>
<td>3.2</td>
     <td>3.3</td>
</tr>
  <tr>
<td>4.1</td>
<td>4.2</td>
      <td>4.3</td>
</tr>
  <tr>
<td>5.1</td>
<td>5.2</td>
      <td>5.3</td>
</tr>
  <tr>
<td>6.1</td>
<td>6.2</td>
      <td>6.3</td>
</tr>
</table>
<p></p>
<table class=""t2"" border=""1"">
  <tr>
<td class=""green"">green cell</td>
<td>1.2</td>
<td class=""green"">green cell</td>
      <td class=""green"">green cell</td>
<td>1.5</td>
<td class=""green"">green cell</td>
      <td class=""green"">green cell</td>
<td>1.8</td>
</tr>
  <tr>
<td class=""green"">green cell</td>
<td>2.2</td>
<td class=""green"">green cell</td>
      <td class=""green"">green cell</td>
<td>2.5</td>
<td class=""green"">green cell</td>
      <td class=""green"">green cell</td>
<td>2.8</td>
</tr>
  <tr>
<td class=""green"">green cell</td>
<td>3.2</td>
<td class=""green"">green cell</td>
      <td class=""green"">green cell</td>
<td>3.5</td>
<td class=""green"">green cell</td>
      <td class=""green"">green cell</td>
<td>3.8</td>
</tr>
</table>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(23, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("ul > li:not(:nth-last-child(odd))");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("ol > li:not(:nth-last-child(even))");
	        Assert.AreEqual(3, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 tr:not(:nth-last-child(-1n+4))");
	        Assert.AreEqual(2, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("table.t2 td:not(:nth-last-child(3n+1))");
	        Assert.AreEqual(15, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("table.t1 td,table.t2 td");
	        Assert.AreEqual(42, selector6.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-75.xml
        /// </summary>
        [Test]
        public void NegatedNthOfTypePseudoClassA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">This paragraph should have green background</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">And this address should be unstyled.</address>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">This paragraph should also have green background!</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">But this one should be unstyled again.</p>
<dl xmlns=""http://www.w3.org/1999/xhtml"">
  <dt>First definition term</dt>
    <dd>First definition</dd>
  <dt class=""red"">Second definition term that should have green background</dt>
    <dd class=""red"">Second definition that should have green background</dd>
  <dt class=""red"">Third definition term that should have green background</dt>
    <dd class=""red"">Third definition that should have green background</dd>
  <dt>Fourth definition term</dt>
    <dd>Fourth definition</dd>
  <dt class=""red"">Fifth definition term that should have green background</dt>
    <dd class=""red"">Fifth definition that should have green background</dd>
  <dt class=""red"">Sixth definition term that should have green background</dt>
    <dd class=""red"">Sixth definition that should have green background</dd>
</dl>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(10, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:nth-of-type(3))");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("dl > *:not(:nth-of-type(3n+1))");
	        Assert.AreEqual(8, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-75b.xml
        /// </summary>
        [Test]
        public void NegatedNthOfTypePseudoClassB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""green"">This paragraph should have green background</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">And this address should be unstyled.</address>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""green"">This paragraph should also have green background!</p>
<p xmlns=""http://www.w3.org/1999/xhtml"">But this one should be unstyled again.</p>
<dl xmlns=""http://www.w3.org/1999/xhtml"">
  <dt>First definition term</dt>
    <dd>First definition</dd>
  <dt class=""green"">Second definition term that should have green background</dt>
    <dd class=""green"">Second definition that should have green background</dd>
  <dt class=""green"">Third definition term that should have green background</dt>
    <dd class=""green"">Third definition that should have green background</dd>
  <dt>Fourth definition term</dt>
    <dd>Fourth definition</dd>
  <dt class=""green"">Fifth definition term that should have green background</dt>
    <dd class=""green"">Fifth definition that should have green background</dd>
  <dt class=""green"">Sixth definition term that should have green background</dt>
    <dd class=""green"">Sixth definition that should have green background</dd>
</dl>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(10, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:nth-of-type(3))");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("dl > *:not(:nth-of-type(3n+1))");
	        Assert.AreEqual(8, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-76.xml
        /// </summary>
        [Test]
        public void NegatedNthLastOfTypePseudoClassA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should be unstyled.</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">This address should be unstyled.</address>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">This paragraph should have green background.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">This paragraph should have green background.</p>
<dl xmlns=""http://www.w3.org/1999/xhtml"">
  <dt class=""red"">First definition term that should have green background.</dt>
    <dd class=""red"">First definition that should also have a green background.</dd>
  <dt class=""red"">Second definition term that should have green background.</dt>
    <dd class=""red"">Second definition that should have green background.</dd>
  <dt>Third definition term.</dt>
    <dd>Third definition.</dd>
  <dt class=""red"">Fourth definition term that should have green background.</dt>
    <dd class=""red"">Fourth definition that should have green background.</dd>
  <dt class=""red"">Fifth definition term that should have green background.</dt>
    <dd class=""red"">Fifth definition that should have green background.</dd>
  <dt>Sixth definition term.</dt>
    <dd>Sixth definition.</dd>
</dl>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(10, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:nth-last-of-type(3))");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("dl > *:not(:nth-last-of-type(3n+1))");
	        Assert.AreEqual(8, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-76b.xml
        /// </summary>
        [Test]
        public void NegatedNthLastOfTypePseudoClassB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should be unstyled.</p>
<address xmlns=""http://www.w3.org/1999/xhtml"">This address should be unstyled.</address>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""green"">This paragraph should have green background.</p>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""green"">This paragraph should have green background.</p>
<dl xmlns=""http://www.w3.org/1999/xhtml"">
  <dt class=""green"">First definition term that should have green background.</dt>
    <dd class=""green"">First definition that should also have a green background.</dd>
  <dt class=""green"">Second definition term that should have green background.</dt>
    <dd class=""green"">Second definition that should have green background.</dd>
  <dt>Third definition term.</dt>
    <dd>Third definition.</dd>
  <dt class=""green"">Fourth definition term that should have green background.</dt>
    <dd class=""green"">Fourth definition that should have green background.</dd>
  <dt class=""green"">Fifth definition term that should have green background.</dt>
    <dd class=""green"">Fifth definition that should have green background.</dd>
  <dt>Sixth definition term.</dt>
    <dd>Sixth definition.</dd>
</dl>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(10, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:nth-last-of-type(3))");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("dl > *:not(:nth-last-of-type(3n+1))");
	        Assert.AreEqual(8, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-77.xml
        /// </summary>
        [Test]
        public void NegatedFirstChildPseudoClassA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <table class=""t1"" border=""1"">
   <tr>
    <td>1.1</td>
    <td class=""red"">green cell</td>
    <td class=""red"">green cell</td>
   </tr>
   <tr>
    <td>2.1</td>
    <td class=""red"">green cell</td>
    <td class=""red"">green cell</td>
   </tr>
   <tr>
    <td>3.1</td>
    <td class=""red"">green cell</td>
    <td class=""red"">green cell</td>
   </tr>
 </table>
 </div>
 <p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph <span>should be</span> unstyled.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(6, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 td:not(:first-child)");
	        Assert.AreEqual(6, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p > *:not(:first-child)");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 td");
	        Assert.AreEqual(9, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-77b.xml
        /// </summary>
        [Test]
        public void NegatedFirstChildPseudoClassB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <table class=""t1"" border=""1"">
   <tr>
    <td>1.1</td>
    <td class=""green"">green cell</td>
    <td class=""green"">green cell</td>
   </tr>
   <tr>
    <td>2.1</td>
    <td class=""green"">green cell</td>
    <td class=""green"">green cell</td>
   </tr>
   <tr>
    <td>3.1</td>
    <td class=""green"">green cell</td>
    <td class=""green"">green cell</td>
   </tr>
 </table>
 </div>
 <p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph <span>should be</span> unstyled.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(6, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 td:not(:first-child)");
	        Assert.AreEqual(6, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p > *:not(:first-child)");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 td");
	        Assert.AreEqual(9, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-78.xml
        /// </summary>
        [Test]
        public void NegatedLastChildPseudoClassA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <table class=""t1"" border=""1"">
   <tr>
    <td class=""red"">green cell</td>
    <td class=""red"">green cell</td>
    <td>1.3</td>
   </tr>
   <tr>
    <td class=""red"">green cell</td>
    <td class=""red"">green cell</td>
    <td>2.3</td>
   </tr>
   <tr>
    <td class=""red"">green cell</td>
    <td class=""red"">green cell</td>
    <td>3.3</td>
   </tr>
  </table>
 </div>
 <p xmlns=""http://www.w3.org/1999/xhtml"">This <span>paragraph should</span> be unstyled.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(6, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 td:not(:last-child)");
	        Assert.AreEqual(6, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p > *:not(:last-child)");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 td");
	        Assert.AreEqual(9, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-78b.xml
        /// </summary>
        [Test]
        public void NegatedLastChildPseudoClassB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <table class=""t1"" border=""1"">
   <tr>
    <td class=""green"">green cell</td>
    <td class=""green"">green cell</td>
    <td>1.3</td>
   </tr>
   <tr>
    <td class=""green"">green cell</td>
    <td class=""green"">green cell</td>
    <td>2.3</td>
   </tr>
   <tr>
    <td class=""green"">green cell</td>
    <td class=""green"">green cell</td>
    <td>3.3</td>
   </tr>
  </table>
 </div>
 <p xmlns=""http://www.w3.org/1999/xhtml"">This <span>paragraph should</span> be unstyled.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(6, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 td:not(:last-child)");
	        Assert.AreEqual(6, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p > *:not(:last-child)");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("table.t1 td");
	        Assert.AreEqual(9, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-79.xml
        /// </summary>
        [Test]
        public void NegatedFirstOfTypePseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">This div contains 3 addresses :
<address>A first address with normal background</address>
<address class=""red"">A second address that should have a green background</address>
<address class=""red"">A third address that should have a green background</address>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("address:not(:first-of-type)");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-80.xml
        /// </summary>
        [Test]
        public void NegatedLastOfTypePseudoClass()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
<address class=""red"">A first address that should have a green background</address>
<address class=""red"">A second address that should have a green background</address>
<address>A third address with normal background</address>
This div should have three addresses above it.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("address:not(:last-of-type)");
	        Assert.AreEqual(2, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-81.xml
        /// </summary>
        [Test]
        public void NegatedOnlyChildPseudoClassA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""red"">This paragraph should have a green background.</p>
 <div xmlns=""http://www.w3.org/1999/xhtml"">This div contains only one paragraph.
  <p>This paragraph should be unstyled.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:only-child)");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.testText > div > p");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-81b.xml
        /// </summary>
        [Test]
        public void NegatedOnlyChildPseudoClassB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""green"">This paragraph should have a green background.</p>
 <div xmlns=""http://www.w3.org/1999/xhtml"">This div contains only one paragraph.
  <p>This paragraph should be unstyled.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(:only-child)");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.testText > div > p");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-82.xml
        /// </summary>
        [Test]
        public void NegatedOnlyOfTypePseudoClassA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""t1"">
<p class=""red"">This paragraph should have green background.</p>
<address>But this address should be unstyled.</address>
<p class=""red"">This paragraph should have green background.</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".red");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 *:not(:only-of-type)");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-82b.xml
        /// </summary>
        [Test]
        public void NegatedOnlyOfTypePseudoClassB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""t1"">
<p class=""green"">This paragraph should have green background.</p>
<address>But this address should be unstyled.</address>
<p class=""green"">This paragraph should have green background.</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".green");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".t1 *:not(:only-of-type)");
	        Assert.AreEqual(2, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-83.xml
        /// </summary>
        [Test]
        public void NegationPseudoClassCannotBeAnArgumentOfItself()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should have a green background</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll("p:not(:not(p))");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-86.xml
        /// </summary>
        [Test]
        public void NondeterministicMatchingOfDescendantAndChildCombinators()
        {
	        var source = @"<blockquote xmlns=""http://www.w3.org/1999/xhtml"">
<div>
<div>
<p>This text should be green.</p>
</div>
</div>
</blockquote>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("blockquote > div p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-87.xml
        /// </summary>
        [Test]
        public void NondeterministicMatchingOfDirectAndIndirectAdjacentCombinators()
        {
	        var source = @"<blockquote xmlns=""http://www.w3.org/1999/xhtml""><div>This text should be unstyled.</div></blockquote>
<div xmlns=""http://www.w3.org/1999/xhtml"">This text should be unstyled.</div>
<div xmlns=""http://www.w3.org/1999/xhtml"">This text should be unstyled.</div>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("blockquote+div~p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-88.xml
        /// </summary>
        [Test]
        public void NondeterministicMatchingOfDescendantAndDirectAdjacentCombinators()
        {
	        var source = @"<blockquote xmlns=""http://www.w3.org/1999/xhtml""><div>This text should be unstyled.</div></blockquote>
<div xmlns=""http://www.w3.org/1999/xhtml"">
<div>
<p>This text should be green.</p>
</div>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("blockquote + div p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-89.xml
        /// </summary>
        [Test]
        public void SimpleCombinationOfDescendantAndChildCombinators()
        {
	        var source = @"<blockquote xmlns=""http://www.w3.org/1999/xhtml"">
<div>
<div>
<p>This text should be green.</p>
</div>
</div>
</blockquote>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("blockquote div > p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-90.xml
        /// </summary>
        [Test]
        public void SimpleCombinationOfDirectAndIndirectAdjacentCombinators()
        {
	        var source = @"<blockquote xmlns=""http://www.w3.org/1999/xhtml""><div>This text should be unstyled.</div></blockquote>
<div xmlns=""http://www.w3.org/1999/xhtml"">This text should be unstyled.</div>
<div xmlns=""http://www.w3.org/1999/xhtml"">This text should be unstyled.</div>
<p xmlns=""http://www.w3.org/1999/xhtml"">This text should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("blockquote~div+p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-91.xml
        /// </summary>
        [Test]
        public void TypeElementSelectorWithDeclaredNamespace()
        {
            var config = new Configuration().WithCss();
            var source = @"<style>@namespace test url(http://www.example.org/a);</style>
<testa xmlns=""http://www.example.org/a"">This paragraph should have a green background</testa>";
	        var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("testa");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("test|testa");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-92.xml
        /// </summary>
        [Test]
        public void TypeElementSelectorWithUniversalNamespace()
        {
	        var source = @"<style>@namespace test url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""myTest"">
<testA xmlns=""http://www.example.org/b"">This paragraph should have a green background</testA>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.myTest *");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.myTest *|testA");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-93.xml
        /// </summary>
        [Test]
        public void TypeElementSelectorWithoutDeclaredNamespace()
        {
	        var source = @"<style>@namespace test url(http://www.example.org/b);</style>
<testA>This paragraph has no declared namespace and should have a green background.</testA>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|testa");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("|testa");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-94.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithDeclaredNamespaceA()
        {
            var config = new Configuration().WithCss();
            var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be unstyled.</p>
<p xmlns=""http://www.example.org/b"">This line should have a green background.</p>
<q xmlns=""http://www.example.org/b"">This line should have a green background.</q>
<p xmlns=""http://www.example.org/a"">This line should be unstyleed.</p>
<p xmlns=""http://www.example.org/b"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("p,q");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("b|*");
	        Assert.AreEqual(3, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-94b.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithDeclaredNamespaceB()
        {
            var config = new Configuration().WithCss();
            var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be unstyled.</p>
<p xmlns=""http://www.example.org/b"" test=""test"">This line should have a green background.</p>
<q xmlns=""http://www.example.org/b"" test=""test"">This line should have a green background.</q>
<p xmlns=""http://www.example.org/a"">This line should be unstyled.</p>
<p xmlns=""http://www.example.org/b"" test=""test"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument(config);
	        
	        var selector1 = doc.QuerySelectorAll("p,q");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("b|*");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("[test]");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-95.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithUniversalNamespace()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
  <p>This line should  have a green background</p>
  <p xmlns=""http://www.example.org/b"">This line should have a green background</p>
  <q xmlns=""http://www.example.org/b"">This line should have a green background</q>
  <p xmlns=""http://www.example.org/a"">This line should have a green background</p>
  <foo xmlns=""http://www.example.org/b"">This line should have a green background</foo>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.test *");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.test *|*");
	        Assert.AreEqual(5, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-96.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
 <p>This line should be unstyled.</p>
 <elementA xmlns=""http://www.example.org/a"">This line should be unstyled.</elementA>
 <elementB xmlns=""http://www.example.org/b"">This line should be unstyled.</elementB>
 <div class=""green"">
  <p xmlns="""">This line should have a green background</p>
  <elementA xmlns="""">This line should have a green background</elementA>
  <elementB xmlns="""">This line should have a green background</elementB>
 </div>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.green *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.test *");
	        Assert.AreEqual(7, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.test |*");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-96b.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
 <p>This line should be unstyled.</p>
 <elementA xmlns=""http://www.example.org/a"">This line should be unstyled.</elementA>
 <elementB xmlns=""http://www.example.org/b"">This line should be unstyled.</elementB>
 <div class=""green"">
  <p xmlns="""">This line should have a green background</p>
  <elementA xmlns="""">This line should have a green background</elementA>
  <elementB xmlns="""">This line should have a green background</elementB>
 </div>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.green *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.test *");
	        Assert.AreEqual(7, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.test |*");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-97.xml
        /// </summary>
        [Test]
        public void AttributeExistenceSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"" title=""a paragraph"">This paragraph should be unstyled.</p>
 <q xmlns=""http://www.example.org/a"" a:title=""a paragraph"">This paragraph should have a green background.</q>
 <r xmlns=""http://www.example.org/b"" b:title=""a paragraph"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p,*|q,*|r");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*[a|title]");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-97b.xml
        /// </summary>
        [Test]
        public void AttributeExistenceSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"" title=""a paragraph"">This paragraph should be unstyled.</p>
 <q xmlns=""http://www.example.org/a"" a:title=""a paragraph"">This paragraph should have a green background.</q>
 <r xmlns=""http://www.example.org/b"" b:title=""a paragraph"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*[a|title]");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-98.xml
        /// </summary>
        [Test]
        public void AttributeValueSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"" title=""foo"">This paragraph should be unstyled.</p>
 <q xmlns=""http://www.example.org/a"" a:title=""foo"">This paragraph should have a green background</q>
 <s xmlns=""http://www.example.org/a"" a:title=""foobar"">This paragraph should be unstyled.</s>
 <r xmlns=""http://www.example.org/b"" b:title=""foo"">This paragraph should be unstyled.</r>
 <t xmlns=""http://www.example.org/a"" a:title=""footwo"">This paragraph should have a green background</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q,*|t");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*[a|title='foo']");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("*[a|title=footwo]");
	        Assert.AreEqual(1, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-98b.xml
        /// </summary>
        [Test]
        public void AttributeValueSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"" title=""foo"">This paragraph should be unstyled.</p>
 <q xmlns=""http://www.example.org/a"" a:title=""foo"">This paragraph should have a green background</q>
 <s xmlns=""http://www.example.org/a"" a:title=""foobar"">This paragraph should be unstyled.</s>
 <r xmlns=""http://www.example.org/b"" b:title=""foo"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p,*|q,*|r,*|s");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*[a|title=\"foo\"]");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-99.xml
        /// </summary>
        [Test]
        public void AttributeSpaceSeparatedValueSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t bar u"">This paragraph should have a green background.</p>
 <q xmlns=""http://www.example.org/a"" a:foo=""hgt bardot f"">This paragraph should be unstyled.</q>
 <r xmlns=""http://www.example.org/a"" a:foo=""hgt bar f"">This paragraph should have a green background.</r>
 <s xmlns=""http://www.example.org/b"" b:foo=""hgt bar f"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p,*|q,*|r,*|s");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|p,*|r");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*|*[a|foo~=\"bar\"], *|*[|class~=\"bar\"]");
	        Assert.AreEqual(2, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("*|*[html|class~='bar']");
	        Assert.AreEqual(0, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-99b.xml
        /// </summary>
        [Test]
        public void AttributeSpaceSeparatedValueSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"" class=""t bar u"">This paragraph should have a green background.</p>
 <q xmlns=""http://www.example.org/a"" a:foo=""hgt bardot f"">This paragraph should be unstyled.</q>
 <r xmlns=""http://www.example.org/a"" a:foo=""hgt bar f"">This paragraph should have a green background.</r>
 <s xmlns=""http://www.example.org/b"" b:foo=""hgt bar f"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p,*|q,*|r,*|s");
	        Assert.AreEqual(4, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|p,*|r");
	        Assert.AreEqual(2, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("*|*[a|foo~='bar'], *|*[html|class~=\"bar\"]");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-100.xml
        /// </summary>
        public void AttributeDashSeparatedValueSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" html:lang=""en-us"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:foo=""bargain-trash"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""bar-drink-glass"">This paragraph should have a green background</r>
<s xmlns=""http://www.example.org/b"" b:foo=""bar-drink-glass"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**bar,**en");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-100b.xml
        /// </summary>
        public void AttributeDashSeparatedValueSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" html:lang=""en-us"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:foo=""bargain-trash"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""bar-drink-glass"">This paragraph should have a green background</r>
<s xmlns=""http://www.example.org/b"" b:foo=""bar-drink-glass"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**bar,**en");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-101.xml
        /// </summary>
        public void SubstringMatchingAttributeValueSelectorOnBeginningWithDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""et si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**si on,**");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-101b.xml
        /// </summary>
        public void SubstringMatchingAttributeValueSelectorOnBeginningWithDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""et si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**si on,**");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-102.xml
        /// </summary>
        public void SubstringMatchingAttributeValueSelectorOnEndWithDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" xmlns:a=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" xmlns:a=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" xmlns:b=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**tait,ptait");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("**tait,**tait");
	        Assert.AreEqual(0, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-102b.xml
        /// </summary>
        public void SubstringMatchingAttributeValueSelectorOnEndWithDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" xmlns:a=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" xmlns:a=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" xmlns:b=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**tait,**tait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-103.xml
        /// </summary>
        public void SubstringMatchingAttributeValueSelectorOnMiddleWithDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**hanta,phanta");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("**hanta,**hanta");
	        Assert.AreEqual(0, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-103b.xml
        /// </summary>
        public void SubstringMatchingAttributeValueSelectorOnMiddleWithDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**hanta,**hanta");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-104.xml
        /// </summary>
        public void AttributeExistenceSelectorWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**title");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-104b.xml
        /// </summary>
        public void AttributeExistenceSelectorWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**title");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-105.xml
        /// </summary>
        public void AttributeValueSelectorWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should be unstyled.</q>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titlesi on chantait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-105b.xml
        /// </summary>
        public void AttributeValueSelectorWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should be unstyled.</q>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titlesi on chantait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-106.xml
        /// </summary>
        public void AttributeSpaceSeparatedValueSelectorWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""un deux trois"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:bar=""un deux trois"">This paragraph should be unstyled.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un second deuxieme trois"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un deux trois"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un deux trois"">This paragraph should have a green background.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**classdeux,**foodeux");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-106b.xml
        /// </summary>
        public void AttributeSpaceSeparatedValueSelectorWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""un deux trois"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:bar=""un deux trois"">This paragraph should be unstyled.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un second deuxieme trois"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un deux trois"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un deux trois"">This paragraph should have a green background.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**classdeux,**foodeux");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-107.xml
        /// </summary>
        public void AttributeDashSeparatedValueSelectorWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-us"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" a:foo=""un-deux-trois"">This paragraph should be unstyled.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un-second-deuxieme-trois"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un-d-trois"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un-d-trois"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**langen,**un-d");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-107b.xml
        /// </summary>
        public void AttributeDashSeparatedValueSelectorWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-us"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" a:foo=""un-deux-trois"">This paragraph should be unstyled.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un-second-deuxieme-trois"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un-d-trois"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un-d-trois"">This paragraph should be unstyled.</s>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**langen,**un-d");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-108.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnBeginningWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titlesi on");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-108b.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnBeginningWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titlesi on");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-109.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnEndWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titletait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-109b.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnEndWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titletait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-110.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnMiddleWithUniversalNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titleon ch");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-110b.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnMiddleWithUniversalNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**titleon ch");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-111.xml
        /// </summary>
        public void AttributeExistenceSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-111b.xml
        /// </summary>
        public void AttributeExistenceSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background</p>
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-112.xml
        /// </summary>
        public void AttributeValueSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**si on chantait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-112b.xml
        /// </summary>
        public void AttributeValueSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**si on chantait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-113.xml
        /// </summary>
        public void AttributeSpaceSeparatedValueSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""bar foo toto"">This paragraph should have a green background.</p>
<address xmlns=""http://www.w3.org/1999/xhtml"" class=""bar foofoo toto"">This address should be unstyled.</address>
<q xmlns=""http://www.example.org/a"" class=""bar foo toto"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/b"" b:class=""bar foo toto"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**foo");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-113b.xml
        /// </summary>
        public void AttributeSpaceSeparatedValueSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""bar foo toto"">This paragraph should have a green background.</p>
<address xmlns=""http://www.w3.org/1999/xhtml"" class=""bar foofoo toto"">This address should be unstyled.</address>
<q xmlns=""http://www.example.org/a"" class=""bar foo toto"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/b"" b:class=""bar foo toto"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**foo");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-114.xml
        /// </summary>
        public void AttributeDashSeparatedValueSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" lang=""foo-bar"">This paragraph should have a green background.</p>
<address xmlns=""http://www.w3.org/1999/xhtml"" lang=""foo-b"">This address should be unstyled.</address>
<address xmlns=""http://www.w3.org/1999/xhtml"" lang=""foo-barbar-toto"">This address should be unstyled.</address>
<q xmlns=""http://www.example.org/a"" myattr=""tat-tut-tot"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/b"" b:myattr=""tat-tut-tot"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**foo-bar,**tat-tut");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-114b.xml
        /// </summary>
        public void AttributeDashSeparatedValueSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" lang=""foo-bar"">This paragraph should have a green background.</p>
<address xmlns=""http://www.w3.org/1999/xhtml"" lang=""foo-b"">This address should be unstyled.</address>
<address xmlns=""http://www.w3.org/1999/xhtml"" lang=""foo-barbar-toto"">This address should be unstyled.</address>
<q xmlns=""http://www.example.org/a"" myattr=""tat-tut-tot"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/b"" b:myattr=""tat-tut-tot"">This paragraph should be unstyled.</r>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**foo-bar,**tat-tut");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-115.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnBeginningWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**si on");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-115b.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnBeginningWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**si on");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-116.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnEndWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**tait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-116b.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnEndWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**tait");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-117.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnMiddleWithoutDeclaredNamespaceA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**on ch");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-117b.xml
        /// </summary>
        public void SubstringMatchingAttributeSelectorOnMiddleWithoutDeclaredNamespaceB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" title=""si on chantait"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should have a green background.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should be unstyled.</t>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("**on ch");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-118.xml
        /// </summary>
        public void NEGATEDTypeElementSelectorWithDeclaredNamespace()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
 <p xmlns=""http://www.w3.org/1999/xhtml"">This paragraph should have a green background.</p>
 <p xmlns=""http://www.example.org/b"">This paragraph should have a green background.</p>
 <p xmlns="""">This paragraph should have a green background.</p>
 <p xmlns=""http://www.example.org/a"">
 <l>This paragraph should have a green background.</l>
 </p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*l");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.test *");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.test *:not(a)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-119.xml
        /// </summary>
        public void NEGATEDTypeElementSelectorWithUniversalNamespace()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">
<div class=""stub"">
<p>This paragraph should have a green background.</p>
<p xmlns=""http://www.example.org/b"">This paragraph should have a green background.</p>
<p xmlns="""">This paragraph should have a green background.</p>
<p xmlns=""http://www.example.org/a"">This paragraph should have a green background.</p>
</div>
<address>This address should have a green background.</address>
<s xmlns=""http://www.example.org/b"">This paragraph should have a green background.</s>
<t xmlns="""">This paragraph should have a green background.</t>
<u xmlns=""http://www.example.org/a"">This paragraph should have a green background.</u>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.test *:not(div)");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.test *:not(p):not(div)");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *:not(div)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-120.xml
        /// </summary>
        public void NEGATEDTypeElementSelectorWithoutDeclaredNamespace()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p>This paragraph should have a green background</p>
<p xmlns=""http://www.example.org/b"">This paragraph should have a green background</p>
<l xmlns=""http://www.example.org/b"">
<p xmlns="""">This paragraph should have a
                green background</p>
</l>
<p xmlns=""http://www.example.org/a"">This paragraph should have a green background</p>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub *");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub *:not(p)");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *l *:not(p)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-121.xml
        /// </summary>
        public void NEGATEDUniversalSelectorWithDeclaredNamespace()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<address>This address should be in green characters.</address>
<s xmlns=""http://www.example.org/b"">This paragraph should be in green characters.</s>
<t xmlns="""">This paragraph should be in green characters.</t>
<u xmlns=""http://www.example.org/a"">
<v>This paragraph should be in green characters.</v>
</u>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub **:not(a)");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub v");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-122.xml
        /// </summary>
        public void NEGATEDUniversalSelectorWithUniversalNamespace()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<address>This address should have a green background</address>
<s xmlns=""http://www.example.org/b"">This paragraph should have a green background</s>
<t xmlns="""">This paragraph should have a green background</t>
<u xmlns=""http://www.example.org/a"">This paragraph should have a green background</u>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-123.xml
        /// </summary>
        public void NEGATEDUniversalSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<address>This address should be in green characters.</address>
<s xmlns=""http://www.example.org/b"">This paragraph should be in green characters.</s>
<u xmlns=""http://www.example.org/a"">This paragraph should be in green characters.</u>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-123b.xml
        /// </summary>
        public void NEGATEDUniversalSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<t xmlns="""">This paragraph should be in green characters.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-124.xml
        /// </summary>
        public void NEGATEDAttributeValueSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""foo"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""foo"">This paragraph should be unstyled.</q>
<s xmlns=""http://www.example.org/a"" a:title=""foobar"">This paragraph should have a green background.</s>
<r xmlns=""http://www.example.org/b"" b:title=""foo"">This paragraph should have a green background.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *:not(foo)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-124b.xml
        /// </summary>
        public void NEGATEDAttributeValueSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""foo"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""foo"">This paragraph should be unstyled.</q>
<s xmlns=""http://www.example.org/a"" a:title=""foobar"">This paragraph should have a green background.</s>
<r xmlns=""http://www.example.org/b"" b:title=""foo"">This paragraph should have a green background.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p,*r,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *:not(foo)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-125.xml
        /// </summary>
        public void NEGATEDAttributeSpaceSeparatedValueSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""hgt bardot f"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""hgt bar f"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""hgt bar f"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(bar)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-125b.xml
        /// </summary>
        public void NEGATEDAttributeSpaceSeparatedValueSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""hgt bardot f"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""hgt bar f"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""hgt bar f"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(bar)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-126.xml
        /// </summary>
        public void NEGATEDAttributeDashSeparatedValueSelectorWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""bargain-trash"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""bar-drink-glass"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""bar-drink-glass"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(bar)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-126b.xml
        /// </summary>
        public void NEGATEDAttributeDashSeparatedValueSelectorWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""bargain-trash"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""bar-drink-glass"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""bar-drink-glass"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(bar)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-127.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeValueSelectorOnBeginningWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""et si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(si)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-127b.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeValueSelectorOnBeginningWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""et si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(si)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-128.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeValueSelectorOnEndWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(tait)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-128b.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeValueSelectorOnEndWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(tait)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-129.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeValueSelectorOnMiddleWithDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(hanta)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-129b.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeValueSelectorOnMiddleWithDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(hanta)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-130.xml
        /// </summary>
        public void NEGATEDAttributeExistenceSelectorWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-130b.xml
        /// </summary>
        public void NEGATEDAttributeExistenceSelectorWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-131.xml
        /// </summary>
        public void NEGATEDAttributeValueSelectorWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should have a green background.</q>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-131b.xml
        /// </summary>
        public void NEGATEDAttributeValueSelectorWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:foo=""si on chantait"">This paragraph should have a green background.</q>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-132.xml
        /// </summary>
        public void NEGATEDAttributeSpaceSeparatedValueSelectorWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p class=""un deux trois"">This paragraph should be unstyled</p>
<p class=""un deu trois"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:bar=""un deux trois"">This paragraph should have a green background.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un second deuxieme trois"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un deux trois"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un deux trois"">This paragraph should be unstyled.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.deu,*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub html*:not(class),div.stub **:not(html):not(foo)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-132b.xml
        /// </summary>
        public void NEGATEDAttributeSpaceSeparatedValueSelectorWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p class=""un deux trois"">This paragraph should be unstyled</p>
<p class=""un deu trois"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:bar=""un deux trois"">This paragraph should have a green background.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un second deuxieme trois"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un deux trois"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un deux trois"">This paragraph should be unstyled.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.deu,*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub html*:not(class),div.stub **:not(html):not(foo)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-133.xml
        /// </summary>
        public void NEGATEDAttributeDashSeparatedValueSelectorWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p lang=""en-us"">This paragraph should be unstyled.</p>
<p lang=""fr"" class=""foo"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:foo=""un-deux-trois"">This paragraph should have a green background.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un-second-deuxieme-trois"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un-d-trois"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un-d-trois"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.foo,*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub html*:not(lang),div.stub **:not(html):not(un-d)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-133b.xml
        /// </summary>
        public void NEGATEDAttributeDashSeparatedValueSelectorWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p lang=""en-us"">This paragraph should be unstyled.</p>
<p lang=""fr"" class=""foo"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:foo=""un-deux-trois"">This paragraph should have a green background.</q>
<q xmlns=""http://www.example.org/a"" a:foo=""un-second-deuxieme-trois"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:foo=""un-d-trois"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:foo=""un-d-trois"">This paragraph should have a green background.</s>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.foo,*q,*s");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub html*:not(lang),div.stub **:not(html):not(un-d)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-134.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeSelectorOnBeginningWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<p title=""si il chantait"" class=""red"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.red,*q,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-134b.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeSelectorOnBeginningWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<p title=""si il chantait"" class=""red"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.red,*q,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-135.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeSelectorOnEndWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<p title=""si tu chantais"" class=""red"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.red,*q,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-135b.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeSelectorOnEndWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<p title=""si tu chantais"" class=""red"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.red,*q,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-136.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeSelectorOnMiddleWithUniversalNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<p title=""si il chantait"" class=""red"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.red,*q,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-136b.xml
        /// </summary>
        public void NEGATEDSubstringMatchingAttributeSelectorOnMiddleWithUniversalNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<p title=""si il chantait"" class=""red"">This paragraph should have a green background.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should be unstyled.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*p.red,*q,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(title)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-137.xml
        /// </summary>
        public void NEGATEDAttributeExistenceSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-137b.xml
        /// </summary>
        public void NEGATEDAttributeExistenceSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-138.xml
        /// </summary>
        public void NEGATEDAttributeValueSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(si)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-138b.xml
        /// </summary>
        public void NEGATEDAttributeValueSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" a:title=""si on chantait"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*q,*r,*s,*t");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*q,*s,*t");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(si)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-139.xml
        /// </summary>
        public void NEGATEDAttributeSpaceSeparatedValueSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p class=""bar foo toto"">This paragraph should be unstyled.</p>
<address class=""bar foofoo toto"">This address should have a green background.</address>
<q xmlns=""http://www.example.org/a"" class=""bar foo toto"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/b"" b:class=""bar foo toto"">This paragraph should have a green background.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*address,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(foo)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-139b.xml
        /// </summary>
        public void NEGATEDAttributeSpaceSeparatedValueSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p class=""bar foo toto"">This paragraph should be unstyled.</p>
<address class=""bar foofoo toto"">This address should have a green background.</address>
<q xmlns=""http://www.example.org/a"" class=""bar foo toto"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/b"" b:class=""bar foo toto"">This paragraph should have a green background.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*address,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(foo)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-140.xml
        /// </summary>
        public void NEGATEDAttributeDashSeparatedValueSelectorWithoutDeclaredNamespaceA()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p lang=""foo-bar"">This paragraph should be unstyled.</p>
<address lang=""foo-b"">This address should have a green background.</address>
<address lang=""foo-barbar-toto"">This address should have a green background.</address>
<q xmlns=""http://www.example.org/a"" lang=""foo-bar"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/b"" b:lang=""foo-bar"">This paragraph should have a green background.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*address,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(foo-bar)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-140b.xml
        /// </summary>
        public void NEGATEDAttributeDashSeparatedValueSelectorWithoutDeclaredNamespaceB()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p lang=""foo-bar"">This paragraph should be unstyled.</p>
<address lang=""foo-b"">This address should have a green background.</address>
<address lang=""foo-barbar-toto"">This address should have a green background.</address>
<q xmlns=""http://www.example.org/a"" lang=""foo-bar"">This paragraph should be unstyled.</q>
<r xmlns=""http://www.example.org/b"" b:lang=""foo-bar"">This paragraph should have a green background.</r>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*p,*address,*q,*r");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*address,*r");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub **:not(foo-bar)");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-141.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnBeginningWithoutDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s, *|t");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q, *|s, *|t");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([|title^='si on'])");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-141b.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnBeginningWithoutDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" b:ti=""si on chantait"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s, *|t");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q,*|s,*|t");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([|title^=\"si on\"])");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-142.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnEndWithoutDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s, *|t");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q, *|s, *|t");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([|title$=tait])");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-142b.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnEndWithoutDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s, *|t");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q, *|s, *|t");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([|title$=tait])");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-143.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnMiddleWithoutDeclaredNamespaceA()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s, *|t");
	        Assert.AreEqual(5, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("*|q,*|s,*|t");
	        Assert.AreEqual(3, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div.stub *|*:not([|title*='on ch'])");
	        Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-143b.xml
        /// </summary>
        [Test]
        public void NegatedSubstringMatchingAttributeSelectorOnMiddleWithoutDeclaredNamespaceB()
        {
	        var source = @"<style>@namespace a url(http://www.example.org/a);
@namespace b url(http://www.example.org/b);
@namespace html url(http://www.w3.org/1999/xhtml);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<p title=""si on chantait"">This paragraph should be unstyled.</p>
<q xmlns=""http://www.example.org/a"" a:title=""si nous chantions"">This paragraph should have a green background.</q>
<r xmlns=""http://www.example.org/a"" title=""si on chantait"">This paragraph should be unstyled.</r>
<s xmlns=""http://www.example.org/b"" b:title=""si on chantait"">This paragraph should have a green background.</s>
<t xmlns=""http://www.example.org/b"" title=""si nous chantions"">This paragraph should have a green background.</t>
</div>";
	        var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("*|p, *|q, *|r, *|s, *|t");
            Assert.AreEqual(5, selector1.Length);
            var selector2 = doc.QuerySelectorAll("*|q,*|s,*|t");
            Assert.AreEqual(3, selector2.Length);
            var selector3 = doc.QuerySelectorAll("div.stub *|*:not([|title*='on ch'])");
            Assert.AreEqual(3, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-144.xml
        /// </summary>
        [Test]
        public void NegatedEnabledDisabledPseudoClasses()
        {
	        var source = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <p>This paragraph should have a green background.</p>
 </div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("div :not(:enabled):not(:disabled)");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-145a.xml
        /// </summary>
        [Test]
        public void NthOfTypePseudoClassWithHiddenElementsA()
        {
	        var source = @"<test xmlns=""http://www.example.org/"">
  <line type=""odd"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"" hidden=""hidden"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""odd"" hidden=""hidden"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
 </test>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("line");
	        Assert.AreEqual(15, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("[type~=odd]");
	        Assert.AreEqual(8, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("line:nth-of-type(odd)");
	        Assert.AreEqual(8, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("[hidden]");
	        Assert.AreEqual(4, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-145b.xml
        /// </summary>
        [Test]
        public void NthOfTypePseudoClassWithHiddenElementsB()
        {
	        var source = @"<test xmlns=""http://www.example.org/"">
  <line type=""odd"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"" hidden=""hidden"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
  <line type=""even"" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""odd"" hidden=""hidden"">This line should be green.</line>
  <line type=""even"">This line should be unstyled.</line>
  <line type=""odd"">This line should be green.</line>
 </test>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("line");
	        Assert.AreEqual(15, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("[type~=odd]");
	        Assert.AreEqual(8, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("line:nth-of-type(odd)");
	        Assert.AreEqual(8, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("[hidden]");
	        Assert.AreEqual(4, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-146a.xml
        /// </summary>
        [Test]
        public void NthChildPseudoClassWithHiddenElementsC()
        {
	        var source = @"<test xmlns=""http://www.example.org/"">
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""match"" hidden=""hidden"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
 </test>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("line");
	        Assert.AreEqual(21, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("[type~=match]");
	        Assert.AreEqual(7, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("line:nth-child(3n-1)");
	        Assert.AreEqual(7, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("[hidden]");
	        Assert.AreEqual(3, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-146b.xml
        /// </summary>
        [Test]
        public void NthChildPseudoClassWithHiddenElementsD()
        {
	        var source = @"<test xmlns=""http://www.example.org/"">
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type=""match"" hidden=""hidden"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
 </test>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("line");
	        Assert.AreEqual(21, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("[type~=match]");
	        Assert.AreEqual(7, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("line:nth-child(3n-1)");
	        Assert.AreEqual(7, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("[hidden]");
	        Assert.AreEqual(3, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-147a.xml
        /// </summary>
        [Test]
        public void NthLastOfTypePseudoClassWithCollapsedElementsA()
        {
	        var source = @"<test xmlns=""http://www.example.org/"">
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"" hidden=""hidden"">This line should be green.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
 </test>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("line");
	        Assert.AreEqual(21, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("[type~=match]");
	        Assert.AreEqual(7, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("line:nth-last-of-type(3n-1)");
	        Assert.AreEqual(7, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("[hidden]");
	        Assert.AreEqual(3, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-147b.xml
        /// </summary>
        [Test]
        public void NthLastOfTypePseudoClassWithCollapsedElementsB()
        {
	        var source = @"<test xmlns=""http://www.example.org/"">
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"" hidden=""hidden"">This line should be green.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""" hidden=""hidden"">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
  <line type="""">This line should be unstyled.</line>
  <line type=""match"">This line should be green.</line>
  <line type="""">This line should be unstyled.</line>
 </test>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("line");
	        Assert.AreEqual(21, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("[type~=match]");
	        Assert.AreEqual(7, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("line:nth-last-of-type(3n-1)");
	        Assert.AreEqual(7, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("[hidden]");
	        Assert.AreEqual(3, selector4.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-148.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndText()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:empty");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-149.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndEmptyElementsA()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml""></address>
 <div xmlns=""http://www.w3.org/1999/xhtml"" class=""text"">This line should have a green background.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address:empty");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".text");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-149b.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndEmptyElementsB()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml""></address>
 <div xmlns=""http://www.w3.org/1999/xhtml"" class=""text"">This line should have a green background.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address:empty");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".text");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-150.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndXMLSgmlConstructs()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml""><!-- --></address>
 <div xmlns=""http://www.w3.org/1999/xhtml"" class=""text"">This line should have a green background.</div>
 <p xmlns=""http://www.w3.org/1999/xhtml"">(Note: This test is based on unpublished errata.)</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address:empty");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".text");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-151.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndWhitespace()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml""> </address>
 <div xmlns=""http://www.w3.org/1999/xhtml"" class=""text"">This line should have a green background.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address:empty");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".text");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-152.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndElements()
        {
	        var source = @"<address xmlns=""http://www.w3.org/1999/xhtml""><span></span></address>
 <div xmlns=""http://www.w3.org/1999/xhtml"" class=""text"">This line should have a green background.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address:empty");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".text");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-153.xml
        /// </summary>
        [Test]
        public void EmptyPseudoClassAndCData()
        {
	        var source = @"<address xmlns=""http://tests.example.org/xml-only/""></address>
 <div xmlns=""http://www.w3.org/1999/xhtml"" class=""text"">This line should have a green background.</div>
 <p xmlns=""http://www.w3.org/1999/xhtml"">(Note: This test is based on unpublished errata.)</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("address");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("address:empty");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll(".text");
	        Assert.AreEqual(1, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-154.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll("p,");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-155.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""5cm"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll(".5cm");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-155a.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingC()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""5cm"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(@".\5cm");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-155b.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingD()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""two words"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".two\\ words");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-155c.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingE()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""one.word"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".one.word");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-155d.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingF()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""one.word"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".one\\.word");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-156.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingG()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll("foo & address, p");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-156b.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingH()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();

            Assert.Catch<DomException>(() =>
            {
                var selector1 = doc.QuerySelectorAll("foo & address, p");
                Assert.AreEqual(0, selector1.Length);
            });

            var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-156c.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingI()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();

            Assert.Catch<DomException>(() =>
            {
                var selector1 = doc.QuerySelectorAll("foo & address, p");
                Assert.AreEqual(0, selector1.Length);
            });

            var selector2 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-157.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingJ()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll("[*=test]");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-158.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingK()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""test"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll("[*|*=test]");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-159.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingOfNewPseudoElements()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">Try selecting some text in this document. It should be have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("::selection");
	        Assert.AreEqual(0, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll(":selection");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-160.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingOfUnknownPseudoClasses()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll("p:subject");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-161.xml
        /// </summary>
        [Test]
        public void SyntaxAndParsingOfUnknownPseudoClassesAndPseudoElements()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should have a green background.</p>
 <p xmlns=""http://www.w3.org/1999/xhtml"">
  UAs may render the following element as a pop up menu. If so, please ensure the menu is unstyled (or green).
  <select size=""1"">
   <option>This should</option>
   <option>have a green</option>
   <option>background.</option>
  </select>
 </p>
 <table xmlns=""http://www.w3.org/1999/xhtml""><tr><td>This line should have a green background (or it might be unstyled).</td></tr></table>
 <!-- only allowed to be unstyled if + and ~ are not supported -->";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(2, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p   *");
	        Assert.AreEqual(4, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("p > *");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("p + *");
	        Assert.AreEqual(2, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("p ~ *");
	        Assert.AreEqual(2, selector5.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector6 = doc.QuerySelectorAll(":canvas");
                Assert.AreEqual(0, selector6.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector7 = doc.QuerySelectorAll(":viewport");
                Assert.AreEqual(0, selector7.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector8 = doc.QuerySelectorAll(":window");
                Assert.AreEqual(0, selector8.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector9 = doc.QuerySelectorAll(":menu");
                Assert.AreEqual(0, selector9.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector10 = doc.QuerySelectorAll(":table");
                Assert.AreEqual(0, selector10.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector11 = doc.QuerySelectorAll(":select");
                Assert.AreEqual(0, selector11.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector12 = doc.QuerySelectorAll("::canvas");
                Assert.AreEqual(0, selector12.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector13 = doc.QuerySelectorAll("::viewport");
                Assert.AreEqual(0, selector13.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector14 = doc.QuerySelectorAll("::window");
                Assert.AreEqual(0, selector14.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector15 = doc.QuerySelectorAll("::menu");
                Assert.AreEqual(0, selector15.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector16 = doc.QuerySelectorAll("::table");
                Assert.AreEqual(0, selector16.Length);
            });

            Assert.Catch<DomException>(() =>
            {
                var selector17 = doc.QuerySelectorAll("::select");
                Assert.AreEqual(0, selector17.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-166.xml
        /// </summary>
        [Test]
        public void FirstLetterWithFirstLetterA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The first letter of this paragraph should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:first-letter");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p::first-letter");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-166a.xml
        /// </summary>
        [Test]
        public void FirstLetterWithFirstLetterB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The first letter of this paragraph should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p::first-letter");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:first-letter");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-167.xml
        /// </summary>
        [Test]
        public void FirstLineWithFirstLineA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The first line of this paragraph should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p:first-line");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p::first-line");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-167a.xml
        /// </summary>
        [Test]
        public void FirstLineWithFirstLineB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">The first line of this paragraph should have a green background.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p::first-line");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:first-line");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-168.xml
        /// </summary>
        [Test]
        public void BeforeWithBeforeA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This test has <span></span>.</p>
   <p xmlns=""http://www.w3.org/1999/xhtml"">(If the previous line just reads This test has . then this test has failed.)</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("span:before");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("span::before");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-168a.xml
        /// </summary>
        [Test]
        public void BeforeWithBeforeB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This test has <span></span>.</p>
   <p xmlns=""http://www.w3.org/1999/xhtml"">(If the previous line just reads This test has . then this test has failed.)</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("span::before");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("span:before");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-169.xml
        /// </summary>
        [Test]
        public void AfterWithAfterA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This test has <span></span>.</p>
   <p xmlns=""http://www.w3.org/1999/xhtml"">(If the previous line just reads This test has . then this test has failed.)</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("span:after");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("span::after");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-169a.xml
        /// </summary>
        [Test]
        public void AfterWithAfterB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This test has <span></span>.</p>
   <p xmlns=""http://www.w3.org/1999/xhtml"">(If the previous line just reads This test has . then this test has failed.)</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("span::after");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("span:after");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-170a.xml
        /// </summary>
        [Test]
        public void LongChainsOfSelectorsA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""span"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".span");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span,.span");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-170b.xml
        /// </summary>
        [Test]
        public void LongChainsOfSelectorsB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""span"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll(".span");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span.span");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-170c.xml
        /// </summary>
        [Test]
        public void LongChainsOfSelectorsC()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p.span");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span):not(.span)");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-170d.xml
        /// </summary>
        [Test]
        public void LongChainsOfSelectorsD()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child:first-child");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-170.xml
        /// </summary>
        [Test]
        public void LongChainsOfSelectorsE()
        {
            var source = @"<p xmlns=""http://www.w3.org/1999/xhtml""><span>This line should be green.</span></p>";
            var doc = source.ToHtmlDocument();

            var selector1 = doc.QuerySelectorAll("span");
            Assert.AreEqual(1, selector1.Length);
            var selector2 = doc.QuerySelectorAll("span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span,span");
            Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-172a.xml
        /// </summary>
        [Test]
        public void NamespacedAttributeSelectorsA()
        {
	        var source = @"<tests xmlns=""http://css.example.net/"" xmlns:test=""http://css.example.net/"">
   <testA test:attribute=""fail"">This should be green.</testA>
   <testB test:attribute=""fail"">This should be green.</testB>
   <testC test:attribute=""fail"">This should be green.</testC>
   <testD test:attribute=""fail"">This should be green.</testD>
   <testE test:attribute=""fail"">This should be green.</testE>
   <testF test:attribute=""fail"">This should be green.</testF>
   <testG test:attribute=""fail"">This should be green.</testG>
  </tests>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("tests,tests *");
	        Assert.AreEqual(8, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("testA[|attribute]");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("testB[|attribute=\"fail\"]");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("testC[|attribute~='fail']");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("testD[|attribute^='fail']");
	        Assert.AreEqual(0, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("testE[|attribute*='fail']");
	        Assert.AreEqual(0, selector6.Length);
	        var selector7 = doc.QuerySelectorAll("testF[|attribute$='fail']");
	        Assert.AreEqual(0, selector7.Length);
	        var selector8 = doc.QuerySelectorAll("testF[|attribute|='fail']");
	        Assert.AreEqual(0, selector8.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-172b.xml
        /// </summary>
        [Test]
        public void NamespacedAttributeSelectorsB()
        {
	        var source = @"<style>@namespace url(http://css.example.net/);</style>
<tests xmlns=""http://css.example.net/"" xmlns:test=""http://css.example.net/"">
   <testA test:attribute=""fail"">This should be green.</testA>
   <testB test:attribute=""fail"">This should be green.</testB>
   <testC test:attribute=""fail"">This should be green.</testC>
   <testD test:attribute=""fail"">This should be green.</testD>
   <testE test:attribute=""fail"">This should be green.</testE>
   <testF test:attribute=""fail"">This should be green.</testF>
   <testG test:attribute=""fail"">This should be green.</testG>
  </tests>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("tests,tests *");
	        Assert.AreEqual(8, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("testA[|attribute]");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("testB[|attribute=\"fail\"]");
	        Assert.AreEqual(0, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("testC[|attribute~=\"fail\"]");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("testD[|attribute^='fail']");
            Assert.AreEqual(0, selector5.Length);
            var selector6 = doc.QuerySelectorAll("testE[|attribute*='fail']");
	        Assert.AreEqual(0, selector6.Length);
	        var selector7 = doc.QuerySelectorAll("testF[|attribute$=fail]");
	        Assert.AreEqual(0, selector7.Length);
	        var selector8 = doc.QuerySelectorAll("testG[|attribute|=fail]");
	        Assert.AreEqual(0, selector8.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-173a.xml
        /// </summary>
        [Test]
        public void NamespacedAttributeSelectorsC()
        {
	        var source = @"<tests>
   <testA attribute=""pass"">This should be green.</testA>
   <testB attribute=""pass"">This should be green.</testB>
   <testC attribute=""pass"">This should be green.</testC>
   <testD attribute=""pass"">This should be green.</testD>
   <testE attribute=""pass"">This should be green.</testE>
   <testF attribute=""pass"">This should be green.</testF>
   <testG attribute=""pass"">This should be green.</testG>
  </tests>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("tests,tests *");
	        Assert.AreEqual(8, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("testA[*|attribute]");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("testB[*|attribute='pass']");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("testC[*|attribute~='pass']");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("testD[*|attribute^='pass']");
	        Assert.AreEqual(1, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("testE[*|attribute*='pass']");
	        Assert.AreEqual(1, selector6.Length);
	        var selector7 = doc.QuerySelectorAll("testF[*|attribute$='pass']");
	        Assert.AreEqual(1, selector7.Length);
	        var selector8 = doc.QuerySelectorAll("testG[*|attribute|='pass']");
	        Assert.AreEqual(1, selector8.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-173b.xml
        /// </summary>
        [Test]
        public void NamespacedAttributeSelectorsD()
        {
	        var source = @"<tests xmlns=""http://css.example.net/"" xmlns:test=""http://css.example.net/"">
   <testA attribute=""pass"">This should be green.</testA>
   <testB attribute=""pass"">This should be green.</testB>
   <testC attribute=""pass"">This should be green.</testC>
   <testD attribute=""pass"">This should be green.</testD>
   <testE attribute=""pass"">This should be green.</testE>
   <testF attribute=""pass"">This should be green.</testF>
   <testG attribute=""pass"">This should be green.</testG>
  </tests>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("tests,tests *");
	        Assert.AreEqual(8, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("testA[*|attribute]");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("testB[*|attribute=pass]");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("testC[*|attribute~='pass']");
	        Assert.AreEqual(1, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("testD[*|attribute^='pass']");
	        Assert.AreEqual(1, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("testE[*|attribute*=pass]");
	        Assert.AreEqual(1, selector6.Length);
	        var selector7 = doc.QuerySelectorAll("testF[*|attribute$=\"pass\"]");
	        Assert.AreEqual(1, selector7.Length);
	        var selector8 = doc.QuerySelectorAll("testG[*|attribute|=\"pass\"]");
	        Assert.AreEqual(1, selector8.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-174a.xml
        /// </summary>
        public void AttributeSelectorsWithMultipleAttributes()
        {
	        var source = @"<tests xmlns=""http://css.example.net/"" xmlns:test=""http://css.example.net/"">
   <testA attribute=""pass"" test:attribute=""fail"">This should be green.</testA>
   <testB attribute=""fail"" test:attribute=""pass"">This should be green.</testB>
  </tests>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("tests,tests *");
	        Assert.AreEqual(0, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("testAattributepass");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("testBattributepass");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-174b.xml
        /// </summary>
        public void NegatedAttributeSelectorsWithMultipleAttributes()
        {
	        var source = @"<tests xmlns=""http://css.example.net/"" xmlns:test=""http://css.example.net/"">
   <testA attribute=""pass"" test:attribute=""fail"">This should be green.</testA>
   <testB attribute=""fail"" test:attribute=""pass"">This should be green.</testB>
  </tests>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("tests, tests *");
	        Assert.AreEqual(3, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("testA:not([*|attribute=\"pass\"])");
	        Assert.AreEqual(0, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("testB:not([*|attribute=pass])");
	        Assert.AreEqual(0, selector3.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-175a.xml
        /// </summary>
        [Test]
        public void ParsingNumbersInClassesA()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""13"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);

            Assert.Catch<DomException>(() =>
            {
                var selector2 = doc.QuerySelectorAll(".13");
                Assert.AreEqual(0, selector2.Length);
            });
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-175b.xml
        /// </summary>
        [Test]
        public void ParsingNumbersInClassesB()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""13"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".\\13");
	        Assert.AreEqual(0, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-175c.xml
        /// </summary>
        [Test]
        public void ParsingNumbersInClassesC()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" class=""13"">This line should be green.</p>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll(".\\31 \\33");
	        Assert.AreEqual(1, selector2.Length);
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-176.xml
        /// </summary>
        [Test]
        public void CombinationsClassesAndIds()
        {
	        var source = @"<p xmlns=""http://www.w3.org/1999/xhtml"" id=""id"" class=""class test"">This line should be green.</p>
  <div xmlns=""http://www.w3.org/1999/xhtml"" id=""theid"" class=""class test"">This line should be green.</div>";
	        var doc = source.ToHtmlDocument();
	        
	        var selector1 = doc.QuerySelectorAll("p");
	        Assert.AreEqual(1, selector1.Length);
	        var selector2 = doc.QuerySelectorAll("p:not(#other).class:not(.fail).test#id#id");
	        Assert.AreEqual(1, selector2.Length);
	        var selector3 = doc.QuerySelectorAll("div");
	        Assert.AreEqual(1, selector3.Length);
	        var selector4 = doc.QuerySelectorAll("div:not(#theid).class:not(.fail).test#theid#theid");
	        Assert.AreEqual(0, selector4.Length);
	        var selector5 = doc.QuerySelectorAll("div:not(#other).notclass:not(.fail).test#theid#theid");
	        Assert.AreEqual(0, selector5.Length);
	        var selector6 = doc.QuerySelectorAll("div:not(#other).class:not(.test).test#theid#theid");
	        Assert.AreEqual(0, selector6.Length);
	        var selector7 = doc.QuerySelectorAll("div:not(#other).class:not(.fail).nottest#theid#theid");
	        Assert.AreEqual(0, selector7.Length);
	        var selector8 = doc.QuerySelectorAll("div:not(#other).class:not(.fail).nottest#theid#other");
	        Assert.AreEqual(0, selector8.Length);
        }
    }
}
