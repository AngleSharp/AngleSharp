namespace AngleSharp.Core.Tests.Xhtml
{
    using AngleSharp.Dom;
    using AngleSharp.Xhtml;
    using NUnit.Framework;
    using System.IO;
    using System.Linq;

    [TestFixture]
    public class AutoSelectMarkupFormatter
    {
        [Test]
        public void TestMetaTags()
        {
            var swResult = new StringWriter();
            var document = (@"<!DOCTYPE html PUBLIC "" -//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                <html xmlns=""http://www.w3.org/1999/xhtml"">
                    <head>
                        <title>test.html</title>
                        <meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
                        <meta name=""viewport"" content=""width=device-width"" />
                    </head>
                    <body>...</body>
                </html>
                ").ToHtmlDocument();

            document.ToHtml(swResult, new AutoSelectedMarkupFormatter(document.Doctype));

            var result = swResult.ToString();
            var c = 0;
            var i = -1;

            while ((i = result.IndexOf("/>", i + 1)) >= 0)
            {
                c++;

                if (i >= result.Length)
                {
                    Assert.Fail("End of result xhtml reached but not found what we are looking for!");
                }
            }

            Assert.AreEqual(2, c);
            Assert.AreEqual(-1, result.IndexOf("meta>"));
        }
        
        [Test]
        public void TestImgTags()
        {
            var swResult = new StringWriter();
            var document = (@"<!DOCTYPE html PUBLIC "" -//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                <html xmlns=""http://www.w3.org/1999/xhtml"">
                    <head>
                        <title>test.html</title>
                    </head>
                    <body>
                        <div>Image label1</div>
                        <img src=""/images/test1.png"" alt=""My Image1""/>

                        <div>Image label2</div>
                        <img src=""/images/test2.png"" alt=""My Image2""/>
                    </body>
                </html>
                ").ToHtmlDocument();

            document.ToHtml(swResult, new AutoSelectedMarkupFormatter(document.Doctype));

            var result = swResult.ToString();
            var c = 0;
            var i = -1;

            while ((i = result.IndexOf("/>", i + 1)) >= 0)
            {
                c++;

                if (i >= result.Length)
                {
                    Assert.Fail("End of result xhtml reached but not found what we are looking for!");
                }
            }

            Assert.AreEqual(2, c);
            Assert.AreEqual(-1, result.IndexOf("img>"));
        }

        [Test]
        public void TestSomeComment()
        {
            var swResult = new StringWriter();
            var source = @"<html xmlns=""http://www.w3.org/1999/xhtml"">
<head></head>
<!-- Comment -->
<body></body>
</html>";
            var document = source.ToHtmlDocument();
            document.ToHtml(swResult, new AutoSelectedMarkupFormatter(document.Doctype));
            Assert.IsNotNull(swResult.ToString());
        }

        [Test]
        public void TestTrailingComment()
        {
            var swResult = new StringWriter();
            var source = @"<html xmlns=""http://www.w3.org/1999/xhtml"">
<head></head>
<body></body>
</html>
<!-- Comment -->";
            var document = source.ToHtmlDocument();
            document.ToHtml(swResult, new AutoSelectedMarkupFormatter(document.Doctype));
            Assert.IsNotNull(swResult.ToString());
        }

        [Test]
        public void TestRemovingComment()
        {
            var swResult = new StringWriter();
            var source = @"<html xmlns=""http://www.w3.org/1999/xhtml"">
<head></head>
<body></body>
</html>
<!-- Comment -->";
            var document = source.ToHtmlDocument();
            var comments = document.Descendents<IComment>().ToList();

            foreach (var comment in comments)
            {
                comment.Remove();
            }

            document.ToHtml(swResult, new AutoSelectedMarkupFormatter(document.Doctype));
            Assert.IsNotNull(swResult.ToString());
        }
    }
}
