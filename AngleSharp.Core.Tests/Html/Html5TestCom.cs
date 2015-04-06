using System;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/html5test-com.dat
    /// </summary>
    [TestFixture]
    public class Html5TestComTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void WrongDivTagMistake()
        {
            var doc = Html(@"<div<div>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydivdiv = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodydivdiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydivdiv.Attributes.Count);
            Assert.AreEqual("div<div", dochtmlbodydivdiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydivdiv.NodeType);
        }

        [Test]
        public void WrongDivAttributeMistake()
        {
            var doc = Html(@"<div foo<bar=''>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);
            Assert.AreEqual("", dochtmlbodydiv.GetAttribute("foo<bar"));
        }

        [Test]
        public void WrongDivLetterInAttributeMistake()
        {
            var doc = Html(@"<div foo=`bar`>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);
            Assert.AreEqual("`bar`", dochtmlbodydiv.Attributes.Get("foo").Value);
        }

        [Test]
        public void EntitiesAngles()
        {
            var doc = Html(@"&lang;&rang;");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"⟨⟩", text.TextContent);
        }

        [Test]
        public void EntitiesApos()
        {
            var doc = Html(@"&apos;");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"'", text.TextContent);
        }

        [Test]
        public void EntitiesKopf()
        {
            var doc = Html(@"&Kopf;");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"𝕂", text.TextContent);
        }

        [Test]
        public void EntitiesNotinva()
        {
            var doc = Html(@"&notinva;");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"∉", text.TextContent);
        }

        [Test]
        public void BogusCommentAsDoctype()
        {
            var doc = Html(@"<?import namespace=""foo"" implementation=""#bar"">");

            var comment = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, comment.NodeType);
            Assert.AreEqual(@"?import namespace=""foo"" implementation=""#bar""", comment.TextContent);

            var dochtml = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void MisplacedCdataSection()
        {
            var doc = Html(@"<![CDATA[x]]>");
            var cdata = doc.ChildNodes[0];
            Assert.AreEqual(0, cdata.ChildNodes.Length);
            Assert.AreEqual("[CDATA[x]]", cdata.TextContent);
            Assert.AreEqual(NodeType.Comment, cdata.NodeType);

            var dochtml = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void TextAreaWithComments()
        {
            var doc = Html(@"<textarea><!--</textarea>--></textarea>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodytextarea = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodytextarea.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytextarea.Attributes.Count);
            Assert.AreEqual("textarea", dochtmlbodytextarea.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytextarea.NodeType);

            var text1 = dochtmlbodytextarea.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"<!--", text1.TextContent);

            var text2 = dochtmlbody.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"-->", text2.TextContent);
        }

        [Test]
        public void UnsortedListWithEntries()
        {
            var doc = Html(@"<ul><li>A </li> <li>B</li></ul>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyul = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtmlbodyul.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyul.Attributes.Count);
            Assert.AreEqual("ul", dochtmlbodyul.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyul.NodeType);

            var dochtmlbodyulli1 = dochtmlbodyul.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodyulli1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyulli1.Attributes.Count);
            Assert.AreEqual("li", dochtmlbodyulli1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyulli1.NodeType);

            var text1 = dochtmlbodyulli1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"A ", text1.TextContent);

            var text2 = dochtmlbodyul.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@" ", text2.TextContent);

            var dochtmlbodyulli2 = dochtmlbodyul.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtmlbodyulli2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyulli2.Attributes.Count);
            Assert.AreEqual("li", dochtmlbodyulli2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyulli2.NodeType);

            var text3 = dochtmlbodyulli2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@"B", text3.TextContent);
        }

        [Test]
        public void TableWithFormAndInputs()
        {
            var doc = Html(@"<table><form><input type=hidden><input></form><div></div></table>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyinput = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodyinput.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyinput.Attributes.Count);
            Assert.AreEqual("input", dochtmlbodyinput.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyinput.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);

            var dochtmlbodytable = dochtmlbody.ChildNodes[2] as Element;
            Assert.AreEqual(2, dochtmlbodytable.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var dochtmlbodytableform = dochtmlbodytable.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodytableform.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytableform.Attributes.Count);
            Assert.AreEqual("form", dochtmlbodytableform.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytableform.NodeType);

            var dochtmlbodytableinput = dochtmlbodytable.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbodytableinput.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodytableinput.Attributes.Count);
            Assert.AreEqual("input", dochtmlbodytableinput.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytableinput.NodeType);
            Assert.AreEqual("hidden", dochtmlbodytableinput.Attributes.Get("type").Value);
        }

        [Test]
        public void MathMLTag()
        {
            var doc = Html(@"<math></math>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);
            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodymath = dochtmlbody.ChildNodes[0] as Element;
            Assert.IsTrue(dochtmlbodymath.Flags.HasFlag(NodeFlags.MathMember));
            Assert.AreEqual(Namespaces.MathMlUri, dochtmlbodymath.NamespaceUri);
            Assert.AreEqual(0, dochtmlbodymath.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodymath.Attributes.Count);
            Assert.AreEqual("math", dochtmlbodymath.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodymath.NodeType);
        }

        [Test]
        public void TabsInClassNames()
        {
            var html = "<html><body><div class=\"class1\tclass2\"></div></body></html>";
            var dom = Html(html);
            var div = dom.QuerySelector("div");

            Assert.AreEqual(2, div.ClassList.Length);
            Assert.IsTrue(div.ClassList.Contains("class1"));
            Assert.IsTrue(div.ClassList.Contains("class2"));
        }

        [Test]
        public void NewLinesInClassNames()
        {
            var html = "<html><body><div class=\"class1" + Environment.NewLine + "class2  class3\r\n\t class4\"></div></body></html>";
            var dom = Html(html);
            var div = dom.QuerySelector("div");

            Assert.AreEqual(4, div.ClassList.Length);
            Assert.IsTrue(div.ClassList.Contains("class1"));
            Assert.IsTrue(div.ClassList.Contains("class4"));
        }

        [Test]
        public void AutoCloseTwoTagsInARow()
        {
            var html = @" <table id=table-uda>
    <thead>
        <tr>
            <th>Attribute
             <th>Setter Condition
   <tbody><tr><td><dfn id=dom-uda-protocol title=dom-uda-protocol><code>protocol</code></dfn>
     <td><a href=#url-scheme title=url-scheme>&lt;scheme&gt;</a>
     </tr></table>";

            var dom = Html(html);

            Assert.AreEqual(1, dom.QuerySelectorAll("tbody").Length);
            Assert.AreEqual("table", dom.QuerySelector("tbody").Parent.GetTagName());
        }

        [Test]
        public void AutoCreateTableTags()
        {
            var html = @"<table id=table-uda>
        <tr>
            <th>Attribute
             <th>Setter Condition
        <tr><td><dfn id=dom-uda-protocol title=dom-uda-protocol><code>protocol</code></dfn>
     <td><a href=#url-scheme title=url-scheme>&lt;scheme&gt;</a>
     </tr></table>";
            var dom = Html(html);

            // should create wrapper
            Assert.AreEqual(1, dom.QuerySelectorAll("body").Length);
            Assert.AreEqual(1, dom.QuerySelectorAll("html").Length);
            Assert.AreEqual(1, dom.QuerySelectorAll("head").Length);
            Assert.AreEqual(1, dom.QuerySelectorAll("tbody").Length);
            Assert.AreEqual(2, dom.QuerySelectorAll("th").Length);
            Assert.AreEqual(2, dom.QuerySelectorAll("tr").Length);
            Assert.AreEqual("table", dom.QuerySelector("tbody").Parent.GetTagName());
            Assert.AreEqual(11, dom.QuerySelectorAll("body *").Length);
        }

        [Test]
        public void AutoCreateHtmlBody()
        {
            var test = @"<html>
                <head>  
            <script type=""text/javascript"">lf={version: 2064750,baseUrl: '/',helpHtml: '<a class=""email"" href=""mailto:xxxxx@xxxcom"">email</a>',prefs: { pageSize: 0}};

            lf.Scripts={""crypt"":{""path"":""/scripts/thirdp/sha512.min.2009762.js"",""nameSpace"":""Sha512""}};

            </script><link rel=""icon"" type=""image/x-icon"" href=""/favicon.ico""> 

                <title>Title</title>
            <script type=""text/javascript"" src=""/scripts/thirdp/jquery-1.7.1.min.2009762.js""></script>
            <script type=""text/javascript"">var _gaq = _gaq || [];

            _gaq.push(['_setAccount', 'UA-xxxxxxx1']);

            _gaq.push(['_trackPageview']);
            </script>

            </head>

            <body>

            <script type=""text/javascript"">
            alert('done');
            </script>";

            var dom = Html(test);
            Assert.AreEqual(4, dom.QuerySelectorAll("script").Length);
        }

        [Test]
        public void AutoCreateHead()
        {
            var test = @"<html>
            <script id=script1 type=""text/javascript"" src=""stuff""></script>
            <div id=div1>This should be in the body.</div>";

            var dom = Html(test);
            Assert.AreEqual(dom.QuerySelector("#script1"), dom.QuerySelector("head > :first-child"));
            Assert.AreEqual(dom.QuerySelector("#div1"), dom.QuerySelector("body > :first-child"));
        }

        [Test]
        public void AutoCreateBody()
        {
            var test = @"<html>
                <div id=div1>This should be in the body.</div>
                <script id=script1 type=""text/javascript"" src=""stuff""></script>";


            var dom = Html(test);

            Assert.AreEqual(0, dom.QuerySelector("head").Children.Length);
            Assert.AreEqual(2, dom.QuerySelector("body").Children.Length);

            Assert.AreEqual(dom.QuerySelector("#div1"), dom.QuerySelector("body > :first-child"));
        }

        [Test]
        public void NewLinesInTags()
        {
            var test = @"<table 
                border
                =0 cellspacing=
                ""2"" cellpadding=""2"" width=""100%""><span" + (Char)10 + "id=test></span></table>";
            var dom = test.ToHtmlFragment();

            var body = dom.QuerySelector("body");
            Assert.IsNotNull(body);

            var output = body.InnerHtml;
            Assert.AreEqual(@"<span id=""test""></span><table border=""0"" cellspacing=""2"" cellpadding=""2"" width=""100%""></table>", output);
        }

        [Test]
        public void HtmlPageSupportsRoundTripping()
        {
            var originalSourceCode = Assets.selectors;
            var initialDocument = originalSourceCode.ToHtmlDocument();
            var initialSourceCode = initialDocument.ToHtml();
            var finalDocument = initialSourceCode.ToHtmlDocument();
            var finalSourceCode = finalDocument.ToHtml();
            Assert.AreEqual(initialSourceCode, finalSourceCode);
        }
    }
}
