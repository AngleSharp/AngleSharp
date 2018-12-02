namespace AngleSharp.Core.Tests.Html
{
    using NUnit.Framework;

    [TestFixture]
    public class NoScriptTests
    {
        [Test]
        public void NoScriptEatsTooMuch_Issue681()
        {
            var html = @"<!DOCTYPE html>
<html lang='et'>
<head>
<noscript><iframe src=""//www.googletagmanager.com/ns.html?id=GTM-TJZQZDM""
height=""0"" width=""0"" style=""display:none;visibility:hidden""></iframe></noscript>
<link rel=""canonical"" href=""https://www.k-rauta.ee/"" />
<link href='https://www.k-rauta.ee/assets/themes/krauta_ee/favicon-1db511562d1946de15eae6b21c44f780ef1f4fdb89a2413e091a1ed58b1b3f14.png' rel='apple-touch-icon'>
<link href='https://www.k-rauta.ee/assets/themes/krauta_ee/favicon-1db511562d1946de15eae6b21c44f780ef1f4fdb89a2413e091a1ed58b1b3f14.png' rel='icon' type='image/png'>
<link rel=""search"" type=""application/opensearchdescription+xml"" href=""https://www.k-rauta.ee/search/opensearch.xml"" title=""k-rauta.ee"" />
</head>
<body class='krauta_ee'></body></html>";
            var document = html.ToHtmlDocument(Configuration.Default.WithScripting());
            var elements = document.QuerySelectorAll("head link[href]");
            Assert.AreEqual(4, elements.Length);
        }
    }
}
