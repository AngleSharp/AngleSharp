namespace AngleSharp.Core.Tests.Xhtml
{
    using System.IO;
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class PreserveValidXhtml
    {
        [Test]
        public void TestMetaTags()
        {
            var document = (@"<!DOCTYPE html PUBLIC "" -//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                <html xmlns=""http://www.w3.org/1999/xhtml"">
                    <head>
                        <title>test.html</title>
                        <meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
                        <meta name=""viewport"" content=""width=device-width"" />
                    </head>
                    <body>...</body>
                </html>
                ").ToXmlDocument();

            StringWriter sw = new StringWriter();
            document.ToHtml(sw, XHtml.XhtmlMarkupFormatter.Instance);

            string result = sw.ToString();

            int c = 0, i = -1;
            while ( (i = result.IndexOf("/>", i+1)) >= 0)
            {
                c++;

                if ( i>= result.Length )
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
                ").ToXmlDocument();

            StringWriter sw = new StringWriter();
            document.ToHtml(sw, XHtml.XhtmlMarkupFormatter.Instance);

            string result = sw.ToString();

            int c = 0, i = -1;
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
    }
}
