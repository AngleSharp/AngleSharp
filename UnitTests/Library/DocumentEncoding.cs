using AngleSharp;
using NUnit.Framework;
using System;
using System.Globalization;

namespace UnitTests.Library
{
    [TestFixture]
    public class DocumentEncodingTests
    {
        [Test]
        public void BaiduEncodingTransformationEnToUtf8VsZhToUtf8()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = new Uri("http://www.baidu.com/", UriKind.Absolute);

                var titleWithEnUs = DocumentBuilder.Html(url, new Configuration { Culture = new CultureInfo("en-US") }).Title;
                var titleWithZhCn = DocumentBuilder.Html(url, new Configuration { Culture = new CultureInfo("zh-CN") }).Title;

                Assert.AreEqual(titleWithEnUs, titleWithZhCn);
            }
        }
    }
}
