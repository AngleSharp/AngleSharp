namespace AngleSharp.Core.Tests.Html
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ScriptDataEscapeTests
    {
        // Reference behaviour taken from Blink; the script data escaped state must survive more
        // than a single character before the nested "<script>".
        [TestCase("<script><!--a<script></script>--></script>", "<!--a<script></script>-->", "")]
        [TestCase("<script><!--ab<script></script>--></script>", "<!--ab<script></script>-->", "")]
        [TestCase("<script><!--abc<script>x</script>y--></script>", "<!--abc<script>x</script>y-->", "")]
        [TestCase("<script><!--ab</script>-->", "<!--ab", "--&gt;")]
        [TestCase("<script><!--ab--></script>after", "<!--ab-->", "after")]
        [TestCase("<script><!--ab<div>--></script>after", "<!--ab<div>-->", "after")]
        public void ScriptDataEscapedMatchesBrowserBehavior_Issue1298(String source, String script, String body)
        {
            var doc = source.ToHtmlDocument();
            Assert.AreEqual(script, doc.QuerySelector("script").TextContent);
            Assert.AreEqual(body, doc.Body.InnerHtml);
        }

        [Test]
        public void ScriptDataEscapedWithDocumentWriteMatchesBrowserBehavior_Issue1298()
        {
            var source = "<script><!--\nif(a){document.write(\"<script>foo</script>\");}\n// -->\n</script><div id=after>AFTER</div>";
            var doc = source.ToHtmlDocument();
            Assert.AreEqual("<!--\nif(a){document.write(\"<script>foo</script>\");}\n// -->\n", doc.QuerySelector("script").TextContent);
            Assert.AreEqual("<div id=\"after\">AFTER</div>", doc.Body.InnerHtml);
        }
    }
}
