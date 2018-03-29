namespace AngleSharp.Core.Tests.Html
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class InnerText
    {

        // text
        [TestCase("test", "test")]
        [TestCase("te  s\nt", "te  s\nt")]
        // paragraph
        [TestCase("<p>test</p>", "test")]
        [TestCase("<p>test1</p><p>test2</p>", "test1\n\ntest2")]
        // block-level
        [TestCase("<div>test1</div><div>test2</div><div>test3</div>", "test1\ntest2\ntest3")]
        [TestCase(@"test1<span style=""display:block"">test2</span>test3", "test1\ntest2\ntest3")]
        // line break
        [TestCase("test1<br>test2<br>test3", "test1\ntest2\ntest3")]
        // table
        [TestCase("<table><tr><td>1</td><td>2</td></tr><tr><td>3</td><td>4</td></tr></table>", "1\t2\n3\t4")]
        [TestCase("<table><tr><td>1</td><td>2</td></tr><tr><td><table><tr><td>3</td><td>4</td></tr></table></td><td>5</td></tr></table>", "1\t2\n\n3\t4\n\t5")]
        // style visibility
        [TestCase(@"<div hidden style=""display:block"">test1<br>test2<div>test3</div></div>", "test1\ntest2\ntest3")]
        [TestCase(@"<div hidden style=""visibility:visible"">test1<br>test2<div>test3</div></div>", "test1\ntest2\ntest3")]
        [TestCase("<div hidden>test1<br>test2<div>test3</div></div>", "")]
        [TestCase(@"<div style=""display:none"">test1<br>test2<div>test3</div></div>", "")]
        [TestCase(@"<div hidden style=""display:block;visibility:hidden;"">test1<br>test2<div>test3</div></div>", "")]
        [TestCase(@"<div hidden style=""display:none;visibility:visible;"">test1<br>test2<div>test3</div></div>", "")]
        // style text-transform
        [TestCase(@"<span style=""text-transform:uppercase"">test</span>", "TEST")]
        [TestCase(@"<span style=""text-transform:lowercase"">TEST</span>", "test")]
        [TestCase("<span style=\"text-transform:capitalize\">test1 test2\ntest3</span>", "Test1 Test2\nTest3")]
        [TestCase(@"<div style=""text-transform:lowercase"">TEST1<span>TEST2</span></div>", "test1test2")]
        // no css box
        [TestCase("<textarea>test</textarea>", "")]
        [TestCase("<script>test</noscript>", "")]
        [TestCase("<style>test</style>", "")]
        public void GetInnerText(String fixture, String expected)
        {
            var config = Configuration.Default.WithCss();
            var doc = (fixture).ToHtmlDocument(config);

            Assert.AreEqual(expected, doc.Body.InnerText);
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("test", "test")]
        [TestCase("test1\ntest2\ntest3", "test1<br>test2<br>test3")]
        [TestCase("te\rst1\r\ntest2\ntest3\r", "te<br>st1<br>test2<br>test3<br>")]
        [TestCase("te st1\nte  st2\nte   st3", "te st1<br>te  st2<br>te   st3")]
        public void SetInnerText(String fixture, String expectedHtml)
        {
            var doc = ("<div>sample content</div>").ToHtmlDocument();

            doc.Body.InnerText = fixture;

            Assert.AreEqual((fixture ?? "").Replace("\r\n", "\n").Replace("\r", "\n").Trim(), doc.Body.InnerText);
            Assert.AreEqual(expectedHtml, doc.Body.InnerHtml);
        }

    }
}
