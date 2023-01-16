using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Html.Parser;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class MarkupFormatter
    {
        [Test]
        public void NotAllElementsUsingTheFormatter_Issue821()
        {
            var html = @"<html><head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
<meta name=""x-apple-disable-message-reformatting"">
<meta name=""Generator"" content=""Microsoft Word 15 (filtered medium)"">
<!--[if !mso]><style>v\:* {behavior:url(#default#VML);}
o\:* {behavior:url(#default#VML);}
w\:* {behavior:url(#default#VML);}
.shape {behavior:url(#default#VML);}
</style><![endif]--><style>@font-face { font-family: ""Cambria Math"" }
@font-face { font-family: Calibri }
@font-face { font-family: Tahoma }
p.MsoNormal, li.MsoNormal, div.MsoNormal { margin: 0 0 0.0001pt; font-size: 11pt; font-family: ""Calibri"", sans-serif }
a:link, span.MsoHyperlink { mso-style-priority: 99; color: rgba(0, 0, 255, 1); text-decoration: underline }
a:visited, span.MsoHyperlinkFollowed { mso-style-priority: 99; color: rgba(149, 79, 114, 1); text-decoration: underline }
p.msonormal0, li.msonormal0, div.msonormal0 { mso-style-name: msonormal; mso-margin-top-alt: auto; margin-right: 0; mso-margin-bottom-alt: auto; margin-left: 0; font-size: 11pt; font-family: ""Calibri"", sans-serif }
span.EmailStyle18 { mso-style-type: personal-reply; font-family: ""Calibri"", sans-serif; color: rgba(0, 0, 0, 1) }
.MsoChpDefault { mso-style-type: export-only; font-size: 10pt }
@page WordSection1 { size: 612.0pt 792.0pt margin-top: 72pt margin-right: 72pt margin-bottom: 72pt margin-left: 72pt }
div.WordSection1 { page: WordSection1 }</style><!--[if gte mso 9]><xml>
<o:shapedefaults v:ext=""edit"" spidmax=""1026"" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext=""edit"">
<o:idmap v:ext=""edit"" data=""1"" />
</o:shapelayout></xml><![endif]-->
</head>
<body lang=""EN-GB"" link=""blue"" vlink=""#954F72"" style=""min-width:100% !important; padding: 0 !important;margin: 0 !important"">
<meta name=""Generator"" content=""Microsoft Word 15 (filtered medium)"">
<!--[if !mso]><style>v\:* {behavior:url(#default#VML);}
o\:* {behavior:url(#default#VML);}
w\:* {behavior:url(#default#VML);}
.shape {behavior:url(#default#VML);}
</style><![endif]--><style>@font-face { font-family: Wingdings }
@font-face { font-family: ""Cambria Math"" }
@font-face { font-family: Calibri }
p.MsoNormal, li.MsoNormal, div.MsoNormal { margin: 0 0 0.0001pt; font-size: 11pt; font-family: ""Calibri"", sans-serif }
.MsoChpDefault { mso-style-type: export-only }
@page WordSection1 { size: 612.0pt 792.0pt margin-top: 72pt margin-right: 72pt margin-bottom: 72pt margin-left: 72pt }
div.WordSection1 { page: WordSection1 }</style>
<div class=""WordSection1"">
</div>
</div>
</div>
</div>
</body>
</html>
";
            var doc = html.ToHtmlDocument();
            var empty = new EmptyMarkupFormatter();
            var output = doc.Body.ToHtml(empty);
            Assert.IsEmpty(output);
        }

        [Test]
        public void EntitiesDecodingInNoScript_Issue1070()
        {
            var html = @"<html><body><noscript>&lt;/noscript</body></html>";
            var dom = html.ToHtmlDocument();
            var h = dom.Body.ToHtml();
            Assert.AreEqual("<body><noscript>&lt;/noscript</noscript></body>", h);
        }

        [Test]
        public void EntitiesDoubleEncodedInNoScriptWithoutScripting_Issue1070()
        {
            var html = @"<html><body><noscript>&lt;</noscript></body></html>";
            var dom = html.ToHtmlDocument();
            var h = dom.Body.ToHtml();
            Assert.AreEqual("<body><noscript>&lt;</noscript></body>", h);
        }

        [Test]
        public void EntitiesDecodingInNoScriptWithScripting_Issue1070()
        {
            var html = @"<html><body><noscript>&lt;/noscript</body></html>";
            var dom = html.ToHtmlDocument(Configuration.Default.WithScripting());
            var h = dom.Body.ToHtml();
            Assert.AreEqual("<body><noscript>&lt;/noscript</body></html></noscript></body>", h);
        }

        [Test]
        public void EntitiesDoubleEncodedInNoScriptWithScripting_Issue1070()
        {
            var html = @"<html><body><noscript>&lt;</noscript></body></html>";
            var dom = html.ToHtmlDocument(Configuration.Default.WithScripting());
            var h = dom.Body.ToHtml();
            Assert.AreEqual("<body><noscript>&lt;</noscript></body>", h);
        }

        [Test]
        public void EntitiesDoubleEncodedInNoScriptWithoutScriptingAlt_Issue1070()
        {
            var html = @"<html><body><noscript>&lt;</noscript></body></html>";
            var parser = new HtmlParser(new HtmlParserOptions { IsScripting = false }, BrowsingContext.New(Configuration.Default));
            var dom = parser.ParseDocument(html);
            var h = dom.Body.ToHtml();
            Assert.AreEqual("<body><noscript>&lt;</noscript></body>", h);
        }

        [Test]
        public void EntitiesDoubleEncodedInNoScriptWithScriptingAlt_Issue1070()
        {
            var html = @"<html><body><noscript>&lt;</noscript></body></html>";
            var parser = new HtmlParser(new HtmlParserOptions { IsScripting = true }, BrowsingContext.New(Configuration.Default));
            var dom = parser.ParseDocument(html);
            var h = dom.Body.ToHtml();
            Assert.AreEqual("<body><noscript>&lt;</noscript></body>", h);
        }
    }
}
