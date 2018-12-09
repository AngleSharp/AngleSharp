namespace AngleSharp.Core.Tests.Examples
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class WikiTests
    {
        [Test]
        public void ParsingWellDefinedDocument()
        {
            var source = @"
<!DOCTYPE html>
<html lang=en>
  <meta charset=utf-8>
  <meta name=viewport content=""initial-scale=1, minimum-scale=1, width=device-width"">
  <title>Error 404 (Not Found)!!1</title>
  <style>
    *{margin:0;padding:0}html,code{font:15px/22px arial,sans-serif}html{background:#fff;color:#222;padding:15px}body{margin:7% auto 0;max-width:390px;min-height:180px;padding:30px 0 15px}* > body{background:url(//www.google.com/images/errors/robot.png) 100% 5px no-repeat;padding-right:205px}p{margin:11px 0 22px;overflow:hidden}ins{color:#777;text-decoration:none}a img{border:0}@media screen and (max-width:772px){body{background:none;margin-top:0;max-width:none;padding-right:0}}#logo{background:url(//www.google.com/images/errors/logo_sm_2.png) no-repeat}@media only screen and (min-resolution:192dpi){#logo{background:url(//www.google.com/images/errors/logo_sm_2_hr.png) no-repeat 0% 0%/100% 100%;-moz-border-image:url(//www.google.com/images/errors/logo_sm_2_hr.png) 0}}@media only screen and (-webkit-min-device-pixel-ratio:2){#logo{background:url(//www.google.com/images/errors/logo_sm_2_hr.png) no-repeat;-webkit-background-size:100% 100%}}#logo{display:inline-block;height:55px;width:150px}
  </style>
  <a href=//www.google.com/><span id=logo aria-label=Google></span></a>
  <p><b>404.</b> <ins>That’s an error.</ins>
  <p>The requested URL <code>/error</code> was not found on this server.  <ins>That’s all we know.</ins>";

            // Create a new parser front-end (can be re-used)
            var parser = new HtmlParser();
            //Just get the DOM representation
            var document = parser.ParseDocument(source);

            //Serialize it back to the console
            Assert.AreNotEqual(String.Empty, document.DocumentElement.OuterHtml);
        }

        [Test]
        public void SimpleDocumentManipulation()
        {
            var text = "This is another paragraph.";
            var parser = new HtmlParser();
            var document = parser.ParseDocument("<h1>Some example source</h1><p>This is a paragraph element");
            //Do something with document like the following

            var repr1 = document.DocumentElement.OuterHtml;

            var p = document.CreateElement("p");
            p.TextContent = text;

            document.Body.AppendChild(p);
            var repr2 = document.DocumentElement.OuterHtml;

            Assert.AreNotEqual(repr2, repr1);
            Assert.IsFalse(repr1.Contains(text));
            Assert.IsTrue(repr2.Contains(text));
        }

        [Test]
        public void GettingCertainElements()
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument("<ul><li>First item<li>Second item<li class='blue'>Third item!<li class='blue red'>Last item!</ul>");

            //Do something with LINQ
            var blueListItemsLinq = document.All.Where(m => m.LocalName == "li" && m.ClassList.Contains("blue"));

            //Or directly with CSS selectors
            var blueListItemsCssSelector = document.QuerySelectorAll("li.blue");

            CollectionAssert.AreEqual(new String [] { "Third item!", "Last item!" }, blueListItemsLinq.Select(item => item.Text()));
            CollectionAssert.AreEqual(new String [] { "Third item!", "Last item!" }, blueListItemsCssSelector.Select(item => item.Text()));
        }

        [Test]
        public void GettingSingleElements()
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument("<b><i>This is some <em> bold <u>and</u> italic </em> text!</i></b>");
            var emphasize = document.QuerySelector("em");

            Assert.AreEqual("<em> bold <u>and</u> italic </em>", emphasize.ToHtml());
            Assert.AreEqual(" bold and italic ", emphasize.Text());
            Assert.AreEqual(" bold <u>and</u> italic ", emphasize.InnerHtml);
            Assert.AreEqual("<em> bold <u>and</u> italic </em>", emphasize.OuterHtml);
            Assert.AreEqual(" bold and italic ", emphasize.TextContent);
        }
    }
}
