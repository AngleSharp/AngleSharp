namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class PrettyMarkupPrinter
    {
        [Test]
        public void ElementsArePrintedWithAdditionalWhitespaceInPrettyFormatter()
        {
            var output = Print("<html><head></head><body><i></i></body></html>");
            var result = @"<html>
	<head></head>
	<body>
		<i></i>
	</body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void ElementsWithAttributesArePrintedLikeNormalElementsInPrettyFormatter()
        {
            var output = Print("<html><head></head><body><script src=\"//ajax.aspnetcdn.com/ajax/bootstrap/3.0.1/bootstrap.min.js\"></script><script src=\"//ajax.aspnetcdn.com/ajax/jquery/1.9.1/jquery.min.js\"></script></body></html>");
            var result = @"<html>
	<head></head>
	<body>
		<script src=""//ajax.aspnetcdn.com/ajax/bootstrap/3.0.1/bootstrap.min.js""></script>
		<script src=""//ajax.aspnetcdn.com/ajax/jquery/1.9.1/jquery.min.js""></script>
	</body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }
        
        [Test]
        public void SelfClosedElementsArePrintedLikeNormalElementsInPrettyFormatter()
        {
            var output = Print("<html><head><meta name=\"this\" content=\"that\"><meta name=\"this\" content=\"that\"></head><body></body></html>");
            var result = @"<html>
	<head>
		<meta name=""this"" content=""that"">
		<meta name=""this"" content=""that"">
	</head>
	<body></body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void StandardWhitespaceIsMergedIntoSerializationOfPrettyFormatter()
        {
            var output = Print(@"
<html>
	<head>
		<meta name=""this"" content=""that"">
		<meta name=""this"" content=""that"">
	</head>
	<body>
	</body>
</html>
");
            var result = @"<html>
	<head>
		<meta name=""this"" content=""that"">
		<meta name=""this"" content=""that"">
	</head>
	<body>
	</body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }
        [Test]
        public void CommentsArePlacedCorrectlyInPrettyFormatter()
        {
            var output = Print("<html><head><!--First--></head><!--Second--><body><!--Third--></body></html><!--Fourth-->");
            var result = @"<html>
	<head>
		<!--First-->
	</head>
	<!--Second-->
	<body>
		<!--Third-->
	</body>
</html>
<!--Fourth-->";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void TextAndElementsAreMixedWithTightSpacingInPrettyFormatter()
        {
            var output = Print(@"<html><head></head><body>hello <i>my</i> friend<u>!</u> How are <strong>you</strong>?</body></html>");
            var result = @"<html>
	<head></head>
	<body>hello
		<i>my</i>
		friend<u>!</u>
		How are
		<strong>you</strong>?</body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void TextAndElementsAreMixedWithInsertedSpacingInPrettyFormatter()
        {
            var output = Print(@"<html><head></head><body> hello <i>my</i> friend<u>!</u> How are <strong>you</strong>? </body></html>");
            var result = @"<html>
	<head></head>
	<body>
		hello
		<i>my</i>
		friend<u>!</u>
		How are
		<strong>you</strong>?
	</body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void SpacedConsecutiveAppendedTextNodesKeepSpace_Issue759()
        {
            var document = "<!DOCTYPE html><html><body></body></html>".ToHtmlDocument();
            var body = document.QuerySelector("body");
            body.AppendChild(document.CreateTextNode("To "));  // With a trailing space
            body.AppendChild(document.CreateTextNode("get ")); // With a trailing space
            body.AppendChild(document.CreateTextNode("her."));
            var output = Print(document);
            var result = @"<!DOCTYPE html>
<html>
	<head></head>
	<body>To get her.</body>
</html>";

            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void SpacedConsecutivePrependedTextNodesKeepSpace()
        {
            var document = "<!DOCTYPE html><html><body></body></html>".ToHtmlDocument();
            var body = document.QuerySelector("body");
            body.AppendChild(document.CreateTextNode("To"));
            body.AppendChild(document.CreateTextNode(" get"));
            body.AppendChild(document.CreateTextNode(" her."));
            var output = Print(document);
            var result = @"<!DOCTYPE html>
<html>
	<head></head>
	<body>To get her.</body>
</html>";
            Assert.AreEqual(result.Replace(Environment.NewLine, "\n"), output);
        }

        [Test]
        public void IgnoringFormating_Issue1131()
        {
            var document = "<!DOCTYPE html>\r\n<html lang=\"en\"><head><title>Some Title</title></head><body><h1>Test</h1>\r\n<pre><code class=\"language-F#\">let a = \"something\"\r\nlet b = \"something else\"\r\nlet c = \"something completely different\" </code></pre>\r\n</body></html>".ToHtmlDocument();
            IEnumerable<INode> toIgnore=new List<INode>() { (INode)(document.QuerySelector("code"))};

            var hm = (toIgnore.ElementAt(0)).ChildNodes;


            var output = Print(document, toIgnore);
        }

        private static String Print(IDocument document, IEnumerable<INode> toIgnore)
        {
            var formatter = new PrettyMarkupFormatter(toIgnore);
            return document.ToHtml(formatter);
        }

        private static String Print(String html)
        {
            var document = html.ToHtmlDocument();
            return Print(document);
        }

        private static String Print(IDocument document)
        {
            var formatter = new PrettyMarkupFormatter();
            return document.ToHtml(formatter);
        }
    }
}
